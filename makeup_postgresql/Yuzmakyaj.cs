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
    public partial class Yuzmakyaj : Form
    {
        public Yuzmakyaj()
        {
            InitializeComponent();
        }
        baglanti conn = new baglanti();
        private string komut;
        NpgsqlCommand cmd;

        void getir()
        {
            komut = @"Select * from yuzmakyaj";
            DataTable list = new DataTable();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(komut, conn.connection());
            da.Fill(list);
            gridControl1.DataSource = list;
        }
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            komut = @"update yuzmakyaj set renk=@p1 , tur=@p2 where yuzid=@p3";
            cmd = new NpgsqlCommand(komut, conn.connection());
            cmd.Parameters.AddWithValue("@p1", txtrenk.Text);
            cmd.Parameters.AddWithValue("@p2", txttur.Text);
            cmd.Parameters.AddWithValue("@p3", int.Parse(txtid.Text));
            cmd.ExecuteNonQuery();
            conn.connection().Close();
            MessageBox.Show("Makyaj kaydı güncellendi!!");
            getir();

        }

        private void Yuzmakyaj_Load(object sender, EventArgs e)
        {
            getir();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            komut = @"delete from yuzmakyaj where yuzid=@p1";
            NpgsqlCommand cmd = new NpgsqlCommand(komut, conn.connection());
            cmd.Parameters.AddWithValue("@p1", int.Parse(txtid.Text));
            cmd.ExecuteNonQuery();
            conn.connection().Close();
            MessageBox.Show("Makyaj kaydı silindi!!!");
            getir();
        }

        private void btnbul_Click(object sender, EventArgs e)
        {
            komut = @"Select * from dudakmakyaj where yuzid='" + txtid.Text + "'";
            DataTable list = new DataTable();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(komut, conn.connection());
            da.Fill(list);
            gridControl1.DataSource = list;
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            System.Data.DataRow oku = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            txtid.Text = oku["yuzid"].ToString();
            txtAd.Text = oku["urunad"].ToString();
            txtMarka.Text = oku["marka"].ToString();
            txtrenk.Text = oku["renk"].ToString();
            txttur.Text = oku["tur"].ToString();
        }
    }
}
