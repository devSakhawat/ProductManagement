using ProductManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.DAL.Constracts
{
   public interface IMachineRepository : IRepository<Machine>
   {
      public Task<Machine> GetMachinByKey(int key);

      public Task<Machine> GetMachinByMahcineName(string machineSN);

      public Task<IEnumerable<Machine>> GetMachines();
   }
}
