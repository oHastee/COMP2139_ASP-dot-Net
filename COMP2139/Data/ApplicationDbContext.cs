using Microsoft.EntityFrameworkCore;
using COMP2139_Labs.Models;
using COMP2139_Labs.Data;

using System;
namespace COMP2139_Labs.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }


        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }


    }
}

