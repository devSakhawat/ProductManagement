using ProductManagement.Domain.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.Domain.Entities
{
   public class Machine : BaseModel
   {
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public int MachineId { get; set; }

      // Machine model number
      [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
      [Display(Name = "Machine Model")]
      public string MachineModel { get; set; }

      // Machine serial number
      [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
      [Display(Name = "Machine Serial No")]
      public string MachineSN { get; set; }

      //// 0 = B&W and 1= Colour
      //[Required(ErrorMessage = MessageConstants.RequiredFieldError)]
      //public bool colorType { get; set; }

      // Hard Coded value (BW or Colour)
      [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
      [Display(Name = "Machine Type")]
      public ColourType ColourType { get; set; }

      // Dropdown value
      [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
      [Display(Name = "Project")]
      //[ForeignKey("ProjectId")]
      public int ProjectId { get; set; }

      //Navigation Property
      public virtual Project? Project { get; set; }

      public virtual IEnumerable<Toner>? Toners { get; set; }
      public virtual IEnumerable<DeliveryToner>? DeliveryToners { get; set; }
      //public IEnumerable<TonerUsage> TonerUsages { get; set; }
      public virtual IEnumerable<PaperUsage>? PaperUsages { get; set; }
      public virtual IEnumerable<ProfitCalculation>? ProfitCalculations { get; set; }

   }
}
