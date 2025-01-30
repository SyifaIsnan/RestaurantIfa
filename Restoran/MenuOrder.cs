using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restoran
{
    public partial class MenuOrder : Form
    {
        int cell = -1;
        SqlConnection conn = Properti.conn;
        public MenuOrder()
        {
            InitializeComponent();
            tampilData1();
            
            dataGridView2.Columns.Add("Menu", "Menu");
            dataGridView2.Columns.Add("Price", "Price");
            dataGridView2.Columns.Add("Qty", "Qty");
            dataGridView2.Columns.Add("Total", "Total");
        }

        private void tampilData2()
        {
            
        }

        private void tampilData1()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from [menu]", conn);
            cmd.CommandType = CommandType.Text;
            DataTable dt = new DataTable();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            conn.Close();
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["photo"].Visible = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            cell = e.RowIndex;
            textBox1.Text = dataGridView1.CurrentRow.Cells["name"].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells["price"].Value.ToString();
            string filename = dataGridView1.CurrentRow.Cells["photo"].Value.ToString();


            if (!string.IsNullOrEmpty(filename))
            {
                string imagepath = Path.Combine(@"C:\Users\Saya\Pictures\Saved Pictures", filename);
                pictureBox4.ImageLocation = imagepath;
            }
            else
            {
                pictureBox4.Image = null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            int price = Convert.ToInt32(textBox3.Text);
            int qty = Convert.ToInt32(textBox2.Text);
            int total = price * qty;


            dataGridView2.Rows.Add(name, price, qty, total);
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            totalData2();
        }

        private decimal totalData2()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                total += Convert.ToDecimal(row.Cells["Total"].Value);
            }

            label5.Text = $"Total :  {total.ToString("C", CultureInfo.GetCultureInfo("id-ID"))}";
            return total;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            try
            {
                SqlCommand order = new SqlCommand("INSERT INTO headOrder VALUES(@employeeid,@memberid,@date,@payment,@bank)", conn);
                order.CommandType = CommandType.Text;
                conn.Open();
                order.Parameters.AddWithValue("@employeeid", Properti.employeeid);
                //order.Parameters.AddWithValue("@employeeid", validEmployeeId);
                order.Parameters.AddWithValue("@memberid", "1");
                order.Parameters.AddWithValue("@date", DateTime.Now);
                order.Parameters.AddWithValue("@payment", "-");
                order.Parameters.AddWithValue("@bank", "-");
                order.ExecuteNonQuery();


                SqlCommand detailorder = new SqlCommand("SELECT MAX(orderid) FROM headOrder", conn);
                detailorder.CommandType = CommandType.Text;
                int orderid = (int)detailorder.ExecuteScalar();
                conn.Close();

                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        string name = row.Cells["Menu"].Value.ToString();
                        int qty = Convert.ToInt32(row.Cells["Qty"].Value);
                        int price = Convert.ToInt32(row.Cells["Price"].Value);

                        SqlCommand menu = new SqlCommand("SELECT menuid FROM Menu WHERE name = @name", conn);
                        menu.CommandType = CommandType.Text;
                        menu.Parameters.AddWithValue("@name", name);
                        conn.Open();
                        int menuid = (int)menu.ExecuteScalar();
                        conn.Close();

                        SqlCommand cmd = new SqlCommand("INSERT INTO detailorder (orderid, menuid, qty, price, status) VALUES (@orderid, @menuid, @qty, @price, @status)", conn);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@orderid", orderid);
                        cmd.Parameters.AddWithValue("@menuid", menuid);
                        cmd.Parameters.AddWithValue("@qty", qty);
                        cmd.Parameters.AddWithValue("@price", price);
                        cmd.Parameters.AddWithValue("@status", "PENDING");

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();

                    }
                }
                MessageBox.Show("Data berhasil ditambahkan", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clear();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menambahkan data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void clear()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            label5.Text = "Total : ";
            pictureBox4.Image = null;
            dataGridView2.Rows.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var konfirmasi = MessageBox.Show("Apakah anda yakin ingin membatalkan pesanan", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (konfirmasi == DialogResult.Yes)
            {
                clear();
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }
    }
}
