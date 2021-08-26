using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Add(int id) // id of product to add
        {
            //get product from database
            // Add proudct to cart cookie

            //redirect back to previous page
            return View();
        }
        public IActionResult Summary()
        {
            //display all products in shopping cart cookie
            return View();
        }
    }
}
