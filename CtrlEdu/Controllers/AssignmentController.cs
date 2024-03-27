using CtrlEdu.Data;
using CtrlEdu.Data.DTO_s;
using CtrlEdu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;

namespace CtrlEdu.Controllers
{
    public class AssignmentController : Controller
    {
        ApplicationDbContext _context;
        public AssignmentController(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Delete(int id)
        {
            _context.Assignments.FirstOrDefault(a => a.AssignmentID == id);
            return View();
        }
        public IActionResult Create(int id)
        {
            AssignmentDTO assignmentDTO = new AssignmentDTO { CourseID = id };
            return View(assignmentDTO);
        }
        private void addAssignmentToUsers(int courseid)
        {
            //ödevi eklediğimiz kurs
            CourseModel course = _context.Courses.Include(c => c.Assignments).Include(c => c.Users).FirstOrDefault(c => c.CourseID == courseid);
            //bütün kullanıcıların listesi
            var users = _context.Users.Include(u => u.MyProperty);

            //kursun bütün ödevlerini dön
            foreach (var assignment in course.Assignments)
            {
                //kursun bütün kullanıcılarını dön
                foreach (var user in course.Users)
                {
                    //bütün kullanıcılar içinden ekleme yapılması gereken kullanıcıyı bul
                    var usertoadd = users.FirstOrDefault(u => u.UserID == user.UserID);
                    //assignment id yok ise ekle
                    if (!usertoadd.MyProperty.Any(scam => scam.AssignmentID == assignment.AssignmentID))
                    {
                        // Veriyi koleksiyona ekler
                        usertoadd.MyProperty.Add(new StudentCourseAssignmentModel { Assignment=assignment, AssignmentID = assignment.AssignmentID, StudentID = usertoadd.UserID, Status = false, Title = assignment.Title, Description = assignment.Description });

                    }
                }
            }
            _context.SaveChanges();
        }
        [HttpPost]
        public IActionResult Create(AssignmentDTO assignment)
        {
            CourseModel course = _context.Courses.Include(c => c.Assignments).Include(c => c.Users).FirstOrDefault(c => c.CourseID == assignment.CourseID);

            course.Assignments.Add(new AssignmentModel { CourseID = assignment.CourseID, Description = assignment.Description, Title = assignment.Title });
            _context.SaveChanges();
            addAssignmentToUsers(assignment.CourseID);
            return RedirectToAction("Detail", "Course", new { id = assignment.CourseID }); ;
        }
    }
}