using DietDiagnosis.Models;
using DietDiagnosis.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DietDiagnosis.Controllers
{
    public class DietPlanController : Controller
    {
        private ApplicationDbContext db;
        public DietPlanController()
        {
            db = new ApplicationDbContext();
        }
        // GET: DietPlan
        public ActionResult Index()
        {
            var appUser = GetUser();
            try
            {
                var dietPlans = db.DietPlans.Where(d => d.AppUserId == appUser.Id).ToList();
                if(dietPlans.Count != 0)
                {
                    return View(dietPlans);

                }
                else
                {
                    return RedirectToAction("Create");
                }
                
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

        // GET: DietPlan/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DietPlan/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserDietViewModel viewModel)
        {
            try
            {
                
                return RedirectToAction("Index", "AppUser");
            }
            catch
            {
                return View();
            }
        }


        // GET: DietPlan/Settings
        public ActionResult Settings()
        {
            var user = GetUser();
            var dietPlan = GetDietPlan(user);
            var viewModel = new UserDietViewModel
            {
                //DietPlan = dietPlan[0],
                AppUser = user,
                DietPreferences = db.DietPreferences.ToList()

            };
            return View(viewModel);
        }

        // POST: DietPlan/Settings
        [HttpPost]
        public ActionResult Settings(UserDietViewModel viewModel)
        {
            try
            {
                
                var userLoggedIn = User.Identity.GetUserId();
                var user = db.AppUsers.SingleOrDefault(a => a.ApplicationUserId == userLoggedIn);
                var dietPlan = db.DietPlans.Where(c => c.AppUserId == user.Id).Single();
                dietPlan.Name = viewModel.DietPlan.Name;
                //dietPlan.AppUser.Preferences = viewModel.AppUser.Preferences;
                //dietPlan.AppUser.Exclusions = viewModel.AppUser.Exclusions;
                db.SaveChanges();
                return RedirectToAction("Create");
            }
            catch
            {
                return View();
            }
        }

        // GET: DietPlan/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DietPlan/Save
        [HttpPost]
        public ActionResult Save(DietPlan dietPlan)
        {
            try
            {
                

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: DietPlan/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DietPlan/Delete/5
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
        private List<DietPlan> GetDietPlan(AppUser user)
        {
            var dietPlan = db.DietPlans.Where(d => d.AppUserId == user.Id).ToList();
            return dietPlan;
        }


    }
}
