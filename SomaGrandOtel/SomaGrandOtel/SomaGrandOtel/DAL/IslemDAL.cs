using System.Data;
using MySql.Data.MySqlClient;
using System;

namespace SomaGrandOtel.DAL
{
    internal class IslemDAL
    {
        private dbBaglanti db = new dbBaglanti();

        
        public decimal OdaFiyatiGetir(int odaID)
        {
            using (MySqlConnection conn = db.BaglantiGetir())
            {
                conn.Open();
                string query = "SELECT oda_fiyat FROM tbl_Oda WHERE OdaID = @odaID";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@odaID", odaID);

                object result = cmd.ExecuteScalar();
                return result != null ? Convert.ToDecimal(result) : 0;
            }
        }

        public bool RezervasyonCakismaKontrol(int odaID, DateTime girisTarihi, DateTime cikisTarihi)
        {
            using (MySqlConnection conn = db.BaglantiGetir())
            {
                conn.Open();
                string query = @"
                    SELECT COUNT(*) 
                    FROM tbl_Rezervasyon 
                    WHERE OdaID = @odaID
                      AND (
                          (@girisTarihi BETWEEN rzv_girisTarihi AND rzv_cikisTarihi) OR
                          (@cikisTarihi BETWEEN rzv_girisTarihi AND rzv_cikisTarihi) OR
                          (@girisTarihi <= rzv_girisTarihi AND @cikisTarihi >= rzv_cikisTarihi)
                      )";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@odaID", odaID);
                cmd.Parameters.AddWithValue("@girisTarihi", girisTarihi);
                cmd.Parameters.AddWithValue("@cikisTarihi", cikisTarihi);

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0; 
            }
        }


    }
}