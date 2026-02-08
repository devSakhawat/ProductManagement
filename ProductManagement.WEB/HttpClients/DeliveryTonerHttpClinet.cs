using Newtonsoft.Json;
using ProductManagement.Domain.Dtos;
using ProductManagement.Domain.Entities;

namespace ProductManagement.WEB.HttpClients
{
   public class DeliveryTonerHttpClinet
   {
      private readonly HttpClient client;
      private readonly string BaseUrl = "https://localhost:7284/toner-api/";

      public DeliveryTonerHttpClinet(HttpClient client)
      {
         this.client = client;
      }

      public async Task<List<DeliveryTonerDto>> GetDeliveryToners()
      {
         var response = await client.GetAsync($"{BaseUrl}delivery-toners");

         if (!response.IsSuccessStatusCode)
         {
            return new List<DeliveryTonerDto>();
         }

         string result = await response.Content.ReadAsStringAsync();
         var deliveryTonerResult = JsonConvert.DeserializeObject<List<DeliveryTonerDto>>(result);
         List<DeliveryTonerDto> deliveryToners = new List<DeliveryTonerDto>(deliveryTonerResult.ToList());
         return deliveryToners;
      }
   }
}
