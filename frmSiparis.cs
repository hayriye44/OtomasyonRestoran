using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _14542513HayriyeCingöz_restoran_
{
    public partial class frmSiparis : Form
    {
        public frmSiparis()
        {
            InitializeComponent();
        }

        private void btnCıkıs_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Çıkmak istediğinize eminmisiniz", "uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            { Application.Exit(); }
        }

        private void btnGeriDon_Click(object sender, EventArgs e)
        {
            frmMenu frm = new frmMenu();
            this.Close();
            frm.Show();
        }
        void Islem(Object sender, EventArgs e)
        {
            Button btn = sender as Button;
            switch (btn.Name)
            {
                case "btn1": txtAdet.Text += (1).ToString();
                    break;
                case "btn2": txtAdet.Text += (2).ToString();
                    break;
                case "btn3": txtAdet.Text += (3).ToString();
                    break;
                case "btn4": txtAdet.Text += (4).ToString();
                    break;
                case "btn5": txtAdet.Text += (5).ToString();
                    break;
                case "btn6": txtAdet.Text += (6).ToString();
                    break;
                case "btn7": txtAdet.Text += (7).ToString();
                    break;
                case "btn8": txtAdet.Text += (8).ToString();
                    break;
                case "btn9": txtAdet.Text += (9).ToString();
                    break;
                case "btn0": txtAdet.Text += (0).ToString();
                    break;
                default: MessageBox.Show("sayı gir");
                    break;
            }
        }
        int tableId; int AdditionId;
        private void frmSiparis_Load(object sender, EventArgs e)
        {
            lblMasaNo.Text = cGenel._ButtonValue;
            cMasalar ms = new cMasalar();
             tableId = ms.MasaNosu(cGenel._ButtonName);
            if (ms.MasaDurumu(tableId, 2) == true || ms.MasaDurumu(tableId, 4) == true)
            {
                cAdisyon Ad = new cAdisyon();
                 AdditionId = Ad.AdisyonBilgisiGetir(tableId);
                cSiparis siparisler = new cSiparis();
                siparisler.SiparisleriGetir(lvSiparisler, AdditionId);

            }
            btn1.Click += new EventHandler(Islem);
            btn2.Click += new EventHandler(Islem);
            btn3.Click += new EventHandler(Islem);
            btn4.Click += new EventHandler(Islem);
            btn5.Click += new EventHandler(Islem);
            btn6.Click += new EventHandler(Islem);
            btn7.Click += new EventHandler(Islem);
            btn8.Click += new EventHandler(Islem);
            btn9.Click += new EventHandler(Islem);
            btn0.Click += new EventHandler(Islem);
        }

        private void btnAnaYemek3_Click_1(object sender, EventArgs e)
        {
            cUrunCesitleri uc = new cUrunCesitleri();
            uc.UrunCesitleriGetir(lvMenu, btnAnaYemek3);
        }

        private void btnIcecek8_Click(object sender, EventArgs e)
        {
            cUrunCesitleri uc = new cUrunCesitleri();
            uc.UrunCesitleriGetir(lvMenu, btnIcecek8);
        }

        private void btnTatlılar7_Click(object sender, EventArgs e)
        {
            cUrunCesitleri uc = new cUrunCesitleri();
            uc.UrunCesitleriGetir(lvMenu, btnTatlılar7);
        }

        private void btnSalata6_Click(object sender, EventArgs e)
        {
            cUrunCesitleri uc = new cUrunCesitleri();
            uc.UrunCesitleriGetir(lvMenu, btnSalata6);
        }

        private void btnFastFood5_Click(object sender, EventArgs e)
        {
            cUrunCesitleri uc = new cUrunCesitleri();
            uc.UrunCesitleriGetir(lvMenu, btnFastFood5);
        }

        private void btnCorba1_Click(object sender, EventArgs e)
        {
            cUrunCesitleri uc = new cUrunCesitleri();
            uc.UrunCesitleriGetir(lvMenu, btnCorba1);
        }

        private void btnMakarna4_Click(object sender, EventArgs e)
        {
            cUrunCesitleri uc = new cUrunCesitleri();
            uc.UrunCesitleriGetir(lvMenu,btnMakarna4);
        }

        private void btnAraSıcak2_Click(object sender, EventArgs e)
        {
            cUrunCesitleri uc = new cUrunCesitleri();
            uc.UrunCesitleriGetir(lvMenu,btnAraSıcak2);
        }
        int sayac = 0; int sayac2 = 0;
        private void lvMenu_DoubleClick(object sender, EventArgs e)
        {
            if(txtAdet.Text=="")
            {
                txtAdet.Text = "1";
            }
            if(lvMenu.Items.Count>0)
            {
                sayac = lvSiparisler.Items.Count;
                lvSiparisler.Items.Add(lvMenu.SelectedItems[0].Text);
                lvSiparisler.Items[sayac].SubItems.Add(txtAdet.Text);
                lvSiparisler.Items[sayac].SubItems.Add(lvMenu.SelectedItems[0].SubItems[2].Text);
                lvSiparisler.Items[sayac].SubItems.Add((Convert.ToDecimal( lvMenu.SelectedItems[0].SubItems[1].Text)*Convert.ToDecimal( txtAdet.Text)).ToString());
                lvSiparisler.Items[sayac].SubItems.Add("0");//satışıdsi
                sayac2 = lvYeniEklenen.Items.Count;
                lvSiparisler.Items[sayac].SubItems.Add(sayac2.ToString());
                lvYeniEklenen.Items.Add(AdditionId.ToString());
                lvYeniEklenen.Items[sayac2].SubItems.Add(lvMenu.SelectedItems[0].SubItems[2].Text);
                lvYeniEklenen.Items[sayac2].SubItems.Add(txtAdet.Text);
                lvYeniEklenen.Items[sayac2].SubItems.Add(tableId.ToString());
                lvYeniEklenen.Items[sayac2].SubItems.Add(sayac2.ToString());
                sayac2++;
                txtAdet.Text = "";
            }
        }
        ArrayList silinenler = new ArrayList();
        private void btnsiparis_Click_1(object sender, EventArgs e)
        {
            //1.Masaboş2.masaDolu3.masarezerve4.masarezerveoturanvar
            cMasalar masa = new cMasalar();
            cAdisyon yeniAdisyon = new cAdisyon();
            cSiparis siparisKaydet = new cSiparis();
            frmMasalar m = new frmMasalar();
            bool sonuc = false;
            if (masa.MasaDurumu(tableId, 1) == true)
            {
                yeniAdisyon.ServisTurNo = 1;
                yeniAdisyon.PersonelId = 1;
                yeniAdisyon.MasaId = tableId;
                yeniAdisyon.Tarih = DateTime.Now;
                sonuc = yeniAdisyon.YeniAdisyonuGetir(yeniAdisyon);
                masa.MasaDurumuDegistir(cGenel._ButtonName,2);
                if (lvSiparisler.Items.Count>0)
                {
                    for (int i = 0; i < lvSiparisler.Items.Count; i++)
                    {
                        siparisKaydet.MasaId = tableId;
                        siparisKaydet.UrunId = Convert.ToInt32(lvSiparisler.Items[i].SubItems[2].Text);
                        siparisKaydet.AdisyonID = yeniAdisyon.AdisyonBilgisiGetir(tableId);
                        siparisKaydet.Adet = Convert.ToInt32(lvSiparisler.Items[i].SubItems[1].Text);
                        siparisKaydet.SiparisleriVeriTabaninaKaydet(siparisKaydet);

                    }
                    this.Close();
                    m.Show();
                }
            }
            else if (masa.MasaDurumu(tableId, 2) == true || masa.MasaDurumu(tableId, 4) == true)
            {
                if ( lvYeniEklenen.Items.Count>0)
                {
                    for (int i = 0; i < lvYeniEklenen.Items.Count; i++)
                    {
                        siparisKaydet.MasaId = tableId;
                        siparisKaydet.UrunId = Convert.ToInt32(lvYeniEklenen.Items[i].SubItems[1].Text);
                        siparisKaydet.AdisyonID = yeniAdisyon.AdisyonBilgisiGetir(tableId);
                        siparisKaydet.Adet = Convert.ToInt32(lvYeniEklenen.Items[i].SubItems[2].Text);
                        siparisKaydet.SiparisleriVeriTabaninaKaydet(siparisKaydet);
                    }
                    cGenel._AdisyonId =Convert.ToString( yeniAdisyon.AdisyonBilgisiGetir(tableId));
                }
                if(silinenler.Count>0)
                {
                    foreach(string item in silinenler)
                    {
                        siparisKaydet.SiparisSil(Convert.ToInt32(item));
                    }
                }
                this.Close();
                m.Show();
            }
            else if(masa.MasaDurumu(tableId, 3) == true )
            {
                yeniAdisyon.ServisTurNo = 1;
                yeniAdisyon.PersonelId = 1;
                yeniAdisyon.MasaId = tableId;
                yeniAdisyon.Tarih = DateTime.Now;
                sonuc = yeniAdisyon.YeniAdisyonuGetir(yeniAdisyon);
                masa.MasaDurumuDegistir(cGenel._ButtonName, 4);
                if (lvSiparisler.Items.Count > 0)
                {
                    for (int i = 0; i < lvSiparisler.Items.Count; i++)
                    {
                        siparisKaydet.MasaId = tableId;
                        siparisKaydet.UrunId = Convert.ToInt32(lvSiparisler.Items[i].SubItems[2].Text);
                        siparisKaydet.AdisyonID = yeniAdisyon.AdisyonBilgisiGetir(tableId);
                        siparisKaydet.Adet = Convert.ToInt32(lvSiparisler.Items[i].SubItems[1].Text);
                        siparisKaydet.SiparisleriVeriTabaninaKaydet(siparisKaydet);

                    }
                    this.Close();
                    m.Show();
                }
            }
        }
        private void lvSiparisler_DoubleClick(object sender, EventArgs e)
        {
            if (lvSiparisler.Items.Count > 0)
            {
                
                if (lvSiparisler.SelectedItems[0].SubItems[4].Text != "0")//satişıd boş değil yani veri tabanına kaydedilmiş veri tananından sil
                {
                    cSiparis saveorder = new cSiparis();
                    saveorder.SiparisSil(Convert.ToInt32(lvSiparisler.SelectedItems[0].SubItems[4].Text));
                }
                else
                {
                    for (int i = 0; i < lvYeniEklenen.Items.Count; i++)
                    {
                        if (lvYeniEklenen.Items[i].SubItems[4].Text == lvSiparisler.SelectedItems[0].SubItems[5].Text)
                        {
                            lvYeniEklenen.Items.RemoveAt(i);
                        }
                    }
                } 
                lvSiparisler.Items.RemoveAt(lvSiparisler.SelectedItems[0].Index);
            }
        }

        private void lvSiparisler_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtUrunNo_TextChanged(object sender, EventArgs e)
        {
            if(txtUrunNo.Text=="")
            {
                txtUrunNo.Text = "";
            }
            else {  cUrunCesitleri c = new cUrunCesitleri();
            c.UrunAra(lvMenu, Convert.ToInt32(txtUrunNo.Text));}
           
        }

        private void btnOdeme_Click(object sender, EventArgs e)
        {
            cGenel._ServisTurNo = 1;
            cGenel._AdisyonId = AdditionId.ToString();
            frmBil frm = new frmBil();
            this.Close();
            frm.Show();
        }

    }
}
