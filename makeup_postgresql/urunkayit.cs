using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.ClipboardSource.SpreadsheetML;
using Npgsql;

namespace makeup_postgresql
{
	public partial class urunkayit : Form
	{
		public urunkayit()
		{
			InitializeComponent();
		}
		baglanti conn = new baglanti();
		private string komut;
		NpgsqlCommand cmd;

		void getir()
		{
			komut = @"select * from urunkayit";
			NpgsqlDataAdapter da = new NpgsqlDataAdapter(komut, conn.connection());
			DataTable dt = new DataTable();
			da.Fill(dt);
			gridControl1.DataSource = dt;
		}
		void marka()
        {
			komut = @"Select markaad from marka";
			cmd = new NpgsqlCommand(komut, conn.connection());
			NpgsqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
				cmbMarka.Properties.Items.Add(dr[0]);
            }
        }
		private void urunkayit_Load(object sender, EventArgs e)
		{
			getir();
			marka();
		}

        private void simpleButton1_Click(object sender, EventArgs e)
        {
			komut = @"insert into urunkayit(urunad,urunmarka,kategorisi,alisfiyat,satisfiyat,adet,detay) 
					values (@p1,@p2,@p3,@p4,@p5,@p6,@p7)";
			cmd = new NpgsqlCommand(komut, conn.connection());
			cmd.Parameters.AddWithValue("@p1", txtAd.Text);
			cmd.Parameters.AddWithValue("@p2", cmbMarka.Text);
			cmd.Parameters.AddWithValue("@p3", cmbKategori.Text);
			cmd.Parameters.AddWithValue("@p4", decimal.Parse(txtAlisfiyat.Text.ToString()));
			cmd.Parameters.AddWithValue("@p5", decimal.Parse(txtSatisfiyat.Text.ToString()));
			cmd.Parameters.AddWithValue("@p6", int.Parse(txtAdet.Text));
			cmd.Parameters.AddWithValue("@p7", txtDetay.Text);
			cmd.ExecuteNonQuery();
			getir();
			conn.connection().Close();
			MessageBox.Show("Ürün kaydı gerçekleşti!");

		}

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
			komut = @"update urunkayit set urunad=@p1,urunmarka=@p2,kategorisi=@p3,alisfiyat=@p4,satisfiyat=@p5,
					adet=@p6,detay=@p7 where urunid=@p8";
			cmd = new NpgsqlCommand(komut, conn.connection());
			cmd.Parameters.AddWithValue("@p1", txtAd.Text);
			cmd.Parameters.AddWithValue("@p2", cmbMarka.Text);
			cmd.Parameters.AddWithValue("@p3", cmbKategori.Text);
			cmd.Parameters.AddWithValue("@p4", decimal.Parse(txtAlisfiyat.Text.ToString()));
			cmd.Parameters.AddWithValue("@p5", decimal.Parse(txtSatisfiyat.Text.ToString()));
			cmd.Parameters.AddWithValue("@p6", int.Parse(txtAdet.Text));
			cmd.Parameters.AddWithValue("@p7", txtDetay.Text);
			cmd.Parameters.AddWithValue("@p8", int.Parse(txtid.Text));
			cmd.ExecuteNonQuery();
			getir();
			conn.connection().Close();
			MessageBox.Show("Ürün kaydı güncellendi!");
		}

        private void btnSil_Click(object sender, EventArgs e)
        {
			komut = @"Delete from urunkayit where urunid=@p1";
			cmd = new NpgsqlCommand(komut, conn.connection());
			cmd.Parameters.AddWithValue("@p1", int.Parse(txtid.Text));
			cmd.ExecuteNonQuery();
			getir();
			conn.connection().Close();
			MessageBox.Show("Ürün kaydı silindi!!!");
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
			System.Data.DataRow oku = gridView1.GetDataRow(gridView1.FocusedRowHandle);
			txtid.Text = oku["urunid"].ToString();
			txtAd.Text = oku["urunad"].ToString();
			cmbMarka.Text = oku["urunmarka"].ToString();
			cmbKategori.Text = oku["kategorisi"].ToString();
			txtAlisfiyat.Text = oku["alisfiyat"].ToString();
			txtSatisfiyat.Text = oku["satisfiyat"].ToString();
			txtAdet.Text = oku["adet"].ToString();
			txtDetay.Text = oku["detay"].ToString();
		}

        private void btnbul_Click(object sender, EventArgs e)
        {
			komut = @"Select * from urunkayit where urunid='" + txtid.Text + "'";
			DataTable list = new DataTable();
			NpgsqlDataAdapter da = new NpgsqlDataAdapter(komut, conn.connection());
			da.Fill(list);
			gridControl1.DataSource=list;
        }
    }
}
