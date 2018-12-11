using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SystemRezerwacjiKortow;
using System.Web.Security;
using System.Web.Mvc;
using SystemRezerwacjiKortow.Database;

namespace SystemRezerwacjiKortow
{
    public static class GethUserRole
    {
        public static string GetUserRole(this HtmlHelper html)
        {
            string CurrentUserEmail = HttpContext.Current.User.Identity.Name.ToString();
            string CurrentUserRole = SqlUser.GetUserRole(CurrentUserEmail);
            return CurrentUserRole;
        }
    }
}