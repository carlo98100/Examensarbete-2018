using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel
{
    class Logs
    {

        static string connectionString = null;
        static SqlConnection cnn;
        static SqlCommand cmd;
        static string sql = null;
        static SqlDataReader reader;

        public static void Logger(string Email, string Change)      //lägger in händelser som folk gör i programmet så att man ska kunna kolla det sen.
        {
            connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\TBTE4HP06\Desktop\Examensarbete 2018\AdminPanel\AdminPanel\employees.mdf;Integrated Security=True";

            sql = "INSERT INTO [Logs] (Email, Change, Time) values ('"+ Email +"', '"+ Change +"',GETDATE())";

            cnn = new SqlConnection(connectionString);

            cnn.Open();
            cmd = new SqlCommand(sql, cnn);
            reader = cmd.ExecuteReader();
        }
    }
}
