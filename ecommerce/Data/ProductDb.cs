using ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce.Data
{
    public static class ProductDb
    {

        /// <summary>
        /// Returns total count of products
        /// </summary>
        /// <param name="_context">Databse context to use</param>
        public static async Task<int> GetTotalProductsAsync(ProductContext _context)
        {
           return await(from p in _context.Products
                  select p).CountAsync();
        }

        /// <summary>
        /// /Get page worth of products
        /// </summary>
        /// <param name="_context">database context</param>
        /// <param name="pageSize">number of products per page</param>
        /// <param name="pageNum">Page of products ot return</param>
        /// <returns></returns>
        public static async Task<List<Product>> 
            GetProductsAsync(ProductContext _context, int pageSize, int pageNum)
        {

            return await (from p in _context.Products
                        orderby p.Title ascending
                        select p)
                        .Skip(pageSize * (pageNum - 1)) // skip must be before take
                        .Take(pageSize)
                        .ToListAsync();
            
        }
        public static async Task<Product> AddProduct(ProductContext _context, Product p)
        {
            //add to db
            _context.Products.Add(p);
            await _context.SaveChangesAsync();
            return p;
        }
        public static async Task <Product> GetProductAsync(ProductContext context, int prodId)
        {
            Product p = await (from products in context.Products
                               where products.ProductId == prodId
                               select products).SingleAsync();
            return p;
        }
    }
}
