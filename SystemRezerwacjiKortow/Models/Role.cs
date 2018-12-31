using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SystemRezerwacjiKortow.Resources;

namespace SystemRezerwacjiKortow.Models
{
    public class Role
    {
        public int RoleID { get; set; }

        [Display(Name = "RoleName", ResourceType = typeof(Texts))]
        [StringLength(30)]
        public string RoleName { get; set; }

        [Display(Name = "CanReserver", ResourceType = typeof(Texts))]
        [Range(0, 1)]
        public int CanReserve { get; set; }

        [Display(Name = "IsAdmin", ResourceType = typeof(Texts))]
        [Range(0, 1)]
        public int IsAdmin { get; set; }
    }
}