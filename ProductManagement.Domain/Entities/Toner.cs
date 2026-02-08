using ProductManagement.Domain.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.Domain.Entities
{
   public class Toner : BaseModel
   {
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public int TonerId { get; set; }

      // Tonar Model
      [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
      [Display(Name = "Tonar Model")]
      public string TonerModel { get; set; }

      // 
      [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
      [Display(Name = "Serial No")]
      public string SerialNo { get; set; }

      // 
      [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
      public ColourType Color { get; set; }

      public virtual IEnumerable<MachineToner?> MachineToners { get; set; }
   }
}
