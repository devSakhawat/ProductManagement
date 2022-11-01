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
      [Route(RouteConstants.ReadToner)]
      public async Task<IActionResult> ReadToner()
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

            throw;
         }
      }
      // URL: toner-api/machine/key/{key}
      // Object to be saved in the table as a row.


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