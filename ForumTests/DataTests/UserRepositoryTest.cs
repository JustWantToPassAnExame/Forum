using Data.Data;
using Data.Entities;
using Data.Repositories;
using Library.Tests;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace ForumTests.DataTests
{
    [TestFixture]
    public class UserRepositoryTest
    {
        [TestCase(1)]
        [TestCase(2)]
        public async Task UserRepository_GetByIdAsync_ReturnsSingleValue(int id)
        {

            using var context = new ForumDbContext(UnitTestHelper.GetUnitTestDBOptions());

            var userRepository = new UserRepository(context);
            var user = await userRepository.GetByIdAsync(id);

            var expected = ExpectedUsers.FirstOrDefault(x => x.Id == id);

            Assert.That(user, Is.EqualTo(expected).Using(new UserEqualityComparer()), message: "GetByIdAsync method works incorrect");
        }

        [Test]

        public async Task UserRepository_GetAllAsync_ReturnAllValues()
        {

            using var context = new ForumDbContext(UnitTestHelper.GetUnitTestDBOptions());

            var userRepository = new UserRepository(context);
            var users = await userRepository.GetAllAsync();

            Assert.That(users, Is.EqualTo(ExpectedUsers).Using(new UserEqualityComparer()), message: "GetAllAsync method works incorrect");
        }

        [Test]

        public async Task UserRepository_AddAsync_AddsValueToDatabase()
        {

            using var context = new ForumDbContext(UnitTestHelper.GetUnitTestDBOptions());

            var userRepository = new UserRepository(context);
            var user = new User { Id = 4, UserRoleId = 1, PersonId = 1,};

            await userRepository.AddAsync(user);
            await context.SaveChangesAsync();

            Assert.That(context.Users.Count(), Is.EqualTo(3), message: "AddAsync method works incorrect");
        }

        [Test]
        public async Task UserRepository_DeleteByIdAsync_DeletesEntity()
        {
            using var context = new ForumDbContext(UnitTestHelper.GetUnitTestDBOptions());

            var userRepository = new UserRepository(context);

            await userRepository.DeleteByIdAsync(1);
            await context.SaveChangesAsync();

            Assert.That(context.Users.Count(), Is.EqualTo(1), message: "DeleteByIdAsync works incorrect");
        }

        [Test]
        public async Task UserRepository_Update_UpdatesEntity()
        {
            using var context = new ForumDbContext(UnitTestHelper.GetUnitTestDBOptions());

            var userRepository = new UserRepository(context);
            var user = new User
            {
                Id = 1,
                NickName = "TestUser",
                UserRoleId = 5,
                PersonId = 5,
                Login = "TestLogin",
                Password = "TestPass"
        
            };

            userRepository.Update(user);
            await context.SaveChangesAsync();

            Assert.That(user, Is.EqualTo(new User
            {
                Id = 1,
                NickName = "TestUser",
               UserRoleId = 5,
                PersonId = 5,
                Login = "TestLogin",
                Password = "TestPass"
            }).Using(new UserEqualityComparer()), message: "Update method works incorrect");
        }
        [TestCase(1)]
        [TestCase(2)]
        public async Task UserRepository_GetByIdWithDetailsAsync_ReturnsWithIncludedEntities(int id)
        {
            using var context = new ForumDbContext(UnitTestHelper.GetUnitTestDBOptions());

            var userRepository = new UserRepository(context);
            var user = await userRepository.GetByIdWithDetailsAsync(id);

            var expected = ExpectedUsers.FirstOrDefault(x => x.Id == id);

            Assert.That(user, Is.EqualTo(expected).Using(new UserEqualityComparer()), message: "GetByIdAsync method works incorrect");

            Assert.That(user.Person,
             Is.EqualTo(ExpectedPersons.FirstOrDefault(p=>p.Id==expected.PersonId)).Using(new PersonEqualityComparer()), message: "GetByIdWithDetailsAsync method doesnt't return included entities");

            Assert.That(user.UserRole,
                Is.EqualTo(ExpectedUserRoles.FirstOrDefault(ur => ur.Id == expected.UserRoleId)).Using(new UserRoleEqualityComparer()), message: "GetByIdWithDetailsAsync method doesnt't return included entities");

            Assert.That(user.Threads,
                Is.EqualTo(ExpectedThreads.Where(t=>t.UserId==expected.Id).OrderBy(i=>i.Id)).Using(new ThreadEqualityComparer()), message: "GetByIdWithDetailsAsync method doesnt't return included entities");
            }

        [Test]
        public async Task UserRepository_GetAllWithDetailsAsync_ReturnsWithIncludedEntities()
        {
            using var context = new ForumDbContext(UnitTestHelper.GetUnitTestDBOptions());

            var userRepository = new UserRepository(context);

            var users = await userRepository.GetAllWithDetailsAsync();

            Assert.That(users,
                Is.EqualTo(ExpectedUsers).Using(new UserEqualityComparer()), message: "GetAllWithDetailsAsync method works incorrect");
            Assert.That(users.Select(u => u.Person).Distinct().OrderBy(p => p.Id),
                Is.EqualTo(ExpectedPersons).Using(new PersonEqualityComparer()), message: "GetAllWithDetailsAsync method doesnt't return included entities");
            Assert.That(users.Select(u => u.UserRole).Distinct().OrderBy(ur => ur.Id),
                Is.EqualTo(ExpectedUserRoles).Using(new UserRoleEqualityComparer()), message: "GetAllWithDetailsAsync method doesnt't return included entities");
            Assert.That(users.SelectMany(u => u.Threads).OrderBy(t => t.Id),
                Is.EqualTo(ExpectedThreads).Using(new ThreadEqualityComparer()), message: "GetAllWithDetailsAsync method doesnt't return included entities");         
        }



        private static IEnumerable<User> ExpectedUsers =>
         new[]
         {
                new User { Id = 1, NickName = "Nick1", UserRoleId = 3, PersonId = 1, Login = "login1", Password = "password1" },
                new User { Id = 2, NickName = "Nick2", UserRoleId = 2, PersonId = 2, Login = "login2", Password = "password2" }
            };
        private static IEnumerable<Person> ExpectedPersons =>
         new[]
         {
               new Person { Id = 1, BirthDate = new DateTime(2001, 4, 20), PersonInfo = "Info1" },
               new Person { Id = 2, BirthDate = new DateTime(2000, 3, 3), PersonInfo = "Info1" }
         };
        private static IEnumerable<UserRole> ExpectedUserRoles =>
         new[]
         { 
                new UserRole { Id = 2, RoleName = "Role2" },
                new UserRole { Id = 3, RoleName = "Role3" }
         };
        private static IEnumerable<Thread_> ExpectedThreads =>
        new[]
        {
                new Thread_
                {
                    Id = 1,
                    UserId = 1,
                    TopicId = 1,
                    CreationDate = new DateTime(1,1,1),
                    Vievs = 1,
                    CensorShipId = 1
                },
                 new Thread_
                 {
                     Id = 2,
                     UserId = 2,
                     TopicId = 3,
                     CreationDate = new DateTime(1,1,1),
                     Vievs = 4,
                     CensorShipId = 2
                 }
        };
    }
}