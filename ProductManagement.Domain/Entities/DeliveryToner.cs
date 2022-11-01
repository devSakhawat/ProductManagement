using ProductManagement.Domain.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProductManagement.Domain.Entities
{
   public class DeliveryToner : BaseModel
   {
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public int DeliveryTonerId { get; set; }

      // Tonar Model
      [Display(Name = "Black&White")]
      public double? BW { get; set; }

      // 
      [Display(Name = "Cyan")]
      public double? Cyan { get; set; }

      // 
      public double? Magenta { get; set; }

      // 
      public double? Yellow { get; set; }

      // Last Month delivery toner (if machine is black : field will fillup with BW value)
      // if(machine is colour: field value will be fillup with colourTotal vlaue
      public double? ColourTotal { get; set; }

      [ForeignKey("MachineId")]
      public int MachineId { get; set; }

      // Navigation Property
      public virtual Machine? Machine { get; set; }
      public virtual IEnumerable<TonerUsage>? TonerUsages { get; set; }
   }
}
