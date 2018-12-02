using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace UrlsAndRoutes.Infrastructure
{
    public class LegacyRoute:RouteBase
    {
        private string[] urls;

        public LegacyRoute(params string[] targetUrls)
        {
            urls = targetUrls;
        }

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            RouteData result = null;
            string requestUrl = httpContext.Request.AppRelativeCurrentExecutionFilePath;
            if (urls.Contains(requestUrl))
            {
                result = new RouteData(this,new MvcRouteHandler());
                result.Values.Add("controller", "Legacy");
                result.Values.Add("action", "GetLegacyUrl");
                result.Values.Add("legacyUrl", requestUrl);
                Debug.WriteLine($"controller: {result.Values["controller"]}");
                Debug.WriteLine($"action: {result.Values["action"]}");
                Debug.WriteLine($"legacyUrl: {result.Values["legacyUrl"]}");
            }
            return result;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            VirtualPathData result = null;
            if (values.ContainsKey("legacyUrl") && urls.Contains((string) values["legacyUrl"]))
            {
                var virtualPath = new UrlHelper(requestContext).Content((string) values["legacyUrl"]).Substring(1);
                result=new VirtualPathData(this,virtualPath);
                Debug.WriteLine(virtualPath+" :VirtualPath");
            }
            return result;
        }
    }
}