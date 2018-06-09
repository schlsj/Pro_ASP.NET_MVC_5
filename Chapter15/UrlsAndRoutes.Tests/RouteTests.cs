using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Reflection;
using System.Web;
using System.Web.Routing;

namespace UrlsAndRoutes.Tests
{
    [TestClass]
    public class RouteTests
    {
        private HttpContextBase CreateHttpContext(string targetUrl = null, string httpMethod = "GET")
        {
            Mock<HttpRequestBase> mockRequest = new Mock<HttpRequestBase>();
            mockRequest.Setup(m => m.AppRelativeCurrentExecutionFilePath).Returns(targetUrl);
            mockRequest.Setup(m => m.HttpMethod).Returns(httpMethod);

            Mock<HttpResponseBase> mockResponse = new Mock<HttpResponseBase>();
            mockResponse.Setup(m => m.ApplyAppPathModifier(It.IsAny<string>())).Returns<string>(s => s);

            Mock<HttpContextBase> mockContext = new Mock<HttpContextBase>();
            mockContext.Setup(m => m.Request).Returns(mockRequest.Object);
            mockContext.Setup(m => m.Response).Returns(mockResponse.Object);

            return mockContext.Object;
        }

        private void TestRouteMatch(string url, string controller, string action, object routeProperties = null,
            string httpMethod = "GET")
        {
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);
            RouteData result = routes.GetRouteData(CreateHttpContext(url, httpMethod));
            Assert.IsNotNull(result);
            Assert.IsTrue(TestIncomingRouteResult(result, controller, action, routeProperties));
        }

        private bool TestIncomingRouteResult(RouteData routeResult, string controller, string action,
            object propertySet = null)
        {
            Func<object, object, bool> valCompare = (v1, v2) =>
            {
                return StringComparer.InvariantCultureIgnoreCase.Compare(v1, v2) == 0;
            };
            bool result = valCompare(routeResult.Values["controller"], controller) &&
                          valCompare(routeResult.Values["action"], action);
            if (propertySet != null)
            {
                PropertyInfo[] propInfo = propertySet.GetType().GetProperties();
                foreach (PropertyInfo pi in propInfo)
                {
                    //是我现在思维卡机了吗？
                    //要判断附件变量符合路由，需要从Url中提取的附加变量值，就是所期望的。。。
                    //应该是，要判断路由设置正确，就要验证：从Url中提取到的路由数据（片段变量），要与期望的片段变量相等；
                    //比如一条的模式是{controller}/{action}/{id}，Url：~/Admin/Index/55值，需要验证路由数据的Values["id"]，与Url期望的附件变量55相等
                    //对于期望的每一个属性，路由数据首先得有对应的键值对，其次键值对的值必须符合条件
                    //路由失败的情况，一种是在路由数据中没有对应的键值对；或者有了键值对，结果值不正确；
                    //而给出的条件，确实路由失败，需要不存在值，且键值对
                    //fuck，括号把我绕晕了。少看了一边括号
                    if (!(routeResult.Values.ContainsKey(pi.Name) &&
                        valCompare(routeResult.Values[pi.Name], pi.GetValue(propertySet, null))))
                    {
                        result = false;
                        break;
                    }
                }
            }
            return result;
        }

        private void TestRouteFail(string url)
        {
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);
            RouteData result = routes.GetRouteData(CreateHttpContext(url));
            Assert.IsTrue(result == null || result.Route == null);
        }

        [TestMethod]
        public void TestIncomingRoutes()
        {
            //TestRouteMatch("~/Admin/Index", "Admin", "Index");
            //TestRouteMatch("~/One/Two", "One", "Two");
            //TestRouteFail("~/Admin/Index/Segment");

            ////TestRouteFail("~/Admin");

            ////TestRouteMatch("~/Admin", "Admin", "Index");

            ////TestRouteMatch("~/Admin", "Admin", "Index");
            ////TestRouteMatch("~/", "Admin", "Index");

            //TestRouteMatch("~/Shop/Index", "Home", "Index");

            //TestRouteMatch("~/","Home","Index",new {id="Default"});
            //TestRouteMatch("~/Customer", "Customer", "index", new {id = "Default"});
            //TestRouteMatch("~/Customer/List", "Customer", "List", new { id = "Default" });
            //TestRouteMatch("~/Customer/List/All", "Customer", "List", new { id = "All" });
            //TestRouteFail("~/Customer/List/All/Delete");

            ////15.6.3
            //TestRouteMatch("~/", "Home", "Index");
            //TestRouteMatch("~/Customer", "Customer", "index");
            //TestRouteMatch("~/Customer/List", "Customer", "List");
            //TestRouteMatch("~/Customer/List/All", "Customer", "List", new { id = "All" });
            //TestRouteMatch("~/Customer/List/All/Delete", "Customer", "List", new { id = "All",catchall="Delete" });
            //TestRouteMatch("~/Customer/List/All/Delete/Perm", "Customer", "List", new { id = "All", catchall = "Delete/Perm" });

            //15.7.3
            TestRouteMatch("~/", "Home", "Index");
            TestRouteMatch("~/Home", "Home", "index");
            TestRouteMatch("~/Home/Index", "Home", "Index");
            TestRouteMatch("~/Home/About", "Home", "About");
            TestRouteMatch("~/Home/About/MyId", "Home", "About", new { id = "MyId" });
            TestRouteMatch("~/Home/About/MyId/More/Segments", "Home", "About", new { id = "MyId", catchall = "More/Segments" });
            TestRouteFail("~/Home/OtherAction");
            TestRouteFail("~/Account/Index");
            TestRouteFail("~/Account/About");
        }
        
    }
}
