using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SystemRezerwacjiKortow.Models
{
    public class Advertisement
    {
        [Display(Name = "Nazwa kortu")]
        [StringLength(50)]
        public string Name { get; set; }

        [Display(Name = "Data rozpoczecia")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "Data jest wymagana")]
        public DateTime DateFrom { get; set; }

        [Display(Name = "Data zakonczenia")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "Data jest wymagana")]
        public DateTime DateTo { get; set; }

        public int CourtID { get; set; }

        [Display(Name = "Należna płatność")]
        [DataType(DataType.Currency)]
        public decimal Payment { get; set; }
    }
}