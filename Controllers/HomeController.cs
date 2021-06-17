using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CRUDelicious.Models;

namespace CRUDelicious.Controllers
{
    public class HomeController : Controller
    {
        private CRUDeliciousContext db;

        public HomeController(CRUDeliciousContext context)
        {
            db = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            List<Dish> allDishes = db.Dishes.ToList();
            return View("Index", allDishes);
        }
        [HttpGet("/add/dish")]
        public IActionResult AddDish()
        {
            return View("NewDish");
        }

        [HttpGet("/read/{dishId}/edit")]
        public IActionResult Edit(int dishId)
        {
            Dish dish = db.Dishes.FirstOrDefault(d => d.DishId == dishId);

            if(dish == null)
            {
                return RedirectToAction("Index");
            }
            return View("EditDish", dish);
        }

        [HttpPost("/dish/{dishId}/update")]
        public IActionResult Update(int dishId, Dish editedDish)
        {
            if (ModelState.IsValid == false)
            {
                editedDish.DishId = dishId;
                return View("EditDish", editedDish);
            }
            Dish dbDish = db.Dishes.FirstOrDefault(d => d.DishId == dishId);
            if(dbDish == null)
            {
                RedirectToAction("Index");
            }

            dbDish.Name = editedDish.Name;
            dbDish.Chef = editedDish.Chef;
            dbDish.Calories = editedDish.Calories;
            dbDish.Description = editedDish.Description;
            dbDish.Tastiness = editedDish.Tastiness;
            dbDish.UpdatedAt = DateTime.Now;

            db.Dishes.Update(dbDish);
            db.SaveChanges();
            return RedirectToAction("Read", new {dishId = dbDish.DishId});
        }

        [HttpPost("/create")]
        public IActionResult Create(Dish newDish)
        {
            if (ModelState.IsValid)
            {
                db.Dishes.Add(newDish);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View("NewDish");
            }
        }
        [HttpGet("/read/{dishId}")]
        public IActionResult Read(int dishId)
        {
            Dish dish = db.Dishes.FirstOrDefault(d => d.DishId == dishId);

            if(dish == null)
            {
                return RedirectToAction("Index");
            }
            return View("ShowDish", dish);
        }
        [HttpPost("/delete")]
        public IActionResult Delete(int dishId)
        {
            Dish dish = db.Dishes.FirstOrDefault(d => d.DishId == dishId);

            if(dish == null)
            {
                return RedirectToAction("Index");
            }
            db.Dishes.Remove(dish);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
