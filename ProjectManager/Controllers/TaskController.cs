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
            var writeAccessResult = _projectService.HasWriteAccessToProject(userId, ProjectId);
            if (!writeAccessResult.IsSuccess)
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

        [HttpPost("{taskId}/comments")]
        public IActionResult AddComment(int taskId, [FromBody] CreateCommentRequest request)
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(claim, out var userId))
            {
                return Unauthorized();
            }

            var task = _taskService.Get(taskId);
            if (task == null)
            {
                return NotFound();
            }

            var writeAccessResult = _projectService.HasWriteAccessToProject(userId, task.ProjectId);
            if (!writeAccessResult.IsSuccess)
            {
                return Forbid();
            }

            var result = _taskService.AddComment(taskId, userId, request);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Data);
        }

        [HttpGet("{taskId}/comments")]
        public IActionResult GetTaskComments(int taskId)
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(claim, out var userId))
            {
                return Unauthorized();
            }

            var task = _taskService.Get(taskId);
            if (task == null)
            {
                return NotFound();
            }

            var readAccessResult = _projectService.HasReadAccessToProject(userId, task.ProjectId);
            if (!readAccessResult.IsSuccess)
            {
                return Forbid();
            }

            var result = _taskService.GetTaskComments(taskId);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Data);
        }

        [HttpPost("comments/{commentId}/likes")]
        public IActionResult AddLikeToComment(int commentId)
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(claim, out var userId))
            {
                return Unauthorized();
            }

            var commentResult = _taskService.GetCommentById(commentId);
            if (!commentResult.IsSuccess)
            {
                return NotFound();
            }

            var task = _taskService.Get(commentResult.Data!.TaskId);
            if (task == null)
            {
                return NotFound();
            }

            var writeAccessResult = _projectService.HasWriteAccessToProject(userId, task.ProjectId);
            if (!writeAccessResult.IsSuccess)
            {
                return Forbid();
            }

            var result = _taskService.AddLikeToComment(commentId, userId);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Data);
        }
    }
}
