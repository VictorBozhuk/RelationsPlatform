using RelationsPlatform.Persistence.Model;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace RelationsPlatform.Persistence.Infrastructure.Repository
{
    public interface IDisciplineAvailabilityStorage
    {
        //Task<DisciplineAvailability> GetAvailability(string availabilityId);
        //Task EditAvailability(string availabilityId, NewAvailabilityArgs args);
        //Task AddNewAvailability(NewAvailabilityArgs args);
        //Task DeleteAvailability(string availabilityId);
        //Task<List<DisciplineAvailability>> GetFilledDisciplineAvailabilities();
        //Task<List<DisciplineAvailability>> GetFilledDisciplineAvailabilitiesOfStudents(string studentId);
    }

    public class NewAvailabilityArgs
    {
        public int Hours { get; set; }
        public int Year { get; set; }
        public int Semester { get; set; }
        public int MinAllovedStudents { get; set; }
        public int MaxAllovedStudents { get; set; }
        public string DisciplineId { get; set; }
        public string LecturerId { get; set; }
    }
}
