using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TvpProjekat2
{
    class RacunArtikli
    {
        String ime;
        int kolicina;
        int cena;


        public override string ToString()
        {
            return ime + " " + kolicina.ToString()+" kom" + " " + cena.ToString()+"din";
        }

        public static List<RacunArtikli> lista = new List<RacunArtikli>();
       
        public string Ime { get => ime; set => ime = value; }
        public int Kolicina { get => kolicina; set => kolicina = value; }
        public int Cena { get => cena; set => cena = value; }

        public RacunArtikli(string ime, int kolicina, int cena)
        {
            this.ime = ime;
            this.kolicina = kolicina;
            this.cena = cena;
        }

        public RacunArtikli()
        {
           
        }


        public static void dodajNaRacun(String s, int k, int c)
        {
            RacunArtikli aa = new RacunArtikli();
            aa.ime = s;
            aa.kolicina = k;
            aa.cena = c;
            lista.Add(aa);
        }

        public static List<RacunArtikli> prikazRacuna() { return lista; }

        public static void izbrisiProizvod(int indeks) {
            try
            {
                lista.RemoveAt(indeks);
            }
            catch (Exception ex) { }
        }

        public static void ocistiRacun() { lista.Clear(); }

    }
}
