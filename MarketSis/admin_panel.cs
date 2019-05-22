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
    public partial class admin_panel : Form
    {
        public admin_panel()
        {
            InitializeComponent();
        }
        OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=market_DB.mdb");
        private void admin_panel_Load(object sender, EventArgs e)
        {

        }

        double ekmek_tl;
        private void button1_Click(object sender, EventArgs e)
        {
            ekmek_tl = Convert.ToDouble(textBox1.Text);
            baglan.Open();
            OleDbCommand ekmek_cmd = new OleDbCommand("update sabit_fiyat set fiyat='"+ekmek_tl+"' where urun='Ekmek'",baglan);
            ekmek_cmd.ExecuteNonQuery();
            baglan.Close();
            MessageBox.Show("Güncelleme Başarılı!");
        }

        double sut_tl;
        private void button2_Click(object sender, EventArgs e)
        {
            sut_tl = Convert.ToDouble(textBox2.Text);
            baglan.Open();
            OleDbCommand sut_cmd = new OleDbCommand("update sabit_fiyat set fiyat='" + sut_tl + "' where urun='Süt'", baglan);
            sut_cmd.ExecuteNonQuery();
            baglan.Close();
            MessageBox.Show("Güncelleme Başarılı!");
        }

        double su10_tl;
        private void button3_Click(object sender, EventArgs e)
        {
            su10_tl = Convert.ToDouble(textBox3.Text);
            baglan.Open();
            OleDbCommand su10_cmd = new OleDbCommand("update sabit_fiyat set fiyat='" + su10_tl + "' where urun='10 LT. SU'", baglan);
            su10_cmd.ExecuteNonQuery();
            baglan.Close();
            MessageBox.Show("Güncelleme Başarılı!");
        }

        double su19_tl;
        private void button4_Click(object sender, EventArgs e)
        {
            su19_tl = Convert.ToDouble(textBox4.Text);
            baglan.Open();
            OleDbCommand su19_cmd = new OleDbCommand("update sabit_fiyat set fiyat='" + su19_tl + "' where urun='19 LT. SU'", baglan);
            su19_cmd.ExecuteNonQuery();
            baglan.Close();
            MessageBox.Show("Güncelleme Başarılı!");
        }
    }
}
