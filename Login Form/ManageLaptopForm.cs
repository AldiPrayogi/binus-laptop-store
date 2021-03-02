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
    public partial class ManageL : Form
    {
        DatabaseEntities db = new DatabaseEntities();
        public ManageL()
        {
            InitializeComponent();
            state();
        }
        public void fill()
        {
            var search = (from x in db.LaptopBrands
                          select x.LaptopBrandName).ToList();
            comboBox1.DataSource = search;
        }
        public void state()
        {
            refresh();
            fill();
            txtLaptopName.Enabled = false;
            txtlaptopID.Enabled = false;
            txtLaptopPrice.Enabled = false;
            txtLaptopSize.Enabled = false;
            txtLaptopVga.Enabled = false;
            comboBox1.Enabled = false;
            numericUpDown1.Enabled = false;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            btnInsert.Enabled = true;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
            txtlaptopID.Text = "";
            txtLaptopName.Text = "";
            txtLaptopPrice.Text = "";
            txtLaptopSize.Text = "";
            txtLaptopVga.Text = "";
            comboBox1.Text = "";
        }
        public void refresh()
        {
            var data = (from x in db.Laptops
                        select x).ToList();
            dataGridView1.DataSource = data;
        }
        private void generateID()
        {
            var result = (from x in db.Laptops
                          orderby x.LaptopID descending
                          select x.LaptopID).FirstOrDefault();
            String newID;
            if (result == null)
            {
                newID = "LP001";
            }
            else
            {
                int id = Int32.Parse(result.Substring(2, 3));
                id++;
                newID = result.Substring(0, 2) + id.ToString("d3");
            }
            txtlaptopID.Text = newID;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtlaptopID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtLaptopName.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            string LBrandID = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            var search = (from x in db.LaptopBrands
                          where x.LaptopBrandID == LBrandID
                          select x.LaptopBrandName).FirstOrDefault();
            comboBox1.SelectedItem = search;
            txtLaptopSize.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtLaptopVga.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            numericUpDown1.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtLaptopPrice.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
        }
        

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string id = txtlaptopID.Text;
            string name = txtLaptopName.Text;
            string brand = comboBox1.SelectedItem.ToString();
            string size = txtLaptopSize.Text;
            string vga = txtLaptopVga.Text;
            string ram = numericUpDown1.Value.ToString();
            string price = txtLaptopPrice.Text;
            if (id == "")
            {
                MessageBox.Show("Please select data first!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                btnInsert.Enabled = false;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
                txtlaptopID.Enabled = false;
                txtLaptopName.Enabled = true;
                txtLaptopPrice.Enabled = true;
                txtLaptopSize.Enabled = true;
                txtLaptopVga.Enabled = true;
                comboBox1.Enabled = true;
                numericUpDown1.Enabled = true;
                btnSave.Enabled = true;
                btnCancel.Enabled = true;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var id = txtlaptopID.Text;
            var name = txtLaptopName.Text;
            var brand = comboBox1.SelectedItem.ToString();
            var size = txtLaptopSize.Text;
            var vga = txtLaptopVga.Text;
            var ram = numericUpDown1.Value.ToString();
            var price = txtLaptopPrice.Text;
            if (id == "" || name == "")
            {
                MessageBox.Show("Please select data first!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                if (MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                System.Windows.Forms.DialogResult.Yes)
                {
                    Laptop deletelaptopform = (from x in db.Laptops
                                               where x.LaptopID == id
                                               select x).FirstOrDefault();
                    db.Laptops.Remove(deletelaptopform);
                    db.SaveChanges();
                    refresh();
                }
                state();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string ID = txtlaptopID.Text;
            string Name = txtLaptopName.Text;
            string Brand = comboBox1.Text;
            string BrandID = (from x in db.LaptopBrands
                          where x.LaptopBrandName == Brand
                          select x.LaptopBrandID).FirstOrDefault();
            string size = txtLaptopSize.Text;
            string VGA = txtLaptopVga.Text;
            string RAM = numericUpDown1.Value.ToString();
            string price = txtLaptopPrice.Text;
            if (Name == "")
            {
                MessageBox.Show("Laptop name must be filled!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (Brand == "")
            {
                MessageBox.Show("Laptop Brand must be chosen!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (size == "")
            {
                MessageBox.Show("Laptop size must be filled!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (VGA == "")
            {
                MessageBox.Show("Laptop VGA must be filled!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (RAM == "")
            {
                MessageBox.Show("Laptop RAM must be filled!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (Int32.Parse(RAM) < 2)
            {
                MessageBox.Show("Laptop RAM must be 2 or more!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (price == "")
            {
                MessageBox.Show("Price must be filled!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (isNumeric(price) == false)
            {
                MessageBox.Show("Laptop price must be numeric!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var search = (from x in db.Laptops
                              where x.LaptopID == ID
                              select x).FirstOrDefault();
                if (search != null)
                {
                    Laptop updateLaptop = (from x in db.Laptops
                                           where x.LaptopID == ID
                                           select x).FirstOrDefault();
                    updateLaptop.LaptopName = Name;
                    updateLaptop.LaptopBrandID = BrandID;
                    updateLaptop.LaptopPrice = Int32.Parse(price);
                    updateLaptop.LaptopRAM = RAM;
                    updateLaptop.LaptopSize = size;
                    updateLaptop.LaptopVGA = VGA;
                }
                else
                {
                    Laptop insertLaptop = new Laptop()
                    {
                        LaptopID = ID,
                        LaptopBrandID = BrandID,
                        LaptopName = Name,
                        LaptopPrice = Int32.Parse(price),
                        LaptopRAM = RAM,
                        LaptopSize = size,
                        LaptopVGA = VGA
                    };
                    db.Laptops.Add(insertLaptop);
                }
                db.SaveChanges();
                MessageBox.Show("Data inserted/updated succesfully", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                state();
            }
        }

        private bool isNumeric(string noHP)
        {
            for(int i = 0; i <noHP.Length; i++)
            {
                if (noHP[i] < '0' || noHP[i] > '9')
                {
                    return false;
                }
            }
            return true;
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            btnInsert.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
            txtLaptopName.Enabled = true;
            txtLaptopName.Text = "";
            txtLaptopPrice.Enabled = true;
            txtLaptopPrice.Text = "";
            txtLaptopSize.Enabled = true;
            txtLaptopSize.Text = "";
            txtLaptopVga.Enabled = true;
            txtLaptopVga.Text = "";
            comboBox1.Enabled = true;
            comboBox1.Text = "";
            numericUpDown1.Enabled = true;
            numericUpDown1.Value = 0;
            generateID();
        }
    }
}
