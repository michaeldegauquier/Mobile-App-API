using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimesheetApp.API.Dto;
using TimesheetApp.API.Services;

namespace TimesheetApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IServices _service;

        public UsersController(IServices service)
        {
            _service = service;
        }

        //Get all users
        [HttpGet]
        [ProducesResponseType(typeof(List<UserDto>), 200)]
        [ProducesResponseType(401)]
        [ProducesErrorResponseType(typeof(void))]
        public async Task<IActionResult> GetAllUsers(string email)
        {
            var userList = await _service.GetAllUsers();
            if (email != null)
            {
                userList = userList.Where(x => x.Email == email).ToList();
            }
            return StatusCode(200, userList);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserDto), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesErrorResponseType(typeof(void))]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _service.GetUserById(id);
            if (user == null)
            {
                return StatusCode(404);
            }
            return StatusCode(200, user);
        }

        [HttpGet("{id}/Logs")]
        [ProducesResponseType(typeof(UserDto), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesErrorResponseType(typeof(void))]
        public async Task<IActionResult> GetUserLogs(int id)
        {
            var loglist = await _service.GetUserLogs(id);
            return StatusCode(200, loglist);
        }

        [HttpGet("{id}/Companies")]
        [ProducesResponseType(typeof(UserDto), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesErrorResponseType(typeof(void))]
        public async Task<IActionResult> GetUserCompanies(int id)
        {
            var companyList = await _service.GetUserCompanies(id);
            return StatusCode(200, companyList);
        }

        //Create a new user
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(UserDto), 200)]
        [ProducesResponseType(400)]
        [ProducesErrorResponseType(typeof(void))]
        public async Task<IActionResult> Post(UserToCreateDto user)
        {
            UserDto createdUser = await _service.CreateUser(user);
            if (createdUser == null)
            {
                return StatusCode(400);
            }
            return StatusCode(200, createdUser);
        }

        //Update a user
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UserDto), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesErrorResponseType(typeof(void))]
        public async Task<IActionResult> Put(int id, UserToUpdateDto user)
        {
            UserDto updatedUser = await _service.UpdateUser(user, id);
            if (updatedUser == null)
            {
                return StatusCode(404);
            }
            return StatusCode(200, updatedUser);
        }

        //Delete a user
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesErrorResponseType(typeof(void))]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await _service.DeleteUser(id))
            {
                return StatusCode(404);
            }
            return StatusCode(200);
        }

        [HttpGet("{id}/projects")]
        [ProducesResponseType(typeof(List<ProjectDto>), 200)]
        [ProducesResponseType(401)]
        [ProducesErrorResponseType(typeof(void))]
        public async Task<IActionResult> GetAllUserProjects(int id)
        {
            var projList = await _service.GetUserProjects(id);

            return StatusCode(200, projList);
        }
    }
}