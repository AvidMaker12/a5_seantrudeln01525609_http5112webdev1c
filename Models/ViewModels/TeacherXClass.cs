using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using a3_seantrudeln01525609_http5112webdev1c.Models;

namespace a3_seantrudeln01525609_http5112webdev1c.Models.ViewModels
{
    //ViewModel combines Teacher and Class tables from database to show more info on a page
    public class TeacherXClass
    {
        public Teacher Teacher { get; set; }
        public Class Class { get; set; }
    }
}