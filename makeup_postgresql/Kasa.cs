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
    public partial class Kasa : Form
    {
        public Kasa()
        {
            InitializeComponent();
        }
        baglanti conn = new baglanti();
        private string komut;
        NpgsqlCommand cmd;

        void getir()
        {
            komut = @"select * from satinalma_view";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(komut, conn.connection());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        private void Kasa_Load(object sender, EventArgs e)
        {
            getir();

            //Gelir:
            komut = @"select gelir()";
            cmd = new NpgsqlCommand(komut, conn.connection());
            NpgsqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblgelir.Text = dr[0].ToString();
            }
            //Marka:
            komut = @"select markatoplam()";
            cmd = new NpgsqlCommand(komut, conn.connection());
            NpgsqlDataReader dr1 = cmd.ExecuteReader();
            while (dr1.Read())
            {
                lblmarka.Text = dr1[0].ToString();
            }
            //Musteri:
            komut = @"select musteritoplam()";
            cmd = new NpgsqlCommand(komut, conn.connection());
            NpgsqlDataReader dr2 = cmd.ExecuteReader();
            while (dr2.Read())
            {
                lblmusteri.Text = dr2[0].ToString();
            }
            //Personel:
            komut = @"select personeltoplam()";
            cmd = new NpgsqlCommand(komut, conn.connection());
            NpgsqlDataReader dr3 = cmd.ExecuteReader();
            while (dr3.Read())
            {
                lblpersonel.Text = dr3[0].ToString();
            }
        }
    }
}
