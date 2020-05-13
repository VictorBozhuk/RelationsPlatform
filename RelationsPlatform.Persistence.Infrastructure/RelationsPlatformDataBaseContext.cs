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

        public virtual DbSet<Person> People { get; set; }
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

    }
}
