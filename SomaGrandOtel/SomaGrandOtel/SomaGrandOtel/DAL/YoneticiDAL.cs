using System;
using System.Data;
using MySql.Data.MySqlClient;
using SomaGrandOtel.Entity;

namespace SomaGrandOtel.DAL
{
    public class YoneticiDAL
    {
        private dbBaglanti db = new dbBaglanti();

        public DataTable YoneticiGetir()
        {
            using (MySqlConnection conn = db.BaglantiGetir())
            {
                conn.Open();
                string query = "SELECT * FROM tbl_Yonetici";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public bool YoneticiEkle(Yonetici yonetici)
        {
            using (MySqlConnection conn = db.BaglantiGetir())
            {
                conn.Open();
                string query = "INSERT INTO tbl_Yonetici (yonetici_TC, yonetici_Sifre) VALUES (@tc, @sifre)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@tc", yonetici.YoneticiTC);
                cmd.Parameters.AddWithValue("@sifre", yonetici.YoneticiSifre);
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }

        public bool YoneticiGuncelle(Yonetici yonetici)
        {
            using (MySqlConnection conn = db.BaglantiGetir())
            {
                conn.Open();
                string query = "UPDATE tbl_Yonetici SET yonetici_Sifre = @sifre WHERE yonetici_TC = @tc";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@tc", yonetici.YoneticiTC);
                cmd.Parameters.AddWithValue("@sifre", yonetici.YoneticiSifre);
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }

        public bool YoneticiSil(string tc)
        {
            using (MySqlConnection conn = db.BaglantiGetir())
            {
                conn.Open();
                string query = "DELETE FROM tbl_Yonetici WHERE yonetici_TC = @tc";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@tc", tc);
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }
    }
}