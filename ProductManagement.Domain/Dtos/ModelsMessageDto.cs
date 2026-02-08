using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Domain.Dtos
{
   public class ModelsMessage
   {
      public string Message { get; set; }
      public object EntityModel { get; set; }
   }
}
