using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using a3_seantrudeln01525609_http5112webdev1c.Models;   //All classes/files in Models folder is accessable
using MySql.Data.MySqlClient;                           //MySql database is accessable

namespace a3_seantrudeln01525609_http5112webdev1c.Controllers
{
    public class TeacherDataController : ApiController
    {
        //Database context class provides access to database "school_db"
        private SchoolDbContext Teachers = new SchoolDbContext();

        //Controller to access Teachers table of school_db database
        /// <summary>
        /// Returns list of Teachers in database.
        /// </summary>
        /// <example>GET: /api/TeacherData/ListTeachers </example>
        /// <returns>List of Teachers with following info: teacher IDs, first names, last names, employee numbers, hire dates, salaries</returns>
        [HttpGet]
        //[Route("api/TeacherData/ListTeachers")]
        public List<Teacher> ListTeachers()
        {
            //Variable: Create connection instance
            MySqlConnection Conn = Teachers.AccessDatabase();

            //Open connection between web server and database
            Conn.Open();

            //Variable: Establish new command (query) for database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL query: show all info from 'Teachers' table
            cmd.CommandText = "Select * from Teachers";

            //Variable: Gather result of SQL query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Variable: Create empty list of teacher information
            List<Teacher> TeacherInformation = new List<Teacher>{};

            //Populate empty list 'TeacherInfo': Loop through each row the result set
            while (ResultSet.Read())
            {
                //Variable: Create instance of teacher info from 'Teacher' object model
                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                NewTeacher.TeacherFName = ResultSet["teacherfname"].ToString();
                NewTeacher.TeacherLName = ResultSet["teacherlname"].ToString();
                NewTeacher.EmployeeNumber = ResultSet["employeenumber"].ToString();
                NewTeacher.HireDate = ResultSet["hiredate"].ToString();
                NewTeacher.Salary = ResultSet["salary"].ToString();

                //Add the Author Name to the List
                TeacherInformation.Add(NewTeacher);
            }

            //Close connection between web server and database
            Conn.Close();

            //Return the final list of author names
            return TeacherInformation;
        }

        //GET: /api/TeacherData/FindTeacher/1
        [HttpGet]
        [Route("api/TeacherData/FindTeacher/{employeenumber}")]
        public string FindTeacher(int employeenumber)
        {
            return "Search results for specified teacher: " + employeenumber.ToString();
        }
    }
}
