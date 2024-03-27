using System.ComponentModel.DataAnnotations;

namespace CtrlEdu.Models
{
    public class StudentCourseAssignmentModel
    {
        [Key]
        public int StudentCourseAssignmentID { get; set; }
        public UserModel Student { get; set; }
        public int StudentID { get; set; }
        public AssignmentModel Assignment { get; set; } = new AssignmentModel();
        public int AssignmentID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
    }
}