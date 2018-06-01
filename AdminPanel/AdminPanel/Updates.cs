using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel
{
    class Updates
    {
        static string connectionString = null;
        static SqlConnection cnn;
        static SqlCommand cmd;
        static string sql = null;
        static SqlDataReader reader;

        public static void Update(string Email, string updates)         //Denna funktionen lägger till "inlägget" som man skrivit i databasen.
        {

            connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\TBTE4HP06\Desktop\Examensarbete 2018\AdminPanel\AdminPanel\employees.mdf;Integrated Security=True";

            sql = "INSERT INTO UpdateUsers (Email, Updates) VALUES ('" + Email + "', '" + updates + "')";

            cnn = new SqlConnection(connectionString);

            cnn.Open();
            cmd = new SqlCommand(sql, cnn);
            reader = cmd.ExecuteReader();
        }

        public static void SearchAllUpdates()                       //denna funktionen tar ut alla uppdateringar som finns i databasen och skriver ut dom i rutan där inläggen ska finnas.
        {

            connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\TBTE4HP06\Desktop\Examensarbete 2018\AdminPanel\AdminPanel\employees.mdf;Integrated Security=True";

            sql = "SELECT * FROM UpdateUsers";

            cnn = new SqlConnection(connectionString);

            cnn.Open();
            cmd = new SqlCommand(sql, cnn);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Form1.settingsUpdate += " - " + reader.GetValue(2).ToString() + Environment.NewLine + Environment.NewLine;

            }
        }
    }
}
