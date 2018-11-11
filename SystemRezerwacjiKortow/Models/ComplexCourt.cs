using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SystemRezerwacjiKortow.Models
{
    public class ComplexCourt
    {
        [Display(Name = "Nazwa kompleksu")]
        [StringLength(50)]
        public string RoleName { get; set; }

        [Display(Name = "Miasto")]
        [StringLength(50)]
        public string City { get; set; }

        [Display(Name = "Miasto")]
        [StringLength(50)]
        public string Street { get; set; }

        [Display(Name = "Miasto")]
        [StringLength(6)]
        public string ZipCode { get; set; }
    }
}