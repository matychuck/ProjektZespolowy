using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SystemRezerwacjiKortow.Models
{
    public class Court
    {
        public int CourtID { get; set; }

        [Display(Name = "Numer Kortu")]
        public int CourtNumber { get; set; }

        [Display(Name = "Typ powierzchni")]
        [StringLength(50)]
        public string SurfaceType { get; set; }

        [Display(Name = "Czy jest dla debli")]
        [Range(0, 1)]
        public bool IsForDoubles { get; set; }

        [Display(Name = "Czy kort jest zakryty")]
        [Range(0, 1)]
        public bool IsCovered { get; set; }

        [Display(Name = "Cena za godzine")]
        [DataType(DataType.Currency)]
        public decimal PriceH { get; set; }

        [Display(Name = "Nazwa kortu")]
        [StringLength(50)]
        public string Name { get; set; }
    }
}