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
    public class TopicRepositoryTest
    {
        [TestCase(1)]
        [TestCase(2)]
        public async Task TopicRepository_GetByIdAsync_ReturnsSingleValue(int id)
        {

            using var context = new ForumDbContext(UnitTestHelper.GetUnitTestDBOptions());

            var topicRepository = new TopicRepository(context);
            var topic = await topicRepository.GetByIdAsync(id);

            var expected = ExpectedTopics.FirstOrDefault(x => x.Id == id);

            Assert.That(topic, Is.EqualTo(expected).Using(new TopicEqualityComparer()), message: "GetByIdAsync method works incorrect");
        }

        [Test]

        public async Task TopicRepository_GetAllAsync_ReturnAllValues()
        {

            using var context = new ForumDbContext(UnitTestHelper.GetUnitTestDBOptions());

            var topicRepository = new TopicRepository(context);
            var topics = await topicRepository.GetAllAsync();

            Assert.That(topics, Is.EqualTo(ExpectedTopics).Using(new TopicEqualityComparer()), message: "GetAllAsync method works incorrect");
        }

        [Test]

        public async Task TopicRepository_AddAsync_AddsValueToDatabase()
        {

            using var context = new ForumDbContext(UnitTestHelper.GetUnitTestDBOptions());

            var topicRepository = new TopicRepository(context);
            var topic = new Topic { Id = 4 };

            await topicRepository.AddAsync(topic);
            await context.SaveChangesAsync();

            Assert.That(context.Topics.Count(), Is.EqualTo(4), message: "AddAsync method works incorrect");
        }

        [Test]
        public async Task TopicRepository_DeleteByIdAsync_DeletesEntity()
        {
            using var context = new ForumDbContext(UnitTestHelper.GetUnitTestDBOptions());

            var topicRepository = new TopicRepository(context);

            await topicRepository.DeleteByIdAsync(1);
            await context.SaveChangesAsync();

            Assert.That(context.Topics.Count(), Is.EqualTo(2), message: "DeleteByIdAsync works incorrect");
        }

        [Test]
        public async Task TopicRepository_Update_UpdatesEntity()
        {
            using var context = new ForumDbContext(UnitTestHelper.GetUnitTestDBOptions());

            var topicRepository = new TopicRepository(context);
            var topic = new Topic
            {
                Id = 1,
                TopicName = "NewTopic"
            };

            topicRepository.Update(topic);
            await context.SaveChangesAsync();

            Assert.That(topic, Is.EqualTo(new Topic
            {
                Id = 1,
               TopicName = "NewTopic"
            }).Using(new TopicEqualityComparer()), message: "Update method works incorrect");
        }




        private static IEnumerable<Topic> ExpectedTopics =>
         new[]
         {
                new Topic { Id = 1, TopicName = "Topic1" },
                new Topic { Id = 2, TopicName = "Topic2" },
                new Topic { Id = 3, TopicName = "Topic3" }
         };
    }
}
