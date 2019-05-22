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
    public partial class urun_duzenle : Form
    {
        public urun_duzenle()
        {
            InitializeComponent();
        }
        OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=market_DB.mdb");
        private void button5_Click(object sender, EventArgs e)
        {
          
                baglan.Open();
                OleDbCommand sil_cmd = new OleDbCommand("delete from envanter where barkod_no='" + textBox13.Text + "'", baglan);
                sil_cmd.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show("Ürün Silindi!");
            
            
        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {
           
                if (textBox14.Text.Length == 13)
                {
                    DataTable guncelle_tb = new DataTable();
                    OleDbDataAdapter guncelle_adp = new OleDbDataAdapter("select * from envanter where barkod_no='" + textBox14.Text + "'", baglan);
                    guncelle_adp.Fill(guncelle_tb);

                    textBox12.Text = guncelle_tb.Rows[0][0].ToString();
                    textBox11.Text = guncelle_tb.Rows[0][1].ToString();
                    textBox10.Text = guncelle_tb.Rows[0][2].ToString();
                    textBox9.Text = guncelle_tb.Rows[0][4].ToString();
                    textBox8.Text = guncelle_tb.Rows[0][5].ToString();
                    textBox7.Text = guncelle_tb.Rows[0][6].ToString();
                    if (guncelle_tb.Rows[0][3].ToString() == "Adet")
                    {
                        comboBox2.SelectedItem = "Adet";
                    }
                    else
                    {
                        comboBox2.SelectedItem = "Kg";
                    }
                }
        
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (Control item in tabPage2.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                    textBox14.Text = "";
                    comboBox2.SelectedItem = null;
                }
            }
        }

        double guncel_alisf;
        double guncel_satisf;
        int stok_adet;
        private void button4_Click(object sender, EventArgs e)
        {
           
                guncel_alisf = Convert.ToDouble(textBox9.Text);
                guncel_satisf = Convert.ToDouble(textBox8.Text);
                stok_adet = Convert.ToInt16(textBox10.Text);

                baglan.Open();
                OleDbCommand guncelle_cmd = new OleDbCommand("update envanter set urun_adi='" + textBox11.Text + "' , barkod_no='" + textBox12.Text + "' , stok=" + stok_adet + " , olcu_birimi='" + comboBox2.Text + "' , alis_fiyati='" + guncel_alisf + "' , satis_fiyati='" + guncel_satisf + "' , urun_turu='" + textBox7.Text + "' where barkod_no='" + textBox14.Text + "'", baglan);
                guncelle_cmd.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show("Güncelleme Başarılı!");
          
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (Control item in tabPage1.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                    comboBox1.SelectedItem = null;
                }
            }
        }
        double ekle_alisf;
        double ekle_satisf;
        int eklestok_adet;
        private void button1_Click(object sender, EventArgs e)
        {
           
                ekle_alisf = Convert.ToDouble(textBox4.Text);
                ekle_satisf = Convert.ToDouble(textBox5.Text);
                eklestok_adet = Convert.ToInt16(textBox3.Text);

                baglan.Open();
                OleDbDataAdapter envanter_adp = new OleDbDataAdapter("select * from envanter where barkod_no='" + textBox1.Text + "'", baglan);
                DataTable envanter_tb = new DataTable();
                envanter_adp.Fill(envanter_tb);

                if (envanter_tb.Rows.Count <= 0)
                {
                    OleDbCommand ekle_cmd = new OleDbCommand("insert into envanter(barkod_no,urun_adi,stok,olcu_birimi,alis_fiyati,satis_fiyati,urun_turu) values('" + textBox1.Text + "','" + textBox2.Text + "'," + eklestok_adet + ",'" + comboBox1.Text + "'," + ekle_alisf + "," + ekle_satisf + ",'" + textBox6.Text + "')", baglan);
                    ekle_cmd.ExecuteNonQuery();

                    MessageBox.Show("Ürün Eklendi!");

                }
                else
                {
                    MessageBox.Show("Ürün Zaten Ekli!");
                }
                baglan.Close();
            
        }
    }
}
