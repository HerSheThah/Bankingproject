using NUnit.Framework;
using bankrepository;

namespace bankreptesting
{
    [TestFixture]
    public class Tests
    {
        public static BankRepository bankRepository;
        [SetUp]
        public void Setup()
        {
            bankRepository = new BankRepository();
        }

        [Test]
        public void TestAccnumberpass()
        {
            bool actualres=bankRepository.ValidateAccNum(1234567890);
            bool expres = true;
            Assert.AreEqual(actualres, expres);
        }

        [Test]
        public void TestAccnumberfail()
        {
            var ex = Assert.Throws<BankRepository.AccnumberException>(() => bankRepository.ValidateAccNum(12356));
            string expectedErrorMessage = "Account number should contain 10 digits";
            Assert.AreEqual(ex.Message, expectedErrorMessage);
        }
    }
}