using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
namespace TvpProjekat2
{
    public partial class FormaArtikli : Form
    {
        Baza baza;
        List<Artikal> artikli;
        String grupa;
        

        public FormaArtikli(String s) {
            InitializeComponent();
            grupa = s;
            baza = new Baza();
            artikli = new List<Artikal>();
        }

        public void racun()
        {
            listBox1.DataSource = null;
            listBox1.Items.Clear();
            int ukupno = 0;//ne znam zasto ovo ovde al ajde, ne mogu da sredjujem
            textBox2.Clear();
            listBox1.DataSource = RacunArtikli.prikazRacuna();
            foreach (RacunArtikli ra in RacunArtikli.prikazRacuna())
            {
                ukupno += ra.Cena;

            }
            textBox2.Text += ukupno.ToString();
            listBox1.DataSource = RacunArtikli.prikazRacuna();
        }
        private void FormaArtikli_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'prodavnicaDataSet.racun' table. You can move, or remove it, as needed.
            this.racunTableAdapter.Fill(this.prodavnicaDataSet.racun);
            // TODO: This line of code loads data into the 'prodavnicaDataSet.grupa' table. You can move, or remove it, as needed.
            this.grupaTableAdapter.Fill(this.prodavnicaDataSet.grupa);

            try
            {
                racun();

                baza.OtvoriKonekciju();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = baza.Connection;
                cmd.CommandText = "select * from artikal where id_grupa="+grupa;
                OleDbDataReader reader = cmd.ExecuteReader();
                artikli.Clear();
                while (reader.Read())
                {
                    Artikal a = new Artikal();
                    a.Id_artikla= int.Parse(reader["id_artikla"].ToString());
                    a.Naziv = reader["naziv"].ToString();
                    a.Cena = int.Parse(reader["cena"].ToString());
                    a.Popust= int.Parse(reader["popust"].ToString());
                    a.Id_grupa= int.Parse(reader["id_grupa"].ToString());
                    artikli.Add(a);

                }
             

            }

            catch (Exception ex)
            { MessageBox.Show("Greska je " + ex); }
            finally
            {
                baza.ZatvoriKonekciju();
            }

            for (int i = 0; i < artikli.Count(); i++)
            {

                Button button = new Button();
                button.Width = 92;
                button.Height = 62;
                button.Text = artikli[i].Naziv;
                button.Name = artikli[i].Id_grupa.ToString();
                button.Click += new EventHandler(otvoriArtikal);
                flowLayoutPanel1.Controls.Add(button);

            }


            
        
        }

        void otvoriArtikal(object sender, EventArgs e)
        {
            String z = ((Button)sender).Text;
            ArtikalJedan aj = new ArtikalJedan(z);
            this.Hide();
            aj.Show();
            

        }
        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        

       

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FormaArtikli_FormClosing(object sender, FormClosingEventArgs e)
        {
            //druga se vraca na prvu
            Form1 fr = new Form1();
            fr.Show();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            RacunArtikli.izbrisiProizvod(listBox1.SelectedIndex);
            racun();
            // MessageBox.Show((listBox1.SelectedIndex).ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {



                baza.OtvoriKonekciju();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = baza.Connection;
                cmd.CommandText = @"insert into 
                racun(cena,datum,vreme)
                 values (@cena,@datum,@vreme)";
                cmd.Parameters.AddWithValue("cena", int.Parse(textBox2.Text));

                cmd.Parameters.AddWithValue("datum", DateTime.Now.ToShortDateString());
                cmd.Parameters.AddWithValue("vreme", DateTime.Now.ToShortTimeString());

                int rezultat = cmd.ExecuteNonQuery();
                if (rezultat > 0)
                    MessageBox.Show("Racun uspesno dodat!");
                else
                    MessageBox.Show("Dodavanje racuna nije uspelo");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { baza.ZatvoriKonekciju(); }


            RacunArtikli.ocistiRacun();
            racun();
        }
    }
}
