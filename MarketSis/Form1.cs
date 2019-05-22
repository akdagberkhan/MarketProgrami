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

namespace MarketSis
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=market_DB.mdb");
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                baglan.Open();
                DataTable tablo = new DataTable();
                OleDbDataAdapter adp = new OleDbDataAdapter("select * from admin where kul_ad='" + textBox1.Text + "' and sifre='" + textBox2.Text + "'", baglan);
                adp.Fill(tablo);
                baglan.Close();

                if (tablo.Rows[0][1].ToString() != "")
                {
                    
                    anasayfa ana = new anasayfa();
                    ana.Show();
                    this.Hide();
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Hatalı Giriş");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = "admin";
            textBox2.Text = "123456";
        }
    }
}
