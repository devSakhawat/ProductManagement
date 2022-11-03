using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ProductManagement.DAL.Constracts;
using ProductManagement.DAL.Repositories;
using ProductManagement.Domain.Constants;
using ProductManagement.Domain.Dtos;
using ProductManagement.Domain.Entities;

namespace ProductManagement.API.Controllers
{
   [Route(RouteConstants.BaseRoute)]
   [ApiController]
   public class TonerUsageController : ControllerBase
   {
      private readonly IUnitOfWork context;
      public TonerUsageController(IUnitOfWork context)
      {
         this.context = context;
      }

      // URL: toner-api/toner-usage
      // Object to be saved in the table as a row.
      //[HttpPost]
      //[Route(RouteConstants.CreateTonerUsage)]
      //public async Task<IActionResult> CreateTonerUsage(TonerUsageDto tonerUsageDto)
      //{
      //   try
      //   {
      //      if (tonerUsageDto == null)
      //         return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

      //      if (IsUsage() == true)
      //         return StatusCode(StatusCodes.Status409Conflict, MessageConstants.DependencyError);


      //      var monthlyDeliveryToner = context.DeliveryTonerRepository.GetAll().OrderByDescending(dl => dl.DateCreated).Where(dl => dl.IsDeleted == false).FirstOrDefault();

      //      if (tonerUsageDto.ColourType == ColourType.BW)
      //      {
      //         var tonerUsageInDb = new TonerUsageDto
      //         {
      //            DeliveryTonerId = tonerUsageDto.DeliveryTonerId,
      //            TonnerPercentageBW = tonerUsageDto.TonnerPercentageBW,
      //            PreviousDeliveryToner = tonerUsageDto.PreviousDeliveryToner,
      //            InHouseToner = tonerUsageDto.InHouseToner,
      //            InMachineToner = Convert.ToDouble(tonerUsageDto.TonnerPercentageBW) / 100,
      //            InHouseTotalToner = Convert.ToDouble(tonerUsageDto.InHouseToner) + (Convert.ToDouble(tonerUsageDto.TonnerPercentageBW) / 100),
      //            MonthlyDeliveryToner = tonerUsageDto.MonthlyDeliveryToner,
      //            MonthlyTonerStock = Convert.ToDouble(tonerUsageDto.MonthlyDeliveryToner) + (Convert.ToDouble(tonerUsageDto.InHouseToner) + (Convert.ToDouble(tonerUsageDto.TonnerPercentageBW) / 100)),

      //         };
      //         context.TonerUsageRepository.AddTonerUsageDto(tonerUsageInDb);
      //      }
      //      else if (tonerUsageDto.ColourType == ColourType.Colour)
      //      {
      //         var tonerUsageInDb = new TonerUsageDto
      //         {
      //            DeliveryTonerId = tonerUsageDto.DeliveryTonerId,
      //            TonnerPercentageBW = tonerUsageDto.TonnerPercentageBW,
      //            PreviousDeliveryToner = tonerUsageDto.PreviousDeliveryToner,
      //            InHouseToner = tonerUsageDto.InHouseToner,
      //            InMachineToner = Convert.ToDouble(tonerUsageDto.TonnerPercentageBW) / 100,
      //            InHouseTotalToner = Convert.ToDouble(tonerUsageDto.InHouseToner) + (Convert.ToDouble(tonerUsageDto.TonnerPercentageBW) / 100),
      //            MonthlyDeliveryToner = tonerUsageDto.MonthlyDeliveryToner,
      //            MonthlyTonerStock = Convert.ToDouble(tonerUsageDto.MonthlyDeliveryToner) + (Convert.ToDouble(tonerUsageDto.InHouseToner) + (Convert.ToDouble(tonerUsageDto.TonnerPercentageBW) / 100)),
      //         };
      //         context.TonerUsageRepository.AddTonerUsageDto(tonerUsageInDb);
      //      }
      //      else
      //      {
      //         return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
      //      }


      //      await context.SaveChangesAsync();

      //      return CreatedAtAction("ReadTonerUsageByKey", new { key = tonerUsageDto.TonerUsageId}, tonerUsageDto);
      //   }
      //   catch (Exception)
      //   {
      //      return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
      //   }
      //}

      [HttpPost]
      [Route(RouteConstants.CreateTonerUsage)]
      public ModelsMessage CreateTonerUsage(TonerUsageDto tonerUsageDto)
      {
         ModelsMessage message = new ModelsMessage();
         //try
         //{
            context.TonerUsageRepository.AddTonerUsageDto(tonerUsageDto);
            //message.Message = tonerUsageDtoReturnMessage.ToString();
            //var categoryInDb = UnitOfWork.CategoryRepository.PostallCat(post);
            //msg.Response = categoryInDb;
            return message;
         //}
         //catch (Exception)
         //{
         //   message.Message = "Data not save";
         //   return message.Message;
         //}
         //return message.Message;
         //dbset.Add(entity);
         //message.Message = Commit("Saved");
         //message.EntityModel = entity;
         //return new ModelsMessage();
         //return message;
      }



      [HttpGet]
      [Route(RouteConstants.ReadTonerUsage)]
      public IActionResult ReadTonerUsage()
      {
         try
         {
            var dtoData = context.TonerUsageRepository.TonerUsagesDto();
            return Ok(dtoData);
         }
         catch (Exception)
         {

            throw;
         }
      }

      private bool IsUsage()
      {
         try
         {
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
