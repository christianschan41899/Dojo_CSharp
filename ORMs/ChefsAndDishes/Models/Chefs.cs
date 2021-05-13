using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ChefModel.Models
{
    public class Chef
    {
        [Key]
        public int ChefId {get; set;}

        [Required]
        [Display(Name = "First Name")]
        public string FirstName {get; set;}

        [Required]
        [Display(Name = "Last Name")]
        public string LastName {get; set;}

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Birthday")]
        [PastDate]
        public DateTime DateOfBirth {get; set;}

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        //Navigation property for List of dishes made by Chef
        //Instantiate new list immediately, else it becomes null
        public List<Dish> ChefDishes {get; set;} = new List<Dish>();
    }

    public class Dish
    {
        [Key]
        public int DishId { get; set; }

        [Required]
        [Display(Name = "Name of Dish")]
        public string Name { get; set; }

        //Whoops accidentally left in. 
        [Display(Name = "Chef's Name")]
        public string Chef { get; set; }

        [Required]
        [Range(1, 6)]
        public int Tastiness { get; set; }

        [Required]
        [Range(1, 100000)]
        [Display(Name = "No. of Calories")]
        public int Calories { get; set; }

        [Required]
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Chef")]
        public int ChefId {get; set;}

        //Navigator proprety to find Dish's creator
        public Chef Creator {get; set;}
    }

    public class MyContext : DbContext 
    { 
        public MyContext(DbContextOptions options) : base(options) { }
        public DbSet<Chef> Chefs { get; set; }
        public DbSet<Dish> Dishes { get; set; }
    }

    public class PastDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //convert object to DateTime
            DateTime _dob = Convert.ToDateTime(value);

            //DateTime has a built in .Compare(d1, d2) function that will return int<0 if d1 is earlier than d2
            int result = DateTime.Compare(_dob, DateTime.Now);

            //Look for result>0 for success, otherwise give error message
            if(result<0)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Only past dates are allowed!");
            }
        }
    }
}
