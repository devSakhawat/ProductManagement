using Microsoft.AspNetCore.Mvc;

namespace ProductManagement.WEB.Controllers
{
   public class ProjectController : Controller
   {
      public IActionResult Index()
      {
         return View();
      }
   }
}
