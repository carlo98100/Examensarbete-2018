using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace AdminPanel
{
    class AddUser
    {
         static string connectionString = null;

        public static void Add(string AddEmail, string AddFistName, string AddLastName, string AddAddress, string AddPhonenumber, string AddJobTitle, string AddSalary)     //Lägg til en deltagare funktionen.
        {
            connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\TBTE4HP06\Desktop\Examensarbete 2018\AdminPanel\AdminPanel\employees.mdf;Integrated Security=True";
            using(var conn = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("insert into Employee (Email, FirstName, LastName, Address, Phonenumber, Salary, JobTitle, StartDate) VALUES ('" + AddEmail + "','" + AddFistName + "','" + AddLastName + "','" + AddAddress + "','" + AddPhonenumber + "', '" + AddJobTitle + "', '" + AddSalary + "', GETDATE())", conn);
                conn.Open();
                cmd.ExecuteNonQuery();

            }

        }

    }
}
