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
    public partial class Karyawan : Form
    {
      

        public static class UserRole
        {
            public static string Email { get; set; }
            public static string Password { get; set; }
            public static string Position { get; set; }

        }

        SqlConnection conn = Properti.conn;
        int cell = -1;
        public Karyawan()
        {
            InitializeComponent();
            tampilData();
            
        }



        private void tampilData()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from [employee]", conn);
            cmd.CommandType = CommandType.Text;
            DataTable dt = new DataTable();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            conn.Close();
            dataGridView1.DataSource = dt;
           
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            { 
                DialogResult = MessageBox.Show("Apakah data yang ingin anda tambahkan sudah benar?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(DialogResult == DialogResult.Yes)
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("insert into [employee] (name, email, password, handphone, position) values (@name, @email, @password, @handphone, @position)", conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@name", textBox2.Text);
                    cmd.Parameters.AddWithValue("@email", textBox3.Text);
                    cmd.Parameters.AddWithValue("@password", textBox4.Text);
                    cmd.Parameters.AddWithValue("@handphone", textBox5.Text);
                    cmd.Parameters.AddWithValue("@position", comboBox1.SelectedItem);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    tampilData();
                    MessageBox.Show("Data berhasil ditambahkan", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear();
                }
            } catch (Exception ex)  
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
            textBox5.Text = "";
            comboBox1.SelectedItem = string.Empty;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            cell = e.RowIndex;
                textBox1.Text = dataGridView1.CurrentRow.Cells["employeeid"].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells["name"].Value.ToString();
                textBox3.Text = dataGridView1.CurrentRow.Cells["email"].Value.ToString();
                textBox4.Text = dataGridView1.CurrentRow.Cells["password"].Value.ToString();
                textBox5.Text = dataGridView1.CurrentRow.Cells["handphone"].Value.ToString();
                comboBox1.Text = dataGridView1.CurrentRow.Cells["position"].Value.ToString();

            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DialogResult = MessageBox.Show("Apakah anda ingin kembali ke halaman utama?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DialogResult == DialogResult.Yes)
            {
                this.Hide();
                Home utama = new Home(UserRole.Position);
                utama.Show();

            }
            else
            {
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = MessageBox.Show("Apakah anda yakin data yang ingin diubah sudah benar?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (DialogResult == DialogResult.Yes)
                {
                    var row = dataGridView1.CurrentRow;
                    int employeeid = Convert.ToInt32(row.Cells["employeeid"].Value.ToString());
                    SqlCommand cmd = new SqlCommand("UPDATE [employee] SET name = @name , email = @email, password = @password , handphone = @handphone, position= @position WHERE employeeid = @employeeid", conn);
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    cmd.Parameters.AddWithValue("@employeeid", employeeid);
                    cmd.Parameters.AddWithValue("@name", textBox2.Text);
                    cmd.Parameters.AddWithValue("@email", textBox3.Text);
                    cmd.Parameters.AddWithValue("@password", textBox4.Text);
                    cmd.Parameters.AddWithValue("@handphone", textBox5.Text);
                    cmd.Parameters.AddWithValue("@position", comboBox1.SelectedItem);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    tampilData();
                    MessageBox.Show("Data berhasil diubah", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear();

                }
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
                DialogResult = MessageBox.Show("Apakah anda yakin data ini ingin dihapus?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (DialogResult == DialogResult.Yes)
                {
                    var row = dataGridView1.CurrentRow;
                    int employeeid = Convert.ToInt32(row.Cells["employeeid"].Value.ToString());
                    SqlCommand cmd = new SqlCommand("DELETE FROM [employee] WHERE  employeeid = @employeeid", conn);
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    cmd.Parameters.AddWithValue("@employeeid", employeeid);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    tampilData();
                    MessageBox.Show("Data berhasil dihapus!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear();
                }
            } 
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            DialogResult = MessageBox.Show("Apakah anda yakin ingin keluar?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DialogResult == DialogResult.Yes)
            {
                Application.Exit();

            }
        }
    }
}
