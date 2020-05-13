using RelationsPlatform.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RelationsPlatform.Persistence.Infrastructure.Repository
{
    public class DisciplineChoiseStorage : IDisciplineChoiseStorage
    {
        private RelationsPlatformDataBaseContext _context;

        public DisciplineChoiseStorage(RelationsPlatformDataBaseContext context)
        {
            _context = context;
        }

        //public void CreateDisciplineChoise(DisciplineAvailability disciplineAvailability, Student student)
        //{
        //    _context.DisciplineChoises.Add(new DisciplineChoise()
        //    {
        //        DisciplineAvailability = disciplineAvailability,
        //        Student = student
        //    });
        //    _context.SaveChanges();
        //}

        //public void DeleteDisciplineChoise(DisciplineAvailability availability, Student student)
        //{
        //    var DisciplineChoiseToDelete = _context.DisciplineChoises.FirstOrDefault(x => x.DisciplineAvailabilityId == availability.Id && x.StudentId == student.Id);
        //    _context.DisciplineChoises.Remove(DisciplineChoiseToDelete);
        //    _context.SaveChanges();
        //}
    }
}
