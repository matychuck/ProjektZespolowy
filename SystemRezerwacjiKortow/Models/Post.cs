using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SystemRezerwacjiKortow.Resources;

namespace SystemRezerwacjiKortow.Models
{
    public class Post
    {
        [Display(Name = "PostID", ResourceType = typeof(Texts))]
        public int PostID { get; set; }

        [Required]
        [Display(Name = "TitlePL", ResourceType = typeof(Texts))]
        [StringLength(50)]
        public string TitlePL { get; set; }

        [Required]
        [Display(Name = "TitleEN", ResourceType = typeof(Texts))]
        [StringLength(50)]
        public string TitleEN { get; set; }

        [Required]
        [Display(Name = "TitleDE", ResourceType = typeof(Texts))]
        [StringLength(50)]
        public string TitleDE { get; set; }

        [Required]
        [Display(Name = "DescriptionPL", ResourceType = typeof(Texts))]
        [StringLength(300)]
        public string DescriptionPL { get; set; }

        [Required]
        [Display(Name = "DescriptionEN", ResourceType = typeof(Texts))]
        [StringLength(300)]
        public string DescriptionEN { get; set; }

        [Required]
        [Display(Name = "DescriptionDE", ResourceType = typeof(Texts))]
        [StringLength(300)]
        public string DescriptionDE { get; set; }

        [Display(Name = "DateOfInsert", ResourceType = typeof(Texts))]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateOfInsert { get; set; }
    }
}