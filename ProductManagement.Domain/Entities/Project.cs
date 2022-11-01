using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductManagement.Domain.Constants;

namespace ProductManagement.Domain.Entities
{
   public class Project : BaseModel
   {
      // Primary key of the table
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public int ProjectId { get; set; }

      // Company branch Name
      [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
      [StringLength(200)]
      public string ProjectName { get; set; }

      // Company branch address
      [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
      [StringLength(200)]
      public string Location { get; set; }

      // Foreign key. 
      [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
      public int CustomerId { get; set; }

      // Navigation property
      [ForeignKey("CustomerId")]
      public virtual Customer Customer { get; set; }

      // List of Child
      public virtual IEnumerable<Machine> Machines { get; set; }
   }
}
