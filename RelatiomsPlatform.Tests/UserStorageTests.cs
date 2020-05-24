using RelationsPlatform.Persistence.Infrastructure.Repository;
using RelationsPlatform.Persistence.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

using Xunit;

namespace RelationsPlatform.Tests
{
    public class UserStorageTests : TestWithSqlite
    {
        [Fact]
        public async Task TheNumberOfUsersShouldIncreaseWhenANewUserIsAdded()
        {
            var userStorage = new UserStorage(DbContext);
            var initialUsersCount = DbContext.Users.Count();
            var createUserArgs = new UserArgs
            {
                Name = "Â³êòîð",
                Login = "Victor.com",
                Password = "123",

            };

            await userStorage.CreateUser(createUserArgs);

            Assert.Equal(initialUsersCount + 1, DbContext.Users.Count());
        }

        [Fact]
        public async Task TheUserMustBeInTheListOfUsersWhenAUserIsAdded()
        {
            var studentStorage = new UserStorage(DbContext);
            var initialStudentsCount = DbContext.Users.Count();
            var createUserArgs = new UserArgs
            {
                Name = "Â³êòîð",
                Login = "Victor.com",
                Password = "123",

            };

            await studentStorage.CreateUser(createUserArgs);
            var newStudent = DbContext.Users.First(x => x.Login == createUserArgs.Login && x.Password == createUserArgs.Password);
            Assert.True(DbContext.Users.Contains(newStudent));
        }

        [Fact]
        public async Task TheUserPasswordMustBeEditedWhenTheUserPasswordWasEdited()
        {
            var studentId = new Guid("08ad1c46-d57c-4e38-b54a-733359e36e43");
            var newPassword = "123456789";
            var studentStorage = new UserStorage(DbContext);
            var args = new UserArgs()
            {
                Password = newPassword,
                Login = "a",
            };

            await studentStorage.ChangePassword(args);
            var newStudent = DbContext.Users.FirstOrDefault(x => x.Id == studentId);
            Assert.Equal(newStudent.Password, newPassword);
        }

        [Fact]
        public async Task TheUserMustBeEditedWhenTheUserWasEdited()
        {
            var studentId = new Guid("08ad1c46-d57c-4e38-b54a-733359e36e43");
            var newName = "Gp37";
            var studentStorage = new UserStorage(DbContext);
            var student = DbContext.Users.FirstOrDefault(x => x.Id == studentId);
            var studentArgs = new UserArgs()
            {
                Id = student.Id,
                Login = student.Login,
                Name = newName,
            };

            await studentStorage.EditUser(studentArgs);

            Assert.Equal(newName, DbContext.Users.FirstOrDefault(x => x.Id == studentId).Name);
        }

        [Fact]
        public async Task TheUserMustReturnWhenCallingTheUserById()
        {
            var studentId = new Guid("08ad1c46-d57c-4e38-b54a-733359e36e43");
            var studentStorage = new UserStorage(DbContext);
            var student = DbContext.Users.FirstOrDefault(x => x.Id == studentId);
            var newStudent = await studentStorage.GetUserById(studentId.ToString());

            Assert.Equal(student, newStudent);
        }

        [Fact]
        public async Task TheUserMustReturnWhenCallingTheUserByLogin()
        {
            var studentLogin = DbContext.Users.First().Login;
            var studentStorage = new UserStorage(DbContext);
            var student = DbContext.Users.FirstOrDefault(x => x.Login == studentLogin);
            var newStudent = await studentStorage.GetUser(studentLogin);

            Assert.Equal(student, newStudent);
        }

        [Fact]
        public async Task TheNumberOfUsersInTheDatabaseMustBeEqualToTheNumberOfReturnedUsers()
        {
            int count = 3;
            var studentStorage = new UserStorage(DbContext);
            var Students = await studentStorage.GetUsers();

            Assert.Equal(count, Students.Count);
        }

        [Fact]
        public async Task TheContactOfUserShouldReturnWhenYouCallHimt()
        {
            var student = DbContext.Users.First();
            var studentStorage = new UserStorage(DbContext);
            var studentr = DbContext.Users.FirstOrDefault(x => x.Login == student.Login);
            var newStudent = await studentStorage.GetUser(student.Login);

            Assert.Equal(student.Login, newStudent.Login);
        }

        [Fact]
        public async Task TheUserShouldIncreaseTheNumberOfFriendsWhenAFriendIsAdded()
        {
            var studentStorage = new UserStorage(DbContext);
            var student1 = await studentStorage.GetUserById("08ad1c46-d57c-4e38-b54a-733359e36e43");
            var student2 = await studentStorage.GetUserById("8c93be5e-359c-4610-89ba-289d79cbb388");
            var countFriends = student1.Relations.Count;
                
            var relation = new Relation()
            {
                RelationUserId = student2.Id,
                UserId = student1.Id,
                Status = "Friend",
            };
            await studentStorage.AddFriend(student1.Id, student2.Id.ToString());

            Assert.Equal(countFriends + 1, student1.Relations.Count);
        }


        [Fact]
        public async Task TheUserMustReceiveAResponseWhenAnotherUserLeavesAFeedback()
        {
            var studentStorage = new UserStorage(DbContext);
            var student1 = await studentStorage.GetUserById("08ad1c46-d57c-4e38-b54a-733359e36e43");
            var student2 = await studentStorage.GetUserById("8c93be5e-359c-4610-89ba-289d79cbb388");
            var count = student1.Feedbacks.Count;

            await studentStorage.AddFeedback(student1.Login, student2.Id.ToString(), 100);

            Assert.Equal(student1.Id.ToString(), student2.Feedbacks.First().SenderId);
        }

        [Fact]
        public async Task TheUserMustHaveAnAddressWhenTheAddressIsAdded()
        {
            var studentStorage = new UserStorage(DbContext);
            var student1 = await studentStorage.GetUserById("08ad1c46-d57c-4e38-b54a-733359e36e43");

            var contact = await studentStorage.GetContact("08ad1c46-d57c-4e38-b54a-733359e36e43");
            var city = "Ëüâ³â";

            await studentStorage.AddAddress(contact.Id, null, null, "Ëüâ³â", null, null, null);
            var address = await studentStorage.GetAddress(contact.Id.ToString());

            Assert.Equal(city, address.City);
        }
    }
}
