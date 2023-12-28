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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace AquaBilling_System
{
    public partial class Form10 : Form
    {
        public Form10()
        {
            InitializeComponent();
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form6 f = new Form6();
            f.Show();
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            
        }

        private void Form10_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login l = new Login();
            l.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionStringProvider.ConnectionString))
                {
                    con.Open();


                    string q = "select * from Consumer";

                    using (SqlCommand cmd = new SqlCommand(q, con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);


                        dataGridView1.DataSource = dt;
                    }
                }
            }
            /*try
            {
                using (AquaEntities context = new AquaEntities(ConnectionStringProvider.ConnectionString))
                {
                    var consumers = from s in context.Consumers
                                    select s;

                    dataGridView1.DataSource = consumers.ToList();
                }
            }*/
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                bool ConsumerFound = false;

                // Iterate through the rows in the DataGridView to find the matching Supplier ID
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    // Assuming the Supplier ID is in the first column 
                    string SupplierIdInRow = row.Cells[0].Value?.ToString();

                    // Check if the Supplier ID in the row matches the one in the TextBox (case-insensitive)
                    if (string.Equals(SupplierIdInRow, textBox1.Text.Trim(), StringComparison.OrdinalIgnoreCase))
                    {
                        // Select the row and make it visible
                        row.Selected = true;
                        dataGridView1.CurrentCell = row.Cells[0]; // Assuming Supplier ID is in the first column

                        // Optionally, scroll to the selected row
                        dataGridView1.FirstDisplayedScrollingRowIndex = row.Index;

                        // Set the flag to indicate that the supplier is found
                        ConsumerFound = true;

                        break;
                    }
                }
                
                if (!ConsumerFound)
                {
                    MessageBox.Show("Consumer not found.");
                    textBox1.Clear();
                }
            }
            else
            {
                MessageBox.Show("Please enter Consumer ID to search.");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Assuming the user ID is in the "UserId" column
                int consumerToDelete = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Consumer_id"].Value);
                
                try
                {
                    using (SqlConnection con = new SqlConnection(ConnectionStringProvider.ConnectionString))
                    {
                        con.Open();

                        String p = "delete from OrderT where Consumer_id=@i";
                        String q = "delete from Consumer where Consumer_id=@d";
                        SqlCommand cmd = new SqlCommand(p, con);
                        SqlCommand cmd1 = new SqlCommand(q, con);
                        cmd.Parameters.AddWithValue("@i", consumerToDelete);
                        cmd1.Parameters.AddWithValue("@d", consumerToDelete);
                        DialogResult r = MessageBox.Show("Are you sure you want to delete this?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (r == DialogResult.Yes)
                        {
                            cmd.ExecuteNonQuery();
                            int c = cmd1.ExecuteNonQuery();

                            if (c == 0)
                            {
                                MessageBox.Show("Consumer deletion failed.");

                            }
                            else
                            {
                                MessageBox.Show("Consumer deleted successfully.");
                                dataGridView1.Refresh();
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
                MessageBox.Show("Please select an Consumer to delete.");
            }
        }
    }
}
