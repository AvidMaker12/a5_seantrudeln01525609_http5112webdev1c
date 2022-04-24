using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace a3_seantrudeln01525609_http5112webdev1c.Models
{
    //Model class to represent 'class' information: 'class' singular as each instance represents 1 class information instance
    public class Class
    {
        public int ClassId { get; set; }
        public int ClassCode { get; set; }
        public int TeacherId { get; set; }
        public string StartDate { get; set; }
        public string FinishDate { get; set; }
        public string ClassName { get; set; }
    }
}