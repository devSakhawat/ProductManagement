using Microsoft.AspNetCore.Mvc;

namespace ProductManagement.WEB.Controllers
{
   public class MachineController : Controller
   {
      public IActionResult Index()
      {
         return View();
      }
   }
}
