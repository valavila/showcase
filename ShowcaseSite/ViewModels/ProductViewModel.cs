using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShowcaseSite.ViewModels
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }

        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter a title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter a description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter a price")]
        public double Price { get; set; }

        [Display(Name = "Choose an image for your product")]
        public IFormFile ProductPic { get; set; }
    }
}
