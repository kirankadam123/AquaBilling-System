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
    public partial class Form7 : Form
    {
        string username = Login.Username;
        public Form7()
        {
            InitializeComponent();
            textBox1.Click += textBox1_Click;
            textBox2.Click += textBox2_Click;
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form9 f = new Form9();
            f.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionStringProvider.ConnectionString))
                {
                    con.Open();
                    
                    String p = "delete from OrderT where Consumer_id=@i";
                    String q = "delete from Consumer where Consumer_id=@d";
                    SqlCommand cmd = new SqlCommand(p, con);
                    SqlCommand cmd1 = new SqlCommand(q, con);
                    cmd.Parameters.AddWithValue("@i", username);
                    cmd1.Parameters.AddWithValue("@d", username);
                    DialogResult r = MessageBox.Show("Are you sure you want to delete this?","Delete Confirmation",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                    if (r == DialogResult.Yes)
                    {
                        cmd.ExecuteNonQuery();
                        int c = cmd1.ExecuteNonQuery();

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

        private void button5_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(ConnectionStringProvider.ConnectionString))
            {
                con.Open();
                String q = "select Order_Status from OrderT where Order_id=@o and Consumer_id=@i";
                using (SqlCommand cmd = new SqlCommand(q, con))
                {
                    cmd.Parameters.AddWithValue("@o", textBox2.Text);
                    cmd.Parameters.AddWithValue("@i", username);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (string.IsNullOrEmpty(textBox2.Text) || textBox2.Text == "Enter Order Id")
                        {
                            MessageBox.Show("Enter Order Id.");
                        }
                        else
                        {


                            if (dr.Read())
                            {
                                if (dr.IsDBNull(0))
                                {
                                    MessageBox.Show("Order Status is not updated yet by Supplier.");
                                }
                                else
                                {
                                    if (dr.GetString(0).Equals("Accept", StringComparison.OrdinalIgnoreCase))
                                        MessageBox.Show("Order accepted.");
                                    else
                                    if (dr.GetString(0).Equals("Refuse", StringComparison.OrdinalIgnoreCase))
                                        MessageBox.Show("Sorry for the Inconvinience. Your order is refused by Supplier due to some emergency.");
                                    else
                                    if (dr.GetString(0).Equals("Order canceled", StringComparison.OrdinalIgnoreCase))
                                        MessageBox.Show("Your order was canceled by you.");

                                }
                            }
                            else
                            {
                                MessageBox.Show("Order not found.");
                                textBox2.Clear();
                            }

                            }
                    }
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
        }

        private void Form7_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Login l = new Login();
            l.ShowDialog();
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login l = new Login();
            l.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            {
                if (comboBox1.Text == "Select the info you want to update" || comboBox1.Text == "")
                {
                    MessageBox.Show("Please select the information you want to update");
                }
                else
                {
                    String i = comboBox1.Text;
                    if (textBox1.Text == "Please Enter new Address" || textBox1.Text == "Please Enter new Name" || textBox1.Text == "" || textBox1.Text == "Please Enter new Contact No")
                    {
                        MessageBox.Show("Please enter new " + i);
                    }
                    else
                    {
                       // string username = UserSession.Username;

                        using (SqlConnection con = new SqlConnection(ConnectionStringProvider.ConnectionString))
                        {
                            con.Open();
                            String q = null;
                            if (comboBox1.Text == "Name")
                                q = "Update Consumer set Consumer_Name=@n where Consumer_id=@i";
                            if (comboBox1.Text == "Address")
                                q = "Update Consumer set Address=@n where Consumer_id=@i";
                            if (comboBox1.Text == "Contact No")
                                q = "Update Consumer set Contact_No=@n where Consumer_id=@i";
                                 
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
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }
    }
}
