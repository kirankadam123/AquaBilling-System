using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AquaBilling_System
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
            
        }

        private void label1_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form11 f = new Form11();
            f.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form10 f = new Form10();
            f.Show();
            this.Close();
        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login l=new Login();
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

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form13 f = new Form13();
            f.Show();
            this.Close();
        }
    }
}
