using System.Data;
using System;
using MySql.Data.MySqlClient;
using SomaGrandOtel.Entity;
using SomaGrandOtel.BL;
using SomaGrandOtel.DAL;

namespace SomaGrandOtel.DAL
{
    internal class RezervasyonDAL
    {
        private dbBaglanti db = new dbBaglanti();
        private IslemDAL islemDAL = new IslemDAL();
        private IslemService islemService = new IslemService();

        public DataTable RezervasyonGetir()
        {
            using (MySqlConnection conn = db.BaglantiGetir())
            {
                conn.Open();
                string query = @"
                    SELECT 
                        RezervasyonID, 
                        MusteriID, 
                        OdaID, 
                        rzv_girisTarihi AS RzvGirisTarihi, 
                        rzv_cikisTarihi AS RzvCikisTarihi, 
                        rzv_toplamTutar AS RzvToplamTutar 
                    FROM tbl_Rezervasyon";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public bool RezervasyonEkle(Rezervasyon rezervasyon)
        {
            try
            {
                
                if (islemService.RezervasyonCakismaKontrol(rezervasyon.OdaID, rezervasyon.RzvGirisTarihi, rezervasyon.RzvCikisTarihi))
                {
                    throw new Exception("Bu oda zaten seçilen tarih aralığında rezerve edilmiş.");
                }

                
                decimal odaFiyati = islemDAL.OdaFiyatiGetir(rezervasyon.OdaID);

                
                int gunSayisi = islemService.GunSayisiHesapla(rezervasyon.RzvGirisTarihi, rezervasyon.RzvCikisTarihi);

                
                decimal toplamTutar = islemService.ToplamTutarHesapla(odaFiyati, gunSayisi);

                
                rezervasyon.RzvToplamTutar = toplamTutar;

                
                using (MySqlConnection conn = db.BaglantiGetir())
                {
                    conn.Open();
                    string query = @"
                INSERT INTO tbl_Rezervasyon 
                (MusteriID, OdaID, rzv_girisTarihi, rzv_cikisTarihi, rzv_toplamTutar) 
                VALUES (@musteriID, @odaID, @girisTarihi, @cikisTarihi, @toplamTutar)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@musteriID", rezervasyon.MusteriID);
                    cmd.Parameters.AddWithValue("@odaID", rezervasyon.OdaID);
                    cmd.Parameters.AddWithValue("@girisTarihi", rezervasyon.RzvGirisTarihi);
                    cmd.Parameters.AddWithValue("@cikisTarihi", rezervasyon.RzvCikisTarihi);
                    cmd.Parameters.AddWithValue("@toplamTutar", rezervasyon.RzvToplamTutar);

                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Rezervasyon eklenirken bir hata oluştu: {ex.Message}");
            }
        }

        public bool RezervasyonGuncelle(Rezervasyon rezervasyon)
        {
            using (MySqlConnection conn = db.BaglantiGetir())
            {
                conn.Open();
                string query = @"
                    UPDATE tbl_Rezervasyon 
                    SET 
                        MusteriID = @musteriID, 
                        OdaID = @odaID, 
                        rzv_girisTarihi = @girisTarihi, 
                        rzv_cikisTarihi = @cikisTarihi, 
                        rzv_toplamTutar = @toplamTutar 
                    WHERE RezervasyonID = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", rezervasyon.RezervasyonID);
                cmd.Parameters.AddWithValue("@musteriID", rezervasyon.MusteriID);
                cmd.Parameters.AddWithValue("@odaID", rezervasyon.OdaID);
                cmd.Parameters.AddWithValue("@girisTarihi", rezervasyon.RzvGirisTarihi);
                cmd.Parameters.AddWithValue("@cikisTarihi", rezervasyon.RzvCikisTarihi);
                cmd.Parameters.AddWithValue("@toplamTutar", rezervasyon.RzvToplamTutar);
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }

        public bool RezervasyonSil(int id)
        {
            using (MySqlConnection conn = db.BaglantiGetir())
            {
                conn.Open();
                string query = "DELETE FROM tbl_Rezervasyon WHERE RezervasyonID = @rezervasyonID";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@rezervasyonID", id);
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }
    }
}
