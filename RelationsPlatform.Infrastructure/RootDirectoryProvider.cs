using DisciplinePicker.Core;

using System;

namespace DisciplinePicker.Infrastructure
{
    public class RootDirectoryProvider : IRootDirectoryProvider
    {
        public string RootDirectory => Environment.CurrentDirectory;
    }
}
