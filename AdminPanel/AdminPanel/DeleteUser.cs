using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace AdminPanel
{
    class DeleteUser
    {

        static string connectionString = null;

        public static void Delete(string Email)     //Ta bort funktionen.
        {
            connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\TBTE4HP06\Desktop\Examensarbete 2018\AdminPanel\AdminPanel\employees.mdf; Integrated Security = True";
            using (var conn = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("DELETE from [employee] WHERE Email = '" + Email+ "' ", conn);
                conn.Open();
                cmd.ExecuteNonQuery();

            }

        }
    }
}
