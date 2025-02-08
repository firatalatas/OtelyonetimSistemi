using System;

namespace SomaGrandOtel.Entity
{
    public class Rezervasyon
    {
        public int RezervasyonID { get; set; } 
        public int MusteriID { get; set; } 
        public int OdaID { get; set; } 
        public DateTime RzvGirisTarihi { get; set; }
        public DateTime RzvCikisTarihi { get; set; }
        public decimal RzvToplamTutar { get; set; }
    }
}