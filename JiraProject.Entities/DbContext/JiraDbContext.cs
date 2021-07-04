using JiraProject.Entities.Entities;
using JiraProject.Entities.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
namespace JiraProject.Entities.DbContext
{
    public class JiraDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public JiraDbContext()
        {
            
        }

        //CON STRINGI STARTUPDAN AL
        public JiraDbContext(DbContextOptions<JiraDbContext> options):base(options)
        {
            
        }



        public DbSet<User> Users { get; set; }
        public DbSet<TaskChange> TaskChanges { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Department> Departments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new TaskChangeConfiguration());
            modelBuilder.ApplyConfiguration(new TaskConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectConfiguration());
            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}