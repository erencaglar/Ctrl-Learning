using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CtrlEdu.Models
{
    public class UserModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }
        [Display(Name = "Username")]
        public string UserName { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        public string Password { get; set; }
        [Display(Name = "Password")]
        public string Rol { get; set; }

        public ICollection<EnrollmentModel> Courses { get; set; } = new HashSet<EnrollmentModel>();
        public ICollection<StudentCourseAssignmentModel> MyProperty { get; set; } = new HashSet<StudentCourseAssignmentModel>();


    }
}
