using Npgsql;
using System.Data;

namespace ReaderBorrowRestAPI.Models
{
    public class DBApplication
    {
        public Response GetBookbyName(NpgsqlConnection con,string name)
        {
            Response response = new Response();
            string Query= "SELECT * FROM book WHERE book_name ='" + name + "'";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(Query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<Book> books = new List<Book>();
            if (dt.Rows.Count > 0)
            {
                Book book = new Book();
                book.Name = (string)dt.Rows[0]["book_name"];
                book.Id = (int)dt.Rows[0]["book_id"];
                book.Count = (int)dt.Rows[0]["book_count"];
                book.Writer = (string)dt.Rows[0]["book_writer"];
                book.Price = (double)dt.Rows[0]["book_price"];
               
               
                book.Surplus = (int)dt.Rows[0]["book_surplus"];

                books.Add(book);
                response.statusCode = 200;
                response.messageCode = "successfully retrieved";
                response.book =book;
                response.books = books;
            }
            else
            {
                response.statusCode = 100;
                response.messageCode = "data couldn't found.. check the id";
                response.books = null;
                response.book = null;
            }
            return response;
        }

        public Response GetBookById(NpgsqlConnection con, int bookId)
        {
            Response response = new Response();

            try
            {
                con.Open();

                string query = "SELECT * FROM book WHERE book_id = @bookId";
                using (NpgsqlCommand command = new NpgsqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@bookId", bookId);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Book book = new Book
                            {
                                Id = (int)reader["book_id"],
                                Name = (string)reader["book_name"],
                                Writer = (string)reader["book_writer"],
                                Price = (double)reader["book_price"],
                                Count = (int)reader["book_count"],
                                Surplus = (int)reader["book_surplus"]
                            };

                            response.statusCode = 200;
                            response.messageCode = "Book found.";
                            response.book = book;
                        }
                        else
                        {
                            response.statusCode = 404;
                            response.messageCode = "Book not found.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.statusCode = 500;
                response.messageCode = "Internal Server Error: " + ex.Message;
            }
            finally
            {
                con.Close();
            }

            return response;
        }
        public Response BorrowBook(NpgsqlConnection con, int userId, int bookId)
        {
            Response response = new Response();

            // Check if the book is available for borrowing
            Response bookResponse = GetBookById(con, bookId);
            if (bookResponse.statusCode == 200)
            {
                Book book = bookResponse.book;

                if (book.Surplus > 0)
                {
                    try
                    {
                        con.Open();

                        using (var transaction = con.BeginTransaction())
                        {
                            // Perform the borrowing operation
                            string borrowCmd = "INSERT INTO BorrowReturnRecord (User_Id, Book_Id, Book_Name, Borrow_Date) " +
                                               "VALUES (@userId, @bookId, @bookName, @borrowDate)";

                            using (NpgsqlCommand borrowCommand = new NpgsqlCommand(borrowCmd, con))
                            {
                                borrowCommand.Parameters.AddWithValue("@userId", userId);
                                borrowCommand.Parameters.AddWithValue("@bookId", bookId);
                                borrowCommand.Parameters.AddWithValue("@bookName", book.Name);
                                borrowCommand.Parameters.AddWithValue("@borrowDate", DateTime.Now);

                                int result = borrowCommand.ExecuteNonQuery();

                                if (result == 1)
                                {
                                    // Update the book surplus count
                                    string updateCmd = "UPDATE book SET book_surplus = book_surplus - 1 WHERE book_id = @bookId";

                                    using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateCmd, con))
                                    {
                                        updateCommand.Parameters.AddWithValue("@bookId", bookId);
                                        updateCommand.ExecuteNonQuery();
                                    }

                                    transaction.Commit();

                                    response.statusCode = 200;
                                    response.messageCode = "Book borrowed successfully!";
                                }
                                else
                                {
                                    transaction.Rollback();
                                    response.statusCode = 500;
                                    response.messageCode = "Failed to borrow the book.";
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        response.statusCode = 500;
                        response.messageCode = "Internal Server Error: " + ex.Message;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
                else
                {
                    response.statusCode = 400;
                    response.messageCode = "All the books have been borrowed.";
                }
            }
            else
            {
                response.statusCode = 404;
                response.messageCode = "Book not found.";
            }

            return response;
        }
    }
}
