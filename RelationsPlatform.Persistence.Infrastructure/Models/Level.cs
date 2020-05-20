using System;
using System.Collections.Generic;
using System.Text;

namespace RelationsPlatform.Persistence.Infrastructure.Models
{
    public class Level
    {
        public static List<KeyValuePair<int, string>> Levels = new List<KeyValuePair<int, string>>
        {
            new KeyValuePair<int, string>(1, "Поверхневі теоретичні знання."),
            new KeyValuePair<int, string>(2, "Базові теоретичні і практичні знання."),
            new KeyValuePair<int, string>(3, "Достатні теоретичні і практичні знання."),
            new KeyValuePair<int, string>(4, "Хороші теоретичні і практичні знання."),
            new KeyValuePair<int, string>(5, "Відмінні теоретичні і практичні знання."),
        };
    }
}
