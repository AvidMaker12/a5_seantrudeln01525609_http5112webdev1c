using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using a3_seantrudeln01525609_http5112webdev1c.Models;   //All classes/files in Models folder are accessable
using MySql.Data.MySqlClient;                           //MySql database is accessable
using System.Diagnostics;

namespace a3_seantrudeln01525609_http5112webdev1c.Controllers
{
    public class TeacherDataController : ApiController
    {
        //Database context class provides access to database "school_db"
        private SchoolDbContext Teachers = new SchoolDbContext();

        //Controller to access Teachers table of school_db database

        /// <summary> Returns list of Teachers in database. </summary>
        /// <param name="SearchKey">SearchKey (optional) of teacher name</param>
        /// <example> GET: /api/TeacherData/ListTeachers </example>
        /// <example> GET: /api/TeacherData/ListTeachers </example>
        /// <returns> List of Teachers (teacher objects) with following info: teacher IDs, first names, last names, employee numbers, hire dates, salaries. </returns>
        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]
        public List<Teacher> ListTeachers(string SearchKey = null)
        {
            if (SearchKey != null)
            {
                Debug.WriteLine("Key: " + SearchKey);
            }

            //Variable: Create connection instance
            MySqlConnection Conn = Teachers.AccessDatabase();

            //Open connection between web server and database
            Conn.Open();

            //Variable: Establish new command (query) for database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL query: show all info from 'Teachers' table
            string query = "SELECT * FROM Teachers";

            if(SearchKey != null)
            {
                query = query + " WHERE LOWER(TeacherFName)=LOWER(@key)";   //Search input syntax to prevent SQL injection attack
                cmd.Parameters.AddWithValue("@key",SearchKey);      //@key is a variable that replaces SearchKey, used to prevent SQL injection attack, query is 'parameterized'
                cmd.Prepare();  //Finds parameters associated with Parameters line (the @key, in this case)
            }
            Debug.WriteLine("Query: " + query);
            
            //Input SQL command using query variable
            cmd.CommandText = query;
            
            //Variable: Gather result of SQL query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Variable: Create empty list of teacher information
            List<Teacher> TeacherInformation = new List<Teacher>{};

            //Populate empty list: Loop through each row the result set
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

                //Add teacher information to the list
                TeacherInformation.Add(NewTeacher);

                //Debug.WriteLine(ListTeachers);
            }

            //Close connection between web server and database
            Conn.Close();

            //Return the final list of author names
            return TeacherInformation;
        }

        /// <summary> Returns single teacher info in database. </summary>
        /// <example> GET: /api/TeacherData/FindTeacher/{TeacherId} </example>
        /// <paramref name="TeacherId"> Find Teacher info from database using Teachers table primary key </paramref>
        /// <returns> Single teacher info with following info displayed: teacher IDs, first name, last name, employee number, hire date, salary. </returns>
        //GET: /api/TeacherData/FindTeacher/1
        [HttpGet]
        [Route("api/TeacherData/FindTeacher/{TeacherId}")]
        public Teacher FindTeacher(int TeacherId)
        {
            //Variable: Create connection instance
            MySqlConnection Conn = Teachers.AccessDatabase();

            //Open connection between web server and database
            Conn.Open();

            //Variable: Establish new command (query) for database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL query: show all info from 'Teachers' table
            cmd.CommandText = "SELECT * FROM Teachers WHERE TeacherId=@id";
            cmd.Parameters.AddWithValue("@id", TeacherId);      //@id is a variable that replaces SearchKey, used to prevent SQL injection attack, query is 'parameterized'
            cmd.Prepare();  //Finds parameters associated with Parameters line (the @id, in this case)

            //Variable: Gather result of SQL query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Variable: Create empty list of teacher information
            Teacher SelectedTeacherInfo = new Teacher();

            //Populate empty list: Loop through each row the result set
            while (ResultSet.Read())
            {
                //Variable: Create instance of teacher info from 'Teacher' object model
                SelectedTeacherInfo.TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                SelectedTeacherInfo.TeacherFName = ResultSet["teacherfname"].ToString();
                SelectedTeacherInfo.TeacherLName = ResultSet["teacherlname"].ToString();
                SelectedTeacherInfo.EmployeeNumber = ResultSet["employeenumber"].ToString();
                SelectedTeacherInfo.HireDate = ResultSet["hiredate"].ToString();
                SelectedTeacherInfo.Salary = ResultSet["salary"].ToString();
            }

            //Close connection between web server and database
            Conn.Close();

            //Return the final list of author names
            return SelectedTeacherInfo;
        }

        /// <summary> Add new teacher to database using inputted teacher info </summary>
        /// <paramref name="NewTeacher"> Teacher info to be added </paramref>
        public void AddTeacher(Teacher NewTeacher)        //Void as nothing will be outputed/returned from this method
        {
            //Variable: Create connection instance
            MySqlConnection Conn = Teachers.AccessDatabase();

            //Open connection between web server and database
            Conn.Open();

            //Variable: Establish new command (query) for database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL query: add user input info into database (teachers table)
            string query = "INSERT INTO teachers (teacherfname, teacherlname, hiredate, salary) VALUES (@teacherfname,@teacherlname,CURRENT_DATE(),@salary)";
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@teacherfname",NewTeacher.TeacherFName);
            cmd.Parameters.AddWithValue("@teacherlname", NewTeacher.TeacherLName);
            cmd.Parameters.AddWithValue("@hiredate", NewTeacher.HireDate);
            cmd.Parameters.AddWithValue("@salary", NewTeacher.Salary);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }

        /// <summary> Delete teacher from database </summary>
        /// <param name="TeacherId"> Primary key teacherid </param>
        public void DeleteTeacher(int TeacherId)
        {
            //Variable: Create connection instance
            MySqlConnection Conn = Teachers.AccessDatabase();

            //Open connection between web server and database
            Conn.Open();

            //Variable: Establish new command (query) for database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL query: delete user specified info from database (teachers table)
            string query = "DELETE FROM teachers WHERE teacherid=@id";
            cmd.Parameters.AddWithValue("@id", TeacherId);
            cmd.CommandText = query;
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }

        /// <summary> Updates teacher in system when given teacher info </summary>
        /// <param name="TeacherId"> Primary key of teacher to update </param>
        /// <param name="TeacherInfo"> Teacher object: first name, last name, bio </param>
        public void UpdateTeacher(int TeacherId, Teacher TeacherInfo)
        {
            //Variable: Create connection instance
            MySqlConnection Conn = Teachers.AccessDatabase();

            //Open connection between web server and database
            Conn.Open();

            //Variable: Establish new command (query) for database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL query: edit user input info into database (teachers table)
            string query = "UPDATE teachers SET teacherfname=@teacherfname, teacherlname=@teacherlname, salary=@salary WHERE teacherid=@teacherid";
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@teacherfname", TeacherInfo.TeacherFName);
            cmd.Parameters.AddWithValue("@teacherlname", TeacherInfo.TeacherLName);
            cmd.Parameters.AddWithValue("@salary", TeacherInfo.Salary);
            cmd.Parameters.AddWithValue("@teacherid", TeacherInfo);
            //cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }

    }
}
