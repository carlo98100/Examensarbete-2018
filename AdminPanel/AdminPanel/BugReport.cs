using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel
{
    class BugReport
    {
        static string connectionString = null;
        static SqlConnection cnn;
        static SqlCommand cmd;
        static string sql = null;
        static SqlDataReader reader;

        public static void UserBugReport(string Email, string Description)         //Funktionen lagrar det problem som användaren skrivit in och sagt att det finns.
        {
            Form1.role = "";
            connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\TBTE4HP06\Desktop\Examensarbete 2018\AdminPanel\AdminPanel\employees.mdf;Integrated Security=True";

            sql = "INSERT INTO Ticket (Email, Description) VALUES ('"+ Email +"', '"+ Description + "')";

            cnn = new SqlConnection(connectionString);

            cnn.Open();
            cmd = new SqlCommand(sql, cnn);
            reader = cmd.ExecuteReader();
        }
    }
}
