using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace _14542513HayriyeCingöz_restoran_
{
    class cUrunCesitleri
    {
        cGenel gnl=new cGenel();
        private int _UrunTurNo;

        public int UrunTurNo
{
  get { return _UrunTurNo; }
  set { _UrunTurNo = value; }
}
        private string _KategoriAd;
        public string KategoriAd
{
  get { return _KategoriAd; }
  set { _KategoriAd = value; }
}
        private string _Aciklama;
        public string Aciklama
{
  get { return _Aciklama; }
  set { _Aciklama = value; }
}
        public void UrunCesitleriGetir(ListView Cesitler,Button btn)
        {
            Cesitler.Items.Clear();
            SqlConnection con=new SqlConnection(gnl.conString);
            SqlCommand cmd=new SqlCommand("Select URUNAD,FIYAT,urunler.ID From kategoriler Inner Join urunler on kategoriler.ID=urunler.KATEGORID where urunler.KATEGORID=@KATEGORID",con);
            string a=btn.Name;
            int uzunluk=a.Length;
            cmd.Parameters.Add("@KATEGORID",SqlDbType.Int).Value=a.Substring(uzunluk-1,1);
            if(con.State==ConnectionState.Closed)
            {
                con.Open();
            }
            SqlDataReader dr=cmd.ExecuteReader();
            int i=0;
            while(dr.Read())
            {
                Cesitler.Items.Add(dr["URUNAD"].ToString());
                Cesitler.Items[i].SubItems.Add(dr["FIYAT"].ToString());
                Cesitler.Items[i].SubItems.Add(dr["ID"].ToString());
                i++;
            }
            dr.Close();
            con.Dispose();
            con.Close();

        }
        public void UrunAra(ListView Cesitler,int txt)
        {
            Cesitler.Items.Clear();
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Select * from urunler where ID=@ID", con);
            
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = txt;
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlDataReader dr = cmd.ExecuteReader();
            int i = 0;
            while (dr.Read())
            {
                Cesitler.Items.Add(dr["URUNAD"].ToString());
                Cesitler.Items[i].SubItems.Add(dr["FIYAT"].ToString());
                Cesitler.Items[i].SubItems.Add(dr["ID"].ToString());
                i++;
            }
            dr.Close();
            con.Dispose();
            con.Close();

        }
        public void UrunCesitleriGetir(ComboBox cb)
        {
            cb.Items.Clear();
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Select * from kategoriler ", con);
            SqlDataReader dr = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                { con.Open(); }
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cUrunCesitleri uc = new cUrunCesitleri();
                    uc._UrunTurNo = Convert.ToInt32(dr["ID"]);
                    uc._KategoriAd = dr["KATEGORIADI"].ToString();
                    uc._Aciklama = dr["ACIKLAMA"].ToString();
                    cb.Items.Add(uc);
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
        public void UrunCesitleriGetir(ListView lv)
        {
            lv.Items.Clear();
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Select * from kategoriler where DURUM=0 ", con);
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
                    lv.Items[sayac].SubItems.Add(dr["KATEGORIADI"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["ACIKLAMA"].ToString());
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
        public void UrunCesitleriGetirArama(ListView lv,string source)
        {
            lv.Items.Clear();
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Select * from kategoriler where DURUM=0 and KATEGORIADI like '%'+@ara+ '%' ", con);
            SqlDataReader dr = null;
            cmd.Parameters.Add("@ara", SqlDbType.VarChar).Value = source;
            try
            {
                if (con.State == ConnectionState.Closed)
                { con.Open(); }
                dr = cmd.ExecuteReader();
                int sayac = 0;
                while (dr.Read())
                {
                    lv.Items.Add(dr["ID"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["KATEGORIADI"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["ACIKLAMA"].ToString());
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
        public bool UrunKategoriEkle(cUrunCesitleri c)
        {
            bool sonuc = false;
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Insert Into kategoriler(KATEGORIADI,ACIKLAMA)values (@KATEGORIAD,@ACIKLAMA)", con);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.Parameters.Add("@KATEGORIAD", SqlDbType.VarChar).Value = c._KategoriAd;
                cmd.Parameters.Add("@ACIKLAMA", SqlDbType.VarChar).Value = c._Aciklama;
                sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
            }
            catch (SqlException ex)
            { string hata = ex.Message; }
            finally { con.Dispose(); con.Close(); }
            return sonuc;
        }
        public bool UrunKategoriGuncelle(cUrunCesitleri u)
        {
            bool sonuc = false;
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Update kategoriler set KATEGORIADI=@KATEGORIADI,ACIKLAMA=@ACIKLAMA where ID=@urunKatID", con);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.Parameters.Add("@KATEGORIADI", SqlDbType.VarChar).Value = u._KategoriAd;
                cmd.Parameters.Add("@ACIKLAMA", SqlDbType.VarChar).Value = u._Aciklama;
                cmd.Parameters.Add("@urunKatID", SqlDbType.Int).Value = u._UrunTurNo;
                sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
            }
            catch (SqlException ex)
            { string hata = ex.Message;
            throw;
            }
            finally { con.Dispose(); con.Close(); }
            return sonuc;

        }
        public bool UrunKategoriSil(int ID)
        {
            bool sonuc = false;
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand(" Update kategoriler set DURUM=1 where ID=@urunKatID ", con);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.Parameters.Add("@urunKatID", SqlDbType.Int).Value = ID;
                sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
            }
            catch (SqlException ex)
            { string hata = ex.Message; }
            finally { con.Dispose(); con.Close(); }
            return sonuc;

        }
        public override string ToString()
        {
            return KategoriAd;
        }

    }
}
