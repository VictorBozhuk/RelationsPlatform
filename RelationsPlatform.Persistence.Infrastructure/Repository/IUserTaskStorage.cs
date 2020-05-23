using RelationsPlatform.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RelationsPlatform.Persistence.Infrastructure.Repository
{
    public interface IUserTaskStorage
    {
        Task AddUserTask(UserTaskArgs args);
        Task<UserTask> GetUserTask(string id);
        Task<List<UserTask>> GetAllUserTasks();
        Task EditUserTask(UserTaskArgs args);
    }

    public class UserTaskArgs
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string Subject { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string Status { get; set; }
    }
}
