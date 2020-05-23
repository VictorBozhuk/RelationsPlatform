using Microsoft.EntityFrameworkCore;
using RelationsPlatform.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RelationsPlatform.Persistence.Infrastructure.Repository
{
    public class UserTaskStorage : IUserTaskStorage
    {
        private RelationsPlatformDataBaseContext _context;

        public UserTaskStorage(RelationsPlatformDataBaseContext context)
        {
            _context = context;
        }

        public async Task AddUserTask(UserTaskArgs args)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id.ToString() == args.Id);
            var task = new UserTask()
            {
                LongDescription = args.LongDescription,
                ShortDescription = args.ShortDescription,
                Subject = args.Subject,
                UserId = user.Id,
                Status = args.Status,
            };

            await _context.UserTasks.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        public async Task EditUserTask(UserTaskArgs args)
        {
            var task = await GetUserTask(args.Id);
            if(task != null)
            {
                task.LongDescription = args.LongDescription;
                task.ShortDescription = args.ShortDescription;
                task.Subject = args.Subject;
                task.Status = args.Status;
            }

            _context.UserTasks.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task<UserTask> GetUserTask(string id)
        {
            return await _context.UserTasks.FirstOrDefaultAsync(x => x.Id.ToString() == id);
        }

        public async Task<List<UserTask>> GetAllUserTasks()
        {
            return await _context.UserTasks.ToListAsync();
        }
    }
}
