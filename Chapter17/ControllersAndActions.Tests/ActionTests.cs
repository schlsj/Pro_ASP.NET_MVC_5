using System;
using System.Web.Mvc;
using ControllersAndActions.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControllersAndActions.Tests
{
    [TestClass]
    public class ActionTests
    {
        [TestMethod]
        public void ControllerTest1()
        {
            ExampleController target=new ExampleController();
            ViewResult result = target.Index();
            Assert.AreEqual("",result.ViewName);
        }

        [TestMethod]
        public void ViewSelectionTest1()
        {
            ExampleController target=new ExampleController();
            var result = target.HomePage();
            Assert.AreEqual("",result.ViewName);
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(DateTime));
            Assert.AreEqual("Hello",result.ViewBag.Message);
        }

        [TestMethod]
        public void RedirectTest()
        {
            ExampleController target=new ExampleController();
            var result = target.Redirect();
            Assert.IsFalse(result.Permanent);
            Assert.AreEqual("/Example/Index", result.Url);
        }

        [TestMethod]
        public void RedirectByRouteTest()
        {
            ExampleController target=new ExampleController();
            var result = target.RedirectByRoute();
            Assert.IsFalse(result.Permanent);
            Assert.AreEqual("Example", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("MyID", result.RouteValues["id"]);
        }

        [TestMethod]
        public void UnauthorizedTest()
        {
            ExampleController target=new ExampleController();
            var result = target.StatusCode();
            Assert.AreEqual(401,result.StatusCode);
        }
    }
}
