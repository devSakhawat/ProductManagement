using ProductManagement.DAL.Constracts;
using ProductManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.DAL.Repositories
{
   public class UnitOfWork : IUnitOfWork
   {
      protected readonly TonerContext context;
      public UnitOfWork(TonerContext context)
      {
         this.context = context;
      }

      public async Task<int> SaveChangesAsync()
      {
         return await context.SaveChangesAsync();
      }

      #region Customer
      private ICustomerRepository customerRepository;
      public ICustomerRepository CustomerRepository
      {
         get
         {
            if (customerRepository == null)
               customerRepository = new CustomerRepository(context);

            return customerRepository;
         }
      }
      #endregion

      #region Project
      private IProjectRepository projectRepository;
      public IProjectRepository ProjectRepository
      {
         get
         { 
            if(projectRepository == null)
               projectRepository = new ProjectRepository(context);

            return projectRepository;
         }
      }
      #endregion

      #region Machine
      private IMachineRepository machineRepository;
      public IMachineRepository MachineRepository
      {
         get
         {
            if (machineRepository == null)
               machineRepository = new MachineRepository(context);

            return machineRepository;
         }
      }
      #endregion

      #region Toner
      private ITonerRepository tonerRepository;
      public ITonerRepository TonerRepository
      {
         get 
         {
            if(tonerRepository == null)
               tonerRepository = new TonerRepository(context);

            return TonerRepository;
         }
      }

      ITonerRepository IUnitOfWork.tonerRepository => throw new NotImplementedException();
      #endregion
   }
}
