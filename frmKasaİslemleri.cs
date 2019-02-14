using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _14542513HayriyeCingöz_restoran_
{
    public partial class frmKasaİslemleri : Form
    {
        public frmKasaİslemleri()
        {
            InitializeComponent();
        }

        private void frmKasaİslemleri_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'DataSet1.DataTable1' table. You can move, or remove it, as needed.
            this.DataTable1TableAdapter.Fill(this.DataSet1.DataTable1);

            this.rpaylik.RefreshReport();
            this.rpGünlük.RefreshReport();
            rpGünlük.Visible = false;
            label1.Text="AYLIK RAPOR";
        }

        private void btnAylik_Click(object sender, EventArgs e)
        {
            label1.Text="AYLIK RAPOR";
            rpaylik.Visible = true;
            rpGünlük.Visible = false;
        }

        private void btnzrapor_Click(object sender, EventArgs e)
        {
            label1.Text = "GÜNLÜK RAPOR";
            rpaylik.Visible = false;
            rpGünlük.Visible = true;
        }

        private void btnGeriDon_Click(object sender, EventArgs e)
        {

            frmMenu frm = new frmMenu();
            this.Close();
            frm.Show();
        }

        private void btnCıkıs_Click(object sender, EventArgs e)
        {
            frmMenu frm = new frmMenu();
            this.Close();
            frm.Show();
        }
    }
}
