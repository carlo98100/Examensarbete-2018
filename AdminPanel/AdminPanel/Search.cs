using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace AdminPanel
{
    class Search
    {
        static string connectionString = null;
        static string sql = null;

        static SqlConnection cnn;
        static SqlCommand cmd;
        static SqlDataReader reader;

        public static void SearchAll()          //Söka fram alla deltagare och visa det i rutan på skärmen.
        {
            Form1.result = "";

            connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\TBTE4HP06\Desktop\Examensarbete 2018\AdminPanel\AdminPanel\employees.mdf;Integrated Security=True";

            sql = "SELECT * FROM [employee]";

            cnn = new SqlConnection(connectionString);

            cnn.Open();
            cmd = new SqlCommand(sql, cnn);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Form1.result += "Email: " + reader.GetValue(1) + Environment.NewLine;
                Form1.result += "First name: " + reader.GetValue(2) + Environment.NewLine;
                Form1.result += "Last name: " + reader.GetValue(3) + Environment.NewLine;
                Form1.result += "Address: " + reader.GetValue(4) + Environment.NewLine;
                Form1.result += "Phonenumber: " + reader.GetValue(5) + Environment.NewLine;                               
                Form1.result += "Jobtitle: " + reader.GetValue(7) + Environment.NewLine;
                Form1.result += "Salary: " + reader.GetValue(6) + Environment.NewLine + Environment.NewLine;
            }
        }

        public static void SearchUser(string Email)         //Söka efter en specifik person genom att söka på Emailen. Sedan så radas informationen upp om personen ifall den finns.
        {
            Form1.result = "";

            connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\TBTE4HP06\Desktop\Examensarbete 2018\AdminPanel\AdminPanel\employees.mdf;Integrated Security=True";

            sql = "SELECT * FROM [employee] WHERE Email = '" + Email + "' ";

            cnn = new SqlConnection(connectionString);

            cnn.Open();
            cmd = new SqlCommand(sql, cnn);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Form1.result += "Email: " + reader.GetValue(1) + Environment.NewLine + Environment.NewLine;     //Detta i slutet är för att kunna göra en ny rad.
                Form1.result += "First name: " + reader.GetValue(2) + Environment.NewLine + Environment.NewLine;
                Form1.result += "Last name: " + reader.GetValue(3) + Environment.NewLine + Environment.NewLine;
                Form1.result += "Address: " + reader.GetValue(4) + Environment.NewLine + Environment.NewLine;
                Form1.result += "Phonenumber: " + reader.GetValue(5) + Environment.NewLine + Environment.NewLine;
                Form1.result += "Jobtitle: " + reader.GetValue(7) + Environment.NewLine + Environment.NewLine;
                Form1.result += "Salary: " + reader.GetValue(6) + Environment.NewLine + Environment.NewLine;               
                Form1.result += "Startdate: " + reader.GetValue(8) + Environment.NewLine + Environment.NewLine;
            }
        }

        public static void Mail(string Email)                        //Email funktionen föra att söka efter användarnamn
        {

            Form1.RecoveryNewPW = "";
            Form1.checker = 0;

            connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\TBTE4HP06\Desktop\Examensarbete 2018\AdminPanel\AdminPanel\employees.mdf;Integrated Security=True";

            sql = "SELECT * FROM [Login] WHERE Username = '" + Email + "' ";

            cnn = new SqlConnection(connectionString);

            cnn.Open();
            cmd = new SqlCommand(sql, cnn);
            reader = cmd.ExecuteReader();

            while (reader.Read())               //Om det finns en email som är samma som personen skrivit in så ska checker i form1 ändras till 1.
            {
                //Form1.emailMailer = reader.GetValue(2).ToString();
                Form1.checker = 1;
            }
        }

        public static void SearchEditUser(string Email)                 //Sökfunktionen som används på editsidan, söker med en specifik email. Sedan skrivs informationen ut i tomma variabler.
        {
            Form1.result = "";

            connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\TBTE4HP06\Desktop\Examensarbete 2018\AdminPanel\AdminPanel\employees.mdf;Integrated Security=True";

            sql = "SELECT * FROM [employee] WHERE Email = '" + Email + "' ";

            cnn = new SqlConnection(connectionString);

            cnn.Open();
            cmd = new SqlCommand(sql, cnn);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Form1.id = reader.GetValue(0).ToString();
                Form1.emailLbl = reader.GetValue(1).ToString();
                Form1.firstNameLbl = reader.GetValue(2).ToString();
                Form1.lastNameLbl = reader.GetValue(3).ToString();
                Form1.addressLbl = reader.GetValue(4).ToString();
                Form1.phoneNumberLbl = reader.GetValue(5).ToString();
                Form1.salaryLbl = reader.GetValue(6).ToString();
                Form1.jobTitleLbl = reader.GetValue(7).ToString();

            }
        }
    }
}
