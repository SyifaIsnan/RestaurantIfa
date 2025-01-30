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
    public partial class Tunai : UserControl
    {
        private decimal total;
        private Payment payment;
        public Tunai(decimal total, Payment payment)
        {
            InitializeComponent();
            this.total = total;
            this.payment = payment;
        }

        SqlConnection conn = Properti.conn;

        private void Tunai_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(textBox1.Text, out decimal bayar))
            {

                decimal kembali = total - bayar;
                textBox2.Text = kembali.ToString();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {


            SqlCommand cmd = new SqlCommand("UPDATE Headorder SET payment = @payment, bank = @bank WHERE orderid = @orderid", conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();
            cmd.Parameters.AddWithValue("@payment", "TUNAI");
            cmd.Parameters.AddWithValue("@bank", "-");
            cmd.Parameters.AddWithValue("@orderid", payment.selectorderid);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Kamu sudah melakukan pembayaran", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
