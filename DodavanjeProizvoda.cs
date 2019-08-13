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
    public partial class DodavanjeProizvoda : Form
    {
        Baza baza;
        List<Grupa> grupe;

        public DodavanjeProizvoda()
        {
            InitializeComponent();
            baza = new Baza();
            grupe = new List<Grupa>();
        }

        private void artikalBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.artikalBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.prodavnicaDataSet);

        }

        private void DodavanjeProizvoda_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'prodavnicaDataSet.artikal' table. You can move, or remove it, as needed.
            this.artikalTableAdapter.Fill(this.prodavnicaDataSet.artikal);
            try
            {
                
                baza.OtvoriKonekciju();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = baza.Connection;
                cmd.CommandText = "select * from grupa";
                OleDbDataReader reader = cmd.ExecuteReader();
                grupe.Clear();
                while (reader.Read())
                {
                    Grupa g = new Grupa();
                    g.Id_grupa = int.Parse(reader["id_grupa"].ToString());
                    g.Naziv = reader["naziv"].ToString();
                    grupe.Add(g);
                }

                if (comboBox1.Items.Count > 0)
                {
                    comboBox1.DataSource = null;
                    comboBox1.Items.Clear();
                }
                comboBox1.DataSource = grupe;
                comboBox1.DisplayMember = "naziv";
                comboBox1.ValueMember = "id_grupa";
                
            }
            catch (Exception ex)
            { MessageBox.Show("Greska je " + ex); }
            finally
            {
                baza.ZatvoriKonekciju();
            }
            


        }
       

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                String x = comboBox1.SelectedItem.ToString()[0].ToString();           
                

                baza.OtvoriKonekciju();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = baza.Connection;
                cmd.CommandText = @"insert into 
                artikal(naziv,cena,popust,id_grupa)
                 values (@naziv,@cena,@popust,@id_grupa)";
                cmd.Parameters.AddWithValue("naziv", textBox1.Text);
                cmd.Parameters.AddWithValue("cena", int.Parse(textBox2.Text));
                cmd.Parameters.AddWithValue("popust", int.Parse(textBox3.Text));
                cmd.Parameters.AddWithValue("id_grupa", int.Parse(x));
                int rezultat = cmd.ExecuteNonQuery();
                if (rezultat > 0)
                    MessageBox.Show("Artikal uspesno dodat!");
                else
                    MessageBox.Show("Dodavanje zapisa nije uspelo");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { baza.ZatvoriKonekciju(); }
        }

        private void DodavanjeProizvoda_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
}
