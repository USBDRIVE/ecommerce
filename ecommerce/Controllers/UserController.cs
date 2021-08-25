using ecommerce.Data;
using ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
namespace ecommerce.Controllers
{
    public class UserController : Controller
    {
        private readonly ProductContext _context;
        public UserController(ProductContext context)
        {
            _context = context;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>  Register(RegisterViewModel reg)
        {
            if (ModelState.IsValid)
            {
                //map data to user account instance
                UserAccount acc = new UserAccount()
                {
                    DateOfBirth = reg.DateOfBirth,
                    Email = reg.Email,
                    Password = reg.Password,
                    Username = reg.Username
                };

                //add to database
                _context.UserAccounts.Add(acc);
                await _context.SaveChangesAsync();
                //redirect to homepage
                return RedirectToAction("Index", "Home");
            }
            return View(reg);
        }
        public IActionResult Login()
        {
            //check if user alread logged in
            if (HttpContext.Session.GetInt32("UserId").HasValue)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            UserAccount account =
                await (from u in _context.UserAccounts
                 where (u.Username == model.UsernameOrEmail
                     || u.Email == model.UsernameOrEmail)
                     && u.Password == model.Password
                 select u).SingleOrDefaultAsync();
            if(account == null)
            {
                //credentials did not match


                //custom error message
                ModelState.AddModelError(string.Empty, "Credentials were not found");

                return View(model);
            }
            // Log user into website
            HttpContext.Session.SetInt32("UserId", account.UserId);
            return RedirectToAction("Index", "Home");
        }
       
    }
}
