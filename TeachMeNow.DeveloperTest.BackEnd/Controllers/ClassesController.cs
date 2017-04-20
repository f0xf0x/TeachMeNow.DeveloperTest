using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using TeachMeNow.DeveloperTest.BackEnd.Models;

namespace TeachMeNow.DeveloperTest.BackEnd.Controllers {
    /// <summary>
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class ClassesController: BaseApiController {
        BackEndDB db;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassesController" /> class.
        /// </summary>
        /// <param name="database">The database.</param>
        public ClassesController(BackEndDB database) {
            db = database;
        }

        // GET: api/Classes
        /// <summary>
        /// Gets all the classes.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IEnumerable<Class> Get() {
            return db.Classes.Where(t => t.StudentId == currentUser.Id);
        }

        // GET: api/Classes/5
        /// <summary>
        /// Gets the specified class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Class Get(int id) {
            return Get().SingleOrDefault(t => t.Id == id);
        }

        // POST: api/Classes
        /// <summary>
        /// Adds the Class to the database.
        /// </summary>
        /// <param name="newClass">The new class.</param>
        public IHttpActionResult Post([FromBody] Class newClass) {
            if(newClass == null) {
                ModelState.AddModelError(nameof(Class),new ArgumentNullException(nameof(Class),"Cannot create empty class"));
            }
            var tutorBusy = db.Classes.Any(t => t.TutorId == newClass.TutorId &&
                                                (
                                                    (t.StartTime > newClass.StartTime && t.StartTime < newClass.EndTime) ||
                                                    (t.EndTime > newClass.StartTime && t.EndTime < newClass.EndTime)
                                                ));
            if(tutorBusy) {
                ModelState.AddModelError(nameof(newClass.StartTime),new ArgumentOutOfRangeException(nameof(newClass.StartTime), "Cannot book a class, tutor is busy"));
            }

            var studentBusy = db.Classes.Any(t => t.StudentId == newClass.StudentId &&
                                                  (
                                                      (t.StartTime > newClass.StartTime && t.StartTime < newClass.EndTime) ||
                                                      (t.EndTime > newClass.StartTime && t.EndTime < newClass.EndTime)
                                                  ));
            if(studentBusy) {
                ModelState.AddModelError(nameof(newClass.StartTime), new ArgumentOutOfRangeException(nameof(newClass.StartTime), "Cannot book a class, student is busy"));
            }
            if(currentUser.IsTutor && newClass.TutorId > 0 && newClass.TutorId != currentUser.Id) {
                addModelError(nameof(newClass.TutorId), "Cannot book a class for different user");
            }
            if(!currentUser.IsTutor && newClass.StudentId > 0 && newClass.StudentId != currentUser.Id) {
                 ModelState.AddModelError(nameof(newClass.StudentId),new ArgumentOutOfRangeException(nameof(newClass.StudentId), "Cannot book a class for different user"));
            }

            if(currentUser.IsTutor) {
                if(!db.Users.Any(t => !t.IsTutor && t.Id == newClass.StudentId)) {
                     ModelState.AddModelError(nameof(newClass.StudentId),new ArgumentOutOfRangeException(nameof(newClass.StudentId), "Cannot book a class: student doesn't exists"));
                }
                newClass.TutorId = currentUser.Id;
            } else {
                if(!db.Users.Any(t => t.IsTutor && t.Id == newClass.TutorId)) {
                     ModelState.AddModelError(nameof(newClass.StudentId),new ArgumentOutOfRangeException(nameof(newClass.StudentId), "Cannot book a class: tutor doesn't exists"));
                }
                newClass.StudentId = currentUser.Id;
            }

            if(!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            db.Classes.Insert(newClass);
            return CreatedAtRoute("DefaultApi", new { id = newClass.Id }, newClass);

        }


        // PUT: api/Classes/5
        /// <summary>
        /// Updates the specified Class in the database.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updateClass">The update class.</param>
        public void Put(int id, [FromBody] Class updateClass) {
        }

        // DELETE: api/Classes/5
        /// <summary>
        /// Deletes the specified Class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id) {
        }
    }
}