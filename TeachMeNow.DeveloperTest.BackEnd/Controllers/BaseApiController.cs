using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

using TeachMeNow.DeveloperTest.BackEnd.Models;

namespace TeachMeNow.DeveloperTest.BackEnd.Controllers {
    public class BaseApiController:ApiController {

        protected User currentUser {
            get {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                return new User(principal);
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