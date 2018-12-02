using System.Diagnostics;
using System.Web.Mvc;

namespace Filters.Infrastructure
{
    public class ProfileResultAttribute:FilterAttribute,IResultFilter
    {
        private Stopwatch timer;
        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            timer=Stopwatch.StartNew();
        }

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            timer.Stop();
            filterContext.HttpContext.Response.Write($"<div>Result elapsed time: {timer.Elapsed.TotalSeconds:F6}s");
        }
    }
}