using CtrlEdu.Data;
using CtrlEdu.Data.DTO_s;
using CtrlEdu.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Security.Claims;

namespace CtrlEdu.Controllers
{


    
    public class UserController : Controller
    {

        ApplicationDbContext _context;
        public UserController(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        public IActionResult Index()
        {
            var data = _context.Users.ToList();
            return View(data);
        }



        public IActionResult Enroll(int id)
        {
            var enroll = _context.Courses.FirstOrDefault(c => c.CourseID == id);
            EnrollmentDto enrollmentDto = new EnrollmentDto { CourseID = id, CourseName = enroll.Title };
            return View(enrollmentDto);
        }

        [HttpPost]
        public IActionResult Enroll(EnrollmentDto enroll)
        {
            var User = _context.Users.FirstOrDefault(u => u.UserID == enroll.UserID);
            if (User == null)
            {
                return RedirectToAction("Index");
            }
            var Course = _context.Courses.FirstOrDefault(c => c.CourseID == enroll.CourseID);
            if (Course == null)
            {
                return RedirectToAction("Index");
            }
            var user = User.Courses.FirstOrDefault(c => c.CourseID == Course.CourseID);
            User.Courses.Add(new EnrollmentModel { UserID = User.UserID, CourseID = Course.CourseID });
            _context.SaveChanges();
            addAssignmentToUsers(enroll.CourseID);
            //INCLUDE EDİNCE ENROLLMENT DATE DE GELİYOR
            var included = _context.Users.Where(u => u.UserID == enroll.UserID)
                .Include(u => u.Courses).ToList();
            for (int i = 0; i < included.Count; i++)
            {
                Console.WriteLine(included[i]);
            }
            return RedirectToAction("Detail", "Course", new { id = enroll.CourseID });

        }

        //ASSIGNMENT
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
                        usertoadd.MyProperty.Add(new StudentCourseAssignmentModel { Assignment = assignment, AssignmentID = assignment.AssignmentID, StudentID = usertoadd.UserID, Status = false, Title = assignment.Title, Description = assignment.Description });

                    }
                }
            }
            _context.SaveChanges();
        }

        public IActionResult Check(int id)
        {
            
            var i = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (i != null)
            {
                var user = _context.Users.Include(u => u.MyProperty).FirstOrDefault(u => u.UserID == int.Parse(i));
                user.MyProperty.FirstOrDefault(scam => scam.StudentCourseAssignmentID == id).Status = true;
                _context.SaveChanges();

            }
            return RedirectToAction("Index", "Course");
           // return RedirectToAction("Detail", "Course");
            
        }

        [Authorize(Roles = "admin,Teacher")]
        //get :users/create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        //create user
        public ActionResult Create(UserModel usermodel)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(usermodel);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(usermodel);

        }
        //get user detail
        public ActionResult Details(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }



        //edit user
        public IActionResult Edit(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(int id, UserModel usermodel)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.Find(id);
                if (user == null)
                {
                    return NotFound();
                }
                user.UserName = usermodel.UserName;
                user.Email = usermodel.Email;
                user.Password = usermodel.Password;
                user.Rol = usermodel.Rol;
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            return View(usermodel);
        }

        //User delete
        public IActionResult Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            _context.Users.Remove(user);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}


