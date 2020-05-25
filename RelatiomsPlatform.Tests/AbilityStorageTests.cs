using RelationsPlatform.Persistence.Infrastructure.Repository;
using RelationsPlatform.Persistence.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

using Xunit;

namespace RelationsPlatform.Tests
{
    public class AbilityStorageTests : TestWithSqlite
    {
        [Fact]
        public async Task TheAbilityMustBeInTheListOfAbilitiesWhenAAbilityIsAdded()
        {
            var id = new Guid("59ee1d62-ef16-4efa-a251-ef771fda8fbc");
            var name = "1234qwer";
            var storage = new AbilityStorage(DbContext);
            var args = new AbilityArgs
            {
                Name = name,
                SkillId = id,
            };

            await storage.AddAbility(args);
            var obj = DbContext.Abilities.First(x => x.Name == name);
            Assert.True(DbContext.Abilities.Contains(obj));
        }

        [Fact]
        public async Task TheAbilityMustReturnWhenCallingTheAbility()
        {
            var id = new Guid("ffb3043c-9e35-4545-be99-09783438eb61");
            var storage = new AbilityStorage(DbContext);
            var obj = DbContext.Abilities.FirstOrDefault(x => x.Id == id);
            var newObj = await storage.GetAbility(id.ToString());
            Assert.Equal(obj, newObj);
        }

        [Fact]
        public async Task TheAbilityMustDeleteWhenDeletingTheAbility()
        {
            var id = new Guid("ffb3043c-9e35-4545-be99-09783438eb61");
            var storage = new AbilityStorage(DbContext);
            var obj = DbContext.Abilities.FirstOrDefault(x => x.Id == id);
            await storage.DeleteAbility(id.ToString());

            Assert.False(DbContext.Abilities.Contains(obj));
        }
    }
}
