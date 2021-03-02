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
    public partial class ChgPass : Form
    {
        public string dbPass;
        DatabaseEntities db = new DatabaseEntities();
        public ChgPass(String password)
        { 
            InitializeComponent();
            dbPass = password;
        }
                
        private void btnChangePass_Click_1(object sender, EventArgs e)
        {
            String oldPass = txtOldPass.Text;
            String newPass = txtNewPass.Text;
            String conPass = txtConfirmPass.Text;
            
            if (oldPass == "")
            {
                MessageBox.Show("Old password must be filled!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!oldPass.Equals(dbPass))
            {
                MessageBox.Show("Old password doesn't match with the current password", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (newPass == "")
            {
                MessageBox.Show("New password must be filled!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (newPass.Length < 5)
            {
                MessageBox.Show("New password length must be 5 characters or more", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (newPass != conPass)
            {
                MessageBox.Show("Re-type password doesn't match with the new password", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                User updateUser = (from x in db.Users
                                   where x.UserPassword == dbPass
                                   select x).FirstOrDefault();
                updateUser.UserPassword = newPass;
                db.SaveChanges();
                MessageBox.Show("Password successfully changed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Dispose();
            }
        }
    }
}
