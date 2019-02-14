using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
namespace _14542513HayriyeCingöz_restoran_
{
    class cPersoneller
    {
        cGenel gnl = new cGenel();
        private int PersonelId;
        private int GorevId;
        private string PersonelAd;
        private string PersonelSoyad;
        private string PersonelParola;
        private string PersonelKullanıcıAdı;
        private bool PersonelDurum;
public int PersonelId1
{
  get { return PersonelId; }
  set { PersonelId = value; }
}
public int GorevId1
{
  get { return GorevId; }
  set { GorevId = value; }
}
public string PersonelAd1
{
  get { return PersonelAd; }
  set { PersonelAd = value; }
}       
public string PersonelSoyad1
{
  get { return PersonelSoyad; }
  set { PersonelSoyad = value; }
}
public string PersonelParola1
{
  get { return PersonelParola; }
  set { PersonelParola = value; }
}
public string PersonelKullanıcıAdı1
{
    get { return PersonelKullanıcıAdı; }
    set { PersonelKullanıcıAdı = value; }
}
public bool PersonelDurum1
{
    get { return PersonelDurum; }
    set { PersonelDurum = value; }
}
        public bool personelGirisKontrol(string pasword,int userId)
        {
            bool sonuc = false;
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Select * from Personeller where ID=@Id and PAROLA=@pasword",con);
            cmd.Parameters.Add("@Id", SqlDbType.VarChar).Value = userId;
            cmd.Parameters.Add("@pasword", SqlDbType.VarChar).Value = pasword;
            try
            {
                if(con.State==ConnectionState.Closed)
                {
                    con.Open();
                }
                sonuc = Convert.ToBoolean(cmd.ExecuteScalar());
            }
            catch(SqlException ex)
            {
                string hata = ex.Message;
                throw;
            }
            return sonuc;
        }
        public void PersonelleriGetir(ComboBox cb)
        {
            cb.Items.Clear();
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Select * from Personeller", con);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlDataReader dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                cPersoneller p = new cPersoneller();
                p.PersonelAd = Convert.ToString(dr["AD"]);
                p.PersonelSoyad = Convert.ToString(dr["SOYAD"]);
                p.PersonelParola = Convert.ToString(dr["PAROLA"]);
                p.PersonelKullanıcıAdı= Convert.ToString(dr["KULLANICIADI"]);
                p.PersonelDurum = Convert.ToBoolean(dr["DURUM"]);
                p.PersonelId = Convert.ToInt32(dr["ID"]);
                p.GorevId= Convert.ToInt32(dr["GOREVID"]);
                cb.Items.Add(p);
            }
            dr.Close();
            con.Close();
        }
        public override string ToString()
        {
            return PersonelAd +" "+ PersonelSoyad;
        }
        public void PersonelBilgileriGetirFormIdLv(ListView lv)
        {
            lv.Items.Clear();
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Select personeller.*,personelGorevleri.GOREV from Personeller Inner Join PersonelGorevleri on personelGorevleri.ID=personeller.GOREVID where personeller.DURUM=0", con);
          
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlDataReader dr = cmd.ExecuteReader();
            int i = 0;
            while (dr.Read())
            {
                lv.Items.Add(dr["ID"].ToString());
                lv.Items[i].SubItems.Add(dr["GOREVID"].ToString());
                lv.Items[i].SubItems.Add(dr["GOREV"].ToString());
                lv.Items[i].SubItems.Add(dr["AD"].ToString());
                lv.Items[i].SubItems.Add(dr["SOYAD"].ToString());
                i++;
            }
            dr.Close();
            con.Close();
        }
        public string PersonelBilgisiGetirİsim(int perId)
        {
            string sonuc = "";
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Select AD from personeller where personeller.DURUM=0 and personeller.ID=@perID", con);
            cmd.Parameters.Add("@perID", SqlDbType.Int).Value = perId;

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                sonuc = Convert.ToString(cmd.ExecuteScalar());
            }
            catch (SqlException ex)
            { string hata = ex.Message; }
            con.Close();
            return sonuc;
        }
        public bool SifreDegis(int perID, string parola)
        {
            bool sonuc = false;
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Update personeller set PAROLA=@parola where ID=@perID", con);
            cmd.Parameters.Add("@parola", SqlDbType.VarChar).Value = parola;
            cmd.Parameters.Add("@perID", SqlDbType.Int).Value = perID;
            try
            {
                if (con.State == ConnectionState.Closed)
                { con.Open(); }
                sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                throw;
            } con.Dispose(); con.Close();
            return sonuc;
        }
        public bool PersonelSil(int perId)
        {
            bool sonuc=false;
            SqlConnection con=new SqlConnection(gnl.conString);
            SqlCommand cmd=new SqlCommand("Update personeller set DURUM=1 where ID=@perId",con);
              cmd.Parameters.Add("@perId",SqlDbType.Int).Value=perId;
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
        public bool PersonelEkle(cPersoneller cp)
        {
            bool sonuc=false;
            SqlConnection con=new SqlConnection(gnl.conString);
            SqlCommand cmd=new SqlCommand("Insert into personeller(AD,SOYAD,PAROLA,GOREVID) values (@AD,@SOYAD,@PAROLA,@GOREVID)",con);
            cmd.Parameters.Add("@AD",SqlDbType.VarChar).Value=cp.PersonelAd1;
            cmd.Parameters.Add("@SOYAD",SqlDbType.VarChar).Value=cp.PersonelSoyad1;
            cmd.Parameters.Add("@PAROLA",SqlDbType.VarChar).Value=cp.PersonelParola1;
            cmd.Parameters.Add("@GOREVID",SqlDbType.Int).Value=cp.GorevId1;
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
          public bool PersonelGuncelle(cPersoneller cp,int perId)
        {
            bool sonuc=false;
            SqlConnection con=new SqlConnection(gnl.conString);
            SqlCommand cmd=new SqlCommand("Update personeller set AD=@AD ,SOYAD=@SOYAD,PAROLA=@PAROLA,GOREVID=@GOREVID where ID=@perId",con);
              cmd.Parameters.Add("@perId",SqlDbType.Int).Value=perId;
            cmd.Parameters.Add("@AD",SqlDbType.VarChar).Value=cp.PersonelAd1;
            cmd.Parameters.Add("@SOYAD",SqlDbType.VarChar).Value=cp.PersonelSoyad1;
            cmd.Parameters.Add("@PAROLA",SqlDbType.VarChar).Value=cp.PersonelParola1;
            cmd.Parameters.Add("@GOREVID",SqlDbType.Int).Value=cp.GorevId1;
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
