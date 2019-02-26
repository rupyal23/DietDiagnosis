using DietDiagnosis.Models;
using DietDiagnosis.ViewModels;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
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

        public async Task<ActionResult> GetDietPlan(PreferenceViewModel viewModel)
        {
            var user = GetUser();
            string API = "788ab6dbaea061d5952f619dbf8feb51";
            var dietPlan = GetDietPlan(user);
            var preferences = viewModel.DietPreferences;
            var exclusions = viewModel.Nutrients;

            //Not right - need to correct below line
            int totalMeals = dietPlan[0].NumberOfMeals;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.edamam.com");
                switch (totalMeals)
                {
                    case 1:
                        var response = await client.GetAsync($"search?q=&app_id=6f52fd65&app_key={API}");
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    default:
                        break;

                }
            }
                
            //API call to get plan based on Recipes and user preferences

            return View();
        }


        public async Task<ActionResult> GetRecipe(string input)
        {
            string API = "788ab6dbaea061d5952f619dbf8feb51";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.edamam.com/");
                var response = await client.GetAsync($"search?q={input}&app_id=6f52fd65&app_key={API}");
                response.EnsureSuccessStatusCode();
                var stringResult = await response.Content.ReadAsStringAsync();

                var json = JObject.Parse(stringResult);
            }
                
                return View();
        }

        public async Task<ActionResult> GetRecipeMultipleIngredients(List<string>Ingredients)
        {
            //Add API call to get recipe on multiple ingredients
            return View();
        }

        public async Task<ActionResult> GetFoodInfo(string input)
        {
            //API call to get food nutrition info from USDA
            return View();
        }
    }
}
