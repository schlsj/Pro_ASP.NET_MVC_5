using System;
using System.Web.Mvc;

namespace ControllersAndActions.Controllers
{
    public class ExampleController : Controller
    {
        // GET: Example
        public ViewResult Index()
        {
            return View();
        }

        public ViewResult HomePage()
        {
            DateTime date=DateTime.Now;
            ViewBag.Message = "Hello";
            ViewBag.Date = DateTime.Now;
            return View(date);
        }

        public RedirectResult Redirect()
        {
            return Redirect("/Example/Index");
        }

        public RedirectToRouteResult RedirectByRoute()
        {
            return RedirectToRoute(new {controller = "Example", action = "Index", id = "MyID"});
        }

        public HttpStatusCodeResult StatusCode()
        {
            return new HttpUnauthorizedResult();
        }
    }
}