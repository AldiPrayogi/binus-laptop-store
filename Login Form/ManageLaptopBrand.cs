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
    public partial class ManageLB : Form
    {
        DatabaseEntities db = new DatabaseEntities();
        public ManageLB()
        {
            InitializeComponent();
            refresh();
            state();
        }
        public void state()
        {
            refresh();
            BrandNameTxt.Enabled = false;
            BrandIdTxt.Enabled = false;
            saveBtn.Enabled = false;
            cancelBtn.Enabled = false;
            insertBtn.Enabled = true;
            updateBtn.Enabled = true;
            deleteBtn.Enabled = true;
            BrandIdTxt.Text = "";
            BrandNameTxt.Text = "";
        }

        public void refresh()
        {
            var data = (from x in db.LaptopBrands
                        select x).ToList();
            dataGridView1.DataSource = data;
        }

        private void insertBtn_Click(object sender, EventArgs e)
        {
            insertBtn.Enabled = false;
            updateBtn.Enabled = false;
            deleteBtn.Enabled = false;
            saveBtn.Enabled = true;
            cancelBtn.Enabled = true;
            BrandNameTxt.Enabled = true;
            generateID();
        }
        private void generateID()
        {
            var result = (from x in db.LaptopBrands
                          orderby x.LaptopBrandID descending
                          select x.LaptopBrandID).FirstOrDefault();
            String newID;
            if (result == null)
            {
                newID = "BN001";
            }
            else
            {
                int id = Int32.Parse(result.Substring(2, 3));
                id++;
                newID = result.Substring(0, 2) + id.ToString("d3");
            }
            BrandIdTxt.Text = newID;
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            var id = BrandIdTxt.Text;
            var name = BrandNameTxt.Text;
            if (id == "" || name == "")
            {
                MessageBox.Show("Please select data first!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                insertBtn.Enabled = false;
                updateBtn.Enabled = false;
                deleteBtn.Enabled = false;
                BrandIdTxt.Enabled = false;
                BrandNameTxt.Enabled = true;
                saveBtn.Enabled = true;
                cancelBtn.Enabled = true;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            BrandIdTxt.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            BrandNameTxt.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            var id = BrandIdTxt.Text;
            var name = BrandNameTxt.Text;
            if (id == "" || name == "")
            {
                MessageBox.Show("Please select data first!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                System.Windows.Forms.DialogResult.Yes)
                {
                    LaptopBrand deleteBrand = (from x in db.LaptopBrands
                                               where x.LaptopBrandID == id
                                               select x).FirstOrDefault();
                    db.LaptopBrands.Remove(deleteBrand);
                    db.SaveChanges();
                    refresh();
                }
                state();
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            String name = BrandNameTxt.Text;
            String id = BrandIdTxt.Text;
            var search = (from x in db.LaptopBrands
                          where name == x.LaptopBrandName
                          select x.LaptopBrandName).FirstOrDefault();
            if (name.Length < 2 || name.Length > 15)
            {
                MessageBox.Show("Brand name length must be between 2-15 characters", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (search != null)
            {
                MessageBox.Show("Brand name already exists!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var searchid = (from x in db.LaptopBrands
                                where x.LaptopBrandID == id
                                select x).FirstOrDefault();
                if (searchid != null)
                {
                    LaptopBrand updateBrand = (from x in db.LaptopBrands
                                               where x.LaptopBrandID == id
                                               select x).FirstOrDefault();
                    updateBrand.LaptopBrandName = name;
                }
                else
                {
                    LaptopBrand insertBrand = new LaptopBrand()
                    {
                        LaptopBrandID = id,
                        LaptopBrandName = name
                    };
                    db.LaptopBrands.Add(insertBrand);
                }
                db.SaveChanges();
                refresh();
                MessageBox.Show("Data inserted/updated succesfully", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                state();
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            state();
        }
    }
}
