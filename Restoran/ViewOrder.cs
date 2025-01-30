using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace Restoran
{
    public partial class ViewOrder : Form
    {

        SqlConnection conn = Properti.conn;

        public ViewOrder()
        {
            InitializeComponent();
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT orderid FROM Headorder", conn);
                SqlDataReader dr = cmd.ExecuteReader();

                comboBox1.Items.Clear();
                if (!dr.HasRows)
                {
                    MessageBox.Show("Tidak ada data yang ditemukan.");
                }

                while (dr.Read())
                {
                    comboBox1.Items.Add(dr["orderid"].ToString());
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT  Detailorder.detailid , Menu.name, Detailorder.qty, Detailorder.status FROM Detailorder INNER JOIN Menu ON Detailorder.menuid = Menu.menuid WHERE orderid = @orderid", conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();
            cmd.Parameters.AddWithValue("@orderid", comboBox1.SelectedItem);
            DataTable dt = new DataTable();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            conn.Close();

            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            if (dt.Rows.Count > 0)
            {
                DataGridViewComboBoxColumn combo = new DataGridViewComboBoxColumn();
                combo.Name = "Status";
                combo.HeaderText = "Status";
                combo.DataPropertyName = "Status";
                combo.DataSource = new string[] { "PENDING", "COOKING", "DELIVER" };


                dataGridView1.Columns.Add("menu", "menu");
                dataGridView1.Columns.Add("detailid", "detailid");
                dataGridView1.Columns.Add("qty", "qty");
                dataGridView1.Columns["detailid"].Visible = false;
                dataGridView1.Columns.Add(combo);

                foreach (DataRow row in dt.Rows)
                {
                    int rowIndex = dataGridView1.Rows.Add();
                    dataGridView1.Rows[rowIndex].Cells["menu"].Value = row["name"].ToString();
                    dataGridView1.Rows[rowIndex].Cells["detailid"].Value = row["detailid"].ToString();
                    dataGridView1.Rows[rowIndex].Cells["qty"].Value = row["qty"].ToString();
                    dataGridView1.Rows[rowIndex].Cells["Status"].Value = row["status"].ToString();

                    dataGridView1.EditingControlShowing += DataGridView1_EditingControlShowing;
                }
            }
        }

        private void DataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is ComboBox comboBox)
            {
                comboBox.SelectedIndexChanged -= ComboBox_SelectedIndexChanged;
                comboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
            }
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox comboBox = (ComboBox)sender;
                var row = dataGridView1.CurrentRow;
                int detailid = Convert.ToInt32(row.Cells["detailid"].Value.ToString());
                string status = comboBox.SelectedItem.ToString();


                SqlCommand cmd = new SqlCommand("UPDATE Detailorder SET status = @status WHERE detailid = @detailid", conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@detailid", detailid);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Data berhasil diubah", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void ViewOrder_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}

