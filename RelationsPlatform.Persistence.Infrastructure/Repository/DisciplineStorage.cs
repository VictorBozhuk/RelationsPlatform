using DisciplinePicker.Persistence.Model;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DisciplinePicker.Persistence.Infrastructure.Repository
{
    public class DisciplineStorage : IDisciplineStorage
    {
        private DisciplinePickerDatabaseContext _context;

        public DisciplineStorage(DisciplinePickerDatabaseContext context)
        {
            _context = context;
        }

        public async Task AddDiscipline(AddDisciplineArgs args)
        {
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            var discipline = new Discipline
            {
                Department = args.Department,
                Description = args.Description,
                Faculty = args.Faculty,
                Name = args.Name
            };

            await _context.Disciplines.AddAsync(discipline);
        }

        public async Task DeleteDiscipline(string disciplineId)
        {
            var discipline = await _context.Disciplines.FirstOrDefaultAsync(x => x.Id.ToString() == disciplineId);
            if (discipline != null)
            {
                _context.Disciplines.Remove(discipline);
                await _context.SaveChangesAsync();
            }
        }

        public async Task EditDiscipline(string disciplineId, AddDisciplineArgs args)
        {
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            var discipline = await _context.Disciplines.FirstOrDefaultAsync(x => x.Id.ToString() == disciplineId);
            if (discipline != null)
            {
                discipline.Name = args.Name;
                discipline.Faculty = args.Faculty;
                discipline.Description = args.Description;
                discipline.Department = args.Department;

                _context.Disciplines.Update(discipline);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Discipline>> GetAllDisciplines()
        {
            return await _context.Disciplines.ToListAsync();
        }

        public async Task<Discipline> GetDiscipline(string disciplineId)
        {
            return await _context.Disciplines.FirstOrDefaultAsync(x => x.Id.ToString() == disciplineId);
        }

        public async Task<Discipline> GetDisciplineByName(string name)
        {
            return await _context.Disciplines.FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}
