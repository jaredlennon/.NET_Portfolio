using SGBank.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.Models;
using System.IO;

namespace SGBank.Data
{
    
    public class FileAccountRepository : IAccountRepository
    {        
        string path = @".Accounts.txt";
        string[] accountData = new string[]
        {
            "AccountNumber,Name,Balance,Type",
            "11111,Free Customer,100,F",
            "22222,Basic Customer,500,B",
            "33333,Premium Customer,1000,P"
        };
                     
        private List<Account> GetAllFromFile()
        {
            if(!File.Exists(path))
            {
                File.Create(path).Close();
                File.WriteAllLines(path, accountData);
            }

            string[] rows = File.ReadAllLines(path);
            List<Account> accounts = new List<Account>();

            for (int i = 1; i < rows.Length; i++)
            {
                string[] columns = rows[i].Split(',');
                Account _account = new Account();
                _account.AccountNumber = columns[0];
                _account.Name = columns[1];
                decimal parseResult;
                decimal.TryParse(columns[2], out parseResult);
                _account.Balance = parseResult;

                switch (columns[3])
                {
                    case "F":
                        _account.Type = AccountType.Free;
                        break;
                    case "B":
                        _account.Type = AccountType.Basic;
                        break;
                    case "P":
                        _account.Type = AccountType.Premium;
                        break;
                }
                accounts.Add(_account);
            }
            return accounts;
        }

        public void SaveToFile(List<Account> accounts)
        {
            string header = "AcountNumber,Name,Balance,Type";
            List<string> fileData = new List<string>();
            fileData.Add(header);
            foreach (Account account in accounts)
            {
                fileData.Add(($"{account.AccountNumber},{account.Name},{account.Balance},{account.Type}"));
            }
            File.WriteAllLines(path, fileData);
        }

        public Account LoadAccount(string AccountNumber)
        {
            List<Account> accounts = GetAllFromFile();            
            return accounts.Where(x => x.AccountNumber == AccountNumber).FirstOrDefault(); 
            //use first or default when you only plan to get one object; will return the one object or the default, in this case null
        }
        
        public void SaveAccount(Account account)
        {
            List<Account> accounts = GetAllFromFile();
            Account oldAccount = accounts.Where(x => x.AccountNumber == account.AccountNumber).FirstOrDefault();
            oldAccount.Balance = account.Balance;
            SaveToFile(accounts);
        }
        
    }
}
