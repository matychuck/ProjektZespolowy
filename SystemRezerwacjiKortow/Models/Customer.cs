using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SystemRezerwacjiKortow.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }

        [Display(Name = "Nazwa firmy")]
        public string CompanyName { get; set; }

        [Display(Name = "Miasto")]
        [StringLength(50)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Miasto jest wymagane")]
        public string City { get; set; }

        [Display(Name = "Ulica")]
        [StringLength(50)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Ulica jest wymagana")]
        public string Street { get; set; }

        [Display(Name = "Kod pocztowy")]
        [StringLength(6)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Kod pocztowy jest wymagany")]
        public string ZipCode { get; set; }

        [Display(Name = "Zniżka")]
        [DataType(DataType.Currency)]
        public decimal DiscountValue { get; set; }
    }
}