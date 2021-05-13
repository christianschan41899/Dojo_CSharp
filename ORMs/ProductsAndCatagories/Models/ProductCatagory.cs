using System;
using System.ComponentModel.DataAnnotations;
using ProductModel.Models;
using CatagoryModel.Models;

namespace ProductCatagory.Models
{
    public class ProductsCatagories
    {
        /*Main Properties*/
        [Key]
        public int ProductCatagoryID {get; set;}

        [Required]
        [Display(Name="Product")]
        public int ProductID {get; set;}

        [Required]
        [Display(Name="Catagory")]
        public int CatagoryID {get; set;}

        /*Navigation Properties*/

        public Product Products {get; set;}
        public Catagory Catagories {get; set;}
    }
}