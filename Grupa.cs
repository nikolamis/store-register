using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TvpProjekat2
{
    class Grupa
    {
        private int id_grupa;
        private string naziv;

        public Grupa() { }

        public Grupa(int id_grupa, string naziv)
        {
            this.Id_grupa = id_grupa;
            this.Naziv = naziv;
        }

        public int Id_grupa { get => id_grupa; set => id_grupa = value; }
        public string Naziv { get => naziv; set => naziv = value; }

        public override string ToString()
        {
            return id_grupa + " " + naziv;
        }
    }
}
