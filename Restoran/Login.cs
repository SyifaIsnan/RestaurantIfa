using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace Restoran
{
    public partial class Login : Form
    {
       

        SqlConnection conn = Properti.conn;
        public Login()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.PasswordChar = '\0';
            }
            else
            {
                textBox2.PasswordChar = '*';
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

            string userRole = string.Empty;

            if (String.IsNullOrWhiteSpace(textBox1.Text) || String.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Username dan password tidak boleh kosong!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    SqlCommand command = new SqlCommand("SELECT * FROM employee WHERE email= @email AND password = @password", conn);
                    command.CommandType = CommandType.Text;
                    conn.Open();
                    command.Parameters.AddWithValue("@email", textBox1.Text);
                    command.Parameters.AddWithValue("@password", textBox2.Text);
                    SqlDataReader dr = command.ExecuteReader();
                    if (dr.Read())
                    {
                        string position = dr["position"].ToString();
                        Properti.employeeid = Convert.ToInt32(dr["employeeid"].ToString());
                        Home mf = new Home(position);
                        mf.Show();
                        this.Hide();
                    }
                    
                    else
                    {
                        MessageBox.Show("Login gagal, periksa kembali email dan password Anda!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    conn.Close();
                }
               
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);

                }
            }
        }
    }
}
