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
      Task<Toner> GetTonerByKey(int key);

      Task<Toner> GetTonerBySerialNo(string serialNo);

      Task<IEnumerable<Toner>> GetToners();
   }
}
