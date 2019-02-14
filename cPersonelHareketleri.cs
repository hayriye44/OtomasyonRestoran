using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Data;
namespace _14542513HayriyeCingöz_restoran_
{
    class cPersonelHareketleri
    { 
        cGenel gnl = new cGenel();
        private int ID;
        private int PersonelId;
        private string Islem;
        private DateTime Tarih; 
        private bool Durum;
        public int ID1
        {
            get { return ID; }
            set { ID = value; }
        }
        public int PersonelId1
        {
            get { return PersonelId; }
            set { PersonelId = value; }
        }
        public string Islem1
        {
            get { return Islem; }
            set { Islem = value; }
        }
        public DateTime Tarih1
        {
            get { return Tarih; }
            set { Tarih = value; }
        }
        public bool Durum1
        {
            get { return Durum; }
            set { Durum = value; }
        }
        public bool PersonelHareketKayıt(cPersonelHareketleri ph)
        {bool sonuc=false;
        SqlConnection con = new SqlConnection(gnl.conString);
        SqlCommand cmd = new SqlCommand("insert into personelHarektleri(PERSONELID, ISLEM, TARİH)Values(@personelId,@islem,@tarih)",con);
        
        try
        {
            if(con.State==ConnectionState.Closed)
            {
                con.Open();
            }
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@personelId", SqlDbType.Int).Value = ph.PersonelId;
            cmd.Parameters.Add("@islem", SqlDbType.VarChar).Value = ph.Islem;
            cmd.Parameters.Add("@tarih", SqlDbType.DateTime).Value =ph.Tarih;
            sonuc =Convert.ToBoolean( cmd.ExecuteNonQuery());
        }
            catch (SqlException ex)
        {
            string hata = ex.Message;
            throw;
        }
            return sonuc;
        }
    }
}
