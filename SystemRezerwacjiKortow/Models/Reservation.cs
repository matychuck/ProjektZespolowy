using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SystemRezerwacjiKortow.Models
{
    public class Reservation
    {
        public int ReservationID { get; set; }

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

        [Display(Name = "Data wpłynięcia rezerwacji")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateOfInsert { get; set; }

        [Display(Name = "Data anulowania rezerwacji")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateOfCancel { get; set; }

        [Display(Name = "Czy zakończone")]
        [Range(0, 1)]
        public int IsExecuted { get; set; }

        [Display(Name = "Numer rezerwacji cyklicznej")]
        public int CyclicReservationID { get; set; }

        [Display(Name = "Numer wydarzenia")]
        public int ContestID { get; set; }
    }
}