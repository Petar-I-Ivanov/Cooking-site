using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CookingSite.Models
{
    public class Category
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Please enter title")]
        [StringLength(30, ErrorMessage = "Don't enter more than 30 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter description")]
        [StringLength(500, ErrorMessage = "Don't enter more than 500 characters")]
        public string Desription { get; set; }

        [Required(ErrorMessage = "Please enter ImagePath")]
        [StringLength(500, ErrorMessage = "Don't enter more than 500 characters")]
        [Display(Name = "Image Path")]
        public string ImagePath { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}