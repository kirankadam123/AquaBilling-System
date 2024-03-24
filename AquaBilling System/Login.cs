using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Diagnostics.Eventing.Reader;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AquaBilling_System
{
   

    public partial class Login : Form
    {
        public static string Username;
        //public static string Role { get; set; }

        private string connectionString = ConfigurationManager.ConnectionStrings["AquaConnection"].ConnectionString;
         
        public Login()
        {
            InitializeComponent();
            

        }

    private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f=new Form2();
            f.ShowDialog();
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
       
        {
            try
            {

                if (comboBox1.SelectedItem != null)
                {
                    string selectedRole = comboBox1.SelectedItem.ToString();

                    if (selectedRole == "Select your role")
                    {
                        if (textBox1.Text != "" || textBox2.Text != "")
                        {
                            MessageBox.Show("Please select Your Appropriate Role");
                        }
                        else
                        {
                            MessageBox.Show("Please Enter valid username & password");
                        }
                    }
                    else
                    {
                        if (textBox1.Text != "" || textBox2.Text != "")
                        {
                            if (textBox4.Text.Equals(textBox3.Text, StringComparison.Ordinal))
                            {

                                if (selectedRole == "Supplier" || selectedRole == "Consumer")
                                {

                                    string table = (selectedRole == "Supplier") ? "Supplier" : "Consumer";

                                    using (SqlConnection con = new SqlConnection(ConnectionStringProvider.ConnectionString))
                                    {
                                        con.Open();

                                        string q = $"select * from {table} where {table}_id=@u and Password=@p";

                                        using (SqlCommand cmd = new SqlCommand(q, con))
                                        {
                                            cmd.Parameters.AddWithValue("@u", textBox1.Text);
                                            cmd.Parameters.AddWithValue("@p", textBox2.Text);

                                            using (SqlDataReader dr = cmd.ExecuteReader())
                                            {
                                                if (dr.Read())
                                                {
                                                    Username = textBox1.Text;
                                                    MessageBox.Show($"Login successful. Welcome {selectedRole}!");

                                                    if (selectedRole == "Supplier")
                                                    {
                                                        this.Hide();
                                                        Form8 f = new Form8();
                                                        f.ShowDialog();
                                                        this.Close();
                                                    }
                                                    else if (selectedRole == "Consumer")
                                                    {
                                                        this.Hide();
                                                        Form7 f = new Form7();
                                                        f.ShowDialog();
                                                        this.Close();
                                                    }
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Invalid username or password");
                                                    textBox3.Text = GenerateRandomCaptcha();
                                                    textBox1.Clear();
                                                    textBox2.Clear();
                                                    textBox4.Clear();
                                                }
                                            }
                                        }
                                    }


                                }
                                else
                                {
                                    if (selectedRole == "Admin" && textBox1.Text == "Admin" && textBox2.Text == "1234")
                                    {
                                        MessageBox.Show($"Login successful. Welcome {selectedRole}!");
                                        this.Hide();
                                        Form6 f = new Form6();
                                        f.ShowDialog();
                                        this.Close();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Invalid username or password");
                                        textBox3.Text = GenerateRandomCaptcha();
                                        textBox1.Clear();
                                        textBox2.Clear();
                                        textBox4.Clear();
                                    }

                                }
                            }
                            else
                            {
                                MessageBox.Show("Incorrect captcha. Please enter the correct captcha.");
                                textBox3.Text = GenerateRandomCaptcha();
                                textBox4.Clear(); textBox2.Clear();
                            }

                        }
                        else
                        {
                            MessageBox.Show("Please Enter username & password");
                            textBox3.Text = GenerateRandomCaptcha();
                            textBox4.Clear();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select Your Appropriate Role");
                    textBox3.Text = GenerateRandomCaptcha();
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox4.Clear();
                }
            }
            catch(Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (comboBox1.Text == "Supplier" || comboBox1.Text=="Consumer")
            {
                this.Hide();
                Form3 f = new Form3(comboBox1.Text);
                f.ShowDialog();
                this.Close();
            }
            else if (comboBox1.Text == "Admin")
            {
                MessageBox.Show("For security reasons, admin password changes are restricted.");
            }
            else
            {
                MessageBox.Show("Please select Your Appropriate Role");
            }
            
        }

        private void linkLabel2_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (comboBox1.Text == "Consumer")
            {
                this.Hide();
                Form4 f = new Form4();
                f.ShowDialog();
                this.Close();
            }
            else if (comboBox1.Text == "Supplier")
            {
                this.Hide();
                Form5 f = new Form5();
                f.ShowDialog();
                this.Close();
            }
            else if (comboBox1.Text == "Admin")
            {
                MessageBox.Show("You can't Sign Up as a Admin, Please select Supplier or Consumer");
            }
            else
            {
                MessageBox.Show("Please select Your Appropriate Role");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {
            textBox3.Text = GenerateRandomCaptcha();
        }

        string GenerateRandomCaptcha()
        {
            string s = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%&";
            Random r = new Random();
            String c = "";

            for (int i = 0; i < 6; i++)
            {
                int n = r.Next(s.Length);
                c += s[n];
            }
            return c;
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
