using ProductManagement.Domain.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.Domain.Entities
{
   public class PaperUsage : BaseModel
   {
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public int PaperUsageId { get; set; }

      [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
      [Display(Name = "Machine")]
      [ForeignKey("MachineId")]
      public int MachineId { get; set; }

      [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
      [Display(Name = "Previous Counter")]
      public long PreviousCounter { get; set; }

      [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
      [Display(Name = "Current Counter")]
      public long CurrentCounter { get; set; }

      // TonnerPercentage / 100
      [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
      [Display(Name = "Total Counter")]
      public long MonthlyTotalConter { get; set; }

      // Navigation property
      public virtual Machine? Machine { get; set; }
   }
}
