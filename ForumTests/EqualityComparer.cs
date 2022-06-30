using Data.Entities;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ForumTests
{
    internal class CensorShipEqualityComparer : IEqualityComparer<CensorShip>
    {
        public bool Equals(CensorShip? x, CensorShip? y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null) 
                return false;

            return x.Id == y.Id
                && x.CensorStatus == y.CensorStatus;
        }

        public int GetHashCode([DisallowNull] CensorShip obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class TopicEqualityComparer : IEqualityComparer<Topic>
    {
        public bool Equals(Topic? x, Topic? y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id
                && x.TopicName == y.TopicName;
        }

        public int GetHashCode([DisallowNull] Topic obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class UserRoleEqualityComparer : IEqualityComparer<UserRole>
    {
        public bool Equals(UserRole? x, UserRole? y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id
                && x.RoleName == y.RoleName;
        }

        public int GetHashCode([DisallowNull] UserRole obj)
        {
            return obj.GetHashCode();
        }
    }
    internal class PersonEqualityComparer : IEqualityComparer<Person>
    {
        public bool Equals(Person? x, Person? y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id
                && x.BirthDate == y.BirthDate
                && x.PersonInfo == y.PersonInfo;
        }

        public int GetHashCode([DisallowNull] Person obj)
        {
            return obj.GetHashCode();
        }
    }
    internal class UserEqualityComparer : IEqualityComparer<User>
    {
        public bool Equals(User? x, User? y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id
                && x.NickName == y.NickName
                && x.UserRoleId == y.UserRoleId
                && x.PersonId == y.PersonId
                && x.Password == y.Password;
        }

        public int GetHashCode([DisallowNull] User obj)
        {
            return obj.GetHashCode();
        }
    }
    internal class ThreadEqualityComparer : IEqualityComparer<Thread_>
    {
        public bool Equals(Thread_? x, Thread_? y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id
                && x.UserId == y.UserId
                && x.TopicId == y.TopicId
                && x.CreationDate == y.CreationDate
                && x.Vievs == y.Vievs
                && x.CensorShipId == y.CensorShipId
                && x.ThreadTitle == y.ThreadTitle
                && x.ThreadBody == y.ThreadBody;
        }

        public int GetHashCode([DisallowNull] Thread_ obj)
        {
            return obj.GetHashCode();
        }
    }
    internal class CommentEqualityComparer : IEqualityComparer<Comment>
    {
        public bool Equals(Comment? x, Comment? y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id
                && x.UserId == y.UserId
                && x.ThreadId == y.ThreadId
                && x.CreationDate == y.CreationDate
                && x.CommentText == y.CommentText;
        }

        public int GetHashCode([DisallowNull] Comment obj)
        {
            return obj.GetHashCode();
        }
    }
}
