using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MySql.Data.MySqlClient;

namespace a3_seantrudeln01525609_http5112webdev1c.Models
{
    //Class that connects to MySQL database
    public class SchoolDbContext
    {
        private static string User { get { return "root"; } }
        private static string Password { get { return "root"; } }
        private static string Database { get { return "school_db"; } }
        private static string Server { get { return "localhost"; } }
        private static string Port { get { return "3306"; } }

        protected static string ConnectionString
        {
            get
            {
                return "server = " + Server
                    + "; user = " + User
                    + "; database = " + Database
                    + "; port = " + Port
                    + "; password = " + Password;
            }
        }

        /// <summary> Returns a connection to the blog database. </summary>
        /// <example>
        /// private SchoolDbContext Teachers = new SchoolDbContext();
        /// MySqlConnection Conn = Teachers.AccessDatabase();
        /// </example>
        /// <returns> MySqlConnection Object </returns>
        public MySqlConnection AccessDatabase()
        {
            //MySqlConnection Class instance creates an object
            //object is a specific connection to school_db database on port 3306 of localhost
            return new MySqlConnection(ConnectionString);
        }

    }
}