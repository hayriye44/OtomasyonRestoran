using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace _14542513HayriyeCingöz_restoran_
{
    class cPersonelGorevler
    {
        cGenel gnl = new cGenel();
        private int _PersonelGorevID;

        public int PersonelGorevID
        {
            get { return _PersonelGorevID; }
            set { _PersonelGorevID = value; }
        }
        private string _Tanim;

        public string Tanim
        {
            get { return _Tanim; }
            set { _Tanim = value; }
        }
       
                public void PersonelGorevGetir(ComboBox cb)
        {
            cb.Items.Clear();
             SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Select * from personelGorevleri", con);
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
                    cPersonelGorevler nesne=new cPersonelGorevler();
                    nesne._PersonelGorevID=Convert.ToInt32(dr["ID"].ToString());
                    nesne._Tanim=dr["GOREV"].ToString();
                    cb.Items.Add(nesne);
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
          public string PersonelGorevTanim(int Personel)
        {string sonuc="";
             SqlConnection con = new SqlConnection(gnl.conString);
             SqlCommand cmd = new SqlCommand(" Select GOREV from personelGorevleri Inner Join personeller on personelGorevleri.ID=personeller.GOREVID where personeller.ID=@perId", con);
            cmd.Parameters.Add("@perId",SqlDbType.Int).Value=Personel;
            try
            {
                if(con.State==ConnectionState.Closed)
                {
                    con.Open();
                }
               sonuc=cmd.ExecuteScalar().ToString();
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
            }
            finally
            {  con.Dispose(); con.Close(); }
            return sonuc;
        }
        public override string ToString()
{
 	 return _Tanim;
}
       public void PersonelBilgileriGetirLv(ListView lv)
        {
            lv.Items.Clear();
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Select personeller.*,personelGorevleri.GOREV from Personeller Inner Join PersonelGorevleri on personelGorevleri.ID=personeller.GOREVID, where personeller.DURUM=0", con);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlDataReader dr = cmd.ExecuteReader();
           int i=0;
            while(dr.Read())
            {
                lv.Items.Add(dr["ID"].ToString());
                lv.Items[i].SubItems.Add(dr["GOREVID"].ToString());
                lv.Items[i].SubItems.Add(dr["GOREV"].ToString());
                lv.Items[i].SubItems.Add(dr["AD"].ToString());
                lv.Items[i].SubItems.Add(dr["SOYAD"].ToString());
                lv.Items[i].SubItems.Add(dr["KULLANICIADI"].ToString());
                i++;
            }
            dr.Close();
            con.Close();
        }
        public void PersonelBilgileriGetirFormIdLv(ListView lv,int perID)
        {
            lv.Items.Clear();
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Select personeller.*,personelGorevleri.GOREV from Personeller Inner Join PersonelGorevleri on personelGorevleri.ID=personeller.GOREVID, where personeller.DURUM=0 and personeller.ID=@perID", con);
                cmd.Parameters.Add("@perID",SqlDbType.Int).Value=perID;
            if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlDataReader dr = cmd.ExecuteReader();
           int i=0;
            while(dr.Read())
            {
                lv.Items.Add(dr["ID"].ToString());
                lv.Items[i].SubItems.Add(dr["GOREVID"].ToString());
                lv.Items[i].SubItems.Add(dr["GOREV"].ToString());
                lv.Items[i].SubItems.Add(dr["AD"].ToString());
                lv.Items[i].SubItems.Add(dr["SOYAD"].ToString());
                lv.Items[i].SubItems.Add(dr["KULLANICIADI"].ToString());
                i++;
            }
            dr.Close();
            con.Close();
        }
        public string birPresonelBilgisiGetirİsim(int perId)
        {
            string sonuc="";
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Select AD from personeller where personeller.DURUM=0 and personeller.ID=@perID", con);
                cmd.Parameters.Add("@perID",SqlDbType.Int).Value=perId;
           
             try
             {
                 if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                 sonuc=Convert.ToString( cmd.ExecuteScalar());
             }
            catch(SqlException ex)
             {string hata=ex.Message;}
            con.Close();
            return sonuc;
        }
        public bool SifreDegis(int perID,string parola)
        {
            bool sonuc=false;
            SqlConnection con=new SqlConnection(gnl.conString);
            SqlCommand cmd=new SqlCommand("Update personeller set PAROLA=@parola where ID=@perID",con);
            cmd.Parameters.Add("@parola",SqlDbType.VarChar).Value=parola;
            cmd.Parameters.Add("@perID",SqlDbType.Int).Value=perID;
            try
            {
                if(con.State==ConnectionState.Closed)
                {con.Open();}
                sonuc=Convert.ToBoolean(cmd.ExecuteNonQuery());
            }
            catch(SqlException ex)
            {
                string hata=ex.Message;
                throw;
            }con.Dispose();con.Close();
            return sonuc;
        }
       
       
        }
    }

