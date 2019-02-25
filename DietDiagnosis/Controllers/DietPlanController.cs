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
            var user = GetUser();
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserDietViewModel viewModel)
        {
            try
            {
                var user = GetUser();
                var dietPlan = new DietPlan();
                dietPlan.Name = viewModel.DietPlan.Name;
                dietPlan.NumberOfMeals = viewModel.DietPlan.NumberOfMeals;
                dietPlan.AppUserId = user.Id;
                db.DietPlans.Add(dietPlan);
                db.SaveChanges();
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
            var viewModel = new PreferenceViewModel
            {   
                DietPreferences = db.DietPreferences.ToList(),
                Nutrients = db.Nutrients.ToList()
                

            };
            return View(viewModel);
        }

        // POST: DietPlan/Settings
        [HttpPost]
        public ActionResult Settings(PreferenceViewModel viewModel)
        {
            try
            {

                var user = GetUser();
                var dietPlan = GetDietPlan(user);
                List<string> dietPreferences = new List<string>();
                List<string> dietExclusions = new List<string>();
                dietPreferences.AddRange(viewModel.SelectedPreferences.ToList());
                dietExclusions.AddRange(viewModel.SelectedNutrients.ToList());
                var preferencesInDb = db.DietPreferences.ToList();
                TogglePreferences(dietPreferences);
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

        public void TogglePreferences(List<string>Preferences)
        {
            ResetPreferences();
            var preferencesInDb = db.DietPreferences.ToList();
            for (var i = 0; i < Preferences.Count; i++)
            {
                foreach (var item in preferencesInDb)
                {
                    if (item.Name == Preferences[i])
                        item.IsSelected = true;
                }
            }
        }

        public void ResetPreferences()
        {
            var preferencesInDb = db.DietPreferences.ToList();
           
            foreach (var item in preferencesInDb)
            {
                item.IsSelected = false;
            }
            
        }
    }
}
