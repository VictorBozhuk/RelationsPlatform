using RelationsPlatform.Core;

using System;

namespace RelationsPlatform.Infrastructure
{
    public class RootDirectoryProvider : IRootDirectoryProvider
    {
        public string RootDirectory => Environment.CurrentDirectory;
    }
}
