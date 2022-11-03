using ProductManagement.Domain.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.Domain.Entities
{
   public class TonerUsage : BaseModel
   {
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public int TonerUsageId { get; set; }

      //[Required(ErrorMessage = MessageConstants.RequiredFieldError)]
      //[Display(Name = "Machine")]
      //[ForeignKey("MachineId")]
      //public int MachineId { get; set; }

      // LastMonthDeliveryToner (if machine is black : field will fillup with BW value)
      // if(machine is colour: field value will be fillup with colourTotal vlaue
      // from DeliveryToner Table

      [ForeignKey("DeliveryTonerId")]
      public int DeliveryTonerId { get; set; }

      [Display(Name = "Percentage BW")]
      public Nullable<double> PercentageBW { get; set; }

      [Display(Name = "Percentage Cyan")]
      public double? PercentageCyan { get; set; }

      [Display(Name = "Percentage Magenta")]
      public double? PercentageMagenta { get; set; }


      [Display(Name = "Percentage Yellow")]
      public double? PercentageYellow { get; set; }


      [Display(Name = "Percentage Black")]
      public double? PercentageBlack { get; set; }


      [Display(Name = "Colour Percentage")]
      public double? TotalColurParcentage { get; set; }

      //// TonnerPercentage / 100
      //[Required(ErrorMessage = MessageConstants.RequiredFieldError)]
      //[Display(Name = "Machine Toner")]
      //public double InMachineToner { get; set; }

      // This field will be readonly and comes DeliveryToner table last record. if BW type machine bw column
      // value comes to field or if colour type ColourTotal value will comes to field.
      [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
      [Display(Name = "Delivery Toner")]
      public int MonthlyDelivery { get; set; }

      // last month delivery unused toner
      public int InHouse { get; set; }

      // calculated field (PrevDeliveryToner - InMachineDeliveryToner + DeliveryToner)
      [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
      [Display(Name = "Toner Stock")]
      public double MonthlyTotalToner { get; set; }

      // hidden field. for profite calculation
      [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
      public double MonthlyUsedToner { get; set; }

      // calculated field (last month TotalToner + DeliveryToner)
      [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
      [Display(Name = "Total Toner")]
      public double TotalToner { get; set; }

      //Navigation property
      //public virtual Machine Machine { get; set; }
      public virtual DeliveryToner? DeliveryToner { get; set; }
   }
}
