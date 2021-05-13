using ProductModel.Models;
using CatagoryModel.Models;
using ProductCatagory.Models;
using Microsoft.EntityFrameworkCore;

namespace ProductCatagoryContext.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options) { }

        public DbSet<Product> Products {get; set;}

        public DbSet<Catagory> Catagories {get; set;}

        public DbSet<ProductsCatagories> ProductsAndCatagories {get; set;}
    }
}