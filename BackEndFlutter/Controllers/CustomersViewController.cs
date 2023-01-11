using BackEndFlutter.Models.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndFlutter.Controllers
{
    public class CustomersViewController : Controller
    {
        private readonly ProjectflutterContext _db;
        public CustomersViewController(ProjectflutterContext db)
        {
            _db = db;
        }
        // GET: CustomersViewController
        [HttpGet]
        public ActionResult Index()
        {
            var data =_db.Customers.ToList();
            return View(data);
        }

        // GET: CustomersViewController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CustomersViewController/Create
        public ActionResult Create()
        {
           
            return View();
        }

        // POST: CustomersViewController/Create
        [HttpPost]
        public ActionResult Create(Customers data)
        {
            try
            {
                _db.Customers.Add(data);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomersViewController/Edit/5
        public ActionResult Edit(int id)
        {
            var data = _db.Customers.Find(id);
            return View(data);
        }

        // POST: CustomersViewController/Edit/5
        [HttpPut]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customers data)
        {
            try
            {
                _db.Customers.Update(data);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }



        // GET: CustomersViewController/Delete/5
        public ActionResult Delete(int? id)
        {
            var delete = _db.Customers.Find(id);
            _db.Customers.Remove(delete);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // POST: CustomersViewController/Delete/5
        
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
