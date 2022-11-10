using Microsoft.AspNetCore.Mvc;

namespace ProductManagement.WEB.Controllers
{
   public class DeliveryTonerController : Controller
   {
      public IActionResult Index()
      {
         return View();
      }

      [HttpGet]
      public IActionResult DeliveryToners()
      {
         return View();
      }
   }
}
