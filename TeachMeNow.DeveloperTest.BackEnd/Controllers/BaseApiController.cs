using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http;

using TeachMeNow.DeveloperTest.BackEnd.Models;

namespace TeachMeNow.DeveloperTest.BackEnd.Controllers {
    public class BaseApiController:ApiController {

        protected User currentUser {
            get {
                IPrincipal principal = Request.GetRequestContext().Principal;
                ClaimsPrincipal claimsPrincipal = principal as ClaimsPrincipal;
                return new User(claimsPrincipal);
            }
        }
        protected void addModelError(string name, string message) {
            ModelState.AddModelError(name, new ArgumentOutOfRangeException(name, message));
        }



        /// <summary>
        /// Workaround for CORS
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage Options() {
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
        }
    }
}