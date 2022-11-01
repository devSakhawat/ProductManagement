using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Domain.Constants
{
   public static class RouteConstants
   {
      public const string BaseRoute = "tuso-api";

      #region UserAccounts
      public const string CreateUserAccount = "user-account";

      public const string ReadUserAccounts = "user-accounts";

      public const string ReadUserAccountByKey = "user-account/key/{key}";

      public const string ReadUserAccountByUsername = "user-account/{username}";

      public const string UserDetails = "user-account/details/{key}";

      public const string UpdateUserAccount = "user-account/{key}";

      public const string DeleteUserAccount = "user-account/{key}";

      public const string UserLogin = "user-account/login";

      public const string ChangedPassword = "user-account/change-password";

      public const string RecoveryRequest = "user-account/recovery-request";
      #endregion

      #region ApplicationPermission
      public const string CreateApplicationPermission = "application-permission";

      public const string ReadApplicationPermissions = "application-permissions";

      public const string ReadApplicationPermissionByKey = "application-permission/key/{OID}";

      public const string ReadApplicationPermissionByRole = "application-permission/role/{RoleID}";

      public const string ReadApplicationPermissionByModule = "application-permission/module/{ModuleID}";

      public const string ReadApplicationPermission = "application-permission/key";

      public const string UpdateApplicationPermission = "application-permission/{key}";

      public const string DeleteApplicationPermission = "application-permission/{key}";
      #endregion



   }
}