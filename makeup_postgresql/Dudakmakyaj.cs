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
    public partial class Dudakmakyaj : Form
    {
        public Dudakmakyaj()
        {
            InitializeComponent();
        }
        baglanti conn = new baglanti();
        private string komut;
        NpgsqlCommand cmd;

        void getir()
        {
            komut = @"Select * from dudakmakyaj";
            DataTable list = new DataTable();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(komut, conn.connection());
            da.Fill(list);
            gridControl1.DataSource = list;
        }
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            komut = @"update dudakmakyaj set renk=@p1 , tur=@p2 where dudakid=@p3";
            cmd = new NpgsqlCommand(komut, conn.connection());
            cmd.Parameters.AddWithValue("@p1", txtrenk.Text);
            cmd.Parameters.AddWithValue("@p2", txttur.Text);
            cmd.Parameters.AddWithValue("@p3", int.Parse(txtid.Text));
            cmd.ExecuteNonQuery();
            conn.connection().Close();
            MessageBox.Show("Makyaj kaydı güncellendi!!");
            getir();
        }

        private void Dudakmakyaj_Load(object sender, EventArgs e)
        {
            getir();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            komut = @"delete from dudakmakyaj where dudakid=@p1";
            NpgsqlCommand cmd = new NpgsqlCommand(komut, conn.connection());
            cmd.Parameters.AddWithValue("@p1", int.Parse(txtid.Text));
            cmd.ExecuteNonQuery();
            conn.connection().Close();
            MessageBox.Show("Makyaj kaydı silindi!!!");
            getir();
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            System.Data.DataRow oku = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            txtid.Text = oku["dudakid"].ToString();
            txtAd.Text = oku["urunad"].ToString();
            txtMarka.Text = oku["marka"].ToString();
            txtrenk.Text = oku["renk"].ToString();
            txttur.Text = oku["tur"].ToString();
        }

        private void btnbul_Click(object sender, EventArgs e)
        {
            komut = @"Select * from dudakmakyaj where dudakid='" + txtid.Text + "'";
            DataTable list = new DataTable();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(komut, conn.connection());
            da.Fill(list);
            gridControl1.DataSource = list;
        }
    }
}
