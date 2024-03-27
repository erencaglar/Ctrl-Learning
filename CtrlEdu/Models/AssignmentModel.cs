using System.ComponentModel.DataAnnotations;

namespace CtrlEdu.Models
{
    public class AssignmentModel
    {
        [Key]
        public int AssignmentID { get; set; }
        public CourseModel Course { get; set; }
        public int CourseID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; } = DateTime.Now.ToUniversalTime();
        public ICollection<StudentCourseAssignmentModel> StudentCourseAssignments { get; set; } = new HashSet<StudentCourseAssignmentModel>();
    }
}