using ecommerce.Data;
using ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce.Controllers
{
       
    public class ProductController : Controller
    
    {
        private readonly ProductContext _context;
        public ProductController(ProductContext context)
        {
            _context = context;
        }
         /// <summary>
        /// Displays a view that lists all products
        /// </summary>

        public IActionResult Index()
        {
            //get all products from database
            //List<Product> products = _context.Products.ToList();
            List<Product> products = 
                    (from p in _context.Products
                    select p).ToList();

            //send list of products to view to be dispalyed
            return View(products);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Product p)
        {
            if (ModelState.IsValid)
            {
                //Add to DB
                _context.Products.Add(p);
                _context.SaveChanges();

                TempData["Message"] = $"{p.ProductId}{p.Title} was added successfully!";

                // redirect back to catalog page
                return RedirectToAction("Index");
            }
            return View();

        }
    }
}
