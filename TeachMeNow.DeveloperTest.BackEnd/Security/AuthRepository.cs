using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;

using TeachMeNow.DeveloperTest.BackEnd.Models;

namespace TeachMeNow.DeveloperTest.BackEnd.Security {
    public class AuthRepository {
        private readonly IBackendDb database;

        public AuthRepository(IBackendDb database) {
            this.database = database;
        }

        public async Task<IdentityResult> RegisterUser(AppUserModel model) {
            var entity = new User() {
                Name = model.UserName,
                IsTutor = model.IsTutor,
                Email = model.Email,
                Password = model.Password
            };
            try {
                database.Users.Insert(entity);

                return IdentityResult.Success;
            } catch(Exception ex) {
                var result = IdentityResult.Failed($"Cannot register user: {ex.Message}");

                return result;
            }
        }

        public async Task<User> FindUser(string email, string password) {
            var user = database.Users.SingleOrDefault(f => f.Email == email && f.Password == password);
            if(user == null) {
                throw new ArgumentNullException(nameof(user));
            }

            return user;
        }
    }
}