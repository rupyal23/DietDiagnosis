using DietDiagnosis.Models;
using DietDiagnosis.ViewModels;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
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
            var user = GetUser();
            var dietPlanList = RetrievePlan(user);
            var dietPlan = dietPlanList.SingleOrDefault(c => c.Id == id);
            var meals = db.Recipes.Where(c => c.DietPlanId == dietPlan.Id).ToList();
            var nutrient = db.Nutrients.SingleOrDefault(n => n.Min != 0 || n.Max != 0);
            var dietModel = new DietPlanViewModel
            {
                DietPlan = dietPlan,
                Meals = meals,
                Nutrient = nutrient
            };
            return View("ViewPlan", dietModel);
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
                var nutrient = db.Nutrients.Single(n => n.Min != 0 || n.Max != 0);
                nutrient.DietPlanId = dietPlan.Id;

                var model = new UserDietViewModel
                {
                    AppUser = user,
                    DietPlan = dietPlan,
                    DietPreferences = dietPreferencesList,
                    HealthLabels = healthLabelsList,
                    Nutrient = nutrient
                };
                db.SaveChanges();
                var plan = await GetDietPlan(model);
                var meals = db.Recipes.Where(c => c.DietPlanId == plan.Id).ToList();
                var dietModel = new DietPlanViewModel
                {
                    DietPlan = plan,
                    Meals = meals,
                    Nutrient = nutrient
                    
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
                string selectedNutrientName = Request.Form["nutrients"].ToString();
                var user = GetUser();
                var dietPlan = RetrievePlan(user);
                List<string> dietPreferences = new List<string>();
                //List<string> dietExclusions = new List<string>();
                List<string> healthLabels = new List<string>();
                var selectedNutrient = db.Nutrients.SingleOrDefault(c => c.Name == selectedNutrientName);
                ResetNutrientValues();
                selectedNutrient.Min = viewModel.SelectedNutrient.Min;
                selectedNutrient.Max = viewModel.SelectedNutrient.Max;
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
            var user = GetUser();
            var dietPlan = db.DietPlans.SingleOrDefault(c => c.Id == id);
            var viewModel = new UserDietViewModel
            {
                AppUser = user,
                DietPlan = dietPlan
            };
            return View(viewModel);
        }

        // POST: DietPlan/Edit/5
        //Need to put preferences option and then call api
        [HttpPost]
        public ActionResult Edit(UserDietViewModel viewModel)
        {
            try
            {
                var dietPlan = db.DietPlans.SingleOrDefault(c => c.Id == viewModel.DietPlan.Id);
                dietPlan.Name = viewModel.DietPlan.Name;
                dietPlan.NumberOfMeals = viewModel.DietPlan.NumberOfMeals;
                dietPlan.TotalCalories = viewModel.DietPlan.TotalCalories;
                var user = GetUser();
                viewModel.AppUser = user;
                //PUT LOGIC TO UPDATE DIET PLAN
                //API CALL IF PREF CHANGED, MEALS CHANGED
                db.SaveChanges();
               
                var dietPlans = RetrievePlan(user);
                return RedirectToAction("Index", "AppUser", dietPlans);
            }
            catch
            {
                return View();
            }
        }
        //// GET: DietPlan/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: DietPlan/Delete/5
        //[HttpPost]
        public ActionResult Delete(int id)
        {
            //try
            //{
                var dietPlan = db.DietPlans.Single(c => c.Id == id);
                db.DietPlans.Remove(dietPlan);
                var recipeAssociated = db.Recipes.Where(c => c.DietPlanId == dietPlan.Id).ToList();
                db.Recipes.RemoveRange(recipeAssociated);
                var nutrientFromDb = db.Nutrients.SingleOrDefault(c => c.DietPlanId == dietPlan.Id);
                if(nutrientFromDb != null)
                nutrientFromDb.DietPlanId = null;
                db.SaveChanges();
                return RedirectToAction("Index", "AppUser");
            //}
            //catch
            //{
            //    return View("Index", "AppUser");
            //}
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

        public void ResetNutrientValues()
        {
            var nutrients = db.Nutrients.ToList();
            foreach(var item in nutrients)
            {
                item.Min = 0;
                item.Max = 0;
            }
        }

        //Need to work - get recipe chart on number of meals entered
        public async Task<DietPlan> GetDietPlan(UserDietViewModel viewModel)
        {
            var dietPlan = viewModel.DietPlan;
            try
            {
                string API = "788ab6dbaea061d5952f619dbf8feb51";
                var user = viewModel.AppUser;
                //var nutrients = viewModel.Nutrients;
                var preferenceString = CreateLabelString(viewModel.DietPreferences);
                var healthString = CreateHealthString(viewModel.HealthLabels);
                var nutrientString = CreateNutrientString(viewModel.Nutrient);
                int totalMeals = dietPlan.NumberOfMeals;
                List<Nutrient> totalNutrients = db.Nutrients.ToList();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://api.edamam.com");
                    Recipe recipe = new Recipe();
                    var response = new HttpResponseMessage();
                    if (viewModel.Nutrient == null)
                    {
                        response = await client.GetAsync($"search?q=&app_id=6f52fd65&app_key={API}&diet={preferenceString}&health={healthString}");
                    }
                    else
                    {
                        response = await client.GetAsync($"search?q=&app_id=6f52fd65&app_key={API}&diet={preferenceString}&health={healthString}&nutrients{nutrientString}");
                    }
                    response.EnsureSuccessStatusCode();
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(stringResult);
                    recipe.DietPlanId = dietPlan.Id;
                    for (int i = 0; i < totalMeals; i++)
                    {   
                        recipe.Name = json["hits"][i]["recipe"]["label"].ToString();
                        recipe.Calories = Double.Parse(json["hits"][i]["recipe"]["calories"].ToString());
                        recipe.Calories = Math.Round(recipe.Calories, 2);
                        recipe.Image = json["hits"][i]["recipe"]["image"].ToString();
                        recipe.Uri = json["hits"][i]["recipe"]["uri"].ToString();
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
        }

        public List<Nutrient> GetNutrientValues(string result, List<Nutrient> nutrients, int i)
        {
            var json = JObject.Parse(result);
            
            for (int j = 0; j < nutrients.Count; j++)
            {
                try
                {
                    nutrients[j].Value = json["hits"][i]["recipe"]["totalNutrients"][nutrients[j].Symbol]["quantity"].ToObject<double>();
                }
                catch
                {
                    j++;
                }
            }
            return nutrients;
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

        private string CreateNutrientString(Nutrient nutrient)
        {
            var resultedString = "";
            if(nutrient.Max == null)
            {
                resultedString = "%5B"+ nutrient.Symbol+ "%5D=" + nutrient.Min + "%2B";
            }
            else if(nutrient.Min == null)
            {
                resultedString = "%5B" + nutrient.Symbol + "%5D=" + nutrient.Max;
            }
            else
            {
                resultedString = "%5B" + nutrient.Symbol + "%5D=" + nutrient.Min + "-" + nutrient.Max;
            }
            return resultedString;
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
        [HttpPost]
        public async Task<ActionResult> GetFoods(string input)
        {
            //API call to get food nutrition info from USDA
            string API = "htn2jn3wOXV60cFNLXNgrsfzlC0yhLVUT2HGLFCm";
            
            List<Food> foodsList = new List<Food>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.nal.usda.gov/");
                var response = await client.GetAsync($"ndb/search/?format=json&q={input}&max=10&offset=0&api_key={API}");
                response.EnsureSuccessStatusCode();
                var stringResult = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(stringResult);
                var listOfItems = json["list"]["item"];
                for(int i = 0; i < 10; i++)
                {
                    Food food = new Food();
                    food.Name = listOfItems[i]["name"].ToString();
                    food.Name = TrimString(food.Name);
                    food.USDANo = listOfItems[i]["ndbno"].ToObject<int>();
                    food.Manufacturer = listOfItems[i]["manu"].ToString();
                    db.Foods.Add(food);
                    db.SaveChanges();
                    foodsList.Add(food);
                }

            }
            return View("FoodsDisplay",foodsList);
        }

        //Helper method to correct the string
        public string TrimString(string input)
        {
            string result = "";
            if(input.Contains("UPC"))
            {
                int index = input.LastIndexOf(",");
                result = input.Remove(index, 19);
            }
            else
            {
                result = input;
            }
            return result;

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

        //Pass in the food id from display food and make logic to get details on the nutrients
        public async Task<ActionResult> GetNutrientsInfo(int id)
        {
            string API = "htn2jn3wOXV60cFNLXNgrsfzlC0yhLVUT2HGLFCm";
            List<Nutrient> nutrientInfo = new List<Nutrient>();
            var food = db.Foods.SingleOrDefault(c => c.Id == id);
            var input = food.Name;
            var ndbno = food.USDANo;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.nal.usda.gov/");
                var response = await client.GetAsync($"ndb/V2/reports?ndbno={ndbno}&type=b&format=json&api_key={API}");
                response.EnsureSuccessStatusCode();
                var stringResult = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(stringResult);
                var nutrientCount = json["foods"][0]["food"]["nutrients"].Count();
                var nutrientList = json["foods"][0]["food"]["nutrients"].ToList();
                for (int i = 0; i < nutrientCount; i++)
                {
                    Nutrient nutrient = new Nutrient();
                    nutrient.Name = json["foods"][0]["food"]["nutrients"][i]["name"].ToString();
                    nutrient.Value = nutrientList[i]["value"].ToObject<double>();
                    nutrient.Unit = nutrientList[i]["unit"].ToObject<string>();
                    nutrientInfo.Add(nutrient);
                }
            }
            List<DataPoint> dataPoints = new List<DataPoint>();
            for(int i =0; i < nutrientInfo.Count; i++)
            {
                dataPoints.Add(new DataPoint(nutrientInfo[i].Name, nutrientInfo[i].Value));
            }
            
            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
            return View("NutritionalInfo", food);
        }

        public async Task<ActionResult> GetRecipeDetails(string inputUri)
        {
            string uri = EncodeUri(inputUri);
            string API = "788ab6dbaea061d5952f619dbf8feb51";
            List<DataPoint> dataPoints = new List<DataPoint>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.edamam.com/");
                var response = await client.GetAsync($"search?r={uri}&app_id=6f52fd65&app_key={API}");
                response.EnsureSuccessStatusCode();
                var stringResult = await response.Content.ReadAsStringAsync();
                var json = JArray.Parse(stringResult);
                var recipeLabel = json[0]["label"].ToString();
                var recipeIngredients = json[0]["ingredientLines"].ToList();
                var recipeCal = json[0]["calories"].ToObject<double>();
                recipeCal = Math.Round(recipeCal, 2);
                var recipeURL = json[0]["url"].ToString();
                var cookTime = json[0]["totalTime"].ToObject<int>();
                var source = json[0]["source"].ToString();
                //var recipeNutrients = json[0]["totalNutrients"].ToList();
                try
                {
                    //dataPoints.Add(new DataPoint(json[0]["totalNutrients"]["ENERC_KCAL"]["label"].ToString(), json[0]["totalNutrients"]["ENERC_KCAL"]["quantity"].ToObject<double>()));
                    dataPoints.Add(new DataPoint(json[0]["totalNutrients"]["FAT"]["label"].ToString(), json[0]["totalNutrients"]["FAT"]["quantity"].ToObject<double>()));
                    dataPoints.Add(new DataPoint(json[0]["totalNutrients"]["CHOCDF"]["label"].ToString(), json[0]["totalNutrients"]["CHOCDF"]["quantity"].ToObject<double>()));
                    dataPoints.Add(new DataPoint(json[0]["totalNutrients"]["PROCNT"]["label"].ToString(), json[0]["totalNutrients"]["PROCNT"]["quantity"].ToObject<double>()));
                    dataPoints.Add(new DataPoint(json[0]["totalNutrients"]["SUGAR"]["label"].ToString(), json[0]["totalNutrients"]["SUGAR"]["quantity"].ToObject<double>()));
                    dataPoints.Add(new DataPoint(json[0]["totalNutrients"]["FIBTG"]["label"].ToString(), json[0]["totalNutrients"]["FIBTG"]["quantity"].ToObject<double>()));
                    ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
                }
                catch
                {
                    ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
                }
                
                ViewBag.RecipeLabel = recipeLabel;
                ViewBag.RecipeCalories = recipeCal;
                ViewBag.RecipeIngr = recipeIngredients;
                ViewBag.RecipeURL = recipeURL;
                ViewBag.RecipeTime = cookTime;
                ViewBag.RecipeSource = source;
            }
            return View("ViewRecipe");
        }
        
        public string EncodeUri(string uri)
        {
            string tempString = uri.Replace(":", "%3A");
            tempString = tempString.Replace("/", "%2F");
            tempString = tempString.Replace("#", "%23");
            return tempString;
        }
    }
}
