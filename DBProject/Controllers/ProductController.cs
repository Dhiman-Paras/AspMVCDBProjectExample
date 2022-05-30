using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DBProject.Models;

namespace DBProject.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index(string search = "")
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();

            // List<Product> products = db.Products.ToList(); // Reteriveing all rows from database
            //List<Product> products = db.Products.Where(temp=>temp.CategoryID==1 && temp.Price>=50000).ToList();

            List<Product> products = db.Products.Where(temp => temp.ProductName.Contains(search)).ToList();
            ViewBag.search = search;
            return View(products); //  Reteriving Muliple ROws Conditionally
        }

        public ActionResult Details(long id)
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            Product product = db.Products.Where(temp => temp.ProductID == id).FirstOrDefault();
            return View(product);
        } // to show the details of individual product row


        public ActionResult Create()
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Brands = db.Brands.ToList();

            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
           
            // code for storing the image into the database
            if (Request.Files.Count >= 1)
            {
                var file = Request.Files[0];
                var imgByte = new Byte[file.ContentLength ];
                file.InputStream.Read(imgByte, 0, file.ContentLength);

                var base64String = Convert.ToBase64String(imgByte, 0, imgByte.Length);

                product.Photo = base64String;
            }

            //end of the code for storing the image into the database
            db.Products.Add(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(long id)
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            Product existProduct = db.Products.Where(temp => temp.ProductID == id).FirstOrDefault();

            ViewBag.Categories = db.Categories.ToList();// for dynamic dropdown to display the name wrt their IDs
            ViewBag.Brands = db.Brands.ToList(); // for dynamic dropdown to display the name wrt thier IDs

            return View(existProduct);
        }


        [HttpPost]
        public ActionResult Edit(Product p)
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();


            // code for stroing the image
            if (Request.Files.Count >= 1) {
                var file = Request.Files[0];
                var imgBytes = new Byte[file.ContentLength];
                file.InputStream.Read(imgBytes, 0, file.ContentLength);

                var base64String = Convert.ToBase64String(imgBytes,0, file.ContentLength);

                p.Photo = base64String;
            }
            //end of the code for storing the img


            Product existProduct = db.Products.Where(temp => temp.ProductID == p.ProductID).FirstOrDefault();

            existProduct.ProductName = p.ProductName;
            existProduct.Price = p.Price;
            existProduct.DateOfPurchase = p.DateOfPurchase;
            existProduct.CategoryID = p.CategoryID;
            existProduct.AvailabilityStatus = p.AvailabilityStatus;
            existProduct.BrandID = p.BrandID;
            existProduct.Active = p.Active;
            existProduct.Photo = p.Photo;// to edit the photo

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(long id)
        {

            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            Product product = db.Products.Where(temp => temp.ProductID == id).FirstOrDefault();
            return View(product);
        }
        [HttpPost]
        public ActionResult Delete(long id, Product p)
        {

            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            Product product = db.Products.Where(temp => temp.ProductID == id).FirstOrDefault();
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}