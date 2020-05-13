using RelationsPlatform.Persistence.Model;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RelationsPlatform.Persistence.Infrastructure.Repository
{
    public class LecturerStorage : ILecturerStorage
    {
        private RelationsPlatformDataBaseContext _context;

        public LecturerStorage(RelationsPlatformDataBaseContext context)
        {
            _context = context;
        }

        //public async Task AddLecturer(NewLecturerArgs args)
        //{
        //    if (args == null)
        //    {
        //        throw new ArgumentNullException(nameof(args));
        //    }

        //    var lecturer = new Lecturer
        //    {
        //        Department = args.Department,
        //        Faculty = args.Faculty,
        //        Name = args.Name
        //    };

        //    await _context.Lecturers.AddAsync(lecturer);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task DeleteLecturer(string lecturerId)
        //{
        //    var lecturer = await _context.Lecturers.FirstOrDefaultAsync(x => x.Id.ToString() == lecturerId);
        //    if (lecturer != null)
        //    {
        //        _context.Lecturers.Remove(lecturer);
        //        await _context.SaveChangesAsync();
        //    }
        //}

        //public async Task EditLecturer(string lecturerId, NewLecturerArgs args)
        //{
        //    if (args == null)
        //    {
        //        throw new ArgumentNullException(nameof(args));
        //    }

        //    var lecturer = await _context.Lecturers.FirstOrDefaultAsync(x => x.Id.ToString() == lecturerId);
        //    if (lecturer != null)
        //    {
        //        lecturer.Department = args.Department;
        //        lecturer.Faculty = args.Faculty;
        //        lecturer.Name = args.Name;

        //        _context.Lecturers.Update(lecturer);
        //        await _context.SaveChangesAsync();
        //    }
        //}

        //public async Task<List<Lecturer>> GetAllLecturers()
        //{
        //    return await _context.Lecturers.ToListAsync();
        //}

        //public async Task<Lecturer> GetLecturer(string lecturerId)
        //{
        //    return await _context.Lecturers.FirstOrDefaultAsync(x => x.Id.ToString() == lecturerId);
        //}

        //public async Task<Lecturer> GetLecturerByName(string name)
        //{
        //    return await _context.Lecturers.FirstOrDefaultAsync(x => x.Name == name);
        //}
    }
}
