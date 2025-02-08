using System;
using System.Data;
using MySql.Data.MySqlClient;
using SomaGrandOtel.Entity;

namespace SomaGrandOtel.DAL
{
    internal class OdaDAL
    {
        private dbBaglanti db = new dbBaglanti();

        
        public bool OdaEkle(Oda oda)
        {
            using (MySqlConnection conn = db.BaglantiGetir())
            {
                conn.Open();
                string query = "INSERT INTO tbl_Oda (oda_tipi, oda_numara, oda_fiyat, oda_durum) VALUES (@tip, @numara, @fiyat, @durum)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@tip", oda.OdaTipi);
                cmd.Parameters.AddWithValue("@numara", oda.OdaNumara);
                cmd.Parameters.AddWithValue("@fiyat", oda.OdaFiyat);
                cmd.Parameters.AddWithValue("@durum", oda.OdaDurum);

                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }

        
        public bool OdaGüncelle(Oda oda)
        {
            using (MySqlConnection conn = db.BaglantiGetir())
            {
                conn.Open();
                string query = "UPDATE tbl_Oda SET oda_tipi = @tip, oda_numara = @numara, oda_fiyat = @fiyat, oda_durum = @durum WHERE OdaID = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", oda.OdaID);
                cmd.Parameters.AddWithValue("@tip", oda.OdaTipi);
                cmd.Parameters.AddWithValue("@numara", oda.OdaNumara);
                cmd.Parameters.AddWithValue("@fiyat", oda.OdaFiyat);
                cmd.Parameters.AddWithValue("@durum", oda.OdaDurum);

                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }

        
        public bool OdaSil(int odaID)
        {
            using (MySqlConnection conn = db.BaglantiGetir())
            {
                conn.Open();
                string query = "DELETE FROM tbl_Oda WHERE OdaID = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", odaID);

                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }

        
        public DataTable OdalariListele()
        {
            using (MySqlConnection conn = db.BaglantiGetir())
            {
                conn.Open();
                string query = "SELECT OdaID, oda_tipi, oda_numara, oda_fiyat, oda_durum FROM tbl_Oda"; // Kolonları açıkça belirt
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }
    }
}