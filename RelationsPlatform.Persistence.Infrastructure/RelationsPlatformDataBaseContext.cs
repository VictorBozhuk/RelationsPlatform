using System;
using RelationsPlatform.Persistence.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using RelationsPlatform.Persistence.Infrastructure.Models;

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
            Role userRole = new Role { Id = new Guid("442d3c5c-8737-4934-bac9-f20f35e99a88"), Name = userRoleName };
            User adminUser = new User { Id = new Guid("1f444245-51e5-452a-bdab-6010e82b8937"), Login = adminEmail, Password = adminPassword, RoleId = adminRole.Id };

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            modelBuilder.Entity<User>().HasData(new User[] { adminUser });

            modelBuilder.Entity<Relation>(entity =>
            {
                entity.HasOne(d => d.RelationUser)
                    .WithMany(p => p.MainRelations)
                    .HasForeignKey(d => d.RelationUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DisciplineChoise_Student");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Relations)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DisciplineChoise_DisciplineAvailability");
            });

            User user1 = new User()
            {
                Birthday = Convert.ToDateTime("25.11.1999"),
                Description = "Мій опис не потребує опису",
                DigitalName = "VictarV",
                Gender = "Male",
                Login = "a",
                Name = "Віктор Божук",
                Password = "123",
                RoleId = userRole.Id,
            };

            User user2 = new User()
            {
                Birthday = Convert.ToDateTime("25.11.2000"),
                Description = "Мій опис не потребує опису",
                DigitalName = "MaksV",
                Gender = "Male",
                Login = "b",
                Name = "Максим Максюк",
                Password = "123",
                RoleId = userRole.Id,
            };

            User user3 = new User()
            {
                Birthday = Convert.ToDateTime("15.1.2001"),
                Description = "Мій опис не потребує опису",
                DigitalName = "MaksV",
                Gender = "Male",
                Login = "b",
                Name = "Павло Стрілецький",
                Password = "123",
                RoleId = userRole.Id,
            };

            Contact contact1 = new Contact()
            {
                Discord = "Discord.com",
                Instagram = "Instagram.com",
                Telegram = "Telegram.com",
                Facebook = "Facebook.com",
                Email = "bozhook@gmail.com",
                UserId = user1.Id,
            };

            Contact contact2 = new Contact()
            {
                Discord = "Discord.com",
                Instagram = "Instagram.com",
                Telegram = "Telegram.com",
                Facebook = "Facebook.com",
                Email = "bozhook@gmail.com",
                UserId = user2.Id,
            };

            Contact contact3 = new Contact()
            {
                Discord = "Discord.com",
                Instagram = "Instagram.com",
                Telegram = "Telegram.com",
                Facebook = "Facebook.com",
                Email = "bozhook@gmail.com",
                UserId = user3.Id,
            };

            Address address1 = new Address()
            {
                Country = "Україна",
                Region = "Львівська",
                City = "Львів",
                District = "Личаківський",
                Street = "Медової печери",
                NumberOfHouse = "39",
                ContactId = contact1.Id,
            };

            Address address2 = new Address()
            {
                Country = "Україна",
                Region = "Львівська",
                City = "Львів",
                District = "Личаківський",
                Street = "Медової печери",
                NumberOfHouse = "39",
                ContactId = contact2.Id,
            };


            Address address3 = new Address()
            {
                Country = "Україна",
                Region = "Львівська",
                City = "Львів",
                District = "Личаківський",
                Street = "Медової печери",
                NumberOfHouse = "39",
                ContactId = contact3.Id,
            };

            Education education1 = new Education()
            {
                UserId = user1.Id,
            };

            Education education2 = new Education()
            {
                UserId = user2.Id,
            };

            Education education3 = new Education()
            {
                UserId = user3.Id,
            };

            School school1 = new School()
            {
                Name = "Школа I-III ступенів с. Ростань",
                EducationId = education1.Id,
            };

            HigherEducation he = new HigherEducation()
            {
                University = "Львівський національний університет імені Івана Франка",
                Faculty = "Прикладної математики та інформатики",
                Specialty = "Комп'ютерні науки(Інформатика)",
                Status = "3 курс",
                EducationId = education1.Id,
            };

            Skill skill1 = new Skill()
            {
                UserId = user1.Id,
                MainProfession = ".NET C# Programmer",
            };

            Skill skill2 = new Skill()
            {
                UserId = user2.Id,
                MainProfession = ".NET C# Programmer",
            };

            Skill skill3 = new Skill()
            {
                UserId = user3.Id,
                MainProfession = ".NET Web Developer",
            };

            ProfessionalSkill ps1 = new ProfessionalSkill()
            {
                Name = "C#",
                Description = Level.Levels[3].Value,
                Level = 4,
                SkillId = skill1.Id,
            };

            ProfessionalSkill ps1s = new ProfessionalSkill()
            {
                Name = "C#",
                Description = Level.Levels[4].Value,
                Level = 5,
                SkillId = skill3.Id,
            };

            ProfessionalSkill ps2 = new ProfessionalSkill()
            {
                Name = "WinForms",
                Description = Level.Levels[1].Value,
                Level = 2,
                SkillId = skill1.Id,
            };

            ProfessionalSkill ps3 = new ProfessionalSkill()
            {
                Name = "WPF",
                Description = Level.Levels[2].Value,
                Level = 3,
                SkillId = skill1.Id,
            };

            ProfessionalSkill ps4 = new ProfessionalSkill()
            {
                Name = "ASP.NET Core",
                Description = Level.Levels[2].Value,
                Level = 3,
                SkillId = skill1.Id,
            };

            ProfessionalSkill ps4s = new ProfessionalSkill()
            {
                Name = "ASP.NET Core",
                Description = Level.Levels[3].Value,
                Level = 4,
                SkillId = skill3.Id,
            };

            ProfessionalSkill ps5 = new ProfessionalSkill()
            {
                Name = "ASP.NET MVC",
                Description = Level.Levels[1].Value,
                Level = 2,
                SkillId = skill1.Id,
            };

            ProfessionalSkill ps6 = new ProfessionalSkill()
            {
                Name = "HTML, CSS",
                Description = Level.Levels[2].Value,
                Level = 3,
                SkillId = skill1.Id,
            };

            ProfessionalSkill ps6s = new ProfessionalSkill()
            {
                Name = "HTML, CSS",
                Description = Level.Levels[3].Value,
                Level = 4,
                SkillId = skill3.Id,
            };

            ProfessionalSkill ps7 = new ProfessionalSkill()
            {
                Name = "JavaScript",
                Description = Level.Levels[1].Value,
                Level = 2,
                SkillId = skill1.Id,
            };

            ProfessionalSkill ps7s = new ProfessionalSkill()
            {
                Name = "JavaScript",
                Description = Level.Levels[2].Value,
                Level = 3,
                SkillId = skill3.Id,
            };

            ProfessionalSkill ps8 = new ProfessionalSkill()
            {
                Name = "Entity Framework",
                Description = Level.Levels[2].Value,
                Level = 3,
                SkillId = skill1.Id,
            };

            ProfessionalSkill ps9 = new ProfessionalSkill()
            {
                Name = "Entity Framework Core",
                Description = Level.Levels[2].Value,
                Level = 3,
                SkillId = skill1.Id,
            };

            ProfessionalSkill ps10 = new ProfessionalSkill()
            {
                Name = "MS SQL Sever",
                Description = Level.Levels[2].Value,
                Level = 3,
                SkillId = skill1.Id,
            };

            ProfessionalSkill ps11 = new ProfessionalSkill()
            {
                Name = "Postgress SQL",
                Description = Level.Levels[2].Value,
                Level = 3,
                SkillId = skill1.Id,
            };

            Ability ab1 = new Ability()
            {
                Name = "Фото, відео і аудіо редактори",
                Description = Level.Levels[1].Value,
                Level = 2,
                SkillId = skill1.Id,
            };

            var relation = new Relation()
            {
                RelationUserId = user2.Id,
                UserId = user1.Id,
                Status = "Friend",
            };

            modelBuilder.Entity<User>().HasData(new User[] { user1, user2, user3 });
            modelBuilder.Entity<Contact>().HasData(new Contact[] { contact1, contact2, contact3 });
            modelBuilder.Entity<Address>().HasData(new Address[] { address1, address2, address3 });
            modelBuilder.Entity<Education>().HasData(new Education[] { education1, education2 });
            modelBuilder.Entity<HigherEducation>().HasData(new HigherEducation[] { he, });
            modelBuilder.Entity<School>().HasData(new School[] { school1, });
            modelBuilder.Entity<Skill>().HasData(new Skill[] { skill1, skill2, skill3 });
            modelBuilder.Entity<ProfessionalSkill>().HasData(new ProfessionalSkill[] { ps1, ps2, ps3, ps4, ps5, ps6, ps7, ps8, ps9, ps10, ps11, ps1s, ps4s, ps6s, ps7s, });
            modelBuilder.Entity<Ability>().HasData(new Ability[] { ab1, });
            modelBuilder.Entity<Relation>().HasData(new Relation[] { relation });

        }

    }
}
