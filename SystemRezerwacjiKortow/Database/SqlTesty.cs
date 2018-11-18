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

            SqlUser.AddAddress(customer, user);
        }

        private static void testCheckEmailVeryfied()
        {
            UserLogin userLogin = new UserLogin();
            userLogin.Email = "admin@wp.pl";

            SqlUser.CheckEmailVeryfied(userLogin);
        }
        #endregion
    }
}