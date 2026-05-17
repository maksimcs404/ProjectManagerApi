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

        //TODO: добавить логирование, добавить DTO для ответа
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
            } catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the project: " + ex.Message);
            }
            
        }
    }
}
