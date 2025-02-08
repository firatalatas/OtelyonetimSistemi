using System.Data;
using SomaGrandOtel.Entity;
using SomaGrandOtel.DAL;

namespace SomaGrandOtel.BL
{
    internal class RezervasyonService
    {
        private DAL.RezervasyonDAL rezervasyonDAL = new DAL.RezervasyonDAL();

        public DataTable RezervasyonGetir()
        {
            return rezervasyonDAL.RezervasyonGetir();
        }

        public bool RezervasyonEkle(Rezervasyon rezervasyon)
        {
            return rezervasyonDAL.RezervasyonEkle(rezervasyon);
        }

        public bool RezervasyonGüncelle(Rezervasyon rezervasyon)
        {
            return rezervasyonDAL.RezervasyonGuncelle(rezervasyon);
        }

        public bool RezervasyonSil(int id)
        {
            RezervasyonDAL rezervasyonDAL = new RezervasyonDAL();
            return rezervasyonDAL.RezervasyonSil(id);
        }

        public string FaturaOlustur(Rezervasyon rezervasyon)
        {
            
            string fatura = @"
                ===============================
                      SOMA GRAND OTEL
                ===============================
                Rezervasyon Detayları:
                -------------------------------
                Müşteri ID: {0}
                Oda ID: {1}
                Giriş Tarihi: {2:dd.MM.yyyy}
                Çıkış Tarihi: {3:dd.MM.yyyy}
                Toplam Tutar: {4:C2}
                -------------------------------
                Sizi tekrar bekleriz!
                ===============================
            ";

            
            return string.Format(
                fatura,
                rezervasyon.MusteriID,
                rezervasyon.OdaID,
                rezervasyon.RzvGirisTarihi,
                rezervasyon.RzvCikisTarihi,
                rezervasyon.RzvToplamTutar
            );
        }
    }
}
