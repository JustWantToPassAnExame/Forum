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
    public class PersonRepositoryTest
    {
        [TestCase(1)]
        [TestCase(2)]
        public async Task PersonRepository_GetByIdAsync_ReturnsSingleValue(int id)
        {

            using var context = new ForumDbContext(UnitTestHelper.GetUnitTestDBOptions());

            var personRepository = new PersonRepository(context);
            var person = await personRepository.GetByIdAsync(id);

            var expected = ExpectedPersons.FirstOrDefault(x => x.Id == id);

            Assert.That(person, Is.EqualTo(expected).Using(new PersonEqualityComparer()), message: "GetByIdAsync method works incorrect");
        }

        [Test]

        public async Task PersonRepository_GetAllAsync_ReturnAllValues()
        {

            using var context = new ForumDbContext(UnitTestHelper.GetUnitTestDBOptions());

            var personRepository = new PersonRepository(context);
            var persons = await personRepository.GetAllAsync();

            Assert.That(persons, Is.EqualTo(ExpectedPersons).Using(new PersonEqualityComparer()), message: "GetAllAsync method works incorrect");
        }

        [Test]

        public async Task PersonRepository_AddAsync_AddsValueToDatabase()
        {

            using var context = new ForumDbContext(UnitTestHelper.GetUnitTestDBOptions());

            var personRepository = new PersonRepository(context);
            var person = new Person { Id = 3 , 
                BirthDate = DateTime.Now, 
            };

            await personRepository.AddAsync(person);
            await context.SaveChangesAsync();

            Assert.That(context.Persons.Count(), Is.EqualTo(3), message: "AddAsync method works incorrect");
        }

        [Test]
        public async Task PersonRepository_DeleteByIdAsync_DeletesEntity()
        {
            using var context = new ForumDbContext(UnitTestHelper.GetUnitTestDBOptions());

            var personRepository = new PersonRepository(context);

            await personRepository.DeleteByIdAsync(1);
            await context.SaveChangesAsync();

            Assert.That(context.Persons.Count(), Is.EqualTo(1), message: "DeleteByIdAsync works incorrect");
        }

        [Test]
        public async Task PersonRepository_Update_UpdatesEntity()
        {
            using var context = new ForumDbContext(UnitTestHelper.GetUnitTestDBOptions());

            var personRepository = new PersonRepository(context);
            var person = new Person
            {
                Id = 1,
                BirthDate = new DateTime(1, 1, 1),
                PersonInfo = "information"
            };

            personRepository.Update(person);
            await context.SaveChangesAsync();

            Assert.That(person, Is.EqualTo(new Person
            {
                Id = 1,
                BirthDate = new DateTime(1, 1, 1),
                PersonInfo = "information"
            }).Using(new PersonEqualityComparer()), message: "Update method works incorrect");
        }




        private static IEnumerable<Person> ExpectedPersons =>
         new[]
         {

                new Person { Id = 1, BirthDate = new DateTime(2001, 4, 20), PersonInfo = "Info1" },
                new Person { Id = 2, BirthDate = new DateTime(2000, 3, 3), PersonInfo = "Info1" }
         };
    }
}
