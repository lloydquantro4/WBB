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
    public class CheckingAccountController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: CheckingAccount
        public ActionResult Index()
        {
            return View();
        }

        // GET: CheckingAccount/Details by user
        public ActionResult Details()
        {
            if (User.Identity.Name == "admin@webbank.com")
            {
                return RedirectToAction("AllAccounts");
            }
            var userId = User.Identity.GetUserId();
            var checkingAccount = db.CheckingAccounts.Where(c => c.ApplicationUserId == userId).First();
            return View(checkingAccount);
        }

        //GET: checkingAccount details for a specific user
        [Authorize(Roles ="Admin")]
        public ActionResult DetailsForAdmin(string id)
        {
            var checkingAccount = db.CheckingAccounts.Find(id);
            return View("Details",checkingAccount);
        }

        [Authorize(Roles ="Admin")] //Get all accounts as an admin
        public ActionResult AllAccounts()
        {
            return View(db.CheckingAccounts.ToList());
        }

        [Authorize(Roles = "Admin")]// GET: CheckingAccount/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CheckingAccount/Create
        [HttpPost]
        public ActionResult Create(CheckingAccount checkingAccount)
        {
            if(ModelState.IsValid){

                db.CheckingAccounts.Add(checkingAccount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();

        }
        //Have 2 edits one for personal info and one for the admin
        // GET: CheckingAccount/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CheckingAccount/Edit/5
        //
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CheckingAccount/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CheckingAccount/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
