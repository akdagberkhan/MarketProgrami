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
    public partial class anasayfa : Form
    {
        public anasayfa()
        {
            InitializeComponent();
        }
        OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=market_DB.mdb");
        private void çıkışToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        double top_fiyat;
        public string barkod = "";
        string gelen;
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
                if (textBox1.Text.Length == 13)
                {
                    barkod = textBox1.Text;
                    textBox1.Text = "";
                    if (sayi != "")
                    {
                        for (int i = 0; i < Convert.ToInt16(sayi); i++)
                        {
                            baglan.Open();
                            DataTable tb = new DataTable();
                            OleDbDataAdapter adp = new OleDbDataAdapter("select barkod_no,urun_adi,olcu_birimi,satis_fiyati,urun_turu from envanter where barkod_no='" + barkod + "'", baglan);
                            adp.Fill(tb);
                            gelen = tb.Rows[0][0].ToString() + "    " + tb.Rows[0][1].ToString() + "    " + tb.Rows[0][2].ToString() + "    " + tb.Rows[0][3].ToString() + "TL    " + tb.Rows[0][4].ToString();
                            listBox1.Items.Add(gelen);
                            listBox1.Items.Add("---------------------------------------------");
                            OleDbCommand sepet = new OleDbCommand("insert into sepet(barkod_no,urun_adi,satis_fiyati,urun_turu) values('" + tb.Rows[0][0].ToString() + "','" + tb.Rows[0][1].ToString() + "','" + tb.Rows[0][3] + "','" + tb.Rows[0][4].ToString() + "')", baglan);
                            sepet.ExecuteNonQuery();
                            baglan.Close();
                            top_fiyat += Convert.ToDouble(tb.Rows[0][3]);
                            label2.Text = "TOPLAM: " + top_fiyat + " TL";

                        }
                        textBox1.Select();
                        sayi = "";
                        label4.Text = "00";
                    }
                    else
                    {

                        baglan.Open();
                        DataTable tb = new DataTable();
                        OleDbDataAdapter adp = new OleDbDataAdapter("select barkod_no,urun_adi,olcu_birimi,satis_fiyati,urun_turu from envanter where barkod_no='" + barkod + "'", baglan);
                        adp.Fill(tb);
                        gelen = tb.Rows[0][0].ToString() + "    " + tb.Rows[0][1].ToString() + "    " + tb.Rows[0][2].ToString() + "    " + tb.Rows[0][3].ToString() + "TL    " + tb.Rows[0][4].ToString();
                        listBox1.Items.Add(gelen);
                        listBox1.Items.Add("---------------------------------------------");
                        OleDbCommand sepet = new OleDbCommand("insert into sepet(barkod_no,urun_adi,satis_fiyati,urun_turu) values('" + tb.Rows[0][0].ToString() + "','" + tb.Rows[0][1].ToString() + "','" + tb.Rows[0][3] + "','" + tb.Rows[0][4].ToString() + "')", baglan);
                        sepet.ExecuteNonQuery();
                        baglan.Close();
                        top_fiyat += Convert.ToDouble(tb.Rows[0][3]);
                        label2.Text = "TOPLAM: " + top_fiyat + " TL";
                        textBox1.Select();
                    }


                }
         
            
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            

            
           
        }

        private void button15_Click(object sender, EventArgs e)
        {
            baglan.Open();
            OleDbCommand cmd_sepet = new OleDbCommand("delete from sepet", baglan);
            cmd_sepet.ExecuteNonQuery();
            baglan.Close();

            label4.Text = "00";
            sayi = "";
            textBox1.Text = "";
            listBox1.Items.Clear();
            textBox2.Text ="";
            label2.Text = "TOPLAM:";
            top_fiyat = 0;
        }

        private void anasayfa_Load(object sender, EventArgs e)
        {
            textBox1.Select();
            for (int i = 0; i <= 9; i++)
            {
                Button btn = new Button();
                btn.Size = new Size(92, 92);
                btn.Tag = i.ToString();
                btn.Font = new Font(btn.Font.FontFamily, 15);
                btn.Text = i.ToString();
                flowLayoutPanel1.Controls.Add(btn);
                btn.Click += Btn_Click;
            }
            
        }
        string sayi ="";
        private void Btn_Click(object sender, EventArgs e)
        {

            Button gelen = (sender as Button);
            sayi +=gelen.Tag;
            label4.Text = sayi;
            textBox1.Select();
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
                baglan.Open();
                OleDbDataAdapter kazanc = new OleDbDataAdapter("select * from gunluk_kazanc", baglan);
                DataTable tb = new DataTable();
                kazanc.Fill(tb);
                MessageBox.Show("Günlük Kazancın: " + tb.Rows[0][0].ToString() + " TL");
                baglan.Close();
        
           
        }

        private void button14_Click(object sender, EventArgs e)
        {
            
                baglan.Open();
                OleDbDataAdapter sepet_adp = new OleDbDataAdapter("select * from sepet", baglan);
                DataTable sepet_tb = new DataTable();
                sepet_adp.Fill(sepet_tb);
       
                for (int i = 0; i < sepet_tb.Rows.Count; i++)
                {
                    OleDbCommand cmd_satis = new OleDbCommand("insert into gunluk_satis(barkod_no,urun_adi,urun_fiyati,urun_turu) values('" + sepet_tb.Rows[i][0] + "','" + sepet_tb.Rows[i][1] + "','" + sepet_tb.Rows[i][2] + "','" + sepet_tb.Rows[i][3] + "')", baglan);
                    cmd_satis.ExecuteNonQuery();
                    OleDbCommand envanter_cmd = new OleDbCommand("update envanter set stok=stok-1 where barkod_no='" + sepet_tb.Rows[i][0] + "'", baglan);
                    envanter_cmd.ExecuteNonQuery();
                }

                OleDbCommand cmd_sepet = new OleDbCommand("delete from sepet", baglan);
                cmd_sepet.ExecuteNonQuery();

                OleDbDataAdapter kazanc_adp = new OleDbDataAdapter("select * from gunluk_kazanc", baglan);
                DataTable kazanc_table = new DataTable();
                kazanc_adp.Fill(kazanc_table);
                double kh = Convert.ToDouble(kazanc_table.Rows[0][0]);
                top_fiyat += kh;

                OleDbCommand kazanc_guncelle = new OleDbCommand("update gunluk_kazanc set kazanc='"+top_fiyat+"'", baglan);
                kazanc_guncelle.ExecuteNonQuery();

                baglan.Close();
                MessageBox.Show("Ürün Satıldı!");
                textBox1.Text = "";
                listBox1.Items.Clear();
                textBox2.Text = "";
                label2.Text = "TOPLAM:";
                top_fiyat = 0;
          
           
        }

        double sut_fiyat;
        double ekmek_fiyat;
        double su19_fiyat;
        double su10_fiyat;
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            textBox2.Text = "";
        }

        private void button10_Click(object sender, EventArgs e)
        {   baglan.Open();
                OleDbDataAdapter sut_adp = new OleDbDataAdapter("select fiyat from sabit_fiyat where urun='Süt' ", baglan);
                DataTable sut_tb = new DataTable();
                sut_adp.Fill(sut_tb);
                sut_fiyat = Convert.ToDouble(sut_tb.Rows[0][0]) * Convert.ToDouble(textBox2.Text);

                listBox1.Items.Add("Süt      " + sut_fiyat + " TL");
                listBox1.Items.Add("---------------------------------------------");
                OleDbCommand sepet = new OleDbCommand("insert into sepet(urun_adi,satis_fiyati) values('Süt','" + sut_fiyat + "')", baglan);
                sepet.ExecuteNonQuery();
                baglan.Close();
                top_fiyat += sut_fiyat;
                label2.Text = "TOPLAM: " + top_fiyat + " TL";
                textBox1.Select();
                textBox2.Text = "";
      
           
            
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (sayi != "")
            {
                baglan.Open();
                OleDbDataAdapter ekmek_adp = new OleDbDataAdapter("select fiyat from sabit_fiyat where urun='Ekmek' ", baglan);
                DataTable ekmek_tb = new DataTable();
                ekmek_adp.Fill(ekmek_tb);
                ekmek_fiyat = Convert.ToDouble(ekmek_tb.Rows[0][0]) * 1;
                for (int i = 0; i < Convert.ToInt16(sayi); i++)
                {
                    listBox1.Items.Add("Ekmek      " + ekmek_fiyat + " TL");
                    listBox1.Items.Add("---------------------------------------------");
                    OleDbCommand sepet = new OleDbCommand("insert into sepet(urun_adi,satis_fiyati) values('Ekmek','" + ekmek_fiyat + "')", baglan);
                    sepet.ExecuteNonQuery();
                    
                }
                baglan.Close();
                top_fiyat += ekmek_fiyat;
                top_fiyat = top_fiyat * (Convert.ToInt16(sayi));
                label2.Text = "TOPLAM: " + top_fiyat + " TL";
                textBox1.Select();
                sayi = "";
                label4.Text = "00";
            }
            else
            {
                baglan.Open();
                OleDbDataAdapter ekmek_adp = new OleDbDataAdapter("select fiyat from sabit_fiyat where urun='Ekmek' ", baglan);
                DataTable ekmek_tb = new DataTable();
                ekmek_adp.Fill(ekmek_tb);
                ekmek_fiyat = Convert.ToDouble(ekmek_tb.Rows[0][0]) * 1;

                listBox1.Items.Add("Ekmek      " + ekmek_fiyat + " TL");
                listBox1.Items.Add("---------------------------------------------");
                OleDbCommand sepet = new OleDbCommand("insert into sepet(urun_adi,satis_fiyati) values('Ekmek','" + ekmek_fiyat + "')", baglan);
                sepet.ExecuteNonQuery();
                baglan.Close();
                top_fiyat += ekmek_fiyat;
                label2.Text = "TOPLAM: " + top_fiyat + " TL";
                textBox1.Select();
            }
  
        }

        private void button12_Click(object sender, EventArgs e)
        {
           
                baglan.Open();
                OleDbDataAdapter su10_adp = new OleDbDataAdapter("select fiyat from sabit_fiyat where urun='10 LT. SU' ", baglan);
                DataTable su10_tb = new DataTable();
                su10_adp.Fill(su10_tb);
                su10_fiyat = Convert.ToDouble(su10_tb.Rows[0][0]) * 1;

                listBox1.Items.Add("10 LT. SU      " + su10_fiyat + " TL");
                listBox1.Items.Add("---------------------------------------------");
                OleDbCommand sepet = new OleDbCommand("insert into sepet(urun_adi,satis_fiyati) values('10 LT. SU','" + su10_fiyat + "')", baglan);
                sepet.ExecuteNonQuery();
                baglan.Close();
                top_fiyat += su10_fiyat;
                label2.Text = "TOPLAM: " + top_fiyat + " TL";
                textBox1.Select();
        
            

            

        }

        private void button13_Click(object sender, EventArgs e)
        {
           
                baglan.Open();
                OleDbDataAdapter su19_adp = new OleDbDataAdapter("select fiyat from sabit_fiyat where urun='19 LT. SU' ", baglan);
                DataTable su19_tb = new DataTable();
                su19_adp.Fill(su19_tb);
                su19_fiyat = Convert.ToDouble(su19_tb.Rows[0][0]) * 1;

                listBox1.Items.Add("19 LT. SU      " + su19_fiyat + " TL");
                listBox1.Items.Add("---------------------------------------------");
                OleDbCommand sepet = new OleDbCommand("insert into sepet(urun_adi,satis_fiyati) values('19 LT. SU','" + su19_fiyat + "')", baglan);
                sepet.ExecuteNonQuery();
                baglan.Close();
                top_fiyat += su19_fiyat;
                label2.Text = "TOPLAM: " + top_fiyat + " TL";
                textBox1.Select();
          
           


            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            gunluk_satis gs = new gunluk_satis();
            gs.ShowDialog();


        }

        private void button7_Click(object sender, EventArgs e)
        {
            envanter er = new envanter();
            er.ShowDialog();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            
                baglan.Open();

                OleDbDataAdapter kazanc_adp = new OleDbDataAdapter("select * from gunluk_kazanc", baglan);
                DataTable kazanc_table = new DataTable();
                kazanc_adp.Fill(kazanc_table);
                double gk = Convert.ToDouble(kazanc_table.Rows[0][0]);

                OleDbDataAdapter top_kazanc = new OleDbDataAdapter("select * from toplam_kazanc", baglan);
                DataTable topkazanc_tb = new DataTable();
                top_kazanc.Fill(topkazanc_tb);
                double tk = Convert.ToDouble(topkazanc_tb.Rows[0][0]);

                tk += gk;
                OleDbCommand topkazanc_sil = new OleDbCommand("delete from toplam_kazanc", baglan);
                topkazanc_sil.ExecuteNonQuery();

                OleDbCommand cmd_topkazanc = new OleDbCommand("insert into toplam_kazanc(kazanc) values('" + tk+ "')", baglan);
                cmd_topkazanc.ExecuteNonQuery();
  

                OleDbCommand sil_kazanc = new OleDbCommand("delete * from gunluk_kazanc", baglan);
                OleDbCommand sil_satis = new OleDbCommand("delete * from gunluk_satis", baglan);
                double s = 0;
                OleDbCommand ekle = new OleDbCommand("insert into gunluk_kazanc(kazanc) values('" + s + "')", baglan);

                sil_kazanc.ExecuteNonQuery();
                sil_satis.ExecuteNonQuery();
                ekle.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show("Gün Bitti!");
                
        

        }

        private void button16_Click(object sender, EventArgs e)
        {
            
                baglan.Open();
                OleDbDataAdapter kazanc = new OleDbDataAdapter("select * from toplam_kazanc", baglan);
                DataTable tb = new DataTable();
                kazanc.Fill(tb);
                MessageBox.Show("Toplam Kazancın: " + tb.Rows[0][0].ToString() + " TL");
                baglan.Close();
          
        }

        private void button6_Click(object sender, EventArgs e)
        {
            stok_durumu sd = new stok_durumu();
            sd.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            admin_panel ap = new admin_panel();
            ap.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            urun_duzenle ud = new urun_duzenle();
            ud.ShowDialog();
        }
    }
}
