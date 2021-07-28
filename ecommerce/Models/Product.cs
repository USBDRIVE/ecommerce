using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce.Models
{
    /// <summary>
    /// A saleable product
    /// </summary>
    public class Product
    {
        [Key] //Make primary key in database
        public int ProductId{ get; set; }
        /// <summary>
        /// consumer facing title of product
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// retail price of the product
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// product category
        /// </summary>
        public string category { get; set; }
    }
}
