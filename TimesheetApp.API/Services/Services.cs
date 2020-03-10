using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimesheetApp.API.Data;
using TimesheetApp.API.Dto;
using TimesheetApp.API.Models;

namespace TimesheetApp.API.Services
{
    public class Services : IServices
    {
        private readonly DataContext _context;

        public Services(DataContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync<T>(T entity) where T : class
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<T> UpdateAsync<T>(T entity) where T : class
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync<T>(T entity) where T : class
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }


        //Get all

        public async Task<List<LogDto>> GetAllLogs()
        {
            List<TimeLog> logList = await _context.TimeLogs.ToListAsync();
            List<LogDto> logDtoList = new List<LogDto>();

            foreach (TimeLog log in logList)
            {
                logDtoList.Add(ConvertToLogDto(log));
            }

            return logDtoList;
        }

        public async Task<List<AddressDto>> GetAllAddresses()
        {
            List<Address> addressesList = await _context.Addresses.ToListAsync();
            List<AddressDto> addressDtoList = new List<AddressDto>();

            foreach (Address address in addressesList)
            {
                addressDtoList.Add(ConvertToAddressDto(address));
            }

            return addressDtoList;
        }

        //Get by id
        public async Task<LogDto> GetLogById(int id)
        {
            TimeLog log = await _context.TimeLogs.FirstOrDefaultAsync(x => x.Id == id);
            return ConvertToLogDto(log);
        }

        public async Task<AddressDto> GetAddressById(int id)
        {
            Address addr = await _context.Addresses.FirstOrDefaultAsync(x => x.Id == id);
            return ConvertToAddressDto(addr);
        }

        #region Company methods

        public async Task<List<CompanyDto>> GetAllCompanies()
        {
            List<Company> companyList = await _context.Companies.Include(x => x.Address).ToListAsync();
            List<CompanyDto> companyDtoList = new List<CompanyDto>();

            foreach (var company in companyList)
            {
                companyDtoList.Add(ConvertToCompanyDto(company));
            }

            return companyDtoList;
        }

        public async Task<CompanyDto> GetCompanyById(int id)
        {
            Company comp = await _context.Companies.Include(x => x.Address).FirstOrDefaultAsync(x => x.Id == id);
            return ConvertToCompanyDto(comp);
        }

        public async Task<CompanyDto> CreateCompany(CompanyToCreateDto companyToCreate)
        {
            Address address = new Address
            {
                Country = companyToCreate.Address.Country,
                City = companyToCreate.Address.City,
                PostalCode = companyToCreate.Address.PostalCode,
                Street = companyToCreate.Address.Street,
                HouseNumber = companyToCreate.Address.HouseNumber,
                BoxNumber = companyToCreate.Address.BoxNumber
            };

            Company company = new Company
            {
                Name = companyToCreate.Name,
                Address = address
            };
            company = await AddAsync(company);

            CompanyRole defaultRole = new CompanyRole
            {
                CompanyId = company.Id,
                Name = "default",
                Description = "The default role every new user gets assigned.",
                IsDefaultRole = true
            };
            await AddAsync(defaultRole);


            return ConvertToCompanyDto(company);
        }

        public async Task<CompanyDto> UpdateCompany(int companyId, CompanyToUpdateDto companyToUpdate)
        {
            Company company = await _context.Companies.Include(x => x.Address).FirstOrDefaultAsync(x => x.Id == companyId);
            if (company == null)
            {
                return null;
            }

            company.Name = companyToUpdate.Name;
            company.Address.Country = companyToUpdate.Address.Country;
            company.Address.City = companyToUpdate.Address.City;
            company.Address.PostalCode = companyToUpdate.Address.PostalCode;
            company.Address.Street = companyToUpdate.Address.Street;
            company.Address.HouseNumber = companyToUpdate.Address.HouseNumber;
            company.Address.BoxNumber = companyToUpdate.Address.BoxNumber;

            return ConvertToCompanyDto(await UpdateAsync(company));
        }

        public async Task<bool> DeleteCompany(int companyId)
        {
            Company company = await _context.Companies.FirstOrDefaultAsync(x => x.Id == companyId);
            if (company == null && company.Id != 3)
            {
                return false;
            }
            await DeleteAsync(company);
            return true;
        }


        #endregion

        #region CompanyRole methods

        public async Task<List<CompanyRoleDto>> GetAllCompanyRoles(int id)
        {
            List<CompanyRole> companyRoleList = await _context.CompanyRoles.Where(x => x.CompanyId == id).ToListAsync();
            List<CompanyRoleDto> companyRoleDtoList = new List<CompanyRoleDto>();

            foreach (var x in companyRoleList)
            {
                companyRoleDtoList.Add(ConvertToCompanyRoleDto(x));
            }

            return companyRoleDtoList;
        }

        public async Task<CompanyRoleDto> GetCompanyRole(int companyRoleId)
        {
            CompanyRole companyRole = await _context.CompanyRoles.FirstOrDefaultAsync(x => x.Id == companyRoleId);
            if (companyRole == null)
            {
                return null;
            }
            return ConvertToCompanyRoleDto(companyRole);
        }

        public async Task<CompanyRoleDto> CreateCompanyRole(int companyId, CompanyRoleToCreateDto roleToCreate)
        {
            CompanyRole companyRole = new CompanyRole
            {
                CompanyId = companyId,
                Name = roleToCreate.Name,
                Description = roleToCreate.Description,
                ManageCompany = roleToCreate.ManageCompany,
                ManageUsers = roleToCreate.ManageUsers,
                ManageProjects = roleToCreate.ManageProjects,
                ManageRoles = roleToCreate.ManageRoles
            };
            companyRole = await AddAsync(companyRole);

            return ConvertToCompanyRoleDto(companyRole);
        }

        public async Task<CompanyRoleDto> UpdateCompanyRole(int companyRoleId, CompanyRoleToUpdateDto roleToUpdate)
        {
            CompanyRole companyRole = await _context.CompanyRoles.FirstOrDefaultAsync(x => x.Id == companyRoleId);

            if (companyRole == null)
            {
                return null;
            }

            companyRole.Id = companyRoleId;
            companyRole.Name = roleToUpdate.Name;
            companyRole.Description = roleToUpdate.Description;
            companyRole.ManageCompany = roleToUpdate.ManageCompany;
            companyRole.ManageUsers = roleToUpdate.ManageUsers;
            companyRole.ManageProjects = roleToUpdate.ManageProjects;
            companyRole.ManageRoles = roleToUpdate.ManageRoles;

            return ConvertToCompanyRoleDto(await UpdateAsync(companyRole));
        }

        public async Task<bool> DeleteCompanyRole(int companyRoleId)
        {
            CompanyRole companyRole = await _context.CompanyRoles.FirstOrDefaultAsync(x => x.Id == companyRoleId);
            if (companyRole == null)
            {
                return false;
            }
            await DeleteAsync(companyRole);
            return true;
        }

        public async Task<CompanyRoleDto> GetDefaultCompanyRole(int companyId)
        {
            CompanyRole defaultRole = await _context.CompanyRoles.Where(x => x.IsDefaultRole == true && x.CompanyId == companyId).FirstOrDefaultAsync();
            return ConvertToCompanyRoleDto(defaultRole);
        }

        #endregion

        #region CompanyUser methods

        public async Task<List<UserDto>> GetAllCompanyUsers(int companyId)
        {
            List<CompanyRole> companyRoles = await _context.CompanyRoles.Where(x => x.CompanyId == companyId).ToListAsync();
            List<User> companyUsers = new List<User>();

            foreach (var role in companyRoles)
            {
                List<User> roleUsers = await _context.Users.Where(x => x.UserCompanyRoles.Contains(x.UserCompanyRoles.FirstOrDefault(y => y.CompanyRoleId == role.Id))).ToListAsync();
                companyUsers.AddRange(roleUsers);
            }
            companyUsers = companyUsers.Distinct().ToList();
            List<UserDto> companyUsersDto = new List<UserDto>();

            foreach(var user in companyUsers)
            {
                companyUsersDto.Add(ConvertToUserDto(user));
            }
            return companyUsersDto;
        }

        public async Task<List<CompanyRoleDto>> GetCompanyRolesFromUser(int companyId, int userId)
        {
            List<CompanyRole> companyRoles = await _context.CompanyRoles.Where(x => x.CompanyId == companyId).ToListAsync();
            List<UserCompanyRole> userRoles = await _context.UserCompanyRoles.Where(x => x.UserId == userId).ToListAsync();

            List<CompanyRoleDto> roles = new List<CompanyRoleDto>();

            foreach(CompanyRole compRole in companyRoles)
            {
                foreach(UserCompanyRole userCompRole in userRoles)
                {
                    if(compRole.Id == userCompRole.CompanyRoleId)
                    {
                        roles.Add(ConvertToCompanyRoleDto(compRole));
                    }
                }
            }
            return roles;

        }

        public async Task<bool> AddUserToCompany(int companyId, string email)
        {
            UserDto user = await GetUserByEmail(email);
            List<CompanyRoleDto> userRoles = await GetCompanyRolesFromUser(companyId, user.ID);

            foreach(CompanyRoleDto role in userRoles)
            {
                if(role.CompanyId == companyId)
                {
                    return false;
                }
            }

            int defaultRoleId = (await GetDefaultCompanyRole(companyId)).Id;

            UserCompanyRole userRole = new UserCompanyRole
            {
                CompanyRoleId = defaultRoleId,
                UserId = user.ID
            };
            await AddAsync(userRole);

            return true;
        }

        public async Task<List<CompanyRoleDto>> AddRoleToCompanyUser(int companyId, int userId, int roleId)
        {
            UserCompanyRole userRole = new UserCompanyRole
            {
                CompanyRoleId = roleId,
                UserId = userId
            };
            await AddAsync(userRole);
            return null;
        }

        public async Task<bool> RemoveRoleFromCompanyUser(int roleId, int userId)
        {
            UserCompanyRole userRole = await _context.UserCompanyRoles.FirstOrDefaultAsync(x => x.CompanyRoleId == roleId && x.UserId == userId);

            if(userRole != null)
            {
                await DeleteAsync(userRole);
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteCompanyUser(int companyId, int userId)
        {
            List<CompanyRole> companyRoles = await _context.CompanyRoles.Where(x => x.CompanyId == companyId).ToListAsync();
            List<UserCompanyRole> userRoles = await _context.UserCompanyRoles.Where(x => x.UserId == userId).ToListAsync();

            List<CompanyRoleDto> roles = new List<CompanyRoleDto>();

            foreach (CompanyRole compRole in companyRoles)
            {
                foreach (UserCompanyRole userCompRole in userRoles)
                {
                    if (compRole.Id == userCompRole.CompanyRoleId)
                    {
                        await DeleteAsync(userCompRole);
                    }
                }
            }
            return true;
        }

        #endregion

        #region Project Methods

        public async Task<List<ProjectDto>> GetAllProjects()
        {
            List<Project> projectList = await _context.Projects.ToListAsync();
            List<ProjectDto> projectDtoList = new List<ProjectDto>();

            foreach (var project in projectList)
            {
                projectDtoList.Add(ConvertToProjectDto(project));
            }

            return projectDtoList;
        }

        public async Task<ProjectDto> GetProjectById(int id)
        {
            Project proj = await _context.Projects.FirstOrDefaultAsync(x => x.Id == id);
            return ConvertToProjectDto(proj);
        }

        public async Task<ProjectDto> CreateProject(ProjectToCreateDto projectToCreate)
        {
            Project project = new Project
            {
                CompanyId = projectToCreate.CompanyId,
                Name = projectToCreate.Name,
                Description = projectToCreate.Description
            };
            return ConvertToProjectDto(await AddAsync(project));
        }

        public async Task<ProjectDto> UpdateProject(int projectId, ProjectToUpdateDto updatedProject)
        {
            Project project = await _context.Projects.FirstOrDefaultAsync(x => x.Id == projectId);

            if(project == null)
            {
                return null;
            }

            project.CompanyId = updatedProject.CompanyId;
            project.Name = updatedProject.Name;
            project.Description = updatedProject.Description;

            await UpdateAsync(project);

            return null;
        }

        public async Task<bool> DeleteProject(int projectId)
        {
            Project project = await _context.Projects.FirstOrDefaultAsync(x => x.Id == projectId);
            if (project.Id != 3)
            {
                await DeleteAsync(project);
                return true;
            }
            return false;
        }

        public async Task<List<ProjectDto>> GetProjectsByCompanyId(int id)
        {
            List<Project> projList = await _context.Projects.Where(x => x.CompanyId == id).ToListAsync();
            List<ProjectDto> projDtoList = new List<ProjectDto>();

            foreach(var proj in projList)
            {
                projDtoList.Add(ConvertToProjectDto(proj));
            }
            return projDtoList;
        }

        public async Task<ProjectUserDto> AddUserToProject(int projectId, string email)
        {
            var user = await GetUserByEmail(email);
            var projectUser = new ProjectUser
            {
                ProjectId = projectId,
                UserId = user.ID
            };

            var result = ConvertToProjectUserDto(await AddAsync(projectUser));
            return result;
        }

        public async Task<bool> RemoveUserFromProject(int projectId, int userId)
        {
            ProjectUser projectUser = await _context.ProjectUsers.FirstOrDefaultAsync(x => x.UserId == userId && x.ProjectId == projectId);
            await DeleteAsync(projectUser);

            return true;
        }

        public async Task<List<UserDto>> GetAllUsersFromProject(int projectId)
        {
            var userProjects = await _context.ProjectUsers.Where(x => x.ProjectId == projectId).ToListAsync();
            var userDtoList = new List<UserDto>();

            foreach(var userProject in userProjects)
            {
                var user = await GetUserById(userProject.UserId);
                userDtoList.Add(user);
            }
            return userDtoList;
        } 

        #endregion

        #region User methods

        public async Task<List<UserDto>> GetAllUsers()
        {
            List<User> userList = await _context.Users.Include(x => x.Address).ToListAsync();
            List<UserDto> userDtoList = new List<UserDto>();

            foreach (User user in userList)
            {
                userDtoList.Add(ConvertToUserDto(user));
            }

            return userDtoList;
        }

        public async Task<UserDto> GetUserById(int id)
        {
            User user = await _context.Users.Include(x => x.Address).FirstOrDefaultAsync(x => x.Id == id);
            return ConvertToUserDto(user);
        }

        public async Task<UserDto> GetUserByEmail(string email)
        {
            email = email.ToLower();
            User user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (user == null)
            {
                return null;
            }
            return ConvertToUserDto(user);
        }

        public async Task<UserDto> CreateUser(UserToCreateDto userToCreate)
        {
            byte[] pswHash, pswSalt;
            CreatePasswordHash(userToCreate.Psw, out pswHash, out pswSalt);

            Address address = new Address
            {
                Country = userToCreate.Address.Country,
                City = userToCreate.Address.City,
                PostalCode = userToCreate.Address.PostalCode,
                Street = userToCreate.Address.Street,
                HouseNumber = userToCreate.Address.HouseNumber,
                BoxNumber = userToCreate.Address.BoxNumber
            };

            User user = new User
            {
                FirstName = userToCreate.FirstName,
                LastName = userToCreate.LastName,
                Email = userToCreate.Email.ToLower(),
                PhoneNumber = userToCreate.PhoneNumber,
                PswHash = pswHash,
                PswSalt = pswSalt,
                Address = address
            };

            var createdUser =  ConvertToUserDto(await AddAsync(user));

            if (createdUser != null)
            {
                ProjectUser projectUser = new ProjectUser
                {
                    ProjectId = 3,
                    UserId = createdUser.ID
                };
                await AddAsync(projectUser);
                await AddUserToCompany(3, createdUser.Email);
            }

            return createdUser;
        }

        public async Task<UserDto> UpdateUser(UserToUpdateDto updatedUser, int userId)
        {
            User userToUpdate = await _context.Users.Include(x => x.Address).FirstOrDefaultAsync(x => x.Id == userId);

            if (userToUpdate != null)
            {
                userToUpdate.FirstName = updatedUser.FirstName;
                userToUpdate.LastName = updatedUser.LastName;
                userToUpdate.Email = updatedUser.Email.ToLower();
                userToUpdate.PhoneNumber = updatedUser.PhoneNumber;
                userToUpdate.Address.Country = updatedUser.Address.Country;
                userToUpdate.Address.City = updatedUser.Address.City;
                userToUpdate.Address.PostalCode = updatedUser.Address.PostalCode;
                userToUpdate.Address.Street = updatedUser.Address.Street;
                userToUpdate.Address.HouseNumber = updatedUser.Address.HouseNumber;
                userToUpdate.Address.BoxNumber = updatedUser.Address.BoxNumber;

                return ConvertToUserDto(await UpdateAsync(userToUpdate));
            }
            return null;
        }

        public async Task<bool> DeleteUser(int userId)
        {
            User user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user != null)
            {
                await DeleteAsync(user);
                return true;
            }
            return false;
        }

        public async Task<List<LogDto>> GetUserLogs(int userId)
        {
            List<TimeLog> logList = await _context.TimeLogs.Where(x => x.UserId == userId).ToListAsync();
            List<LogDto> logDtoList = new List<LogDto>();

            foreach(TimeLog log in logList)
            {
                logDtoList.Add(ConvertToLogDto(log));
            }
            return logDtoList;
        }

        public async Task<List<CompanyDto>> GetUserCompanies(int userId)
        {
            var userCompanyRoles = _context.UserCompanyRoles.Include(x => x.CompanyRole).Where(x => x.UserId == userId);
            var companies = new List<CompanyDto>();

            foreach (var role in userCompanyRoles)
            {
                var company = await _context.Companies.FirstOrDefaultAsync(x => x.Id == role.CompanyRole.CompanyId);

                if (companies.FirstOrDefault(x => x.ID == company.Id) == null)
                {
                    companies.Add(ConvertToCompanyDto(company));
                }
            }

            return companies;
        }

        #endregion

        #region Session methods

        public async Task<UserDto> CreateSession(UserToLoginDto userToLogin)
        {
            User user = await _context.Users.FirstOrDefaultAsync(x => x.Email == userToLogin.Email);

            if (user == null)
            {
                return null;
            }
            else if (!VerifyPasswordHash(userToLogin.Password, user.PswHash, user.PswSalt))
            {
                return null;
            }
            return ConvertToUserDto(user);
        }

        #endregion

        #region Conversion methods

        private ProjectUserDto ConvertToProjectUserDto(ProjectUser projectUser)
        {
            var projectUserDto = new ProjectUserDto
            {
                ProjectId = projectUser.ProjectId,
                UserId = projectUser.UserId
            };
            return projectUserDto;
        }

        private UserDto ConvertToUserDto(User user)
        {
            if (user != null)
            {
                return new UserDto
                {
                    ID = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Address = ConvertToAddressDto(user.Address)
                };
            }
            return null;
        }

        private AddressDto ConvertToAddressDto(Address address)
        {
            if (address != null)
            {
                return new AddressDto
                {
                    ID = address.Id,
                    Country = address.Country,
                    City = address.City,
                    PostalCode = address.PostalCode,
                    Street = address.Street,
                    HouseNumber = address.HouseNumber,
                    BoxNumber = address.BoxNumber,
                };
            }
            return null;
        }

        private LogDto ConvertToLogDto(TimeLog log)
        {
            if (log != null)
            {
                return new LogDto
                {
                    ID = log.Id,
                    UserId = log.UserId,
                    ProjectId = log.ProjectId,
                    Description = log.Description,
                    StartTime = log.StartDate,
                    StopTime = log.EndDate
                };
            }
            return null;
        }

        private CompanyDto ConvertToCompanyDto(Company company)
        {
            if (company != null)
            {
                return new CompanyDto
                {
                    ID = company.Id,
                    Name = company.Name,
                    Address = ConvertToAddressDto(company.Address)
                };
            }
            return null;
        }

        private ProjectDto ConvertToProjectDto(Project project)
        {
            if (project != null)
            {
                return new ProjectDto
                {
                    ID = project.Id,
                    CompanyId = project.CompanyId,
                    Description = project.Description,
                    Name = project.Name
                };
            }
            return null;
        }

        private CompanyRoleDto ConvertToCompanyRoleDto(CompanyRole companyRole)
        {
            CompanyRoleDto companyRoleDto = new CompanyRoleDto
            {
                Id = companyRole.Id,
                CompanyId = companyRole.CompanyId,
                Name = companyRole.Name,
                Description = companyRole.Description,
                ManageCompany = companyRole.ManageCompany,
                ManageUsers = companyRole.ManageUsers,
                ManageProjects = companyRole.ManageProjects,
                ManageRoles = companyRole.ManageRoles,
            };
            return companyRoleDto;
        }

        public async Task<List<ProjectDto>> GetUserProjects(int userId)
        {
            var userProjects = new List<ProjectDto>();
            var result = await _context.Users.Include(x => x.UserProjects).ThenInclude(x => x.Project).FirstOrDefaultAsync(x => x.Id == userId);

            if (result != null)
            {
                foreach (var proj in result.UserProjects)
                {
                    userProjects.Add(ConvertToProjectDto(proj.Project));
                }
            }
            return userProjects;
        }

        #endregion

        #region Log methods

        public async Task<LogDto> CreateLog(LogToCreateDto logToCreate)
        {
            TimeLog log = new TimeLog()
            {
                UserId = logToCreate.UserId,
                ProjectId = logToCreate.ProjectId,
                Description = logToCreate.Description,
                StartDate = logToCreate.StartTime,
                EndDate = logToCreate.StopTime
            };

            return ConvertToLogDto(await AddAsync(log));
        }

        public async Task<LogDto> UpdateLog(int logId, LogToUpdateDto updatedLog)
        {
            TimeLog logToUpdate = await _context.TimeLogs.FirstOrDefaultAsync(x => x.Id == logId);

            logToUpdate.ProjectId = updatedLog.ProjectId;
            logToUpdate.Description = updatedLog.Description;
            logToUpdate.StartDate = updatedLog.StartTime;
            logToUpdate.EndDate = updatedLog.StopTime;

            return ConvertToLogDto(await UpdateAsync(logToUpdate));
        }

        public async Task<bool> DeleteLog(int logId)
        {
            TimeLog logToDelete = await _context.TimeLogs.FirstOrDefaultAsync(x => x.Id == logId);
            await DeleteAsync(logToDelete);
            return true;
        }

        #endregion

        //Hash methods
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
