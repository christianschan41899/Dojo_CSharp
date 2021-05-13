using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using UserModel.Models;
using ActModel.Models;

namespace ActingUserModel.Models
{
    public class ActingUser
    {
        [Key]
        public int ActingUserID {get; set;}

        //Foreign Keys
        public int UserID {get; set;}

        public int ActID {get; set;}

        /*
            Navigation properties.
            .Include to access any non-primitive/DateTime data type fields
        */

        public User Attendee {get; set;}
        public Act Item {get; set;}
    }
}