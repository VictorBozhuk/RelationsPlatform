using System;
using RelationsPlatform.Persistence.Model;
using Microsoft.EntityFrameworkCore;

namespace RelationsPlatform.Persistence.Infrastructure
{
    public class RelationsPlatformDataBaseContext : DbContext
    {
        public RelationsPlatformDataBaseContext() { }

        public RelationsPlatformDataBaseContext(DbContextOptions<RelationsPlatformDataBaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Relation> Relations  { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<ProfessionalSkill> ProfessionSkills { get; set; }
        public virtual DbSet<Ability> Abilities { get; set; }
        public virtual DbSet<Education> Educations { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<School> Schools { get; set; }
        public virtual DbSet<HigherEducation> HigherEducation { get; set; }
        public virtual DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminRoleName = "admin";
            string userRoleName = "user";

            string adminEmail = "admin.com";
            string adminPassword = "123";

            Role adminRole = new Role { Id = new Guid("2432fe24-3a95-4bfe-85bd-4191cd41b230"), Name = adminRoleName };
            Role studentRole = new Role { Id = new Guid("442d3c5c-8737-4934-bac9-f20f35e99a88"), Name = userRoleName };
            User adminUser = new User { Id = new Guid("1f444245-51e5-452a-bdab-6010e82b8937"), Login = adminEmail, Password = adminPassword, RoleId = adminRole.Id };

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, studentRole });
            modelBuilder.Entity<User>().HasData(new User[] { adminUser });

            modelBuilder.Entity<User>()
                .HasMany(p => p.Relations)
                .WithOne(p => p.User)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }

    }
}
