using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SystemRezerwacjiKortow.Resources;

namespace SystemRezerwacjiKortow.Models
{
    public class Advertisement
    {
        [Display(Name = "AdvertisementName", ResourceType = typeof(Texts))]
        [StringLength(50)]
        public string Name { get; set; }

        [Display(Name = "StartDate", ResourceType = typeof(Texts))]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Texts), ErrorMessageResourceName = "EnterDate")]
        public DateTime DateFrom { get; set; }

        [Display(Name = "EndDate", ResourceType = typeof(Texts))]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Texts), ErrorMessageResourceName = "EnterDate")]
        public DateTime DateTo { get; set; }

        [Display(Name = "CourtID", ResourceType = typeof(Texts))]
        public int CourtID { get; set; }

        [Display(Name = "DuePayment", ResourceType = typeof(Texts))]
        [DataType(DataType.Currency)]
        public decimal Payment { get; set; }

        [Display(Name = "UserID", ResourceType = typeof(Texts))]
        public int UserID { get; set; }

        // // potrzebne do widoku
        [Display(Name = "CourtNumber", ResourceType = typeof(Texts))]
        public int CourtNumber { get; set; }

        [Display(Name = "CourtName", ResourceType = typeof(Texts))]
        public string CourtName { get; set; }

        [Display(Name = "UserName", ResourceType = typeof(Texts))]
        public string UserName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}