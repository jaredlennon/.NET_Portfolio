using NUnit.Framework;
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
    public class PremiumAccountTests
    {
        [TestCase("11111","Premium Account",1000,AccountType.Basic,-600,false)] // fail, wrong account type
        [TestCase("11111", "Premium Account",100,AccountType.Premium,-100,false)] // fail, negative number deposited
        [TestCase("11111", "Premium Account", 100, AccountType.Premium, 50, true)] // success
        public void PremiumAccountDepositRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, bool expectedResult)
        {
            IDeposit depositTest = new NoLimitDepositRule();

            Account accountForDeposit = new Account();
            accountForDeposit.AccountNumber = accountNumber;
            accountForDeposit.Name = name;
            accountForDeposit.Balance = balance;
            accountForDeposit.Type = accountType;

            AccountDepositResponse response = depositTest.Deposit(accountForDeposit, amount);

            Assert.AreEqual(expectedResult, response.Success);
        }

        [TestCase("11111", "Premium Account",100,AccountType.Basic,-50,50, false)] // fail, wrong account type
        [TestCase("11111", "Premium Account",100,AccountType.Premium,100,100, false)] // fail, positive number withdrawn
        [TestCase("11111", "Premium Account",150,AccountType.Premium,-50,100, true)] // success
        public void PremiumAccountWithdrawRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, decimal newBalance, bool expectedResult)
        {
            IWithdraw withdrawTest = new PremiumAccountWithdrawRule();

            Account accountForWithdrawal = new Account();
            accountForWithdrawal.AccountNumber = accountNumber;
            accountForWithdrawal.Name = name;
            accountForWithdrawal.Balance = balance;
            accountForWithdrawal.Type = accountType;

            AccountWithdrawResponse response = withdrawTest.Withdraw(accountForWithdrawal, amount);

            Assert.AreEqual(expectedResult, response.Success);

            if (response.Success == true)
            {
                Assert.AreEqual(newBalance, response.Account.Balance);
            }
        }

    }
}
