using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RelationsPlatform.Persistence.Infrastructure;

namespace RelationsPlatform.Tests
{
    public class TestWithSqlite : IDisposable
    {
        private const string InMemoryConnectionString = "DataSource=:memory:";
        private readonly SqliteConnection _connection;

        protected readonly RelationsPlatformDataBaseContext DbContext;

        protected TestWithSqlite()
        {
            _connection = new SqliteConnection(InMemoryConnectionString);
            _connection.Open();
            var options = new DbContextOptionsBuilder<RelationsPlatformDataBaseContext>()
                    .UseSqlite(_connection)
                    .Options;
            DbContext = new RelationsPlatformDataBaseContext(options);
            DbContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _connection.Close();
        }
    }
}
