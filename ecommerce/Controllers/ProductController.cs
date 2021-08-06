﻿using ecommerce.Data;
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
        /// Displays a view that lists all products
        /// </summary>

        public async Task<IActionResult> Index()
        {
            //get all products from database
            //List<Product> products = _context.Products.ToList();
            List<Product> products =
                  await (from p in _context.Products
                         select p).ToListAsync();
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
                //add to db
                _context.Products.Add(p);
                await _context.SaveChangesAsync();


                TempData["Message"] = $"{p.ProductId}:{p.Title} was added successfully";
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
        public async Task<IActionResult> Delete(int id) 
        {
            Product p = await (from prod in _context.Products
                         where prod.ProductId == id
                         select prod).SingleAsync();
            return View (p);
        }
    }
}
