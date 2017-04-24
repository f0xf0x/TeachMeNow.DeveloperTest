using System.Linq;

namespace TeachMeNow.DeveloperTest.BackEnd.Models {
    public class ClassViewModel: Class {
        public string StudentName { get; set; }

        public string TutorName { get; set; }

        public ClassViewModel(BackEndDB db, Class cl) {
            Id = cl.Id;
            StartTime = cl.StartTime;
            EndTime = cl.EndTime;
            Subject = cl.Subject;
            StudentId = cl.StudentId;
            TutorId = cl.TutorId;

            var tutorName = db.Users.SingleOrDefault(t => t.Id == cl.TutorId)?.Name;
            var studentName = db.Users.SingleOrDefault(t => t.Id == cl.StudentId)?.Name;
            TutorName = tutorName;
            StudentName = studentName;
        }
    }
}