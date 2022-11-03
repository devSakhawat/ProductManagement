using ProductManagement.Domain.Dtos;
using ProductManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.DAL.Constracts
{
   public interface ITonerUsageRepository : IRepository<TonerUsageDto>
   {
      public Task<TonerUsageDto> GetTonerUsageByKey(int key);

      public int GetTonerUsageByCurrentMonth();

      public Task<TonerUsageDto> TonerUsagesDto();
   }
}
