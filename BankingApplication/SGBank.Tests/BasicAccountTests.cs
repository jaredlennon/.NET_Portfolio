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
    public class BasicAccountTests
    {
        [TestCase("33333","Basic Account",100,AccountType.Free,250,false)] // fail, wrong account type
        [TestCase("33333", "Basic Account", 100, AccountType.Basic,-100, false)] // fail, negative number deposited
        [TestCase("33333","Basic Account",100,AccountType.Basic,250,true)] // success
        public void BasicAccountDepositRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, bool expectedResult)
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

        [TestCase("33333","Basic Account",1500,AccountType.Basic,-1000,1500,false)] // fail, withdraw amount exceeds $500 limit
        [TestCase("33333", "Basic Account",100,AccountType.Free,-100,100,false)] // fail, wrong account type
        [TestCase("33333", "Basic Account",100,AccountType.Basic,100,100,false)] // fail, postive number withdrawn
        [TestCase("33333", "Basic Account",150, AccountType.Basic,-50,100,true)] // success
        [TestCase("33333", "Basic Account",100, AccountType.Basic,-150,-60,true)] // success, overdraft fee
        public void BasicAccountWithdrawRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, decimal newBalance, bool expectedResult)
        {
            IWithdraw withdrawTest = new BasicAccountWithdrawRule();

            Account accountForWithdrawal = new Account();
            accountForWithdrawal.AccountNumber = accountNumber;
            accountForWithdrawal.Name = name;
            accountForWithdrawal.Balance = balance;
            accountForWithdrawal.Type = accountType;

            AccountWithdrawResponse response = withdrawTest.Withdraw(accountForWithdrawal, amount);

            Assert.AreEqual(expectedResult, response.Success);            

            if(response.Success == true)
            {
                Assert.AreEqual(newBalance, response.Account.Balance);
            }
        }
    }
}
