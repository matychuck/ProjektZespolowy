using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SystemRezerwacjiKortow.Database;
using SystemRezerwacjiKortow.Models;

namespace SystemRezerwacjiKortow.Controllers
{
    public class GearController : Controller
    {
        List<Gear> list = SqlDatabase.GetGears();
        // GET: Gear
        public ActionResult Index()
        {
            return View(list);
        }

        // GET: Gear/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Gear/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Gear/Create
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

        // GET: Gear/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Gear/Edit/5
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

        // GET: Gear/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Gear/Delete/5
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
