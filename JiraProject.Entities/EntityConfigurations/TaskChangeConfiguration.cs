using JiraProject.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JiraProject.Entities.EntityConfigurations
{
    public class TaskChangeConfiguration : IEntityTypeConfiguration<TaskChange>
    {

        public void Configure(EntityTypeBuilder<TaskChange> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().UseIdentityColumn();
            builder.Property(x => x.TaskDescriptionValue).IsRequired().HasMaxLength(300);
            builder.Property(x => x.Created).IsRequired();
            builder.Property(x => x.LastUpdated).IsRequired();


            //RELATIONS
            builder.HasOne(x => x.Task).WithMany(x => x.TaskChanges).HasForeignKey(x => x.TaskId);
            builder.HasOne(x => x.User).WithMany(x => x.TaskChanges).HasForeignKey(x => x.UserId);
        }

    }
}