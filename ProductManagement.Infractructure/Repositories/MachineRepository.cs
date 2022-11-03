using ProductManagement.DAL.Constracts;
using ProductManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.DAL.Repositories
{
   public class MachineRepository : Repository<Machine>, IMachineRepository
   {
      public MachineRepository(TonerContext context): base(context)
      {
      }

      public async Task<Machine> GetMachinByKey(int key)
      {
         try
         {
            return await FirstOrDefaultAsync(m => m.MachineId == key && m.IsDeleted == false);
         }
         catch (Exception)
         {
            throw;
         }
      }

      public async Task<Machine> GetMachinByMahcineName(string machineSN)
      {
         try
         {
            return await FirstOrDefaultAsync(m => m.MachineSN == machineSN && m.IsDeleted == false);
         }
         catch (Exception)
         {
            throw;
         }
      }

      public async Task<IEnumerable<Machine>> GetMachines()
      {
         try
         {
            return await QueryAsync(m => m.IsDeleted == false, t => t.Toners);
         }
         catch (Exception)
         {
            throw;
         }
      }
   }
}