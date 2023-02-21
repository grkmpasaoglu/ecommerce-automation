using DevExpress.XtraBars.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicariOtomasyonGP
{
    public partial class FormAnaModul : Form
    {
        public FormAnaModul()
        {
            InitializeComponent();
        }

        FrmUrunler fr;
        private void BtnUrunler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
                fr = new FrmUrunler();
                fr.MdiParent = this;
                fr.Show();
            
        }

        FrmMusteriler fr2;
        private void BtnMusteriler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
                fr2 = new FrmMusteriler();
                fr2.MdiParent = this;
                fr2.Show();
            
        }


        FrmFirmalar fr3;
        private void BtnFirmalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
                fr3 = new FrmFirmalar();
                fr3.MdiParent = this;
                fr3.Show();
            
        }

        FrmPersonel fr4;
        private void BtnPersoneller_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
                fr4 = new FrmPersonel();
                fr4.MdiParent = this;
                fr4.Show();
            
        }

        FrmRehber fr5;
        private void BtnRehber_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
                fr5 = new FrmRehber();
                fr5.MdiParent = this;
                fr5.Show();
            
        }

        FrmGiderler fr6;
        private void BtnGiderler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

                fr6 = new FrmGiderler();
                fr6.MdiParent = this;
                fr6.Show();
            
        }

        FrmBankalar fr7;
        private void BtnBankalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
                fr7 = new FrmBankalar();
                fr7.MdiParent = this;
                fr7.Show();
            
        }

        private void FormAnaModul_Load(object sender, EventArgs e)
        {
            
                fr11 = new FrmAnaSayfa();
                fr11.MdiParent = this;
                fr11.Show();
            
        }

        FrmFaturalar fr8;
        private void BtnFaturalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
                fr8 = new FrmFaturalar();
                fr8.MdiParent = this;
                fr8.Show();
            
        }

        FrmStok fr9;
        private void BtnStoklar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
                fr9 = new FrmStok();
                fr9.MdiParent = this;
                fr9.Show();
            
        }

        FrmAyarlar fr10;
        private void BtnAyarlar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
                fr10 = new FrmAyarlar();
                fr10.Show();
            
        }

        FrmAnaSayfa fr11;
        private void BtnAnasayfa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
                fr11 = new FrmAnaSayfa();
                fr11.MdiParent = this;
                fr11.Show();
            
        }
    }
}
