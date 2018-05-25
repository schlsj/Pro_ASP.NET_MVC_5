using System.Web.Mvc;
using EssentialTools.Models;
using Ninject;

namespace EssentialTools.Controllers
{
    public class HomeController : Controller
    {
        private IValueCalculator calc;

        private Product[] products = {
            new Product {Name = "Kayak", Category = "Watersports", Price = 275m},
            new Product {Name = "Lifejacket", Category = "watersports", Price = 48.95m},
            new Product {Name = "Soccer ball", Category = "Soccer", Price = 19.50m},
            new Product {Name = "Corner flag", Category = "Soccer", Price = 34.95m}
        };

        public HomeController(IValueCalculator calcParam,IValueCalculator calc2)
        {
            calc = calcParam;
        }

        // GET: Home
        public ActionResult Index()
        {
            //IKernel ninjectKernel = new StandardKernel();
            //ninjectKernel.Bind<IValueCalculator>().To<LinqValueCalculator>();
            //IValueCalculator calc = ninjectKernel.Get<IValueCalculator>();
            //IValueCalculator calc=new LinqValueCalculator();

            ShoppingCart cart = new ShoppingCart(calc) {Products = products};
            decimal totalValue = cart.CalculateProductTotal();
            return View(totalValue);
        }
    }
}