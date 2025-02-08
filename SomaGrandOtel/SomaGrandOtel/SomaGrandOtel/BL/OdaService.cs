using System.Data;
using SomaGrandOtel.Entity;
using SomaGrandOtel.DAL;

namespace SomaGrandOtel.BL
{
    internal class OdaService
    {
        private OdaDAL odaDAL = new OdaDAL();

        
        public bool OdaEkle(Oda oda)
        {
            return odaDAL.OdaEkle(oda);
        }

        
        public bool OdaGüncelle(Oda oda)
        {
            return odaDAL.OdaGüncelle(oda);
        }

        
        public bool OdaSil(int odaID)
        {
            return odaDAL.OdaSil(odaID);
        }

        
        public DataTable OdalariGetir()
        {
            return odaDAL.OdalariListele();
        }
    }
}