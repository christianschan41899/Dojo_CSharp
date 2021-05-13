using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ProductCatagory.Models;

namespace CatagoryModel.Models
{
    public class Catagory
    {
        /* Main Properties*/
        [Key]
        public int CatagoryID {get; set;}

        [Required]
        [Display(Name="Name")]
        public string CatagoryName {get; set;}

        /*Navigation Properties*/
        public List<ProductsCatagories> Products {get; set;} = new List<ProductsCatagories>();
    }
}