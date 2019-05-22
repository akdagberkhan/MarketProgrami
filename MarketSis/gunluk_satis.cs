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
    public partial class gunluk_satis : Form
    {
        public gunluk_satis()
        {
            InitializeComponent();
        }
        OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=market_DB.mdb");

        private void gunluk_satis_Load(object sender, EventArgs e)
        {
            baglan.Open();

            DataTable tb = new DataTable();
            OleDbDataAdapter adp = new OleDbDataAdapter("select * from gunluk_satis",baglan);
            adp.Fill(tb);

            dataGridView1.DataSource = tb;
            baglan.Close();
        }
    }
}
