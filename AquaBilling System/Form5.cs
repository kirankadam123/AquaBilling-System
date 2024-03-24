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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form5_Load(object sender, EventArgs e)
        {
            textBox8.Text = GenerateRandomCaptcha();
        }

        string GenerateRandomCaptcha()
        {
            string[] s = { "AB4cDy", "StOP92", "WQ86FD", "E8VGrI", "PRfDs5", "DMB4cD", "QtO4P9", "GF86FD", "UYTnrI", "P58Ds5" };
            Random r=new Random();

            int n = r.Next(s.Length);
            string selString;
            switch (n)
            {
                case 0:
                    selString = s[0];
                    break;
                case 1:
                    selString = s[1];
                    break;
                case 2:
                    selString = s[2];
                    break;
                case 3:
                    selString = s[3];
                    break;
                case 4:
                    selString = s[4];
                    break;
                case 5:
                    selString = s[5];
                    break;
                case 6:
                    selString = s[6];
                    break;
                case 7:
                    selString = s[7];
                    break;
                case 8:
                    selString = s[8];
                    break;
                case 9:
                    selString = s[9];
                    break;
                default:
                    selString = "Invalid Index";
                    break;
            }
            return selString;
        }

        private void label4_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
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

            if (password.Length >= 6)
            {
                MessageBox.Show("Password must be at least 6 characters long.");
                return; 
            }

            try
            {
               
                using (SqlConnection con = new SqlConnection(ConnectionStringProvider.ConnectionString))
                {
                    con.Open();
                    string q = "insert into Supplier(Supplier_Name,Category,Address,[Available_Quantity(in ltr)],Rate_per_ltr,Contact_No,Password) values(@name,@cat,@address,@qua,@rate,@cn,@pass)";

                    if (textBox7.Text.Equals(textBox8.Text, StringComparison.Ordinal))
                    {
                        

                        using (SqlCommand cmd = new SqlCommand(q, con))
                        {
                            cmd.Parameters.AddWithValue("@name", textBox1.Text);
                            cmd.Parameters.AddWithValue("@cat", comboBox1.Text);
                            cmd.Parameters.AddWithValue("@address", textBox2.Text);
                            cmd.Parameters.AddWithValue("@cn", textBox3.Text);
                            cmd.Parameters.AddWithValue("@qua", textBox4.Text);
                            cmd.Parameters.AddWithValue("@rate", textBox9.Text);
                            cmd.Parameters.AddWithValue("@pass", textBox6.Text);
                            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || comboBox1.Text == "" || textBox6.Text == "" || textBox4.Text == "" || textBox9.Text == "")
                            {
                                MessageBox.Show("Please enter all the necessary details.");

                            }
                            else
                            {
                                cmd.ExecuteNonQuery();

                                string p = "select Supplier_id from Supplier where Password=@pass";
                                cmd.CommandText = p;
                                object r = cmd.ExecuteScalar();

                                if (r != null)
                                {


                                    int generatedOwnerId = Convert.ToInt32(r);
                                    MessageBox.Show("You registered Successfully with username:" + generatedOwnerId);


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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
