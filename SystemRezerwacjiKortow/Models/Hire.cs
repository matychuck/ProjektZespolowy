using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SystemRezerwacjiKortow.Models
{
    public class Hire
    {
        public int HireID { get; set; }

        public int UserID { get; set; }

        [Display(Name = "Data rozpoczecia")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateFrom { get; set; }

        [Display(Name = "Data zakonczenia")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateTo { get; set; }

        [Display(Name = "Należna płatność")]
        [DataType(DataType.Currency)]
        public decimal Payment { get; set; }

        [Display(Name = "Numer sprzętu")]
        public int GearID { get; set; }

        [Display(Name = "Ilość sprzętu")]
        public int GearAmount { get; set; }

        [Display(Name = "Numer kortu")]
        public int CourtID { get; set; }


        [Display(Name = "Data zapłaty")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DatePayment{ get; set; }
    }
}