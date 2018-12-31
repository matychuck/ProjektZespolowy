using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SystemRezerwacjiKortow.Resources;

namespace SystemRezerwacjiKortow.Models
{
    public class Contest
    {
        public int ContestID { get; set; }

        [Display(Name = "EventName", ResourceType = typeof(Texts))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Texts), ErrorMessageResourceName = "EnterName")]
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
    }
}