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
    public class ThreadRepositoryTest
    {
        [TestCase(1)]
        [TestCase(2)]
        public async Task ThreadRepository_GetByIdAsync_ReturnsSingleValue(int id)
        {

            using var context = new ForumDbContext(UnitTestHelper.GetUnitTestDBOptions());

            var threadRepository = new ThreadRepository(context);
            var thread = await threadRepository.GetByIdAsync(id);

            var expected = ExpectedThreads.First(x => x.Id == id);

            Assert.That(thread, Is.EqualTo(expected).Using(new ThreadEqualityComparer()), message: "GetByIdAsync method works incorrect");

        }

        [Test]

        public async Task ThreadRepository_GetAllAsync_ReturnAllValues()
        {

            using var context = new ForumDbContext(UnitTestHelper.GetUnitTestDBOptions());

            var threadRepository = new ThreadRepository(context);
            var threads = await threadRepository.GetAllAsync();

            Assert.That(threads, Is.EqualTo(ExpectedThreads).Using(new ThreadEqualityComparer()), message: "GetAllAsync method works incorrect");
        }

        [Test]

        public async Task ThreadRepository_AddAsync_AddsValueToDatabase()
        {

            using var context = new ForumDbContext(UnitTestHelper.GetUnitTestDBOptions());

            var threadRepository = new ThreadRepository(context);
            var thread = new Thread_ {
                Id = 4,
                UserId = 1,
                TopicId = 1,
                CreationDate = System.DateTime.Now,
                Vievs = 1,
                CensorShipId = 1,
            };

            await threadRepository.AddAsync(thread);
            await context.SaveChangesAsync();

            Assert.That(context.Threads.Count(), Is.EqualTo(3), message: "AddAsync method works incorrect");
        }

        [Test]
        public async Task ThreadRepository_DeleteByIdAsync_DeletesEntity()
        {
            using var context = new ForumDbContext(UnitTestHelper.GetUnitTestDBOptions());

            var threadRepository = new ThreadRepository(context);

            await threadRepository.DeleteByIdAsync(1);
            await context.SaveChangesAsync();

            Assert.That(context.Threads.Count(), Is.EqualTo(1), message: "DeleteByIdAsync works incorrect");
        }

        [Test]
        public async Task ThreadRepository_Update_UpdatesEntity()
        {
            using var context = new ForumDbContext(UnitTestHelper.GetUnitTestDBOptions());

            var threadRepository = new ThreadRepository(context);
            var thread = new Thread_
            {
                Id = 1,
                UserId = 1,
                TopicId = 1,
                CreationDate = new System.DateTime(1, 1, 1),
                Vievs = 1,
                CensorShipId = 1,
                ThreadTitle = "test",
                ThreadBody = "test"
            };

            threadRepository.Update(thread);
            await context.SaveChangesAsync();

            Assert.That(thread, Is.EqualTo(new Thread_
            {
                Id = 1,
                UserId = 1,
                TopicId = 1,
                CreationDate = new System.DateTime(1, 1, 1),
                Vievs = 1,
                CensorShipId = 1,
                ThreadTitle = "test",
                ThreadBody = "test"
            }).Using(new ThreadEqualityComparer()), message: "Update method works incorrect");
        }

        [TestCase(1)]
        [TestCase(2)]
        public async Task ThreadRepository_GetByIdWithDetailsAsync_ReturnsWithIncludedEntities(int id)
        {
            using var context = new ForumDbContext(UnitTestHelper.GetUnitTestDBOptions());

            var threadRepository = new ThreadRepository(context);
            var thread = await threadRepository.GetByIdWithDetailsAsync(id);

            var expected = ExpectedThreads.FirstOrDefault(x => x.Id == id);

            Assert.That(thread, Is.EqualTo(expected).Using(new ThreadEqualityComparer()), message: "GetByIdAsync method works incorrect");

            Assert.That(thread.Owner,
             Is.EqualTo(ExpectedUsers.FirstOrDefault(u => u.Id == expected.UserId)).Using(new UserEqualityComparer()), message: "GetByIdWithDetailsAsync method doesnt't return included entities");

            Assert.That(thread.CensorShip,
                Is.EqualTo(ExpectedCensorShips.FirstOrDefault(cs => cs.Id == expected.CensorShipId)).Using(new CensorShipEqualityComparer()), message: "GetByIdWithDetailsAsync method doesnt't return included entities");

            Assert.That(thread.Topic,
               Is.EqualTo(ExpectedTopics.FirstOrDefault(t => t.Id == expected.TopicId)).Using(new TopicEqualityComparer()), message: "GetByIdWithDetailsAsync method doesnt't return included entities");

            Assert.That(thread.Coments.OrderBy(c => c.Id),
                Is.EqualTo(ExpectedComments.Where(c => c.ThreadId == expected.Id).OrderBy(c => c.Id)).Using(new CommentEqualityComparer()), message: "GetByIdWithDetailsAsync method doesnt't return included entities");

            Assert.That(thread.Coments.Select(c => c.Owner).Distinct().OrderBy(u => u.Id),
                Is.EqualTo(ExpectedUsers.Where(u => u.Id == 1 || u.Id==2)).Using(new UserEqualityComparer()), message: "GetByIdWithDetailsAsync method doesnt't return included entities");

        }

        [Test]
        public async Task ThredRepository_GetAllWithDetailsAsync_ReturnsWithIncludedEntities()
        {
            using var context = new ForumDbContext(UnitTestHelper.GetUnitTestDBOptions());

            var threadRepository = new ThreadRepository(context);

            var threads = await threadRepository.GetAllWithDetailsAsync();

            Assert.That(threads,
                Is.EqualTo(ExpectedThreads).Using(new ThreadEqualityComparer()), message: "GetAllWithDetailsAsync method works incorrect");

            Assert.That(threads.Select(t => t.Owner).Distinct().OrderBy(o => o.Id),
                    Is.EqualTo(ExpectedUsers).Using(new UserEqualityComparer()), message: "GetAllWithDetailsAsync method doesnt't return included entities");
            
            Assert.That(threads.Select(t => t.CensorShip).Distinct().OrderBy(cs => cs.Id),
                Is.EqualTo(ExpectedCensorShips).Using(new CensorShipEqualityComparer()), message: "GetAllWithDetailsAsync method doesnt't return included entities");
           
            Assert.That(threads.Select(th=>th.Topic).Distinct().OrderBy(t=>t.Id),
                Is.EqualTo(ExpectedTopics).Using(new TopicEqualityComparer()),message: "GetAllWithDetailsAsync method doesnt't return included entities");

            Assert.That(threads.SelectMany(t => t.Coments).Distinct().OrderBy(c => c.Id),
                 Is.EqualTo(ExpectedComments).Using(new CommentEqualityComparer()), message: "GetAllWithDetailsAsync method doesnt't return included entities");

            Assert.That(threads.SelectMany(t => t.Coments.Select(c=>c.Owner)).Distinct().OrderBy(u=>u.Id),
                 Is.EqualTo(ExpectedUsers).Using(new UserEqualityComparer()), message: "GetAllWithDetailsAsync method doesnt't return included entities");

        }




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
        private static IEnumerable<User> ExpectedUsers =>
        new[]
        {
           new User { Id = 1, NickName = "Nick1", UserRoleId = 3, PersonId = 1, Login = "login1", Password = "password1" },
           new User { Id = 2, NickName = "Nick2", UserRoleId = 2, PersonId = 2, Login = "login2", Password = "password2" }

        };
       private static IEnumerable<CensorShip> ExpectedCensorShips =>
       new[]
       {
                new CensorShip { Id = 1, CensorStatus = "CensorStatus1" },
                new CensorShip { Id = 2, CensorStatus = "CensorStatus2" },
       };
       private static IEnumerable<Topic> ExpectedTopics =>
       new[]
       {
                new Topic { Id = 1, TopicName = "Topic1" },
                new Topic { Id = 3, TopicName = "Topic3" }
       };
        private static IEnumerable<Comment> ExpectedComments =>
        new[]
        {
                new Comment { Id = 1, UserId = 1, ThreadId = 1, CreationDate = new DateTime(2022, 4, 20, 22, 25, 3), CommentText = "Comment1" },
                new Comment { Id = 2, UserId = 2, ThreadId = 1, CreationDate = new DateTime(2022, 4, 20, 22, 26, 20), CommentText = "Comment2" },
                new Comment { Id = 3, UserId = 1, ThreadId = 2, CreationDate = new DateTime(2022, 6, 21, 21, 10, 58), CommentText = "Comment3" },
                new Comment { Id = 5, UserId = 2, ThreadId = 2, CreationDate = new DateTime(2022, 6, 21, 21, 11, 5), CommentText = "Comment4"}
             };


    }
}