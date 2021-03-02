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
    public partial class ManageU : Form
    {

        DatabaseEntities db = new DatabaseEntities();
        public ManageU()
        {
            InitializeComponent();
            refresh();
            state();
        }

        public void state()
        {
            refresh();
            txtUsName.Enabled = false;
            txtUsID.Enabled = false;
            txtUsPass.Enabled = false;
            txtUsPhone.Enabled = false;
            txtUsEmail.Enabled = false;
            txtUsAddress.Enabled = false;
            txtdob.Enabled = false;
            txtCBoxRole.Enabled = false;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            txtUsID.Text = "";
            txtUsName.Text = "";
            txtUsPass.Text = "";
            txtUsPhone.Text = "";
            txtUsEmail.Text = "";
            txtUsAddress.Text = "";
            txtCBoxRole.Text = "";
        }
        public void refresh()
        {
            var data = (from x in db.Users
                        select x).ToList();
            dataGridView1.DataSource = data;
        }
        private void generateID()
        {
            var result = (from x in db.Users
                          orderby x.UserID descending
                          select x.UserID).FirstOrDefault();
            String newID;
            if (result == null)
            {
                newID = "US001";
            }
            else
            {
                int id = Int32.Parse(result.Substring(2, 3));
                id++;
                newID = result.Substring(0, 2) + id.ToString("d3");
            }
            txtUsID.Text = newID;
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            btnInsert.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
            txtUsName.Enabled = true;
            txtUsPass.Enabled = true;
            txtUsPhone.Enabled = true;
            txtUsEmail.Enabled = true;
            txtUsAddress.Enabled = true;
            txtdob.Enabled = true;
            txtCBoxRole.Enabled = true;
            radMale.Enabled = true;
            radFemale.Enabled = true;
            generateID();
        }
        private bool isNumeric(String noTelp)
        {
            for (int i = 0; i < noTelp.Length; i++)
            {
                if (noTelp[i] < '0' || noTelp[i] > '9')
                {
                    return false;
                }
            }
            return true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var id = txtUsID.Text;
            var name = txtUsName.Text;
            var email = txtUsEmail.Text;


            if (id == "" || name == "")
            {
                MessageBox.Show("Please select data first!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                btnInsert.Enabled = false;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
                txtUsID.Enabled = false;
                txtUsName.Enabled = true;
                txtUsPass.Enabled = true;
                txtUsPhone.Enabled = true;
                txtUsEmail.Enabled = true;
                txtUsAddress.Enabled = true;
                txtdob.Enabled = true;
                txtCBoxRole.Enabled = true;
                btnSave.Enabled = true;
                btnCancel.Enabled = true;

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var id = txtUsID.Text;
            var name = txtUsName.Text;
            var email = txtUsEmail.Text;
            var telp = txtUsPhone.Text;
            var alamat = txtUsAddress.Text;
            var pass = txtUsPass.Text;
            var role = txtCBoxRole.Text;
            var gender = "";
            var dob = txtdob;

            if (id == "" || name == "")
            {
                MessageBox.Show("Please select data first!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                if (MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                System.Windows.Forms.DialogResult.Yes)
                {
                    User userpengguna = (from x in db.Users
                                         where x.UserID == id
                                         select x).FirstOrDefault();
                    db.Users.Remove(userpengguna);
                    db.SaveChanges();
                    refresh();
                }
                state();
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            String id = txtUsID.Text;
            String nama = txtUsName.Text;
            String email = txtUsEmail.Text;
            String gender = "";
            String dob = txtdob.Text;
            String noTelp = txtUsPhone.Text;
            String alamat = txtUsAddress.Text;
            String password = txtUsPass.Text;
            String role = txtCBoxRole.Text;
            var search = (from x in db.Users
                          where x.UserName == nama
                          select x).FirstOrDefault();
            if (search != null)
            {
                if (nama == "")
                {
                    MessageBox.Show("Username must be filled!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (email == "")
                {
                    MessageBox.Show("Email must be filled!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (!(email.Contains("@") && email.Contains(".")))
                {
                    MessageBox.Show("Email must contain '@' and '.'", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (email.StartsWith("@") || email.StartsWith(".")
                   || email.EndsWith("@") || email.EndsWith("."))
                {
                    MessageBox.Show("Email cannot start with '@' or '.'", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if ((email.IndexOf("@") + 1) == email.IndexOf("."))
                {
                    MessageBox.Show("Email cannot start with '@' or '.'", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (email.IndexOf("@") + 1 == email.IndexOf("."))
                {
                    MessageBox.Show("'@' and '.' cannot be placed beside each other", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (!(radMale.Checked || radFemale.Checked))
                {
                    MessageBox.Show("Gender must be chosen!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (txtdob.Value.Date > DateTime.Now.Date)
                {
                    MessageBox.Show("DOB cannot be greater than current date!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (password == "")
                {
                    MessageBox.Show("Password must be filled!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (password.Length < 5)
                {
                    MessageBox.Show("Password length must be 5 characters of more", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (noTelp == "")
                {
                    MessageBox.Show("Phone number must be filled!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (noTelp.Length != 12)
                {
                    MessageBox.Show("Phone number must be 12 digits!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (!(isNumeric(noTelp)))
                {
                    MessageBox.Show("Phone must be numeric!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (alamat == "")
                {
                    MessageBox.Show("Address must be filled!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (!(alamat.Contains("Street")))
                {
                    MessageBox.Show("Address must contain 'Street'", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (role == "")
                {
                    MessageBox.Show("Role must be chosen", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (radMale.Checked)
                    {
                        gender = "Male";
                    }
                    else
                    {
                        gender = "Female";
                    }
                    User updateUser = (from x in db.Users
                                       where x.UserID == id
                                       select x).FirstOrDefault();
                    updateUser.UserName = nama;
                    updateUser.UserPassword = password;
                    updateUser.UserPhone = noTelp;
                    updateUser.UserRole = role;
                    updateUser.UserEmail = email;
                    updateUser.UserGender = gender;
                    updateUser.UserDoB = txtdob.Value;
                    updateUser.UserAddress = alamat;
                    db.SaveChanges();
                    refresh();
                    MessageBox.Show("Data inserted/updated succesfully", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    state();
                    generateID();
                }
            }
            else {
                if (nama == "")
                {
                    MessageBox.Show("Username must be filled!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (search != null)
                {
                    MessageBox.Show("Username already exists!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (email == "")
                {
                    MessageBox.Show("Email must be filled!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (!(email.Contains("@") && email.Contains(".")))
                {
                    MessageBox.Show("Email must contain '@' and '.'", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (email.StartsWith("@") || email.StartsWith(".")
                   || email.EndsWith("@") || email.EndsWith("."))
                {
                    MessageBox.Show("Email cannot start with '@' or '.'", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (email.IndexOf("@") + 1 == email.IndexOf("."))
                {
                    MessageBox.Show("'@' and '.' cannot be placed beside each other", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (!(radMale.Checked || radFemale.Checked))
                {
                    MessageBox.Show("Gender must be chosen!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (txtdob.Value.Date > DateTime.Now.Date)
                {
                    MessageBox.Show("DOB cannot be greater than current date!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (password == "")
                {
                    MessageBox.Show("Password must be filled!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (password.Length < 5)
                {
                    MessageBox.Show("Password length must be 5 characters of more", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (noTelp == "")
                {
                    MessageBox.Show("Phone number must be filled!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (noTelp.Length != 12)
                {
                    MessageBox.Show("Phone number must be 12 digits!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (!(isNumeric(noTelp)))
                {
                    MessageBox.Show("Phone must be numeric!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (alamat == "")
                {
                    MessageBox.Show("Address must be filled!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (!(alamat.Contains("Street")))
                {
                    MessageBox.Show("Address must contain 'Street'", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (role == "")
                {
                    MessageBox.Show("Role must be chosen", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
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
                        UserName = nama,
                        UserEmail = email,
                        UserPassword = password,
                        UserDoB = txtdob.Value,
                        UserGender = gender,
                        UserAddress = alamat,
                        UserID = id,
                        UserPhone = noTelp,
                        UserRole = role
                    };
                    MessageBox.Show(nama);
                    MessageBox.Show(email);
                    MessageBox.Show(txtdob.Value.ToString());
                    MessageBox.Show(gender);
                    MessageBox.Show(alamat);
                    db.Users.Add(newUser);
                    db.SaveChanges();
                    refresh();
                    MessageBox.Show("Data inserted/updated succesfully", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    state();
                    generateID();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            state();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtUsID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtUsName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            String gender = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            if (gender == "Male")
                radMale.Checked = true;
            else
                radFemale.Checked = true;
            txtUsEmail.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtdob.Value = DateTime.Parse(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
            txtUsPhone.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtUsAddress.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            txtUsPass.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            txtCBoxRole.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
        }
    }
}
