using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.Domain.Entities
{
    public class MachineToner : BaseModel
    {
        public int MachineTonerId { get; set; }

        [ForeignKey("MachineId")]
        public int MachineId { get; set; }

        [ForeignKey("TonerId")]
        public int TonerId { get; set; }

        public virtual Machine? Machine { get; set; }
        public virtual Toner? Toner { get; set; }
    }
}
