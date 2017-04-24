using System;

using NMemory;
using NMemory.Indexes;
using NMemory.Tables;

namespace TeachMeNow.DeveloperTest.BackEnd.Models {
    public class BackEndDB: Database, IBackEndDb {
        public ITable<User> Users { get; private set; }

        public ITable<Class> Classes { get; private set; }

        public BackEndDB() {
            var userTable = Tables.Create<User, int>(u => u.Id, new IdentitySpecification<User>(u => u.Id));
            var classTable = Tables.Create<Class, int>(c => c.Id, new IdentitySpecification<Class>(u => u.Id));

            var userClassIdIndex = userTable.CreateIndex(
                new RedBlackTreeIndexFactory(),
                p => p.Id);

            Tables.CreateRelation<User, int, Class, int>(
                userTable.PrimaryKeyIndex,
                classTable.CreateIndex(new RedBlackTreeIndexFactory(), x => x.StudentId),
                x => x, x => x, new RelationOptions());

            Users = userTable;
            Classes = classTable;

            //Seed database with Users
            //Users
            var user1 = new User {
                Name = "User 1",
                Email = "user1@example.com",
                Password = "password1",
                IsTutor = false
            };
            Users.Insert(
                user1);
            var user2 = new User {
                Name = "User 2",
                Email = "user2@example.com",
                Password = "password2",
                IsTutor = false
            };
            Users.Insert(
                user2);

            //Tutors
            var tutor1 = new User {
                Name = "Tutor 1",
                Email = "tutor1@example.com",
                Password = "pass_t1",
                IsTutor = true
            };
            Users.Insert(
                tutor1);

            var tutor2 = new User {
                Name = "Tutor 2",
                Email = "tutor2@example.com",
                Password = "pass_t2",
                IsTutor = true
            };
            Users.Insert(
                tutor2);

            Classes.Insert(new Class() {
                Subject = "Math - Lesson 1",
                StudentId = user1.Id,
                TutorId = tutor1.Id,
                StartTime = DateTime.Now.AddDays(1),
                EndTime = DateTime.Now.AddDays(1).AddHours(1)
            });
            Classes.Insert(new Class() {
                Subject = "Math - Lesson 2",
                StudentId = user1.Id,
                TutorId = tutor1.Id,
                StartTime = DateTime.Now.AddDays(2),
                EndTime = DateTime.Now.AddDays(2).AddHours(2)
            });

            Classes.Insert(new Class() {
                Subject = "Physics - Lesson 7",
                StudentId = user2.Id,
                TutorId = tutor2.Id,
                StartTime = DateTime.Now.AddDays(2),
                EndTime = DateTime.Now.AddDays(2).AddHours(1)
            });
            Classes.Insert(new Class() {
                Subject = "Physics - Lesson 8",
                StudentId = user2.Id,
                TutorId = tutor2.Id,
                StartTime = DateTime.Now.AddDays(3),
                EndTime = DateTime.Now.AddDays(3).AddHours(1)
            });
        }
    }
}