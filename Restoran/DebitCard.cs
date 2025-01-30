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

namespace Restoran
{
    public partial class DebitCard : UserControl
    {
        private Payment payment;

        public DebitCard(Payment payment)
        {
            InitializeComponent();
            this.payment = payment;
            comboBox1.Items.Add("BCA");
            comboBox1.Items.Add("BRI");
            comboBox1.Items.Add("BNI");

        }

        SqlConnection conn = Properti.conn;

        private void DebitCard_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!Properti.number(textBox2.Text))
            {
                MessageBox.Show("Card number harus angka", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                SqlCommand cmd = new SqlCommand("UPDATE haedOrder SET payment = @payment, bank = @bank WHERE orderid = @orderid", conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                cmd.Parameters.AddWithValue("@payment", "DEBIT CARD");
                cmd.Parameters.AddWithValue("@bank", comboBox1.SelectedItem);
                cmd.Parameters.AddWithValue("@orderid", payment.selectorderid);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Kamu sudah melakukan pembayaran", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comboBox1.SelectedIndex = -1;
                textBox2.Text = "";
            }
        }
    }
}
