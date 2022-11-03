using Microsoft.AspNetCore.Mvc;

namespace ProductManagement.WEB.Controllers
{
   public class TonerUsageController : Controller
   {
      private readonly HttpClient httpClient;
      public IActionResult Index()
      {
         return View();
      }
   }
}
