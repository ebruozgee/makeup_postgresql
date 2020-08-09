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
    public partial class sepetekle : Form
    {
        public sepetekle()
        {
            InitializeComponent();
        }
        baglanti conn = new baglanti();
        private string komut;
        NpgsqlCommand cmd;
        public string urunid;
        void getir()
        {
            komut = @"select * from sepetekle where sepetid='"+urunid+"'";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(komut, conn.connection());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        private void sepetekle_Load(object sender, EventArgs e)
        {
            getir();
        }
    }
}
