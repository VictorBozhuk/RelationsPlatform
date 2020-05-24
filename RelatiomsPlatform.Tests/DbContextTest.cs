using System.Threading.Tasks;
using Xunit;

namespace RelationsPlatform.Tests
{
    public class DbContextTest : TestWithSqlite
    {
        [Fact]
        public async Task DatabaseIsAvailableAndCanBeConnectedTo()
        {
            Assert.True(await DbContext.Database.CanConnectAsync());
        }
    }
}
