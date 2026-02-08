using Microsoft.EntityFrameworkCore;
using ProductManagement.DAL.Constracts;
using ProductManagement.Domain.Dtos;
using ProductManagement.Domain.Entities;

namespace ProductManagement.DAL.Repositories
{
   public class MachineRepository : Repository<Machine>, IMachineRepository
   {
      public MachineRepository(TonerContext context) : base(context)
      {
      }

      public async Task<Machine> GetMachinByKey(int key)
      {
         try
         {
            return await FirstOrDefaultAsync(m => m.MachineId == key && m.IsDeleted == false);
         }
         catch (Exception)
         {
            throw;
         }
      }

      public async Task<IEnumerable<Machine>> GetMachinByProjectId(int key)
      {
         try
         {
            return await QueryAsync(m => m.ProjectId == key && m.IsDeleted == false);
         }
         catch (Exception)
         {
            throw;
         }
      }

      public async Task<IEnumerable<TonerUsageDetailsDto>> GetUsageDetailByProjectId(int key)
      {
         try
         {

            //// for dynamicly return current month or last month
            //var currentDate = DateTime.Now;
            //var currentMonth = currentDate.Month;
            //var currentYear = currentDate.Year;
            //var lastMonth = currentMonth - 1;

            //var deliverytonerLast = context.DeliveryToners.AsNoTracking().Where(dt => dt.MachineId == key).GroupBy(dt => dt.MachineId).Select(dt => dt.OrderByDescending(d => d.DateCreated).First()).ToList();
            // *** last reocord based on MachineId
            //var deliverytonerLast = context.DeliveryToners.AsNoTracking().AsEnumerable().OrderByDescending(dt => dt.DateCreated).Where(dl => dl.IsDeleted == false).DistinctBy(dt => dt.MachineId).ToList();

            //var lastData = context.DeliveryToners.AsNoTracking().AsEnumerable().GroupBy(dt => dt.MachineId).Select(dt => dt.OrderByDescending(dt => dt.DateCreated).Where(dt => dt.IsDeleted == false).First()).ToList();

            var lastDeliveryToner = await (from m in context.Machines.Where(m => m.ProjectId == key && m.IsDeleted == false)

                                           join dt in (from d in context.DeliveryToners.Where(dt => dt.IsDeleted == false)
                                                       group new { d } by new { d.MachineId } into g
                                                       select new
                                                       {
                                                          g.Key.MachineId,
                                                          DeliveryTonerId = g.Select(dt => dt.d.DeliveryTonerId).FirstOrDefault(),
                                                          BW = g.Select(dt => dt.d.BW).FirstOrDefault(),
                                                          Cyan = g.Select(dt => dt.d.Cyan).FirstOrDefault(),
                                                          Magenta = g.Select(dt => dt.d.Magenta).FirstOrDefault(),
                                                          Yellow = g.Select(dt => dt.d.Yellow).FirstOrDefault(),
                                                          Black = g.Select(dt => dt.d.Black).FirstOrDefault(),
                                                          ColourTotal = g.Select(dt => dt.d.ColourTotal).FirstOrDefault(),
                                                          DateCreated = g.OrderByDescending(dt => dt.d.DateCreated).Select(dt => dt.d.DateCreated).FirstOrDefault(),
                                                          CreatedBy = g.Select(dt => dt.d.CreatedBy).FirstOrDefault(),
                                                          DateModified = g.Select(dt => dt.d.DateModified).FirstOrDefault(),
                                                          ModifiedBy = g.Select(dt => dt.d.ModifiedBy).FirstOrDefault(),
                                                       }
                                                       )
                                              //join dt in context.DeliveryToners.AsNoTracking().GroupBy(dt => dt.MachineId).Select(dt => dt.First())
                                              //join dt in context.DeliveryToners.AsEnumerable().OrderByDescending(dt => dt.DateCreated).Where(dt => dt.IsDeleted == false).DistinctBy(dt => dt.MachineId)
                                              //on m.MachineId equals dt.MachineId
                                              on m.MachineId equals dt.MachineId into machineToner
                                              from deliveryToner in machineToner.DefaultIfEmpty()

                                              //join tu in (from t in context.TonerUsages.Where(tu => tu.IsDeleted == false)
                                              //            group t by  t.MachineId  into tug
                                              //            orderby tug.Select(tu => tu.MachineId)
                                              //            select new
                                              //            {
                                              //               tug.Key.,
                                              //               TonerUsageId = tug.Select(dt => dt.t.TonerUsageId).FirstOrDefault(),
                                              //               PercentageBW = tug.Select(dt => dt.t.PercentageBW).FirstOrDefault(),
                                              //               PercentageCyan = tug.Select(dt => dt.t.PercentageCyan).FirstOrDefault(),
                                              //               PercentageMagenta = tug.Select(dt => dt.t.PercentageMagenta).FirstOrDefault(),
                                              //               PercentageYellow = tug.Select(dt => dt.t.PercentageYellow).FirstOrDefault(),
                                              //               PercentageBlack = tug.Select(dt => dt.t.PercentageBlack).FirstOrDefault(),
                                              //               TotalColurParcentage = tug.Select(dt => dt.t.TotalColurParcentage).FirstOrDefault(),
                                              //               InHouse = tug.Select(dt => dt.t.InHouse).FirstOrDefault(),
                                              //               MonthlyTotalToner = tug.Select(dt => dt.t.MonthlyTotalToner).FirstOrDefault(),
                                              //               MonthlyUsedToner = tug.Select(dt => dt.t.MonthlyUsedToner).FirstOrDefault(),
                                              //               TotalToner = tug.Select(dt => dt.t.TotalToner).FirstOrDefault(),
                                              //               DateCreated = tug.OrderByDescending(tu => tu.t.DateCreated).Select(dt => dt.t.DateCreated).FirstOrDefault(),
                                              //               CreatedBy = tug.Select(dt => dt.t.CreatedBy).FirstOrDefault(),
                                              //               DateModified = tug.Select(dt => dt.t.DateCreated).FirstOrDefault(),
                                              //               ModifiedBy = tug.Select(dt => dt.t.ModifiedBy).FirstOrDefault()
                                              //            }
                                              //            )
                                              //  on m.MachineId equals tu.MachineId into tonerUse
                                              //from tonerUsage in tonerUse.DefaultIfEmpty()

                                           select new
                                           {
                                              // machine
                                              MachineId = m.MachineId,
                                              MachineModel = m.MachineModel,
                                              MachineSN = m.MachineSN,
                                              ColourType = m.ColourType,

                                              //// DeliveryToner
                                              DeliveryTonerId = deliveryToner != null ? deliveryToner.DeliveryTonerId : 0,
                                              BW = deliveryToner != null ? deliveryToner.BW : 0,
                                              Cyan = deliveryToner != null ? deliveryToner.Cyan : 0,
                                              Magenta = deliveryToner != null ? deliveryToner.Magenta : 0,
                                              Yellow = deliveryToner != null ? deliveryToner.Yellow : 0,
                                              Black = deliveryToner != null ? deliveryToner.Black : 0,
                                              ColourTotal = deliveryToner != null ? deliveryToner.ColourTotal : 0,
                                              DevliveryTonerDateCreated = deliveryToner != null ? deliveryToner.DateCreated : null,
                                              DevliveryTonerCreatedBy = deliveryToner != null ? deliveryToner.CreatedBy : 0,
                                              DevliveryTonerDateModified = deliveryToner != null ? deliveryToner.DateModified : null,
                                              DevliveryTonerModifiedBy = deliveryToner != null ? deliveryToner.ModifiedBy : 0,

                                              //-------- TonerUsage
                                              //TonerUsageId = tonerUsage != null ? tonerUsage.TonerUsageId : 0,
                                              //PercentageBW = tonerUsage != null ? tonerUsage.PercentageBW : 0,
                                              //PercentageCyan = tonerUsage != null ? tonerUsage.PercentageCyan : 0,
                                              //PercentageMagenta = tonerUsage != null ? tonerUsage.PercentageMagenta : 0,
                                              //PercentageYellow = tonerUsage != null ? tonerUsage.PercentageYellow : 0,
                                              //PercentageBlack = tonerUsage != null ? tonerUsage.PercentageBlack : 0,
                                              //TotalColurParcentage = tonerUsage != null ? tonerUsage.TotalColurParcentage : 0,
                                              //InHouse = tonerUsage != null ? tonerUsage.InHouse : 0,
                                              //MonthlyTotalToner = tonerUsage != null ? tonerUsage.MonthlyTotalToner : 0,
                                              //MonthlyUsedToner = tonerUsage != null ? tonerUsage.MonthlyUsedToner : 0,
                                              //TotalToner = tonerUsage != null ? tonerUsage.TotalToner : 0,
                                              //TonerUsageDateCreated = tonerUsage != null ? tonerUsage.DateCreated : null,
                                              //TonerUsageCreatedBy = tonerUsage != null ? tonerUsage.CreatedBy : 0,
                                              //TonerUsageDateModified = tonerUsage != null ? tonerUsage.DateModified : null,
                                              //TonerUsageModifiedBy = tonerUsage != null ? tonerUsage.ModifiedBy : 0


                                              //// ---- PaperUsage
                                              //PaperUsageId = paperUsage != null ? paperUsage.PaperUsageId : 0,
                                              //PreviousCounter = paperUsage != null ? paperUsage.PreviousCounter : 0,
                                              //CurrentCounter = paperUsage != null ? paperUsage.CurrentCounter : 0,
                                              //MonthlyTotalCounter = paperUsage != null ? paperUsage.MonthlyTotalCounter : 0,
                                              //PaperUsageDateCreated = paperUsage != null ? paperUsage.DateCreated : null,
                                              //PaperUsageCreatedBy = paperUsage != null ? paperUsage.CreatedBy : null,
                                              //PaperUsageDateModified = paperUsage != null ? paperUsage.DateModified : null,
                                              //PaperUsageModifiedBy = paperUsage != null ? paperUsage.ModifiedBy : null,

                                              //// ---- ProfiteCalculation
                                              //CalculationId = machinProfite != null ? machinProfite.CalculationId : 0,
                                              //CounterPerToner = machinProfite != null ? machinProfite.CounterPerToner : 0,
                                              //IsProfitable = machinProfite != null ? machinProfite.IsProfitable : false,
                                              //ProfiteDateCreate = machinProfite != null ? machinProfite.DateCreated : null,
                                              //ProfiteCreatedBy = machinProfite != null ? machinProfite.CreatedBy : null,
                                              //ProfiteDateModified = machinProfite != null ? machinProfite.DateModified : null,
                                              //ProfiteModifiedBy = machinProfite != null ? machinProfite.ModifiedBy : null
                                           }).ToListAsync();
            List<TonerUsageDetailsDto> tonerUsageDetails = new List<TonerUsageDetailsDto>();
            foreach (var tonerUsageDetail in lastDeliveryToner)
            {
               tonerUsageDetails.Add(new TonerUsageDetailsDto
               {
                  MachineId = tonerUsageDetail.MachineId,
                  MachineModel = tonerUsageDetail.MachineModel,
                  MachineSN = tonerUsageDetail.MachineSN,
                  ColourType = tonerUsageDetail.ColourType,

                  // DeliveryToner
                  DeliveryTonerId = tonerUsageDetail.DeliveryTonerId,
                  BW = tonerUsageDetail.BW,
                  Cyan = tonerUsageDetail.Cyan,
                  Magenta = tonerUsageDetail.Magenta,
                  Yellow = tonerUsageDetail.Yellow,
                  Black = tonerUsageDetail.Black,
                  ColourTotal = tonerUsageDetail.ColourTotal,
                  DevliveryTonerDateCreated = tonerUsageDetail.DevliveryTonerDateCreated,
                  DevliveryTonerCreatedBy = tonerUsageDetail.DevliveryTonerCreatedBy,
                  DevliveryTonerDateModified = tonerUsageDetail.DevliveryTonerDateModified,
                  DevliveryTonerModifiedBy = tonerUsageDetail.DevliveryTonerModifiedBy,
                  DeliveryMonth = tonerUsageDetail.DevliveryTonerDateCreated.Value.Month,
                  DeliveryYear = tonerUsageDetail.DevliveryTonerDateCreated.Value.Year,

                  ////-------- TonerUsage
                  //TonerUsageId = tonerUsageDetail.TonerUsageId,
                  //PercentageBW = tonerUsageDetail.PercentageBW,
                  //PercentageCyan = tonerUsageDetail.PercentageCyan,
                  //PercentageMagenta = tonerUsageDetail.PercentageMagenta,
                  //PercentageYellow = tonerUsageDetail.PercentageYellow,
                  //PercentageBlack = tonerUsageDetail.PercentageBlack,
                  //TotalColurParcentage = tonerUsageDetail.TotalColurParcentage,
                  //InHouse = tonerUsageDetail.InHouse,
                  //MonthlyTotalToner = tonerUsageDetail.MonthlyTotalToner,
                  //MonthlyUsedToner = tonerUsageDetail.MonthlyUsedToner,
                  //TotalToner = tonerUsageDetail.TotalToner,
                  //TonerUsageDateCreated = tonerUsageDetail.TonerUsageDateCreated,
                  //TonerUsageCreatedBy = tonerUsageDetail.TonerUsageCreatedBy,
                  //TonerUsageDateModified = tonerUsageDetail.TonerUsageDateModified,
                  //TonerUsageModifiedBy = tonerUsageDetail.TonerUsageModifiedBy
               });
            }

            return tonerUsageDetails;
         }
         catch (Exception)
         {
            throw;
         }
      }

      public async Task<Machine> GetMachinByMahcineName(string machineSN)
      {
         try
         {
            return await FirstOrDefaultAsync(m => m.MachineSN == machineSN && m.IsDeleted == false);
         }
         catch (Exception)
         {
            throw;
         }
      }

      public async Task<IEnumerable<Machine>> GetMachines()
      {
         try
         {
            return await QueryAsync(m => m.IsDeleted == false);
         }
         catch (Exception)
         {
            throw;
         }
      }
   }
}