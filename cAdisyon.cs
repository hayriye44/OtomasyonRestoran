using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _14542513HayriyeCingöz_restoran_
{
    class cAdisyon
    {
        cGenel gnl = new cGenel();
        private int _ID;
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        private int _ServisTurNo;
        public int ServisTurNo
        {
            get { return _ServisTurNo; }
            set { _ServisTurNo = value; }
        }
        private decimal _Tutar;
        public decimal Tutar
        {
            get { return _Tutar; }
            set { _Tutar = value; }
        }
        private DateTime _Tarih;
        public DateTime Tarih
        {
            get { return _Tarih; }
            set { _Tarih = value; }
        }
        private int _PersonelId;
        public int PersonelId
        {
            get { return _PersonelId; }
            set { _PersonelId = value; }
        }
        private int _Durum;
        public int Durum
        {
            get { return _Durum; }
            set { _Durum = value; }
        }
        private int _MasaId;
        public int MasaId
        {
            get { return _MasaId; }
            set { _MasaId = value; }
        }
       public int AdisyonBilgisiGetir(int MasaId)
        {
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Select top 1 ID From adisyonlar Where MASAID=@MasaId Order by ID desc", con);
            cmd.Parameters.Add("@MasaId", SqlDbType.Int).Value = MasaId;
           try
           {
             if(con.State==ConnectionState.Closed)
             {
                 con.Open();
             } MasaId = Convert.ToInt32(cmd.ExecuteScalar());
           }
           catch(SqlException ex)
           {
               string hata = ex.Message;
           }
           finally
           {
               con.Close();
           }
           return MasaId;
        }
       public bool YeniAdisyonuGetir(cAdisyon Bilgiler)
       {
           bool sonuc = false;
           SqlConnection con = new SqlConnection(gnl.conString);
           SqlCommand cmd = new SqlCommand("Insert Into adisyonlar(SERVISTURNO,TARIH,PERSONELID,MASAID,Durum) values (@ServisTurNo,@Tarih,@PersonelId,@MasaId,@Durum)", con);
           try
           {
               if (con.State == ConnectionState.Closed)
               { con.Open(); }
               cmd.Parameters.Add("@ServisTurNo", SqlDbType.Int).Value = Bilgiler.ServisTurNo;
               cmd.Parameters.Add("@Tarih", SqlDbType.DateTime).Value = Bilgiler.Tarih;
               cmd.Parameters.Add("@PersonelId", SqlDbType.Int).Value = Bilgiler.PersonelId;
               cmd.Parameters.Add("@MasaId", SqlDbType.Int).Value = Bilgiler.MasaId;
               cmd.Parameters.Add("@Durum", SqlDbType.Bit).Value = 0;
               sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
           }
           catch(SqlException ex)
           { string hata = ex.Message; }
           finally
           {
               con.Dispose();
               con.Close();
           }
           return sonuc;
       }
       public void AdisyonKapat(int adisyonId,int durum)
       {
           SqlConnection con = new SqlConnection(gnl.conString);
           SqlCommand cmd = new SqlCommand("Update adisyonlar set DURUM=@durum where ID=@adisyonId", con);

           try
           {
               if (con.State == ConnectionState.Closed)
               { con.Open(); }
               cmd.Parameters.Add("@adisyonId", SqlDbType.Int).Value = adisyonId;
               cmd.Parameters.Add("@durum", SqlDbType.Int).Value = durum;

              cmd.ExecuteScalar();
           }
           catch (SqlException ex)
           {
               string hata = ex.Message;
               throw;
           }
           finally
           { con.Dispose(); con.Close(); }
       }
        public int paketAdisyonIdAdediBul()
       {
           int miktar = 0;
           SqlConnection con = new SqlConnection(gnl.conString);
           SqlCommand cmd = new SqlCommand("Select count(*) as Sayi from adisyonlar where (DURUM=0) and (SERVISTURNO=2)", con);

           try
           {
               if (con.State == ConnectionState.Closed)
               { con.Open(); }
               miktar=Convert.ToInt32( cmd.ExecuteScalar());
           }
           catch (SqlException ex)
           {
               string hata = ex.Message;
               throw;
           }
           finally
           { con.Dispose(); con.Close(); }
           return miktar;

       }
        public void AcikPaketAdisyonlari(ListView lv)
        {
            lv.Items.Clear();
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Select paketSiparis.MUSTERID,musteriler.AD+' '+musteriler.SOYAD as Musteri,adisyonlar.ID as adisyonID from paketSiparis Inner Join musteriler on musteriler.ID=paketSiparis.MUSTERID Inner Join adisyonlar on adisyonlar.ID=paketSiparis.ADISYONID where adisyonlar.DURUM=0 ", con);
            SqlDataReader dr = null;
            try
            {
                if(con.State==ConnectionState.Closed)
                {
                    con.Open();
                }
                dr = cmd.ExecuteReader();
                int sayac = 0;
                while(dr.Read())
                {
                    lv.Items.Add(dr["MUSTERID"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["Musteri"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["adisyonID"].ToString());
                    sayac++;
                }

            }
            catch(SqlException ex)
            {
                string hata = ex.Message;
            }
            finally
            { dr.Close(); con.Dispose(); con.Close(); }
        }
        public int MüsterininSonAdisyonIDsi(int müsteriID)
        {
            int sonuc = 0;
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Select adisyonlar.ID from adisyonlar Inner Join paketSiparis on paketSiparis.ADISYONID=adisyonlar.ID where paketSiparis.DURUM=0 and adisyonlar.DURUM=0 and MUSTERID=@müsteriId", con);
            try
            {
                if(con.State==ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.Parameters.Add("@müsteriId", SqlDbType.Int).Value = müsteriID;
                sonuc =Convert.ToInt32( cmd.ExecuteScalar());
            }
            catch(SqlException ex)
            {
                string hata = ex.Message;
            }
            finally
            { con.Dispose(); con.Close(); }
            return sonuc;
        }
        public void MusteriDetay(ListView lv,int müsteriId)
        {
            lv.Items.Clear();
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Select paketSiparis.MUSTERID,paketSiparis.ADISYONID,musteriler.AD,musteriler.SOYAD,CONVERT(varchar(10),adisyonlar.TARIH,104 ) as tarih from adisyonlar Inner Join paketSiparis on paketSiparis.ADISYONID=adisyonlar.ID Inner Join musteriler on musteriler.ID=paketSiparis.MUSTERID where adisyonlar.SERVISTURNO=2 and paketSiparis.MUSTERID= @müsteriId", con);
            cmd.Parameters.Add("@müsteriId", SqlDbType.Int).Value = müsteriId;
            SqlDataReader dr = null;
            try
            {
                if(con.State==ConnectionState.Closed)
                {
                    con.Open();
                }
                dr = cmd.ExecuteReader();
                int sayac = 0;
                while(dr.Read())
                {
                    lv.Items.Add(dr["MUSTERID"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["AD"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["SOYAD"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["tarih"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["ADISYONID"].ToString());
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
