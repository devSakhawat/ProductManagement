using ProductManagement.Domain.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ProductManagement.Domain.Entities
{
   public class TonerUsage : BaseModel
   {
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public int TonerUsageId { get; set; }

      //// 
      //[Required(ErrorMessage = MessageConstants.RequiredFieldError)]
      //[Display(Name = "Machine")]
      //[ForeignKey("MachineId")]
      //public int MachineId { get; set; }

      // LastMonthDeliveryToner (if machine is black : field will fillup with BW value)
      // if(machine is colour: field value will be fillup with colourTotal vlaue
      // from DeliveryToner Table
      [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
      [ForeignKey("DeliveryTonerId")]
      public int DeliveryTonerId { get; set; }

      [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
      [Display(Name = "Percentage")]
      public double TonnerPercentage { get; set; }

      // TonnerPercentage / 100
      [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
      [Display(Name = "Machine Toner")]
      public double InMachineToner { get; set; }

      // This field will be readonly and comes DeliveryToner table last record. if BW type machine bw column
      // value comes to field or if colour type ColourTotal value will comes to field.
      [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
      [Display(Name = "Delivery Toner")]
      public int MonthlyDeliveryToner { get; set; }

      // calculated field (PrevDeliveryToner - InMachineDeliveryToner + DeliveryToner)
      [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
      [Display(Name = "Toner Stock")]
      public double MonthlyTonerStock { get; set; }

      // hidden field. for profite calculation
      [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
      public double MonthlyUsedToner { get; set; }

      // calculated field (last month TotalToner + DeliveryToner)
      [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
      [Display(Name = "Total Toner")]
      public double TotalToner { get; set; }

      // Navigation property
      //public virtual Machine Machine { get; set; }
      public virtual DeliveryToner? DeliveryToner { get; set; }
   }
}
