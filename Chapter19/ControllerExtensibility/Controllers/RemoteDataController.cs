using System.Threading.Tasks;
using System.Web.Mvc;
using ControllerExtensibility.Models;

namespace ControllerExtensibility.Controllers
{
    public class RemoteDataController : Controller
    {
        // GET: RemoteData
        public ActionResult Data()
        {
            RemoteService service=new RemoteService();
            string data = service.GetRemoteData();
            return View((object) data);
        }

        public async Task<ActionResult> DataAsync()
        {
            string data = await Task<string>.Factory.StartNew(() => new RemoteService().GetRemoteData());
            return View("Data",(object) data);
        }

        public async Task<ActionResult> ConsumeAsyncMethod()
        {
            string data = await new RemoteService().GetRemoteDataAsync();
            return View("Data", (object) data);
        }
    }
}