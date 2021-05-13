using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using UserModel.Models;
using ActingUserModel.Models;

namespace ActModel.Models
{
    public class Act
    {
        /*Main Properties*/
        [Key]
        public int ActID {get; set;}

        [Required]
        public string Title {get; set;}

        public string Time {get; set;}

        [FutureDate]
        public DateTime Date {get; set;}

        public int Duration {get; set;}

        [Display(Name = "")]
        public string DurationUnit {get; set;}

        [Required]
        public string Description {get; set;}

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        //Foreign Keys
        public int UserID {get; set;}

        /*
            Navigation properties.
            .Include to access any non-primitive/DateTime data type fields
        */

        public List<ActingUser> Attendees {get; set;}
        public User Creator {get; set;}

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