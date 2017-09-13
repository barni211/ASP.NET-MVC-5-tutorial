﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageFeatures.Models
{
    public class Product
    {
        private string name;

        public string productId { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public string category { get; set; }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}