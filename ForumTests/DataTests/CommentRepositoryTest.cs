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
    public class CommentRepositoryTest
    {
        [TestCase(1)]
        [TestCase(2)]
        public async Task CommentRepository_GetByIdAsync_ReturnsSingleValue(int id)
        {

            using var context = new ForumDbContext(UnitTestHelper.GetUnitTestDBOptions());

            var commentRepository = new CommentRepository(context);
            var comment = await commentRepository.GetByIdAsync(id);

            var expected = ExpectedComments.FirstOrDefault(x => x.Id == id);

            Assert.That(comment, Is.EqualTo(expected).Using(new CommentEqualityComparer()), message: "GetByIdAsync method works incorrect");
        }

        [Test]

        public async Task CommentRepository_GetAllAsync_ReturnAllValues()
        {

            using var context = new ForumDbContext(UnitTestHelper.GetUnitTestDBOptions());

            var commentRepository = new CommentRepository(context);
            var comments = await commentRepository.GetAllAsync();

            Assert.That(comments, Is.EqualTo(ExpectedComments).Using(new CommentEqualityComparer()), message: "GetAllAsync method works incorrect");
        }

        [Test]

        public async Task CommentRepository_AddAsync_AddsValueToDatabase()
        {

            using var context = new ForumDbContext(UnitTestHelper.GetUnitTestDBOptions());

            var commentRepository = new CommentRepository(context);
            var comment = new Comment { Id = 6, UserId = 1, ThreadId = 1, CreationDate = new DateTime(2022, 4, 20, 22, 25, 3), CommentText = "Comment1" };

            await commentRepository.AddAsync(comment);
            await context.SaveChangesAsync();

            Assert.That(context.Comments.Count(), Is.EqualTo(5), message: "AddAsync method works incorrect");
        }

        [Test]
        public async Task CommentRepository_DeleteByIdAsync_DeletesEntity()
        {
            using var context = new ForumDbContext(UnitTestHelper.GetUnitTestDBOptions());

            var commentRepository = new CommentRepository(context);

            await commentRepository.DeleteByIdAsync(1);
            await context.SaveChangesAsync();

            Assert.That(context.Comments.Count(), Is.EqualTo(3), message: "DeleteByIdAsync works incorrect");
        }

        [Test]
        public async Task CommentRepository_Update_UpdatesEntity()
        {
            using var context = new ForumDbContext(UnitTestHelper.GetUnitTestDBOptions());

            var commentRepository = new CommentRepository(context);
            var comment = new Comment { Id = 1, UserId = 2, ThreadId = 2, CreationDate = new DateTime(1,1,1), CommentText = "CommentTest" };


            commentRepository.Update(comment);
            await context.SaveChangesAsync();

            Assert.That(comment, Is.EqualTo(new Comment { Id = 1, UserId = 2, ThreadId = 2, CreationDate = new DateTime(1, 1, 1), CommentText = "CommentTest" })
            .Using(new CommentEqualityComparer()), message: "Update method works incorrect");
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task CommentRepository_GetByIdWithDetailsAsync_ReturnsWithIncludedEntities(int id)
        {
            using var context = new ForumDbContext(UnitTestHelper.GetUnitTestDBOptions());

            var commentRepository = new CommentRepository(context);

            var comment = await commentRepository.GetByIdWithDetailsAsync(id);

            var expected = ExpectedComments.FirstOrDefault(x => x.Id == id);

            Assert.That(comment,
                Is.EqualTo(expected).Using(new CommentEqualityComparer()), message: "GetByIdWithDetailsAsync method works incorrect");

            Assert.That(comment.Owner,
                Is.EqualTo(ExpectedUsers.FirstOrDefault(u=>u.Id == expected.UserId)).Using(new UserEqualityComparer()), message: "GetByIdWithDetailsAsync method doesnt't return included entities");
        }

        [Test]
        public async Task CommentRepository_GetAllWithDetailsAsync_ReturnsWithIncludedEntities()
        {
            using var context = new ForumDbContext(UnitTestHelper.GetUnitTestDBOptions());

            var commentRepository = new CommentRepository(context);

            var comments = await commentRepository.GetAllWithDetailsAsync();

            Assert.That(comments,
                Is.EqualTo(ExpectedComments).Using(new CommentEqualityComparer()), message: "GetAllWithDetailsAsync method works incorrect");

            Assert.That(comments.Select(c => c.Owner).Distinct().OrderBy(u => u.Id),
                Is.EqualTo(ExpectedUsers).Using(new UserEqualityComparer()), message: "GetByIdWithDetailsAsync method doesnt't return included entities");
        }




        private static IEnumerable<Comment> ExpectedComments =>
         new[]
         {
                new Comment { Id = 1, UserId = 1, ThreadId = 1, CreationDate = new DateTime(2022, 4, 20, 22, 25, 3), CommentText = "Comment1" },
                new Comment { Id = 2, UserId = 2, ThreadId = 1, CreationDate = new DateTime(2022, 4, 20, 22, 26, 20), CommentText = "Comment2" },
                new Comment { Id = 3, UserId = 1, ThreadId = 2, CreationDate = new DateTime(2022, 6, 21, 21, 10, 58), CommentText = "Comment3" },
                new Comment { Id = 5, UserId = 2, ThreadId = 2, CreationDate = new DateTime(2022, 6, 21, 21, 11, 5), CommentText = "Comment4"}
         };
        private static IEnumerable<User> ExpectedUsers =>
         new[]
         {
                new User { Id = 1, NickName = "Nick1", UserRoleId = 3, PersonId = 1, Login = "login1", Password = "password1" },
                new User { Id = 2, NickName = "Nick2", UserRoleId = 2, PersonId = 2, Login = "login2", Password = "password2" }
         };
    }
}
