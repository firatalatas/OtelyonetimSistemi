using System.Data;
using SomaGrandOtel.Entity;

namespace SomaGrandOtel.BL
{
    public class YoneticiService
    {
        private DAL.YoneticiDAL yoneticiDAL = new DAL.YoneticiDAL();

        public DataTable YöneticileriGetir()
        {
            return yoneticiDAL.YoneticiGetir();
        }

        public bool YöneticiEkle(Yonetici yonetici)
        {
            return yoneticiDAL.YoneticiEkle(yonetici);
        }

        public bool YöneticiGüncelle(Yonetici yonetici)
        {
            return yoneticiDAL.YoneticiGuncelle(yonetici);
        }

        public bool YöneticiSil(string tc)
        {
            return yoneticiDAL.YoneticiSil(tc);
        }
    }
}