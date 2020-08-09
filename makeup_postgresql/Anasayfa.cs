using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace makeup_postgresql
{
    public partial class Anasayfa : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Anasayfa()
        {
            InitializeComponent();
        }
        urunkayit ur;
        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (ur == null || ur.IsDisposed)
            {
                ur = new urunkayit();
                ur.MdiParent = this;
                ur.Show();
            }
        }
        Personel pr;
        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (pr == null || pr.IsDisposed)
            {
                pr = new Personel();
                pr.MdiParent = this;
                pr.Show();
            }
        }
        Müsteri mu;
        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (mu == null || mu.IsDisposed)
            {
                mu = new Müsteri();
                mu.MdiParent = this;
                mu.Show();
            }
        }
        Stok st;
        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (st == null || st.IsDisposed)
            {
                st = new Stok();
                st.MdiParent = this;
                st.Show();
            }
        }
        Dudakmakyaj dm;
        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (dm == null || dm.IsDisposed)
            {
                dm = new Dudakmakyaj();
                dm.MdiParent = this;
                dm.Show();
            }
        }
        Yuzmakyaj ym;
        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (ym == null || ym.IsDisposed)
            {
                ym = new Yuzmakyaj();
                ym.MdiParent = this;
                ym.Show();
            }
        }
        Ciltbakim cm;
        private void barButtonItem7_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (cm == null || cm.IsDisposed)
            {
                cm = new Ciltbakim();
                cm.MdiParent = this;
                cm.Show();
            }
        }
        Marka mr;
        private void barButtonItem8_ItemClick(object sender, ItemClickEventArgs e)
        {

            if (mr == null || mr.IsDisposed)
            {
                mr = new Marka();
                mr.MdiParent = this;
                mr.Show();
            }
        }
        Sepet sp;
        private void barButtonItem9_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (sp == null || sp.IsDisposed)
            {
                sp = new Sepet();
                sp.MdiParent = this;
                sp.Show();
            }
        }
        Kasa ks;
        private void barButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (ks == null || ks.IsDisposed)
            {
                ks = new Kasa();
                ks.MdiParent = this;
                ks.Show();
            }
        }
    }
}