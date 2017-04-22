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
    public class UsersController : BaseApiController
    {
        BackEndDB db = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="database">The database.</param>
        public UsersController(BackEndDB database)
        {
            db = database;
        }

        /// <summary>
        /// Gets a list of Users.
        /// </summary>
        /// <returns>IEnumerable&lt;User&gt;</returns>
        public IEnumerable<User> Get([FromUri]bool onlyPartners = false)
        {
            if(onlyPartners) {
                return db.Users.Where(t => t.IsTutor != currentUser.IsTutor).AsEnumerable();
            }
            return db.Users.AsEnumerable();
        }

        /// <summary>
        /// Gets the specified user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public User Get(int id)
        {
            return db.Users.FirstOrDefault(x => x.Id == id);
        }
    }

}
