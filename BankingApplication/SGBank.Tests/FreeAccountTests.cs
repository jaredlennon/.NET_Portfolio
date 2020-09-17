using NUnit.Framework;
using SGBank.BLL;
using SGBank.BLL.DepositRules;
using SGBank.BLL.WithdrawRules;
using SGBank.Models;
using SGBank.Models.Interfaces;
using SGBank.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.Tests
{
    [TestFixture]
    public class FreeAccountTests
    {
        [Test]
        public void CanLoadFreeAccountTestData()
        {
            AccountManager manager = AccountManagerFactory.Create();

            AccountLookupResponse response = manager.LookupAccount("12345");

            Assert.IsNotNull(response.Account);
            Assert.IsTrue(response.Success);
            Assert.AreEqual("12345", response.Account.AccountNumber);
        }

        [TestCase("12345","Free Account",100,AccountType.Free,250,false)] // fail, too much deposited
        [TestCase("12345", "Free Account",100, AccountType.Free,-100,false)] // fail, negative number deposited
        [TestCase("12345", "Free Account",100, AccountType.Basic,50,false)] // fail, not a free account type
        [TestCase("12345","Free Account",100,AccountType.Free,50,true)] // success
        public void FreeAccountDepositRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, bool expectedResult)
        {
            IDeposit deposit = new FreeAccountDepositRule();

            Account accountForDeposit = new Account();
            accountForDeposit.AccountNumber = accountNumber;
            accountForDeposit.Name = name;
            accountForDeposit.Balance = balance;
            accountForDeposit.Type = accountType;

            AccountDepositResponse response = deposit.Deposit(accountForDeposit, amount);

            Assert.AreEqual(expectedResult, response.Success);
        }

        [TestCase("12345","Free Account",100,AccountType.Free,50,false)] // fail, positive withdraw amount
        [TestCase("12345", "Free Account",100, AccountType.Free,-500,false)] // fail, negative withdraw over limit
        [TestCase("12345", "Free Account",100,AccountType.Basic,-50,false)] // fail, wrong account type
        [TestCase("12345", "Free Account",100, AccountType.Free,-150,false)] // fail, overdraft
        [TestCase("12345", "Free Account",100, AccountType.Free,-50,true)] // success
        public void FreeAccountWithdrawRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, bool expectedResult)
        {
            IWithdraw withdraw = new FreeAccountWithdrawRule();

            Account accountForWithdraw = new Account();
            accountForWithdraw.AccountNumber = accountNumber;
            accountForWithdraw.Name = name;
            accountForWithdraw.Balance = balance;
            accountForWithdraw.Type = accountType;

            AccountWithdrawResponse response = withdraw.Withdraw(accountForWithdraw, amount);

            Assert.AreEqual(expectedResult, response.Success);

        }

    }
}
