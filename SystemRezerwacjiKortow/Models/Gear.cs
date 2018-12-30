using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SystemRezerwacjiKortow.Resources;

namespace SystemRezerwacjiKortow.Models
{
    public class Gear
    {
        public int GearID { get; set; }

        [Display(Name = "Price", ResourceType = typeof(Texts))]
        [DataType(DataType.Currency)]
        //[Required(ErrorMessage = "Cena jest wymagana")]
        public decimal PriceH { get; set; }

        [Display(Name = "GearName", ResourceType = typeof(Texts))]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Nazwa jest wymagana")]
        public string Name { get; set; }

        [Display(Name = "Amount", ResourceType = typeof(Texts))]
        public int Amount { get; set; }
    }
}