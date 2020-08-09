using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace makeup_postgresql
{
    public partial class Sepet : Form
    {
        public Sepet()
        {
            InitializeComponent();
        }
        baglanti conn = new baglanti();
        private string komut;
        NpgsqlCommand cmd;

        void getir()
        {
            komut = @"select * from sepet";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(komut, conn.connection());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        void urun()
        {
            komut = @"Select urunad from urunkayit";
            cmd = new NpgsqlCommand(komut, conn.connection());
            NpgsqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cmburunad.Properties.Items.Add(dr[0]);
            }
        }
        void personel()
        {
            komut = @"Select ad from personel";
            cmd = new NpgsqlCommand(komut, conn.connection());
            NpgsqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cmbPersonel.Properties.Items.Add(dr[0]);
            }
        }
        void musteri()
        {
            komut = @"Select (ad||' '||soyad) as Musteri from musteri";
            cmd = new NpgsqlCommand(komut, conn.connection());
            NpgsqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cmbmusteri.Properties.Items.Add(dr[0]);
            }
        }
        private void Sepet_Load(object sender, EventArgs e)
        {
            getir();
            urun();
            personel();
            musteri();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            komut = @"insert into sepet(alici,personel) 
					values (@p1,@p2)";
            cmd = new NpgsqlCommand(komut, conn.connection());
            cmd.Parameters.AddWithValue("@p1", cmbmusteri.Text);
            cmd.Parameters.AddWithValue("@p2", cmbPersonel.Text);
            cmd.ExecuteNonQuery();
            getir();
            conn.connection().Close();
            MessageBox.Show("Sepet kaydı gerçekleşti!");
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            komut = @"update sepet set alici=@p1,personel=@p2 where sepetid=@p3";
            cmd = new NpgsqlCommand(komut, conn.connection());
            cmd.Parameters.AddWithValue("@p1", cmbmusteri.Text);
            cmd.Parameters.AddWithValue("@p2", cmbPersonel.Text);
            cmd.Parameters.AddWithValue("@p3", int.Parse(txtid.Text));
            cmd.ExecuteNonQuery();
            getir();
            conn.connection().Close();
            MessageBox.Show("Sepet kaydı güncellendi!");
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            komut = @"Delete from sepet where sepetid=@p1";
            cmd = new NpgsqlCommand(komut, conn.connection());
            cmd.Parameters.AddWithValue("@p1", int.Parse(txtid.Text));
            cmd.ExecuteNonQuery();
            getir();
            conn.connection().Close();
            MessageBox.Show("Sepet kaydı silindi!!!");
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            System.Data.DataRow oku = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            txtid.Text = oku["sepetid"].ToString();
            cmbmusteri.Text = oku["alici"].ToString();
            cmbPersonel.Text = oku["personel"].ToString();
        }

        private void btnbul_Click(object sender, EventArgs e)
        {
            komut = @"Select * from sepet where sepetid='" + txtid.Text + "'";
            DataTable list = new DataTable();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(komut, conn.connection());
            da.Fill(list);
            gridControl1.DataSource = list;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtAdet.Text) >= int.Parse(txtmiktar.Text))
            {
                double miktar, fiyat, tutar;
                fiyat = Convert.ToDouble(txtfiyat.Text);
                miktar = Convert.ToDouble(txtmiktar.Text);
                tutar = fiyat * miktar;
                txttutar.Text = tutar.ToString();
                komut = @"insert into sepetekle(urunad,adet,miktar,tutar,sepetid,fiyat,urunkayitid) 
					values (@p1,@p2,@p3,@p4,@p5,@p6,@p7)";
                cmd = new NpgsqlCommand(komut, conn.connection());
                cmd.Parameters.AddWithValue("@p1", cmbmusteri.Text);
                cmd.Parameters.AddWithValue("@p2", int.Parse(txtAdet.Text));
                cmd.Parameters.AddWithValue("@p3", int.Parse(txtmiktar.Text));
                cmd.Parameters.AddWithValue("@p4", decimal.Parse(txttutar.Text));
                cmd.Parameters.AddWithValue("@p5", int.Parse(txtsepetid.Text));
                cmd.Parameters.AddWithValue("@p6", decimal.Parse(txtfiyat.Text));
                cmd.Parameters.AddWithValue("@p7", int.Parse(txturunid.Text));
                cmd.ExecuteNonQuery();
                getir();
                conn.connection().Close();
                MessageBox.Show("Sepete ürün eklendi!");

                komut = @"insert into satinalma(urunid,tutar,sepetid,musteri) 
					values (@p1,@p2,@p3,@p4)";
                cmd = new NpgsqlCommand(komut, conn.connection());
                cmd.Parameters.AddWithValue("@p1", int.Parse(txturunid.Text));
                cmd.Parameters.AddWithValue("@p2", decimal.Parse(txttutar.Text));
                cmd.Parameters.AddWithValue("@p3", int.Parse(txtsepetid.Text));
                cmd.Parameters.AddWithValue("@p4", cmbmusteri.Text);
                cmd.ExecuteNonQuery();
                conn.connection().Close();

            }
           
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            komut = @"Select urunid,satisfiyat,adet from urunkayit where urunad=@p1";
            cmd = new NpgsqlCommand(komut, conn.connection());
            cmd.Parameters.AddWithValue("@p1", cmburunad.Text);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txturunid.Text = dr["urunid"].ToString();
                txtfiyat.Text = dr["satisfiyat"].ToString();
                txtAdet.Text = dr["adet"].ToString();
            }
            conn.connection().Close();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            sepetekle sp = new sepetekle();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr!=null)
            {
                sp.urunid = dr["sepetid"].ToString();
            }
            sp.Show();
        }
    }
}
