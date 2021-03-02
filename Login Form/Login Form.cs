using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login_Form
{
    public partial class LoginForm : Form
    {
        MainFrmStff main;
        DatabaseEntities db = new DatabaseEntities();
        public LoginForm()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            string name = txtusername.Text;
            string password = txtpassword.Text;
            var search = (from a in db.Users
                          where a.UserName == name && a.UserPassword == password
                          select a).FirstOrDefault();
            if (search == null)
            {
                MessageBox.Show("Invalid Username or Password!");
            }
            else if(name.Equals(""))
            {
                MessageBox.Show("Name must be filled!", "Warning!", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            else if (password.Equals(""))
            {
                MessageBox.Show("Password must be filled!", "Warning!", MessageBoxButtons.OK,
                   MessageBoxIcon.Warning);
            }
            else
            {
                string role = search.UserRole;
                string id = search.UserID;
                main = new MainFrmStff(role, password, id);
                this.Hide();
                main.ShowDialog();
                this.Dispose();
            }
        }
        

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            RegisterForm register = new RegisterForm();
            register.ShowDialog();
            this.Show();
        }
    }
}
