using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SystemRezerwacjiKortow.Resources;

namespace SystemRezerwacjiKortow.Models
{
    // do formularza logowania użytkownika
    public class UserLogin
    {
        public int id { get; set; }

        [Display(Name = "Email")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Texts), ErrorMessageResourceName = "EnterYourEmail")]
        public string Email { get; set; }

        [Display(Name = "Password", ResourceType = typeof(Texts))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Texts), ErrorMessageResourceName = "EnterYourPassword")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "RememberMe", ResourceType = typeof(Texts))]
        public bool RememberMe { get; set; }
    }
}