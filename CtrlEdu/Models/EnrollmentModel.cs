    using System.ComponentModel.DataAnnotations;

    namespace CtrlEdu.Models
    {
        //cross table inshAllah
        public class EnrollmentModel
        {
            public UserModel User { get; set; }
            public int UserID { get; set; }
            public CourseModel Course { get; set; }
            public int CourseID { get; set; }
            public DateTime EnrollmentDate { get; set; } = DateTime.Now.ToUniversalTime();

        public ICollection<UserModel> Users { get; set; } = new List<UserModel>();
    }

    }
