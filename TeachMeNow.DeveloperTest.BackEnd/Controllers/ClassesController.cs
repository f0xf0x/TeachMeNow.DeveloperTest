using System;
using System.Collections.Generic;
using System.Web.Http;
using TeachMeNow.DeveloperTest.BackEnd.Models;

namespace TeachMeNow.DeveloperTest.BackEnd.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class ClassesController : ApiController
    {
        BackEndDB _database = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassesController"/> class.
        /// </summary>
        /// <param name="database">The database.</param>
        public ClassesController(BackEndDB database)
        {
            _database = database;
        }
        // GET: api/Classes
        /// <summary>
        /// Gets all the classes.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IEnumerable<Class> Get()
        {
            throw new NotImplementedException();
        }

        // GET: api/Classes/5
        /// <summary>
        /// Gets the specified class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Class Get(int id)
        {
            throw new NotImplementedException();
        }

        // POST: api/Classes
        /// <summary>
        /// Adds the Class to the database.
        /// </summary>
        /// <param name="newClass">The new class.</param>
        public void Post([FromBody]Class newClass)
        {
        }

        // PUT: api/Classes/5
        /// <summary>
        /// Updates the specified Class in the database.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updateClass">The update class.</param>
        public void Put(int id, [FromBody]Class updateClass)
        {
        }

        // DELETE: api/Classes/5
        /// <summary>
        /// Deletes the specified Class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
        }
    }
}
