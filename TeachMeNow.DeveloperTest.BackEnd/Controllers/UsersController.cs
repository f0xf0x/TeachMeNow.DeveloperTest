using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TeachMeNow.DeveloperTest.BackEnd.Models;

namespace TeachMeNow.DeveloperTest.BackEnd.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class UsersController : ApiController
    {
        BackEndDB _database = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="database">The database.</param>
        public UsersController(BackEndDB database)
        {
            _database = database;
        }

        /// <summary>
        /// Gets a list of Users.
        /// </summary>
        /// <returns>IEnumerable&lt;User&gt;</returns>
        public IEnumerable<User> Get()
        {
            return _database.Users.AsEnumerable();
        }

        /// <summary>
        /// Gets the specified user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public User Get(int id)
        {
            return _database.Users.FirstOrDefault(x => x.Id == id);
        }
    }

}
