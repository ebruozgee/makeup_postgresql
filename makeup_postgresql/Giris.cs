using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Npgsql;

namespace makeup_postgresql
{
    public partial class Giris : DevExpress.XtraEditors.XtraForm
    {
        public Giris()
        {
            InitializeComponent();
        }
        baglanti conn = new baglanti();
        private string komut;
        NpgsqlCommand cmd;
        private void Giris_Load(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            komut = @"Select * from giris where kulad=@p1 and sifre=@p2";
            cmd = new NpgsqlCommand(komut, conn.connection());
            cmd.Parameters.AddWithValue("@p1", txtkulad.Text);
            cmd.Parameters.AddWithValue("@p2", txtsifre.Text);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                Anasayfa an = new Anasayfa();
                an.Show();
                this.Hide();
            }
            else
            {
                label3.Visible = true;
                label3.Text = "Kullanıcı adı veya şifre yanlış!";
            }
            conn.connection().Close();
        }
    }
}