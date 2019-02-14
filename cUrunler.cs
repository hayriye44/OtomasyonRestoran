using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace _14542513HayriyeCingöz_restoran_
{
    class cUrunler
    {
        cGenel gnl = new cGenel();
        private int _urunId;

        public int UrunId
        {
            get { return _urunId; }
            set { _urunId = value; }
        }
        private int _urunTurNo;

        public int UrunTurNo
        {
            get { return _urunTurNo; }
            set { _urunTurNo = value; }
        }
        private string _urunAd;

        public string UrunAd
        {
            get { return _urunAd; }
            set { _urunAd = value; }
        }
        private decimal _fiyat;

        public decimal Fiyat
        {
            get { return _fiyat; }
            set { _fiyat = value; }
        }
        private string _aciklama;

        public string Aciklama
        {
            get { return _aciklama; }
            set { _aciklama = value; }
        }
        //urunadına göre listeleme
        public void UrunlerListeleAdaGöre(ListView lv,string urunAdi)
        {
            lv.Items.Clear();
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Select * from urunler where DURUM=0 and URUNAD like '%'+@urunadi+ '%'", con);
            SqlDataReader dr = null;
            cmd.Parameters.Add("@urunadi", SqlDbType.VarChar).Value = urunAdi;
            try
            {
                if (con.State == ConnectionState.Closed)
                { con.Open(); }
                dr = cmd.ExecuteReader();
                int sayac = 0;
                while (dr.Read())
                {
                    lv.Items.Add(dr["ID"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["KATEGORID"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["URUNAD"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["ACIKLAMA"].ToString());
                    lv.Items[sayac].SubItems.Add(string.Format("{0:0#00.0}",dr["FIYAT"].ToString()) );
                    sayac++;
                }
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                throw;
            }
            finally
            {
                dr.Close();
                con.Dispose(); con.Close();
            }
        }
        public bool UrunEkle(cUrunler c)
        {
            bool sonuc = false;
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Insert Into urunler(URUNAD,KATEGORID,FIYAT)values (@URUNAD,@KATEGORID,@FIYAT)", con);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.Parameters.Add("@URUNAD", SqlDbType.VarChar).Value = c._urunAd;
                cmd.Parameters.Add("@KATEGORID", SqlDbType.Int).Value = c._urunTurNo;
                cmd.Parameters.Add("@FIYAT", SqlDbType.Money).Value = c._fiyat;
                sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
            }
            catch (SqlException ex)
            { string hata = ex.Message; throw; }
            finally { con.Dispose(); con.Close(); }
            return sonuc;
        }
        public void UrunlerListele(ListView lv)
        {
            lv.Items.Clear();
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Select urunler.*,KATEGORIADI from urunler Inner Join kategoriler on kategoriler.ID=urunler.KATEGORID where urunler.DURUM=0 ", con);
            SqlDataReader dr = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                { con.Open(); }
                dr = cmd.ExecuteReader();
                int sayac = 0;
                while (dr.Read())
                {
                    lv.Items.Add(dr["ID"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["KATEGORID"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["KATEGORIADI"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["URUNAD"].ToString());
                    lv.Items[sayac].SubItems.Add(string.Format("{0:0#00.0}", dr["FIYAT"].ToString()));
                 //   lv.Items[sayac].SubItems.Add(dr["ACIKLAMA"].ToString());
                    sayac++;
                }
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                throw;
            }
            finally
            {
                dr.Close();
                con.Dispose(); con.Close();
            }
        }
        public bool UrunleriGuncelle(cUrunler u)
        {
            bool sonuc = false;
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Update urunler set URUNAD=@URUNAD,KATEGORID=@KATEGORID,ACIKLAMA=@ACIKLAMA,FIYAT=@FIYAT where ID=@urunID", con);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.Parameters.Add("@URUNAD", SqlDbType.VarChar).Value = u.UrunAd;
                cmd.Parameters.Add("@KATEGORID", SqlDbType.Int).Value = u._urunTurNo;
                cmd.Parameters.Add("@ACIKLAMA", SqlDbType.VarChar).Value = u._aciklama;
                cmd.Parameters.Add("@FIYAT", SqlDbType.Money).Value = u._fiyat;
                cmd.Parameters.Add("@urunID", SqlDbType.VarChar).Value = u._urunId;
                sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
            }
            catch (SqlException ex)
            { string hata = ex.Message; throw; }
            finally { con.Dispose(); con.Close(); }
            return sonuc;

        }
        public bool UrunSil(cUrunler u)
        {
            bool sonuc = false;
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Update urunler set urunler.DURUM=1  from urunler Inner Join kategoriler on kategoriler.ID=urunler.KATEGORID where kategoriler.DURUM=1 or urunler.ID=@urunID ", con);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.Parameters.Add("@urunID", SqlDbType.VarChar).Value = u._urunId;
                sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
            }
            catch (SqlException ex)
            { string hata = ex.Message; throw; }
            finally { con.Dispose(); con.Close(); }
            return sonuc;

        }
        public void UrunlerListeleKatIdyeGore(ListView lv, int urunId)
        {
            lv.Items.Clear();
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand(" Select urunler.*,KATEGORIADI from urunler Inner Join kategoriler on kategoriler.ID=urunler.KATEGORID where urunler.DURUM=0 and urunler.KATEGORID=@urunId", con);
            SqlDataReader dr = null;
            cmd.Parameters.Add("@urunId", SqlDbType.Int).Value = urunId;
            try
            {
                if (con.State == ConnectionState.Closed)
                { con.Open(); }
                dr = cmd.ExecuteReader();
                int sayac = 0;
                while (dr.Read())
                {
                    lv.Items.Add(dr["ID"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["KATEGORID"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["KATEGORIADI"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["URUNAD"].ToString());
                    lv.Items[sayac].SubItems.Add(string.Format("{0:0#00.0}", dr["FIYAT"].ToString()));
                    sayac++;
                }
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                throw;
            }
            finally
            {
                dr.Close();
                con.Dispose(); con.Close();
            }
        }
        public void UrunlerListeleIstatistigeGore(ListView lv,DateTimePicker baslangic,DateTimePicker bitis)
        {

           lv.Items.Clear();
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Select top 10 urunler.URUNAD,SUM(satislar.ADET) as adeti from kategoriler Inner Join urunler on urunler.KATEGORID=kategoriler.ID Inner Join satislar on satislar.URUNID=urunler.ID Inner Join adisyonlar on satislar.ADISYONID=adisyonlar.ID where (CONVERT(datetime,TARIH,104) BETWEEN CONVERT(datetime,'25.04.2017',104) AND CONVERT(datetime,'01.05.2017',104)) group by urunler.URUNAD order by adeti desc", con);
            SqlDataReader dr = null;
            cmd.Parameters.Add("@baslangic", SqlDbType.VarChar).Value = baslangic.Value.ToShortDateString();
            cmd.Parameters.Add("@bitis", SqlDbType.VarChar).Value = bitis.Value.ToShortDateString();
            try
            {
                if (con.State == ConnectionState.Closed)
                { con.Open(); }
                dr = cmd.ExecuteReader();
                int sayac = 0;
                while (dr.Read())
                {
                    lv.Items[sayac].SubItems.Add(dr["URUNAD"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["ADETİ"].ToString());
                    sayac++;
                }
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                throw;
            }
            finally
            {
                dr.Close();
                con.Dispose(); con.Close();
            }

        }
        public void UrunlerListeleIstatistigeGoreUrunKategoriId(ListView lv, DateTimePicker baslangic, DateTimePicker bitis,int urunkatId)
        {

            lv.Items.Clear();
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Select top 10 urunler.URUNAD,SUM(satislar.ADET) as adeti from kategoriler Inner Join urunler on urunler.KATEGORID=kategoriler.ID Inner Join satislar on satislar.URUNID=urunler.ID Inner Join adisyonlar on satislar.ADISYONID=adisyonlar.ID where (CONVERT(datetime,TARIH,104) BETWEEN CONVERT(datetime,'25.04.2017',104) AND CONVERT(datetime,'01.05.2017',104))and(urunler.KATEGORID=@urunkatId) group by urunler.URUNAD order by adeti desc", con);
            SqlDataReader dr = null;
            cmd.Parameters.Add("@baslangic", SqlDbType.VarChar).Value = baslangic.Value.ToShortDateString();
            cmd.Parameters.Add("@bitis", SqlDbType.VarChar).Value = bitis.Value.ToShortDateString();
            cmd.Parameters.Add("@urunkatId", SqlDbType.Int).Value = urunkatId;
            try
            {
                if (con.State == ConnectionState.Closed)
                { con.Open(); }
                dr = cmd.ExecuteReader();
                int sayac = 0;
                while (dr.Read())
                {
                    lv.Items[sayac].SubItems.Add(dr["URUNAD"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["ADETİ"].ToString());
                    sayac++;
                }
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                throw;
            }
            finally
            {
                dr.Close();
                con.Dispose(); con.Close();
            }

        }
    }
}
