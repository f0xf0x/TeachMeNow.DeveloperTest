using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeachMeNow.DeveloperTest.BackEnd.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class User
    {
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
    }
}