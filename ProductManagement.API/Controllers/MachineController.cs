using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.DAL.Constracts;
using ProductManagement.Domain.Constants;
using ProductManagement.Domain.Entities;

namespace ProductManagement.API.Controllers
{
   [Route(RouteConstants.BaseRoute)]
   [ApiController]
   public class MachineController : ControllerBase
   {
      private readonly IUnitOfWork context;
      public MachineController(IUnitOfWork context)
      {
         this.context = context;
      }

      // URL: toner-api/machine
      // Object to be saved in the table as a row.
      [HttpPost]
      [Route(RouteConstants.CreateMachine)]
      public async Task<IActionResult> CreateMachine(Machine machine)
      {
         try
         {
            if(await IsDuplicate(machine) == true)
               return StatusCode(StatusCodes.Status409Conflict, MessageConstants.DuplicateError);

            context.MachineRepository.Add(machine);
            await context.SaveChangesAsync();

            return CreatedAtAction("ReadMachineByKey", new { key = machine.MachineId}, machine);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      // URL: toner-api/machines
      // return all machines.
      [HttpGet]
      [Route(RouteConstants.ReadMachines)]
      public async Task<IActionResult> ReadMachines()
      {
         try
         {
            var machines = await context.MachineRepository.GetMachines();
            return Ok(machines);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      // URL: toner-api/machine/key/{key}
      // Object to be saved in the table as a row.
      [HttpGet]
      [Route(RouteConstants.ReadMachineByKey)]
      public async Task<IActionResult> ReadMachinesByKey(int key)
      {
         try
         {
            if (key <= 0)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

            var machine = await context.MachineRepository.GetMachinByKey(key);

            if(machine != null)
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

            return Ok(machine);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      // URL: toner-api/machine/{key}
      // Object to be update in the table as a row.
      [HttpPut]
      [Route(RouteConstants.UpdateMachine)]
      public async Task<IActionResult> UpdateMachine(int key, Machine machine)
      {
         try
         {
            if (key != machine.MachineId)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.UnauthorizedAttemptOfRecordUpdateError);

            if(await IsDuplicate(machine) == true)
               return StatusCode(StatusCodes.Status409Conflict, MessageConstants.DuplicateError);

            context.MachineRepository.Update(machine);
            await context.SaveChangesAsync();
            return Ok(machine);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      // URL: toner-api/machine/{key}
      // Object to be update in the table as a row.
      [HttpDelete]
      [Route(RouteConstants.DeleteMachine)]
      public async Task<IActionResult> DeleteMachine(int key)
      {
         try
         {
            if (key <= 0)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);
            
            var machine = await context.MachineRepository.GetMachinByKey(key);

            if (machine == null)
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

            if (machine.Toners.Where(p => p.IsDeleted == false).ToList().Count() > 0)
               return StatusCode(StatusCodes.Status405MethodNotAllowed, MessageConstants.DependencyError);

            machine.IsDeleted = true;

            context.MachineRepository.Delete(machine);
            await context.SaveChangesAsync();

            return Ok(machine);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      // check duplicate data.
      private async Task<bool> IsDuplicate(Machine machine)
      {
         try
         {
            var machineInDb = await context.MachineRepository.GetMachinByMahcineName(machine.MachineSN);

            if (machineInDb != null)
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