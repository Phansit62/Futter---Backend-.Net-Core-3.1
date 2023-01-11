using BackEndFlutter.Models.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndFlutter.Controllers
{
    public class FoodsViewController : Controller
    {
        // GET: FoodsViewController
        private readonly ProjectflutterContext _db;
        public FoodsViewController(ProjectflutterContext db)
        {
            _db = db;
        }
        public ActionResult Index()
        {
            var data = _db.Foods.Include(x=>x.Catefood).ToList();
            return View(data);
        }

        // GET: FoodsViewController/Details/5
        public ActionResult Details(int id)
        {
            var data = _db.Foods.Find(id);
            return View(data);
        }

        // GET: FoodsViewController/Create
        public ActionResult Create()
        {
            ViewBag.CatefoodId = _db.CategoryFood.Select(x => new SelectListItem { Value = x.CategoryFoodId.ToString(), Text = x.TypeFood });
            return View();
        }

        // POST: FoodsViewController/Create
        [HttpPost]
        public ActionResult Create(Foods data)
        {
            ViewBag.CatefoodId = _db.CategoryFood.Select(x => new SelectListItem { Value = x.CategoryFoodId.ToString(), Text = x.TypeFood });
            try
            {
                _db.Foods.Add(data);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FoodsViewController/Edit/5
        public ActionResult Edit(int id)
        {
            var data = _db.Foods.Find(id);
            return View(data);
        }

        // POST: FoodsViewController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Foods data)
        {
            try
            {
                _db.Foods.Update(data);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            var data = _db.Foods.Find(id);
            _db.Remove(data);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        
    }
}
