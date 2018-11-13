using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SystemRezerwacjiKortow.Models
{
    public class CyclicReservation
    {
        public int CyclicReservationID { get; set; }

        [Display(Name = "Opis")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Imię jest wymagane")]
        public string FirstName { get; set; }

        [Display(Name = "Dzień tygodnia")]
        [Range(1, 7)]
        public int DayOfWeek { get; set; }

        [Display(Name = "Dzień miesiąca")]
        [Range(1, 31)]
        public int DayOfMonth { get; set; }

        [Display(Name = "Częstotliwość")]
        public int DayInterval { get; set; }

        [Display(Name = "Godzina")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Time { get; set; }

        [Display(Name = "Data rozpoczecia")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateStart { get; set; }

        [Display(Name = "Data zakonczenia")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateStop { get; set; }

        [Display(Name = "Należna płatność")]
        [DataType(DataType.Currency)]
        public decimal Payment { get; set; }

        [Display(Name = "Numer sprzętu")]
        public int GearID { get; set; }

        [Display(Name = "Ilość sprzętu")]
        public int GearAmount { get; set; }

        [Display(Name = "Numer kortu")]
        public int CourtID { get; set; }

        [Display(Name = "Data anulowania rezerwacji")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateOfCancel { get; set; }

    }
}