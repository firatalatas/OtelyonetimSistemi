using System;
using SomaGrandOtel.DAL;
using SomaGrandOtel.Entity;

namespace SomaGrandOtel.BL
{
    internal class IslemService

    {
        private IslemDAL islemDAL = new IslemDAL();
        
        public int GunSayisiHesapla(DateTime girisTarihi, DateTime cikisTarihi)
        {
            TimeSpan fark = cikisTarihi - girisTarihi;
            return Math.Max(0, fark.Days); 
        }

        
        public decimal ToplamTutarHesapla(decimal odaFiyati, int gunSayisi)
        {
            return odaFiyati * gunSayisi;
        }

        public bool RezervasyonCakismaKontrol(int odaID, DateTime girisTarihi, DateTime cikisTarihi)
        {
            return islemDAL.RezervasyonCakismaKontrol(odaID, girisTarihi, cikisTarihi);
        }
    }
}