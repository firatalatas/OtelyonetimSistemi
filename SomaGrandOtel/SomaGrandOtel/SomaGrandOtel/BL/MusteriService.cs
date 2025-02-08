using System.Data;
using SomaGrandOtel.Entity;

namespace SomaGrandOtel.BL
{
    internal class MusteriService
    {
        private DAL.MusteriDAL musteriDAL = new DAL.MusteriDAL();

        public DataTable MüşterileriGetir()
        {
            return musteriDAL.MusteriGetir();
        }

        public bool MüşteriEkle(Musteri musteri)
        {
            return musteriDAL.MusteriEkle(musteri);
        }

        public bool MüşteriGüncelle(Musteri musteri)
        {
            return musteriDAL.MusteriGuncelle(musteri);
        }

        public bool MüşteriSil(int id)
        {
            return musteriDAL.MusteriSil(id);
        }
    }
}
