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
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging;
using ShowcaseSite.Data;
using ShowcaseSite.Models;
using ShowcaseSite.ViewModels;

namespace ShowcaseSite.Controllers
{
    public class HomeController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ApplicationDbContext DBcontext;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _userManager = userManager;
            _logger = logger;
            DBcontext = context;
            webHostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var product = await DBcontext.Products.ToListAsync();
            return View(product);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(ProductViewModel model)
        {
            ApplicationUser currentUser = await _userManager.GetUserAsync(User); 
            
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                // If the Photo property on the incoming model object is not null, then the user
                // has selected an image to upload.
                if (model.ProductPic != null)
                {
                    // The image must be uploaded to the images folder in wwwroot
                    // To get the path of the wwwroot folder we are using the inject
                    // HostingEnvironment service provided by ASP.NET Core
                    string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");

                    // To make sure the file name is unique we are appending a new
                    // GUID value and and an underscore to the file name
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProductPic.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    // Use CopyTo() method provided by IFormFile interface to
                    // copy the file to wwwroot/images folder
                    model.ProductPic.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                Product newProduct = new Product
                {
                    UserName = currentUser.UserName,
                    Title = model.Title,
                    Description = model.Description,
                    Price = model.Price,
                    // Store the file name in PhotoPath property of the employee object
                    // which gets saved to the Employees database table
                    PicUrl = uniqueFileName
                };

                DBcontext.Add(newProduct);
                await DBcontext.SaveChangesAsync();
                return RedirectToAction("Profile");
            }

            return View();
        }
      

        public async Task<IActionResult> Profile()
        {
            ApplicationUser currentUser = await _userManager.GetUserAsync(User);
         
            ViewBag.Message = currentUser.Bio;
            ViewBag.Name = currentUser.FirstName + " " + currentUser.LastName;
            
            return View();
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
