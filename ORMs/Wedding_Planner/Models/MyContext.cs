using UserModel.Models;
using WeddingModel.Models;
using AttendWeddingModel.Models;
using Microsoft.EntityFrameworkCore;

namespace WeddingPlannerContext.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users {get; set;}
        public DbSet<Wedding> Weddings {get; set;}
        public DbSet<AttendWedding> AttendWeddings {get; set;}
    }
}