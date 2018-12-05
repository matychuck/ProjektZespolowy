using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SystemRezerwacjiKortow.Database;
using SystemRezerwacjiKortow.Models;

namespace SystemRezerwacjiKortow.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Status = true;
            SqlDatabase.init();  // ustawianie wartości początkowych zmiennych
            //SqlTesty.Testy();   // do testowania bazy danych

            // List<Customer> customers = SqlDatabase.GetCustomers();
            //List<User> users = SqlUser.GetUsers();
            // List<Court> courts = SqlCourt.GetCourts();
            // List<Gear> gears = SqlGear.GetGears();
            List<OpeningHours> openingHours = SqlCompany.GetOpeningHours();
            return View(openingHours);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}