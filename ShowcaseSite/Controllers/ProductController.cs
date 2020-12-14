using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShowcaseSite.Data;
using ShowcaseSite.Models;

namespace ShowcaseSite.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Displays a view that lists a page of products
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index(int? id)
        {
            // Getting the page number
            int pageNum = id ?? 1;
            const int PageSize = 3;
            ViewData["CurrentPage"] = pageNum;


            int numProducts = await ProductDb.GetTotalProductsAsync(_context);
            int totalPages = (int)(Math.Ceiling((double)numProducts / PageSize));
            ViewData["MaxPage"] = totalPages;


            //Get sll producrs from databse

            //List<Product> products = _context.Products.ToList();
            List<Product> products = await ProductDb.GetProductsAsync(_context, PageSize, pageNum);

            //Send list of products to view to be diplayed
            return View(products);
        }
    }
}
