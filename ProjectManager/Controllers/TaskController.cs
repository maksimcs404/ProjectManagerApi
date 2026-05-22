using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Application.DTO.Request;
using ProjectManager.Application.Interfaces;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace ProjectManager.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly IProjectService _projectService;
        public TaskController(ITaskService taskService, IProjectService projectService)
        {
            _taskService = taskService;
            _projectService = projectService;
        }
        [HttpPost("{ProjectId}/create")] // проверка на владельца проекта сделать
        public IActionResult CreateTask([FromBody] CreateTaskRequest request, int ProjectId)
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(claim, out var userId))
            {
                return Unauthorized();
            }
            var projectResult = _projectService.GetProjectById(ProjectId);



            if (!projectResult.IsSuccess)
            {
                return NotFound();
            }


            var project = projectResult.Data;

            if (project!.OwnerId != userId)
            {
                return Forbid();
            }

            var result = _taskService.Create(request, userId, ProjectId);

            if (!result.IsSuccess)
            {
                Console.WriteLine(result.Error);
                return StatusCode(500, "Something went wrong.");
            }
            return Ok(result.Data);
           
        }

        [HttpGet]
        public IActionResult Get()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(claim, out var userId))
            {
                return Unauthorized();
            }

            var result = _taskService.GetAll(userId);
            return Ok(result);
        }
    }
}
