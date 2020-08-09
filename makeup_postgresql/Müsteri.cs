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
    public partial class Müsteri : Form
    {
        public Müsteri()
        {
            InitializeComponent();
        }
        baglanti conn = new baglanti();
        private string komut;
        NpgsqlCommand cmd;

        void getir()
        {
            komut = @"select * from musteri";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(komut, conn.connection());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        void iller()
        {
            komut = @"Select ilad from iller";
            cmd = new NpgsqlCommand(komut, conn.connection());
            NpgsqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cmbil.Properties.Items.Add(dr[0]);
            }
        }
        private void Müsteri_Load(object sender, EventArgs e)
        {
            getir();
            iller();
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            System.Data.DataRow oku = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            txtid.Text = oku["musteriid"].ToString();
            txtAd.Text = oku["ad"].ToString();
            txtSoyad.Text = oku["soyad"].ToString();
            txtMail.Text = oku["mail"].ToString();
            mskTel.Text = oku["telefon"].ToString();
            cmbil.Text = oku["il"].ToString();
            cmbilce.Text = oku["ilce"].ToString();
            txtAdres.Text = oku["adres"].ToString();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            komut = @"insert into musteri(ad,soyad,mail,telefon,il,ilce,adres) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7)";
            cmd = new NpgsqlCommand(komut, conn.connection());
            cmd.Parameters.AddWithValue("@p1", txtAd.Text);
            cmd.Parameters.AddWithValue("@p2", txtSoyad.Text);
            cmd.Parameters.AddWithValue("@p3", txtMail.Text);
            cmd.Parameters.AddWithValue("@p4", mskTel.Text);
            cmd.Parameters.AddWithValue("@p5", cmbil.Text);
            cmd.Parameters.AddWithValue("@p6", cmbilce.Text);
            cmd.Parameters.AddWithValue("@p7", txtAdres.Text);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Musteri kaydı gerçekleşti!");
            conn.connection().Close();
            getir();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            komut = @"update musteri set ad=@p1,soyad=@p2,mail=@p4,telefon=@p5,il=@p6,ilce=@p7,adres=@p8 where 
                        musteriid=@p9";
            cmd = new NpgsqlCommand(komut, conn.connection());
            cmd.Parameters.AddWithValue("@p1", txtAd.Text);
            cmd.Parameters.AddWithValue("@p2", txtSoyad.Text);
            cmd.Parameters.AddWithValue("@p4", txtMail.Text);
            cmd.Parameters.AddWithValue("@p5", mskTel.Text);
            cmd.Parameters.AddWithValue("@p6", cmbil.Text);
            cmd.Parameters.AddWithValue("@p7", cmbilce.Text);
            cmd.Parameters.AddWithValue("@p8", txtAdres.Text);
            cmd.Parameters.AddWithValue("@p9", int.Parse(txtid.Text));
            cmd.ExecuteNonQuery();
            MessageBox.Show("Musteri kaydı güncellendi!");
            conn.connection().Close();
            getir();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            komut = @"delete from musteri where musteriid=@p1";
            cmd = new NpgsqlCommand(komut, conn.connection());
            cmd.Parameters.AddWithValue("@p1", int.Parse(txtid.Text));
            cmd.ExecuteNonQuery();
            MessageBox.Show("Musteri kaydı silindi!");
            conn.connection().Close();
            getir();
        }

        private void btnbul_Click(object sender, EventArgs e)
        {
            komut = @"Select * from musteri where musteriid='" + txtid.Text + "'";
            DataTable list = new DataTable();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(komut, conn.connection());
            da.Fill(list);
            gridControl1.DataSource = list;
        }

        private void cmbil_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbilce.Properties.Items.Clear();
            komut = @"select ilceadi from ilceler where ild=@p1";
            cmd = new NpgsqlCommand(komut, conn.connection());
            cmd.Parameters.AddWithValue("@p1", cmbil.SelectedIndex + 1);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cmbilce.Properties.Items.Add(dr[0]);
            }
            conn.connection().Close();
        }

        private void cmbilce_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
