using System;
using System.Linq;
using System.Security.Claims;

namespace TeachMeNow.DeveloperTest.BackEnd.Models {
    /// <summary>
    /// User model
    /// </summary>
    public class User {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is tutor.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is tutor; otherwise, <c>false</c>.
        /// </value>
        public bool IsTutor { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public User(ClaimsPrincipal principal) {
            string claimsId = principal.Claims.SingleOrDefault(c => c.Type == nameof(Id))?.Value;
            Id = Convert.ToInt32(claimsId);
            Name = principal.Claims.SingleOrDefault(c => c.Type == nameof(Name))?.Value;
            Email = principal.Claims.SingleOrDefault(c => c.Type == nameof(Email))?.Value;
            IsTutor = principal.Claims.SingleOrDefault(c => c.Type == "role")?.Value == "tutor";
        }

        public User() {
        }
    }
}