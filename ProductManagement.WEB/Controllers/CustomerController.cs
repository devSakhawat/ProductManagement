using Microsoft.AspNetCore.Mvc;
using ProductManagement.Domain.Constants;
using ProductManagement.Domain.Entities;
using ProductManagement.WEB.HttpClients;
using ProductManagement.WEB.Models;

namespace ProductManagement.WEB.Controllers
{
   public class CustomerController : Controller
   {
      private readonly HttpClient client;
      public CustomerController(HttpClient client)
      {
         this.client = client;
      }

      // Create new Customer
      #region CreateCustomer
      [HttpGet]
      public IActionResult Create()
      {
         try
         {
            return View();
         }
         catch (Exception)
         {
            throw;
         }
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Create(Customer customer)
      {
         try
         {
            var customerInDb = await new CustomerHttpClient(client).AddCustomer(customer);

            if (customerInDb.CustomerId <= 0)
            {
               TempData[SessionConstants.MessageKey] = MessageConstants.DuplicateError;
               return RedirectToAction("Index");
            }
            else
            {
               TempData[SessionConstants.MessageKey] = MessageConstants.RecordSaved;
               return RedirectToAction("Index");
            }
         }
         catch (Exception)
         {
            throw;
         }
      }
      #endregion


      // Read Customers
      #region Index
      [HttpGet]
      public async Task<IActionResult> Index()
      {
         try
         {
            var customers = await new CustomerHttpClient(client).GetCustomers();
            return View(customers);
         }
         catch (Exception)
         {
            throw;
         }
      }
      #endregion

      // Update Customer
      #region UpdateCustomer
      [HttpGet]
      public async Task<IActionResult> Edit(int customerId)
      {
         try
         {
            if (customerId <= 0)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.UnauthorizedAttemptOfRecordUpdateError);

            var customer = await new CustomerHttpClient(client).GetCustomer(customerId);
            if (customer == null)
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

            return View(customer);
         }
         catch (Exception)
         {
            throw;
         }
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Edit(Customer customer)
      {
         try
         {
            var customerInDb = await new CustomerHttpClient(client).EditCustomer(customer);

            if (customerInDb != null)
            {
               TempData[SessionConstants.MessageKey] = MessageConstants.DuplicateError;
               return RedirectToAction("Index");
            }
            else
            {
               TempData[SessionConstants.MessageKey] = MessageConstants.RecordUpdated;
               return RedirectToAction("Index");
            }
         }
         catch (Exception)
         {
            throw;
         }
      }
      #endregion

      // Delete Customer
      public async Task<IActionResult> Delete(int customerId)
      {
         var customer = await    new CustomerHttpClient(client).GetCustomer(customerId);
         await new CustomerHttpClient(client).RemoveCustomer(customer);
         return RedirectToAction("ReadCustomers");
      }
   }
}
