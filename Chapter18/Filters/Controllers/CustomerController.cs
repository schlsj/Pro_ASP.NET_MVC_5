using System.Web.Mvc;
using Filters.Infrastructure;

namespace Filters.Controllers
{
    [SimpleMessage(Message = "Controller")]
    public class CustomerController : Controller
    {
        [SimpleMessage(Message = "A",Order = 1)]
        [SimpleMessage(Message = "B",Order = 2)]
        public string Index()
        {
            return "This is the customer controller";
        }

        [CustomOverrideActionFilters]
        [SimpleMessage(Message = "Action")]
        public string OverlapTest()
        {
            return "This is the overlap test of customer.";
        }

        [SimpleMessage(Message = "Action")]
        public string OverrideTest()
        {
            return "This is the override test of customer.";
        }
    }
}