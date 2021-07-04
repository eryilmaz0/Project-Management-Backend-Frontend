using JiraProject.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JiraProject.Entities.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().UseIdentityColumn();
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.PasswordHash).IsRequired();
            builder.Property(x => x.PasswordSalt).IsRequired();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(75);
            builder.Property(x => x.Gender).IsRequired();
            builder.Property(x => x.Created).IsRequired();
            builder.Property(x => x.LastUpdated).IsRequired();


            //RELATIONS
            builder.HasMany(x => x.Roles).WithMany(x => x.Users).UsingEntity(x => x.ToTable("UserRoles"));
            builder.HasMany(x => x.Projects).WithMany(x => x.Members).UsingEntity(x => x.ToTable("UserProjects"));
        }

    }
}