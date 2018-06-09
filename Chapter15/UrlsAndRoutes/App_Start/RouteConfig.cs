using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Routing.Constraints;
using System.Web.Routing;
using UrlsAndRoutes.Infrastructure;

namespace UrlsAndRoutes
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            ////1.默认
            //routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);

            ////2.实例化
            //Route myRoute = new Route("{controller}/{action}", new MvcRouteHandler());
            //routes.Add("MyRoute",myRoute);

            ////3.RouteCollection的封装方法
            //routes.MapRoute("MyRoute", "{controller}/{action}");

            ////4.使用默认值1
            //routes.MapRoute("MyRoute", "{controller}/{action}", new { action = "Index" });

            ////5.使用默认值2
            //routes.MapRoute("MyRoute", "{controller}/{action}", new { controller="Admin",action = "Index" });

            ////6.使用静态值
            //routes.MapRoute("ShopSchema2", "Shop/OldAction", new {controller = "Home", action = "Index"});
            //routes.MapRoute("ShopRoute", "Shop/{action}", new {controller = "Home"});
            //routes.MapRoute("", "X{controller}/{action}");
            //routes.MapRoute("MyRoute", "{controller}/{action}", new {controller = "Home", action = "Index"});
            //routes.MapRoute("", "Public/{controller}/{action}", new {controller = "Home", action = "Index"});

            ////15.6.1 使用自定义片段
            //routes.MapRoute("MyRoute", "{controller}/{action}/{id}",
            //    new {controller = "Home", action = "Index", id = "Default"});

            ////15.6.2 可选自定义片段
            //routes.MapRoute("MyRoute", "{controller}/{action}/{id}",
            //    new { controller = "Home", action = "Index", id = UrlParameter.Optional });

            ////15.6.3  可变长路由
            //routes.MapRoute("MyRoute", "{controller}/{action}/{id}/{*catchall}",
            //    new { controller = "Home", action = "Index", id = UrlParameter.Optional });

            ////15.6.4 按命名空间区分控制器优先顺序
            //routes.MapRoute("MyRoute", "{controller}/{action}/{id}/{*catchall}",
            //    new { controller = "Home", action = "Index", id = UrlParameter.Optional },new[]{"UrlsAndRoutes.AdditionalControllers"});

            ////15.6.4 需要按照优先级来查找命名空间的解决方案
            ////其实对Url做了一定的限制，不过思路很巧妙
            //routes.MapRoute("MyRoute1", "Home/{action}/{id}/{*catchall}",
            //    new { controller = "Home", action = "Index", id = UrlParameter.Optional }, new[] { "UrlsAndRoutes.AdditionalControllers" });
            //routes.MapRoute("MyRoute2", "{controller}/{action}/{id}/{*catchall}",
            //    new { controller = "Home", action = "Index", id = UrlParameter.Optional }, new[] { "UrlsAndRoutes.Controllers" });

            ////15.6.4 只查找某个命名空间的解决方案
            //var route = routes.MapRoute("MyRoute1", "Home/{action}/{id}/{*catchall}",
            //    new { controller = "Home", action = "Index", id = UrlParameter.Optional }, new[] { "UrlsAndRoutes.AdditionalControllers" });
            //route.DataTokens["UseNamespaceFallback"] = false;

            ////15.7.1 用正则表达式约束一条路由
            ////默认值会优于约束被使用
            //routes.MapRoute("MyRoute", "{controller}/{action}/{id}/{*catchall}",
            //    new { controller = "Home", action = "Index", id = UrlParameter.Optional }, new{controller="^H.*"},new[] { "UrlsAndRoutes.Controllers" });

            ////15.7.2 用正则表达式约束一条路由 约束到一组指定的值
            ////看了下重载方法，使用约束的路由，一定要有默认值，哪怕传递的是空
            //routes.MapRoute("MyRoute", "{controller}/{action}/{id}/{*catchall}",
            //    new { controller = "Home", action = "Index", id = UrlParameter.Optional }, 
            //    new { controller = "^H.*", action="^Index$|^About$" }, new[] { "UrlsAndRoutes.Controllers" });

            ////15.7.3 使用HTTP方法约束路由
            //routes.MapRoute("MyRoute", "{controller}/{action}/{id}/{*catchall}",
            //    new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            //    new { controller = "^H.*", action = "^Index$|^About$",httpmethod=new HttpMethodConstraint("GET","POST") }, new[] { "UrlsAndRoutes.Controllers" });

            ////15.7.3 使用类型和值约束
            //routes.MapRoute("MyRoute", "{controller}/{action}/{id}/{*catchall}",
            //    new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            //    new
            //    {
            //        controller = "^H.*", action = "^Index$|^About$", httpmethod = new HttpMethodConstraint("GET"),
            //        id=new RangeRouteConstraint(10,20),
            //    }, 
            //    new[] { "UrlsAndRoutes.Controllers" });

            ////15.7.3 使用多种类型和值约束
            //routes.MapRoute("MyRoute", "{controller}/{action}/{id}/{*catchall}",
            //    new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            //    new
            //    {
            //        controller = "^H.*",
            //        action = "^Index$|^About$",
            //        httpmethod = new HttpMethodConstraint("GET"),
            //        id = new CompoundRouteConstraint(new List<IRouteConstraint>()
            //        {
            //            new AlphaRouteConstraint(),new MinLengthRouteConstraint(6),
            //        }),
            //    },
            //    new[] { "UrlsAndRoutes.Controllers" });

            ////15.7.4 定义自定义约束
            //routes.MapRoute("ChromeRoute", "{*Catchall}", new { Controller = "Home", Action = "Index" },
            //    new { customConstraint = new UserAgentConstraint("Chrome") },
            //    new[] { "UrlsAndRoutes.AdditionalControllers" });
            //routes.MapRoute("MyRoute", "{ControllEr}/{Action}/{id}/{*catchall}",
            //    new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            //    new
            //    {
            //        controller = "^H.*",
            //        action = "^Index$|^About$",
            //        httpmethod = new HttpMethodConstraint("GET"),
            //        id = new CompoundRouteConstraint(new List<IRouteConstraint>()
            //        {
            //            new AlphaRouteConstraint(),new MinLengthRouteConstraint(6),
            //        }),
            //    },
            //    new[] { "UrlsAndRoutes.Controllers" });

            //15.8.1 启用和运用属性路由
            routes.MapMvcAttributeRoutes();
            routes.MapRoute("Default", "{controller}/{action}/{id}",
                new {controller = "Home", action = "index", id = UrlParameter.Optional},
                new[] {"UrlsAndRoutes.Controllers"});
        }
    }
}
