using ProductManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.DAL.Constracts
{
   public interface IDeliveryTonerRepository : IRepository<DeliveryToner>
   {
      Task<DeliveryToner> GetDeliveryTonerByKey(int key);

      // to check that database contain current month delivery toner or not.
      // datetime will be splite for month and year only.
      int GetDeliveryTonerByCurrentMonth();

      Task<IEnumerable<DeliveryToner>> GetDeliveryToners();
   }
}
