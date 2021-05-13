using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ProductCatagory.Models;

namespace ProductModel.Models
{
    public class Product
    {
        /* Main properties */
        [Key]
        public int ProductID {get; set;}

        [Required]
        [Display(Name="Name")]
        public string ProductName {get; set;}

        [Required]
        public int Price {get; set;}
        
        [Required]
        public string Description {get; set;}

        /*Navigation Properties*/

        public List<ProductsCatagories> Catagories {get; set;} = new List<ProductsCatagories>();
    }
}