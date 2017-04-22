using System.Linq;

namespace TeachMeNow.DeveloperTest.BackEnd.Models {
    public class ClassViewModel: Class {
        public ClassViewModel(Class cl) {
            this.Id = cl.Id;
            this.StartTime = cl.StartTime;
            this.EndTime = cl.EndTime;
            this.Subject = cl.Subject;
            this.StudentId = cl.StudentId;
            this.TutorId = cl.TutorId;

            var db = new BackEndDB();
            var tutorName = db.Users.SingleOrDefault(t => t.Id == cl.TutorId)?.Name;
            var studentName = db.Users.SingleOrDefault(t => t.Id == cl.StudentId)?.Name;
            this.TutorName = tutorName;
            this.StudentName = studentName;
        }

        public string StudentName { get; set; }
        public string TutorName { get; set; }
    }
}