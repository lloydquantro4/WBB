using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBank.Models;

namespace WebBank.Services
{
    public class CheckingAccountService
    {
        private ApplicationDbContext db;

        public CheckingAccountService(ApplicationDbContext dbContext)
        {
            db = dbContext;
        }

        public void CreateCheckingAccount(string firstName, string lastName, string userId, decimal initialBalance)
        {
            var db = new ApplicationDbContext();
            var accountNum = (123456 + db.CheckingAccounts.Count()).ToString().PadLeft(10, '0');
            var checkingAccountNew = new CheckingAccount { FirstName = firstName, LastName = lastName, AccountNumber = accountNum, Balance = 0, ApplicationUserId = userId };
            db.CheckingAccounts.Add(checkingAccountNew);
            db.SaveChanges();

        }
    }
}