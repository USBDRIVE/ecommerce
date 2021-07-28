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
    }
}
