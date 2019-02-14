using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace _14542513HayriyeCingöz_restoran_
{
    class cPaketler
    {
        private int _ID;
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        private int _AdisyonId;
        public int AdisyonId
        {
            get { return _AdisyonId; }
            set { _AdisyonId = value; }
        }
        private int _MüsteriId;
        public int MüsteriId
        {
            get { return _MüsteriId; }
            set { _MüsteriId = value; }
        }
        private string _Aciklama;
        public string Aciklama
        {
            get { return _Aciklama; }
            set { _Aciklama = value; }
        }
        private int _Durum;
        public int Durum
        {
            get { return _Durum; }
            set { _Durum = value; }
        }
        private int _OdemeTuru;
        public int OdemeTuru
        {
            get { return _OdemeTuru; }
            set { _OdemeTuru = value; }
        }
        cGenel gnl = new cGenel();
        //paket servisleri ekleyecek
        public bool PaketServisAc(cPaketler paket)
        {
            bool result = false;
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Insert Into paketSiparis(ADISYONID,MUSTERID,ODEMETURID,ACIKLAMA) values (@ADISYONID,@MUSTERID,@ODEMETURID,@ACIKLAMA)",con);
            try
            {
                if(con.State==ConnectionState.Closed)
                { con.Open(); }
                cmd.Parameters.Add("@ADISYONID", SqlDbType.Int).Value = paket._AdisyonId;
                cmd.Parameters.Add("@MUSTERID", SqlDbType.Int).Value = paket._MüsteriId;
                cmd.Parameters.Add("@ODEMETURID", SqlDbType.Int).Value = paket._OdemeTuru;
                cmd.Parameters.Add("@ACIKLAMA", SqlDbType.VarChar).Value = paket._Aciklama;
                result=Convert.ToBoolean(cmd.ExecuteNonQuery());
            }
            catch(SqlException ex)
            {
                string hata = ex.Message;
                throw;
            }
            finally
            { con.Dispose(); con.Close(); }
            return result;
        }
        public void PaketServisiKapat(int AdditionId)
        {
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Update paketSiparis set paketSiparis.durum=1 where paketSiparis.ADISYONID=@ADISYONID",con);
            try
            {
                if (con.State == ConnectionState.Closed)
                { con.Open(); }
                cmd.Parameters.Add("@ADISYONID", SqlDbType.Int).Value = AdditionId; ;
               Convert.ToBoolean(cmd.ExecuteNonQuery());
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                throw;
            }
            finally
            { con.Dispose(); con.Close(); }
        }
        //açılan adisyon ve paket sipariş ait ön girilen ödemetür ıd
        public int OdemeTurIdGetir(int adisyonId)
        {
            int odemeTurID = 0;
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Select paketSiparis.ODEMETURID from paketSiparis Inner Join adisyonlar on paketSiparis.ADISYONID=adisyonlar.ID where adisyonlar.ID=@AdisyonID",con);
            try
            {
                if (con.State == ConnectionState.Closed)
                { con.Open(); }
                cmd.Parameters.Add("@AdisyonID", SqlDbType.Int).Value = adisyonId;
                odemeTurID= Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                throw;
            }
            finally
            { con.Dispose(); con.Close(); } return odemeTurID;
        }
        //sipariş kontrol için müşteriye ait açık olan en son adisyonu getiriyo bir müşteriye ait 2 sipariş açık olamaz
        public int musteriSonAdisyonIdGetir(int musteriID)
        {
            int no = 0;
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Select adisyonlar.ID from adisyonlar Inner Join paketSiparis on paketSiparis.ADISYONID=adisyonlar.ID where(adisyonlar.Durum=0) and (paketSiparis.DURUM=0) and paketSiparis.MUSTERID=@musteriID", con);
            try
            {
                if (con.State == ConnectionState.Closed)
                { con.Open(); }
                cmd.Parameters.Add("@musteriID", SqlDbType.Int).Value = musteriID;
                no = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                throw;
            }
            finally
            { con.Dispose(); con.Close(); } 
            return no;
        }
        //müsteri arama ekranında adisyonbul butonu için adisyon açıkmı kapalımı kontrol
        public bool AcıkAdisyonVarmi(int adisyonId)
        {
            bool result = false;
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Select *from adisyonlar where (DURUM=0)and (ID=@adisyonID)",con);
            try
            {
                if (con.State == ConnectionState.Closed)
                { con.Open(); }
                cmd.Parameters.Add("@adisyonID", SqlDbType.Int).Value = adisyonId;
                
                result = Convert.ToBoolean(cmd.ExecuteScalar());
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                throw;
            }
            finally
            { con.Dispose(); con.Close(); }
            return result;
        }
    }
}
