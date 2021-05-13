using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using UserModel.Models;
using AttendWeddingModel.Models;

namespace WeddingModel.Models
{
    public class Wedding
    {
        [Key]
        public int WeddingID {get; set;}

        [Required]
        [Display(Name="Wedder One")]
        public string Wedder1 {get; set;}

        [Required]
        [Display(Name="Wedder Two")]
        public string Wedder2 {get; set;}

        [Required]
        [DataType(DataType.Date)]
        [FutureDate]
        public DateTime Date {get; set;}

        [Required]
        public string Address {get; set;}

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        /*Navigation properties*/
        //Foreign Key for Creator
        public int UserID {get; set;}
        //For one-to-many relationship with creator User.
        public User Creator {get; set;} 
        //For Many-to-Many with Attendees to wedding
        public List<AttendWedding> Attendees {get; set;} = new List<AttendWedding>();
    }

    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //convert object to DateTime
            DateTime _dob = Convert.ToDateTime(value);

            //DateTime has a built in .Compare(d1, d2) function that will return int>0 if d1 is later than d2
            int result = DateTime.Compare(_dob, DateTime.Now);

            //Look for result>0 for success, otherwise give error message
            if(result>0)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Only future dates are allowed!");
            }
        }
    }
}