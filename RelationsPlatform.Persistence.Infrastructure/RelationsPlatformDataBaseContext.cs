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
        public virtual DbSet<Feedback> Feedbacks  { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<ProfessionalSkill> ProfessionSkills { get; set; }
        public virtual DbSet<Ability> Abilities { get; set; }
        public virtual DbSet<Education> Educations { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<School> Schools { get; set; }
        public virtual DbSet<HigherEducation> HigherEducation { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserTask> UserTasks { get; set; }

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
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Relations");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Relations)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Realtions2");
            });

            User user1 = new User()
            {
                Id = new Guid("08ad1c46-d57c-4e38-b54a-733359e36e43"),
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
                Id = new Guid("6b8355ab-84c6-4ea4-b05e-52befb062e48"),
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
                Id = new Guid("8c93be5e-359c-4610-89ba-289d79cbb388"),
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
                Id = new Guid("6b8355ab-84c6-4ea4-b05e-52befb062e48"),
                Discord = "Discord.com",
                Instagram = "Instagram.com",
                Telegram = "Telegram.com",
                Facebook = "Facebook.com",
                Email = "bozhook@gmail.com",
                UserId = user1.Id,
            };

            Contact contact2 = new Contact()
            {
                Id = new Guid("813ac5df-595a-4a86-ae49-7df794753785"),
                Discord = "Discord.com",
                Instagram = "Instagram.com",
                Telegram = "Telegram.com",
                Facebook = "Facebook.com",
                Email = "bozhook@gmail.com",
                UserId = user2.Id,
            };

            Contact contact3 = new Contact()
            {
                Id = new Guid("6b19e7a5-ac66-4c4e-8b63-64028e333ae3"),
                Discord = "Discord.com",
                Instagram = "Instagram.com",
                Telegram = "Telegram.com",
                Facebook = "Facebook.com",
                Email = "bozhook@gmail.com",
                UserId = user3.Id,
            };

            Address address1 = new Address()
            {
                Id = new Guid("8c93be5e-359c-4610-89ba-289d79cbb388"),
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
                Id = new Guid("50a2f41c-33cc-413c-be39-1d67cbf14642"),
                Country = "Україна",
                Region = "Львівська",
                City = "Львів",
                District = "Личаківський",
                Street = "Медової печери",
                NumberOfHouse = "39",
                ContactId = contact2.Id,
            };


            //Address address3 = new Address()
            //{
            //    Id = new Guid("12eb7aca-50eb-4cfd-95ff-62524585b721"),
            //    Country = "Україна",
            //    Region = "Львівська",
            //    City = "Львів",
            //    District = "Личаківський",
            //    Street = "Медової печери",
            //    NumberOfHouse = "39",
            //    ContactId = contact3.Id,
            //};

            Education education1 = new Education()
            {
                Id = new Guid("59ee1d62-ef16-4efa-a251-ef771fda8fbc"),
                UserId = user1.Id,
            };

            Education education2 = new Education()
            {
                Id = new Guid("ffb3043c-9e35-4545-be99-09783438eb61"),
                UserId = user2.Id,
            };

            Education education3 = new Education()
            {
                UserId = user3.Id,
            };

            School school1 = new School()
            {
                Id = new Guid("59ee1d62-ef16-4efa-a251-ef771fda8fbc"),
                Name = "Школа I-III ступенів с. Ростань",
                EducationId = education1.Id,
            };

            HigherEducation he = new HigherEducation()
            {
                Id = new Guid("59ee1d62-ef16-4efa-a251-ef771fda8fbc"),
                University = "Львівський національний університет імені Івана Франка",
                Faculty = "Прикладної математики та інформатики",
                Specialty = "Комп'ютерні науки(Інформатика)",
                Status = "3 курс",
                EducationId = education1.Id,
            };

            Skill skill1 = new Skill()
            {
                Id = new Guid("59ee1d62-ef16-4efa-a251-ef771fda8fbc"),
                UserId = user1.Id,
                MainProfession = ".NET C# Programmer",
            };

            Skill skill2 = new Skill()
            {
                Id = new Guid("47c33e26-eea8-47f7-9625-708d98dae3b2"),
                UserId = user2.Id,
                MainProfession = ".NET C# Programmer",
            };

            Skill skill3 = new Skill()
            {
                Id = new Guid("8352484e-63b7-41fd-a00a-a17094cf0ce9"),
                UserId = user3.Id,
                MainProfession = ".NET Web Developer",
            };

            ProfessionalSkill ps1 = new ProfessionalSkill()
            {
                Id = new Guid("ffb3043c-9e35-4545-be99-09783438eb61"),
                Name = "C#",
                Description = Level.Levels[3].Value,
                Level = 4,
                SkillId = skill1.Id,
            };

            ProfessionalSkill ps1s = new ProfessionalSkill()
            {
                Id = new Guid("47c33e26-eea8-47f7-9625-708d98dae3b2"),
                Name = "C#",
                Description = Level.Levels[4].Value,
                Level = 5,
                SkillId = skill3.Id,
            };

            ProfessionalSkill ps2 = new ProfessionalSkill()
            {
                Id = new Guid("a79c56da-12df-48f1-920a-06a36f79fdd2"),
                Name = "WinForms",
                Description = Level.Levels[1].Value,
                Level = 2,
                SkillId = skill1.Id,
            };

            ProfessionalSkill ps3 = new ProfessionalSkill()
            {
                Id = new Guid("d5bc1cbb-4a39-4609-8250-8213cdaf44b4"),
                Name = "WPF",
                Description = Level.Levels[2].Value,
                Level = 3,
                SkillId = skill1.Id,
            };

            ProfessionalSkill ps4 = new ProfessionalSkill()
            {
                Id = new Guid("8dcf061e-1b0e-43d4-af30-22775a114d12"),
                Name = "ASP.NET Core",
                Description = Level.Levels[2].Value,
                Level = 3,
                SkillId = skill1.Id,
            };

            ProfessionalSkill ps4s = new ProfessionalSkill()
            {
                Id = new Guid("c296f2bf-fc87-4e18-b926-399c62c13111"),
                Name = "ASP.NET Core",
                Description = Level.Levels[3].Value,
                Level = 4,
                SkillId = skill3.Id,
            };

            ProfessionalSkill ps5 = new ProfessionalSkill()
            {
                Id = new Guid("3b9b8c8b-1977-49d2-b01a-33a6247e723d"),
                Name = "ASP.NET MVC",
                Description = Level.Levels[1].Value,
                Level = 2,
                SkillId = skill1.Id,
            };

            ProfessionalSkill ps6 = new ProfessionalSkill()
            {
                Id = new Guid("46d71fc3-14e0-40d6-8740-6df49e7186df"),
                Name = "HTML, CSS",
                Description = Level.Levels[2].Value,
                Level = 3,
                SkillId = skill1.Id,
            };

            ProfessionalSkill ps6s = new ProfessionalSkill()
            {
                Id = new Guid("b8359c3f-87e9-4fab-a80b-55cf6545f2d9"),
                Name = "HTML, CSS",
                Description = Level.Levels[3].Value,
                Level = 4,
                SkillId = skill3.Id,
            };

            ProfessionalSkill ps7 = new ProfessionalSkill()
            {
                Id = new Guid("ccba1b82-b8d0-4114-b47f-7400abf1a326"),
                Name = "JavaScript",
                Description = Level.Levels[1].Value,
                Level = 2,
                SkillId = skill1.Id,
            };

            ProfessionalSkill ps7s = new ProfessionalSkill()
            {
                Id = new Guid("bf970fbb-d015-47a1-9f43-410d53d9736b"),
                Name = "JavaScript",
                Description = Level.Levels[2].Value,
                Level = 3,
                SkillId = skill3.Id,
            };

            ProfessionalSkill ps8 = new ProfessionalSkill()
            {
                Id = new Guid("9225e34a-4c2c-4d05-af43-fb0cde1e38b0"),
                Name = "Entity Framework",
                Description = Level.Levels[2].Value,
                Level = 3,
                SkillId = skill1.Id,
            };

            ProfessionalSkill ps9 = new ProfessionalSkill()
            {
                Id = new Guid("f5f956d9-63d0-43d0-b920-4a5a0f49a8da"),
                Name = "Entity Framework Core",
                Description = Level.Levels[2].Value,
                Level = 3,
                SkillId = skill1.Id,
            };

            ProfessionalSkill ps10 = new ProfessionalSkill()
            {
                Id = new Guid("a9f7d381-a647-4332-97cf-f4f862a96a18"),
                Name = "MS SQL Sever",
                Description = Level.Levels[2].Value,
                Level = 3,
                SkillId = skill1.Id,
            };

            ProfessionalSkill ps11 = new ProfessionalSkill()
            {
                Id = new Guid("cc5ab721-b3bf-456f-84d8-1d242c64bb2f"),
                Name = "Postgress SQL",
                Description = Level.Levels[2].Value,
                Level = 3,
                SkillId = skill1.Id,
            };

            Ability ab1 = new Ability()
            {
                Id = new Guid("ffb3043c-9e35-4545-be99-09783438eb61"),
                Name = "Фото, відео і аудіо редактори",
                Description = Level.Levels[1].Value,
                Level = 2,
                SkillId = skill1.Id,
            };

            var relation = new Relation()
            {
                Id = new Guid("ffb3043c-9e35-4545-be99-09783438eb61"),
                RelationUserId = user2.Id,
                UserId = user1.Id,
                Status = "Friend",
            };

            var task1 = new UserTask()
            {
                Id = new Guid("ffb3043c-9e35-4545-be99-09783438eb61"),
                LongDescription = "wgefugefgoqw efqwgeof gf wef qg\nwe fwefwecwqecfqwefqwefc\ncwerwqeferfcawefawfergserhsrthdrth",
                ShortDescription = "Написати консольну програму на мові C#.",
                UserId = user1.Id,
                Subject = "Програмування",
            };




            modelBuilder.Entity<User>().HasData(new User[] { user1, user2, user3 });
            modelBuilder.Entity<Contact>().HasData(new Contact[] { contact1, contact2, contact3 });
            modelBuilder.Entity<Address>().HasData(new Address[] { address1, address2, });
            modelBuilder.Entity<Education>().HasData(new Education[] { education1, education2 });
            modelBuilder.Entity<HigherEducation>().HasData(new HigherEducation[] { he, });
            modelBuilder.Entity<School>().HasData(new School[] { school1, });
            modelBuilder.Entity<Skill>().HasData(new Skill[] { skill1, skill2, skill3 });
            modelBuilder.Entity<ProfessionalSkill>().HasData(new ProfessionalSkill[] { ps1, ps2, ps3, ps4, ps5, ps6, ps7, ps8, ps9, ps10, ps11, ps1s, ps4s, ps6s, ps7s, });
            modelBuilder.Entity<Ability>().HasData(new Ability[] { ab1, });
            modelBuilder.Entity<Relation>().HasData(new Relation[] { relation });
            modelBuilder.Entity<UserTask>().HasData(new UserTask[] { task1, });

        }

    }
}
