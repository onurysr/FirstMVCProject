using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ilk_Mvc_Projesi.Models
{
    public class Urun
    {
        public string Ad { get; set; }
        public decimal Fiyat { get; set; }

    }

    public static class UrunManager
    {
        public static List<Urun> GetUrunLer()
        {
            var list = new List<Urun>();
            list.Add(new Urun()
            {
                Ad = "kitap",
                Fiyat = 15

            }) ;
            list.Add(new Urun()
            {
                Ad = "kalem",
                Fiyat = 5
            });
            list.Add(new Urun()
            {
                Ad = "Defter",
                Fiyat = 7
            });
            return list;
        }
    }
}
