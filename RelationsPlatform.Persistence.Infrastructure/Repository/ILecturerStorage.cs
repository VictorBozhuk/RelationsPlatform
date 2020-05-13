using RelationsPlatform.Persistence.Model;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace RelationsPlatform.Persistence.Infrastructure.Repository
{
    public interface ILecturerStorage
    {
        //Task<List<Lecturer>> GetAllLecturers();
        //Task<Lecturer> GetLecturer(string lecturerId);
        //Task<Lecturer> GetLecturerByName(string name);
        //Task AddLecturer(NewLecturerArgs args);
        //Task EditLecturer(string lecturerId, NewLecturerArgs args);
        //Task DeleteLecturer(string lecturerId);
    }

    public class NewLecturerArgs
    {
        public string Name { get; set; }
        public string Department { get; set; }
        public string Faculty { get; set; }
    }
}
