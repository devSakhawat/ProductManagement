using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using ProductManagement.DAL;
using ProductManagement.DAL.Constracts;
using ProductManagement.Domain.Constants;
using ProductManagement.Domain.Dtos;
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
      public async Task<IActionResult> CreateDeliveryToner(List<DeliveryTonerDto> deliveryToners)
      {
         try
         {
            if (deliveryToners == null)
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

            foreach (var deliveryToner in deliveryToners)
            {
               if (IsDeliverd(deliveryToner) == true)
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
               await context.SaveChangesAsync();
            };

            return RedirectToAction("Index");
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      // URL: toner-api/delivery-toners
      // All delivery toner data.
      [HttpGet]
      [Route(RouteConstants.ReadDeliveryToners)]
      public async Task<IActionResult> ReadDeliveryToners()
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
            if (key <= 0)
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


      // URL: toner-api/delivery-toner/machine/{machineId}
      // when machine will select than this mathod will call by ajax.
      [HttpGet]
      [Route(RouteConstants.ReadDeliveryTonerByMachineId)]
      public async Task<IActionResult> GetDeliveryTonerByMachineId(int machineId)
      {
         try
         {
            if (machineId <= 0)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

            var deliveryToner = await context.DeliveryTonerRepository.GetDeliveryTonerByMachineId(machineId);

            if (deliveryToner == null)
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

            return Ok(deliveryToner);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      // URL: toner-api/delivery-toner/machine/{machineId}
      // last delivary toner month value
      [HttpGet]
      [Route(RouteConstants.ReadDeliveryTonerByDeliveryDate)]
      public async Task<IActionResult> ReadDeliveryTonerByDeliveryDate(DateTime deliveryDate)
      {
         try
         {
            if (deliveryDate >= DateTime.Now)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

            var deliveryToner = await context.DeliveryTonerRepository.GetDeliveryTonerByDeliveryDate();

            //var currentMonth = Convert.ToDateTime(deliveryToner.DateCreated).Month;

            foreach (var deliveryTonerItem in deliveryToner)
            {
               if (deliveryTonerItem.ColourType == ColourType.BW)
               {
                  var deliveryTonerDto = new DeliveryTonerDto
                  {
                     DeliveryTonerId = deliveryTonerItem.DeliveryTonerId,
                     BW = deliveryTonerItem.BW,
                     MachineId = deliveryTonerItem.MachineId,
                     DateCreated = deliveryTonerItem.DateCreated,
                     CreatedBy = deliveryTonerItem.CreatedBy,

                     ColourType = deliveryTonerItem.ColourType,
                     MachineSN = deliveryTonerItem.MachineSN,
                     CurrentMonth = deliveryTonerItem.CurrentMonth
                  };
                  return Ok(deliveryTonerDto);
               }
               //else if (deliveryToner.Machine.ColourType == ColourType.Colour)
               //{
               //   var deliveryTonerDto = new DeliveryTonerDto
               //   {
               //      DeliveryTonerId = deliveryToner.DeliveryTonerId,
               //      Cyan = deliveryToner.Cyan,
               //      Magenta = deliveryToner.Magenta,
               //      Yellow = deliveryToner.Yellow,
               //      Black = deliveryToner.Black,
               //      ColourTotal = deliveryToner.ColourTotal,
               //      MachineId = deliveryToner.MachineId,
               //      DateCreated = deliveryToner.DateCreated,
               //      CreatedBy = deliveryToner.CreatedBy,

               //      ColourType = deliveryToner.Machine.ColourType,
               //      MachineSN = deliveryToner.Machine.MachineSN,
               //      CurrentMonth = currentMonth
               //   };
               //   return Ok(deliveryTonerDto);
               //}
               else
               {
                  return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
               }
            }

            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
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

            //if (deliveryToner.TonerUsages.Where(p => p.IsDeleted == false).ToList().Count() > 0)
            //   return StatusCode(StatusCodes.Status405MethodNotAllowed, MessageConstants.DependencyError);

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

      // check duplicate data 
      private bool IsDeliverd(DeliveryTonerDto deliveryToner)
      {
         try
         {
            //var ShortDateString = DateTime.Now.ToShortDateString().Replace("-", string.Empty);
            //var checkCurrentMonthTonerDelivery = ShortDateString.Substring(2, ShortDateString.Length - 2);
            //var data = context.DeliveryTonerRepository.GetDeliveryTonerByCurrentMonth();

            var lastDeliveryByMachine = context.DeliveryTonerRepository.GetLastDeliveryByMachineId(deliveryToner.MachineId);
            if (lastDeliveryByMachine == null)
            {
               return false;
            }
            else
            {
               var lastDeliveryMonth = lastDeliveryByMachine.DateCreated.Value.Month;
               var lastDeliveryYear = lastDeliveryByMachine.DateCreated.Value.Year;

               var currentMonth = DateTime.Now.Month;
               var currentYear = DateTime.Now.Year;
               if (currentMonth == lastDeliveryMonth && currentYear == lastDeliveryYear)
                  return true;
            }
            return false;
         }
         catch (Exception)
         {
            throw;
         }
      }
   }
}
