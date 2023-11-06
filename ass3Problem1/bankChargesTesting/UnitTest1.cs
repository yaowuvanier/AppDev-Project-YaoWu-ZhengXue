using ProblemOne.Models;

namespace bankChargesTesting
{
    public class Tests
    {
        private bankCharges _mnc { get; set; } = null;
        [SetUp]
        public void Setup()
        {
            _mnc=new bankCharges(1000,25);
        }

        [Test]
        public void TestBalanceLessThan400NumberchecklessThan20()
        {
            // Set up BankCharges object with a balance less than 400
            _mnc = new bankCharges(300, 19);
            double expectedFee = 26.9; // Expected fee for balance < 400

            // Test if the calculated fee matches the expected fee
            Assert.AreEqual(expectedFee, _mnc.calculateFees(), 0.01);
        }
        [Test]
        public void TestBalanceLessThan400NumberchecklessThan40()
        {
            // Set up BankCharges object with a balance less than 400
            _mnc = new bankCharges(300, 20);
            double expectedFee = 26.6; // Expected fee for balance < 400

            // Test if the calculated fee matches the expected fee
            Assert.AreEqual(expectedFee, _mnc.calculateFees(), 0.01);
        }
        [Test]
        public void TestBalanceLessThan400NumberchecklessThan60()
        {
            // Set up BankCharges object with a balance less than 400
            _mnc = new bankCharges(300, 40);
            double expectedFee = 27.4; // Expected fee for balance < 400

            // Test if the calculated fee matches the expected fee
            Assert.AreEqual(expectedFee, _mnc.calculateFees(), 0.01);
        }
        [Test]
        public void TestBalanceLessThan400NumbercheckMoreThan60()
        {
            // Set up BankCharges object with a balance less than 400
            _mnc = new bankCharges(300, 60);
            double expectedFee = 31; // Expected fee for balance < 400

            // Test if the calculated fee matches the expected fee
            Assert.AreEqual(expectedFee, _mnc.calculateFees(), 0.01);
        }
        [Test]
        public void TestBalanceLessThan300NumberchecklessThan20()
        {
            // Set up BankCharges object with a balance less than 400
            _mnc = new bankCharges(400, 19);
            double expectedFee = 11.9; // Expected fee for balance < 400

            // Test if the calculated fee matches the expected fee
            Assert.AreEqual(expectedFee, _mnc.calculateFees(), 0.01);
        }
        [Test]
        public void TestBalanceLessThan300NumberchecklessThan40()
        {
            // Set up BankCharges object with a balance less than 400
            _mnc = new bankCharges(400,20);
            double expectedFee = 11.6; // Expected fee for balance < 400

            // Test if the calculated fee matches the expected fee
            Assert.AreEqual(expectedFee, _mnc.calculateFees(), 0.01);
        }
        [Test]
        public void TestBalanceLessThan300NumberchecklessThan60()
        {
            // Set up BankCharges object with a balance less than 400
            _mnc = new bankCharges(400, 40);
            double expectedFee = 12.4; // Expected fee for balance < 400

            // Test if the calculated fee matches the expected fee
            Assert.AreEqual(expectedFee, _mnc.calculateFees(), 0.01);
        }
        [Test]
        public void TestBalanceLessThan300NumbercheckMoreThan60()
        {
            // Set up BankCharges object with a balance less than 400
            _mnc = new bankCharges(400, 60);
            double expectedFee = 16; // Expected fee for balance < 400

            // Test if the calculated fee matches the expected fee
            Assert.AreEqual(expectedFee, _mnc.calculateFees(), 0.01);
        }
    }
}