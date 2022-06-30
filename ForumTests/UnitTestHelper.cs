using Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using AutoMapper;
using Data.Entities;
namespace Library.Tests
{
    internal static class UnitTestHelper
    {
        public static DbContextOptions<ForumDbContext> GetUnitTestDBOptions() {
            var options = new DbContextOptionsBuilder<ForumDbContext>().
                UseInMemoryDatabase(Guid.NewGuid().ToString()).
                Options;

            using (var context = new ForumDbContext(options)) {
                SeedData(context);
            }

            return options;
        }

        /* public static IMapper CreateMapperProfile() {
             var myProfile = new AutoMapperProfile;
             var configuration = new MapperConfiguration(cfg => cfg.AddProfile(MyProfile));

             return new Mapper(configuration);
         }*/
        //W8 for creating mapper in bll

        public static void SeedData(ForumDbContext context)
        {
            context.Persons.AddRange(
                new Person { Id = 1, BirthDate = new DateTime(2001, 4, 20), PersonInfo = "Info1" },
                new Person { Id = 2, BirthDate = new DateTime(2000, 3, 3), PersonInfo = "Info1" }
                );
            context.UserRoles.AddRange(
                new UserRole { Id = 1, RoleName = "Role1" },
                new UserRole { Id = 2, RoleName = "Role2" },
                new UserRole { Id = 3, RoleName = "Role3" }
                );
            context.Topics.AddRange(
                new Topic { Id = 1, TopicName = "Topic1" },
                new Topic { Id = 2, TopicName = "Topic2" },
                new Topic { Id = 3, TopicName = "Topic3" }
                );
            context.CensorShips.AddRange(
                new CensorShip { Id = 1, CensorStatus = "CensorStatus1" },
                new CensorShip { Id = 2, CensorStatus = "CensorStatus2" },
                new CensorShip { Id = 3, CensorStatus = "CensorStatus3" }
                );
            context.Users.AddRange(
                new User { Id = 1, NickName = "Nick1", UserRoleId = 3, PersonId = 1, Login = "login1", Password = "password1" },
                new User { Id = 2, NickName = "Nick2", UserRoleId = 2, PersonId = 2, Login = "login2", Password = "password2" }
                );
            context.Threads.AddRange(
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
                );

            context.Comments.AddRange(
                new Comment { Id = 1, UserId = 1, ThreadId = 1, CreationDate = new DateTime(2022, 4, 20, 22, 25, 3), CommentText = "Comment1" },
                new Comment { Id = 2, UserId = 2, ThreadId = 1, CreationDate = new DateTime(2022, 4, 20, 22, 26, 20), CommentText = "Comment2" },
                new Comment { Id = 3, UserId = 1, ThreadId = 2, CreationDate = new DateTime(2022, 6, 21, 21, 10, 58), CommentText = "Comment3" },
                new Comment { Id = 5, UserId = 2, ThreadId = 2, CreationDate = new DateTime(2022, 6, 21, 21, 11, 5), CommentText = "Comment4"}
                ) ;

            context.SaveChanges();
        }

    }


}