using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DBProject.Models;

namespace DBProject.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index(string search="")
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            // List<Category> categories = db.Categories.ToList();
            List<Category> category = db.Categories.Where(temp => temp.CategoryName.Contains(search)).ToList();
            ViewBag.Search = search;
            return View(category);
        }


        public ActionResult Detail(long id)
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            Category category = db.Categories.Where(temp => temp.CategoryID == id).FirstOrDefault();
            return View(category);
        }

        public ActionResult Create(long id) {
            //  EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            db.Categories.Add(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(long id) {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            Category category = db.Categories.Where(temp => temp.CategoryID == id).FirstOrDefault();
            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(Category c) {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            Category category = db.Categories.Where(temp => temp.CategoryID == c.CategoryID).FirstOrDefault();

            category.CategoryName = c.CategoryName;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(long id) {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            Category category = db.Categories.Where(temp => temp.CategoryID == id).FirstOrDefault();
            return View(category);
        }
        [HttpPost]
        public ActionResult Delete(long id, Category c)
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            Category category = db.Categories.Where(temp => temp.CategoryID == id).FirstOrDefault();

            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}