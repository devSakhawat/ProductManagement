using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using ProductManagement.DAL.Constracts;
using ProductManagement.Domain.Dtos;
using ProductManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.DAL.Repositories
{
   public class DeliveryTonerRepository : Repository<DeliveryToner>, IDeliveryTonerRepository
   {
      public DeliveryTonerRepository(TonerContext context) : base(context)
      {
      }

      public async Task<DeliveryToner> GetDeliveryTonerByKey(int key)
      {
         try
         {
            return await FirstOrDefaultAsync(dt => dt.DeliveryTonerId == key && dt.IsDeleted == false);
         }
         catch (Exception)
         {
            throw;
         }
      }

      public async Task<IEnumerable<DeliveryTonerDto>> GetDeliveryTonerByDeliveryDate()
      {
         var currentDate = DateTime.Now;
         var currentYear = currentDate.Year;
         var currentMonth = currentDate.Month;

         try
         {
            var deliveryToners = await (from m in context.Machines
                                  join dl in context.DeliveryToners on m.MachineId equals dl.MachineId
                                  where (dl.DateCreated.Value.Month == currentMonth && dl.DateCreated.Value.Year == currentYear)
                                  select new { 
                                    dl.DeliveryTonerId, dl.BW, dl.Cyan, dl.Magenta, dl.Yellow, dl.Black,dl.MachineId, m.MachineSN, m.ColourType
                                  }).ToListAsync();
            List<DeliveryTonerDto> currentDeliveryToner = new List<DeliveryTonerDto>();
            foreach (var deliveryToner in deliveryToners)
            {
               currentDeliveryToner.Add(new DeliveryTonerDto { 
                  DeliveryTonerId = deliveryToner.DeliveryTonerId,
                  BW = deliveryToner.BW,
                  Cyan = deliveryToner.Cyan,
                  Magenta = deliveryToner.Magenta,
                  Yellow = deliveryToner.Yellow,
                  Black = deliveryToner.Black,
                  MachineId=deliveryToner.MachineId,
                  MachineSN=deliveryToner.MachineSN,
                  ColourType=deliveryToner.ColourType,
                  CurrentMonth = currentMonth,
                  CurrentYear = currentYear
               });
            }

            var deliveryTonersList = await context.DeliveryToners.Where(dl => dl.IsDeleted == false && dl.DateCreated.Value.Month == currentMonth && dl.DateCreated.Value.Year == currentYear).Include(dl => dl.Machine).ToListAsync();
            return currentDeliveryToner;

            //var lastDeliveryDate = context.DeliveryToners.ToList().OrderByDescending(dl => dl.DateCreated).Where(dl => dl.IsDeleted == false).Select(dl => dl.DateCreated).FirstOrDefault();
            //var shortDateString = Convert.ToDateTime(lastDeliveryDate).ToShortDateString().Replace("-", string.Empty); ;
            //var checkCurrentMonthTonerDelivery = shortDateString.Substring(2, shortDateString.Length - 2);


            //var currentMonthDeliveryToner = await context.DeliveryToners.Where(dl => dl.IsDeleted == false && dl.MachineId == machineId).Include(dl => dl.Machine).FirstOrDefaultAsync();


            //await context.DeliveryToners.OrderByDescending(dl => dl.DateCreated).Where(dl => dl.IsDeleted == false).Include(dl => dl.Machine).FirstOrDefaultAsync();
            //var currentMonth = Convert.ToDateTime(currentMonthDeliveryToner.DateCreated).Month; ;
            //var lastRecordCreateDate = context.DeliveryToners.OrderByDescending(dl => dl.DateCreated).Where(dl => dl.IsDeleted == false).Select(dl => dl.DateCreated).FirstOrDefault();
            //var currentMonth = Convert.ToDateTime(currentMonthDeliveryToner).Month;


         }
         catch (Exception)
         {
            throw;
         }
      }

      public async Task<IEnumerable<DeliveryToner>> GetDeliveryToners()
      {


         var tryFromOne = context.DeliveryToners.OrderByDescending(dl => dl.DateCreated).Take(2).Include(dl => dl.Machine).ToList();
         var preDelToner = Convert.ToInt16(tryFromOne.Skip(1).Select(dl => dl.BW));
         var curDelToner = Convert.ToInt16(tryFromOne.FirstOrDefault().BW);

         try
         {
            return await QueryAsync(dl => dl.IsDeleted == false, tu => tu.TonerUsages);
         }
         catch (Exception)
         {
            throw;
         }
      }
   }
}