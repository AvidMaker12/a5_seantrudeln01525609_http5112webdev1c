using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using a3_seantrudeln01525609_http5112webdev1c.Models;   //All classes/files in Models folder are accessable
using MySql.Data.MySqlClient;                           //MySql database is accessable

namespace a3_seantrudeln01525609_http5112webdev1c.Controllers
{
    public class ClassDataController : ApiController
    {
        //Database context class provides access to database "school_db"
        private SchoolDbContext Class = new SchoolDbContext();

        //Controller to access Classes table of school_db database

        /// <summary> Returns list of Classes in database. </summary>
        /// <example> GET: /api/TeacherData/ListTeachers </example>
        /// <returns> List of Classes with following info: class IDs, class codes, teacher IDs, start dates, finish dates, class names. </returns>
        [HttpGet]
        //[Route("api/ClassData/ListClasses")]
        public List<Class> ListClasses()
        {
            //Variable: Create connection instance
            MySqlConnection Conn = Class.AccessDatabase();

            //Open connection between web server and database
            Conn.Open();

            //Variable: Establish new command (query) for database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL query: show all info from 'Classes' table
            cmd.CommandText = "Select * from Classes";

            //Variable: Gather result of SQL query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Variable: Create empty list of class information
            List<Class> ClassInformation = new List<Class> { };

            //Populate empty list: Loop through each row the result set
            while (ResultSet.Read())
            {
                //Variable: Create instance of class info from 'Class' object model
                Class NewClass = new Class();
                NewClass.ClassId = Convert.ToInt32(ResultSet["classid"]);
                NewClass.ClassCode = Convert.ToInt32(ResultSet["classcode"]);
                NewClass.TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                NewClass.StartDate = ResultSet["startdate"].ToString();
                NewClass.FinishDate = ResultSet["finishdate"].ToString();
                NewClass.ClassName = ResultSet["classname"].ToString();

                //Add class information to the list
                ClassInformation.Add(NewClass);

                //Debug.WriteLine(ListClasses);
            }

            //Close connection between web server and database
            Conn.Close();

            //Return the final list of author names
            return ClassInformation;
        }
    }
}
