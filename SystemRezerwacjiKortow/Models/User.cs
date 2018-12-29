using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SystemRezerwacjiKortow.Resources;

namespace SystemRezerwacjiKortow.Models
{
    public class User
    {
        public int UserID { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Texts))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Texts), ErrorMessageResourceName = "EnterYourName")]
        public string FirstName { get; set; }

        [Display(Name = "Surname", ResourceType = typeof(Texts))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Texts), ErrorMessageResourceName = "EnterYourSurname")]
        public string Surname { get; set; }

        [Display(Name = "Email")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Texts), ErrorMessageResourceName = "EnterYourEmail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "DateOfBirth", ResourceType = typeof(Texts))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Texts), ErrorMessageResourceName = "EnterYourDateOfBirth")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Password", ResourceType = typeof(Texts))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Texts), ErrorMessageResourceName = "EnterYourPassword")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessageResourceType = typeof(Texts), ErrorMessageResourceName = "ErrorPasswordLength")]
        public string Password { get; set; }

        [Display(Name = "ConfirmPassword", ResourceType = typeof(Texts))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Texts), ErrorMessageResourceName = "EnterYourConfirmPassword")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessageResourceType = typeof(Texts), ErrorMessageResourceName = "ErrorPasswordMatch")]
        public string ConfirmPassword { get; set; }

        public bool IsEmailVeryfied { get; set; }

        [Display(Name = "RoleID", ResourceType = typeof(Texts))]
        [Range(0, 5)]
        public int RoleID { get; set; }

        public int CustomerID { get; set; }

       // [Display(Name = "Adres")]
       // [Required(AllowEmptyStrings = false, ErrorMessage = "Adres jest wymagany")]
       // public string Address { get; set; }

        public string ActivationCode { get; set; }

        [Display(Name = "Role", ResourceType = typeof(Texts))]
        public string RoleName { get; set; }
    }
}