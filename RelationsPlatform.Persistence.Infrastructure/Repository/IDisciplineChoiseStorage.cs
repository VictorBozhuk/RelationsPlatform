using DisciplinePicker.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisciplinePicker.Persistence.Infrastructure.Repository
{
    public interface IDisciplineChoiseStorage
    {
        void CreateDisciplineChoise(DisciplineAvailability disciplineAvailability, Student student);

        void DeleteDisciplineChoise(DisciplineAvailability availability, Student student);
    }
}
