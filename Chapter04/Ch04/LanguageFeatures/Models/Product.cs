using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageFeatures.Models
{
    public class Product
    {
        private string name;

        public string Namge
        {
            get { return name; }
            set { name = value; }
        }

        public decimal Price { get; set; }
    }
}