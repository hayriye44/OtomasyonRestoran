using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace _14542513HayriyeCingöz_restoran_
{
    class cOdeme
    {
        private int _OdemeID;
        public int OdemeID
        {
            get { return _OdemeID; }
            set { _OdemeID = value; }
        }
        private int _AdisyonID;
        public int AdisyonID
        {
            get { return _AdisyonID; }
            set { _AdisyonID = value; }
        }
        private int _OdemeTurId;
        public int OdemeTurId
        {
            get { return _OdemeTurId; }
            set { _OdemeTurId = value; }
        }
        private decimal _AraToplam;
        public decimal AraToplam
        {
            get { return _AraToplam; }
            set { _AraToplam = value; }
        }
        private decimal _Indirim;
        public decimal Indirim
        {
            get { return _Indirim; }
            set { _Indirim = value; }
        }
        private decimal _KdvTutari;
        public decimal KdvTutari
        {
            get { return _KdvTutari; }
            set { _KdvTutari = value; }
        }
        private decimal _GenelToplam;
        public decimal GenelToplam
        {
            get { return _GenelToplam; }
            set { _GenelToplam = value; }
        }
        private DateTime _Tarih;
        public DateTime Tarih
        {
            get { return _Tarih; }
            set { _Tarih = value; }
        }
        private int _MusteriId;
        public int MusteriId
        {
            get { return _MusteriId; }
            set { _MusteriId = value; }
        }
        cGenel gnl = new cGenel();
        public Boolean HesapKapat(cOdeme hesap)
        {
            bool sonuc = false;
                SqlConnection con = new SqlConnection(gnl.conString);
                SqlCommand cmd = new SqlCommand("Insert Into HesapOdemeleri(ADISYONID,ODEMETURID,MUSTERID,ARATOPLAM,KDVTUTARI,TOPLAMTUTAR,INDIRIM) values (@ADISYONID,@ODEMETURID,@MUSTERID,@ARATOPLAM,@KDVTUTARI,@TOPLAMTUTAR,@INDIRIM)", con);
                
            try
            {
                if(con.State==ConnectionState.Closed)
                { con.Open(); }
                cmd.Parameters.Add("@ADISYONID", SqlDbType.Int).Value = hesap._AdisyonID;
                cmd.Parameters.Add("@ODEMETURID", SqlDbType.Int).Value = hesap._OdemeTurId;
                cmd.Parameters.Add("@MUSTERID", SqlDbType.Int).Value = hesap._MusteriId;
                cmd.Parameters.Add("@ARATOPLAM", SqlDbType.Money).Value = hesap._AraToplam;
                cmd.Parameters.Add("@KDVTUTARI", SqlDbType.Money).Value = hesap._KdvTutari;
                cmd.Parameters.Add("@TOPLAMTUTAR", SqlDbType.Money).Value = hesap._GenelToplam;
                cmd.Parameters.Add("@INDIRIM", SqlDbType.Money).Value = hesap._Indirim;
                sonuc =Convert.ToBoolean( cmd.ExecuteNonQuery());
            }
            catch(SqlException ex)
            {
                string hata = ex.Message;
                throw;
            }
            finally
            {
                con.Dispose(); con.Close();
            }
            return sonuc;
        }
        //Müşterinin masa hesapını kapatma
        public decimal toplamHesapMusteiIdyegore(int musteriId)
        {
            decimal toplam = 0;
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Select sum(TOPLAMTUTAR) as toplam from hesapOdemeleri Where MUSTERID=@MüsteriID", con);
            
            try
            {
                if(con.State==ConnectionState.Closed)
                { con.Open(); }
            cmd.Parameters.Add("@MüsteriID", SqlDbType.Int).Value = musteriId;
                //müsterinin toplam borcu
            toplam = Convert.ToDecimal( cmd.ExecuteScalar());
            }
            catch(SqlException ex)
            {
                string hata = ex.Message;
                throw;
            }
            finally
            { con.Dispose(); con.Close(); }
            return toplam;
        }
        
    }
}
