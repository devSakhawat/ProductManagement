using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProductManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProductManagement.DAL.Constracts
{
   public interface ICustomerRepository : IRepository<Customer>
   {
      //Returns a customer if key matched.
      public Task<Customer> GetCustomerByKey(int id);

      // Returns a customer if the name matched.
      public Task<Customer> GetCustomerByName(string customerName);

      // Returns all customer.
      public Task<IEnumerable<Customer>> GetCustomers();
   }
}
