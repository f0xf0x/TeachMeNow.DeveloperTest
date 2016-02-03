using System;
using System.ComponentModel.DataAnnotations;

namespace TeachMeNow.DeveloperTest.BackEnd.Models
{
    /// <summary>
    /// Class contains the details of a lesson between a tutor and student
    /// </summary>
    public class Class
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>
        /// The subject.
        /// </value>
        [Required]
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        /// <value>
        /// The start time.
        /// </value>
        [Required]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Gets or sets the end time.
        /// </summary>
        /// <value>
        /// The end time.
        /// </value>
        [Required]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Gets or sets the student.
        /// </summary>
        /// <value>
        /// The student.
        /// </value>
        [Required]
        public int Student { get; set; }

        /// <summary>
        /// Gets or sets the tutor.
        /// </summary>
        /// <value>
        /// The tutor.
        /// </value>
        [Required]
        public int Tutor { get; set; }
    }
}