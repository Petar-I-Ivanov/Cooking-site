using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CookingSite.Models
{
    public class Recipe
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Please enter Category")]
        [Display(Name = "Category")]
        public long CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [Required(ErrorMessage = "Please enter title")]
        [StringLength(100, ErrorMessage = "Don't enter more than 100 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter description")]
        [StringLength(10000, ErrorMessage = "Don't enter more than 10000 characters")]
        public string Desription { get; set; }

        [Required(ErrorMessage = "Please enter number of needed products")]
        [Range(1, int.MaxValue, ErrorMessage = "Enter correct value")]
        [Display(Name = "Needed products")]
        public int NeededProducts { get; set; }

        [Required(ErrorMessage = "Please enter date of publishing" )]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [Display(Name = "Date of publish")]
        public DateTime PublishedDate { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}