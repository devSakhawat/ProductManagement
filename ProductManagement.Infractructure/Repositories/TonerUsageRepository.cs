using Microsoft.EntityFrameworkCore;
using ProductManagement.DAL.Constracts;
using ProductManagement.Domain.Dtos;
using ProductManagement.Domain.Entities;

namespace ProductManagement.DAL.Repositories
{
   public class TonerUsageRepository : Repository<TonerUsageDto>, ITonerUsageRepository
   {
      public TonerUsageRepository(TonerContext context) : base(context)
      {
      }

      public async Task<TonerUsageDto> GetTonerUsageByKey(int key)
      {
         try
         {
            return await FirstOrDefaultAsync(tu => tu.TonerUsageId == key, tu => tu.IsDeleted == false);
         }
         catch (Exception)
         {
            throw;
         }
      }

      public int GetTonerUsageByCurrentMonth()
      {
         try
         {
            //var lastDeliveryDate = context.TonerUsages.ToList().OrderByDescending(dl => dl.DateCreated).Where(dl => dl.IsDeleted == false).Select(dl => dl.DateCreated).FirstOrDefault();
            //var shortDateString = Convert.ToDateTime(lastDeliveryDate).ToShortDateString().Replace("-", string.Empty); ;
            //var checkCurrentMonthTonerDelivery = shortDateString.Substring(2, shortDateString.Length - 2);

            var lastRecordCreateDate = context.TonerUsages.OrderByDescending(tu => tu.DateCreated).Where(tu => tu.IsDeleted == false).Select(tu => tu.DateCreated).FirstOrDefault();
            var month = Convert.ToDateTime(lastRecordCreateDate).Month;


            // return value is {Nov-22}
            return month;
         }
         catch (Exception)
         {
            throw;
         }
      }

      public async Task<TonerUsageDto> TonerUsagesDto()
      {
         try
         {

            var tryFromOne = context.DeliveryToners.OrderByDescending(dl => dl.DateCreated).Take(2).Include(dl => dl.Machine).ToList();
            var preDelToner = Convert.ToInt16(tryFromOne.Skip(1).Select(dl => dl.BW));
            var curDelToner = Convert.ToInt16(tryFromOne.FirstOrDefault().BW);

            //var prevDelTonerWithMachine = context.DeliveryToners.OrderByDescending(dl => dl.DateCreated).Skip(1).Take(1).Include(dl => dl.Machine);
            var prevDelTonerWithMachine = context.DeliveryToners.OrderByDescending(dl => dl.DateCreated).Skip(1).Include(dl => dl.Machine).FirstOrDefault();
            var preDeliveryToner = await context.DeliveryToners.OrderByDescending(dl => dl.DateCreated).FirstOrDefaultAsync();
            //var machineTypeWithOutInclude = preDeliveryToner.Select(dl => dl.Machine.ColourType);
            // firstordefult korle select kora lagena.
            //var machineType =Convert.ToInt32(prevDelTonerWithMachine.Select(dl => dl.Machine.ColourType));
            
            
            //var machineType = Convert.ToInt32(prevDelTonerWithMachine.Machine.ColourType);
            //if (machineType == Convert.ToInt32(ColourType.BW))
            //{
            //   var tonerUsagesDto = (from tu in context.TonerUsages
            //                         join dt in context.DeliveryToners on tu.DeliveryTonerId equals dt.DeliveryTonerId
            //                         join m in context.Machines on dt.MachineId equals m.MachineId
            //                         select new
            //                         {
            //                            DeliveryTonerId = tu.DeliveryTonerId,
            //                            MachineId = dt.MachineId,
            //                            ColourType = m.ColourType,
            //                            //PreviousDeliveryToner = prevDelTonerWithMachine.Select(dl => dl.BW),
            //                            PreviousDeliveryToner = prevDelTonerWithMachine.BW,
            //                            InHouseToner = tu.InHouse,
            //                            MonthlyDeliveryToner = preDeliveryToner.BW
            //                         });
            //}

            return new TonerUsageDto();
         }
         catch (Exception)
         {
            throw;
         }
      }
   }
}