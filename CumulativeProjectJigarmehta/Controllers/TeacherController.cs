using CumulativeProjectJigarmehta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CumulativeProjectJigarmehta.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// This function returns list teachers
        /// </summary>
        /// <param name="SearchKey"></param>
        /// <returns>list of Teachers</returns>
        //GET : /Teachers/List
        public ActionResult List(string SearchKey = null)
        {

            TeachersController controller = new TeachersController();
            IEnumerable<Teacher> Teachers =  controller.ListTeachers(SearchKey);
            return View(Teachers);
        }


        /// <summary>
        /// rThis function returns multiple details of teachers.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>details of NewTeacher</returns>
        //GET: /Teacher/Show/{id}

        public ActionResult Show(int id)
        {
            TeachersController controller = new TeachersController();
            Teacher NewTeacher = controller.FindTeacher(id);
            return View(NewTeacher);
        }
    }
}