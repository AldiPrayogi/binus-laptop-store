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
    public partial class RegisterForm : Form
    {
        DatabaseEntities db = new DatabaseEntities();
        public RegisterForm()
        {
            InitializeComponent();
        }

        private bool isNumeric(String noHP)
        {
            for (int i = 0; i < noHP.Length; i++)
            {
                if (noHP[i] < '0' || noHP[i] > '9')
                {
                    return false;
                }
            }
            return true;
        }

        private void validate()
        {
            var username = txtUsername.Text;
            var email = txtEmail.Text;
            var pass = txtPassword.Text;
            var cpass = txtConfPassword.Text;
            var address = txtAddress.Text;
            var phone = txtPhone.Text;
            var gender = "";
            var search = (from a in db.Users
                          where a.UserName == username
                          select a).FirstOrDefault();
            if (username.Equals(""))
            {
                MessageBox.Show("Username must be filled!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (email.Equals(""))
            {
                MessageBox.Show("Email must be filled!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (email.IndexOf('@') == 0 || email.IndexOf('.') == 0)
            {
                MessageBox.Show("Email cannot start with '@' or '.'", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (email.IndexOf('@') == email.Length - 1 || email.IndexOf('.') == email.Length - 1)
            {
                MessageBox.Show("Email cannot end with '@' or '.'", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!email.Contains('@') && !email.Contains('.'))
            {
                MessageBox.Show("Email must contain '@' and '.'", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (radMale.Checked == false && radFemale.Checked == false)
            {
                MessageBox.Show("Gender must be chosen!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (dateOB.Value.Date > DateTime.Now.Date)
            {
                MessageBox.Show("DOB cannot be greater than current date!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (email.IndexOf("@") + 1 == email.IndexOf("."))
            {
                MessageBox.Show("'@' and '.' cannot be placed beside each other", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (isNumeric(phone) == false)
            {
                MessageBox.Show("Phone must be numeric!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (phone.Length != 12)
            {
                MessageBox.Show("Phone number must be 12 digits!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (phone.Equals(""))
            {
                MessageBox.Show("Phone number must be filled!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (address.Equals(""))
            {
                MessageBox.Show("Address must be filled!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!address.Contains("Street"))
            {
                MessageBox.Show("Address must contain 'Street'", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (pass.Equals(""))
            {
                MessageBox.Show("Password must be filled!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (pass.Length < 5)
            {
                MessageBox.Show("Password length must be 5 characters of more", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!cpass.Equals(pass))
            {
                MessageBox.Show("Confirm Password must be the same with Password", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (search != null)
            {
                MessageBox.Show("Username already exists", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var id = (from a in db.Users
                          orderby a.UserID descending
                          select a.UserID).FirstOrDefault();
                int ids = Int32.Parse(id.Substring(2, 3));
                ids++;
                string newID = id.Substring(0, 2) + ids.ToString("d3");
                if (radMale.Checked)
                {
                    gender = "Male";
                }
                else
                {
                    gender = "Female";
                }
                User newUser = new User()
                {
                    UserID = newID,
                    UserName = username,
                    UserEmail = email,
                    UserPassword = pass,
                    UserDoB = dateOB.Value,
                    UserAddress = address,
                    UserGender = gender,
                    UserPhone = phone,
                    UserRole = "Member"
                };
                db.Users.Add(newUser);
                db.SaveChanges();
                this.Hide();
            }
        }

        private void butRegister_Click(object sender, EventArgs e)
        {
            validate();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
        }
    }
}
