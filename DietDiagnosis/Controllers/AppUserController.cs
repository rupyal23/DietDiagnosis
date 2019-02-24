using DietDiagnosis.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DietDiagnosis.Controllers
{
    public class AppUserController : Controller
    {
        private ApplicationDbContext db;

        public AppUserController()
        {
            db = new ApplicationDbContext();
        }
        // GET: AppUser
        public ActionResult Index()
        {
            var user = GetUser();
            if(user.LastName == null)
            {
                return View("Create", user);
            }
            var dietPlans = GetDietPlan(user);
            
            return View(dietPlans);
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
                var userFromDb = db.AppUsers.SingleOrDefault(c => c.Id == user.Id);
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

        public AppUser Update(AppUser user)
        {   
            var userFromDb = db.AppUsers.SingleOrDefault(c => c.ApplicationUserId == user.ApplicationUserId);
            userFromDb.FirstName = user.FirstName;
            userFromDb.LastName = user.LastName;
            userFromDb.Age = user.Age;
            userFromDb.Sex = user.Sex;
            db.SaveChanges();
            return user;
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
        private AppUser GetUser()
        {
            var userLoggedIn = User.Identity.GetUserId();
            var user = db.AppUsers.Single(c => c.ApplicationUserId == userLoggedIn);
            return user;
        }

        private List<DietPlan> GetDietPlan(AppUser user)
        {
            var dietPlan = db.DietPlans.Where(d => d.AppUserId == user.Id).ToList();
            return dietPlan;
        }
    }
}
