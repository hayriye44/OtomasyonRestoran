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
    public partial class frmMusteriEkle : Form
    {
        public frmMusteriEkle()
        {
            InitializeComponent();
        }

        private void btnYeniMüsteri_Click(object sender, EventArgs e)
        {
           if(txtMüsteriTelefon.Text.Length>6)
           {
               if(txtmüsteriAd.Text=="" || txtMüsteriSoyad.Text=="")
               {
                   MessageBox.Show("Lütfen müsteri adı ve soyadını giriniz");
               }
               else
               {
                   cMusteriler c = new cMusteriler();
                   bool sonuc = c.MüsteriVarmı(txtMüsteriTelefon.Text);
                   if(!sonuc)
                   {
                       c.Musateriad = txtmüsteriAd.Text;
                       c.Musterisoyad = txtMüsteriSoyad.Text;
                       c.Telefon = txtMüsteriTelefon.Text;
                       c.Adres = txtMüsteriAdres.Text;
                       c.Email = txtMüsteriEmail.Text;
                       txtMusteriNo.Text = c.MusteriEkle(c).ToString();
                       if(txtMusteriNo.Text!="")
                       {
                           MessageBox.Show("Müşteri Eklendi");
                       }
                       else
                       {
                           MessageBox.Show("Müşteri eklenemedi");
                       }
                   }
                   else
                   { MessageBox.Show("Bu telefon numarasını kullanan kayıtlı müşteri var kayıt olmadı"); }
               }
           }
           else
           {
               MessageBox.Show("Lütfen en az 7 haneli telefon numarası girin");
           }
        }

        private void btnMüsteriSec_Click(object sender, EventArgs e)
        {
            if(cGenel._MusteriEkleme==0)
            {
                frmRezervasyon frm = new frmRezervasyon();
                cGenel._MusteriEkleme = 1;
                this.Close();
                frm.Show();
            }
            else if (cGenel._MusteriEkleme == 1)
            {
                frmPaketSiparis frm = new frmPaketSiparis();
                cGenel._MusteriEkleme = 0;
                this.Close();
                frm.Show();
            }
        }

        private void btnMüsteriGüncelle_Click(object sender, EventArgs e)
        {
            if (txtMüsteriTelefon.Text.Length > 6)
            {
                if (txtmüsteriAd.Text == "" || txtMüsteriSoyad.Text == "")
                {
                    MessageBox.Show("Lütfen müsteri adı ve soyadını giriniz");
                }
                else
                {
                    cMusteriler c = new cMusteriler();
                       c.Musateriad = txtmüsteriAd.Text;
                        c.Musterisoyad = txtMüsteriSoyad.Text;
                        c.Telefon = txtMüsteriTelefon.Text;
                        c.Adres = txtMüsteriAdres.Text;
                        c.Email = txtMüsteriEmail.Text;
                        c.MusteriId =Convert.ToInt32( txtMusteriNo.Text);
                       bool sonuc= c.MusteriBilgileriGüncelle(c);
                    if (sonuc)
                    {
                        
                        if (txtMusteriNo.Text != "")
                        {
                            MessageBox.Show("Müşteri Güncellend");
                        }
                        else
                        {
                            MessageBox.Show("Müşteri güncellenmedi");
                        }
                    }
                    else
                    { MessageBox.Show("Bu telefon numarasını kullanan kayıtlı müşteri var kayıt olmadı"); }
                }
            }
            else
            {
                MessageBox.Show("Lütfen en az 7 haneli telefon numarası girin");
            }
        }

        private void btnGeriDon_Click(object sender, EventArgs e)
        {

            frmMusteriAra frm = new frmMusteriAra();
            this.Close();
            frm.Show();
        }

        private void frmMusteriEkle_Load(object sender, EventArgs e)
        {
            if(cGenel._MusteriID>0)
            {
             cMusteriler c = new cMusteriler();
             txtMusteriNo.Text = cGenel._MusteriID.ToString();
            c.MusteriyiIdyeGöreGetir(Convert.ToInt32(txtMusteriNo.Text), txtmüsteriAd, txtMüsteriSoyad, txtMüsteriTelefon, txtMüsteriAdres, txtMüsteriEmail);
            }

           
        }
    }
}
