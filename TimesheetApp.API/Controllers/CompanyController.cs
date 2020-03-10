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
    public class CompaniesController : Controller
    {
        private readonly IServices _service;

        public CompaniesController(IServices service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<CompanyDto>), 200)]
        [ProducesResponseType(401)]
        [ProducesErrorResponseType(typeof(void))]
        public async Task<IActionResult> GetAllCompanies()
        {
            var companyList = await _service.GetAllCompanies();

            return StatusCode(200, companyList);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CompanyDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesErrorResponseType(typeof(void))]
        public async Task<IActionResult> CreateCompany(CompanyToCreateDto companyToCreate)
        {
            CompanyDto createdCompany = await _service.CreateCompany(companyToCreate);
            if (createdCompany == null)
            {
                return StatusCode(400);
            }
            return StatusCode(200, createdCompany);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CompanyDto), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesErrorResponseType(typeof(void))]
        public async Task<IActionResult> GetCompanyById(int id)
        {
            var comp = await _service.GetCompanyById(id);
            if (comp == null)
            {
                return StatusCode(404);
            }
            return StatusCode(200, comp);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CompanyDto), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesErrorResponseType(typeof(void))]
        public async Task<IActionResult> UpdateCompany(int id, CompanyToUpdateDto companyToUpdate)
        {
            CompanyDto updatedCompany = await _service.UpdateCompany(id, companyToUpdate);
            if (updatedCompany == null)
            {
                return StatusCode(404);
            }
            return StatusCode(200, updatedCompany);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesErrorResponseType(typeof(void))]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            if (!await _service.DeleteCompany(id))
            {
                return StatusCode(404);
            }
            return StatusCode(200);
        }

        [HttpGet("{id}/Roles")]
        [ProducesResponseType(typeof(List<CompanyRoleDto>), 200)]
        [ProducesResponseType(401)]
        [ProducesErrorResponseType(typeof(void))]
        public async Task<IActionResult> GetAllCompanyRoles(int id)
        {
            var companyRoleList = await _service.GetAllCompanyRoles(id);

            return StatusCode(200, companyRoleList);
        }

        [HttpPost("{id}/Roles")]
        [ProducesResponseType(typeof(CompanyRoleDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesErrorResponseType(typeof(void))]
        public async Task<IActionResult> CreateCompanyRole(int id, CompanyRoleToCreateDto companyRole)
        {
            CompanyRoleDto createdRole = await _service.CreateCompanyRole(id, companyRole);
            if (createdRole == null)
            {
                return StatusCode(400);
            }
            return StatusCode(200, createdRole);
        }

        [HttpGet("{id}/Roles/{roleId}")]
        [ProducesResponseType(typeof(CompanyRoleDto), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesErrorResponseType(typeof(void))]
        public async Task<IActionResult> GetCompanyRoleById(int roleId)
        {
            CompanyRoleDto companyRole = await _service.GetCompanyRole(roleId);
            if (companyRole == null)
            {
                return StatusCode(404);
            }
            return StatusCode(200, companyRole);
        }

        [HttpPut("{id}/Roles/{roleId}")]
        [ProducesResponseType(typeof(CompanyRoleDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesErrorResponseType(typeof(void))]
        public async Task<IActionResult> UpdateCompanyRole(int id, int roleId, CompanyRoleToUpdateDto companyRole)
        {
            CompanyRoleDto defaultCompanyRole = await _service.GetDefaultCompanyRole(id);

            if(defaultCompanyRole.Id == roleId)
            {
                return StatusCode(400, "You can not update the default role!");
            }

            CompanyRoleDto updatedRole = await _service.UpdateCompanyRole(roleId, companyRole);
            if (updatedRole == null)
            {
                return StatusCode(400);
            }
            return StatusCode(200, updatedRole);
        }

        [HttpDelete("{id}/Roles/{roleId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesErrorResponseType(typeof(void))]
        public async Task<IActionResult> DeleteCompanyRole(int id, int roleId)
        {
            CompanyRoleDto defaultCompanyRole = await _service.GetDefaultCompanyRole(id);

            if (defaultCompanyRole.Id == roleId)
            {
                return StatusCode(400, "You can not delete the default role!");
            }

            if (!await _service.DeleteCompanyRole(roleId))
            {
                return StatusCode(404);
            }
            return StatusCode(200);
        }

        [HttpGet("{id}/Users")]
        [ProducesResponseType(typeof(List<UserDto>), 200)]
        [ProducesResponseType(401)]
        [ProducesErrorResponseType(typeof(void))]
        public async Task<IActionResult> GetAllCompanyUsers(int id)
        {
            var companyUserList = await _service.GetAllCompanyUsers(id);
            return StatusCode(200, companyUserList);
        }

        [HttpPost("{id}/Users")]
        [ProducesResponseType(typeof(List<CompanyRoleDto>), 200)]
        [ProducesResponseType(401)]
        [ProducesErrorResponseType(typeof(void))]
        public async Task<IActionResult> CreateCompanyUser(int id, string email)
        {
            await _service.AddUserToCompany(id, email);
            return StatusCode(200);
        }

        [HttpDelete("{id}/Users/{userId}")]
        [ProducesResponseType(typeof(List<CompanyRoleDto>), 200)]
        [ProducesResponseType(401)]
        [ProducesErrorResponseType(typeof(void))]
        public async Task<IActionResult> DeleteCompanyUser(int id, int userId)
        {
            await _service.DeleteCompanyUser(id, userId);
            return StatusCode(200);
        }

        [HttpGet("{id}/Users/{userId}/Roles")]
        [ProducesResponseType(typeof(List<UserDto>), 200)]
        [ProducesResponseType(401)]
        [ProducesErrorResponseType(typeof(void))]
        public async Task<IActionResult> GetCompanyUserRoles(int id, int userId)
        {
            List<CompanyRoleDto> userRoles = await _service.GetCompanyRolesFromUser(id, userId);
            return StatusCode(200, userRoles);
        }

        [HttpPost("{id}/Users/{userId}/Roles")]
        [ProducesResponseType(typeof(List<CompanyRoleDto>), 200)]
        [ProducesResponseType(401)]
        [ProducesErrorResponseType(typeof(void))]
        public async Task<IActionResult> CreateCompanyUserRole(int id, int userId, int roleId)
        {
            List<CompanyRoleDto> roles = await _service.AddRoleToCompanyUser(id, userId, roleId);
            return StatusCode(200, roles);
        }

        [HttpDelete("{id}/Users/{userId}/Roles/{roleId}")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesErrorResponseType(typeof(void))]
        public async Task<IActionResult> DeleteCompanyUserRole(int id, int userId, int roleId)
        {
            if(await _service.RemoveRoleFromCompanyUser(roleId, userId))
            {
                return StatusCode(200);
            }
            return StatusCode(400);
        }

        [HttpGet("{id}/Projects")]
        [ProducesResponseType(typeof(List<ProjectDto>),200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesErrorResponseType(typeof(void))]
        public async Task<IActionResult> GetProjects(int id)
        {
            List<ProjectDto> projects = await _service.GetProjectsByCompanyId(id);
            return StatusCode(200, projects);
        }
    }
}