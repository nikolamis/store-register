using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TvpProjekat2
{
    class Artikal
    {
        private int id_artikla;
        private string naziv;
        private int cena;
        private int popust;
        private int id_grupa;

        public Artikal()
        {
        }

        public Artikal(int id_artikla, string naziv, int cena, int popust, int id_grupa)
        {
            this.id_artikla = id_artikla;
            this.naziv = naziv;
            this.cena = cena;
            this.popust = popust;
            this.id_grupa = id_grupa;
        }

        public int Id_artikla { get => id_artikla; set => id_artikla = value; }
        public string Naziv { get => naziv; set => naziv = value; }
        public int Cena { get => cena; set => cena = value; }
        public int Popust { get => popust; set => popust = value; }
        public int Id_grupa { get => id_grupa; set => id_grupa = value; }
    }
}
