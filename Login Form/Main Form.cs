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
    public partial class MainFrmStff : Form
    {
        public string pass;
        public string userID;
        LoginForm loginForm;
        ChgPass changepass;
        ManageLB MlaptopBrand;
        ManageL MLaptop;
        ManageU MUser;
        ViewTrans Transactions;
        ViewHistory His;
        BuyForm Buy;
        public MainFrmStff(String role, String password, String ID)
        {
            InitializeComponent();
            if (role == "Admin")
            {
                buyToolStripMenuItem.Visible = false;
                historyToolStripMenuItem.Visible = false;
            }
            else
            {
                manageToolStripMenuItem.Visible = false;
                transactionToolStripMenuItem.Visible = false;
            }
            pass = password;
            userID = ID; 
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (changepass == null || changepass.IsDisposed)
            {
                changepass = new ChgPass(pass);
                changepass.MdiParent = this;
                changepass.Show();
            }
        }

        private void laptopBrandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MlaptopBrand == null || MlaptopBrand.IsDisposed)
            {
                MlaptopBrand = new ManageLB();
                MlaptopBrand.MdiParent = this;
                MlaptopBrand.Show();
            }
        }

        private void laptopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MLaptop == null || MLaptop.IsDisposed)
            {
                MLaptop = new ManageL();
                MLaptop.MdiParent = this;
                MLaptop.Show();
            }
        }

        private void userToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MUser == null || MUser.IsDisposed)
            {
                MUser = new ManageU();
                MUser.MdiParent = this;
                MUser.Show();
            }
        }

        private void transactionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Transactions == null || Transactions.IsDisposed)
            {
                Transactions = new ViewTrans();
                Transactions.MdiParent = this;
                Transactions.Show();
            }
        }

        private void historyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (His == null || His.IsDisposed)
            {
                His = new ViewHistory(userID);
                His.MdiParent = this;
                His.Show();
            }
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loginForm = new LoginForm();
            this.Hide();
            loginForm.ShowDialog();
        }

        private void buyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Buy == null || Buy.IsDisposed)
            {
               
                Buy = new BuyForm(userID);
                Buy.MdiParent = this;
                Buy.Show();
            }
        }
        
    }
}