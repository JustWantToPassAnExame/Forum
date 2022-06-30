using Data.Data;
using Data.Entities;
using Data.Repositories;
using Library.Tests;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumTests.DataTests
{
    [TestFixture]
    public class CensorShipRepositoryTest
    {
        [TestCase(1)]
        [TestCase(2)]
        public async Task CensorShipRepository_GetByIdAsync_ReturnsSingleValue(int id) {

            using var context = new ForumDbContext(UnitTestHelper.GetUnitTestDBOptions());

            var censorShipRepository = new CensorShipRepository(context);
            var censorShip =  await censorShipRepository.GetByIdAsync(id);

            var expected = ExpectedCensorShips.FirstOrDefault(x => x.Id == id);

            Assert.That(censorShip, Is.EqualTo(expected).Using(new CensorShipEqualityComparer()), message: "GetByIdAsync method works incorrect");
        }

        [Test]

        public async Task CensorShipRepository_GetAllAsync_ReturnAllValues() {
            
            using var context = new ForumDbContext(UnitTestHelper.GetUnitTestDBOptions());

            var censorShipRepository = new CensorShipRepository(context);
            var censorShips = await censorShipRepository.GetAllAsync();

            Assert.That(censorShips, Is.EqualTo(ExpectedCensorShips).Using(new CensorShipEqualityComparer()), message: "GetAllAsync method works incorrect"); 
        }

        [Test]

        public async Task CensorShipRepository_AddAsync_AddsValueToDatabase() {

            using var context = new ForumDbContext(UnitTestHelper.GetUnitTestDBOptions());

            var censorShipRepository = new CensorShipRepository(context);
            var productCategory = new CensorShip { Id = 4 };

            await censorShipRepository.AddAsync(productCategory);
            await context.SaveChangesAsync();

            Assert.That(context.CensorShips.Count(), Is.EqualTo(4), message: "AddAsync method works incorrect");
        }

        [Test]
        public async Task CensorShipRepository_DeleteByIdAsync_DeletesEntity()
        {
            using var context = new ForumDbContext(UnitTestHelper.GetUnitTestDBOptions());

            var censorShipRepository = new CensorShipRepository(context);

            await censorShipRepository.DeleteByIdAsync(1);
            await context.SaveChangesAsync();

            Assert.That(context.CensorShips.Count(), Is.EqualTo(2), message: "DeleteByIdAsync works incorrect");
        }

        [Test]
        public async Task CensorShipRepository_Update_UpdatesEntity()
        {
            using var context = new ForumDbContext(UnitTestHelper.GetUnitTestDBOptions());

            var censorShipRepository = new CensorShipRepository(context);
            var censorShip = new CensorShip
            {
                Id = 1,
                CensorStatus = "100+"
            };

            censorShipRepository.Update(censorShip);
            await context.SaveChangesAsync();

            Assert.That(censorShip, Is.EqualTo(new CensorShip
            {
                Id = 1,
                CensorStatus = "100+"
            }).Using(new CensorShipEqualityComparer()), message: "Update method works incorrect");
        }




        private static IEnumerable<CensorShip> ExpectedCensorShips =>
         new[]
         {
                new CensorShip { Id = 1, CensorStatus = "CensorStatus1" },
                new CensorShip { Id = 2, CensorStatus = "CensorStatus2" },
                new CensorShip { Id = 3, CensorStatus = "CensorStatus3" }
         };
    }
}
