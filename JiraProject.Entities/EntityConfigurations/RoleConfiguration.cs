using JiraProject.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JiraProject.Entities.EntityConfigurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {

        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().UseIdentityColumn();
            builder.Property(x => x.RoleName).IsRequired();
            builder.Property(x => x.RoleDescription).HasMaxLength(100);
            builder.Property(x => x.Created).IsRequired();
            builder.Property(x => x.LastUpdated).IsRequired();

            
            //RELATIONS
            builder.HasMany(x => x.Users).WithMany(x => x.Roles).UsingEntity(x => x.ToTable("UserRoles"));
        }

    }
}