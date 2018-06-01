using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace AdminPanel
{
    public partial class Form1 : Form
    {
        public static int checker;
        public static int OldPWChecker;

        public static string RecoveryNewPW;
        public static string loginEmail;
        public static string result;
        public static string Log;
        public static string choice;
        public static string id;
        public static string role;
        public static string settingsUpdate;
        public static string changes;
        public static string hashed;

        public static string emailLbl;
        public static string firstNameLbl;
        public static string lastNameLbl;
        public static string addressLbl;
        public static string phoneNumberLbl;
        public static string salaryLbl;
        public static string jobTitleLbl;

        public Form1()
        {
            InitializeComponent();

            TabControl1.Hide();

            PasswordLoginTxt.PasswordChar = '*';
            OldPWTxt.PasswordChar = '*';
            NewPWTxt.PasswordChar = '*';
            RepeatNewPWTxt.PasswordChar = '*';

            Updates.SearchAllUpdates();
            UpdateTxt.Text = settingsUpdate;
        }

        private void AddBtn_Click(object sender, EventArgs e)   //Lägg till Användare
        {
            if (EmailAddTxt.Text == "" || FirstNameAddTxt.Text == "" || LastNameAddTxt.Text == "" || AddressAddTxt.Text == "" || PhonenumberAddTxt.Text == "" || JobTitleAddTxt.Text == "" || SalaryAddTxt.Text == "")
            {
                MessageBox.Show("You need to fill all required fields.");    //Om alla fälten inte är ifyllda så ska detta skrivas ut.
            }
            else
            {
                AddUser.Add(EmailAddTxt.Text, FirstNameAddTxt.Text, LastNameAddTxt.Text, AddressAddTxt.Text, PhonenumberAddTxt.Text, JobTitleAddTxt.Text, SalaryAddTxt.Text);

                changes = "Added a new user with Email: " + EmailAddTxt.Text;
                Logs.Logger(loginEmail, changes);

                MessageBox.Show("Sucessfully added!");       //Annars ska en användare läggas till och detta ska skrivas ut.
            }

            EmailAddTxt.Clear();                    //För att rensa alla fälten så görs detta nedan.
            FirstNameAddTxt.Clear();
            LastNameAddTxt.Clear();
            AddressAddTxt.Clear();
            PhonenumberAddTxt.Clear();
            JobTitleAddTxt.Clear();
            SalaryAddTxt.Clear();
        }

        private void SearchAllBtn_Click(object sender, EventArgs e)     //Söka på alla användare.
        {
            Search.SearchAll();
            SearchResultsTxt.Text = result;
        }

        private void SearchUserBtn_Click(object sender, EventArgs e)        //Söka en speciell användare genom att söka på email.
        {
            Search.SearchUser(SearchUserTxt.Text);
            SearchResultsTxt.Text = result;                         
            SearchUserTxt.Clear();                                          //Fältet ska rensas efteråt.
        }

        private void DeleteBtn_Click(object sender, EventArgs e)            //Knappen för att ta bort en användare.
        {            
            DeleteUser.Delete(DeleteTxt.Text);                              //Skriver in vilken användare som ska tas bort genom att ange email.

            changes = "Deleted person with Email: " + DeleteTxt.Text;
            Logs.Logger(loginEmail, changes);

            MessageBox.Show("Deleted!");
            DeleteTxt.Clear();                                                //Rensar fältet.
        }

        private void ExportBtn_Click(object sender, EventArgs e)            //Överför all information i databasen till en Excel fil ifall man skulle vilja ha det. Man måste dock ändra sökningsvägen ifall man vill spara filen någon annan stanns.
        {
            using (TextWriter sw = new StreamWriter("\\Test\\Employees_" + DateTime.Today.ToString("yyyy-MM-dd") +".csv"))
            {
                sw.WriteLine("{0}", result);
                MessageBox.Show("Done!");
            }
        }

        private void SearchBtn_Click(object sender, EventArgs e)           //Skriver ut informationen om personen man man sökt på i Edit sidan.
        {
            Search.SearchEditUser(SearchTxt.Text);

            EmailLbl.Text = emailLbl;                   //Här nedan så skrivs all information in i alla tomma variaberna på skärmen.
            FirstNameLbl.Text = firstNameLbl;
            LastNameLbl.Text = lastNameLbl;
            AddressLbl.Text = addressLbl;
            PhonenumberLbl.Text = phoneNumberLbl;
            JobTitleLbl.Text = jobTitleLbl;
            SalaryLbl.Text = salaryLbl;
            SearchTxt.Clear();                                            //Rensar fältet.
        }

        private void EditBtn_Click(object sender, EventArgs e)                      //Här nedan så kollar man ifall man har man har valt något att ändra. Isåfall så läser man av fältet för att se vad man vill ändra till och sedan när görs ändringen.
        {
            if(EmailRdo.Checked == true)            //För email.
            {
                choice = "Email";
                EditUser.Edit(choice, ChoiceTxt.Text);
                MessageBox.Show("Done!");               
            }
            else if (FirstNameRdo.Checked == true)      //För förnamn.
            {
                choice = "Firstname";
                EditUser.Edit(choice, ChoiceTxt.Text);      
                MessageBox.Show("Done!");
            }
            else if (LastNameRdo.Checked == true)           //För efternamn.
            {
                choice = "Lastname";
                EditUser.Edit(choice, ChoiceTxt.Text);
                MessageBox.Show("Done!");                
            }
            else if (AddressRdo.Checked == true)            //För adress.
            {
                choice = "Address";
                EditUser.Edit(choice, ChoiceTxt.Text);
                MessageBox.Show("Done!");               
            }
            else if (PhonenumberRdo.Checked == true)        //För telefonnummer.
            {
                choice = "Phonenumber";
                EditUser.Edit(choice, ChoiceTxt.Text);
                MessageBox.Show("Done!");               
            }
            else if (JobTitleRdo.Checked == true)       //För jobbtitel.
            {
                choice = "Jobtitle";
                EditUser.Edit(choice, ChoiceTxt.Text);
                MessageBox.Show("Done!");                
            }
            else if (SalaryRdo.Checked == true)         //För lönen.
            {
                choice = "Salary";
                EditUser.Edit(choice, ChoiceTxt.Text);
                MessageBox.Show("Done!");              
            }

            changes = "Edited person with Email: " + EmailLbl.Text;
            Logs.Logger(loginEmail, changes);
            ChoiceTxt.Clear();
        }

        private void LoginBtn_Click(object sender, EventArgs e)             //Login delen är nedan.
        {
            Encrypter.MD5Hash(PasswordLoginTxt.Text);
            Login.LoginChecker(EmailLoginTxt.Text, hashed);
            Login.LoginRoleChecker(EmailLoginTxt.Text);

            if (Log == "Successful")            //Om inloggningen går igenom så ska "nästa sida" visas. Då ska inloggninssidan gömmas.
            {
                if(role == "User")              //Om man har rollen "User" så ska man kunna se vissa saker.
                {                    
                    loginEmail = EmailLoginTxt.Text;
                    TabControl1.Show();
                    TabControl1.TabPages.Remove(tabPage2);
                    TabControl1.TabPages.Remove(tabPage3);
                    TabControl1.TabPages.Remove(TabPage4);
                    TabControl1.TabPages.Remove(tabPage6);
                    EmailLoginLbl.Hide();
                    PasswordLoginLbl.Hide();
                    LoginBtn.Hide();
                    EmailLoginTxt.Hide();
                    PasswordLoginTxt.Hide();
                    MailTxt.Hide();
                    MailBtn.Hide();
                    PWLbl.Hide();
                }
                else                         //Är man inte User så är man Admin och då ska man kunna se vissa saker.
                {                    
                    loginEmail = EmailLoginTxt.Text;
                    TabControl1.Show();
                    EmailLoginLbl.Hide();
                    PasswordLoginLbl.Hide();
                    LoginBtn.Hide();
                    EmailLoginTxt.Hide();
                    PasswordLoginTxt.Hide();
                    MailTxt.Hide();
                    MailBtn.Hide();
                    PWLbl.Hide();
                }
            }
            else                              //Annars så ska den skriva att det är fel lösenord eller email.
            {
                MessageBox.Show("Wrong Email or password!");
            }
        }

        private void MailBtn_Click(object sender, EventArgs e)      //Skicka mail ifall man glöm sitt lösenord.
        {
            Search.Mail(MailTxt.Text);
            Login.PasswordChanger(MailTxt.Text);
            MailTxt.Clear();
        
            if(checker == 1)            //Om det är 1 så finns det en användare och lösenordes skickas.
            {
                Outlook.Application app = new Outlook.Application();
                Outlook.MailItem mailItem = app.CreateItem(Outlook.OlItemType.olMailItem);

                mailItem.Subject = "Password recovery";
                mailItem.To = MailTxt.Text;
                mailItem.HTMLBody = "Your new password is: " + RecoveryNewPW + ". <br/> If you want to change the password, you need to do following:<ul><li>Login and go to settings tab.</li><li>Write old password in the first textbox.</li><li>Write the new password in the second textbox</li><li>Write the new password again in the third textbox</li><li>Done!</li></ul>";
                mailItem.Send();
                MessageBox.Show("Sent!");
            }
            else                //Annars så finns det ingen användare med den emailen.
            {
                MessageBox.Show("Email dosent exist!");
            }
        }

        private void SettingsSubmitBtn_Click(object sender, EventArgs e)
        {
            Encrypter.MD5Hash(OldPWTxt.Text);
            Settings.PasswordFinder(hashed);

            if (OldPWChecker == 1)              //Om den är 1 så finns lösenordet i databasen.
            {
                if (NewPWTxt.Text == RepeatNewPWTxt.Text)       //Om det nya lösenordet stämmer överräns så byter det plats med det gamla lösenordet i databasen.
                {
                    Encrypter.MD5Hash(NewPWTxt.Text);
                    Settings.PasswordChanger(hashed, loginEmail);
                    MessageBox.Show("Done!");
                    OldPWTxt.Clear();
                    NewPWTxt.Clear();
                    RepeatNewPWTxt.Clear();
                }
                else                      // Annars om man skrivit två olika lösenord som nytt lösenord så får man reda på att det är fel.
                {
                    MessageBox.Show("New password dosent match!");
                }
            }
            else                  //Om man skrivit in gamla lösenordet och att det är fel så skrivs det ut att det gamla lösenordet är fel.
            {
                MessageBox.Show("Old password is wrong!");
            }
        }

        private void LogOutBtn_Click(object sender, EventArgs e)
        {
            TabControl1.Hide();
            EmailLoginLbl.Show();
            PasswordLoginLbl.Show();
            LoginBtn.Show();
            EmailLoginTxt.Show();
            PasswordLoginTxt.Show();
            MailTxt.Show();
            MailBtn.Show();
            PWLbl.Show();

            if(role == "User")                          //Nedan är för att user och admin ska kunna ha olika sidor.
            {
                TabControl1.TabPages.Add(tabPage2);
                TabControl1.TabPages.Add(tabPage3);
                TabControl1.TabPages.Add(TabPage4);
                TabControl1.TabPages.Remove(TabPage5);
                TabControl1.TabPages.Add(TabPage5);
                TabControl1.TabPages.Add(tabPage6);
            }

            PasswordLoginTxt.Clear();
            EmailLoginTxt.Clear();
        }

        private void BugReportBtn_Click(object sender, EventArgs e)             //Om man skriver ett problem som finns så skickas det direkt till företagets email.
        {
            BugReport.UserBugReport(loginEmail, BugReportTxt.Text);
            Search.Mail(MailTxt.Text);

                Outlook.Application app = new Outlook.Application();
                Outlook.MailItem mailItem = app.CreateItem(Outlook.OlItemType.olMailItem);
                mailItem.Subject = "Bug report";
                mailItem.To = "carlo.goretti@live.se";
                mailItem.HTMLBody = "A new ticket has been created please check the error.";
                mailItem.Send();
                MessageBox.Show("Sent!");        
        }

        private void UpdateBtn_Click(object sender, EventArgs e)        //Denna gör att inlägget som admin har skrivit läggs till i databasen.
        {
            Updates.Update(loginEmail, AddUpdatesTxt.Text);
             
            Updates.SearchAllUpdates();
            UpdateTxt.Text = settingsUpdate;

            changes = "Added a new update";
            Logs.Logger(loginEmail, changes);

            MessageBox.Show("Done!");
        }
    }
}
