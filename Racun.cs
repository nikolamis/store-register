using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TvpProjekat2
{
    class Racun
    {
        private int id_racuna;
        private int cena;
        private DateTime datum;
        private DateTime vreme;


        public Racun()
        {
            
        }
        public Racun(int id_racuna, int cena, DateTime datum, DateTime vreme)
        {
            this.id_racuna = id_racuna;
            this.cena = cena;
            this.datum = datum;
            this.vreme = vreme;
        }

        public int Id_racuna { get => id_racuna; set => id_racuna = value; }
        public int Cena { get => cena; set => cena = value; }
        public DateTime Datum { get => datum; set => datum = value; }
        public DateTime Vreme { get => vreme; set => vreme = value; }

        public override string ToString()
            
        {
            
            return "Cena: " + cena + "din, datum:" + (datum.ToShortDateString()).ToString() + " " 
                + (vreme.ToShortTimeString()).ToString();
        }
    }
}
