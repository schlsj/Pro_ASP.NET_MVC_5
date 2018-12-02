using System.Web.Mvc;
using System.Web.Routing;

namespace ControllersAndActions.Controllers
{
    public class BasicController:IController
    {
        public void Execute(RequestContext requestContext)
        {
            string controller = (string) requestContext.RouteData.Values["controller"];
            string action = (string) requestContext.RouteData.Values["action"];

            ////17.2 Implement IController
            //requestContext.HttpContext.Response.Write($"Controller: {controller}, Action: {action}.");

            //17.4 Generate output of controller
            if (action.ToLower() == "redirect")
            {
                requestContext.HttpContext.Response.Redirect("/Derived/Index");
            }
            else
            {
                requestContext.HttpContext.Response.Write($"Controller: {controller}, Action: {action}.");
            }
        }
    }
}