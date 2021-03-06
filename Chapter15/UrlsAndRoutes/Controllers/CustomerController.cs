﻿using System.Web.Mvc;

namespace UrlsAndRoutes.Controllers
{
    [RouteArea("Services")]
    [RoutePrefix("Users")]
    public class CustomerController : Controller
    {
        [Route("~/Test")]
        // GET: Customer
        public ActionResult Index()
        {
            ViewBag.Controller = "Customer";
            ViewBag.Action = "Index";
            return View("ActionName");
        }

        [Route("Add/{user}/{id:int}")]
        public string Create(string user, int id)
        {
            return $"Create Method - User: {user}, ID: {id}";
        }

        [Route("Add/{user}/{password:alpha:length(6)}")]
        public string ChangePass(string user, string password)
        {
            return $"ChangePass Method - User: {user}, Pass: {password}";
        }

        public ActionResult List()
        {
            ViewBag.Controller = "Customer";
            ViewBag.Action = "List";
            return View("ActionName");
        }

        //CustomVariable
        //public ActionResult CustomVariable(string iD)
        //{
        //    ViewBag.Controller = "Home";
        //    ViewBag.Action = "CustomVariable";
        //    ViewBag.CustomVariable = iD;
        //    return View();
        //}
    }
}