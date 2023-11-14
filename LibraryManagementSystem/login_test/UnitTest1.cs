using LibraryManagementSystem;

namespace login_test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }


        [Test]
        public void GoodLogin()
        {
            AdminOpr adminOpr = new AdminOpr("111111", "pad111");

            // Assert.That(adminOpr.adminLogin(), Is.EqualTo(true), "login successfully");

            Assert.AreEqual(true, adminOpr.adminLogin(), "login successfully");
        }
        [Test]
        public void FailedLogin()
        {
            AdminOpr adminOpr = new AdminOpr("1223", "pd11321");

            //Assert.That(adminOpr.adminLogin(), Is.EqualTo(false), "login failed");

            Assert.AreEqual(false, adminOpr.adminLogin(), "login failed");
        }

        [Test]
        public void Test()
        {
            AdminOpr adminOpr = new AdminOpr();
            Assert.AreEqual(6, adminOpr.userAdd1(5), "adding user successfully");

        }
    }
}