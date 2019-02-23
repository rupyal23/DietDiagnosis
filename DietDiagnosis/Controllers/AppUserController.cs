using DietDiagnosis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DietDiagnosis.Controllers
{
    public class AppUserController : Controller
    {
        ApplicationDbContext db;

        public AppUserController()
        {
            db = new ApplicationDbContext();
        }
        // GET: AppUser
        public ActionResult Index()
        {
            return View();
        }

        // GET: AppUser/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AppUser/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: AppUser/Create
        [HttpPost]
        public ActionResult Create(AppUser user)
        {
            try
            {
                var userFromDb = db.AppUsers.SingleOrDefault(c => c.ApplicationUserId == user.ApplicationUserId);
                userFromDb.FirstName = user.FirstName;
                userFromDb.LastName = user.LastName;
                userFromDb.Age = user.Age;
                userFromDb.Sex = user.Sex;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AppUser/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AppUser/Edit/5
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

        // GET: AppUser/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AppUser/Delete/5
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
