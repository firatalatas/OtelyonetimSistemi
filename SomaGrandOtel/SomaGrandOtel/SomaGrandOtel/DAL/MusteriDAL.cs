using System;
using System.Data;
using MySql.Data.MySqlClient;
using SomaGrandOtel.Entity;

namespace SomaGrandOtel.DAL
{
    public class MusteriDAL
    {
        private dbBaglanti db = new dbBaglanti();

        public DataTable MusteriGetir()
        {
            using (MySqlConnection conn = db.BaglantiGetir())
            {
                conn.Open();
                string query = "SELECT * FROM tbl_Musteri";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public bool MusteriEkle(Musteri musteri)
        {
            using (MySqlConnection conn = db.BaglantiGetir())
            {
                conn.Open();
                string query = "INSERT INTO tbl_Musteri (musteri_Ad, musteri_Soyad, musteri_Tel, musteri_Eposta) VALUES (@ad, @soyad, @tel, @eposta)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ad", musteri.MusteriAd);
                cmd.Parameters.AddWithValue("@soyad", musteri.MusteriSoyad);
                cmd.Parameters.AddWithValue("@tel", musteri.MusteriTel);
                cmd.Parameters.AddWithValue("@eposta", musteri.MusteriEposta);
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }

        public bool MusteriGuncelle(Musteri musteri)
        {
            using (MySqlConnection conn = db.BaglantiGetir())
            {
                conn.Open();
                string query = "UPDATE tbl_Musteri SET musteri_Ad = @ad, musteri_Soyad = @soyad, musteri_Tel = @tel, musteri_Eposta = @eposta WHERE MusteriID = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", musteri.MusteriID);
                cmd.Parameters.AddWithValue("@ad", musteri.MusteriAd);
                cmd.Parameters.AddWithValue("@soyad", musteri.MusteriSoyad);
                cmd.Parameters.AddWithValue("@tel", musteri.MusteriTel);
                cmd.Parameters.AddWithValue("@eposta", musteri.MusteriEposta);
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }

        public bool MusteriSil(int id)
        {
            using (MySqlConnection conn = db.BaglantiGetir())
            {
                conn.Open();
                string query = "DELETE FROM tbl_Musteri WHERE MusteriID = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }
    }
}