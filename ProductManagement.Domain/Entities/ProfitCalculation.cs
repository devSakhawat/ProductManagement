using ProductManagement.Domain.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Domain.Entities
{
   public class ProfitCalculation : BaseModel
   {
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public int CalculationId { get; set; }

      // ekhane machineid dhore paperusage and tonerUsage er field guli niye aste pari.
      // taile indivisually paper usage and toner usege ke foreign key table hiseb e neya hoyni.
      [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
      [Display(Name = "Machine")]
      [ForeignKey("MachineId")]
      public int MachineId { get; set; }

      // paperUsage.MonthlyTotalCounter / tonerUsage.MonthlyUsedToner = cunter for per toner
      [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
      [Display(Name = "Counter Per Toner")]
      public double CounterPerToner { get; set; }

      // for bw =  if(CunterPerToner >= 600 ) = profit (yes)
      // colour = if(counterPerToner >= 400) = NonProfit (No)
      [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
      public bool IsProfitable { get; set; }

      public virtual Machine? Machine { get; set; }
   }
}
