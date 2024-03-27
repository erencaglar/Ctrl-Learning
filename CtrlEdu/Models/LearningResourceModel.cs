using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;

namespace CtrlEdu.Models
{
    public class LearningResourceModel
    {
        [Key]
        public int ResourceID { get; set; }
        public CourseModel Course { get; set; }
        public int CourseID { get; set; }

        public string Title { get; set; }

        public string Type { get; set; } 

        public string Content { get; set; } 


    }
}
