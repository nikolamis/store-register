using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TvpProjekat2
{
    public partial class ArtikalJedan : Form
    {
        Baza baza;
        List<Artikal> artikal;
        String ime;
        Artikal nasArtikal;
        

        public ArtikalJedan(String s)
        {
            InitializeComponent();
            ime = s;
            baza = new Baza();
            artikal = new List<Artikal>();
        }

        private void ArtikalJedan_Load(object sender, EventArgs e)
        {
            try
            {
                baza.OtvoriKonekciju();
                OleDbCommand cmd = new OleDbCommand();
                /*
                 dodao zbog zatvaranja formi
                 
                 */
                



                cmd.Connection = baza.Connection;
                cmd.CommandText = "select * from artikal";
                OleDbDataReader reader = cmd.ExecuteReader();
                artikal.Clear();
                while (reader.Read())
                {
                    Artikal a = new Artikal();
                    a.Id_artikla = int.Parse(reader["id_artikla"].ToString());
                    a.Naziv = reader["naziv"].ToString();
                    a.Cena = int.Parse(reader["cena"].ToString());
                    a.Popust = int.Parse(reader["popust"].ToString());
                    a.Id_grupa = int.Parse(reader["id_grupa"].ToString());
                    artikal.Add(a);

                }
                //ovaj deo sam morao jer mi upit baca problem kada pretrazujem prozivode
                // koji imaju to ime sa naziv like ime, ili naziv in ime.......

                foreach (Artikal a in artikal)
                {
                    if (a.Naziv == ime)
                    {
                        nasArtikal = a;
                    }
                }
                textBox1.Text = nasArtikal.Naziv;
                textBox2.Text = nasArtikal.Cena.ToString();
                textBox3.Text = nasArtikal.Popust.ToString();


            }

            catch (Exception ex)
            { MessageBox.Show("Greska je " + ex); }
            finally
            {
                baza.ZatvoriKonekciju();
            }

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            int cena = (nasArtikal.Cena - nasArtikal.Popust) * Convert.ToInt32(Math.Round(numericUpDown1.Value, 0));
            textBox4.Text = cena.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(Math.Round(numericUpDown1.Value, 0)) == 0)
            {
                MessageBox.Show("Kolicina ne moze biti 0");
            }
            else
            {
                int cena = (nasArtikal.Cena - nasArtikal.Popust) * Convert.ToInt32(Math.Round(numericUpDown1.Value, 0));

                RacunArtikli.dodajNaRacun(nasArtikal.Naziv, Convert.ToInt32(Math.Round(numericUpDown1.Value, 0)), cena);
                MessageBox.Show("artikal uspesno dodat");
            }
        }

        private void ArtikalJedan_FormClosing(object sender, FormClosingEventArgs e)
        {
            String grupa="";
            try {
                baza.OtvoriKonekciju();
                OleDbCommand cmd2 = new OleDbCommand();
                cmd2.Connection = baza.Connection;
                cmd2.CommandText = "select id_grupa from artikal where id_artikla=" + nasArtikal.Id_artikla;
                OleDbDataReader reader2 = cmd2.ExecuteReader();
                
                while (reader2.Read())
                {
                    
                    grupa = reader2["id_grupa"].ToString();
                    

                }


            } catch(Exception ex)
            { MessageBox.Show("Greska je " + ex); }
            finally
            {
                baza.ZatvoriKonekciju();
            }
            
            FormaArtikli formica = new FormaArtikli(grupa);
            formica.Show();



        }
    }
}