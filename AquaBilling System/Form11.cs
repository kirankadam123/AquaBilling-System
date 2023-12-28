using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
namespace AquaBilling_System
{
    public partial class Form11 : Form
    {
        public Form11()
        {
            InitializeComponent();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form6 f=new Form6();
            f.Show();
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            
        }

        private void Form11_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login l = new Login();
            l.Show();
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
             /*try
             {
                 using (SqlConnection con = new SqlConnection(ConnectionStringProvider.ConnectionString))
                 {
                     con.Open();


                     string q = "select * from Supplier";

                     using (SqlCommand cmd = new SqlCommand(q, con))
                     {                       
                         SqlDataAdapter da = new SqlDataAdapter(cmd);
                         DataTable dt = new DataTable();
                         da.Fill(dt);


                         dataGridView2.DataSource = dt;
                     }
                 }
             }*/
            try
            {
                using (AquaEntities context = new AquaEntities(ConnectionStringProvider.ConnectionString))
                {
                    var suppliers = from s in context.Suppliers
                                    select s;

                    dataGridView2.DataSource = suppliers.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                bool SupplierFound = false;

                // Iterate through the rows in the DataGridView to find the matching Supplier ID
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    // Assuming the Supplier ID is in the first column
                    string SupplierIdInRow = row.Cells[0].Value?.ToString();

                    // Check if the Supplier ID in the row matches the one in the TextBox (case-insensitive)
                    if (string.Equals(SupplierIdInRow, textBox1.Text.Trim(), StringComparison.OrdinalIgnoreCase))
                    {
                        // Select the row and make it visible
                        row.Selected = true;
                        dataGridView2.CurrentCell = row.Cells[0]; // Assuming Supplier ID is in the first column

                        // Optionally, scroll to the selected row
                        dataGridView2.FirstDisplayedScrollingRowIndex = row.Index;

                        // Set the flag to indicate that the supplier is found
                        SupplierFound = true;
                        break;
                    }
                }
               
                if (!SupplierFound)
                {
                    MessageBox.Show("Supplier not found.");
                    textBox1.Clear();
                }
            }
            else
            {
                MessageBox.Show("Please enter Supplier ID to search.");
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                
                int supplierToDelete = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells["Supplier_id"].Value);

                try
                {
                    /*using (SqlConnection con = new SqlConnection(ConnectionStringProvider.ConnectionString))
                    {
                        con.Open();

                        string q = "delete from Supplier where Supplier_id = @i";
                        SqlCommand cmd = new SqlCommand(q, con);
                        cmd.Parameters.AddWithValue("@i", supplierToDelete);

                        int c = cmd.ExecuteNonQuery();

                        if (c == 0)
                        {
                            MessageBox.Show("Supplier deletion failed.");

                        }
                        else
                        {
                            MessageBox.Show("Supplier deleted Successfully.");
                        }
                    }*/

                    using (AquaEntities context = new AquaEntities(ConnectionStringProvider.ConnectionString))
                    {
                        var supplier = context.Suppliers.SingleOrDefault(s => s.Supplier_id == supplierToDelete);
                        DialogResult r = MessageBox.Show("Are you sure you want to delete this?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (r == DialogResult.Yes)
                        {
                            if (supplier != null)
                            {
                                context.Suppliers.Remove(supplier);
                                context.SaveChanges();
                                MessageBox.Show("Supplier deleted successfully.");
                                dataGridView2.Refresh();
                            }
                            else
                            {
                                MessageBox.Show("Supplier not found or deletion failed.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please select an Supplier to delete.");
            }
        }
    }
}
