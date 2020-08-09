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
    public partial class Marka : Form
    {
        public Marka()
        {
            InitializeComponent();
        }
        baglanti conn = new baglanti();
        private string komut;
        NpgsqlCommand cmd;

        void getir()
        {
            komut = @"select * from marka";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(komut, conn.connection());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        private void Marka_Load(object sender, EventArgs e)
        {
            getir();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            komut = @"insert into marka(markaad,kategori,yetkili) 
					values (@p1,@p2,@p3)";
            cmd = new NpgsqlCommand(komut, conn.connection());
            cmd.Parameters.AddWithValue("@p1", txtAd.Text);
            cmd.Parameters.AddWithValue("@p2", cmbKategori.Text);
            cmd.Parameters.AddWithValue("@p3", txtYetkili.Text);
            cmd.ExecuteNonQuery();
            getir();
            conn.connection().Close();
            MessageBox.Show("Marka kaydı gerçekleşti!");
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            komut = @"update marka set markaad=@p1 , kategori=@p2 , yetkili=@p3 where markaid=@p4";
            cmd = new NpgsqlCommand(komut, conn.connection());
            cmd.Parameters.AddWithValue("@p1", txtAd.Text);
            cmd.Parameters.AddWithValue("@p2", cmbKategori.Text);
            cmd.Parameters.AddWithValue("@p3", txtYetkili.Text);
            cmd.Parameters.AddWithValue("@p4", int.Parse(txtid.Text));
            cmd.ExecuteNonQuery();
            conn.connection().Close();
            MessageBox.Show("Marka kaydı güncellendi!!");
            getir();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            komut = @"delete from marka where markaid=@p1";
            NpgsqlCommand cmd = new NpgsqlCommand(komut, conn.connection());
            cmd.Parameters.AddWithValue("@p1", int.Parse(txtid.Text));
            cmd.ExecuteNonQuery();
            conn.connection().Close();
            MessageBox.Show("Marka kaydı silindi!!!");
            getir();
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            System.Data.DataRow oku = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            txtid.Text = oku["markaid"].ToString();
            txtAd.Text = oku["markaad"].ToString();
            cmbKategori.Text = oku["kategori"].ToString();
            txtYetkili.Text = oku["yetkili"].ToString();
        }
    }
}
