using ProductManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.DAL.Constracts
{
   public interface ITonerRepository : IRepository<Toner>
   {
      public Task<Toner> GetTonerByKey(int key);

      public Task<Toner> GetTonerBySerialNo(string serialNo);

      public Task<IEnumerable<Toner>> GetToners();
   }
}
