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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Restoran
{
    public partial class Member : Form
    {

        SqlConnection conn = Properti.conn;
        int cell = -1;

        public Member()
        {
            InitializeComponent();
            tampilData();
        }

        private void tampilData()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from [member]", conn);
            cmd.CommandType = CommandType.Text;
            DataTable dt = new DataTable();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            conn.Close();
            dataGridView1.DataSource = dt;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            cell = e.RowIndex;
            textBox1.Text = dataGridView1.CurrentRow.Cells["memberid"].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells["name"].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells["email"].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells["handphone"].Value.ToString();
            dateTimePicker1.Value = DateTime.Parse(dataGridView1.CurrentRow.Cells["joindate"].Value?.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = MessageBox.Show("Apakah anda yakin data yang ingin ditambahkan sudah benar?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (DialogResult == DialogResult.Yes)
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("insert into [member] values (@name, @email, @handphone, @joindate)", conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@name", textBox2.Text);
                    cmd.Parameters.AddWithValue("@email", textBox3.Text);
                    cmd.Parameters.AddWithValue("@handphone", textBox4.Text);
                    cmd.Parameters.AddWithValue("@joindate",dateTimePicker1.Value);
                    cmd.ExecuteNonQuery();
                    conn.Close();                    
                    MessageBox.Show("Data berhasil ditambahkan!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tampilData();
                    clear();
                }
            } 
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void clear()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            dateTimePicker1.Value = DateTime.Now;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                var row = dataGridView1.CurrentRow;
                int memberid = Convert.ToInt32(row.Cells["memberid"].Value.ToString());
                SqlCommand cmd = new SqlCommand("update [member] set name = @name, email = @email, handphone = @handphone, joindate = @joindate where memberid = @memberid", conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("memberid", memberid);
                cmd.Parameters.AddWithValue("@name", textBox2.Text);
                cmd.Parameters.AddWithValue("@email", textBox3.Text);
                cmd.Parameters.AddWithValue("@handphone", textBox4.Text);
                cmd.Parameters.AddWithValue("@joindate", dateTimePicker1.Value);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Data berhasil diubah!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tampilData();
                clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                var row = dataGridView1.CurrentRow;
                int memberid = Convert.ToInt32(row.Cells["memberid"].Value.ToString());
                SqlCommand cmd = new SqlCommand("delete from [member] where memberid = @memberid", conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("memberid", memberid);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Data berhasil dihapus!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tampilData();
                clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            clear();
        }
    }
}
