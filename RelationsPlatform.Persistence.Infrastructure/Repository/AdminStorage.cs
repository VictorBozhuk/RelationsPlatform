using DisciplinePicker.Persistence.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DisciplinePicker.Persistence.Infrastructure.Repository
{
    public class AdminStorage : IAdminStorage
    {
        private DisciplinePickerDatabaseContext _context;

        public AdminStorage(DisciplinePickerDatabaseContext context)
        {
            _context = context;
        }

        public async Task EditAdminData(string login, string password)
        {
            var admin = await _context.Students.FirstOrDefaultAsync(x => x.Role.Name == "admin");
            if (admin != null)
            {
                admin.Login = login;
                admin.Password = password;

                _context.Students.Update(admin);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Student> GetAdmin()
        {
            return await _context.Students.FirstOrDefaultAsync(x => x.Role.Name == "admin");
        }
    }
}
