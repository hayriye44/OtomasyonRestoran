using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace _14542513HayriyeCingöz_restoran_
{
    class cRezervasyon
    {
        cGenel gnl = new cGenel();
        private int _ID;
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        private int _MasaId;
        public int MasaId
        {
            get { return _MasaId; }
            set { _MasaId = value; }
        }
        private int _MusteriId;
        public int MusteriId
        {
            get { return _MusteriId; }
            set { _MusteriId = value; }
        }
        private DateTime _Data;
        public DateTime Data
        {
            get { return _Data; }
            set { _Data = value; }
        }
        private int _KisiSayisi;
        public int KisiSayisi
        {
            get { return _KisiSayisi; }
            set { _KisiSayisi = value; }
        }
        private int _AdisyonId;
        public int AdisyonId
        {
            get { return _AdisyonId; }
            set { _AdisyonId = value; }
        }
        
        public int MusteriIdGetirRezervasyondan(int masaId)
        {
            int musteriId= 0;
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Select top 1 MUSTERID from Rezervasyonlar where MASAID=@masaID order by MUSTERID Desc", con);

            try
            {
                if (con.State == ConnectionState.Closed)
                { con.Open(); }
                cmd.Parameters.Add("@masaID", SqlDbType.Int).Value = masaId;
                
                musteriId = Convert.ToInt32(cmd.ExecuteNonQuery());
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                throw;
            }
            finally
            {
                con.Dispose(); con.Close();
            }
            return musteriId;
        }
        //Rezervasyonlu masayı kapatmak hesap kapatırken
        public bool rezervasyonKapat(int adisyonId)
        {
            bool sonuc = false;
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Update rezervasyonlar set DURUM=0 where ADİSYONID=@adisyonId", con);

            try
            {
                if (con.State == ConnectionState.Closed)
                { con.Open(); }
                cmd.Parameters.Add("@adisyonId", SqlDbType.Int).Value = adisyonId;
                sonuc = Convert.ToBoolean(cmd.ExecuteScalar());
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                throw;
            }
            finally
            { con.Dispose(); con.Close(); }
            return sonuc;
        }

    }
}
