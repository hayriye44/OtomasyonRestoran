using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace _14542513HayriyeCingöz_restoran_
{
    class cSiparis
    {
        cGenel gnl = new cGenel();
        private int _Id;

        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private int _adisyonID;

        public int AdisyonID
        {
            get { return _adisyonID; }
            set { _adisyonID = value; }
        }
        private int _urunId;

        public int UrunId
        {
            get { return _urunId; }
            set { _urunId = value; }
        }
        private int _adet;

        public int Adet
        {
            get { return _adet; }
            set { _adet = value; }
        }
        private int _masaId;

        public int MasaId
        {
            get { return _masaId; }
            set { _masaId = value; }
        }
        public void SiparisleriGetir(ListView Iv, int AdisyonId)
        {
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Select URUNAD,FIYAT,satislar.URUNID,satislar.ADET,satislar.ID from satislar Inner join urunler on satislar.URUNID=urunler.ID Where ADISYONID=@AdisyonId", con);
            SqlDataReader dr = null;
            cmd.Parameters.Add("@AdisyonId", SqlDbType.Int).Value = AdisyonId;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                dr = cmd.ExecuteReader();
                int sayac = 0;
                while (dr.Read())
                {
                    Iv.Items.Add(dr["URUNAD"].ToString());
                    Iv.Items[sayac].SubItems.Add(dr["ADET"].ToString());
                    Iv.Items[sayac].SubItems.Add(dr["URUNID"].ToString());
                    Iv.Items[sayac].SubItems.Add((Convert.ToDecimal(dr["FIYAT"]) * Convert.ToDecimal(dr["ADET"])).ToString());
                    Iv.Items[sayac].SubItems.Add(dr["ID"].ToString());
                    sayac++;
                }
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
            }
            finally
            {
                dr.Close();
                con.Dispose();
                con.Close();
            }
        }
        public bool SiparisleriVeriTabaninaKaydet(cSiparis Bilgiler)
        {
            bool sonuc = false;
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Insert Into satislar(ADISYONID,URUNID,ADET,MASAID)values (@AdisyonNo,@UrunId,@Adet,@MasaId)", con);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.Parameters.Add("@AdisyonNo", SqlDbType.Int).Value = Bilgiler._adisyonID;
                cmd.Parameters.Add("@UrunId", SqlDbType.Int).Value = Bilgiler._urunId;
                cmd.Parameters.Add("@Adet", SqlDbType.Int).Value = Bilgiler.Adet;
                cmd.Parameters.Add("@MasaId", SqlDbType.Int).Value = Bilgiler._masaId;
                sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
            }
            catch (SqlException ex)
            { string hata = ex.Message; }
            finally { con.Dispose(); con.Close(); }
            return sonuc;
        }
        public void SiparisSil(int satisId)
        {
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Delete From satislar Where ID=@SatisID" ,con);
            cmd.Parameters.Add("@SatisID", SqlDbType.Int).Value = satisId;
            if (con.State == ConnectionState.Closed)
            { con.Open(); }
            cmd.ExecuteNonQuery();
            con.Dispose();
            con.Close();
        }
        public decimal GenelToplamBul(int musteriId)
        {
            decimal geneltoplam = 0;
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("SELECT SUM(dbo.satislar.ADET * FIYAT) AS fiyat FROM dbo.musteriler INNER JOIN dbo.paketSiparis ON  dbo.musteriler.ID = dbo.paketSiparis.MUSTERID INNER JOIN  adisyonlar ON adisyonlar.ID = paketSiparis.ADISYONID INNER JOIN satislar ON dbo.adisyonlar.ID =dbo.satislar.ADISYONID INNER JOIN dbo.urunler ON dbo.satislar.URUNID=urunler.ID WHERE  (dbo.paketSiparis.MUSTERID = @müsteriId) AND (dbo.paketSiparis.DURUM = 0)", con);
            cmd.Parameters.Add("@müsteriId", SqlDbType.Int).Value = musteriId;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                geneltoplam = Convert.ToDecimal(cmd.ExecuteScalar());

            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
            }
            finally
            { con.Dispose(); con.Close(); } return geneltoplam;
        }
        public void AdisyonPaketSiparisDetay(ListView lv ,int adisyonId)
        {
            lv.Items.Clear();
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("SELECT satislar.ID as satisID,urunler.URUNAD,urunler.FIYAT,satislar.ADET from satislar Inner Join adisyonlar on adisyonlar.ID=satislar.ADISYONID Inner Join urunler on urunler.ID=satislar.URUNID where satislar.ADISYONID=@adisyonId", con);
            cmd.Parameters.Add("@adisyonId", SqlDbType.Int).Value = adisyonId;
            SqlDataReader dr=null;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                
                int sayac=0;
                dr=cmd.ExecuteReader();
                while(dr.Read())
                {
                    lv.Items.Add(dr["satisID"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["URUNAD"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["ADET"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["FIYAT"].ToString());
                    sayac++;
                }

            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
            }
            finally
            { dr.Close(); con.Dispose(); con.Close(); }
        }
        }
    }
