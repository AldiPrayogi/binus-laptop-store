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
    public partial class ViewTrans : Form
    {
        DatabaseEntities db = new DatabaseEntities();
        public ViewTrans()
        {
            InitializeComponent();
            refresh();
        }

        public void refresh()
        {
            var data = (from x in db.HeaderTransactions
                        select x).ToList();
            dataGridViewHeader.DataSource = data;
        }

        public void state()
        {
            refresh();
            cmbMonth.Text = "";
            dataGridViewDetail.DataSource = null;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            state();
        }

        private void dataGridViewHeader_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String click = dataGridViewHeader.Rows[e.RowIndex].Cells[0].Value.ToString();
            var data = (from x in db.DetailTransactions
                        where x.TransactionID.Equals(click)
                        select x).ToList();
            dataGridViewDetail.DataSource = data;
        }

        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMonth.Text.Equals("January"))
            {
                var data = (from x in db.HeaderTransactions
                            where x.TransactionDate.Substring(3, 2) == "01"
                            select x).ToList();
                dataGridViewHeader.DataSource = data;
            }
            else if (cmbMonth.Text.Equals("February"))
            {
                var data = (from x in db.HeaderTransactions
                            where x.TransactionDate.Substring(3, 2) == "02"
                            select x).ToList();
                dataGridViewHeader.DataSource = data;
            }
            else if (cmbMonth.Text.Equals("March"))
            {
                var data = (from x in db.HeaderTransactions
                            where x.TransactionDate.Substring(3, 2) == "03"
                            select x).ToList();
                dataGridViewHeader.DataSource = data;
            }
            else if (cmbMonth.Text.Equals("April"))
            {
                var data = (from x in db.HeaderTransactions
                            where x.TransactionDate.Substring(3, 2) == "04"
                            select x).ToList();
                dataGridViewHeader.DataSource = data;
            }
            else if (cmbMonth.Text.Equals("May"))
            {
                var data = (from x in db.HeaderTransactions
                            where x.TransactionDate.Substring(3, 2) == "05"
                            select x).ToList();
                dataGridViewHeader.DataSource = data;
            }
            else if (cmbMonth.Text.Equals("June"))
            {
                var data = (from x in db.HeaderTransactions
                            where x.TransactionDate.Substring(3, 2) == "06"
                            select x).ToList();
                dataGridViewHeader.DataSource = data;
            }
            else if (cmbMonth.Text.Equals("July"))
            {
                var data = (from x in db.HeaderTransactions
                            where x.TransactionDate.Substring(3, 2) == "07"
                            select x).ToList();
                dataGridViewHeader.DataSource = data;
            }
            else if (cmbMonth.Text.Equals("August"))
            {
                var data = (from x in db.HeaderTransactions
                            where x.TransactionDate.Substring(3, 2) == "08"
                            select x).ToList();
                dataGridViewHeader.DataSource = data;
            }
            else if (cmbMonth.Text.Equals("September"))
            {
                var data = (from x in db.HeaderTransactions
                            where x.TransactionDate.Substring(3, 2) == "09"
                            select x).ToList();
                dataGridViewHeader.DataSource = data;
            }
            else if (cmbMonth.Text.Equals("October"))
            {
                var data = (from x in db.HeaderTransactions
                            where x.TransactionDate.Substring(3, 2) == "10"
                            select x).ToList();
                dataGridViewHeader.DataSource = data;
            }
            else if (cmbMonth.Text.Equals("November"))
            {
                var data = (from x in db.HeaderTransactions
                            where x.TransactionDate.Substring(3, 2) == "11"
                            select x).ToList();
                dataGridViewHeader.DataSource = data;
            }
            else
            {
                var data = (from x in db.HeaderTransactions
                            where x.TransactionDate.Substring(3, 2) == "12"
                            select x).ToList();
                dataGridViewHeader.DataSource = data;
            }
        }
    }
}
