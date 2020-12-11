using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShowcaseSite.Data;
using ShowcaseSite.Models;
using ShowcaseSite.ViewModels;

namespace ShowcaseSite.Controllers
{
    public class HomeController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext dbContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ILogger<HomeController> _logger;

        public char[] UploadedFiles { get; private set; }

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _userManager = userManager;
            _logger = logger;
            dbContext = context;
            webHostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var product = await dbContext.Products.ToListAsync();
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }

        
        
        private string UploadedFile(ProductViewModel model)
        {
            string uniqueFileName = null;

            if(model.PicUrl != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.PicUrl.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.PicUrl.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(ProductViewModel model)
        {
            ApplicationUser currentUser = await _userManager.GetUserAsync(User);
         
            ViewBag.Message = currentUser.Bio;
            ViewBag.Name = currentUser.FirstName + " " + currentUser.LastName;

            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model);

                Product product = new Product
                {
                    UserName = currentUser.UserName,
                    Title = model.Title,
                    Description = model.Description,
                    Price = model.Price,
                    PicUrl = uniqueFileName
                };

                dbContext.Add(product);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
