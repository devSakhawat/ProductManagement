using ProductManagement.Domain.Entities;

namespace ProductManagement.Domain.Dtos
{
   public class TonerUsageDto : BaseModel
   {
      public int TonerUsageId { get; set; }

      // 
      //[Required(ErrorMessage = MessageConstants.RequiredFieldError)]
      //[Display(Name = "Machine")]
      //[ForeignKey("MachineId")]
      //public int MachineId { get; set; }

      // LastMonthDeliveryToner (if machine is black : field will fillup with BW value)
      // if(machine is colour: field value will be fillup with colourTotal vlaue
      // from DeliveryToner Table
      public int DeliveryTonerId { get; set; }

      // from machine table
      public int MachineId { get; set; }

      public double? TonnerPercentageBW { get; set; }
      public double? TonnerPercentageCyan { get; set; }
      public double? TonnerPercentageMagenta { get; set; }
      public double? TonnerPercentageYellow { get; set; }
      public double? TonnerPercentageBlack { get; set; }
      public double? TotalColurTonerParcentage { get; set; }

      // from machine table.
      // this field will come form machin table machine colourtype.
      public ColourType ColourType { get; set; }

      // form DeliveryToner table database. skip(1).take(1)
      // readOnly field.
      public int PreviousDeliveryToner { get; set; }

      // last month delivery unused toner
      public int InHouseToner { get; set; }

      // TonnerPercentage / 100
      public double InMachineToner { get; set; }

      //  InHouseToner + InMachineToner
      public double InHouseTotalToner { get; set; }

      // current month delivery toner form database.
      // if(ColourType == BW) MonthlyDeliveryToner will come form DeliveryToner.BW from database.
      // if(ColourType == Colour) MonthlyDeliveryToner will come form DeliveryToner.ColourTotal
      public int MonthlyDeliveryToner { get; set; }

      // calculated field (InHouseTotalToner + MonthlyDeliveryToner)
      public double MonthlyTonerStock { get; set; }

      // hidden field. for profite calculation.
      // PreviousDeliveryToner - InHouseTotalToner
      public double MonthlyUsedToner { get; set; }

      // calculated field (last month TotalToner + DeliveryToner)
      // last month TotalToner form database last record.
      public double TotalToner { get; set; }
   }
}
