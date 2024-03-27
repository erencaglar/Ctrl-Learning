using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CtrlEdu.Models
{
    public class CourseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public UserModel? Instructor { get; set; }
        public int InstructorID { get; set; }
        public string Category { get; set; }
        public int EnrollmetCount { get; set; }
        public string? ImageURL { get; set; }
        public ICollection<EnrollmentModel>? Users { get; set; } = new HashSet<EnrollmentModel>();
        public ICollection<AssignmentModel>? Assignments { get; set; } = new HashSet<AssignmentModel>();

        public ICollection<LearningResourceModel>? LearningResources { get; set; } = new HashSet<LearningResourceModel>();

    }
}
