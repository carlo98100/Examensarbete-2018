using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel
{
    class Login
    {

        static string connectionString = null;
        static SqlConnection cnn;
        static SqlCommand cmd;
        static string sql = null;
        static SqlDataReader reader;

        public static void LoginChecker(string email, string Password)          //Funktionen som kollar ifall man får logga in eller inte.
        {
            Form1.result = "";
            connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\TBTE4HP06\Desktop\Examensarbete 2018\AdminPanel\AdminPanel\employees.mdf;Integrated Security=True";
            sql = "SELECT * FROM Login WHERE UserName = '" + email + "' AND Password = '" + Password + "'";
            cnn = new SqlConnection(connectionString);

            cnn.Open();
            cmd = new SqlCommand(sql, cnn);
            reader = cmd.ExecuteReader();

            if (reader.Read() == true)                          //Om det finns ett inlogg med rätt email och lösenord så kommer man in.
            {
                Form1.Log = "Successful";
            }
            else                                               //Om det inte finns ett inlogg med det som skrivits in så kommer man inte in.
            {
                Form1.Log = "Failed";
            }
        }

        public static void PasswordChanger(string User)         //Ändrar lösenordet till ett nytt för att sedan skickas till mailen.
        {

            string newPassword = GetRandomString();

            Form1.RecoveryNewPW = newPassword;
            Encrypter.MD5Hash(newPassword);

            connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\TBTE4HP06\Desktop\Examensarbete 2018\AdminPanel\AdminPanel\employees.mdf;Integrated Security=True";

            sql = "UPDATE Login SET Password = '"+ Form1.hashed +"' where Username = '" + User + "'";

            cnn = new SqlConnection(connectionString);

            cnn.Open();
            cmd = new SqlCommand(sql, cnn);
            reader = cmd.ExecuteReader();
        }

        public static string GetRandomString()              //Skapar ett random lösenord som man använder i funktionen ovanför.
        {
            string path = Path.GetRandomFileName();
            path = path.Replace(".", "");
            return path;
        }

        public static void LoginRoleChecker(string Email)         //Funktionen som kollar vilken roll personen som loggar in har och beroende på roll så visas olika sidor.
        {
            Form1.role = "";
            connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\TBTE4HP06\Desktop\Examensarbete 2018\AdminPanel\AdminPanel\employees.mdf;Integrated Security=True";

            sql = "SELECT Role FROM Login WHERE UserName = '" + Email + "' ";

            cnn = new SqlConnection(connectionString);

            cnn.Open();
            cmd = new SqlCommand(sql, cnn);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Form1.role += reader.GetValue(0);              
            }
        }
    }
}
