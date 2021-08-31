using ecommerce.Data;
using ecommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce.Controllers
{
    public class CartController : Controller
    {
        private readonly ProductContext _context;
        private readonly IHttpContextAccessor _httpContext;
        public CartController(ProductContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }
        /// <summary>
        /// Add a product to the shopping cart
        /// </summary>
        /// <param name="id">The id of the product to add</param>
        /// <returns></returns>
        public async Task<IActionResult> Add(int id) // id of product to add
        {
            //get product from database
            Product p = await ProductDb.GetProductAsync(_context, id);
            const string CartCookie = "CartCookie";

            //Get existing cart items
            string existingItems = _httpContext.HttpContext.Request.Cookies[CartCookie];
            List<Product> cartProducts = new List<Product>();
            if(existingItems != null)
            {
                 cartProducts = JsonConvert.DeserializeObject<List<Product>>(existingItems);
            }
            // Add current product to existing cart
            cartProducts.Add(p);

            // Add proudct to cart cookie
            string data = JsonConvert.SerializeObject(cartProducts);
            CookieOptions options = new CookieOptions()
            {
                Expires = DateTime.Now.AddDays(300),
                Secure = true,
                IsEssential = true
            };


            _httpContext.HttpContext.Response.Cookies.Append(CartCookie, data, options);

            //redirect back to previous page

            return RedirectToAction("Index","Product");
        }
        public IActionResult Summary()
        {
            //display all products in shopping cart cookie
            return View();
        }
    }
}
