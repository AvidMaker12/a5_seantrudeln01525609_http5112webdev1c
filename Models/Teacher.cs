using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace a3_seantrudeln01525609_http5112webdev1c.Models
{
    //Model class to represent 'teacher' information: 'Teacher' singular as each instance represents 1 teacher information instance
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string TeacherFName { get; set; }
        public string TeacherLName { get; set; }
        public string EmployeeNumber { get; set; }
        public string HireDate { get; set; }
        public string Salary { get; set; }
    }
}