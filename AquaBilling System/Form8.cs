using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace AquaBilling_System
{
    public partial class Form8 : Form
    {
        string username = Login.Username;
        private DataTable dataTable = new DataTable();
        public Form8()
        {
            InitializeComponent();
            textBox1.Click += textBox1_Click;
            textBox2.Click += textBox2_Click;
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            dataTable = new DataTable();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login l = new Login();
            l.Show();
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login l = new Login();
            l.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text == "Select the info you want to update" || comboBox1.Text == "")
                {
                    MessageBox.Show("Please select the information you want to update");
                }
                else
                {
                    String i = comboBox1.Text;
                    if (textBox1.Text == "Please Enter new Available Quantity" || textBox1.Text == "Please Enter new Address" || textBox1.Text == "Please Enter new Name" || textBox1.Text == "" || textBox1.Text == "Please Enter new Contact No" || textBox1.Text == "Please Enter new Rate.")
                    {
                        MessageBox.Show("Please enter new " + i);
                    }
                    else
                    {


                        using (SqlConnection con = new SqlConnection(ConnectionStringProvider.ConnectionString))
                        {
                            con.Open();
                            String q = null;
                            if (comboBox1.Text == "Name")
                                q = "Update Supplier set Supplier_Name=@n where Supplier_id=@i";
                            if (comboBox1.Text == "Address")
                                q = "Update Supplier set Address=@n where Supplier_id=@i";
                            if (comboBox1.Text == "Contact No")
                                q = "Update Supplier set Contact_No=@n where Supplier_id=@i";
                            if (comboBox1.Text == "Available Quantity")
                                q = "Update Supplier set Available_Quantity(in ltr)=@n where Supplier_id=@i";
                            if (comboBox1.Text == "Rate per ltr")
                                q = "Update Supplier set Rate_per_ltr=@n where Supplier_id=@i";

                            using (SqlCommand cmd = new SqlCommand(q, con))
                            {
                                cmd.Parameters.AddWithValue("@n", textBox1.Text);
                                cmd.Parameters.AddWithValue("@i", username);

                                int c = cmd.ExecuteNonQuery();

                                if (c == 0)
                                    MessageBox.Show(" Account not found. ");
                                else
                                {
                                    MessageBox.Show("New " + i + " updated Successfully.");
                                    textBox1.Text = ""; comboBox1.Text = "Please select the information you want to update";
                                }
                            }

                        }
                    }
                }
            }
            catch (Exception e1) { }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            string s1 = comboBox1.SelectedItem?.ToString();
            if (s1 == "Name")
                textBox1.Text = "Please Enter new Name";
            if (s1 == "Address")
                textBox1.Text = "Please Enter new Address";
            if (s1 == "Contact No")
                textBox1.Text = "Please Enter new Contact No";
            if (s1 == "Available Quantity")
                textBox1.Text = "Please Enter new Available Quantity";
            if (s1 == "Rate per ltr")
                textBox1.Text = "Please Enter new Rate.";


        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Are you sure you want to delete this?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(ConnectionStringProvider.ConnectionString))
                    {
                        con.Open();
                        
                        String q = "delete from Supplier where Supplier_id=@i";

                        using (SqlCommand cmd = new SqlCommand(q, con))
                        {
                            cmd.Parameters.AddWithValue("@i", username);
                            int c = cmd.ExecuteNonQuery();

                            if (c == 0)
                                MessageBox.Show(" Account not found. ");
                            else
                            {
                                MessageBox.Show("Your account deleted successfully.");
                                this.Hide();
                                Login l = new Login();
                                l.Show();
                                this.Close();
                            }
                        }
                    }
                }
                catch (Exception e1) { }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionStringProvider.ConnectionString))
                {
                    con.Open();


                    string q = "select * from OrderT where Supplier_Name in (select Supplier_Name from Supplier where Supplier_id = @i) and Order_Status is NULL";

                    using (SqlCommand cmd = new SqlCommand(q, con))
                    {
                        cmd.Parameters.AddWithValue("@i", username);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);


                        dataGridView1.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

            try
            {
                dataTable = (DataTable)dataGridView1.DataSource;
                if (dataTable != null)
                {
                    // Get changes from the DataTable
                    DataTable changes = dataTable.GetChanges();

                    if (changes != null)
                    {
                        
                        using (SqlConnection con = new SqlConnection(ConnectionStringProvider.ConnectionString))
                        {
                            con.Open();
                            String q = "select * from OrderT";
                            
                            SqlDataAdapter adapter = new SqlDataAdapter(q, con);

                            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                            adapter.UpdateCommand = commandBuilder.GetUpdateCommand();
                            adapter.Update(changes);

                            // Accept changes in the DataTable
                            dataTable.AcceptChanges();                          

                            MessageBox.Show("Order Status is updated Successfully");

                            int s = 0;
                            foreach (DataRow ur in changes.Rows)
                            {
                                string os = ur["Order_Status"].ToString();

                                if (os.Equals("Accept", StringComparison.OrdinalIgnoreCase))                                    
                                {
                                    // Retrieve the updated row's quantity column value
                                    int qu = Convert.ToInt32(ur["Quantity"]);
                                    s = s + qu;
                                    MessageBox.Show(" " + s);
                                }
                            }
                            String p = "update Supplier set [Available_Quantity(in ltr)]=[Available_Quantity(in ltr)]-@s where Supplier_id=@i";
                            using (SqlCommand cmd = new SqlCommand(p, con))
                            {
                                cmd.Parameters.AddWithValue("@s", s);
                                cmd.Parameters.AddWithValue("@i", username);
                                cmd.ExecuteNonQuery();
                               
                            }


                        }
                    }
                    else
                    {
                        MessageBox.Show("No changes to save.");
                    }
                }
                else
                {
                    MessageBox.Show("DataTable is null. Please check your data source.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving changes: {ex.Message}");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
                if (!string.IsNullOrEmpty(textBox2.Text.Trim()))
                
                {
                    bool orderFound = false;

                    // Iterate through the rows in the DataGridView to find the matching Order ID
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        // Assuming the Order ID is in the first column
                        string orderIdInRow = row.Cells[0].Value?.ToString();

                        // Check if the Order ID in the row matches the one in the TextBox
                        if (orderIdInRow == textBox2.Text.Trim())
                        {
                            // Select the row and make it visible
                            row.Selected = true;
                            dataGridView1.CurrentCell = row.Cells[0]; // Assuming Order ID is in the first column

                            // Optionally, scroll to the selected row
                            dataGridView1.FirstDisplayedScrollingRowIndex = row.Index;

                            // Set the flag to indicate that the order is found
                            orderFound = true;
                            break;
                        }
                    }

                    if (!orderFound)
                    {
                        MessageBox.Show("This order does not belong to you.");
                    }
                }
else
                {
                    MessageBox.Show("Please enter Order ID to search.");
                }

            }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionStringProvider.ConnectionString))
                {
                    con.Open();


                    string q = "select * from OrderT where Supplier_Name in (select Supplier_Name from Supplier where Supplier_id = @i) and Order_Status in ('ACCEPT', 'REFUSE')";

                    using (SqlCommand cmd = new SqlCommand(q, con))
                    {
                        cmd.Parameters.AddWithValue("@i", username);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);


                        dataGridView1.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}


    

    


