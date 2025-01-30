using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restoran
{
    public partial class Payment : Form
    {
        public int selectorderid
        {
            get { return Convert.ToInt32(comboBox1.SelectedItem.ToString()); }
        }

        public Payment()
        {
            InitializeComponent();
            comboBox2.Items.Add("TUNAI");
            comboBox2.Items.Add("DEBIT CARD");
        }

        SqlConnection conn = Properti.conn;

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT orderid FROM Headorder", conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            comboBox1.Items.Clear();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["orderid"].ToString());
            }
            conn.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT Menu.name, Detailorder.qty, Detailorder.price  FROM Detailorder  INNER JOIN Menu ON Detailorder.menuid = Menu.menuid  WHERE orderid = @orderid", conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();
            cmd.Parameters.AddWithValue("@orderid", comboBox1.SelectedItem);
            DataTable dt = new DataTable();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            conn.Close();

            dt.Columns.Add("Total", typeof(string));
            foreach (DataRow row in dt.Rows)
            {
                int price = Convert.ToInt32(row["price"].ToString());
                int qty = Convert.ToInt32(row["qty"].ToString());
                int total = price * qty;

                row["Total"] = total;

            }
            dataGridView1.DataSource = dt;
            MunculTotal();
        }

        private decimal MunculTotal()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                total += Convert.ToDecimal(row.Cells["Total"].Value);
            }

            label4.Text = $"Total :  {total.ToString("C", CultureInfo.GetCultureInfo("id-ID"))}";
            return total;
        }

        private void Payment_Load(object sender, EventArgs e)
        {

        }

        

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0)
            {
                flowLayoutPanel1.Controls.Clear();
                flowLayoutPanel1.Controls.Add(new Tunai(MunculTotal(), this));
            }
            else if (comboBox2.SelectedIndex == 1)
            {
                flowLayoutPanel1.Controls.Clear();
                flowLayoutPanel1.Controls.Add(new DebitCard(this));
            }
            else
            {
                flowLayoutPanel1.Controls.Clear();
            }

        }
    }

}
