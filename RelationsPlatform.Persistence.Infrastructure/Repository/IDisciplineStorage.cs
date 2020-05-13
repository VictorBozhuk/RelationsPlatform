using DisciplinePicker.Persistence.Model;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace DisciplinePicker.Persistence.Infrastructure.Repository
{
    public interface IDisciplineStorage
    {
        Task AddDiscipline(AddDisciplineArgs args);
        Task<Discipline> GetDiscipline(string disciplineId);
        Task<Discipline> GetDisciplineByName(string name);
        Task EditDiscipline(string disciplineId, AddDisciplineArgs args);
        Task DeleteDiscipline(string disciplineId);
        Task<List<Discipline>> GetAllDisciplines();
    }

    public class AddDisciplineArgs
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Faculty { get; set; }
        public string Department { get; set; }
    }
}
