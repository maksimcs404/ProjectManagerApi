using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Application.DTO.Request;
using ProjectManager.Application.Interfaces;
using System.Security.Claims;

namespace ProjectManager.Controllers
{


    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : ControllerBase
    {

        //TODO: добавить логирование, добавить DTO для ответа, доделать добавление участника в проект
        private readonly IProjectService _projectService;
        public ProjectController(IProjectService _service)
        {
            _projectService = _service;
        }


        [HttpPost("create")]
        public IActionResult CreateProject([FromBody] CreateProjectRequest request)
        {
            try
            {
                int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

                var response = _projectService.CreateProject(userId, request.Title, request.Description);
                if (response.IsSuccess)
                    return Ok(response.Data);
                else
                    return BadRequest(response.Error);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the project: " + ex.Message);
            }

        }

        [Authorize]
        [HttpGet("projects")]
        public IActionResult GetAllProject()
        {
            try
            {
                int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                if (userId == 0)
                    return Unauthorized("User ID not found in token.");
                Console.WriteLine(userId);

                var projects = _projectService.GetMyProjects(userId);
                if (projects.IsSuccess)
                    return Ok(projects.Data);
                else
                    return StatusCode(500, "Something went wrong");

            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving projects: " + ex.Message);
            }
        }
        [Authorize]
        [HttpPost("{id}/members")]
        public IActionResult AddMemberToProject(int id, [FromBody] AddMemberRequest request)
        {
            try
            {
                Console.WriteLine("1");
                int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

                var isOwnerResult = _projectService.IsOwnerOfTheProject(userId, id);

                if (userId == request.userId)
                    return BadRequest("You cannot add yourself as a member."); 
                if (!isOwnerResult.IsSuccess)
                    return StatusCode(403, "Only project owner can add members.");

                if (userId == 0)
                    return Unauthorized("User ID not found in token.");

                var result = _projectService.AddMemberToProject(id, request.userId, request.Role);
                if (!result.IsSuccess)
                {
                    Console.WriteLine("1");
                    return StatusCode(400, result.Error);
                }
                return Ok(result.Data);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while adding member to project: " + ex.Message);
            }
        }
    }
        
}
