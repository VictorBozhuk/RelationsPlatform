using DisciplinePicker.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DisciplinePicker.Persistence.Infrastructure.Repository
{
    public interface IStudentStorage
    {
        Task<Student> GetStudent(string studentId);
        Task<Student> GetStudentByLogin(string login);
        Task AddNewStudent(AddNewStudentArgs args);
        Task EditStudent(string studentId, AddNewStudentArgs args);
        Task DeleteStudent(string studentId);
        Task ChangeStudentsPassword(string studentId, string newPassword);
    }

    public class AddNewStudentArgs
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public int Course { get; set; }
        public string Department { get; set; }
        public string Group { get; set; }
        public string Name { get; set; }
        public string Speciality { get; set; }
    }
}
