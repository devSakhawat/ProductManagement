using ProductManagement.Domain.Entities;

namespace ProductManagement.Domain.Dtos
{
   public class TonerUsageDetailsDto
   {
      // --- Machine info
      public int MachineId { get; set; }
      public string MachineModel { get; set; }
      public string MachineSN { get; set; }
      public ColourType ColourType { get; set; }
      public int ProjectId { get; set; }

      // --- Delivery Toner Info
      public int DeliveryTonerId { get; set; }
      public double? BW { get; set; }
      public double? Cyan { get; set; }
      public double? Magenta { get; set; }
      public double? Yellow { get; set; }
      public double? Black { get; set; }
      public double? ColourTotal { get; set; }

      public DateTime? DevliveryTonerDateCreated { get; set; }
      public long? DevliveryTonerCreatedBy { get; set; }
      public DateTime? DevliveryTonerDateModified { get; set; }
      public long? DevliveryTonerModifiedBy { get; set; }

      public int DeliveryMonth { get; set; }
      public int DeliveryYear { get; set; }

      // --- TonerUsage
      public int TonerUsageId { get; set; }
      public double? PercentageBW { get; set; }
      public double? PercentageCyan { get; set; }
      public double? PercentageMagenta { get; set; }
      public double? PercentageYellow { get; set; }
      public double? PercentageBlack { get; set; }
      public double? TotalColurParcentage { get; set; }
      public int InHouse { get; set; }
      public double MonthlyTotalToner { get; set; }
      public double MonthlyUsedToner { get; set; }
      public double TotalToner { get; set; }

      public DateTime? TonerUsageDateCreated { get; set; }
      public long? TonerUsageCreatedBy { get; set; }
      public DateTime? TonerUsageDateModified { get; set; }
      public long? TonerUsageModifiedBy { get; set; }

      public int TonerUsageMonth { get; set; }
      public int TonerUsageYear { get; set; }

      //--- PaperUsage
      public int PaperUsageId { get; set; }
      public long PreviousCounter { get; set; }
      public long CurrentCounter { get; set; }
      public long MonthlyTotalCounter { get; set; }

      public DateTime? PaperUsageDateCreated { get; set; }
      public long? PaperUsageCreatedBy { get; set; }
      public DateTime? PaperUsageDateModified { get; set; }
      public long? PaperUsageModifiedBy { get; set; }

      public int PaperUsageMonth { get; set; }
      public int PaperUsageYear { get; set; }

      // --- ProfiteCalculation
      public int CalculationId { get; set; }
      public double CunterPerToner { get; set; }
      public bool IsProfitable { get; set; }

      public DateTime? ProfiteDateCreated { get; set; }
      public long? ProfiteCreatedBy { get; set; }
      public DateTime? ProfiteDateModified { get; set; }
      public long? ProfiteModifiedBy { get; set; }

      // --- table types
      //public Machine Machines { get; set; }
      //public DeliveryToner DeliveryToners { get; set; }
      //public TonerUsage TonerUsages { get; set; }
      //public PaperUsage PaperUsages { get; set; }
      //public ProfitCalculation ProfitCalculations { get; set; }

      //public List<Machine> Machines { get; set; }
      //public List<DeliveryToner> DeliveryToners { get; set; }
      //public List<TonerUsage> TonerUsages { get; set; }
      //public List<PaperUsage> PaperUsages { get; set; }
      //public List<ProfitCalculation> ProfitCalculations { get; set; }
   }
}
