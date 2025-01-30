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
    public partial class Home : Form
    {
        //public string position;
        
        SqlConnection conn = Properti.conn;
        public string UserRole { get; set; }


        private void MainForm_Load(object sender, EventArgs e)
        {

        }
        
        public Home(string position)
        {
            InitializeComponent();
            
            UserRole = position;
            if (position == null)
                {
                    lOGINToolStripMenuItem.Enabled = true;
                    lOGOUTToolStripMenuItem.Enabled = false;
                    eXITToolStripMenuItem.Enabled = false;
                    mENURESTOToolStripMenuItem.Enabled = false;
                    mEMBERToolStripMenuItem.Enabled = false;
                    kARYAWANToolStripMenuItem.Enabled = false;
                    vIEWORDERToolStripMenuItem.Enabled = false;
                    pAYMENTToolStripMenuItem.Enabled = false;
                    oRDERToolStripMenuItem.Enabled = false;
                    rEPORTINCOMEToolStripMenuItem.Enabled = false;
                }
                else if (position == "Admin")
                {
                    lOGINToolStripMenuItem.Enabled = false;
                    lOGOUTToolStripMenuItem.Enabled = true;
                    eXITToolStripMenuItem.Enabled = true;
                    mENURESTOToolStripMenuItem.Enabled = true;
                    mEMBERToolStripMenuItem.Enabled = true;
                    kARYAWANToolStripMenuItem.Enabled = true;
                    vIEWORDERToolStripMenuItem.Enabled = true;
                    pAYMENTToolStripMenuItem.Enabled = true;
                    oRDERToolStripMenuItem.Enabled = true;
                    rEPORTINCOMEToolStripMenuItem.Enabled = true;
                }
                else if (position == "Cashier")
                {
                    lOGINToolStripMenuItem.Enabled = false;
                    lOGOUTToolStripMenuItem.Enabled = true;
                    eXITToolStripMenuItem.Enabled = true;
                    mENURESTOToolStripMenuItem.Enabled = false;
                    mEMBERToolStripMenuItem.Enabled = false;
                    kARYAWANToolStripMenuItem.Enabled = false;
                    vIEWORDERToolStripMenuItem.Enabled = false;
                    pAYMENTToolStripMenuItem.Enabled = true;
                    oRDERToolStripMenuItem.Enabled = true;
                    rEPORTINCOMEToolStripMenuItem.Enabled = false;
                }
                else if (position == "Chef")
                {
                    lOGINToolStripMenuItem.Enabled = false;
                    lOGOUTToolStripMenuItem.Enabled = true;
                    eXITToolStripMenuItem.Enabled = true;
                    mENURESTOToolStripMenuItem.Enabled = false;
                    mEMBERToolStripMenuItem.Enabled = false;
                    kARYAWANToolStripMenuItem.Enabled = false;
                    vIEWORDERToolStripMenuItem.Enabled = true;
                    pAYMENTToolStripMenuItem.Enabled = false;
                    oRDERToolStripMenuItem.Enabled = false;
                    rEPORTINCOMEToolStripMenuItem.Enabled = false;
                }
            
        }

        

        private void hOMEToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void lOGINToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.ShowDialog();
            
        }

        private void eXITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult = MessageBox.Show("Apakah anda yakin ingin keluar dari aplikasi?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DialogResult == DialogResult.Yes)
            {
                Application.Exit();
            } else {
                MessageBox.Show("Dibatalkan", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void lOGOUTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult = MessageBox.Show("Apakah anda yakin ingin logout dari akun?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DialogResult == DialogResult.Yes)
            {
                Login login = new Login();
                login.ShowDialog(); 
                this.Hide();
            }
            else
            {
                MessageBox.Show("Dibatalkan", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        

        private void kARYAWANToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Karyawan karyawan = new Karyawan();
            karyawan.Show();
        }

        private void mEMBERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Member member = new Member();
            member.Show();
        }

        private void mENURESTOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menu menu = new Menu();
            menu.Show();
        }

        private void vIEWORDERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            ViewOrder order = new ViewOrder();  
            order.Show();
        }

        private void oRDERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            MenuOrder order = new MenuOrder();
            order.Show();
        }

        private void pAYMENTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Payment payment = new Payment();
            payment.Show();
        }

        private void rEPORTINCOMEToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
