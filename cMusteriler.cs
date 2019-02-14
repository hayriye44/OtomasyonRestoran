using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace _14542513HayriyeCingöz_restoran_
{
    class cMusteriler
    {
        cGenel gnl = new cGenel();
        private int _musteriId;

        public int MusteriId
        {
            get { return _musteriId; }
            set { _musteriId = value; }
        }
        private string _musateriad;

        public string Musateriad
        {
            get { return _musateriad; }
            set { _musateriad = value; }
        }
        private string _musterisoyad;

        public string Musterisoyad
        {
            get { return _musterisoyad; }
            set { _musterisoyad = value; }
        }
        private string _telefon;

        public string Telefon
        {
            get { return _telefon; }
            set { _telefon = value; }
        }
        private string _adres;

        public string Adres
        {
            get { return _adres; }
            set { _adres = value; }
        }
        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        public bool MüsteriVarmı(string telefon)
        {
            bool sonuc = false;
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Select ID from musteriler where TELEFON=@telefon", con);
            try
            {
                if (con.State == ConnectionState.Closed)
                { con.Open(); }
                cmd.Parameters.Add("@telefon", SqlDbType.VarChar).Value = telefon;
                sonuc = Convert.ToBoolean(cmd.ExecuteScalar());
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                throw;
            }
            finally
            { con.Dispose(); con.Close(); } return sonuc;
        }
        public int MusteriEkle(cMusteriler m)
        {
            int sonuc = 0;
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Insert Into musteriler(AD,SOYAD,TELEFON,ADRES,EMAİL)values (@AD,@SOYAD,@TELEFON,@ADRES,@EMAİL)", con);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.Parameters.Add("@AD", SqlDbType.VarChar).Value = m._musateriad;
                cmd.Parameters.Add("@SOYAD", SqlDbType.VarChar).Value = m._musterisoyad;
                cmd.Parameters.Add("@TELEFON", SqlDbType.VarChar).Value = m._telefon;
                cmd.Parameters.Add("@ADRES", SqlDbType.VarChar).Value = m._adres;
                cmd.Parameters.Add("@EMAİL", SqlDbType.VarChar).Value = m._email;
                sonuc = Convert.ToInt32(cmd.ExecuteNonQuery());
            }
            catch (SqlException ex)
            { string hata = ex.Message; }
            finally { con.Dispose(); con.Close(); }
            return sonuc;

        }
        public bool MusteriBilgileriGüncelle(cMusteriler m)
        {
            bool sonuc = false;
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Update musteriler set AD=@AD,SOYAD=@SOYAD,TELEFON=@TELEFON,ADRES=@ADRES,EMAİL=@EMAİL where ID=@musteriID", con);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.Parameters.Add("@AD", SqlDbType.VarChar).Value = m._musateriad;
                cmd.Parameters.Add("@SOYAD", SqlDbType.VarChar).Value = m._musterisoyad;
                cmd.Parameters.Add("@TELEFON", SqlDbType.VarChar).Value = m._telefon;
                cmd.Parameters.Add("@ADRES", SqlDbType.VarChar).Value = m._adres;
                cmd.Parameters.Add("@EMAİL", SqlDbType.VarChar).Value = m._email;
                cmd.Parameters.Add("@musteriID", SqlDbType.VarChar).Value = m._musteriId;
                sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
            }
            catch (SqlException ex)
            { string hata = ex.Message; }
            finally { con.Dispose(); con.Close(); }
            return sonuc;

        }
        public void MüsterileriGetir(ListView lv )
        {
            lv.Items.Clear();
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Select * from musteriler", con);
            SqlDataReader dr = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                { con.Open(); }
                dr = cmd.ExecuteReader();
                int sayac = 0;
                while(dr.Read())
                {
                    lv.Items.Add(dr["ID"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["AD"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["SOYAD"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["TELEFON"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["ADRES"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["EMAİL"].ToString());
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
                con.Dispose(); con.Close(); }
        }
        public void MusteriyiIdyeGöreGetir(int musteriId,TextBox ad,TextBox soyad,TextBox telefon,TextBox adres,TextBox email)
        {
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Select * from musteriler where ID=@ıd", con);
            SqlDataReader dr = null;
            cmd.Parameters.Add("@ıd",SqlDbType.Int).Value=musteriId;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                dr=cmd.ExecuteReader();
                while(dr.Read())
                {
                    ad.Text = dr["AD"].ToString();
                    soyad.Text = dr["SOYAD"].ToString();
                    telefon.Text = dr["TELEFON"].ToString();
                    adres.Text = dr["ADRES"].ToString();
                    email.Text = dr["EMAİL"].ToString();
                }
            }
                catch(SqlException ex)
            {
                string hata = ex.Message;
                throw;
           }
            dr.Close();
            con.Dispose(); con.Close();


        }
        public void MüsteriGetirAdaGöre(ListView lv,string ad)
        {
            lv.Items.Clear();
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Select * from musteriler where AD like @ad + '%' ", con);
            SqlDataReader dr = null;
            cmd.Parameters.Add("@ad", SqlDbType.VarChar).Value = ad;
            try
            {
                if(con.State==ConnectionState.Closed)
                { con.Open(); }
                dr = cmd.ExecuteReader();
                int sayac = 0;
                while(dr.Read())
                {

                    lv.Items.Add(dr["ID"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["AD"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["SOYAD"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["TELEFON"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["ADRES"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["EMAİL"].ToString());
                    sayac++;
                }
            }
            catch(SqlException ex)
            { string hata = ex.Message; }
            dr.Close();
            con.Dispose();
            con.Close();

        }
        public void MüsteriGetirSoyAdaGöre(ListView lv, string soyad)
        {
            lv.Items.Clear();
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Select * from musteriler where SOYAD like @soyad + '%' ", con);
            SqlDataReader dr = null;
            cmd.Parameters.Add("@soyad", SqlDbType.VarChar).Value = soyad;
            try
            {
                if (con.State == ConnectionState.Closed)
                { con.Open(); }
                dr = cmd.ExecuteReader();
                int sayac = 0;
                while (dr.Read())
                {

                    lv.Items.Add(dr["ID"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["AD"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["SOYAD"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["TELEFON"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["ADRES"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["EMAİL"].ToString());
                    sayac++;
                }
            }
            catch (SqlException ex)
            { string hata = ex.Message; }
            dr.Close();
            con.Dispose();
            con.Close();

        }
        public void MüsteriGetirTelefonaGöre(ListView lv, string telefon)
        {
            lv.Items.Clear();
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Select * from musteriler where TELEFON like @telefon + '%' ", con);
            SqlDataReader dr = null;
            cmd.Parameters.Add("@telefon", SqlDbType.VarChar).Value = telefon;
            try
            {
                if (con.State == ConnectionState.Closed)
                { con.Open(); }
                dr = cmd.ExecuteReader();
                int sayac = 0;
                while (dr.Read())
                {

                    lv.Items.Add(dr["ID"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["AD"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["SOYAD"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["TELEFON"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["ADRES"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["EMAİL"].ToString());
                    sayac++;
                }
            }
            catch (SqlException ex)
            { string hata = ex.Message; }
            dr.Close();
            con.Dispose();
            con.Close();

        }
    }
}

