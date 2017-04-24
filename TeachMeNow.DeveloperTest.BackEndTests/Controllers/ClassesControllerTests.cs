using Microsoft.VisualStudio.TestTools.UnitTesting;
using TeachMeNow.DeveloperTest.BackEnd.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;

using Moq;

using TeachMeNow.DeveloperTest.BackEnd.Models;

namespace TeachMeNow.DeveloperTest.BackEnd.Controllers.Tests {
    [TestClass()]
    public class ClassesControllerTests {
        BackendDb db;
        private User student;

        [TestMethod()]
        public async Task PostTestEndDateWrong() {
            db.Classes.Insert(new Class() {
                StudentId = 1,
                TutorId = 2,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(1)
            });
            var ctrl = new ClassesController(db);

            ctrl.RequestPrincipal = new ClaimsPrincipal(student.GetIdentity());
            var result = await ctrl.Post(new Class() {
                Subject = "test",
                StudentId = 1,
                TutorId = 2,
                StartTime = DateTime.Now.AddMinutes(30),
                EndTime = DateTime.Now.AddHours(1).AddMinutes(30)
            });
            Assert.IsInstanceOfType(result, typeof(InvalidModelStateResult));
        }


        [TestMethod()]
        public async Task PostTestStartDateWrong() {
            db.Classes.Insert(new Class() {
                StudentId = 1,
                TutorId = 2,
                StartTime = DateTime.Now.AddMinutes(30),
                EndTime = DateTime.Now.AddHours(1).AddMinutes(30)
            });
            var ctrl = new ClassesController(db);

            ctrl.RequestPrincipal = new ClaimsPrincipal(student.GetIdentity());
            var result = await ctrl.Post(new Class() {
                Subject = "test",
                StudentId = 1,
                TutorId = 2,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(1)
            });
            Assert.IsInstanceOfType(result, typeof(InvalidModelStateResult));
        }


        [TestInitialize]
        public void Initialize() {
            db = new BackendDb(true);
            student = new User {
                Id = 1,
                Name = "test",
                Email = "example@example.com",
                IsTutor = false
            };
            db.Users.Insert(student);
            db.Users.Insert(new User {
                Id = 2,
                Email = "example@example.com",
                Name = "tutor test",
                IsTutor = true
            });
        }
    }
}