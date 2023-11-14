using LibraryManagementSystem;

namespace Library_login_Nunit
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            AdminOpr adminOpr = new AdminOpr("1111", "password");

            Assert.AreEqual(true,adminOpr.adminLogin(), "Wrong password or id");
        }
    }
}