using ProductManagement.DAL.Constracts;
using ProductManagement.Domain.Entities;

namespace ProductManagement.DAL.Repositories
{
   public class ProjectRepository : Repository<Project>, IProjectRepository
   {
      public ProjectRepository(TonerContext context) : base(context)
      {
      }

      public async Task<Project> GetProjectByKey(int id)
      {
         try
         {
            return await FirstOrDefaultAsync(p => p.ProjectId == id && p.IsDeleted == false);
         }
         catch (Exception)
         {
            throw;
         }
      }

      public async Task<IEnumerable<Project>> GetProjectByCustomerId(int id)
      {
         try
         {
            return await QueryAsync(p => p.CustomerId == id && p.IsDeleted == false);
         }
         catch (Exception)
         {
            throw;
         }
      }

      public async Task<Project> GetProjectByProjectName(string projectName)
      {
         try
         {
            return await FirstOrDefaultAsync(p => p.ProjectName.ToLower().Trim() == projectName.ToLower().Trim() && p.IsDeleted == false);
         }
         catch (Exception)
         {
            throw;
         }
      }

      public async Task<IEnumerable<Project>> GetProjects()
      {
         try
         {
            return await QueryAsync(p => p.IsDeleted == false, m => m.Machines);
         }
         catch (Exception)
         {
            throw;
         }
      }
   }
}