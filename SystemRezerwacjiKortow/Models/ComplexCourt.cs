using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SystemRezerwacjiKortow.Resources;

namespace SystemRezerwacjiKortow.Models
{
    public class ComplexCourt
    {
        [Display(Name = "ComplexName", ResourceType = typeof(Texts))]
        [StringLength(50)]
        public string ComplexName { get; set; }

        [Display(Name = "City", ResourceType = typeof(Texts))]
        [StringLength(50)]
        public string City { get; set; }

        [Display(Name = "Street", ResourceType = typeof(Texts))]
        [StringLength(50)]
        public string Street { get; set; }

        [Display(Name = "PostalCode", ResourceType = typeof(Texts))]
        [StringLength(6)]
        public string ZipCode { get; set; }

        public int FirstWinterSeasonMonth { get; set; } // start sezonu ziomoweo - miesiąc

        public int LastWinterSeasonMonth { get; set; }  // koniec sezonu zimowego - miesiąc
    }
}