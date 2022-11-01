using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.Domain.Entities
{
   public class Customer : BaseModel
   {
      // Pmarimary key of the table.
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public int CustomerId { get; set; }

      // Name of companies
      //[Required(ErrorMessage = MessageConstants.RequiredFieldError)]
      [StringLength(200)]
      [Display(Name = "Company")]
      public string CustomerName { get; set; }

      // company address
      //[Required(ErrorMessage = MessageConstants.RequiredFieldError)]
      [StringLength(200)]
      [Display(Name = "Address")]
      public string Address { get; set; }

      // company contact number
      //[Required(ErrorMessage = MessageConstants.RequiredFieldError)]
      [StringLength(14)]
      [Display(Name = "Phone No")]
      public string Phone { get; set; }

      // Navigation Property
      public virtual IEnumerable<Project>? Projects { get; set; }
   }
}
