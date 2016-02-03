using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeachMeNow.DeveloperTest.BackEnd.Models
{
    public class Class
    {
        public int Id { get; set; }

        public string Subject { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public List<User> Users { get; set; }
    }
}