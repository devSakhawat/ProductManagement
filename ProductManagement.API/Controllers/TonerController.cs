using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.DAL.Constracts;
using ProductManagement.Domain.Constants;
using ProductManagement.Domain.Entities;

namespace ProductManagement.API.Controllers
{
   [Route(RouteConstants.BaseRoute)]
   [ApiController]
   public class TonerController : ControllerBase
   {
      private readonly IUnitOfWork context;
      public TonerController(IUnitOfWork context)
      {
         this.context = context;
      }

      // URL: toner-api/toner
      // Object to be saved in the table as a row.
      [HttpPost]
      [Route(RouteConstants.CreateToner)]
      public async Task<IActionResult> CreateToner(Toner toner)
      {
         try
         {
            if (await IsDuplicate(toner) == true)
               return StatusCode(StatusCodes.Status409Conflict, MessageConstants.DuplicateError);

            context.TonerRepository.Add(toner);
            await context.SaveChangesAsync();

            return CreatedAtAction("ReadTonerByKey", new { key = toner.TonarId}, toner);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }
      // URL: toner-api/toners
      // return all toners.
      [HttpPost]
      [Route(RouteConstants.ReadToners)]
      public async Task<IActionResult> ReadToners()
      {
         try
         {
            var toners =await context.TonerRepository.GetToners();

            if (toners == null)
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);
            
            return Ok(toners);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }
      // URL: toner-api/toner/key/{key}
      // return first match row data.
      [HttpGet]
      [Route(RouteConstants.ReadTonerByKey)]
      public async Task<IActionResult> ReadTonerByKey(int key)
      {
         try
         {
            if(key <= 0)
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.InvalidParameterError);

            var toner = await context.TonerRepository.GetTonerByKey(key);

            if (toner == null)
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

            return Ok(toner);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      // URL: toner-api/toner/{key}
      // Object to be update in the table as a row.
      [HttpPut]
      [Route(RouteConstants.UpdateToner)]
      public async Task<IActionResult> UpdateToner(int key, Toner toner)
      {
         try
         {
            if (key != toner.TonarId)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.UnauthorizedAttemptOfRecordUpdateError);

            if (await IsDuplicate(toner) == true)
               return StatusCode(StatusCodes.Status409Conflict, MessageConstants.DuplicateError);

            context.TonerRepository.Update(toner);
            await context.SaveChangesAsync();

            return Ok(toner);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      // URL: toner-api/toner/{key}
      // Object to be deleted form table as row.
      [HttpDelete]
      [Route(RouteConstants.DeleteToner)]
      public async Task<IActionResult> DeleteToner(int key)
      {
         try
         {
            if (key <= 0)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

            var toner = await context.TonerRepository.GetTonerByKey(key);

            if (toner == null)
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

            toner.IsDeleted = true;
            return Ok(toner);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      // check duplicate value.
      private async Task<bool> IsDuplicate(Toner toner)
      {
         try
         {
            var tonerInDb = await context.TonerRepository.GetTonerBySerialNo(toner.SerialNo);

            if (tonerInDb != null)
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