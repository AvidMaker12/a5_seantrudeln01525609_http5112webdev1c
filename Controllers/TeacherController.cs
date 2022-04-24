using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using a3_seantrudeln01525609_http5112webdev1c.Models;
using System.Diagnostics;   //Allows for debugging

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
        //Displays list of all teachers information from Teacher Database
        [Route("/Teacher/List/{SearchKey}")]
        public ActionResult List(string SearchKey)
        {
            //Debugging: check if key is collected
            Debug.WriteLine("Key: " + SearchKey);
            //TeacherDataController API instance
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers(SearchKey);

            //Routes all teacher info to List.cshtml
            return View(Teachers);
        }

        // GET: /Teacher/Show/{id}
        //Displays specific teacher information after using teacher name links on /Teacher/List page
        //TO DO: search function on /Teacher/List page
        [HttpGet]
        //[Route("/Teacher/Show/{TeacherId}")]
        public ActionResult Show(int id)
        //public ActionResult Show(string teacherFName, string teacherLName, int teacherHireDate, int teacherSalary)
        {
            //Debug.WriteLine(Show);

            /* //Code for future search tool to find and filter teachers from database by using form inputs
            ViewData["TeacherName"] = teacherFName + " " + teacherLName;
            ViewData["teacherHireDate"] = teacherHireDate;
            ViewData["teacherSalary"] = teacherSalary;
            */
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacherInfo = controller.FindTeacher(id);

            //Routes 1 selected teacher info to Show.cshtml
            return View(SelectedTeacherInfo);
        }

        //GET: /Teacher/New
        public ActionResult New()
        {
            return View();
        }

        //POST: /Teacher/Create
        [HttpPost]
        public ActionResult Create(string teacherfname, string teacherlname, string salary)
        {
            Debug.WriteLine("Teacher info: " + teacherfname + " " + teacherlname + " " + salary);

            Teacher NewTeacher = new Teacher(); //New instance of Teacher class
            NewTeacher.TeacherFName = teacherfname;
            NewTeacher.TeacherLName = teacherlname;
            NewTeacher.Salary = salary;

            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);

            //Redirect immediately to List view
            return RedirectToAction("List");

            //TO BE ADDED LATER: Redirect to show view of teacher that has been just created
        }

        // GET: /Teacher/DeleteConfirm/{id}
        //Deletes specific teacher information after using teacher name links on /Teacher/List page
        //[Route("/Teacher/DeleteConfirm/{TeacherId}")]
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacherInfo = controller.FindTeacher(id);

            //Routes 1 selected teacher info to Show.cshtml
            return View(SelectedTeacherInfo);
        }

        //POST: /Teacher/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }

        //GET: /Teacher/Edit/{id}
        /// <summary> Returns teacher page </summary>
        /// <param name="id"></param>
        /// <returns>  </returns>
        public ActionResult Edit(int id)
        {
            //Pass teacher info to View to display to user
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacherInfo = controller.FindTeacher(id);

            return View(SelectedTeacherInfo);
        }

        //POST: /Teacher/Update/{id}
        /// <summary> Method to update teacher info </summary>
        /// <param name="id"></param>
        /// <returns>  </returns>
        [HttpPost]
        public ActionResult Update(int id, string teacherfname, string teacherlname, string salary)
        {

            Debug.WriteLine("Teacher name: "+teacherfname + " " + teacherlname);
            Debug.WriteLine("Teacher ID: "+id);

            Teacher TeacherInfo = new Teacher();
            TeacherInfo.TeacherFName = teacherfname;
            TeacherInfo.TeacherLName = teacherlname;
            TeacherInfo.Salary = salary;
            TeacherInfo.TeacherId = id;

            //Update teacher info
            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(id, TeacherInfo);

            //Return to page of teacher that was just changed/updated
            return RedirectToAction("Show/" + id);
        }


    }
}