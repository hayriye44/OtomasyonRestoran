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
    public partial class frmSiparisKontrol : Form
    {
        public frmSiparisKontrol()
        {
            InitializeComponent();
        }

        private void frmSiparisKontrol_Load(object sender, EventArgs e)
        {
            cAdisyon c = new cAdisyon();
            int butonsayisi = c.paketAdisyonIdAdediBul();
            c.AcikPaketAdisyonlari(lvMüsteriler);
            int alt = 50;
            int sol = 7;
            int bol = Convert.ToInt32(Math.Ceiling(Math.Sqrt(butonsayisi)));
            for(int i=1;i<=butonsayisi;i++)
            {
                Button btn = new Button();
                btn.AutoSize = false;
                btn.Size = new Size(100, 55);
                btn.FlatStyle = FlatStyle.Flat;
                btn.Name = lvMüsteriler.Items[i - 1].SubItems[0].Text;
                btn.Text = lvMüsteriler.Items[i - 1].SubItems[1].Text;
                btn.Font=new Font(btn.Font.FontFamily.Name,18);
                btn.Location = new Point(sol, alt);
                this.Controls.Add(btn);

                sol += btn.Width + 7;
                if(i==2)
                {
                    sol = 7;
                    alt += 50;
                }
                btn.Click += new EventHandler(dinamikMetot);
                btn.MouseEnter += new EventHandler(dinamikMetot2);
            }
        }
        
        protected void dinamikMetot(object sender, EventArgs e)
        {cAdisyon a = new cAdisyon();
            Button dinamikButon = (sender as Button);
            frmBil frm = new frmBil();
            cGenel._ServisTurNo = 2;
            cGenel._AdisyonId = Convert.ToString(a.MüsterininSonAdisyonIDsi(Convert.ToInt32(dinamikButon.Name)));
            frm.Show();
        }
        protected void dinamikMetot2(object sender, EventArgs e)
        {
            Button dinamikButon = (sender as Button);
            cAdisyon a = new cAdisyon();
            a.MusteriDetay(lvMusteriDetay,Convert.ToInt32(dinamikButon.Name));
            sonSiparisTarihi();
            lvSatisDetay.Items.Clear();
            cSiparis s=new cSiparis();
            cGenel._ServisTurNo = 2;
            cGenel._AdisyonId = Convert.ToString(a.MüsterininSonAdisyonIDsi(Convert.ToInt32(dinamikButon.Name)));
            lblGenelToplam.Text = s.GenelToplamBul(Convert.ToInt32(dinamikButon.Name)).ToString()+"TL";
        }
        void sonSiparisTarihi()
        {
            if(lvMusteriDetay.Items.Count>0)
            {
                int s=lvMusteriDetay.Items.Count;
                lblSonSiparis.Text = lvMusteriDetay.Items[s - 1].SubItems[3].Text;
                txtToplamTutar.Text =( s + "kez geldi").ToString();
            }
        }
        void toplam()
        {
            int kayitSayisi=lvSatisDetay.Items.Count;
            decimal toplam = 0;
            for(int i=0;i<kayitSayisi;i++)
            {
                toplam += Convert.ToDecimal(lvSatisDetay.Items[i].SubItems[2].Text)*Convert.ToDecimal(lvSatisDetay.Items[i].SubItems[3].Text);
            }
            lblToplam.Text = toplam.ToString() + "TL";
        }

        private void lvMusteriDetay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lvMusteriDetay.SelectedItems.Count>0)
            {
                cSiparis c = new cSiparis();
                c.AdisyonPaketSiparisDetay(lvSatisDetay,Convert.ToInt32( lvMusteriDetay.SelectedItems[0].SubItems[4].Text));
                toplam();
            }
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

    }
}
