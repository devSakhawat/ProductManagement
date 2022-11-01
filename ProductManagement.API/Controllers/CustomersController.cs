using Microsoft.AspNetCore.Mvc;
using ProductManagement.DAL.Constracts;
using ProductManagement.Domain.Constants;
using ProductManagement.Domain.Entities;

namespace ProductManagement.API.Controllers
{
   //Country Controller
   [Route(RouteConstants.BaseRoute)]
   [ApiController]
   public class CustomersController : ControllerBase
   {
      private readonly IUnitOfWork context;

      // Default constructor
      public CustomersController(IUnitOfWork context)
      {
         this.context = context;
      }

      // URL: toner-api/customer
      // Object to be saved in the table as a row.
      [HttpPost]
      [Route(RouteConstants.CreateCustomer)]
      public async Task<IActionResult> CreateCustomer(Customer customer)
      {
         try
         {
            if (await IsDuplicate(customer) == true)
               return StatusCode(StatusCodes.Status409Conflict, MessageConstants.DuplicateError);
            context.CustomerRepository.Add(customer);
            await context.SaveChangesAsync();

            return CreatedAtAction("ReadCustomerbyKey", new {key = customer.CustomerId}, customer);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      // URL: toner-api/customers
      [HttpGet]
      [Route(RouteConstants.ReadCustomers)]
      public async Task<IActionResult> ReadCustomers()
      {
         try
         {
            var customers = await context.CustomerRepository.GetCustomers();
            return Ok(customers);
         }
         catch (Exception)
         {
            throw;
         }
      }

      // URL: toner-api/customer/key
      [HttpGet]
      [Route(RouteConstants.ReadCustomerByKey)]
      public async Task<IActionResult> ReadCustomerByKey(int key)
      {
         try
         {
            if (key <= 0)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

            var customer = await context.CustomerRepository.GetCustomerByKey(key);

            if (customer == null)
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

            return Ok(customer);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      // URL: tuso-api/country/{key}
      // Object to be updated
      [HttpPut]
      [Route(RouteConstants.UpdateCustomer)]
      public async Task<IActionResult> UpdateCustomer(int key, Customer customer)
      {
         try
         {
            if (key != customer.CustomerId)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.UnauthorizedAttemptOfRecordUpdateError);

            if (await IsDuplicate(customer) == true)
               return StatusCode(StatusCodes.Status409Conflict, MessageConstants.DuplicateError);

            context.CustomerRepository.Update(customer);
            await context.SaveChangesAsync();

            return Ok(customer);

         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      // URL: tuso-api/country/{key}
      // Object to be deleted
      [HttpDelete]
      [Route(RouteConstants.DeleteCustomer)]
      public async Task<IActionResult> DeleteCustomer(int key)
      {
         try
         {
            if (key <= 0)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

            var customerInDb = await context.CustomerRepository.GetCustomerByKey(key);

            if (customerInDb == null)
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

            if (customerInDb.Projects.Where(p => p.IsDeleted == false).ToList().Count() > 0)
               return StatusCode(StatusCodes.Status405MethodNotAllowed, MessageConstants.DuplicateError);

            customerInDb.IsDeleted = true;

            context.CustomerRepository.Delete(customerInDb);
            await context.SaveChangesAsync();

            return Ok(customerInDb);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      // Checks whether the customer name is duplicate?
      private async Task<bool> IsDuplicate(Customer customer)
      {
         try
         {
            var customerInDb = await context.CustomerRepository.GetCustomerByName(customer.CustomerName);

            if (customerInDb != null)
               return true;

            return false;
         }
         catch (Exception)
         {
            throw;
         }
      }

   }
}