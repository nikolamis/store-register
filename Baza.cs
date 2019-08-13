using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TvpProjekat2
{
    class Baza
    {
        OleDbConnection connection;

        public Baza() {
            this.connection = new OleDbConnection();
            connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\mirko\Desktop\prodavnica.mdb";
            }

        public OleDbConnection Connection { get => connection; set => connection = value; }

        public void OtvoriKonekciju()
        {
            if (connection != null)
            {
                connection.Open();
            }
        }
        public void ZatvoriKonekciju()
        {
            if (connection != null)
            {
                connection.Close();
            }
        }

    }
}
