using System;
using System.ComponentModel.DataAnnotations;


namespace CookingSite.Models
{
    public class Comment
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Please enter Recipe")]
        [Display(Name = "Recipe")]
        public long RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; }

        [Required(ErrorMessage = "Please enter content")]
        [StringLength(200, ErrorMessage = "Don't enter more than 200 characters")]
        public string Content { get; set; }

        [StringLength(50, ErrorMessage = "Don't enter more than 50 characters")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Please enter date of publishing")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [Display(Name = "Date of publish")]
        public DateTime PublishedAt { get; set; }
    }
}