using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GroobyPortfel
{
    public partial class Form1 : Form
    {

        public string username, password;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var sr = new System.IO.StreamReader("loginy\\" + txtUserName.Text + "\\login.ID");
                username = sr.ReadLine();
                password = sr.ReadLine();
                sr.Close();

                if (username == txtUserName.Text && password == txtPassword.Text)

                {
                    Config.LoggedUser = txtUserName.Text;
                    MessageBox.Show("Zalogowałeś się poprawnie", "To wielki sukces");
                    Form3 f3 = new Form3();
                    f3.ShowDialog();



                    Form1 f1 = new Form1();
                    f1.Show();

                    //frm2.Close();
                    f1.Dispose();

                    this.Close();


                }

                else
                    MessageBox.Show("Błędne dane!", "Błąd!");
            }

            catch (System.IO.DirectoryNotFoundException ex)
            {
                MessageBox.Show("Użytkownik nie istnieje.", "Błąd");
            }


        }
    }
    }

