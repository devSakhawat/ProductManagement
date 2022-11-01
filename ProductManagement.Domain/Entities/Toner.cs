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
   public class Toner : BaseModel
   {
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public int TonarId { get; set; }

      // Tonar Model
      [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
      [Display(Name = "Tonar Model")]
      public string TonarModel { get; set; }

      // 
      [Display(Name = "Serial No")]
      public string? SerialNo { get; set; }

      // 
      [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
      public ColourType Color { get; set; }


      // It's not company stock or sells its only for tracking paper use based on toner.
      // Toner depend on Machine so machine id is required.
      // there is no orphan tonar.
      // here main fact is machine base toner use and paper use.
      [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
      [ForeignKey("MachineId")]
      public int MachineId { get; set; }

      // Navigation Property
      public virtual Machine Machine { get; set; }
   }
}
