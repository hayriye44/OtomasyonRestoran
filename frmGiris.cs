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
    public partial class frmGiris : Form
    {
        public frmGiris()
        {
            InitializeComponent();
        }
        
        private void btnGiris_Click(object sender, EventArgs e)
        {
            cGenel gnl = new cGenel();
            cPersoneller p = new cPersoneller();
            bool sonuc = p.personelGirisKontrol(txtSifre.Text, cGenel.personelId);
            if(sonuc)
            {
                cPersonelHareketleri ch = new cPersonelHareketleri();
                ch.PersonelId1 = cGenel.personelId;
                ch.Islem1="Giriş yaptı";
                ch.Tarih1=DateTime.Now;
                ch.PersonelHareketKayıt(ch);
                
                this.Hide();
                frmMenu menu = new frmMenu();
                menu.Show();
            }
            else
            {
                MessageBox.Show("Şifreniz Yanlış", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void frmGiris_Load(object sender, EventArgs e)
        {
            cPersoneller p = new cPersoneller();
            p.PersonelleriGetir(cmbKullanıcıGiris);


        }

        private void cmbKullanıcıGiris_SelectedIndexChanged(object sender, EventArgs e)
        {
            cPersoneller p = (cPersoneller)cmbKullanıcıGiris.SelectedItem;
              cGenel.personelId = p.PersonelId1;
                cGenel.gorevId = p.GorevId1;
        }

        private void btnCıkıs_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Çıkmak istediğinize eminmisiniz","uyarı",MessageBoxButtons.YesNo,MessageBoxIcon.Warning)==DialogResult.Yes)
            { Application.Exit(); }
        }

    }
}
