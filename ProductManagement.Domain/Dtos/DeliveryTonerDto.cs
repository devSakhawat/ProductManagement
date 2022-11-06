using ProductManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProductManagement.Domain.Dtos
{
   public class DeliveryTonerDto : BaseModel
   {
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

      // 
      public double? Black { get; set; }

      // Last Month delivery toner (if machine is black : field will fillup with BW value)
      // if(machine is colour: field value will be fillup with colourTotal vlaue
      public double? ColourTotal { get; set; }

      public int MachineId { get; set; }

      //from machine table
      public string MachineSN { get; set; }
      public ColourType ColourType { get; set; }

      // current month toner delivery yes or no
      public int CurrentMonth { get; set; }
      public int CurrentYear { get; set; }
   }
}
