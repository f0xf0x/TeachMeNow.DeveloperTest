using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http;

using TeachMeNow.DeveloperTest.BackEnd.Models;

namespace TeachMeNow.DeveloperTest.BackEnd.Controllers {
    public class BaseApiController: ApiController {
        protected readonly IBackendDb db;
        private ClaimsPrincipal claimsPrincipal;

        public BaseApiController(IBackendDb db) {
            this.db = db;
        }

        public ClaimsPrincipal RequestPrincipal {
            get {
                if(claimsPrincipal == null) {
                    
                IPrincipal principal = Request.GetRequestContext().Principal;
                 claimsPrincipal = principal as ClaimsPrincipal;
                }
                return claimsPrincipal;
            }
            set {
                claimsPrincipal = value;
            }
        }

        protected User currentUser {
            get {
                return new User(RequestPrincipal);
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