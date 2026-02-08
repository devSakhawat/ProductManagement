using ProductManagement.DAL.Constracts;
using ProductManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.DAL.Repositories
{
   public class TonerRepository : Repository<Toner>, ITonerRepository
   {
      public TonerRepository(TonerContext context): base(context)
      {
      }

      public async Task<Toner> GetTonerByKey(int key)
      {
         try
         {
            return await FirstOrDefaultAsync(t => t.TonerId == key && t.IsDeleted == false);
         }
         catch (Exception)
         {
            throw;
         }
      }

      public async Task<Toner> GetTonerBySerialNo(string serialNo)
      {
         try
         {
            return await FirstOrDefaultAsync(t => t.SerialNo == serialNo && t.IsDeleted == false);
         }
         catch (Exception)
         {
            throw;
         }
      }

      public async Task<IEnumerable<Toner>> GetToners()
      {
         try
         {
            return await QueryAsync(t => t.IsDeleted == false);
         }
         catch (Exception)
         {
            throw;
         }
      }
   }
}