using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ReaderBorrowWPF2
{
    /// <summary>
    /// Interaction logic for Reader.xaml
    /// </summary>
    public partial class Reader : Window
    {
        public Reader()
        {
            InitializeComponent();
        }
        private void Button_Click_bookLend(object sender, RoutedEventArgs e)  // borrow
        {
            Reader_Borrow reader_Borrow = new Reader_Borrow();
            Application.Current.MainWindow = reader_Borrow;
            reader_Borrow.Show();
        }
    }
}
