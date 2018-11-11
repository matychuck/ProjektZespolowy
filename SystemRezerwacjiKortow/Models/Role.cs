using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SystemRezerwacjiKortow.Models
{
    public class Role
    {
        public int RoleID { get; set; }

        [Display(Name ="Rola uzytkownika")]
        [StringLength(30)]
        public string RoleName { get; set; }

        [Display(Name = "Czy może rezerwować")]
        [Range(0, 1)]
        public int CanReserve { get; set; }

        [Display(Name = "Czy jest adminem")]
        [Range(0, 1)]
        public int IsAdmin { get; set; }
    }
}