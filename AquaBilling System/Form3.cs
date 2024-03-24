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
    public partial class Form3 : Form
    {
        private string selectedRole;
        public Form3(String role)
        {
            InitializeComponent();
            selectedRole = role;
        }

       

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try{
            using (SqlConnection con= new SqlConnection(ConnectionStringProvider.ConnectionString))
            {
                    con.Open();
                    String q=$"update {selectedRole} set Password=@p where {selectedRole}_id=@i";

               using (SqlCommand cmd=new SqlCommand(q,con))
                {
                        cmd.Parameters.AddWithValue("@p", textBox2.Text);
                        cmd.Parameters.AddWithValue("@i", textBox1.Text);

                        int c = cmd.ExecuteNonQuery();
                        if (c == 0)
                        {
                            MessageBox.Show($"Password update failed for {selectedRole}.");
                            textBox1.Text="";
                            textBox2.Text="";
                        }

                        else
                        {
                            MessageBox.Show($"Password updated Successfully for {selectedRole}.");
                            this.Hide();
                            Login l = new Login();

                            l.Show();
                            this.Close();
                        }
               }
               
            }
        }
       catch (Exception ex)
{
    MessageBox.Show(ex.Message);
}
}

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
