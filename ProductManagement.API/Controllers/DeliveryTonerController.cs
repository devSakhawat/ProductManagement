using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.DAL.Constracts;
using ProductManagement.Domain.Constants;
using ProductManagement.Domain.Entities;

namespace ProductManagement.API.Controllers
{
   [Route(RouteConstants.BaseRoute)]
   [ApiController]
   public class DeliveryTonerController : ControllerBase
   {
      private readonly IUnitOfWork context;
      public DeliveryTonerController(IUnitOfWork context)
      {
         this.context = context;
      }

      // URL: toner-api/delivery-toner
      // Object to be saved in the table as a row.
      [HttpPost]
      [Route(RouteConstants.CreateDeliveryToner)]
      public async Task<IActionResult> CreateDeliveryToner(DeliveryToner deliveryToner)
      {
         try
         {
            if (deliveryToner == null)
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

            if(IsDeliverd() == true)
               return StatusCode(StatusCodes.Status409Conflict, MessageConstants.DuplicateError);

            double totalColourToner = Convert.ToDouble(deliveryToner.Cyan)
               + Convert.ToDouble(deliveryToner.Magenta)
               + Convert.ToDouble(deliveryToner.Yellow)
               + Convert.ToDouble(deliveryToner.Black);

            var deliveryTonerInDb = new DeliveryToner
            {
               MachineId = deliveryToner.MachineId,
               BW = Convert.ToDouble(deliveryToner.BW),
               Cyan = Convert.ToDouble(deliveryToner.Cyan),
               Magenta = Convert.ToDouble(deliveryToner.Magenta),
               Yellow = Convert.ToDouble(deliveryToner.Yellow),
               Black = Convert.ToDouble(deliveryToner.Black),
               ColourTotal = totalColourToner,
               DateCreated = DateTime.Now.Date
            };
            context.DeliveryTonerRepository.Add(deliveryTonerInDb);

            //deliveryToner.ColourTotal = deliveryToner.Cyan + deliveryToner.Magenta + deliveryToner.Yellow + deliveryToner.Black;
            //deliveryToner.DateModified = DateTime.Now.Date;


            context.DeliveryTonerRepository.Add(deliveryToner);
            await context.SaveChangesAsync();

            return CreatedAtAction("ReadDeliveryByKey", new { key = deliveryTonerInDb.DeliveryTonerId}, deliveryTonerInDb);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      // URL: toner-api/delivery-toners
      // All delivery toner data.
      [HttpGet]
      [Route(RouteConstants.ReadDeliveryToner)]
      public async Task<IActionResult> ReadDeliveryToner()
      {
         try
         {
            var deliveryToners = await context.DeliveryTonerRepository.GetDeliveryToners();

            if (deliveryToners == null)
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

            return Ok(deliveryToners);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      // URL: toner-api/delivery-toner/key/{key}
      // return single row
      [HttpGet]
      [Route(RouteConstants.ReadDeliveryTonerByKey)]
      public async Task<IActionResult> ReadDeliveryTonerByKey(int key)
      {
         try
         {
            if(key <= 0)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

            var deliveryToner = await context.DeliveryTonerRepository.GetDeliveryTonerByKey(key);

            if (deliveryToner == null)
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

            return Ok(deliveryToner);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      // URL: toner-api/delivery-toner/key/{key}
      // Object to be updated in the table as a row.
      [HttpPut]
      [Route(RouteConstants.UpdateDeliveryToner)]
      public async Task<IActionResult> UpdateDeliveryToner(int key, DeliveryToner deliveryToner)
      {
         try
         {
            if (key != deliveryToner.DeliveryTonerId)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.UnauthorizedAttemptOfRecordUpdateError);

            double totalColourToner = Convert.ToDouble(deliveryToner.Cyan)
            + Convert.ToDouble(deliveryToner.Magenta)
            + Convert.ToDouble(deliveryToner.Yellow)
            + Convert.ToDouble(deliveryToner.Black);

            var deliveryTonerInDb = new DeliveryToner
            {
               MachineId = deliveryToner.MachineId,
               BW = Convert.ToDouble(deliveryToner.BW),
               Cyan = Convert.ToDouble(deliveryToner.Cyan),
               Magenta = Convert.ToDouble(deliveryToner.Magenta),
               Yellow = Convert.ToDouble(deliveryToner.Yellow),
               Black = Convert.ToDouble(deliveryToner.Black),
               ColourTotal = totalColourToner,
               DateModified = DateTime.Now.Date
            };
            //deliveryToner.ColourTotal = deliveryToner.Cyan + deliveryToner.Magenta + deliveryToner.Yellow + deliveryToner.Black;
            //deliveryToner.DateModified = DateTime.Now.Date;

            context.DeliveryTonerRepository.Update(deliveryTonerInDb);
            await context.SaveChangesAsync();

            return Ok(deliveryTonerInDb);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError); ;
         }
      }

      // URL: toner-api/delivery-toner/{key}
      // Object to be deleted form table as row.
      [HttpDelete]
      [Route(RouteConstants.DeleteDeliveryToner)]
      public async Task<IActionResult> DeleteDeliveryToner(int key)
      {
         try
         {
            if (key <= 0)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

            var deliveryToner = await context.DeliveryTonerRepository.GetDeliveryTonerByKey(key);

            if (deliveryToner == null)
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

            if (deliveryToner.TonerUsages.Where(p => p.IsDeleted == false).ToList().Count() > 0)
               return StatusCode(StatusCodes.Status405MethodNotAllowed, MessageConstants.DependencyError);

            deliveryToner.IsDeleted = true;

            context.DeliveryTonerRepository.Delete(deliveryToner);
            await context.SaveChangesAsync();
            return Ok(deliveryToner);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }


      private bool IsDeliverd()
      {
         try
         {
            //var ShortDateString = DateTime.Now.ToShortDateString().Replace("-", string.Empty);
            //var checkCurrentMonthTonerDelivery = ShortDateString.Substring(2, ShortDateString.Length - 2);
            //var data = context.DeliveryTonerRepository.GetDeliveryTonerByCurrentMonth();
            var month = context.TonerUsageRepository.GetTonerUsageByCurrentMonth();
            var currentMonth = DateTime.Now.Month;

            if (month == currentMonth)
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
