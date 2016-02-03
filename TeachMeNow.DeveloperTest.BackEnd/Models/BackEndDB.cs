using NMemory;
using NMemory.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NMemory.Indexes;

namespace TeachMeNow.DeveloperTest.BackEnd.Models
{
    public class BackEndDB : Database
    {


        public BackEndDB()
        {
            var userTable = base.Tables.Create<User, int>(u => u.Id, new IdentitySpecification<User>(u => u.Id));
            var classTable = base.Tables.Create<Class, int>(c => c.Id, new IdentitySpecification<Class>(u => u.Id));

            var userClassIdIndex = userTable.CreateIndex(
                new RedBlackTreeIndexFactory(),
                p => p.Id);


            this.Tables.CreateRelation<User, int, Class, int>(
                userTable.PrimaryKeyIndex,
                classTable.CreateIndex(new RedBlackTreeIndexFactory(), x => x.Student),
                x => x, x => x, new RelationOptions());

            this.Users = userTable;
            this.Classes = classTable;


            //Seed database with Users
            //Students
            this.Users.Insert(
                new User
                {
                    Name = "Student 1",
IsTutor = false
                });
            this.Users.Insert(
                new User
                {
                    Name = "Student 2",
                    IsTutor = false
                });

            //Tutors
            this.Users.Insert(
                new User
                {
                    Name = "Tutor 1",
                    IsTutor = true
                });

            this.Users.Insert(
                new User
                {
                    Name = "Tutor 2",
                    IsTutor = true
                });
        }

        public ITable<User> Users { get; private set; }

        public ITable<Class> Classes { get; private set; }
    }
}