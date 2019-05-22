using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace MarketSis
{
    public partial class stok_durumu : Form
    {
        public stok_durumu()
        {
            InitializeComponent();
        }

        OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=market_DB.mdb");
        private void stok_durumu_Load(object sender, EventArgs e)
        {
            baglan.Open();

            DataTable tb = new DataTable();
            OleDbDataAdapter adp = new OleDbDataAdapter("select * from envanter where stok<=5", baglan);
            adp.Fill(tb);

            dataGridView1.DataSource = tb;
            baglan.Close();
        }
    }
}
