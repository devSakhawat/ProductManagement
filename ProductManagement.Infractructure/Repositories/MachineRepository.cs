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
            var currentDate = DateTime.Now;
            var currentMonth = currentDate.Month;
            var currentYear = currentDate.Year;
            var lastMonthPaperUse = currentMonth - 1;
            var tonerUsageDetailByProject = await (from m in context.Machines.Where(m => m.ProjectId == key && m.IsDeleted == false)
                                                   join dl in context.DeliveryToners.Where(dl => dl.DateCreated.Value.Month == currentMonth && dl.DateCreated.Value.Year == currentYear && dl.IsDeleted == false)
                                                   on m.MachineId equals dl.MachineId into machineTonerDelivery
                                                   from deliveryToner in machineTonerDelivery.DefaultIfEmpty()

                                                   join tu in context.TonerUsages.Where(tu => tu.DateCreated.Value.Month == currentMonth && tu.DateCreated.Value.Year == currentYear && tu.IsDeleted == false )
                                                   on m.MachineId equals tu.MachineId into machineTonerUsage
                                                   from tonerUsage in machineTonerUsage.DefaultIfEmpty()

                                                   // get last month paper use for previous counter in front end
                                                   join pu in context.PaperUsages.Where(pu => pu.DateCreated.Value.Month == lastMonthPaperUse && pu.DateCreated.Value.Year == currentYear && pu.IsDeleted == false)
                                                   on m.MachineId equals pu.MachineId into machinePaperusage
                                                   from paperUsage in machinePaperusage.DefaultIfEmpty()

                                                   join pc in context.ProfitCalculations.Where(pc => pc.DateCreated.Value.Month == currentMonth && pc.DateCreated.Value.Year == currentYear && pc.IsDeleted == false)
                                                   on m.MachineId equals pc.MachineId into machinProfiteCalculation
                                                   from machinProfite in machinProfiteCalculation.DefaultIfEmpty()
                                                   select new
                                                   {
                                                      // machine
                                                      MachineId = m.MachineId,
                                                      MachineSN = m.MachineSN,
                                                      ColourType = m.ColourType,
                                                      // DeliveryToner
                                                      DeliveryTonerId = deliveryToner != null ? deliveryToner.DeliveryTonerId : 0,
                                                      BW = deliveryToner != null ? deliveryToner.BW : 0,
                                                      Cyan = deliveryToner != null ? deliveryToner.Cyan : 0,
                                                      Magenta = deliveryToner != null ? deliveryToner.Magenta : 0,
                                                      Yellow = deliveryToner != null ? deliveryToner.Yellow : 0,
                                                      Black = deliveryToner != null ? deliveryToner.Black : 0,
                                                      ColourTotal = deliveryToner != null ? deliveryToner.ColourTotal : 0,
                                                      DateCreated = deliveryToner != null ? deliveryToner.DateCreated : null,
                                                      CreatedBy = deliveryToner != null ? deliveryToner.CreatedBy : 0,
                                                      DateModified = deliveryToner != null ? deliveryToner.DateModified : null,
                                                      ModifiedBy = deliveryToner != null ? deliveryToner.ModifiedBy : 0,
                                                      //-------- TonerUsage
                                                      TonerUsageId = tonerUsage != null ? tonerUsage.TonerUsageId : 0,
                                                      PercentageBW = tonerUsage != null ? tonerUsage.PercentageBW: 0,
                                                      PercentageCyan = tonerUsage != null ? tonerUsage.PercentageCyan: 0,
                                                      PercentageMagenta = tonerUsage != null ? tonerUsage.PercentageMagenta: 0,
                                                      PercentageYellow = tonerUsage != null ? tonerUsage.PercentageYellow: 0,
                                                      PercentageBlack = tonerUsage != null ? tonerUsage.PercentageBlack : 0,
                                                      TotalColurParcentage = tonerUsage != null ? tonerUsage.TotalColurParcentage: 0,
                                                      InHouse = tonerUsage != null ? tonerUsage.InHouse: 0,
                                                      MonthlyTotalToner = tonerUsage != null ? tonerUsage.MonthlyTotalToner: 0,
                                                      MonthlyUsedToner = tonerUsage != null ? tonerUsage.MonthlyUsedToner: 0,
                                                      TotalToner = tonerUsage != null ? tonerUsage.TotalToner: 0,
                                                      TonerUsageDateCreate = tonerUsage != null ? tonerUsage.DateCreated: null,
                                                      TonerUsageDateModified = tonerUsage != null ? tonerUsage.DateModified: null,
                                                      // ---- PaperUsage
                                                      PaperUsageId = paperUsage != null ? paperUsage.PaperUsageId : 0,
                                                      PreviousCounter = paperUsage != null ? paperUsage.PreviousCounter: 0,
                                                      CurrentCounter = paperUsage != null ? paperUsage.CurrentCounter: 0,
                                                      MonthlyTotalCounter = paperUsage != null ? paperUsage.MonthlyTotalCounter: 0,
                                                      PaperUsageDateCreate = paperUsage != null ? paperUsage.DateCreated: null,
                                                      PaperUsageDateModified = paperUsage != null ? paperUsage.DateModified: null,
                                                      // ---- ProfiteCalculation
                                                      CalculationId = machinProfite != null ? machinProfite.CalculationId: 0,
                                                      CounterPerToner = machinProfite != null ? machinProfite.CounterPerToner: 0,
                                                      IsFrofitable = machinProfite != null ? machinProfite.IsFrofitable: false,
                                                      ProfiteDateCreate = machinProfite != null ? machinProfite.DateCreated: null,
                                                      ProfiteDateModified = machinProfite != null ? machinProfite.DateModified: null
                                                   }).ToListAsync();
            List<TonerUsageDetailsDto> tonerUsageDetails = new List<TonerUsageDetailsDto>();
            foreach (var tonerUsageDetail in tonerUsageDetailByProject)
            {
               if (tonerUsageDetail.DateCreated != null)
               {
                  tonerUsageDetails.Add(new TonerUsageDetailsDto
                  {
                     MachineId = tonerUsageDetail.MachineId,
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
                     DevliveryTonerDateCreated = tonerUsageDetail.DateCreated,
                     DevliveryTonerCreatedBy = tonerUsageDetail.CreatedBy,
                     DevliveryTonerDateModified = tonerUsageDetail.DateModified,
                     DevliveryTonerModifiedBy = tonerUsageDetail.ModifiedBy,
                     DeliveryMonth = tonerUsageDetail.DateCreated.Value.Month,
                     DeliveryYear = tonerUsageDetail.DateCreated.Value.Year,

                     //-------- TonerUsage
                     TonerUsageId = tonerUsageDetail.TonerUsageId,
                     PercentageBW = tonerUsageDetail.PercentageBW,
                     PercentageCyan = tonerUsageDetail.PercentageCyan,
                     PercentageMagenta = tonerUsageDetail.PercentageMagenta,
                     PercentageYellow = tonerUsageDetail.PercentageYellow,
                     PercentageBlack = tonerUsageDetail.PercentageBlack,
                     TotalColurParcentage = tonerUsageDetail.TotalColurParcentage,
                     InHouse = tonerUsageDetail.InHouse,
                     MonthlyTotalToner = tonerUsageDetail.MonthlyTotalToner,
                     MonthlyUsedToner = tonerUsageDetail.MonthlyTotalToner,
                     TotalToner = tonerUsageDetail.TotalToner,
                     TonerUsageDateCreated = tonerUsageDetail.DateCreated,
                     TonerUsageCreatedBy = tonerUsageDetail.CreatedBy,
                     TonerUsageDateModified = tonerUsageDetail.DateModified,
                     TonerUsageModifiedBy = tonerUsageDetail.ModifiedBy,
                     // ---- PaperUsage
                     PaperUsageId = tonerUsageDetail.PaperUsageId,
                     PreviousCounter = tonerUsageDetail.PreviousCounter,
                     CurrentCounter = tonerUsageDetail.CurrentCounter,
                     MonthlyTotalCounter = tonerUsageDetail.MonthlyTotalCounter,
                     PaperUsageDateCreated = tonerUsageDetail.DateCreated,
                     PaperUsageCreatedBy = tonerUsageDetail.CreatedBy,
                     PaperUsageDateModified = tonerUsageDetail.DateModified,
                     PaperUsageModifiedBy = tonerUsageDetail.ModifiedBy,
                     // ------------ ProfiteCalculation
                     CalculationId = tonerUsageDetail.CalculationId,
                     CunterPerToner = tonerUsageDetail.CounterPerToner,
                     IsFrofitable = tonerUsageDetail.IsFrofitable,
                     ProfiteCalculationDateCreated = tonerUsageDetail.DateCreated,
                     ProfiteCalculationCreatedBy = tonerUsageDetail.CreatedBy,
                     ProfiteCalculationDateModified = tonerUsageDetail.DateModified,
                     ProfiteCalculationModifiedBy = tonerUsageDetail.ModifiedBy
                  });
               }
               else
               {
                  tonerUsageDetails.Add(new TonerUsageDetailsDto
                  {
                     MachineId = tonerUsageDetail.MachineId,
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
                     DevliveryTonerDateCreated = tonerUsageDetail.DateCreated,
                     DevliveryTonerCreatedBy = tonerUsageDetail.CreatedBy,
                     DevliveryTonerDateModified = tonerUsageDetail.DateModified,
                     DevliveryTonerModifiedBy = tonerUsageDetail.ModifiedBy,
                     DeliveryMonth = 0,
                     DeliveryYear = 0,

                     //-------- TonerUsage
                     TonerUsageId = tonerUsageDetail.TonerUsageId,
                     PercentageBW = tonerUsageDetail.PercentageBW,
                     PercentageCyan = tonerUsageDetail.PercentageCyan,
                     PercentageMagenta = tonerUsageDetail.PercentageMagenta,
                     PercentageYellow = tonerUsageDetail.PercentageYellow,
                     PercentageBlack = tonerUsageDetail.PercentageBlack,
                     TotalColurParcentage = tonerUsageDetail.TotalColurParcentage,
                     InHouse = tonerUsageDetail.InHouse,
                     MonthlyTotalToner = tonerUsageDetail.MonthlyTotalToner,
                     MonthlyUsedToner = tonerUsageDetail.MonthlyTotalToner,
                     TotalToner = tonerUsageDetail.TotalToner,
                     TonerUsageDateCreated = tonerUsageDetail.DateCreated,
                     TonerUsageCreatedBy = tonerUsageDetail.CreatedBy,
                     TonerUsageDateModified = tonerUsageDetail.DateModified,
                     TonerUsageModifiedBy = tonerUsageDetail.ModifiedBy,
                     // ---- PaperUsage
                     PaperUsageId = tonerUsageDetail.PaperUsageId,
                     PreviousCounter = tonerUsageDetail.PreviousCounter,
                     CurrentCounter = tonerUsageDetail.CurrentCounter,
                     MonthlyTotalCounter = tonerUsageDetail.MonthlyTotalCounter,
                     PaperUsageDateCreated = tonerUsageDetail.DateCreated,
                     PaperUsageCreatedBy = tonerUsageDetail.CreatedBy,
                     PaperUsageDateModified = tonerUsageDetail.DateModified,
                     PaperUsageModifiedBy = tonerUsageDetail.ModifiedBy,
                     // ------------ ProfiteCalculation
                     CalculationId = tonerUsageDetail.CalculationId,
                     CunterPerToner = tonerUsageDetail.CounterPerToner,
                     IsFrofitable = tonerUsageDetail.IsFrofitable,
                     ProfiteCalculationDateCreated = tonerUsageDetail.DateCreated,
                     ProfiteCalculationCreatedBy = tonerUsageDetail.CreatedBy,
                     ProfiteCalculationDateModified = tonerUsageDetail.DateModified,
                     ProfiteCalculationModifiedBy = tonerUsageDetail.ModifiedBy
                  });
               }              
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