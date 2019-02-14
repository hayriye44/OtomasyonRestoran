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
    public partial class frmMusteriAra : Form
    {
        public frmMusteriAra()
        {
            InitializeComponent();
        }

        private void btnGeriDon_Click(object sender, EventArgs e)
        {
            frmMenu frm = new frmMenu();
            this.Close();
            frm.Show();
        }

        private void btnCıkıs_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Çıkmak istediğinize eminmisiniz", "uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            { Application.Exit(); }
        }

        private void btnYeniMüsteri_Click(object sender, EventArgs e)
        {
            frmMusteriEkle m = new frmMusteriEkle();
            cGenel._MusteriEkleme = 1;
            m.Show();
        }

        private void frmMusteriAra_Load(object sender, EventArgs e)
        {
            cMusteriler m = new cMusteriler();
            m.MüsterileriGetir(lvMusteriler);
        }

        private void btnMüsteriSec_Click(object sender, EventArgs e)
        {
        }

        private void btnMüsteriGüncelle_Click(object sender, EventArgs e)
        {
            if (lvMusteriler.SelectedItems.Count > 0)
            { frmMusteriEkle frm = new frmMusteriEkle();
            cGenel._MusteriEkleme = 1;
            cGenel._MusteriID = Convert.ToInt32(lvMusteriler.SelectedItems[0].SubItems[0].Text);
            this.Close();
            frm.Show(); }
            else
            { MessageBox.Show("Güncellenecek müşteriyi seç"); }
        }

        private void txtAd_TextChanged(object sender, EventArgs e)
        {
            cMusteriler c = new cMusteriler();
            c.MüsteriGetirAdaGöre(lvMusteriler, txtAd.Text);
        }

        private void lvMusteriler_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtSoyad_TextChanged(object sender, EventArgs e)
        {
            cMusteriler c = new cMusteriler();
            c.MüsteriGetirSoyAdaGöre(lvMusteriler, txtSoyad.Text);
        }

        private void txtTelefon_TextChanged(object sender, EventArgs e)
        {
            cMusteriler c = new cMusteriler();
            c.MüsteriGetirTelefonaGöre(lvMusteriler, txtTelefon.Text);
        }

        private void btnAdisyonBul_Click(object sender, EventArgs e)
        {
            if(txtAdisyon.Text!="")
            {
                cGenel._AdisyonId = txtAdisyon.Text;
                cPaketler p = new cPaketler();
                bool sonuc = p.AcıkAdisyonVarmi(Convert.ToInt32(txtAdisyon.Text));
                if(sonuc)
                {
                    frmBil hesap = new frmBil();
                    cGenel._ServisTurNo = 2;
                    hesap.Show();
                }
                else
                {
                    MessageBox.Show(txtAdisyon.Text + "nolu adisyon yok");
                }
            }
            else
            {
                MessageBox.Show("aramak istediğiniz adisyonu girin");
            }
        }

        private void btnSiparisler_Click(object sender, EventArgs e)
        {
            frmSiparisKontrol frm = new frmSiparisKontrol();
            this.Close();
            frm.Show();
        }
    }
}
