using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UrlsAndRoutes.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Controller = "Home";
            ViewBag.Action = "Index";
            return View("ActionName");
        }

        public ActionResult About()
        {
            ViewBag.Controller = "Home";
            ViewBag.Action = "About";
            return View("ActionName");
        }

        ////15.6.1
        //public ActionResult CustomVariable()
        //{
        //    ViewBag.Controller = "Home";
        //    ViewBag.Action = "CustomVariable";
        //    ViewBag.CustomVariable = RouteData.Values["id"];
        //    return View();
        //}

        //    //15.6
        //public ActionResult CustomVariable(string iD)
        //{
        //    ViewBag.Controller = "Home";
        //    ViewBag.Action = "CustomVariable";
        //    //15.6.1
        //    ViewBag.CustomVariable = iD;
        //    //15.6.2
        //    ViewBag.CustomVariable = iD ?? "<no value>";
        //    return View();
        //}

        //15.6
        public ActionResult CustomVariable(string iD="DefaultId")
        {
            //刚刚有一个误解，以为在现在的方法中，当Url是"~/"的情况时，调用的是CustomVariable()方法。
            //好像还是有点混乱的。就是当Url是"~/Home/CustomVariable"时，是调用的CustomVariable()（获取iD失败，调用无参构造方法）
            //还是调用CustomVariable("DefaultId")(获取iD失败，iD被DefaultId填充；
            //从上面15.6.2的方法来看，Url是"~/Home/CustomVariable"时，调用的似乎是CustomVariable(null)，
            //从当前方法来看，Url是"~/Home/CustomVariable"时，调用的似乎是CustomVariable()
            ViewBag.Controller = "Home";
            ViewBag.Action = "CustomVariable";
            ViewBag.CustomVariable = iD;
            return View();
        }

        public void Test()
        {
            CustomVariable("20");
        }
    }
}