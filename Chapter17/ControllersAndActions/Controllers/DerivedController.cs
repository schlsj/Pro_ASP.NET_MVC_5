using System.Web.Mvc;
using ControllersAndActions.Infrastructure;

namespace ControllersAndActions.Controllers
{
    public class DerivedController : Controller
    {
        // GET: Derived
        public ActionResult Index()
        {
            ViewBag.Message = "Hello from the DerivedController->Index method.";
            ViewBag.MachineName = Server.MachineName;
            return View("MyView");
        }

        public void ProduceOutput1()
        {
            if (Server.MachineName == "TINY")
            {
                Response.Redirect("/Basic/Index");
            }
            else
            {
                Response.Write("Controller: Derived, Action: ProduceOutput1");
            }
        }

        //17.4.1 use the custom actionResult
        public ActionResult ProduceOutput2()
        {
            if (Server.MachineName == "PC-20181018GIJQ")
            {
                return new CustomRedirectResult() {Url = "/Basic/Index"};
            }
            else
            {
                Response.Write("Controller: Derived, Action: ProduceOutput2");
                return null;
            }
        }

        //17.4.1 use the redirectResult class
        public ActionResult ProduceOutput3()
        {
            return new RedirectResult("/Basic/Index");
        }
    }
}