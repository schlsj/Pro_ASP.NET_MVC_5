using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LanguageFeatures.Models;

namespace LanguageFeatures.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public String Index()
        {
            return "Navigate to a URL to show an example";
        }

        public ViewResult AutoProperty()
        {
            Product myProduct=new Product();
            myProduct.Namge = "kayak";
            string productName = myProduct.Namge;
            return View("Result", (object) string.Format("Product name: {0}", productName));
        }
    }

    public class ShpiingCard : IEnumerable<Product>
    {
        public List<Product> Products { get; set; }
        public IEnumerator<Product> GetEnumerator()
        {
            return Products.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public static class MyExtensionMethods
    {
        public static decimal TotalPrices(this IEnumerable<Product> productEnum)
        {
            decimal total = 0;
            foreach (Product product in productEnum)
            {
                total += product.Price;
            }
            return total;
        }
    }
}