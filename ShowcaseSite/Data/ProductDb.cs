using Microsoft.EntityFrameworkCore;
using ShowcaseSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShowcaseSite.Data
{
    public class ProductDb
    {
        

        /// <summary>
        /// Return the total count of products
        /// </summary>
        public static async Task<int> GetTotalProductsAsync(ApplicationDbContext _context)
        {

            return await (from p in _context.Products
                          select p).CountAsync();
        }

        /// <summary>
        /// Get a page worth of products 
        /// </summary>
        public async static Task<List<Product>> GetProductsAsync(ApplicationDbContext _context, int pageSize, int pageNum)
        {
            return await (from p in _context.Products
                          orderby p.Title ascending
                          select p)
                                            .Skip(pageSize * (pageNum - 1)) //Skip must be before take
                                            .Take(pageSize)
                                            .ToListAsync();
        }
    }
}
