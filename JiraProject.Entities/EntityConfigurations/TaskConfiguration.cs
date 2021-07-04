using JiraProject.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JiraProject.Entities.EntityConfigurations
{
    public class TaskConfiguration : IEntityTypeConfiguration<Task>
    {

        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().UseIdentityColumn();
            builder.Property(x => x.TaskDescription).IsRequired().HasMaxLength(300);
            builder.Property(x => x.ProjectId).IsRequired();
            builder.Property(x => x.Priority).IsRequired();
            builder.Property(x => x.Type).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.AssignedUserId).IsRequired();
            builder.Property(x => x.Created).IsRequired();
            builder.Property(x => x.LastUpdated).IsRequired();

            //RELATIONS
            builder.HasOne(x => x.Project).WithMany(x => x.Tasks).HasForeignKey(x => x.ProjectId);
            builder.HasOne(x => x.User).WithMany(x => x.Tasks).HasForeignKey(x => x.AssignedUserId);
        }

    }
}