using System.ComponentModel.DataAnnotations;

namespace UserModel.Models
{
    public class User
    {
        [Required]
        [MinLength(2)]
        [Display(Name = "Your Name")]
        public string Name {get; set;}

        [Required]
        [Display(Name = "Dojo Location")]
        public string Location {get; set;}

        [Required]
        [Display(Name = "Favorite Language")]
        public string FavLang {get; set;}

        [MinLength(20)]
        [Display(Name = "Comment (optional)")]
        public string Comment {get; set;}
    }
}