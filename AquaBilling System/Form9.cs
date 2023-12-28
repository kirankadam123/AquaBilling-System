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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;


namespace AquaBilling_System
{
    public partial class Form9 : Form
    {
        private int cancelTimerSeconds = 15;
        private int elapsedSeconds;
        string username = Login.Username;
        int generatedOrderId;
        public Form9()
        {
            InitializeComponent();
            button1.Visible = false;
            button4.Visible = false;
            radioButton4.Visible = false;
            radioButton5.Visible = false;
            radioButton6.Visible = false;
            textBox2.Click += textBox2_Click;
            button5.Click += button5_Click;
            button1.Click += button1_Click;
            timer1.Interval = 1000; 
            timer1.Tick += Timer1_Tick;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton4.Checked || radioButton5.Checked || radioButton6.Checked)
            {
                label5.Text = "Price Details";
                int t;
                if (int.TryParse(label8.Text, out t))
                {
                    // Add the delivery fee
                    int totalWithDeliveryFee = t + 5;

                    if (radioButton4.Checked)
                    {
                        
                        label6.Text = $"Price: {t}\n Delivery fee: 5\n Total Amount : {totalWithDeliveryFee}";
                    }
                    else
                        label6.Text = $"Price: {t}\n Delivery fee: 0\n Total Amount : {t}";

                }
                button4.Visible = true;
            }
            else
                MessageBox.Show("Please select Payment Mode.");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form7 f = new Form7();
            f.Show();
            this.Close();
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            //string username = UserSession.Username;
            using (SqlConnection con = new SqlConnection(ConnectionStringProvider.ConnectionString))
            {
                con.Open();

                String q = "select Address from Consumer where Consumer_id=@c";
                using (SqlCommand cmd = new SqlCommand(q, con))
                {

                    cmd.Parameters.AddWithValue("@c", username);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            textBox1.Text = dr.GetString(0);
                        }
                    }
                }
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login l = new Login();
            l.Show();
            this.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem != null)
            {
                comboBox2.Text = comboBox2.SelectedItem.ToString();
            }
        }

        private void LoadSuppliers()
        {
            using (SqlConnection con = new SqlConnection(ConnectionStringProvider.ConnectionString))
            {
                con.Open();
                String s = null;
                if (radioButton1.Checked)
                    s = "Drinking Water";
                if (radioButton2.Checked)
                    s = "Regular Water";
                if (radioButton3.Checked)
                    s = "Mineral Water";
                string q = "select Supplier_Name from Supplier where Category=@c";
                using (SqlCommand cmd = new SqlCommand(q, con))
                {
                    cmd.Parameters.AddWithValue("@c", s);
                    SqlDataReader dr = cmd.ExecuteReader();

                    comboBox2.Items.Clear();

                    while (dr.Read())
                    {
                        string supplierName = dr["Supplier_Name"].ToString();
                        comboBox2.Items.Add(supplierName);
                    }


                }
            }
        }


        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                LoadSuppliers();
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                LoadSuppliers();
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                LoadSuppliers();
            }
        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {

            using (SqlConnection con = new SqlConnection(ConnectionStringProvider.ConnectionString))
            {
                con.Open();

                String q = "select Rate_per_ltr from Supplier where Supplier_Name=@c";
                using (SqlCommand cmd = new SqlCommand(q, con))
                {

                    cmd.Parameters.AddWithValue("@c", comboBox2.Text);
                    var result = cmd.ExecuteScalar();
                    int t = 0;
                    if (result != null && result != DBNull.Value)
                    {
                        int r = Convert.ToInt32(result);
                        int i;
                        int.TryParse(textBox2.Text, out i);
                        t = r * i;
                        label8.Text = ""+t;
                    }
                    label4.Text = "Total amount: " + label8.Text;

                }

            }
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text == "Select the Supplier" || comboBox2.Text == "" || textBox2.Text == "Add Quantity(Ltr)" || textBox2.Text == "" || textBox1.Text == "Add Address....." || textBox1.Text == "")
                MessageBox.Show("Please add all the necessary details.");
            else 
            { 
                label7.Text = "Payments";
                button1.Visible = true;
                radioButton4.Visible = true;
                radioButton5.Visible = true;
                radioButton6.Visible = true;
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (radioButton4.Checked || radioButton5.Checked || radioButton6.Checked)
            {
                using (SqlConnection con = new SqlConnection(ConnectionStringProvider.ConnectionString))
                {
                    con.Open();

                    string q = "insert into OrderT(Consumer_id,Supplier_Name,Quantity,Price,Address) values(@ci,@n,@q,@p,@a); select scope_identity();";

                    using (SqlCommand cmd = new SqlCommand(q, con))
                    {
                        cmd.Parameters.AddWithValue("@ci", username);
                        cmd.Parameters.AddWithValue("@n", comboBox2.Text);
                        cmd.Parameters.AddWithValue("@q", textBox2.Text);
                        cmd.Parameters.AddWithValue("@p", label8.Text);
                        cmd.Parameters.AddWithValue("@a", textBox1.Text);

                        //cmd.ExecuteNonQuery();

                        //string p = "select Order_id from OrderT where Consumer_id=@m";
                        //cmd.Parameters.AddWithValue("@m", username);
                        //cmd.CommandText = p;
                        object r = cmd.ExecuteScalar();

                        if (r != null)
                        {
                            generatedOrderId = Convert.ToInt32(r);
                            MessageBox.Show("Your order placed Successfully with Order Id: " + generatedOrderId);

                            StartCancelTimer();
                        }
                        else
                        {
                            MessageBox.Show("Please select payment method");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Payment method select karein.");
            }
        }

        private void StartCancelTimer()
        {
            elapsedSeconds = cancelTimerSeconds;
            timer1.Start();
        }
        private bool isOrderCanceled = false;
        private void Timer1_Tick(object sender, EventArgs e)
        {
            
            if (!isOrderCanceled && elapsedSeconds > 0)
            {
                label2.Text = $"Remaining Time: {elapsedSeconds} seconds";
                elapsedSeconds--;
            }
            else if (!isOrderCanceled)
            {
                // Time has elapsed, perform cancel order logic
                //CancelOrder();
                // Set the flag to indicate that the order is canceled
                isOrderCanceled = true;

                // Stop the timer
                timer1.Stop();

                // Display the message
                label2.Text = "Time's up! Further cancellations are not allowed.";
            }
        }

        
        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        { 
        if (radioButton4.Checked)
            {
                button4.Text = "Place Order";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
          
                if (!isOrderCanceled)
                {
                    // Timer is still running, cancel the order
                    DialogResult r = MessageBox.Show("Are you sure you want to cancel this order?", "Cancel Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (r == DialogResult.Yes)
                    {
                        timer1.Stop();

                        using (SqlConnection con = new SqlConnection(ConnectionStringProvider.ConnectionString))
                        {
                            try
                            {
                                con.Open();
                                String q = "update OrderT set Order_Status=@o where Order_id=@d";
                                String o = "Order Canceled";
                                using (SqlCommand cmd = new SqlCommand(q, con))
                                {
                                    cmd.Parameters.AddWithValue("@d", generatedOrderId);
                                    cmd.Parameters.AddWithValue("@o", o);

                                    int s = cmd.ExecuteNonQuery();

                                    if (s > 0)
                                    {
                                        label2.Text = "Order canceled.";
                                        isOrderCanceled = true;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Order not found or could not be deleted.");
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("An error occurred while canceling the order: " + ex.Message);
                            }
                        }
                    }
                }
                else
                {
                    // Timer has already elapsed
                    MessageBox.Show("Timer has already elapsed. You cannot cancel the order now.");
                }
            
        }
       

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked)
            {
                button4.Text = "Pay";
            }
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton6.Checked)
            {
                button4.Text = "Pay";
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
