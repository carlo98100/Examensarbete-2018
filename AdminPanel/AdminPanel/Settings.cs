using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel
{
    class Settings
    {
        static string connectionString = null;
        static SqlConnection cnn;
        static SqlCommand cmd;
        static string sql = null;
        static SqlDataReader reader;

        public static void PasswordFinder(string oldPW)         //Hämtar det gamla lösenordet.
        {
            Form1.OldPWChecker = 0;
            connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\TBTE4HP06\Desktop\Examensarbete 2018\AdminPanel\AdminPanel\employees.mdf;Integrated Security=True";

            sql = "SELECT * FROM Login WHERE Password = '"+ oldPW +"'";


            cnn = new SqlConnection(connectionString);

            cnn.Open();
            cmd = new SqlCommand(sql, cnn);
            reader = cmd.ExecuteReader();


            while (reader.Read())       //Om lösenordet finns så blir OldPWChecker 1 istället för 0 och då händer det saker i form1.
            {
                Form1.OldPWChecker = 1;
            }
        }

        public static void PasswordChanger(string newPW, string loginEmail)     //Funktionen för att ändra sitt inloggs lösenord.
        {
            
            connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\TBTE4HP06\Desktop\Examensarbete 2018\AdminPanel\AdminPanel\employees.mdf;Integrated Security=True";

            sql = "UPDATE Login SET Password = '" + newPW + "' where Username = '" + Form1.loginEmail + "'";


            cnn = new SqlConnection(connectionString);

            cnn.Open();
            cmd = new SqlCommand(sql, cnn);
            reader = cmd.ExecuteReader();
        }
    }
}

