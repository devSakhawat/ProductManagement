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

      #region Projects
      public const string CreateProject = "project";
      public const string ReadProjects = "projects";
      public const string ReadProjectByKey = "project/key/{key}";
      public const string ReadProjectByCustomerId = "project/customers/{key}";
      public const string ReadProjectByProjectName = "project/{projectName}";
      public const string UpdateProject = "project/{key}";
      public const string DeleteProject = "project/{key}";
      #endregion

      #region Machine
      public const string CreateMachine = "machine";
      public const string ReadMachines = "machines";
      public const string ReadMachineByKey = "machine/key/{key}";
      public const string ReadMachineByMachine = "machine/projects/{key}";
      public const string ReadMachinebyMachineName = "machine/{machineName}";
      public const string UpdateMachine = "machine/{key}";
      public const string DeleteMachine = "machine/{key}";
      #endregion

      #region Toner
      public const string CreateToner = "toner";
      public const string ReadToner = "toners";
      public const string ReadTonerByKey = "toner/key/{key}";
      public const string ReadTonerByTonerName = "toner/{tonerName}";
      public const string UpdateToner = "toner/{key}";
      public const string DeleteToner = "toner/{key}";
      #endregion

      #region DeliveryToner
      public const string CreateDeliveryToner = "delivery-toner";
      public const string ReadDeliveryToner = "delivery-toners";
      public const string ReadDeliveryTonerByKey = "delivery-toner/key/{key}";
      public const string UpdateDeliveryToner = "delivery-toner/{key}";
      public const string DeleteDeliveryToner = "delivery-toner/{key}";
      #endregion

      #region TonerUsage
      public const string CreateTonerUsage = "toner-usage";
      public const string ReadTonerUsage = "toner-usages";
      public const string ReadTonerUsageByKey = "toner-usage/key/{key}";
      public const string UpdateTonerUsage = "toner-usage/{key}";
      public const string DeleteTonerUsage = "toner-usage/{key}";
      #endregion

   }
}