using CtrlEdu.Data;
using CtrlEdu.Data.DTO_s;
using CtrlEdu.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace CtrlEdu.Controllers
{

   
    [Authorize(Roles = "admin,Teacher")]
    public class EnrollmentController : Controller
    {

        ApplicationDbContext _context;
        public EnrollmentController(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        public IActionResult Index(int courseId)
        {
            ViewBag.CourseId = courseId;

            var enrollmentsForCourse = _context.Enrollments
                .Where(e => e.CourseID == courseId)
                .Include(e => e.User)
                .ToList();

            return View(enrollmentsForCourse);
        }

        public IActionResult DisplayCourse(int courseId)
        {
            // Retrieve course information and other data
            // ...

            return View("CourseView", courseId);
        }

        [HttpGet]
        public IActionResult Enroll2(int? courseId)
        {

            var currentCourse = _context.Courses.FirstOrDefault(c => c.CourseID == courseId);

            if (currentCourse == null)
            {
                // Handle the case where the course is not found
                return RedirectToAction("Index");
            }

            ViewBag.Users = _context.Users.ToList();
            ViewBag.CurrentCourse = currentCourse;
            return View();
        }

        [HttpPost]
        public IActionResult Enroll2(int UserID, int CourseID)
        {
            // Retrieve the user and course based on the selected IDs
            var user = _context.Users.FirstOrDefault(u => u.UserID == UserID);
            var course = _context.Courses.FirstOrDefault(c => c.CourseID == CourseID);

            if (user == null || course == null)
            {
                // Handle the case where the user or course doesn't exist (e.g., display an error message)
                return RedirectToAction("Index");
            }

            // Check if the user is already enrolled in the course (you might want to add this check)
            if (user.Courses.Any(c => c.CourseID == course.CourseID))
            {
                // Handle the case where the user is already enrolled in the course (e.g., display an error message)
                return RedirectToAction("Index");
            }

            // Create a new enrollment record for the user and course
            var enrollment = new EnrollmentModel
            {
                UserID = user.UserID,
                CourseID = course.CourseID,
                EnrollmentDate = DateTime.Now.ToUniversalTime()
            };

            // Add the enrollment to the user's Courses collection
            user.Courses.Add(enrollment);

            // Save changes to the database within a transaction
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                // Handle the exception (e.g., log the error and display an error message)
                return RedirectToAction("Index");
            }

            // Redirect to "Index" or another appropriate action upon successful enrollment
            return RedirectToAction("Index", new { courseId = CourseID });

        }

        [HttpGet]
        public IActionResult DeEnroll(int UserID, int CourseID)
        {
            // Find the enrollment record based on the provided UserID and CourseID
            var enrollment = _context.Enrollments.FirstOrDefault(e => e.UserID == UserID && e.CourseID == CourseID);

            if (enrollment == null)
            {
                // Handle the case where the enrollment record is not found
                return RedirectToAction("Index");
            }

            // Remove the enrollment record from the database
            _context.Enrollments.Remove(enrollment);

            // Save changes to the database within a transaction
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                // Handle the exception (e.g., log the error and display an error message)
                return RedirectToAction("Index");
            }

            // Redirect to "Index" or another appropriate action upon successful de-enrollment
            return RedirectToAction("Index", new { courseId = CourseID });
        }

    }




}



