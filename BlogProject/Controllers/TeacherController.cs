using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogProject.Models;

namespace BlogProject.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Teacher/List
        [HttpGet]
        public ActionResult List(string SearchKey = null)
        {
            //this class will help us gather information from the db
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers(SearchKey);
            return View(Teachers);
        }

        // GET : /Teacher/Show/{id}
        [HttpGet]
        [Route("Teacher/Show/{id}")]
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);

            return View(NewTeacher);
        }
    }
}