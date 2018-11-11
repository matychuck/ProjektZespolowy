using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SystemRezerwacjiKortow.Models
{
    public class Contest
    {
        public int ContestID { get; set; }

        [Display(Name = "Nazwa eventu")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Nazwa jest wymagana")]
        public string Name { get; set; }

        [Display(Name = "Data rozpoczecia")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Data jest wymagana")]
        public DateTime DateFrom { get; set; }

        [Display(Name = "Data zakonczenia")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Data jest wymagana")]
        public DateTime DateTo { get; set; }
    }
}