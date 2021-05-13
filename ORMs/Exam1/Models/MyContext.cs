using UserModel.Models;
using ActModel.Models;
using ActingUserModel.Models;
using Microsoft.EntityFrameworkCore;

namespace ActivitiesUsersContext.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users {get; set;}
        public DbSet<Act> Activities {get; set;}
        public DbSet<ActingUser> ActingUsers {get; set;}
    }
}