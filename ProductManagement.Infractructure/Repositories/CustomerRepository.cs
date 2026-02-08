using ProductManagement.DAL.Constracts;
using ProductManagement.Domain.Entities;

namespace ProductManagement.DAL.Repositories
{
   public class CustomerRepository : Repository<Customer>, ICustomerRepository
   {
      public CustomerRepository(TonerContext context) : base(context)
      {
      }

      public async Task<Customer> GetCustomerByKey(int id)
      {
         try
         {
            return await FirstOrDefaultAsync(c => c.CustomerId == id && c.IsDeleted == false, p => p.Projects);
         }
         catch (Exception)
         {
            throw;
         }
      }

      public async Task<Customer> GetCustomerByName(string customerName)
      {
         try
         {
            return await FirstOrDefaultAsync(c => c.CustomerName.ToLower().Trim() == customerName.ToLower().Trim() && c.IsDeleted == false);
         }
         catch (Exception)
         {
            throw;
         }
      }

      public async Task<IEnumerable<Customer>> GetCustomers()
      {
         try
         {
            return await QueryAsync(c => c.IsDeleted == false);
         }
         catch (Exception)
         {
            throw;
         }
      }
   }
}