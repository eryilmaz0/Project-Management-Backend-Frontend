using JiraProject.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JiraProject.Entities.EntityConfigurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {

        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().UseIdentityColumn();
            builder.Property(x => x.ProjectName).IsRequired();
            builder.Property(x => x.ProjectDescription).IsRequired();
            builder.Property(x => x.DepartmentId).IsRequired();
            builder.Property(x => x.Created).IsRequired();
            builder.Property(x => x.LastUpdated).IsRequired();

            //RELATIONS
            builder.HasOne(x => x.Department).WithMany(x => x.Projects).HasForeignKey(x => x.DepartmentId);
            builder.HasMany(x => x.Members).WithMany(x => x.Projects).UsingEntity(x => x.ToTable("UserProjects"));
            builder.HasOne(x=>x.ProjectLeader).WithMany(x=>x.AssignedProjects).HasForeignKey(x=>x.ProjectLeaderId);
        }

    }
}