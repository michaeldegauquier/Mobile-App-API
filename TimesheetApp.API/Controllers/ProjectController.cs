using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TimesheetApp.API.Dto;
using TimesheetApp.API.Services;

namespace TimesheetApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : Controller
    {
        private readonly IServices _service;

        public ProjectsController(IServices service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ProjectDto>), 200)]
        [ProducesResponseType(401)]
        [ProducesErrorResponseType(typeof(void))]
        public async Task<IActionResult> GetAllProjects()
        {
            var projectList = await _service.GetAllProjects();
            return StatusCode(200, projectList);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProjectDto), 200)]
        [ProducesResponseType(401)]
        [ProducesErrorResponseType(typeof(void))]
        public async Task<IActionResult> Get(int id)
        {
            var project = await _service.GetProjectById(id);
            return StatusCode(200, project);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProjectDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesErrorResponseType(typeof(void))]
        public async Task<IActionResult> CreateProject(ProjectToCreateDto projectToCreate)
        {
            var project = await _service.CreateProject(projectToCreate);

            return StatusCode(200, project);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ProjectDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesErrorResponseType(typeof(void))]
        public async Task<IActionResult> UpdateProject(int id, ProjectToUpdateDto updatedProject)
        {
            var project = await _service.UpdateProject(id, updatedProject);
            return StatusCode(200, project);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesErrorResponseType(typeof(void))]
        public async Task<IActionResult> DeleteProject(int id)
        {
            await _service.DeleteProject(id);
            return StatusCode(200);
        }

        [HttpPost("{id}/users")]
        [ProducesResponseType(typeof(ProjectUserDto),200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesErrorResponseType(typeof(void))]
        public async Task<IActionResult> AddUserToProject(int id, string email)
        {
            var projectUser = await _service.AddUserToProject(id, email);

            if (projectUser != null)
            {
                return StatusCode(200, projectUser);
            }
            return null;
        }

        [HttpDelete("{id}/users")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesErrorResponseType(typeof(void))]
        public async Task<IActionResult> RemoveUserFromProject(int id, int userId)
        {
            await _service.RemoveUserFromProject(id, userId);
            return StatusCode(200);
        }

        [HttpGet("{id}/Users")]
        [ProducesResponseType(typeof(ProjectDto), 200)]
        [ProducesResponseType(401)]
        [ProducesErrorResponseType(typeof(void))]
        public async Task<IActionResult> GetAllUsers(int id)
        {
            var project = await _service.GetAllUsersFromProject(id);
            return StatusCode(200, project);
        }
    }
}