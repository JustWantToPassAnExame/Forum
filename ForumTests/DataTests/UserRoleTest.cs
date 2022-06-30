using Data.Data;
using Data.Entities;
using Data .Repositories;
using Library.Tests;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumTests.DataTests
{
    [TestFixture]
    public class UserRoleTest
    {
        [TestCase(1)]
        [TestCase(2)]
        public async Task UserRoleRepository_GetByIdAsync_ReturnsSingleValue(int id)
        {

            using var context = new ForumDbContext(UnitTestHelper.GetUnitTestDBOptions());

            var userRoleRepository = new UserRoleRepository(context);
            var userRole = await userRoleRepository.GetByIdAsync(id);

            var expected = ExpectedUserRoles.FirstOrDefault(x => x.Id == id);

            Assert.That(userRole, Is.EqualTo(expected).Using(new UserRoleEqualityComparer()), message: "GetByIdAsync method works incorrect");
        }

        [Test]

        public async Task UserRoleRepository_GetAllAsync_ReturnAllValues()
        {

            using var context = new ForumDbContext(UnitTestHelper.GetUnitTestDBOptions());

            var userRoleRepository = new UserRoleRepository(context);
            var userRoles = await userRoleRepository.GetAllAsync();

            Assert.That(userRoles, Is.EqualTo(ExpectedUserRoles).Using(new UserRoleEqualityComparer()), message: "GetAllAsync method works incorrect");
        }

        [Test]

        public async Task UserRoleRepository_AddAsync_AddsValueToDatabase()
        {

            using var context = new ForumDbContext(UnitTestHelper.GetUnitTestDBOptions());

            var userRoleRepository = new UserRoleRepository(context);
            var userRole = new UserRole { Id = 4 };

            await userRoleRepository.AddAsync(userRole);
            await context.SaveChangesAsync();

            Assert.That(context.UserRoles.Count(), Is.EqualTo(4), message: "AddAsync method works incorrect");
        }

        [Test]
        public async Task UserRoleRepository_DeleteByIdAsync_DeletesEntity()
        {
            using var context = new ForumDbContext(UnitTestHelper.GetUnitTestDBOptions());

            var userRoleRepository = new UserRoleRepository(context);

            await userRoleRepository.DeleteByIdAsync(1);
            await context.SaveChangesAsync();

            Assert.That(context.UserRoles.Count(), Is.EqualTo(2), message: "DeleteByIdAsync works incorrect");
        }

        [Test]
        public async Task UserRoleRepository_Update_UpdatesEntity()
        {
            using var context = new ForumDbContext(UnitTestHelper.GetUnitTestDBOptions());

            var userRoleRepository = new UserRoleRepository(context);
            var userRole = new UserRole
            {
                Id = 1,
                RoleName = "NewUserRole"
            };

            userRoleRepository.Update(userRole);
            await context.SaveChangesAsync();

            Assert.That(userRole, Is.EqualTo(new UserRole
            {
                Id = 1,
                RoleName = "NewUserRole"
            }).Using(new UserRoleEqualityComparer()), message: "Update method works incorrect");
        }




        private static IEnumerable<UserRole> ExpectedUserRoles =>
         new[]
         {
                new UserRole { Id = 1, RoleName = "Role1" },
                new UserRole { Id = 2, RoleName = "Role2" },
                new UserRole { Id = 3, RoleName = "Role3" }
         };
    }
}
