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
    
    public partial class Form1 : Form
    {
        
        Baza baza;
        List<Grupa> grupe;

        public Form1()
        {
            InitializeComponent();
            baza = new Baza();
            grupe = new List<Grupa>();
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
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {

                racun();
                baza.OtvoriKonekciju();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = baza.Connection;
                cmd.CommandText = "select * from grupa";
                OleDbDataReader reader = cmd.ExecuteReader();
                grupe.Clear();
                while (reader.Read()) {
                    Grupa g = new Grupa();
                    g.Id_grupa= int.Parse(reader["id_grupa"].ToString());
                    g.Naziv = reader["naziv"].ToString();
                    grupe.Add(g);
                }



            }
            catch(Exception ex)
            { MessageBox.Show("Greska je " + ex); }
            finally
            {
                baza.ZatvoriKonekciju();
            }

           

            for (int i = 0; i < grupe.Count(); i++)
            {
                
                Button button = new Button();
                button.Width = 92;
                button.Height = 62;
                button.Text = grupe[i].Naziv;
                button.Name = grupe[i].Id_grupa.ToString();
                button.Click += new EventHandler(otvoriArtikle);
                flowLayoutPanel1.Controls.Add(button);
                //dodao ih i postavio im ime da bih mogao da u sledecoj formi
                //povucem ime i izvucem iz baze odredjene prozivode
            }


        }

        void otvoriArtikle(object sender, EventArgs e) {
            String k = ((Button)sender).Name;
            FormaArtikli frmartikli = new FormaArtikli(k);// saljem u sledecu formu informaciju koju je            
            this.Hide();
            frmartikli.Show();                            // grupu izabrao korisnik
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            DodavanjeProizvoda dodavanje = new DodavanjeProizvoda();
            dodavanje.Show();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();//puca ovde kad se ugasi formica
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RacunArtikli.izbrisiProizvod(listBox1.SelectedIndex);
            racun();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //treba ubaciti u bazu :o

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

     

        private void button8_Click(object sender, EventArgs e)
        {
            FormicaRacuni fr = new FormicaRacuni();
            this.Hide();
            fr.Show();
        }
    }
    }

