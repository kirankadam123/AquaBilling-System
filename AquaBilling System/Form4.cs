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

namespace AquaBilling_System
{
    public partial class Form4 : Form
    {
       
        public Form4()
        {
            InitializeComponent();
            
        }

        void Form4_Load(object sender, EventArgs e)
        {

            textBox8.Text = GenerateRandomCaptcha();
        }
            string GenerateRandomCaptcha()
            {
                string s = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%&";
                Random r = new Random();
                String c = "";

             for(int i=0;i<6;i++)
             {
                int n = r.Next(s.Length);
                c += s[n];
             }
            return c;
            }
        

      
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login l = new Login();
            l.Show();
            this.Close();
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            string password = textBox6.Text;

            // Check if the password meets the minimum length requirement
            if (password.Length >= 6)
            {
                MessageBox.Show("Password must be at least 6 characters long.");
                return;  // Exit the method without proceeding further
            }
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionStringProvider.ConnectionString))
                {
                    con.Open();
                    string q = "insert into Consumer(Consumer_Name,Address,Contact_No,Category,Password) values(@name,@address,@cn,@cat,@pass)";

                    if (textBox7.Text.Equals(textBox8.Text, StringComparison.Ordinal))
                    {

                        using (SqlCommand cmd = new SqlCommand(q, con))
                        {
                            cmd.Parameters.AddWithValue("@name", textBox1.Text);
                            cmd.Parameters.AddWithValue("@address", textBox2.Text);
                            cmd.Parameters.AddWithValue("@cn", textBox3.Text);
                            cmd.Parameters.AddWithValue("@cat", comboBox1.Text);
                            cmd.Parameters.AddWithValue("@pass", textBox6.Text);
                            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || comboBox1.Text == "" || textBox6.Text == "")
                            {
                                MessageBox.Show("Please enter all the necessary details.");

                            }
                            else
                            {
                                cmd.ExecuteNonQuery();

                                string p = "select Consumer_id from Consumer where Password=@pass";
                                cmd.CommandText = p;
                                object r = cmd.ExecuteScalar();

                                if (r != null)
                                {


                                    int generatedConsumerId = Convert.ToInt32(r);
                                    MessageBox.Show("You registered Successfully with username:" + generatedConsumerId);


                                }
                                else
                                {
                                    MessageBox.Show("Registration failed. Please try again.");
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Incorrect captcha. Please enter the correct captcha.");
                        textBox8.Text = GenerateRandomCaptcha();
                        textBox7.Clear();
                    }

                }
                
                    
            }
            catch(Exception ex) 
            { 
                MessageBox.Show(ex.Message);
            }
        }
    }
}
