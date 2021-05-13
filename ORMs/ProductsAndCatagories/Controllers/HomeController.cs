using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductModel.Models;
using CatagoryModel.Models;
using ProductCatagory.Models;
using ProductCatagoryContext.Models;
using Microsoft.EntityFrameworkCore;

namespace ProductsAndCatagories.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;

        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("products")]
        public IActionResult ProductList()
        {
            ViewBag.Products = dbContext.Products.OrderBy(product => product.ProductID);
            return View("ProductList");
        }

        [HttpPost("products")]
        public IActionResult CreateProduct(Product newProduct)
        {
            if(ModelState.IsValid)
            {
                dbContext.Add(newProduct);
                dbContext.SaveChanges();
                return RedirectToAction("ProductList");
            }
            else
            {
                ViewBag.Products = dbContext.Products.OrderBy(product => product.ProductID);
                return View("ProductList");
            }
        }

        [HttpGet("catagories")]
        public IActionResult CatagoryList()
        {
            ViewBag.Catagories = dbContext.Catagories.OrderBy(catagory => catagory.CatagoryName);
            return View("CatagoryList");
        }

        [HttpPost("catagories")]
        public IActionResult CreateCatagory(Catagory newCatagory)
        {
            if(ModelState.IsValid)
            {
                //Check for repeat catagory
                List<Catagory> matchingCatagories = dbContext.Catagories.Where(catagory => catagory.CatagoryName == newCatagory.CatagoryName).ToList();
                if(matchingCatagories.Count == 0)
                {
                    dbContext.Add(newCatagory);
                    dbContext.SaveChanges();
                    return RedirectToAction("CatagoryList");
                }
                else
                {
                    ModelState.AddModelError("CatagoryName", "Catagory already exists!");
                    ViewBag.Catagories = dbContext.Catagories.OrderBy(catagory => catagory.CatagoryName);
                    return View("CatagoryList");
                }
            }
            else
            {
                ViewBag.Catagories = dbContext.Catagories.OrderBy(catagory => catagory.CatagoryName);
                return View("CatagoryList");
            }
        }

        [HttpGet("catagories/{id}")]
        public IActionResult CatagoryAndProducts(int id)
        {
            ViewBag.SelectCatagory = dbContext.Catagories.FirstOrDefault(catagory => catagory.CatagoryID == id);
            //Get all Products
            List<Product> AllProducts = dbContext.Products
                .Include(product => product.Catagories)
                    .ThenInclude(catagory => catagory.Catagories)
                .Include(product => product.Catagories)
                    .ThenInclude(catagory => catagory.Products)
                .OrderBy(product => product.ProductID).ToList();
        
            //Two new Lists of products
            List<Product> AssignedProducts = new List<Product>();
            List<Product> UnassignedProducts = new List<Product>();
            //Assign bool value
            bool hasCatagory = false;
            //For each product in all products
            foreach (var product in AllProducts)
            {
                //For each catagory in the product's List<Catagory>
                foreach(var catagory in product.Catagories)
                {
                    //If the Catagory is detected, set hasCatagory = true and put that product in the list of products assigned this catagory
                    if(catagory.CatagoryID == id)
                    {
                        AssignedProducts.Add(product);
                        hasCatagory = true;
                        break;
                    }
                }
                if(!hasCatagory)
                {
                    UnassignedProducts.Add(product);
                }
                hasCatagory = false;
            }
            
            ViewBag.AllProducts = AllProducts;
            ViewBag.AssignedProducts = AssignedProducts;
            ViewBag.UnassignedProducts = UnassignedProducts;
            return View("CatagoryToProduct");
        }

        [HttpPost("catagories/{id}")]
        public IActionResult AssignProduct(int id, ProductsCatagories submitData)
        {
            Catagory SelectCatagory = dbContext.Catagories.FirstOrDefault(catagory => catagory.CatagoryID == id);
            Product SelectProduct = dbContext.Products.FirstOrDefault(product => product.ProductID == submitData.ProductID);

            submitData.CatagoryID = id;
            submitData.Products = SelectProduct;
            submitData.Catagories = SelectCatagory;

            SelectProduct.Catagories.Add(submitData);
            SelectCatagory.Products.Add(submitData);

            dbContext.SaveChanges();
                
            return RedirectToAction("CatagoryAndProducts");
        }

        [HttpGet("products/{id}")]
        public IActionResult ProductsToCatagory(int id)
        {
            ViewBag.SelectProduct = dbContext.Products.FirstOrDefault(product => product.ProductID == id);
            //Get all Products
            List<Catagory> AllCatagories = dbContext.Catagories
                .Include(catagory => catagory.Products)
                    .ThenInclude(products => products.Products)
                .OrderBy(catagory => catagory.CatagoryID).ToList();
        
            //Two new Lists of ctagories
            List<Catagory> AssignedCatagories = new List<Catagory>();
            List<Catagory> UnassignedCatagories = new List<Catagory>();
            //Assign bool value
            bool hasProduct = false;
            //For each product in all products
            foreach (var catagory in AllCatagories)
            {
                //For each product in the catagory's List<Product>
                foreach(var product in catagory.Products)
                {
                    //If the Product is detected, set hasProduct = true and put that catagory in the list of catagories assigned this product
                    if(product.ProductID == id)
                    {
                        AssignedCatagories.Add(catagory);
                        hasProduct = true;
                        break;
                    }
                }
                if(!hasProduct)
                {
                    UnassignedCatagories.Add(catagory);
                }
                hasProduct = false;
            }
            
            ViewBag.AssignedCatagories = AssignedCatagories;
            ViewBag.UnassignedCatagories = UnassignedCatagories;
            return View("ProductToCatagory");
        }

        [HttpPost("products/{id}")]
        public IActionResult AssignCatagory(int id, ProductsCatagories submitData)
        {
            Product SelectProduct = dbContext.Products.FirstOrDefault(product => product.ProductID == id);
            Catagory SelectCatagory = dbContext.Catagories.FirstOrDefault(catagory => catagory.CatagoryID == submitData.CatagoryID);

            submitData.ProductID = id;
            submitData.Products = SelectProduct;
            submitData.Catagories = SelectCatagory;

            SelectProduct.Catagories.Add(submitData);
            SelectCatagory.Products.Add(submitData);

            dbContext.SaveChanges();
                
            return RedirectToAction("ProductsToCatagory");
        }

    }
}
