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
    public partial class FormicaRacuni : Form
    {
        Baza baza;
        List<Racun> racun;
        public FormicaRacuni()
        {
            InitializeComponent();
            baza = new Baza();
            racun = new List<Racun>();
        }

        private void FormicaRacuni_Load(object sender, EventArgs e)
        {
            try
            {

                
                baza.OtvoriKonekciju();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = baza.Connection;
                cmd.CommandText = "select * from racun";
                OleDbDataReader reader = cmd.ExecuteReader();
                racun.Clear();
                while (reader.Read())
                {
                    
                    Racun r = new Racun();
                    r.Cena = int.Parse(reader["cena"].ToString());
                    r.Datum = DateTime.Parse(reader["datum"].ToString());
                    r.Vreme = DateTime.Parse(reader["vreme"].ToString());
                   //ajde ovo ispravi leba ti


                    racun.Add(r);
                }

                listBox1.DataSource = racun;

            }
            catch (Exception ex)
            { MessageBox.Show("Greska je " + ex); }
            finally
            {
                baza.ZatvoriKonekciju();
            }


            
        }

        private void FormicaRacuni_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            Form1 fr = new Form1();
            fr.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Hide();
            Form1 fr = new Form1();
            
            fr.Show();
            timer1.Stop();

        }
    }
}
