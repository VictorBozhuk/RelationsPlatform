using DisciplinePicker.Persistence.Model;

using System.Threading.Tasks;

namespace DisciplinePicker.Persistence.Infrastructure.Repository
{
    public interface IAdminStorage
    {
        Task<Student> GetAdmin();
        Task EditAdminData(string login, string password);
    }
}
