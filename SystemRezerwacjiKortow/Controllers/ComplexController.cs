using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SystemRezerwacjiKortow.Database;
using SystemRezerwacjiKortow.Models;

namespace SystemRezerwacjiKortow.Controllers
{
    public class ComplexController : Controller
    {
        List<ComplexCourt> list = SqlDatabase.GetComplexes();
        // GET: Complex
        public ActionResult Index()
        {
            return View(list);
        }

        // GET: Complex/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Complex/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Complex/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Complex/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Complex/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Complex/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Complex/Delete/5
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
    }
}
