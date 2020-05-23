using RelationsPlatform.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RelationsPlatform.Persistence.Infrastructure.Repository
{
    public interface IUserStorage
    {
        Task<User> GetUser(string login);
        Task<User> GetUserById(string id);
        Task<List<User>> GetUsers();
        Task EditUser(UserArgs args);
        Task<Contact> GetContact(string userId);
        Task<Address> GetAddress(string contactId);
        Task AddContact(Guid userId, string instagram, string telegram, string facebook, string email, string discord);
        Task AddAddress(Guid contactId, string country, string region, string city, string district, string street, string numberOfHouse);
        Task AddFriend(Guid userId, string friendId);
        Task DeleteRelation(Guid userId, string friendId);
        Task ChangePassword(UserArgs args);
        Task AddFeedback(string login, string friendId, int note);
    }

    public class UserArgs
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Description { get; set; }
        public string DigitalName { get; set; }
        public byte[] Avatar { get; set; }
        public string Birthday { get; set; }

        public Contact Contact { get; set; }
        public virtual Skill Skill { get; set; }
        public virtual Education Education { get; set; }
        public virtual ICollection<Relation> Relations { get; set; }
    }
}
