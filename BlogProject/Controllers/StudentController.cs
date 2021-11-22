using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogProject.Models;

namespace BlogProject.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student/List
        [HttpGet]
        public ActionResult List()
        {
            //this class will help us gather information from the db
            StudentDataController Controller = new StudentDataController();
            IEnumerable<Student> Students = (IEnumerable<Student>)Controller.ListStudents();
            return View(Students);
        }

        // GET : Student/Show/{id}
        [HttpGet]
        [Route("Student/Show/{id}")]
        public ActionResult Show(int id)
        {
            StudentDataController Controller = new StudentDataController();
            Student SelectedStudent = Controller.FindStudent(id);
            return View(SelectedStudent);
        }
    }
}