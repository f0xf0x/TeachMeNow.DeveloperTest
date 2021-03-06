﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

using TeachMeNow.DeveloperTest.BackEnd.Models;

namespace TeachMeNow.DeveloperTest.BackEnd.Controllers {
    /// <summary>
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class UsersController : BaseApiController {

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController" /> class.
        /// </summary>
        /// <param name="database">The database.</param>
        public UsersController(IBackendDb database) : base(database) {
        }

        /// <summary>
        /// Gets a list of Users.
        /// </summary>
        /// <returns>IEnumerable&lt;User&gt;</returns>
        public async Task<IHttpActionResult> Get([FromUri] bool onlyPartners = false) {
            IEnumerable<User> users = db.Users;
            if (onlyPartners) {
                users = users.Where(t => t.IsTutor != currentUser.IsTutor);
            }
            return Ok(users.AsEnumerable());
        }

        /// <summary>
        /// Gets the specified user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Get(int id) {
            User user = db.Users.SingleOrDefault(x => x.Id == id);
            if (user == default(User)) {
                return NotFound();
            }
            return Ok(user);
        }
    }
}