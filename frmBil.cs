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
    public partial class frmBil : Form
    {
        public frmBil()
        {
            InitializeComponent();
        }

        private void btnCıkıs_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Çıkmak istediğinize eminmisiniz", "uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            { Application.Exit(); }
        }

        private void btnGeriDon_Click(object sender, EventArgs e)
        {    frmMenu frm = new frmMenu();
            this.Close();
            frm.Show();
        }
        cSiparis cs = new cSiparis();
        int odemeTurId = 0;
        private void frmBil_Load(object sender, EventArgs e)
        {
            if(cGenel._ServisTurNo==1)
            {
                lblAdisyonId.Text = cGenel._AdisyonId;
                txtIndirimTutari.TextChanged +=new EventHandler(txtIndirimTutari_TextChanged);

                cs.SiparisleriGetir(lvUrunler, Convert.ToInt32(lblAdisyonId.Text));
                if(lvUrunler.Items.Count>0)
                {
                    decimal toplam = 0;
                    for(int i=0;i<lvUrunler.Items.Count;i++)
                    { 
                        toplam += Convert.ToDecimal(lvUrunler.Items[i].SubItems[3].Text);

                    }
                     lbToplamTutar.Text = string.Format("{0:0.000}",toplam);
                      lbÖdenecek.Text = string.Format("{0:0.000}", toplam);

                }
                gbIndirim.Visible = false; 
                txtIndirimTutari.Clear();
            }
                //masa siparişinde ödeme türünü seçmeye gerek yoktu ama paket serviste ödeme türü seçilmeli
            else if(cGenel._ServisTurNo==2)
            {
                lblAdisyonId.Text = cGenel._AdisyonId;
                cPaketler pc = new cPaketler();
                odemeTurId= pc.OdemeTurIdGetir(Convert.ToInt32(lblAdisyonId.Text));
                txtIndirimTutari.TextChanged += new EventHandler(txtIndirimTutari_TextChanged);
                cs.SiparisleriGetir(lvUrunler, Convert.ToInt32(lblAdisyonId.Text));
                if(odemeTurId==1)
                {
                    rbKrediKarti.Checked = true;
                }
                else if(odemeTurId==2)
                {
                    rbKrediKarti.Checked = true;
                }
                else if(odemeTurId==3)
                { rbTicket.Checked = true; }
                if (lvUrunler.Items.Count > 0)
                {
                    decimal toplam = 0;
                    for (int i = 0; i < lvUrunler.Items.Count; i++)
                    {
                        toplam += Convert.ToDecimal(lvUrunler.Items[i].SubItems[3].Text);

                    }
                    lbToplamTutar.Text = string.Format("{0:0.000}", toplam);
                    lbÖdenecek.Text = string.Format("{0:0.000}", toplam);
                    decimal kdv = Convert.ToDecimal(lbÖdenecek.Text) * 18 / 100;
                    lbKDV.Text = string.Format("{0:0.000}", kdv);

                }
                gbIndirim.Visible = false;
                txtIndirimTutari.Clear();
            }
        }

        private void txtIndirimTutari_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDecimal(txtIndirimTutari.Text) < Convert.ToDecimal(lbToplamTutar.Text))
                {
                    try
                    {
                        lbIndirim.Text = string.Format("{0:0.000}", Convert.ToDecimal(txtIndirimTutari.Text));
                    }
                    catch (Exception)
                    {
                        lbIndirim.Text = string.Format("{0:0.000}",0);
                    }
                }
                else
                {MessageBox.Show("İndirim tutari toplam tutardan büyük olamaz");}
            }
            
            catch (Exception)
            {
                lbIndirim.Text = string.Format("{0:0.000}",0);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void chkindirim_CheckedChanged(object sender, EventArgs e)
        {
            if(chkindirim.Checked)
            {
                gbIndirim.Visible = true;
                txtIndirimTutari.Clear();
            }
            else
            { gbIndirim.Visible = false; txtIndirimTutari.Clear(); }
        }

        private void lbIndirim_TextChanged(object sender, EventArgs e)
        {
            if(Convert.ToDecimal(lbIndirim.Text)>0)
            {
                decimal odenecek = 0;
                lbÖdenecek.Text = lbToplamTutar.Text;
                odenecek = Convert.ToDecimal(lbÖdenecek.Text) - Convert.ToDecimal(lbIndirim.Text);
                 lbÖdenecek.Text = string.Format("{0:0.000}", odenecek);

            }
            decimal kdv = Convert.ToDecimal(lbÖdenecek.Text)*18/100;
            lbKDV.Text = string.Format("{0:0.000}", kdv);
        }
        cMasalar masalar = new cMasalar();
        cRezervasyon rezerve = new cRezervasyon();
        private void button2_Click(object sender, EventArgs e)
        {//rezervasyon
            if(cGenel._ServisTurNo==1)
            {
                int masaId = masalar.MasaNosu(cGenel._ButtonName);
                int musteriId = 0;
                if(masalar.MasaDurumu(masaId,4)==true)
                {
                    musteriId = rezerve.MusteriIdGetirRezervasyondan(masaId);
                }
                else
                { musteriId = 1; }
                int ödemeTurId = 0;
                if(rbKrediKarti.Checked)
                {
                    ödemeTurId = 2;
                }
                if (rbNakit.Checked)
                {
                    ödemeTurId = 1;
                }
                if(rbTicket.Checked)
                { ödemeTurId = 3; }
                cOdeme odeme = new cOdeme();
                //ADISYONID,ODEMETURID,MUSTERID,ARATOPLAM,KDVTUTARI,TOPLAMTUTAR,INDIRIM 
               odeme.AdisyonID= Convert.ToInt32(lblAdisyonId.Text);
               odeme.OdemeTurId=ödemeTurId;
                odeme.MusteriId=musteriId;
                odeme.AraToplam=Convert.ToDecimal(lbÖdenecek.Text);
                odeme.KdvTutari = Convert.ToDecimal(lbKDV.Text);
                odeme.GenelToplam = Convert.ToDecimal(lbToplamTutar.Text);
                odeme.Indirim = Convert.ToDecimal(lbIndirim.Text);
                bool sonuc = odeme.HesapKapat(odeme);
                if(sonuc)
                {
                    MessageBox.Show("Masa kapatılmıştır.");
                    masalar.MasaDurumuDegistir(Convert.ToString(masaId), 1);
                    cRezervasyon c = new cRezervasyon();
                    c.rezervasyonKapat(Convert.ToInt32(lblAdisyonId.Text));
                    cAdisyon a = new cAdisyon();
                    a.AdisyonKapat(Convert.ToInt32(lblAdisyonId.Text), 1);
                    this.Close();
                    frmMasalar frm = new frmMasalar();
                    frm.Show();
                }
                else
                {
                    MessageBox.Show("hesap kapatılırken hata oldu");
                }
            }//paketsipariş
            else if(cGenel._ServisTurNo==2)
            {
                cOdeme odeme = new cOdeme();
                //ADISYONID,ODEMETURID,MUSTERID,ARATOPLAM,KDVTUTARI,TOPLAMTUTAR,INDIRIM 
                odeme.AdisyonID = Convert.ToInt32(lblAdisyonId.Text);
                odeme.OdemeTurId = odemeTurId;
                odeme.MusteriId = 1;//paket sipariş ıd
                odeme.AraToplam = Convert.ToDecimal(lbÖdenecek.Text);
                odeme.KdvTutari = Convert.ToDecimal(lbKDV.Text);
                odeme.GenelToplam = Convert.ToDecimal(lbToplamTutar.Text);
                odeme.Indirim = Convert.ToDecimal(lbIndirim.Text);
                bool sonuc = odeme.HesapKapat(odeme);
                if (sonuc)
                {
                    cAdisyon a = new cAdisyon();
                    a.AdisyonKapat(Convert.ToInt32(lblAdisyonId.Text), 1);
                    cPaketler paket = new cPaketler();
                    paket.PaketServisiKapat(Convert.ToInt32(lblAdisyonId.Text));
                    MessageBox.Show("HesapKapatılmıştır.");
                    this.Close();
                    frmPaketSiparis frm = new frmPaketSiparis();
                    frm.Show();
                }
                else
                {
                    MessageBox.Show("hesap kapatılırken hata oldu");
                }
            }

        }

    }
}
