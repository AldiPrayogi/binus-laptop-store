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
    public partial class BuyForm : Form
    {
        public string userID;
        int cartCount = 0;
        public string ID = "";
        DatabaseEntities db = new DatabaseEntities();
        int x = 0;
        public BuyForm(String ID)
        {
            InitializeComponent();
            refresh();
            state();
            userID = ID;
        }

        public void state()
        {
            txtLaptopID.Enabled = false;
            txtLaptopName.Enabled = false;
            txtPrice.Enabled = false;
            txtSelectedLaptopID.Text = "";
            txtSelectedLaptopID.Enabled = false;
            dataGridViewCart.Rows.Clear();
            dataGridViewCart.Refresh();
            lblPrice.Text = "";
        }

        public void refresh()
        {
            var data = (from x in db.Laptops
                        select x).ToList();
            dataGridViewLaptop.DataSource = data;
        }
        
        private void dataGridViewLaptop_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtLaptopID.Text = dataGridViewLaptop.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtLaptopName.Text = dataGridViewLaptop.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtPrice.Text = dataGridViewLaptop.Rows[e.RowIndex].Cells[6].Value.ToString();
        }

        private void btnAddCart_Click(object sender, EventArgs e)
        {
            if (txtLaptopID.Text == "")
            {
                MessageBox.Show("Please select data first!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (numericQty.Value < 1)
            {
                MessageBox.Show("Quantity must be more than or equals to 1!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                x = x + (Int32.Parse(txtPrice.Text) * Int32.Parse(numericQty.Value.ToString()));
                string LaptopID = txtLaptopID.Text;
                string Name = txtLaptopName.Text;
                string Qty = numericQty.Value.ToString();
                string Price = txtPrice.Text;
                string SubTotal = (Int32.Parse(txtPrice.Text)*numericQty.Value).ToString();
                string[] row = { LaptopID, Name, Qty, Price, SubTotal };
                dataGridViewCart.Rows.Add(row);
                lblPrice.Text = "Rp. " + x.ToString();
                dataGridViewCart.Refresh();
                cartCount++;
            }
        }

        private void dataGridViewCart_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtSelectedLaptopID.Text = dataGridViewCart.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (txtSelectedLaptopID.Text == "")
            {
                MessageBox.Show("Please select data first!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int deletePrice;
                for (int i = 0; i < cartCount; i++)
                {
                    if (string.Equals(dataGridViewCart[0, i].Value as string, txtSelectedLaptopID.Text))
                    {
                        deletePrice = Int32.Parse(dataGridViewCart.Rows[i].Cells[4].Value.ToString());
                        x = x - deletePrice;
                        lblPrice.Text = "Rp. " + x.ToString();
                        dataGridViewCart.Rows.RemoveAt(i);
                        cartCount--;
                        i--;
                    }
                }
                txtSelectedLaptopID.Text = "";
            }
        }
        public void generateID()
        {
            var result = (from x in db.HeaderTransactions
                          orderby x.TransactionID descending
                          select x.TransactionID).FirstOrDefault();
            String newID;
            if (result == null)
            {
                newID = "TR001";
            }
            else
            {
                int id = Int32.Parse(result.Substring(2, 3));
                id++;
                newID = result.Substring(0, 2) + id.ToString("d3");
            }
            ID = newID;
        }
        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            if (dataGridViewCart.Rows[0].Cells[0].Value == null)
            {
                MessageBox.Show("No laptop in cart!!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                generateID();
                MessageBox.Show(DateTime.Now.Date.ToShortDateString());
                HeaderTransaction insertHeader = new HeaderTransaction()
                {
                    TransactionID = ID,
                    TransactionDate = DateTime.Now.Date.ToString(),
                    UserID = userID
                };
                db.HeaderTransactions.Add(insertHeader);
                List<DetailTransaction> insertDetail = new List<DetailTransaction>();
                for (int i = 0; i < cartCount; i++)
                {
                    DetailTransaction f = new DetailTransaction();
                    f.TransactionID = ID;
                    f.LaptopID = dataGridViewCart.Rows[i].Cells[0].ToString();
                    f.Quantity = Int32.Parse(dataGridViewCart.Rows[i].Cells[2].Value.ToString());
                    insertDetail.Add(f);
                }
                db.DetailTransactions.AddRange(insertDetail);
                db.SaveChanges();

                MessageBox.Show("Data inserted/updated succesfully", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                state();
            }
        }
    }
}
