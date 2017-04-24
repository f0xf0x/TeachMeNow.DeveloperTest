using System.Threading.Tasks;
using System.Web.Http;

using Microsoft.AspNet.Identity;

using TeachMeNow.DeveloperTest.BackEnd.Models;
using TeachMeNow.DeveloperTest.BackEnd.Security;

namespace TeachMeNow.DeveloperTest.BackEnd.Controllers {
    /// <summary>
    /// Account controller to provide user registration funcionality
    /// </summary>
    [RoutePrefix("api/Account")]
    public class AccountController: ApiController {
        private AuthRepository _repo = null;

        public AccountController(BackEndDB database) {
            _repo = new AuthRepository(database);
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(AppUserModel userModel) {
            if(!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            IdentityResult result = await _repo.RegisterUser(userModel);

            IHttpActionResult errorResult = GetErrorResult(result);

            if(errorResult != null) {
                return errorResult;
            }

            return Ok();
        }

        private IHttpActionResult GetErrorResult(IdentityResult result) {
            if(result == null) {
                return InternalServerError();
            }

            if(!result.Succeeded) {
                if(result.Errors != null) {
                    foreach(string error in result.Errors) {
                        ModelState.AddModelError("", error);
                    }
                }

                if(ModelState.IsValid) {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}