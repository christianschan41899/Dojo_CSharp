using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using UserModel.Models;
using WeddingModel.Models;

/*Holds many-to-many between User's attended Weddings Wedding's Users attended*/
namespace AttendWeddingModel.Models
{
    public class AttendWedding
    {
        [Key]
        public int AttendWeddingID {get; set;}
        

        /*Navigation properties*/
        //UserID foreign key
        public int UserID {get; set;}

        //WeddingID Foreign Key
        public int WeddingID {get; set;}
        public Wedding Event {get; set;}
        public User Attendee {get; set;}

    }
}