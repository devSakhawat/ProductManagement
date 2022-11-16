using Microsoft.AspNetCore.Mvc;
using ProductManagement.Domain.Dtos;
using ProductManagement.WEB.HttpClients;

namespace ProductManagement.WEB.Controllers
{
   public class DeliveryTonerController : Controller
   {
      private readonly HttpClient httpClient;
      public DeliveryTonerController(HttpClient httpClient)
      {
         this.httpClient = httpClient;
      }

      public async Task<IActionResult> Index()
      {
         List<DeliveryTonerDto> deliveryToner = await new DeliveryTonerHttpClinet(httpClient).GetDeliveryToners();
         List<DeliveryTonerDto> deliveryToners = new List<DeliveryTonerDto>(deliveryToner.ToList());
         return View(deliveryToners);
      }

      public IActionResult DeliveryToners()
      {
         return View();
      }
   }
}
