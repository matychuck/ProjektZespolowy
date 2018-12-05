using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SystemRezerwacjiKortow.Models;

namespace SystemRezerwacjiKortow.Database
{
    public static class SqlTesty
    {
        public static void Testy()
        {
            // testy usera
            //testInsertUser();
            //testAddAddress();
            //SqlUser.CheckUserExists("alamakota@onet.pl");
            //SqlUser.CheckUserExists("admin@wp.pl");
            //testCheckEmailVeryfied();

            // testy kortu
            //testAddModyfyCourt();
        }
        #region User
        private static void testInsertUser()
        {
            User user = new User();
            user.FirstName = "Test15";
            user.Surname = "Test15";
            user.Email = "Test15";
            user.DateOfBirth = DateTime.Now;
            user.Password = "******";
            user.CustomerID = SqlDatabase.CustomerAtr;
            user.RoleID = SqlDatabase.UserRoleId;
            user.ActivationCode = Guid.NewGuid().ToString();

            SqlUser.InsertUser(user);
        }

        private static void testAddAddress()
        {
            User user = new User();
            user.UserID = 8;

            Customer customer = new Customer();
            customer.CompanyName = "Test";
            customer.City = "Test";
            customer.Street = "Test";
            customer.ZipCode = "00-000";
            customer.DiscountValue = 0;

            SqlUser.AddModyfyAddress(customer, user);
        }

        private static void testCheckEmailVeryfied()
        {
            UserLogin userLogin = new UserLogin();
            userLogin.Email = "admin@wp.pl";

            SqlUser.CheckEmailVeryfied(userLogin);
        }
        #endregion

        #region Court
        private static void testAddModyfyCourt()
        {
            Court court = new Court();
            court.CourtID = 0;

            court.CourtNumber = 2;
            court.SurfaceType = "ceglasty";
            court.IsForDoubles = true;
            court.IsCovered = false;
            court.PriceH = 99;
            court.Name = "kort 2";
            

            SqlCourt.AddModifyCourt(court);
        }
        #endregion
    }
}