using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using ProductManagement.DAL.Constracts;
using ProductManagement.Domain.Dtos;
using ProductManagement.Domain.Entities;
using System.Reflection.PortableExecutable;

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

      // For every machine select that call database instantly.
      public async Task<IEnumerable<DeliveryTonerDto>> GetDeliveryTonerByMachineId(int machineId)
      {
         try
         {
            var currentDate = DateTime.Now;
            var currentYear = currentDate.Year;
            var currentMonth = currentDate.Month;

            //var deliveryTonerByMachine = context.Machines.Where(m => m.MachineId == machineId).LeftJoin(context.DeliveryToners,);
            //var deliveryTonersList = context.DeliveryToners.Where(dl => dl.IsDeleted == false && dl.DateCreated.Value.Month == currentMonth && dl.DateCreated.Value.Year == currentYear && dl.MachineId == machineId);

            var deliveryToners = await (from m in context.Machines.Where(m => m.MachineId == machineId)
                                        join dl in context.DeliveryToners.Where(dl => dl.DateCreated.Value.Month == currentMonth && dl.DateCreated.Value.Year == currentYear)
                                        //where (dl.DateCreated.Value.Month == currentMonth && dl.DateCreated.Value.Year == currentYear)
                                        on m.MachineId equals dl.MachineId into machineTonerDelivery
                                        from deliveryToner in machineTonerDelivery.DefaultIfEmpty()
                                        select new
                                        {
                                           MachineId = m.MachineId,
                                           MachineSN = m.MachineSN,
                                           ColourType = m.ColourType,
                                           DeliveryTonerId = deliveryToner != null ? deliveryToner.DeliveryTonerId : 0,
                                           BW = deliveryToner != null ? deliveryToner.BW : 0,
                                           Cyan = deliveryToner != null ? deliveryToner.Cyan : 0,
                                           Magenta = deliveryToner != null ? deliveryToner.Magenta : 0,
                                           Yellow = deliveryToner != null ? deliveryToner.Yellow : 0,
                                           Black = deliveryToner != null ? deliveryToner.Black : 0,
                                           DateCreated = deliveryToner != null ? deliveryToner.DateCreated : null,
                                           CreatedBy = deliveryToner != null ? deliveryToner.CreatedBy : 0,
                                           DateModified = deliveryToner != null ? deliveryToner.DateModified : null,
                                           ModifiedBy = deliveryToner != null ? deliveryToner.ModifiedBy : 0
                                        }).ToListAsync();
            List<DeliveryTonerDto> currentDeliveryToner = new List<DeliveryTonerDto>();
            foreach (var deliveryToner in deliveryToners)
            {
               if (deliveryToner.DateCreated == null)
               {
                  currentDeliveryToner.Add(new DeliveryTonerDto
                  {
                     MachineId = deliveryToner.MachineId,
                     MachineSN = deliveryToner.MachineSN,
                     ColourType = deliveryToner.ColourType,
                     DeliveryTonerId = deliveryToner.DeliveryTonerId,
                     BW = deliveryToner.BW,
                     Cyan = deliveryToner.Cyan,
                     Magenta = deliveryToner.Magenta,
                     Yellow = deliveryToner.Yellow,
                     Black = deliveryToner.Black,
                     CurrentMonth = 0,
                     CurrentYear = 0,
                     DateCreated = deliveryToner.DateCreated,
                     CreatedBy = deliveryToner.CreatedBy,
                     DateModified = deliveryToner.DateModified,
                     ModifiedBy = deliveryToner.ModifiedBy
                  });
               }
               //else if (deliveryToner.DateModified == null)
               //{
               //   currentDeliveryToner.Add(new DeliveryTonerDto
               //   {
               //      MachineId = deliveryToner.MachineId,
               //      MachineSN = deliveryToner.MachineSN,
               //      ColourType = deliveryToner.ColourType,
               //      DeliveryTonerId = deliveryToner.DeliveryTonerId,
               //      BW = deliveryToner.BW,
               //      Cyan = deliveryToner.Cyan,
               //      Magenta = deliveryToner.Magenta,
               //      Yellow = deliveryToner.Yellow,
               //      Black = deliveryToner.Black,
               //      CurrentMonth = 0,
               //      CurrentYear = 0,
               //      DateModified = deliveryToner.DateModified,
               //      ModifiedBy = deliveryToner.ModifiedBy
               //   });
               //}
               else
               {
                  currentDeliveryToner.Add(new DeliveryTonerDto
                  {
                     MachineId = deliveryToner.MachineId,
                     MachineSN = deliveryToner.MachineSN,
                     ColourType = deliveryToner.ColourType,
                     DeliveryTonerId = deliveryToner.DeliveryTonerId,
                     BW = deliveryToner.BW,
                     Cyan = deliveryToner.Cyan,
                     Magenta = deliveryToner.Magenta,
                     Yellow = deliveryToner.Yellow,
                     Black = deliveryToner.Black,
                     CurrentMonth = deliveryToner.DateCreated.Value.Month,
                     CurrentYear = deliveryToner.DateCreated.Value.Year,
                     DateCreated = deliveryToner.DateCreated,
                     CreatedBy = deliveryToner.CreatedBy,
                     DateModified = deliveryToner.DateModified,
                     ModifiedBy = deliveryToner.ModifiedBy
                  });
               }
            }
            return currentDeliveryToner;
         }
         catch (Exception)
         {
            throw;
         }
      }

      // will query will be upgrated for project base machine than machine base Delivery toner
      public async Task<IEnumerable<DeliveryTonerDto>> GetDeliveryTonerByDeliveryDate()
      {
         var currentDate = DateTime.Now;
         var currentYear = currentDate.Year;
         var currentMonth = currentDate.Month;

         try
         {
            var deliveryToners = await (from m in context.Machines
                                        join dl in context.DeliveryToners.Where(dl => dl.DateCreated.Value.Month == currentMonth && dl.DateCreated.Value.Year == currentYear)
                                        //where (dl.DateCreated.Value.Month == currentMonth && dl.DateCreated.Value.Year == currentYear)
                                        on m.MachineId equals dl.MachineId into machineTonerDelivery
                                        from deliveryToner in machineTonerDelivery.DefaultIfEmpty()
                                        select new
                                        {
                                           MachineId = m.MachineId,
                                           MachineSN = m.MachineSN,
                                           ColourType = m.ColourType,
                                           DeliveryTonerId = deliveryToner != null ? deliveryToner.DeliveryTonerId : 0,
                                           BW = deliveryToner != null ? deliveryToner.BW : 0,
                                           Cyan = deliveryToner != null ? deliveryToner.Cyan : 0,
                                           Magenta = deliveryToner != null ? deliveryToner.Magenta : 0,
                                           Yellow = deliveryToner != null ? deliveryToner.Yellow : 0,
                                           Black = deliveryToner != null ? deliveryToner.Black : 0
                                        }).ToListAsync();
            List<DeliveryTonerDto> currentDeliveryToner = new List<DeliveryTonerDto>();
            foreach (var deliveryToner in deliveryToners)
            {
               currentDeliveryToner.Add(new DeliveryTonerDto
               {
                  DeliveryTonerId = deliveryToner.DeliveryTonerId,
                  BW = deliveryToner.BW,
                  Cyan = deliveryToner.Cyan,
                  Magenta = deliveryToner.Magenta,
                  Yellow = deliveryToner.Yellow,
                  Black = deliveryToner.Black,
                  MachineId = deliveryToner.MachineId,
                  MachineSN = deliveryToner.MachineSN,
                  ColourType = deliveryToner.ColourType,
                  CurrentMonth = currentMonth,
                  CurrentYear = currentYear
               });
            }
            return currentDeliveryToner;

            // right outer join kora possible na.
            //var deliveryTonersList = await context.DeliveryToners.Where(dl => dl.IsDeleted == false && dl.DateCreated.Value.Month == currentMonth && dl.DateCreated.Value.Year == currentYear).Include(dl => dl.Machine).ToListAsync();


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

      public DeliveryToner GetLastDeliveryByMachineId(int machineId)
      {
         try
         {
            var lastDeliveryToner = context.DeliveryToners.Where(dl => dl.MachineId == machineId && dl.IsDeleted == false).OrderByDescending(dl => dl.DateCreated).FirstOrDefault();

            return lastDeliveryToner;
         }
         catch (Exception)
         {
            throw;
         }
      }
   }
}