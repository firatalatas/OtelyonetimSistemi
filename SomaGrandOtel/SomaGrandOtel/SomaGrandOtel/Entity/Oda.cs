using System;

namespace SomaGrandOtel.Entity
{
    public class Oda
    {
        public int OdaID { get; set; } 
        public string OdaTipi { get; set; }
        public int OdaNumara { get; set; }
        public decimal OdaFiyat { get; set; }
        public string OdaDurum { get; set; }
    }
}