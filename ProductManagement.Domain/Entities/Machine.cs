﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ProductManagement.Domain.Constants;

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
      public int ProjectId { get; set; }
      [ForeignKey("ProjectId")]

      //Navigation Property
      public virtual Project Project { get; set; }

      public IEnumerable<Toner> Toners { get; set; }
      public IEnumerable<DeliveryToner> DeliveryToners { get; set; }
      //public IEnumerable<TonerUsage> TonerUsages { get; set; }
      public IEnumerable<PaperUsage> PaperUsages { get; set; }
      public IEnumerable<ProfitCalculation> ProfitCalculations { get; set; }

   }
}