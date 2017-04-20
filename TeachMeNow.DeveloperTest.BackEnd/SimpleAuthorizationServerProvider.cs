using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.Owin.Security.OAuth;

using TeachMeNow.DeveloperTest.BackEnd.Models;

namespace TeachMeNow.DeveloperTest.BackEnd {
    public class SimpleAuthorizationServerProvider: OAuthAuthorizationServerProvider {
        private readonly BackEndDB db;

        public SimpleAuthorizationServerProvider(BackEndDB db) {
            this.db = db;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context) {
            var user = db.Users.SingleOrDefault(f => f.Email == context.UserName && f.Password == context.Password);
            if(user == default(User)) {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(nameof(user.Name), user.Name));
            identity.AddClaim(new Claim(nameof(user.Email), user.Email));
            identity.AddClaim(new Claim(nameof(User.Id),user.Id.ToString()));
            identity.AddClaim(new Claim("role", user.IsTutor?"tutor":"student"));

            context.Validated(identity);
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context) {
            context.Validated();
        }
    }
}