using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace a3_seantrudeln01525609_http5112webdev1c.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
            //Teacher database website homepage
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Teacher/List
            //Displays list of teacher information from Teacher Database
        public ActionResult List()
        {
            return View();
        }

        // POST: /Teacher/Show
            //Displays specific teacher information afterusing search function on /Teacher/List page
        [HttpPost]
        public ActionResult Show(string teacherFName, string teacherLName, int teacherHireDate, int teacherSalary)
        {
            //Debug.WrtieLine();

            ViewData["TeacherName"] = teacherFName + " " + teacherLName;
            ViewData["teacherHireDate"] = teacherHireDate;
            ViewData["teacherSalary"] = teacherSalary;

            return View();
        }
    }
}