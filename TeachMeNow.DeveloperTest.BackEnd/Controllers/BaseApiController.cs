using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http;
using System.Web.Http.ModelBinding;

using TeachMeNow.DeveloperTest.BackEnd.Models;

namespace TeachMeNow.DeveloperTest.BackEnd.Controllers {
    public class BaseApiController: ApiController {
        protected User currentUser {
            get {
                IPrincipal principal = Request.GetRequestContext().Principal;
                ClaimsPrincipal claimsPrincipal = principal as ClaimsPrincipal;
                return new User(claimsPrincipal);
            }
        }

        /// <summary>
        /// ModelState.AddModelError wrapper
        /// </summary>
        /// <param name="name">propery name</param>
        /// <param name="message">message to client</param>
        protected void addModelError(string name, string message) {
            ModelState.AddModelError(name, new ArgumentOutOfRangeException(name, message));
        }

        /// <summary>
        /// Workaround for CORS
        /// </summary>
        /// <returns>Options stub</returns>
        public HttpResponseMessage Options() {
            return new HttpResponseMessage {
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}