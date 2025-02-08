using MySql.Data.MySqlClient;
using System;
using System.Data.SqlClient;
namespace SomaGrandOtel.DAL
{
    public class dbBaglanti
    {
        private MySqlConnection baglanti;
        public MySqlConnection BaglantiGetir()
        {
            string connectionString = "Server=172.21.54.253;Database=25_132330006;User=25_132330006;Password=!nif.ogr06AL;";
            baglanti = new MySqlConnection(connectionString);
            return baglanti;
        }

        public void BaglantiKapat()
        {
            if (baglanti != null && baglanti.State == System.Data.ConnectionState.Open)
            {
                baglanti.Close();
            }
        }
    }
}