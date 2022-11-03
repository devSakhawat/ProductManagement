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
      Task<Project> GetProjectByKey(int id);

      // Returns a project if the name matched.
      Task<Project> GetProjectByProjectName(string customerName);

      // Returns all project.
      Task<IEnumerable<Project>> GetProjects();
   }
}
