using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShowcaseSite.ViewModels
{
    public class ProductViewModel
    {
        public string UserName { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public IFormFile PicUrl { get; set; }
    }
}
