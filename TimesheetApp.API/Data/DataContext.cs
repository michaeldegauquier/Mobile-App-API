using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimesheetApp.API.Models;

namespace TimesheetApp.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyRole> CompanyRoles { get; set; }
        public DbSet<Project> Projects { get; set; }
        //public DbSet<ProjectRole> ProjectRoles { get; set; }
        public DbSet<TimeLog> TimeLogs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserCompanyRole> UserCompanyRoles { get; set; }
        //public DbSet<UserProjectRole> UserProjectRoles { get; set; }
        public DbSet<Invite> Invites { get; set; }
        public DbSet<ProjectUser> ProjectUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //User - CompanyRole
            modelBuilder.Entity<UserCompanyRole>()
                .HasKey(t => new { t.UserId, t.CompanyRoleId });

            modelBuilder.Entity<UserCompanyRole>()
                .HasOne(pt => pt.User)
                .WithMany(p => p.UserCompanyRoles)
                .HasForeignKey(pt => pt.UserId);

            modelBuilder.Entity<UserCompanyRole>()
                .HasOne(pt => pt.CompanyRole)
                .WithMany(t => t.UserCompanyRoles)
                .HasForeignKey(pt => pt.CompanyRoleId);
            //User - ProjectRole
            //modelBuilder.Entity<UserProjectRole>()
            //    .HasKey(t => new { t.UserId, t.ProjectRoleId });

            //modelBuilder.Entity<UserProjectRole>()
            //    .HasOne(pt => pt.User)
            //    .WithMany(p => p.UserProjectRoles)
            //    .HasForeignKey(pt => pt.UserId);

            //modelBuilder.Entity<UserProjectRole>()
            //    .HasOne(pt => pt.ProjectRole)
            //    .WithMany(t => t.UserProjectRoles)
            //    .HasForeignKey(pt => pt.ProjectRoleId);

            //User - project
            modelBuilder.Entity<ProjectUser>()
                .HasKey(t => new { t.UserId, t.ProjectId });

            modelBuilder.Entity<ProjectUser>()
                .HasOne(x => x.User)
                .WithMany(x => x.UserProjects)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<ProjectUser>()
                .HasOne(x => x.Project)
                .WithMany(x => x.ProjectUsers)
                .HasForeignKey(x => x.ProjectId);
        }
    }
}
