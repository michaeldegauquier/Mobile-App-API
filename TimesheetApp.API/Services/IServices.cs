using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimesheetApp.API.Dto;
using TimesheetApp.API.Models;

namespace TimesheetApp.API.Services
{
    public interface IServices
    {
        Task<T> AddAsync<T>(T entity) where T : class;
        Task<T> UpdateAsync<T>(T entity) where T : class;
        Task DeleteAsync<T>(T entity) where T : class;

        Task<List<LogDto>> GetAllLogs();
        Task<List<AddressDto>> GetAllAddresses();
        Task<List<ProjectDto>> GetAllProjects();

        Task<LogDto> GetLogById(int id);
        Task<AddressDto> GetAddressById(int id);
        Task<ProjectDto> GetProjectById(int id);

        Task<List<CompanyDto>> GetAllCompanies();
        Task<CompanyDto> GetCompanyById(int id);
        Task<CompanyDto> CreateCompany(CompanyToCreateDto companyToCreate);
        Task<CompanyDto> UpdateCompany(int companyId, CompanyToUpdateDto companyToUpdate);
        Task<bool> DeleteCompany(int companyId);

        Task<List<CompanyRoleDto>> GetAllCompanyRoles(int id);
        Task<CompanyRoleDto> GetCompanyRole(int companyRoleId);
        Task<CompanyRoleDto> CreateCompanyRole(int companyId, CompanyRoleToCreateDto roleToCreate);
        Task<CompanyRoleDto> UpdateCompanyRole(int companyRoleId, CompanyRoleToUpdateDto companyRoleToUpdate);
        Task<bool> DeleteCompanyRole(int companyRoleId);
        Task<CompanyRoleDto> GetDefaultCompanyRole(int companyId);

        Task<List<UserDto>> GetAllCompanyUsers(int companyId);
        //Returns all the roles a user has in the given company.
        Task<List<CompanyRoleDto>> GetCompanyRolesFromUser(int companyId, int userId);
        Task<List<CompanyRoleDto>> AddRoleToCompanyUser(int companyId, int userId, int roleId);
        Task<bool> RemoveRoleFromCompanyUser(int roleId, int userId);
        Task<bool> AddUserToCompany(int companyId, string email);
        Task<bool> DeleteCompanyUser(int companyId, int userId);

        Task<ProjectDto> CreateProject(ProjectToCreateDto projectToCreate);
        Task<ProjectDto> UpdateProject(int projectId, ProjectToUpdateDto updatedProject);
        Task<bool> DeleteProject(int projectId);
        Task<ProjectUserDto> AddUserToProject(int projectId, string email);
        Task<bool> RemoveUserFromProject(int projectId, int userId);
        Task<List<UserDto>> GetAllUsersFromProject(int projectId);

        Task<LogDto> CreateLog(LogToCreateDto log);
        Task<LogDto> UpdateLog(int logId, LogToUpdateDto updateLog);
        Task<bool> DeleteLog(int logId);

        Task<List<UserDto>> GetAllUsers();
        Task<UserDto> GetUserById(int id);
        Task<UserDto> GetUserByEmail(string email);
        Task<List<LogDto>> GetUserLogs(int userId);
        Task<List<CompanyDto>> GetUserCompanies(int userId);
        Task<UserDto> CreateUser(UserToCreateDto userToCreate);
        Task<UserDto> UpdateUser(UserToUpdateDto userToUpdate, int userId);
        Task<List<ProjectDto>> GetUserProjects(int userId);
        Task<bool> DeleteUser(int userId);

        Task<UserDto> CreateSession(UserToLoginDto userToLogin);

        Task<List<ProjectDto>> GetProjectsByCompanyId(int id);
    }
}
 