﻿using System;
using System.Web.Mvc;

namespace Views.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Message = "Hello, world";
            ViewBag.Time = DateTime.Now.ToShortTimeString();
            return View("DebugData");
        }

        public ActionResult List()
        {
            return View();
        }
    }
}