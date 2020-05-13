using DisciplinePicker.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DisciplinePicker.Persistence.Infrastructure.Repository
{
    public class StudentStorage : IStudentStorage
    {
        private DisciplinePickerDatabaseContext _context;

        public StudentStorage(DisciplinePickerDatabaseContext context)
        {
            _context = context;
        }

        public async Task AddNewStudent(AddNewStudentArgs args)
        {
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            var role = await _context.Roles.FirstOrDefaultAsync(x => x.Name == "student");
            if (role != null)
            {
                var student = new Student
                {
                    Login = args.Login,
                    Password = args.Password,
                    Course = args.Course,
                    Department = args.Department,
                    Group = args.Group,
                    Name = args.Name,
                    Speciality = args.Speciality,
                    RoleId = role.Id
                };

                _context.Students.Add(student);
                await _context.SaveChangesAsync();
            }
        }

        public async Task ChangeStudentsPassword(string studentId, string newPassword)
        {
            var student = await _context.Students.FirstOrDefaultAsync(x => x.Id.ToString() == studentId);
            if (student != null)
            {
                student.Password = newPassword;
            }
        }

        public async Task DeleteStudent(string studentId)
        {
            var student = await _context.Students.FirstOrDefaultAsync(x => x.Id.ToString() == studentId);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
        }

        public async Task EditStudent(string studentId, AddNewStudentArgs args)
        {
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            var student = await _context.Students.FirstOrDefaultAsync(x => x.Id.ToString() == studentId);
            if (student != null)
            {
                student.Speciality = args.Speciality;
                student.Course = args.Course;
                student.Department = args.Department;
                student.Group = args.Group;
                student.Name = args.Name;
                student.Password = args.Password;
                student.Login = args.Login;

                _context.Students.Update(student);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Student> GetStudent(string studentId)
        {
            return await _context.Students.FirstOrDefaultAsync(x => x.Id.ToString() == studentId);
        }

        public async Task<Student> GetStudentByLogin(string login)
        {
            return await _context.Students.FirstOrDefaultAsync(x => x.Login == login);
        }
    }
}
