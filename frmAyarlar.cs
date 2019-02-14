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
    public partial class frmAyarlar : Form
    {
        public frmAyarlar()
        {
            InitializeComponent();
        }

        private void btnGeriDon_Click(object sender, EventArgs e)
        {
            
            frmMenu frm = new frmMenu();
            this.Close();
            frm.Show();
        }
        private void btnCıkıs_Click_1(object sender, EventArgs e)
        {
        if (MessageBox.Show("Çıkmak istediğinize eminmisiniz", "uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            { Application.Exit(); }
        }

        private void frmAyarlar_Load(object sender, EventArgs e)
        {
            cPersoneller p = new cPersoneller();
            cPersonelGorevler g = new cPersonelGorevler();
            string gorev = g.PersonelGorevTanim(cGenel.personelId);
            if(gorev=="Müdür")
            {
                p.PersonelleriGetir(cmbPersonelAd);
                g.PersonelGorevGetir(cmbGorev);
                p.PersonelBilgileriGetirFormIdLv(lvPersoneller);
                btnYeni.Enabled = true;
                btnSil.Enabled = false;
                btnDegis.Enabled = false;
                btnEkle.Enabled = false;
                groupBox1.Visible = true;
                groupBox2.Visible = true;
                groupBox3.Visible = false;
                groupBox4.Visible = true;
                txtSifre.ReadOnly = true;
                txtSifreTekrar.ReadOnly = true;
                bilgi.Text = p.PersonelBilgisiGetirİsim(cGenel.personelId);
            }
            else
            {
                groupBox1.Visible = false;
                groupBox2.Visible = false;
                groupBox3.Visible = true;
                groupBox4.Visible = false;
                bilgi.Text = p.PersonelBilgisiGetirİsim(cGenel.personelId);
            }
        }

        private void btnDegistir_Click(object sender, EventArgs e)
        {
            if(txtYeniSifre.Text.Trim()!="" || txtYeniSifreTekrar.Text.Trim()!="")
            {
                if(txtYeniSifre.Text==txtYeniSifreTekrar.Text)
                {
                    if(txtPersonelId.Text!="")
                    {
                        cPersoneller c = new cPersoneller();
                        bool sonuc = c.SifreDegis(Convert.ToInt32(txtPersonelId.Text), txtYeniSifre.Text);
                        if(sonuc)
                         { MessageBox.Show("Şifre değiştirildi");}
                    }
                    else
                    {
                        MessageBox.Show("Personel seçin");
                    }
                }
                else
                { MessageBox.Show("Şifreler aynı değil"); }

            }
            else
            { MessageBox.Show("Şifre alanını boş bırakmayınız"); }
        }

        private void cmbGorev_SelectedIndexChanged(object sender, EventArgs e)
        {
            cPersonelGorevler c = (cPersonelGorevler)cmbGorev.SelectedItem;
            txtGörevId.Text = Convert.ToString(c.PersonelGorevID);
        }

        private void cmbPersonelAd_SelectedIndexChanged(object sender, EventArgs e)
        {
            cPersoneller c = (cPersoneller)cmbPersonelAd.SelectedItem;
            txtPersonelId.Text = Convert.ToString(c.PersonelId1);
        }

        private void btnYeni_Click(object sender, EventArgs e)
        {
            btnYeni.Enabled = false;
            btnEkle.Enabled = true;
            btnSil.Enabled = false;
            btnDegistir.Enabled = false;
            txtSifre.ReadOnly = false;
            txtSifreTekrar.ReadOnly = false;
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if(lvPersoneller.SelectedItems.Count>0)
            {
                if(MessageBox.Show("Silmek istediğinizden eminmisiniz","Uyarı",MessageBoxButtons.YesNo,MessageBoxIcon.Warning)==DialogResult.Yes)
                {
                    cPersoneller c = new cPersoneller();
                   bool sonuc= c.PersonelSil(Convert.ToInt32(lvPersoneller.SelectedItems[0].Text));
                   if (sonuc)
                   {
                       MessageBox.Show("silme başarılı");
                       c.PersonelBilgileriGetirFormIdLv(lvPersoneller);
                   }
                   else
                   { MessageBox.Show("Kayıt silinemedi"); }
                }
                else
                { MessageBox.Show("Silinecek kaydı seç"); }
            }
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if(txtAd.Text.Trim()!="" & txtSoyad.Text.Trim()!="" & txtSifre.Text.Trim()!=""& txtSifreTekrar.Text.Trim()!=""& txtGörevId.Text.Trim()!="" )
            {
                if(txtSifre.Text.Trim()==txtSifreTekrar.Text.Trim())
                {
                    cPersoneller c = new cPersoneller();
                    c.PersonelAd1 = txtAd.Text.Trim();
                    c.PersonelSoyad1 = txtSoyad.Text.Trim();
                    c.PersonelParola1=txtSifre.Text.Trim();
                    c.GorevId1 = Convert.ToInt32(txtGörevId.Text);
                    bool sonuc = c.PersonelEkle(c);
                    if (sonuc)
                    {
                        MessageBox.Show("Personel Ekleme başarılı");
                        c.PersonelBilgileriGetirFormIdLv(lvPersoneller);
                    }
                    else
                        MessageBox.Show("Personel ekleme başarısız");
                }
                else
                {
                    MessageBox.Show("Şifreler aynı değil");
                }
            }
            else
            { MessageBox.Show("boş alan bırakma"); }
        }

        private void btnDegis_Click(object sender, EventArgs e)
        {
            if(lvPersoneller.SelectedItems.Count>0)
            {
             if(txtAd.Text.Trim()!="" && txtSoyad.Text.Trim()!="" && txtSifre.Text.Trim()!="" && txtSifreTekrar.Text.Trim()!=""&& txtGörevId.Text.Trim()!="" )
            {
                if(txtSifre.Text.Trim()==txtSifreTekrar.Text.Trim())
                {
                    cPersoneller c = new cPersoneller();
                    c.PersonelAd1 = txtAd.Text.Trim();
                    c.PersonelSoyad1 = txtSoyad.Text.Trim();
                    c.PersonelParola1=txtSifre.Text.Trim();
                    c.GorevId1 = Convert.ToInt32(txtGörevId.Text);
                    bool sonuc = c.PersonelGuncelle(c,Convert.ToInt32(txPersonelId.Text));
                    if (sonuc)
                    {MessageBox.Show("Personel güncelleme başarılı");
                        c.PersonelBilgileriGetirFormIdLv(lvPersoneller);}
                    else
                    {MessageBox.Show("Personel güncelleme başarısız");
                    }
                        
                }
                else
                {
                    MessageBox.Show("Şifreler aynı değil");
                }
            }
            }
            else
            {
                MessageBox.Show("Güncellenecek personeli seçin");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
        if(textBox8.Text.Trim()!="" ||  textBox6. Text.Trim()!="")
            {
                if(textBox8. Text==textBox6. Text)
                {
                    if(cGenel.personelId.ToString() !="")
                    {
                        cPersoneller c = new cPersoneller();
                        bool sonuc = c.SifreDegis(Convert.ToInt32(cGenel.personelId), txtYeniSifre.Text);
                        if(sonuc)
                         { MessageBox.Show("Şifre değiştirildi");}
                    }
                }
                else
                { MessageBox.Show("Şifreler aynı değil"); }

            }
            else
            { MessageBox.Show("Şifre alanını boş bırakmayınız"); }
        }

        private void lvPersoneller_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvPersoneller.SelectedItems.Count > 0)
            {
                btnSil.Enabled = true;
                btnDegis.Enabled = true;
                txPersonelId.Text = lvPersoneller.SelectedItems[0].SubItems[0].Text;
                txtAd.Text = lvPersoneller.SelectedItems[0].SubItems[3].Text;
                txtSoyad.Text = lvPersoneller.SelectedItems[0].SubItems[4].Text;
                txtGörevId.Text = lvPersoneller.SelectedItems[0].SubItems[1].Text;
                
                cmbGorev.SelectedIndex = Convert.ToInt32(lvPersoneller.SelectedItems[0].SubItems[1].Text) - 1;

            }
            else
            {
                btnSil.Enabled = false; btnDegis.Enabled = false;
            }
            
   
        }

        }
        

        
    }

