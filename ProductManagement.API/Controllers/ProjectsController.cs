using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.DAL.Constracts;
using ProductManagement.Domain.Constants;
using ProductManagement.Domain.Entities;

namespace ProductManagement.API.Controllers
{
   [Route(RouteConstants.BaseRoute)]
   [ApiController]
   public class ProjectsController : ControllerBase
   {
      private readonly IUnitOfWork context;
      public ProjectsController(IUnitOfWork context)
      {
         this.context = context;
      }

      // Object to be saved in the table as a row.
      [HttpPost]
      [Route(RouteConstants.CreateProject)]
      public async Task<IActionResult> CreateProject(Project project)
      {
         try
         {
            if (await IsDuplicate(project) == true)
               return StatusCode(StatusCodes.Status409Conflict, MessageConstants.DuplicateError);

            context.ProjectRepository.Add(project);
            await context.SaveChangesAsync();
            return CreatedAtAction("ReadProjectByKey", new { key = project.ProjectId }, project);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      // return all project.
      [HttpGet]
      [Route(RouteConstants.ReadProjects)]
      public async Task<IActionResult> ReadProjects()
      {
         try
         {
            var projects = await context.ProjectRepository.GetProjects();
            return Ok(projects);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }

      }

      // return match first row data.
      [HttpGet]
      [Route(RouteConstants.ReadProjectByKey)]
      public async Task<IActionResult> ReadProjectByKey(int key)
      {
         try
         {
            if (key <= 0)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

            var project = await context.ProjectRepository.GetProjectByKey(key);

            if (project == null)
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

            return Ok(project);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      // Object to be updated in the table as a row.
      [HttpPut]
      [Route(RouteConstants.UpdateProject)]
      public async Task<IActionResult> UpdateProject(int key, Project project)
      {
         try
         {
            if (key != project.ProjectId)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.UnauthorizedAttemptOfRecordUpdateError);

            if (await IsDuplicate(project) == true)
               return StatusCode(StatusCodes.Status409Conflict, MessageConstants.DuplicateError);

            context.ProjectRepository.Update(project);
            await context.SaveChangesAsync();

            return Ok(project);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      // Object to be remove from table.
      [HttpDelete]
      [Route(RouteConstants.DeleteProject)]
      public async Task<IActionResult> DeleteProject(int key)
      {
         try
         {
            if (key <= 0)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

            var project = await context.ProjectRepository.GetProjectByKey(key);

            if (project == null)
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

            if (project.Machines.Where(m => m.IsDeleted == false).ToList().Count() > 0)
               return StatusCode(StatusCodes.Status405MethodNotAllowed, MessageConstants.DependencyError);

            project.IsDeleted = false;
            context.ProjectRepository.Delete(project);
            await context.SaveChangesAsync();

            return Ok(project);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      // Duplicat value check
      private async Task<bool> IsDuplicate(Project project)
      {
         try
         {
            var projectInDb = await context.ProjectRepository.GetProjectByProjectName(project.ProjectName);

            if (projectInDb != null)
               return true;

            return false;
         }
         catch (Exception)
         {
            throw;
         }
      }
   }
}
