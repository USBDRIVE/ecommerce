using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce.Models
{
    public static class CookieHelper
    {
        const string CartCookie = "CartCookie";
        /// <summary>
        /// returns the current list of cart products if cart is empty and empty list will be returned.
        /// </summary>
        /// <param name="http"></param>
        /// <returns>an empty list if cart is empty</returns>
        public static List<Product> GetCartProducts(IHttpContextAccessor http)
        {
            

            //Get existing cart items
            string existingItems = http.HttpContext.Request.Cookies[CartCookie];
            List<Product> cartProducts = new List<Product>();
            if (existingItems != null)
            {
                cartProducts = JsonConvert.DeserializeObject<List<Product>>(existingItems);
            }
            return cartProducts;
        }

        public static void AddProductToCart(IHttpContextAccessor http, Product p)
        {
            List<Product> cartProducts = GetCartProducts(http);
            cartProducts.Add(p);
            string data = JsonConvert.SerializeObject(cartProducts);
            CookieOptions options = new CookieOptions()
            {
                Expires = DateTime.Now.AddDays(300),
                Secure = true,
                IsEssential = true
            };
            http.HttpContext.Response.Cookies.Append(CartCookie, data, options);
        }
        public static int GetTotalCartProducts(IHttpContextAccessor http)
        {
            List<Product> cartProducts = GetCartProducts(http);
            return cartProducts.Count;
        }
    }
}
