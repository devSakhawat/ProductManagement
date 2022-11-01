using ProductManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.DAL.Constracts
{
   public interface IProjectRepository : IRepository<Project>
   {
      //Returns a project if key matched.
      public Task<Project> GetProjectByKey(int id);

      // Returns a project if the name matched.
      public Task<Project> GetProjectByProjectName(string customerName);

      // Returns all project.
      public Task<IEnumerable<Project>> GetProjects();
   }
}
