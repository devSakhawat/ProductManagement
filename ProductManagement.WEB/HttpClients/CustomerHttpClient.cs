using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProductManagement.Domain.Entities;
using System.Text;

namespace ProductManagement.WEB.HttpClients
{
   public class CustomerHttpClient
   {
      private readonly HttpClient client;
      private readonly string BaseApi = "https://localhost:7284/toner-api/";

      public CustomerHttpClient(HttpClient client)
      {
         this.client = client;
      }

      // Create Customer
      #region CreateCustomer
      public async Task<Customer> AddCustomer(Customer customer)
      {
         if (customer.CustomerId == 0)
         {
            var data = JsonConvert.SerializeObject(customer);
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{BaseApi}customer", httpContent);

            if (response.ReasonPhrase == "conflict")
            {
               return new Customer();
            }
            else if(!response.IsSuccessStatusCode)
            {
               return new Customer();
            }
            else
            {
               string result = await response.Content.ReadAsStringAsync();
               var addCustomer = JsonConvert.DeserializeObject<Customer>(result);
               return addCustomer;
            }
         }
         else
         {
            return new Customer();
         }
      }

      #endregion
      
      // Read customers
      #region ReadCustomer
      public async Task<List<Customer>> GetCustomers()
      {
         var response = await client.GetAsync($"{BaseApi}customers");

         if (!response.IsSuccessStatusCode)
         {
            return new List<Customer>();
         }

         string result = await response.Content.ReadAsStringAsync();
         var customer = JsonConvert.DeserializeObject<List<Customer>>(result);
         List<Customer> customers = new List<Customer>(customer.ToList());

         return customers;
      }
      #endregion

      // Read single customer
      #region GetCustomerById
      public async Task<Customer> GetCustomer(int key)
      {
         var response = await client.GetAsync($"{BaseApi}customer/key/" + key + "");
         if (!response.IsSuccessStatusCode)
         {
            return new Customer();
         }

         string result = await response.Content.ReadAsStringAsync();
         var customer = JsonConvert.DeserializeObject<Customer>(result);

         return customer;
      }
      #endregion

      // Update Customer
      #region UpdateCustomer
      public async Task<Customer> EditCustomer(Customer customer)
      {
         if (customer.CustomerId > 0)
         {
            var data = JsonConvert.SerializeObject(customer);
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{BaseApi}customer/" + customer.CustomerId + "", httpContent);

            if (!response.IsSuccessStatusCode)
            {
               return new Customer();
            }
            string result = await response.Content.ReadAsStringAsync();
            var editCustomer = JsonConvert.DeserializeObject<Customer>(result);

            return editCustomer;
         }
         else
         {
            return new Customer();
         }
      }
      #endregion

      // Delete Customer
      #region DeleteCustomer
      public async Task<Customer> RemoveCustomer(Customer customer)
      {
         var data = JsonConvert.SerializeObject(customer);
         var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
         var response = await client.PatchAsync($"{BaseApi}Customer/DeleteCustomer", httpContent);

         if (!response.IsSuccessStatusCode)
         {
            return new Customer();
         }

         string result = await response.Content.ReadAsStringAsync();
         var removeCustomer = JsonConvert.DeserializeObject<Customer>(result);

         return removeCustomer;
      }
      #endregion
   }
}
