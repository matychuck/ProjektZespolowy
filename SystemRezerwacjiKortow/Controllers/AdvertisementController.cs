using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SystemRezerwacjiKortow.Database;
using SystemRezerwacjiKortow.Models;

namespace SystemRezerwacjiKortow.Controllers
{
    public class AdvertisementController : Controller
    {
        // GET: Advertisement
        public ActionResult Index()
        {
            List<Advertisement> ads=null;
            User user = SqlUser.GetUser(User.Identity.Name);
            if(user!=null)
            {
                ads = SqlAdvertisement.GetAdvertisements(user.UserID);
                return View(ads);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
            
        }

        [HttpGet]
        public ActionResult ChooseCourt()
        {
            List<Court> courts = SqlCourt.GetCourts();
            ViewBag.CourtID = new SelectList(courts, "CourtID", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult ChooseCourt([Bind(Include = "CourtID,Name")]Court c)
        {
            int idCourt = c.CourtID;
            string nameCourt = c.Name;
            return RedirectToAction("Create", "Advertisement", new { id = idCourt,name = nameCourt });
        }


        [HttpGet]
        public ActionResult Create(int id, string name)
        {
            ViewBag.CourtID = id;
            ViewBag.NAME = name;
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "Name, DateFrom,DateTo, CourtID")] Advertisement ad)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User user = SqlUser.GetUser(User.Identity.Name);
                    ad.Payment = 0;
                    ad.UserID = user.UserID;
                    SqlAdvertisement.AddModifyAdvertisement(ad);
                    if (user.RoleID == 1)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return RedirectToAction("AdsForUsers");
                    }
                }

                return RedirectToAction("Index","Home");
            }
            catch
            {
                return View(ad);
            }
        }

        public ActionResult Edit(DateTime from, DateTime to, int idCourt)
        {
            User user = SqlUser.GetUser(User.Identity.Name);
            Advertisement ad = SqlAdvertisement.GetAdvertisement(from,to,idCourt);
            ViewBag.Role = user.RoleID;
            return View(ad);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "Name, DateFrom, DateTo, CourtID, Payment, UserID, CourtNumber, CourtName, UserName, Email")] Advertisement ad)
        {
            User user = SqlUser.GetUser(User.Identity.Name);
            try
            {
                SqlAdvertisement.AddModifyAdvertisement(ad);
                if (user.RoleID == 1)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("AdsForUsers");
                }
            }
            catch
            {
                return View(ad);
            }
        }
        public ActionResult Delete(DateTime from, DateTime to, int idCourt)
        {
            Advertisement ad = SqlAdvertisement.GetAdvertisement(from, to, idCourt);
            
            return View(ad);
        }

        [HttpPost]
        public ActionResult Delete(DateTime from, DateTime to, int idCourt, FormCollection collection)
        {
            try
            {
                Advertisement ad = SqlAdvertisement.GetAdvertisement(from, to, idCourt);
                User user = SqlUser.GetUser(User.Identity.Name);
                if(user.RoleID==1)
                {
                    SqlAdvertisement.DeleteAdvertisement(ad, user.UserID);
                }
                else
                {
                    if(ad.Payment==0)
                    {
                        SqlAdvertisement.DeleteAdvertisement(ad, user.UserID);
                    }
                    else
                    {
                        return RedirectToAction("ErrorDelete");
                    }
                }
                if (user.RoleID == 1)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("AdsForUsers");
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ErrorDelete()
        {
            return View();
        }

        public ActionResult AdsForUsers()
        {
            User user = SqlUser.GetUser(User.Identity.Name);
            List<Advertisement> ads = SqlAdvertisement.GetAdvertisements(user.UserID);
            return View(ads);
        }
    }
}