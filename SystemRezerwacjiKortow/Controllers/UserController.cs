using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SystemRezerwacjiKortow.Database;
using SystemRezerwacjiKortow.Models;
using Microsoft.AspNet;

namespace SystemRezerwacjiKortow.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration([Bind(Exclude = "IsEmailVerified,ActivationCode")] User user)
        {
            bool Status = false;
            string message = "";

            // Walidacja modelu uzytkownika 
            if (ModelState.IsValid)
            {

                // czy email istnieje
                bool isExist = SqlUser.CheckUserExists(user.Email);
                if (isExist)
                {
                    ViewBag.Message = "Podany adres email już jest w bazie";
                    ModelState.AddModelError("EmailExist", "Podany email już istnieje");
                    return View(user);
                }

                // generowanie kodu aktywacji
                user.ActivationCode = Guid.NewGuid().ToString();

                // hashing hasla
                user.Password = Crypto.Hash(user.Password);

                user.IsEmailVeryfied = false;
                user.RoleID = SqlDatabase.UserRoleId;  // domyślnie dodawany użytkownik o roli zwykłego user'a
                user.CustomerID = SqlDatabase.CustomerAtr;  // dodanie adresu użytkownika - atrapa, żeby baza przyjęła dane

                //zapis do bazy danych
                SqlUser.InsertUser(user);

                // wyslanie emaila do uzytkownika
                SendVerificationLinkEmail(user.Email, user.ActivationCode, user.FirstName);
                message = "Wszystko już prawie gotowe. Link aktywacyjny do konta " +
                    " został wysłany na adres:" + user.Email;
                Status = true;
       
            }
            else
            {
                message = "Nieprawidłowe żądanie";
            }

            ViewBag.Message = message;
            ViewBag.Status = Status;
            return View(user);
        }

        [NonAction]
        public void SendVerificationLinkEmail(string email, string activationCode, string firstName)
        {
            var verifyUrl = "/User/VerifyAccount/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            string subject = "Twoje konto zostało pomyślnie utworzone!";

            string body = "Twoje konto zostało pomyślnie utworzone.<br/>" +
                "Aby aktywować konto kliknij w poniższy link aktywacyjny:" +
                " <br/><a href='" + link + "'>" + link + "</a> ";

            Email.SendEmail(subject, body, email, firstName);
        }

        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            bool Status = false;
            bool find = SqlUser.FindActivationCode(id);

            if (find)
            {
                SqlUser.UpdateUserEmailVeryfied(id);
                Status = true;
            }
            else
            {
                ViewBag.Message = "Nieprawidłowe żądanie";
            }

            ViewBag.Status = Status;
            return View();
        }

        // do logowania
        [HttpGet]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Profile", "User");
            else
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin login, string ReturnUrl = "")
        {
            bool Status = false;
            string message = "";
            if (SqlUser.CheckUserExists(login.Email))
            {
                if (!SqlUser.CheckEmailVeryfied(login))
                {
                    ViewBag.Message = "Najpierw zweryfikuj swój adres email";
                    return View();
                }
                if (string.Compare(Crypto.Hash(login.Password), SqlUser.GetUserPassword(login)) == 0)
                {
                    int timeout = login.RememberMe ? 525600 : 30; // 525600 min = 1 rok, 30 dni czas zycia cookiem
                    var ticket = new FormsAuthenticationTicket(login.Email, login.RememberMe, timeout);
                    string encrypted = FormsAuthentication.Encrypt(ticket);
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted)
                    {
                        Expires = DateTime.Now.AddMinutes(timeout),
                        HttpOnly = true
                    };
                    Response.Cookies.Add(cookie);
                    Status = true;

                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        switch (SqlUser.GetUserRole(login.Email))
                        {
                            case "administrator":
                            //return RedirectToAction("AdminPanel", "Admin");
                            //case "worker":
                            //return RedirectToAction("Profile", "User");
                            case "user":
                                return RedirectToAction("Profile", "User");
                        }
                    }
                }
                else
                {
                    message = "Podano błędne hasło";
                }
            }
            else
            {
                message = "Użytkownik o podanym adresie email nie istnieje";
            }

            ViewBag.Message = message;
            ViewBag.Status = Status;
            return View();
        }

        // do wylogowania 
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            TempData["Logout"] = "Udało sie wylogować";
            return RedirectToAction("Login", "User");
        }

        [Authorize]
        [HttpGet]
        public ActionResult Profile()
        {
            Customer customer = SqlUser.GetCustomer(SqlUser.GetUser(User.Identity.Name));
            //Console.WriteLine(customer.CompanyName);
            ViewBag.user = SqlUser.GetUser(User.Identity.Name);
            return View(customer);
        }
       

        [Authorize]
        [HttpGet]
        public ActionResult EditProfile()
        {
            Customer customer = SqlUser.GetCustomer(SqlUser.GetUser(User.Identity.Name));
            return View(customer);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(Customer customer)
        {
            
            if (ModelState.IsValid)
            {
                SqlUser.AddModyfyAddress(customer, User.Identity.Name);
            }
            else
            {
                return RedirectToAction("EditProfile", "User");
                
            }
            return RedirectToAction("Profile", "User");
        }

    }
}