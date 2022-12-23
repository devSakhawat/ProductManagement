
using Newtonsoft.Json;
using ProductManagement.Domain.Entities;
using System.Text;

namespace ProductManagement.WEB.HttpClients
{
   public class ProjectHttpClient
   {
      private readonly HttpClient client;
      private readonly string BaseApi = "https://localhost:7284/toner-api/";
      public ProjectHttpClient(HttpClient client)
      {
         this.client = client;
      }

      // Create Project
      //public async Task<Project> AddProject(Project project)
      //{
      //   if (project.ProjectId == 0)
      //   {
      //      var data = JsonConvert.SerializeObject(project);
      //      var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
      //      var response = client.PostAsync($"{BaseApi}Project/project");
      //   }
      //}
   }
}
