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

            modelBuilder.Entity<User>()
                .HasMany(p => p.Relations)
                .WithOne(p => p.User)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            User user1 = new User()
            {
                Id = new Guid("37bc7c3c-74aa-4e74-9273-3fd54a3984a7"),
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
                Id = new Guid("2c3126ba-9a18-437d-bd8c-0f8f3870726b"),
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
                Id = new Guid("2c3126ba-9a18-437d-bd8c-0f8f3870726b"),
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
                Id = new Guid("2c3126ba-9a18-437d-bd8c-0f8f3870726b"),
                Discord = "Discord.com",
                Instagram = "Instagram.com",
                Telegram = "Telegram.com",
                Facebook = "Facebook.com",
                Email = "bozhook@gmail.com",
                UserId = user1.Id,
            };

            Contact contact2 = new Contact()
            {
                Id = new Guid("a3ced0f8-7d7a-4442-871e-5bcd401e74a3"),
                Discord = "Discord.com",
                Instagram = "Instagram.com",
                Telegram = "Telegram.com",
                Facebook = "Facebook.com",
                Email = "bozhook@gmail.com",
                UserId = user2.Id,
            };

            Contact contact3 = new Contact()
            {
                Id = new Guid("a3ced0f8-7d7a-4442-871e-5bcd401e74a3"),
                Discord = "Discord.com",
                Instagram = "Instagram.com",
                Telegram = "Telegram.com",
                Facebook = "Facebook.com",
                Email = "bozhook@gmail.com",
                UserId = user3.Id,
            };


            Address address1 = new Address()
            {
                Id = new Guid("d7b727f8-373b-4145-8cd9-f0ec0d09d098"),
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
                Id = new Guid("3fb6a621-256f-4c92-8f21-8e725590f749"),
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
                Id = new Guid("3fb6a621-256f-4c92-8f21-8e725590f749"),
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
                Id = new Guid("7bb37e34-041e-4098-b1b5-65023f6c2f1f"),
                UserId = user1.Id,
            };

            Education education2 = new Education()
            {
                Id = new Guid("9d3e378b-889e-4871-b6ad-c34ac598fe06"),
                UserId = user2.Id,
            };

            Education education3 = new Education()
            {
                Id = new Guid("9d3e378b-889e-4871-b6ad-c34ac598fe06"),
                UserId = user3.Id,
            };


            School school1 = new School()
            {
                Id = new Guid("349c5197-4d38-439e-b536-4b0c1b3fdcce"),
                Name = "Школа I-III ступенів с. Ростань",
                EducationId = education1.Id,
            };

            HigherEducation he = new HigherEducation()
            {
                Id = new Guid("43fdafa1-88a8-4538-9327-145109a02586"),
                University = "Львівський національний університет імені Івана Франка",
                Faculty = "Прикладної математики та інформатики",
                Specialty = "Комп'ютерні науки(Інформатика)",
                Status = "3 курс",
                EducationId = education1.Id,
            };

            Skill skill1 = new Skill()
            {
                Id = new Guid("1844b523-d4a2-422e-8ac1-cfc6320bc1d7"),
                UserId = user1.Id,
                MainProfession = ".NET C# Programmer",
            };

            Skill skill2 = new Skill()
            {
                Id = new Guid("58e681f8-36f3-4a6c-805d-02101fdd2fac"),
                UserId = user2.Id,
                MainProfession = ".NET C# Programmer",
            };

            Skill skill3 = new Skill()
            {
                Id = new Guid("58e681f8-36f3-4a6c-805d-02101fdd2fac"),
                UserId = user3.Id,
                MainProfession = ".NET Web Developer",
            };

            ProfessionalSkill ps1 = new ProfessionalSkill()
            {
                Id = new Guid("3cf9a078-fdff-4a49-b3f8-98e5fb704389"),
                Name = "C#",
                Description = Level.Levels[3].Value,
                Level = 4,
                SkillId = skill1.Id,
            };

            ProfessionalSkill ps1s = new ProfessionalSkill()
            {
                Id = new Guid("58e681f8-36f3-4a6c-805d-02101fdd2fac"),
                Name = "C#",
                Description = Level.Levels[4].Value,
                Level = 5,
                SkillId = skill3.Id,
            };

            ProfessionalSkill ps2 = new ProfessionalSkill()
            {
                Id = new Guid("f8cdc96a-b2a7-4486-bd22-681f64c47b23"),
                Name = "WinForms",
                Description = Level.Levels[1].Value,
                Level = 2,
                SkillId = skill1.Id,
            };

            ProfessionalSkill ps3 = new ProfessionalSkill()
            {
                Id = new Guid("bcb4de5c-b017-4359-bc1d-e7cdd01c9004"),
                Name = "WPF",
                Description = Level.Levels[2].Value,
                Level = 3,
                SkillId = skill1.Id,
            };

            ProfessionalSkill ps4 = new ProfessionalSkill()
            {
                Id = new Guid("ccee00a9-7fa0-4454-bc92-a6712fa2dd31"),
                Name = "ASP.NET Core",
                Description = Level.Levels[2].Value,
                Level = 3,
                SkillId = skill1.Id,
            };

            ProfessionalSkill ps4s = new ProfessionalSkill()
            {
                Id = new Guid("ccee00a9-7fa0-4454-bc92-a6712fa2dd31"),
                Name = "ASP.NET Core",
                Description = Level.Levels[3].Value,
                Level = 4,
                SkillId = skill1.Id,
            };

            ProfessionalSkill ps5 = new ProfessionalSkill()
            {
                Id = new Guid("6953c2fd-abed-403f-bd83-3adee9f18a1b"),
                Name = "ASP.NET MVC",
                Description = Level.Levels[1].Value,
                Level = 2,
                SkillId = skill1.Id,
            };

            ProfessionalSkill ps6 = new ProfessionalSkill()
            {
                Id = new Guid("c1acf64a-82b5-4dad-b792-699553a93f90"),
                Name = "HTML, CSS",
                Description = Level.Levels[2].Value,
                Level = 3,
                SkillId = skill1.Id,
            };

            ProfessionalSkill ps6s = new ProfessionalSkill()
            {
                Id = new Guid("c1acf64a-82b5-4dad-b792-699553a93f90"),
                Name = "HTML, CSS",
                Description = Level.Levels[3].Value,
                Level = 4,
                SkillId = skill3.Id,
            };

            ProfessionalSkill ps7 = new ProfessionalSkill()
            {
                Id = new Guid("a4a1f872-ff15-4022-b6cb-fd7406129338"),
                Name = "JavaScript",
                Description = Level.Levels[1].Value,
                Level = 2,
                SkillId = skill1.Id,
            };

            ProfessionalSkill ps7s = new ProfessionalSkill()
            {
                Id = new Guid("a4a1f872-ff15-4022-b6cb-fd7406129338"),
                Name = "JavaScript",
                Description = Level.Levels[2].Value,
                Level = 3,
                SkillId = skill3.Id,
            };

            ProfessionalSkill ps8 = new ProfessionalSkill()
            {
                Id = new Guid("11cc0691-ad51-46ae-91d8-42ab61bd39ff"),
                Name = "Entity Framework",
                Description = Level.Levels[2].Value,
                Level = 3,
                SkillId = skill1.Id,
            };

            ProfessionalSkill ps9 = new ProfessionalSkill()
            {
                Id = new Guid("dbde8f57-eb7c-4037-b4d7-df63d1693dcf"),
                Name = "Entity Framework Core",
                Description = Level.Levels[2].Value,
                Level = 3,
                SkillId = skill1.Id,
            };

            ProfessionalSkill ps10 = new ProfessionalSkill()
            {
                Id = new Guid("a59deb47-bdd7-40c2-856d-6c5f82d190da"),
                Name = "MS SQL Sever",
                Description = Level.Levels[2].Value,
                Level = 3,
                SkillId = skill1.Id,
            };

            ProfessionalSkill ps11 = new ProfessionalSkill()
            {
                Id = new Guid("f305918a-ccab-422c-8e4b-f3e252749fcd"),
                Name = "Postgress SQL",
                Description = Level.Levels[2].Value,
                Level = 3,
                SkillId = skill1.Id,
            };

            Ability ab1 = new Ability()
            {
                Id = new Guid("a86b40f6-f89e-4756-81a1-8c0e72425bb7"),
                Name = "Фото, відео і аудіо редактори",
                Description = Level.Levels[1].Value,
                Level = 2,
                SkillId = skill1.Id,
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

        }

    }
}
