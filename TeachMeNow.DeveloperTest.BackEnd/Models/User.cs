using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace TeachMeNow.DeveloperTest.BackEnd.Models
{
    /// <summary>
    /// User model
    /// </summary>
    public class User
    {
        public User(ClaimsPrincipal principal) {
            this.Id = Convert.ToInt32(principal.Claims.Single(c => c.Type == nameof(Id)).Value);
            this.Name = principal.Claims.Single(c => c.Type == nameof(Name)).Value;
            this.Email = principal.Claims.Single(c => c.Type == nameof(Email)).Value;
            this.IsTutor = principal.Claims.Single(c => c.Type == "role").Value == "tutor";
        }

        public User() {
        }

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
    }
}