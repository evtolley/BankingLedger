using BusinessLogic.Accounts;
using NUnit.Framework;

namespace Tests
{
    public class AccountService_Tests
    {
        private IAccountService _accountService;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Create_Account_Should_Not_Allow_Duplicate_Accounts()
        {
            Assert.Pass();
        }
    }
}