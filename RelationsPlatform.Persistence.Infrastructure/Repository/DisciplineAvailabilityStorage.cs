using DisciplinePicker.Persistence.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisciplinePicker.Persistence.Infrastructure.Repository
{
    public class DisciplineAvailabilityStorage : IDisciplineAvailabilityStorage
    {
        private DisciplinePickerDatabaseContext _context;

        public DisciplineAvailabilityStorage(DisciplinePickerDatabaseContext context)
        {
            _context = context;
        }

        public async Task AddNewAvailability(NewAvailabilityArgs args)
        {
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            var discipline = await _context.Disciplines.FirstOrDefaultAsync(x => x.Id.ToString() == args.DisciplineId);
            if (discipline == null)
            {
                throw new ArgumentException("Discipline not found", nameof(args));
            }

            var lecturer = await _context.Lecturers.FirstOrDefaultAsync(x => x.Id.ToString() == args.LecturerId);
            if (discipline == null)
            {
                throw new ArgumentException("Lecturer not found", nameof(args));
            }

            var availability = new DisciplineAvailability
            {
                Discipline = discipline,
                Lecturer = lecturer,
                Hours = args.Hours,
                MaxAllovedStudents = args.MaxAllovedStudents,
                MinAllovedStudents = args.MinAllovedStudents,
                Semester = args.Semester,
                Year = args.Year
            };
        }

        public async Task DeleteAvailability(string availabilityId)
        {
            var availability = await _context.DisciplineAvailabilities.FirstOrDefaultAsync(x => x.Id.ToString() == availabilityId);
            if (availability != null)
            {
                _context.DisciplineAvailabilities.Remove(availability);
                await _context.SaveChangesAsync();
            }
        }

        public async Task EditAvailability(string availabilityId, NewAvailabilityArgs args)
        {
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            var discipline = await _context.Disciplines.FirstOrDefaultAsync(x => x.Id.ToString() == args.DisciplineId);
            if (discipline == null)
            {
                throw new ArgumentException("Discipline not found", nameof(args));
            }

            var lecturer = await _context.Lecturers.FirstOrDefaultAsync(x => x.Id.ToString() == args.LecturerId);
            if (discipline == null)
            {
                throw new ArgumentException("Lecturer not found", nameof(args));
            }

            var availability = await _context.DisciplineAvailabilities.FirstOrDefaultAsync(x => x.Id.ToString() == availabilityId);
            if (availability != null)
            {
                availability.Discipline = discipline;
                availability.Lecturer = lecturer;
                availability.Semester = args.Semester;
                availability.MaxAllovedStudents = args.MaxAllovedStudents;
                availability.MinAllovedStudents = args.MinAllovedStudents;
                availability.Hours = args.Hours;
                availability.Year = args.Year;

                _context.DisciplineAvailabilities.Update(availability);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<DisciplineAvailability> GetAvailability(string availabilityId)
        {
            return await _context.DisciplineAvailabilities.FirstOrDefaultAsync(x => x.Id.ToString() == availabilityId);
        }

        public async Task<List<DisciplineAvailability>> GetFilledDisciplineAvailabilities()
        {
            throw new NotImplementedException();
        }

        public async Task<List<DisciplineAvailability>> GetFilledDisciplineAvailabilitiesOfStudents(string studentId)
        {
            throw new NotImplementedException();
        }
        public IQueryable<DisciplineAvailability> GetAvailabilities()
        {
            return _context.DisciplineAvailabilities.Include("Discipline").Include("DisciplineChoises").Include("Lecturer");
        }

        public DisciplineAvailability GetById(Guid id)
        {
            return _context.DisciplineAvailabilities.FirstOrDefault(x => x.Id == id);
        }
    }
}
