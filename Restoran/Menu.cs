using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Restoran
{
    public partial class Menu : Form
    {

        SqlConnection conn = Properti.conn;
        int cell = -1;

        public Menu()
        {
            InitializeComponent();
            tampilData();
        }

        private void tampilData()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from [menu]", conn);
            cmd.CommandType = CommandType.Text;
            DataTable dt = new DataTable();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            conn.Close();
            dataGridView1.DataSource = dt;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog()
            {
                Filter = "Images File(*.*png;*.*jpg;*.*jpeg)|*.*png;*.*jpg;*.*jpeg",
                Title = "Pilih gambar"
            };


            if (od.ShowDialog() == DialogResult.OK)
            {
                pictureBox4.ImageLocation = od.FileName;
                string filename = Path.GetFileName(pictureBox4.ImageLocation);
                textBox4.Text = filename;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(textBox1.Text) || String.IsNullOrWhiteSpace(textBox2.Text) || String.IsNullOrWhiteSpace(textBox3.Text) || String.IsNullOrWhiteSpace(textBox4.Text))
                {
                    MessageBox.Show("Harap isi semua data yang disediakan!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    DialogResult = MessageBox.Show("Apakah data yang ingin ditambahkan sudah benar?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (DialogResult == DialogResult.Yes)
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO menu VALUES(@name,@price,@photo)", conn);
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        cmd.Parameters.AddWithValue("@name", textBox2.Text);
                        cmd.Parameters.AddWithValue("@price", textBox3.Text);
                       

                        if (!string.IsNullOrEmpty(pictureBox4.ImageLocation))
                        {
                            string filename = Path.GetFileName(pictureBox4.ImageLocation);
                            cmd.Parameters.AddWithValue("@photo", filename);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@photo", DBNull.Value);
                        }
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        tampilData();
                        MessageBox.Show("Data berhasil ditambahkan", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clear();

                    }
                }
            } 
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var row = dataGridView1.CurrentRow;
                int menuid = Convert.ToInt32(row.Cells["menuid"].Value.ToString());
                DialogResult = MessageBox.Show("Apakah data ingin diubah sudah benar?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (DialogResult == DialogResult.Yes)
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("update [menu] set name = @name, price = @price, photo = @photo where menuid = @menuid", conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("menuid", menuid);
                    cmd.Parameters.AddWithValue("@name", textBox2.Text);
                    cmd.Parameters.AddWithValue("@price", textBox3.Text);
                    if (!string.IsNullOrEmpty(pictureBox4.ImageLocation))
                    {
                        string filename = Path.GetFileName(pictureBox4.ImageLocation);
                        cmd.Parameters.AddWithValue("@photo", filename);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@photo", DBNull.Value);
                    }
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Data berhasil diubah!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            pictureBox4.Image = null;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                var row = dataGridView1.CurrentRow;
                int menuid = Convert.ToInt32(row.Cells["menuid"].Value.ToString());
                DialogResult = MessageBox.Show("Apakah anda yakin ingin menghapus data ini?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (DialogResult == DialogResult.Yes)
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("delete from [menu] where menuid = @menuid", conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("menuid", menuid);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Data berhasil diubah!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tampilData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            cell = e.RowIndex;
            textBox1.Text = dataGridView1.CurrentRow.Cells["name"].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells["name"].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells["price"].Value.ToString();
            string filename = dataGridView1.CurrentRow.Cells["photo"].Value.ToString();

            textBox4.Text = filename;

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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            clear();
        }
    }
}
