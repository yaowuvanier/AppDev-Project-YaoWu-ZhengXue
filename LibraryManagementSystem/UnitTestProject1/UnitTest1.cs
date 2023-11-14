using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LibraryManagementSystem;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            AdminOpr adminOpr = new AdminOpr("111111", "pad111");

            Assert.AreEqual(true, adminOpr.adminLogin(), "login successfully");
        }
    }
}
