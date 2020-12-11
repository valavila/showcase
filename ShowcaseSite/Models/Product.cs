﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShowcaseSite.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        public string UserName { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public string PicUrl { get; set; }
    }
}
