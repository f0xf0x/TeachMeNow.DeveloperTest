using System.Collections.Generic;
using System.Security.Claims;

namespace TeachMeNow.DeveloperTest.BackEnd.Models {
    public static class Exts {
        public static List<Claim> GetClaims(this User user) {
            
            var claims = new List<Claim>();
            claims.Add(new Claim(nameof(user.Name), user.Name));
            claims.Add(new Claim(nameof(user.Email), user.Email));
            claims.Add(new Claim(nameof(User.Id), user.Id.ToString()));
            claims.Add(new Claim("role", user.IsTutor ? "tutor" : "student"));
            return claims;
        }

        public static ClaimsIdentity GetIdentity(this User user) {
            
            var claims = user.GetClaims();
            var identity = new ClaimsIdentity(claims);
            return identity;
        }
        }
}