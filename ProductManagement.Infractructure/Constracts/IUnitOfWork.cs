﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.DAL.Constracts
{
   public interface IUnitOfWork
   {
      Task<int> SaveChangesAsync();

      ICustomerRepository CustomerRepository { get; }

      IProjectRepository ProjectRepository { get; }

      IMachineRepository MachineRepository { get; }

      ITonerRepository tonerRepository { get; }
   }
}
