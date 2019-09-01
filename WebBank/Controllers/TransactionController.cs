using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBank.Models;

namespace WebBank.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private CheckingAccount checkingAccount = new CheckingAccount();
        // GET: Transaction
        public ActionResult Deposit(int checkingAccountId)
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult Deposit(Transaction transaction)
        {

            var userId = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Transactions.Add(transaction);
                checkingAccount = db.CheckingAccounts.Where(s => s.ApplicationUserId == userId).First();
                checkingAccount.Balance = transaction.Amount + checkingAccount.Balance;
                db.SaveChanges();
                
                return RedirectToAction("Index", "Home");
            }
           
            return View();
        }

        public ActionResult Withdraw(int checkingAccountId)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Withdraw(Transaction transaction){//todo

            var userId = User.Identity.GetUserId(); 
            if(ModelState.IsValid){

                db.Transactions.Add(transaction);
                checkingAccount = db.CheckingAccounts.Where(s => s.ApplicationUserId == userId).First();
                checkingAccount.Balance = checkingAccount.Balance - transaction.Amount;
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }

            return View();
        }

    }
}