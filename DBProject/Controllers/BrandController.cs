using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DBProject.Models;

namespace DBProject.Controllers
{
    public class BrandController : Controller
    {

        public ActionResult Index(string search="")
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            //List<Brand> brands = db.Brands.ToList();
            List<Brand> brands = db.Brands.Where(temp => temp.BrandName.Contains(search)).ToList();
            ViewBag.Search = search;
            return View(brands);
        }
        public ActionResult Detail(long id) {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            Brand brand = db.Brands.Where(temp => temp.BrandID == id).FirstOrDefault();
            return View(brand);
        }

        public ActionResult Create()
        {
         return View();
        }
        [HttpPost]
        public ActionResult Create(Brand brand) {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            db.Brands.Add(brand);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(long id) {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            Brand brand = db.Brands.Where(temp => temp.BrandID == id).FirstOrDefault();
            return View(brand);
        }

        [HttpPost]
        public ActionResult Edit(Brand b)
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            Brand brand = db.Brands.Where(temp => temp.BrandID == b.BrandID).FirstOrDefault();
            brand.BrandName = b.BrandName;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(long id) {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            Brand brand = db.Brands.Where(temp => temp.BrandID == id).FirstOrDefault();
            return View(brand);
        }
        [HttpPost]
        public ActionResult Delete(long id, Brand d)
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            Brand brand = db.Brands.Where(temp => temp.BrandID == id).FirstOrDefault();

            db.Brands.Remove(brand);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}