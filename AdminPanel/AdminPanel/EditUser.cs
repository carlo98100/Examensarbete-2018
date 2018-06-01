using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel
{
    class EditUser
    {
        static string connectionString = null;

        public static void Edit(string Choice, string choiceTxt)        //Ändra funktionen.
        {
            connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\TBTE4HP06\Desktop\Examensarbete 2018\AdminPanel\AdminPanel\employees.mdf;Integrated Security=True";
            using (var conn = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("UPDATE Employee SET " + Choice + " ='" + choiceTxt + "' WHERE ID = " + Form1.id + " ", conn);
                conn.Open();
                cmd.ExecuteNonQuery();

            }

        }
    }
}
