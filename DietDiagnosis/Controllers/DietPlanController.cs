﻿using DietDiagnosis.Models;
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
        [HttpGet]
        public ActionResult Create()
        {
            var user = GetUser();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(UserDietViewModel viewModel)
        {
            try
            {
                var user = GetUser();
                var dietPlan = new DietPlan();
                dietPlan.Name = viewModel.DietPlan.Name;
                dietPlan.NumberOfMeals = viewModel.DietPlan.NumberOfMeals;
                dietPlan.TotalCalories = viewModel.DietPlan.TotalCalories;
                dietPlan.AppUserId = user.Id;
                db.DietPlans.Add(dietPlan);
                var dietPreferencesList = db.DietPreferences.Where(c => c.IsSelected == true).Select(c => c.Name).ToList();
                var healthLabelsList = db.HealthLabels.Where(d => d.IsSelected == true).Select(c => c.Name).ToList();
               
                var model = new UserDietViewModel
                {
                    AppUser = user,
                    DietPlan = dietPlan,
                    DietPreferences = dietPreferencesList,
                    HealthLabels = healthLabelsList
                };
                db.SaveChanges();
                var plan = await GetDietPlan(model);
                var meals = db.Recipes.Where(c => c.DietPlanId == plan.Id).ToList();
                var dietModel = new DietPlanViewModel
                {
                    DietPlan = plan,
                    Recipe = meals
                };
                return View("ViewPlan", dietModel);
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
            var dietPlan = RetrievePlan(user);
            var viewModel = new PreferenceViewModel
            {   
                DietPreferences = db.DietPreferences.ToList(),
                Nutrients = db.Nutrients.ToList(),
                HealthLabels = db.HealthLabels.ToList()
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
                var dietPlan = RetrievePlan(user);
                List<string> dietPreferences = new List<string>();
                //List<string> dietExclusions = new List<string>();
                List<string> healthLabels = new List<string>();
                dietPreferences.AddRange(viewModel.SelectedPreferences.ToList());
                healthLabels.AddRange(viewModel.SelectedLabels.ToList());
               // dietExclusions.AddRange(viewModel.SelectedNutrients.ToList());
                var preferencesInDb = db.DietPreferences.ToList();
                TogglePreferences(dietPreferences);
                ToggleHealthLabels(healthLabels);
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

        // POST: DietPlan/Edit/5
        [HttpPost]
        public ActionResult Edit(DietPlan dietPlan)
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
        private List<DietPlan> RetrievePlan(AppUser user)
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
        
        public void ResetHealthLabels()
        {
            var healthLabelsInDb = db.HealthLabels.ToList();
            foreach (var item in healthLabelsInDb)
            {
                item.IsSelected = false;
            }
        }

        public void ToggleHealthLabels(List<string> labels)
        {
            ResetHealthLabels();
            var labelsInDb = db.HealthLabels.ToList();
            for (var i = 0; i < labels.Count; i++)
            {
                foreach (var item in labelsInDb)
                {
                    if (item.Name == labels[i])
                        item.IsSelected = true;
                }
            }
        }

        //THINKING OF WRITING THE CORE LOGIC IN HERE
        public void GeneratePlan(UserDietViewModel viewModel)
        {

        }
        //Need to work - get recipe chart on number of meals entered
        public async Task<DietPlan> GetDietPlan(UserDietViewModel viewModel)
        {
            var dietPlan = viewModel.DietPlan;
            try
            {
                string API = "788ab6dbaea061d5952f619dbf8feb51";
                var user = viewModel.AppUser;
                var preferences = viewModel.DietPreferences;
                var healthLabels = viewModel.HealthLabels;
                //var exclusions = viewModel.Nutrients;
                var preferenceString = CreateLabelString(preferences);
                var healthString = CreateHealthString(healthLabels);
                int totalMeals = dietPlan.NumberOfMeals;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://api.edamam.com");
                    Recipe recipe = new Recipe();
                    for (int i = 0; i < totalMeals; i++)
                    {
                        var response = await client.GetAsync($"search?q=&app_id=6f52fd65&app_key={API}&diet={preferenceString}&health={healthString}");
                        response.EnsureSuccessStatusCode();
                        var stringResult = await response.Content.ReadAsStringAsync();
                        var json = JObject.Parse(stringResult);
                        recipe.DietPlanId = dietPlan.Id;
                        recipe.Name = json["hits"][i]["recipe"]["label"].ToString();
                        recipe.Calories = Double.Parse(json["hits"][i]["recipe"]["calories"].ToString());
                        recipe.Calories = Math.Round(recipe.Calories, 2);
                        recipe.Image = json["hits"][i]["recipe"]["image"].ToString();
                        db.Recipes.Add(recipe);
                        db.SaveChanges();
                    // totalCals += recipe.Calories;
                    }  
                    return dietPlan;
                }
            }
            catch(IndexOutOfRangeException)
            {
                return dietPlan;
            }

            //API call to get plan based on Recipes and user preferences


        }

        //Helper method for URL string
        private string CreateLabelString(List<string> label)
        {
           
            string labelString = "";
            for(var i = 0; i < label.Count; i++)
            {
                labelString += label[i].ToLower();
                labelString += "&diet=";
            }
            int index = labelString.LastIndexOf("&");
            var returnedString = labelString.Remove(index, 6);
            return returnedString;
        }

        //Helper method for URL string
        private string CreateHealthString(List<string> list)
        {
            var resultedString = "";
            for (var i = 0; i < list.Count; i++)
            {
                resultedString += list[i].ToLower();
                resultedString += "&health=";
            }
            int index = resultedString.LastIndexOf("&");
            var returnedString = resultedString.Remove(index, 8);
            return returnedString;
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

        public async Task<List<Food>> GetFoods(string input)
        {
            //API call to get food nutrition info from USDA
            string API = "htn2jn3wOXV60cFNLXNgrsfzlC0yhLVUT2HGLFCm";
            Food food = new Food();
            List<Food> foodsList = new List<Food>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.nal.usda.gov/");
                var response = await client.GetAsync($"ndb/search/?format=json&q={input}&sort=n&max=10&offset=0&api_key={API}");
                response.EnsureSuccessStatusCode();
                var stringResult = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(stringResult);
                var listOfItems = json["list"]["item"];
                var listOfNames = listOfItems["name"];
                for(int i = 0; i < 10; i++)
                {
                    food.Name = listOfItems[i]["name"].ToString();
                    food.USDANo = listOfItems[i]["ndbno"].ToObject<int>();
                    food.Manufacturer = listOfItems[i]["manu"].ToString();
                    //db.Foods.Add(food);
                    //db.SaveChanges();
                    foodsList.Add(food);
                }
            }
            return foodsList;
        }

        //Need to work 
        public async Task<Recipe> GetRecipeOnFood(string input)
        {
            string API = "788ab6dbaea061d5952f619dbf8feb51";
            Recipe recipe = new Recipe();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.edamam.com/");
                var response = await client.GetAsync($"search?q={input}&app_id=6f52fd65&app_key={API}");
                response.EnsureSuccessStatusCode();
                var stringResult = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(stringResult);
                recipe.Name = json["hits"][0]["recipe"]["label"].ToString();
                recipe.Calories = Double.Parse(json["hits"][0]["recipe"]["calories"].ToString());
                recipe.Calories = Math.Round(recipe.Calories, 2);
                var dietPlan = RetrievePlan(GetUser());
                //Need to fix
                recipe.DietPlanId = dietPlan[0].Id;
            }
            db.Recipes.Add(recipe);
            db.SaveChanges();
            return recipe;
        }
    }
}
