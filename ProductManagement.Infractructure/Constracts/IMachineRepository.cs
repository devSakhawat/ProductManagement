using ProductManagement.Domain.Dtos;
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
      Task<Machine> GetMachinByKey(int key);

      Task<IEnumerable<Machine>> GetMachinByProjectId(int key);

      Task<IEnumerable<TonerUsageDetailsDto>> GetUsageDetailByProjectId(int key);

      Task<Machine> GetMachinByMahcineName(string machineSN);

      Task<IEnumerable<Machine>> GetMachines();
   }
}
