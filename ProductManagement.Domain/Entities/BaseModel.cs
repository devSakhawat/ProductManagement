using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.Domain.Entities
{
   // Base properties of the model classes.
   public class BaseModel
   {
      //Creation date of the row.
      [Column(TypeName = "smalldatetime")]
      [Display(Name = "Date Created")]
      public DateTime? DateCreated { get; set; }

      // Reference of the user who has created the row.
      public long? CreatedBy { get; set; }

      // Last modification date of the row.
      [Column(TypeName = "smalldatetime")]
      [Display(Name = "Date Modified")]
      public DateTime? DateModified { get; set; }

      // Reference of the user who has last modified the row.
      public long? ModifiedBy { get; set; }

      // Status of the row. It indicates the row is deleted or not.
      [Display(Name = "Row Status")]
      public bool IsDeleted { get; set; }
   }
}
