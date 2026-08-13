using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsCRUD.DataBaseConnection
{
    public class DbConnection
    {
        private static string connectionString =
         ConfigurationManager
        .ConnectionStrings["StudentsCRUD.Properties.Settings.studentsConnectionString"]
        .ConnectionString;

        public static OleDbConnection GetDbConnection()
        {
            return new OleDbConnection(connectionString);
        }
    }
}
