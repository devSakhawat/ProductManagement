using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Domain.Constants
{
   public static class RouteConstants
   {
      public const string BaseRoute = "toner-api";

      #region Customer
      public const string CreateCustomer = "customer";

      public const string ReadCustomers = "customers";

      public const string ReadCustomerByKey = "customer/key/{key}";

      public const string ReadCustomerByCustomerName = "customer/{customerName}";

      public const string UpdateCustomer = "customer/{key}";

      public const string DeleteCustomer = "customer/{key}";
      #endregion


   }
}