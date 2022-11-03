using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using ProductManagement.DAL.Constracts;
using ProductManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.DAL.Repositories
{
   public class DeliveryTonerRepository : Repository<DeliveryToner>, IDeliveryTonerRepository
   {
      public DeliveryTonerRepository(TonerContext context) : base(context)
      {
      }

      public async Task<DeliveryToner> GetDeliveryTonerByKey(int key)
      {
         try
         {
            return await FirstOrDefaultAsync(dt => dt.DeliveryTonerId == key && dt.IsDeleted == false);
         }
         catch (Exception)
         {
            throw;
         }
      }

      public int GetDeliveryTonerByCurrentMonth()
      {
         try
         {
            //var lastDeliveryDate = context.DeliveryToners.ToList().OrderByDescending(dl => dl.DateCreated).Where(dl => dl.IsDeleted == false).Select(dl => dl.DateCreated).FirstOrDefault();
            //var shortDateString = Convert.ToDateTime(lastDeliveryDate).ToShortDateString().Replace("-", string.Empty); ;
            //var checkCurrentMonthTonerDelivery = shortDateString.Substring(2, shortDateString.Length - 2);

            var lastRecordCreateDate = context.DeliveryToners.OrderByDescending(dl => dl.DateCreated).Where(dl => dl.IsDeleted == false).Select(dl => dl.DateCreated).FirstOrDefault();
            var month = Convert.ToDateTime(lastRecordCreateDate).Month;


            // return value is {11}
            return month;
         }
         catch (Exception)
         {
            throw;
         }
      }

      public async Task<IEnumerable<DeliveryToner>> GetDeliveryToners()
      {
         try
         {
            return await QueryAsync(dl => dl.IsDeleted == false, tu => tu.TonerUsages);
         }
         catch (Exception)
         {
            throw;
         }
      }
   }
}