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
    public partial class frmMutfak : Form
    {
        public frmMutfak()
        {
            InitializeComponent();
        }

        private void lvKategoriler_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void frmMutfak_Load(object sender, EventArgs e)
        {
            cUrunCesitleri anakategori = new cUrunCesitleri();
            anakategori.UrunCesitleriGetir(cbKategoriler);
            cbKategoriler.Items.Insert(0, "Tüm Kategoriler");
            cbKategoriler.SelectedIndex = 0;
            label5.Visible = false;
            txtAra.Visible = false;
            cUrunler altkategori = new cUrunler();
            altkategori.UrunlerListele(lvGidaListesi);
        }
        private void Temizle()
        {
            txtgıdaad.Clear();
            txtgıdafiyat.Clear();
            txtgıdafiyat.Text = string.Format("{0:##0.00}", 0);
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (rbAltKategori.Checked)
            {
                if (txtgıdaad.Text.Trim() == "" || txtgıdafiyat.Text.Trim() == "" || cbKategoriler.SelectedItem.ToString() == "Tüm Kategoriler")
                {
                    MessageBox.Show("Ürün Adı ,fiyatı veya kategori seçilmemis", "Bilgiler Eksik", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    cUrunler urun = new cUrunler();
                    urun.Fiyat = Convert.ToDecimal(txtgıdafiyat.Text);
                    urun.UrunAd = txtgıdaad.Text;
                    urun.UrunTurNo = urunTurno;
                    bool sonuc = urun.UrunEkle(urun);
                    if (sonuc)
                    {
                        MessageBox.Show("Ürün eklenmiştir");
                        cbKategoriler_SelectedIndexChanged(sender, e);
                        yenile();
                        Temizle();
                    }
                }
            }
            else
            {
                if (txtKategorıAd.Text.Trim() == "")
                {
                    MessageBox.Show("Lütfen Kategori ismi girin", "Bilgiler Eksik", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    cUrunCesitleri uc = new cUrunCesitleri();
                    uc.KategoriAd = txtKategorıAd.Text;
                    uc.Aciklama = txtAciklama.Text;
                    bool sonuc = uc.UrunKategoriEkle(uc);
                    if (sonuc)
                    {
                        MessageBox.Show("Kategori eklenmisştir.");
                        yenile();
                        Temizle();
                    }
                }
            }
        }
        int urunTurno = 0;
        private void cbKategoriler_SelectedIndexChanged(object sender, EventArgs e)
        {
            cUrunler u = new cUrunler();
            if (cbKategoriler.SelectedItem.ToString() == "Tüm Kategoriler")
            {
                u.UrunlerListele(lvGidaListesi);
            }
            else
            {
                cUrunCesitleri cesit = (cUrunCesitleri)cbKategoriler.SelectedItem;
                urunTurno = cesit.UrunTurNo;
                u.UrunlerListeleKatIdyeGore(lvGidaListesi, urunTurno);

            }

        }

        private void btnDegis_Click(object sender, EventArgs e)
        {
            if (rbAltKategori.Checked)
            {
                if (txtgıdaad.Text.Trim() == "" || txtgıdafiyat.Text.Trim() == "" || cbKategoriler.SelectedItem.ToString() == "Tüm Kategoriler")
                {
                    MessageBox.Show("Ürün Adı ,fiyatı veya kategori seçilmemis", "Bilgiler Eksik", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    cUrunler urun = new cUrunler();
                    urun.Fiyat = Convert.ToDecimal(txtgıdafiyat.Text);
                    urun.UrunAd = txtgıdaad.Text;
                    urun.UrunTurNo = urunTurno;
                    urun.Aciklama = "urun güncellendi";
                    urun.UrunId = Convert.ToInt32(txtUrunId.Text);
                    bool sonuc = urun.UrunleriGuncelle(urun);
                    if (sonuc)
                    {
                        MessageBox.Show("Ürün güncellenmiştir");
                        yenile();
                        Temizle();
                    }
                }
            }
            else
            {
                if (txtKategoriId.Text.Trim() == "")
                {
                    MessageBox.Show("Lütfen Kategori seçiniz", "Bilgiler Eksik", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    cUrunCesitleri uc = new cUrunCesitleri();
                    uc.KategoriAd = txtKategorıAd.Text;
                    uc.Aciklama = txtAciklama.Text;
                    uc.UrunTurNo = Convert.ToInt32(txtKategoriId.Text);
                    bool sonuc = uc.UrunKategoriGuncelle(uc);
                    if (sonuc)
                    {
                        MessageBox.Show("Kategori güncellenmiştir.");
                        yenile();
                        uc.UrunCesitleriGetir(lvKategori);
                        Temizle();
                    }
                }
            }
        }

        private void lvGidaListesi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvGidaListesi.SelectedItems.Count > 0)
            {
                txtgıdaad.Text = lvGidaListesi.SelectedItems[0].SubItems[3].Text;
                txtgıdafiyat.Text = lvGidaListesi.SelectedItems[0].SubItems[4].Text;
                txtUrunId.Text = lvGidaListesi.SelectedItems[0].SubItems[0].Text;
            }
        }

        private void lvKategori_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvKategori.SelectedItems.Count > 0)
            {
                txtKategorıAd.Text = lvKategori.SelectedItems[0].SubItems[1].Text;
                txtKategoriId.Text = lvKategori.SelectedItems[0].SubItems[0].Text;
                txtAciklama.Text = lvKategori.SelectedItems[0].SubItems[2].Text;
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (rbAltKategori.Checked)
            {
                if(lvGidaListesi.SelectedItems.Count>0)
                {if (MessageBox.Show("Ürünü silmeye eminmisin", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    cUrunler urun = new cUrunler();
                    urun.UrunId = Convert.ToInt32(txtUrunId.Text);
                    bool sonuc = urun.UrunSil(urun);
                    if (sonuc)
                    {
                        MessageBox.Show("Ürün silinmiştir");
                        
                        cbKategoriler_SelectedIndexChanged(sender, e);
                        yenile();
                        Temizle();
                    }
                    else
                        MessageBox.Show("ürün silinemedi");
                } 
                }
                else
                { MessageBox.Show("silmek için ürün seç"); }
            }
            else
            {
                if(lvKategori.SelectedItems.Count>0)
                { 
                    if (MessageBox.Show("Ürünü silmeye eminmisin", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        cUrunCesitleri urun = new cUrunCesitleri();
                        bool sonuc = urun.UrunKategoriSil(Convert.ToInt32( txtKategoriId.Text));
                        if (sonuc)
                        {
                            MessageBox.Show("Ürün silinmiştir");
                            yenile();
                            Temizle();
                        }
                        else
                            MessageBox.Show("ürün silinemedi");
                    }
                }
                else
                {
                    MessageBox.Show("Silinecek kategoriyi seç");
                }
                
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
            frmMenu frm = new frmMenu();
            this.Close();
            frm.Show();
        }

        private void btnBul_Click(object sender, EventArgs e)
        {
            label5.Visible = true;
            txtAra.Visible = true;

        }

        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            if(rbAltKategori.Checked)
            {
                cUrunler u = new cUrunler();
                u.UrunlerListeleAdaGöre(lvGidaListesi, txtAra.Text);
            }
            else
            {
                cUrunCesitleri uc = new cUrunCesitleri();
                uc.UrunCesitleriGetirArama(lvKategori, txtAra.Text);
            }
        }

        private void rbAltKategori_CheckedChanged(object sender, EventArgs e)
        {
            panelUrun.Visible = true;
            panelAnaKategori.Visible = false;
            lvKategori.Visible = false;
            lvGidaListesi.Visible = true;
            yenile();
        }

        private void rbAnaKategori_CheckedChanged(object sender, EventArgs e)
        {

            panelUrun.Visible = false;
            panelAnaKategori.Visible = true;
            lvKategori.Visible = true;
            lvGidaListesi.Visible = false;
            yenile();
           // cUrunCesitleri uc = new cUrunCesitleri();
           // uc.UrunCesitleriGetir(lvKategori);
        }
        private void yenile()
        {
            cUrunCesitleri uc = new cUrunCesitleri();
            uc.UrunCesitleriGetir(cbKategoriler);
            cbKategoriler.Items.Insert(0, "Tüm Kategoriler");
            cbKategoriler.SelectedIndex = 0;
            uc.UrunCesitleriGetir(lvKategori);
            cUrunler u = new cUrunler();
            u.UrunlerListele(lvGidaListesi);
        }
    }
}
