using CtrlEdu.Data;
using CtrlEdu.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


namespace CtrlEdu.Controllers
{
    public class CourseController : Controller
    {
        ApplicationDbContext _context;
        public CourseController(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        public IActionResult Index()
        {
            var courses = _context.Courses.Include(c => c.Users).ToList();
            return View(courses);
        }
        public IActionResult Detail(int id)
        {
            var i = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //UserModel currentUser = _context.Users.FirstOrDefault(u=>u.UserID == int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value)));
            CourseModel Course = _context.Courses.Include(c => c.Assignments).Include(c => c.Users).FirstOrDefault(c => c.CourseID == id);
            //giriş yapılmışsa ve giriş yapan kullanıcı kursa üye ise
            if (i != null && Course.Users.FirstOrDefault(u => u.UserID == int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value)) != null)
            {
                UserModel currentUser = _context.Users.Include(u => u.MyProperty).FirstOrDefault(u => u.UserID == int.Parse(i));
                ViewBag.CurrentUserAssignments = currentUser.MyProperty.Where(p => p.Assignment.CourseID == id);
                
            }
            else
            {
                ViewBag.CurrentUserAssignments = new HashSet<StudentCourseAssignmentModel>();
            }
           
            return View(Course);
        }
        [Authorize(Roles = "admin,Teacher")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CourseModel courseModel)
        {
            if (!ModelState.IsValid)
            {
                return View(courseModel);
            }
           
            _context.Courses.Add(courseModel);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        //edit course
        public IActionResult Edit(int id)
        {
            var course = _context.Courses.Find(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        [HttpPost]
        // Edit course
        public ActionResult Edit(int id, CourseModel courseModel)
        {
            if (ModelState.IsValid)
            {
                var course = _context.Courses.Find(id);
                if (course == null)
                {
                    return NotFound();
                }

                course.Title = courseModel.Title;
                course.Description = courseModel.Description;
                course.Category = courseModel.Category;
                course.ImageURL = courseModel.ImageURL;

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(courseModel);
        }


        //delete course
        public IActionResult Delete(int id)
        {
            var course = _context.Courses.Find(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCourseConfirmed(int id)
        {
            var course = _context.Courses.Find(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
