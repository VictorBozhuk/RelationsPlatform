﻿using Microsoft.EntityFrameworkCore;
using RelationsPlatform.Persistence.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationsPlatform.Persistence.Infrastructure.Repository
{
    public class UserStorage : IUserStorage
    {
        private RelationsPlatformDataBaseContext _context;

        public UserStorage(RelationsPlatformDataBaseContext context)
        {
            _context = context;
        }

        public async Task CreateUser(UserArgs args)
        {
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            var role = await _context.Roles.FirstOrDefaultAsync(x => x.Name == "user");
            if (role != null)
            {
                var student = new User
                {
                    Login = args.Login,
                    Password = args.Password,
                    Name = args.Name,
                    RoleId = role.Id
                };

                await _context.Users.AddAsync(student);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<User> GetUser(string login, string password)
        {
            return await _context.Users.Include(x => x.Role).FirstOrDefaultAsync(u => u.Login == login && u.Password == password);
        }

        public async Task<User> GetUser(string login)
        {
            return await _context.Users.Include(x => x.Role).Include(x => x.Contact).ThenInclude(x => x.Address)
                .Include(x => x.Skill).ThenInclude(x => x.Abilities).Include(x => x.Skill).ThenInclude(x => x.Jobs)
                .Include(x => x.Skill).ThenInclude(x => x.ProfesionSkills).Include(x => x.Education).ThenInclude(x => x.Courses)
                .Include(x => x.Education).ThenInclude(x => x.HigherEducations).Include(x => x.Education).ThenInclude(x => x.Schools)
                .Include(x => x.Relations).ThenInclude(x => x.RelationUser).Include(x => x.MainRelations).ThenInclude(x => x.User)
                .Include(x => x.Feedbacks).Include(x => x.Tasks).FirstOrDefaultAsync(u => u.Login == login);
        }

        public async Task<User> GetUserById(string id)
        {
            return await _context.Users.Include(x => x.Role).Include(x => x.Contact).ThenInclude(x => x.Address)
                .Include(x => x.Skill).ThenInclude(x => x.Abilities).Include(x => x.Skill).ThenInclude(x => x.Jobs)
                .Include(x => x.Skill).ThenInclude(x => x.ProfesionSkills).Include(x => x.Education).ThenInclude(x => x.Courses)
                .Include(x => x.Education).ThenInclude(x => x.HigherEducations).Include(x => x.Education).ThenInclude(x => x.Schools)
                .Include(x => x.Relations).ThenInclude(x => x.RelationUser).Include(x => x.MainRelations).ThenInclude(x => x.User)
                .Include(x => x.Feedbacks).Include(x => x.Tasks).FirstOrDefaultAsync(u => u.Id.ToString() == id);
        }

        public async Task<List<User>> GetUsers()
        {
            return await _context.Users.Include(x => x.Role).Include(x => x.Contact).ThenInclude(x => x.Address)
                .Include(x => x.Skill).ThenInclude(x => x.Abilities).Include(x => x.Skill).ThenInclude(x => x.Jobs)
                .Include(x => x.Skill).ThenInclude(x => x.ProfesionSkills).Include(x => x.Education).ThenInclude(x => x.Courses)
                .Include(x => x.Education).ThenInclude(x => x.HigherEducations).Include(x => x.Education).ThenInclude(x => x.Schools)
                .Include(x => x.Relations).ThenInclude(x => x.RelationUser).Include(x => x.MainRelations).ThenInclude(x => x.User)
                .Include(x => x.Feedbacks).Include(x => x.Tasks).Where(x => x.Role.Name == "user").ToListAsync();
        }

        public async Task<Contact> GetContact(string userId)
        {
            return await _context.Contacts.Include(x => x.Address).FirstOrDefaultAsync(x => x.UserId.ToString() == userId);
        }

        public async Task<Address> GetAddress(string contactId)
        {
            return await _context.Addresses.FirstOrDefaultAsync(x => x.ContactId.ToString() == contactId);
        }

        public async Task AddContact(Guid userId, string instagram, string telegram, string facebook, string email, string discord)
        {
            var contact = new Contact()
            {
                UserId = userId,
                Instagram = instagram,
                Telegram = telegram,
                Facebook = facebook,
                Email = email,
                Discord = discord,
            };

            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();
        }

        public async Task AddFriend(Guid userId, string friendId)
        {
            var friendUser = await GetUserById(friendId);
            var relation = new Relation()
            {
                UserId = userId,
                RelationUserId = friendUser.Id,
                Status = "Friend",
            };
            await _context.Relations.AddAsync(relation);
            await _context.SaveChangesAsync();
        }

        public async Task AddFeedback(string login, string friendId, int note)
        {
            var user = await GetUser(login);
            var user2 = await GetUserById(friendId);

            if(user2.Feedbacks.FirstOrDefault(x => x.SenderId == user.Id.ToString()) == null)
            {
                var feedback = new Feedback()
                {
                    SenderId = user.Id.ToString(),
                    Note = note,
                    UserId = user2.Id,
                };

                await _context.Feedbacks.AddAsync(feedback);
                await _context.SaveChangesAsync();
            }
            else
            {
                var feedback = user2.Feedbacks.FirstOrDefault(x => x.SenderId == user.Id.ToString());
                feedback.Note = note;
                _context.Feedbacks.Update(feedback);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddAddress(Guid contactId, string country, string region, string city, string district, string street, string numberOfHouse)
        {
            var address = new Address()
            {
                Country = country,
                Region = region,
                City = city,
                District = district,
                Street = street,
                NumberOfHouse = numberOfHouse,
                ContactId = contactId,
            };

            await _context.Addresses.AddAsync(address);
            await _context.SaveChangesAsync();
        }

        public async Task ChangePassword(UserArgs args)
        {
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            var user = await GetUser(args.Login);

            if (user != null)
            {
                user.Password = args.Password;
            }

            await _context.SaveChangesAsync();
        }

        public async Task EditUser(UserArgs args)
        {
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            var user = await GetUser(args.Login);
            DateTime? birthday = null;

            if(args.Birthday != "")
            {
                try
                {
                    birthday = Convert.ToDateTime(args.Birthday);
                }
                catch(Exception)
                {
                    args.Birthday = null;
                }
            }

            if (user != null)
            {
                user.Login = args.Login;
                user.Name = args.Name;
                user.Password = args.Password;
                user.Birthday = birthday;
                user.Description = args.Description;
                user.DigitalName = args.DigitalName;
                user.Gender = args.Gender;
                user.Avatar = args.Avatar;
                if (args.Contact != null)
                {
                    var contact = await GetContact(user.Id.ToString());
                    if (contact != null)
                    {
                        contact.Discord = args.Contact.Discord;
                        contact.Instagram = args.Contact.Instagram;
                        contact.Facebook = args.Contact.Facebook;
                        contact.Email = args.Contact.Email;
                        contact.Telegram = args.Contact.Telegram;

                        if (args.Contact.Address != null)
                        {
                            var address = await GetAddress(contact.Id.ToString());
                            if (address != null)
                            {
                                address.Country = args.Contact.Address.Country;
                                address.Region = args.Contact.Address.Region;
                                address.City = args.Contact.Address.City;
                                address.District = args.Contact.Address.District;
                                address.Street = args.Contact.Address.Street;
                                address.NumberOfHouse = args.Contact.Address.NumberOfHouse;
                            }
                            else
                            {
                                await AddAddress(contact.Id, args.Contact.Address.Country, args.Contact.Address.Region, args.Contact.Address.City,
                                    args.Contact.Address.District, args.Contact.Address.Street, args.Contact.Address.NumberOfHouse);
                            }
                        }
                    }
                    else
                    {
                        await AddContact(user.Id, args.Contact.Instagram, args.Contact.Telegram, args.Contact.Facebook, args.Contact.Email, args.Contact.Discord);
                    }
                }

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteRelation(Guid userId, string friendId)
        {
            var relation = await _context.Relations.FirstAsync(x => x.UserId == userId && x.RelationUserId.ToString() == friendId);
            _context.Relations.Remove(relation);
            await _context.SaveChangesAsync();
        }
    }
}
