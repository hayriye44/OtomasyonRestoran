using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace _14542513HayriyeCingöz_restoran_
{
    class cMasalar
    {
        private int _ID;
        private int _KAPASITE;
        private int _SERVISTURU; 
        private int _DURUM;
        private int _ONAY;
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public int KAPASITE
        {
            get { return _KAPASITE; }
            set { _KAPASITE = value; }
        }
        public int SERVISTURU
        {
            get { return _SERVISTURU; }
            set { _SERVISTURU = value; }
        }
        public int DURUM
        {
            get { return _DURUM; }
            set { _DURUM = value; }
        }
        public int ONAY
        {
            get { return _ONAY; }
            set { _ONAY = value; }
        }
        cGenel gnl = new cGenel();
        public string SessionSum(int state,string masaId)
        {
            string dt = "";
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Select TARIH,MASAID From adisyonlar Right Join masalar on adisyonlar.MASAID=masalar.ID Where masalar.DURUM=@durum and adisyonlar.Durum=0 and masalar.ID=@masaId", con);
            SqlDataReader dr = null;
            cmd.Parameters.Add("@durum", SqlDbType.Int).Value = state;
            cmd.Parameters.Add("@masaId", SqlDbType.Int).Value = masaId;
             try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                dr = cmd.ExecuteReader();
                 while(dr.Read())
                 {
                     dt = Convert.ToDateTime(dr["TARIH"]).ToString();
                 }
            }
            catch(SqlException ex)
             {
                 string hata = ex.Message;
                 throw;
             }
            finally
             {
                 dr.Close();
                 con.Dispose();
                 con.Close();
             }
             return dt;

        }
     public int MasaNosu(string TableValue)
        {
            string a = TableValue;
            int lenght = a.Length;
            if (lenght > 8) { return Convert.ToInt32(a.Substring(lenght - 2, 2)); }
            else
            { return Convert.ToInt32(a.Substring(lenght - 1, 1)); }
        }
     public bool MasaDurumu(int ButtonName, int state)
     {
         bool sonuc = false;
         SqlConnection con = new SqlConnection(gnl.conString);
         SqlCommand cmd = new SqlCommand("Select durum From Masalar Where ID=@ButtonName and DURUM=@state", con);
         cmd.Parameters.Add("@ButtonName", SqlDbType.Int).Value = ButtonName;
         cmd.Parameters.Add("@state", SqlDbType.Int).Value = state;
         try
         {
             if (con.State == ConnectionState.Closed)
             { con.Open(); }
             sonuc = Convert.ToBoolean(cmd.ExecuteScalar());

         }
         catch (SqlException ex)
         { string hata = ex.Message; }
         finally
         {
             con.Dispose();
             con.Close();
         }
         return sonuc;
     }
        public void MasaDurumuDegistir(string ButtonName,int state)
        {
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("UPDATE masalar SET DURUM=@Durum where ID=@MasaNo", con);
            if(con.State==ConnectionState.Closed)
            { con.Open(); }
            string a = ButtonName;
            int uzunluk = a.Length;
            cmd.Parameters.Add("@Durum", SqlDbType.Int).Value = state;
            if(uzunluk>8)
            {
                cmd.Parameters.Add("@MasaNo", SqlDbType.Int).Value = a.Substring(uzunluk - 2, 2);
            }
            else
            {
                cmd.Parameters.Add("@MasaNo", SqlDbType.Int).Value = a.Substring(uzunluk - 1, 1);
            }
           
            cmd.ExecuteNonQuery();
            con.Dispose(); con.Close();
        }
    }
}
