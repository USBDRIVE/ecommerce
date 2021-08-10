using ecommerce.Data;
using ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        /// Displays a view that lists a page of products
        /// </summary>

        public async Task<IActionResult> Index(int? id)
        {
            int pageNum = id.HasValue ? id.Value : 1;
            const int pageSize = 3;

            ViewData["CurrentPage"] = pageNum;

            int numProducts = await ProductDb.GetTotalProductsAsync(_context);
            int totalPages = (int)Math.Ceiling((double)numProducts / pageSize);

            ViewData["MaxPage"] = totalPages;




            List<Product> products =
                 await ProductDb.GetProductsAsync(_context, pageSize, pageNum);


            //send list of products to view to be dispalyed
            return View(products);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Product p)
        {
            if (ModelState.IsValid)
            {
                await ProductDb.AddProduct(_context, p);


                TempData["Message"] = $"{p.Title} was added successfully";
                //redirect back to catalog page
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            // get produtct iwth corresponding id
            Product p =
             await (from prod in _context.Products
                    where prod.ProductId == id
                    select prod).SingleAsync();




            // pass product to view


            return View(p);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Product p)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(p).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                ViewData["Message"] = "Product updated successfullly";
            }
            return View(p);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id) 
        {
            Product p = await (from prod in _context.Products
                         where prod.ProductId == id
                         select prod).SingleAsync();
            return View (p);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Product p = 
               await  (from prod in _context.Products
                where prod.ProductId == id
                select prod).SingleAsync();

            _context.Entry(p).State = EntityState.Deleted;


           await  _context.SaveChangesAsync();

            TempData["Message"] = $"{p.Title} was deleted";


            return RedirectToAction("index");
        }
    }
}
