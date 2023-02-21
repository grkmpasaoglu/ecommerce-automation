using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using DevExpress.XtraBars;

namespace TicariOtomasyonGP
{
    public partial class FrmStok : Form
    {
        public FrmStok()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();
        private void FrmStok_Load(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("Select UrunAd,Sum(Adet) as 'Miktar' from TBL_URUNLER group by UrunAd ", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;

            //Chartta stok miktarı listeleme
            SqlCommand komut = new SqlCommand("Select UrunAd,Sum(Adet) as 'Miktar' from TBL_URUNLER group by UrunAd ", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                chartControl1.Series["Series 1"].Points.AddPoint(Convert.ToString(dr[0]), int.Parse(dr[1].ToString()));
            }
            bgl.baglanti().Close();
            
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            gridControl1.ShowPrintPreview();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            gridControl1.ExportToPdf(@"D:\Görkem Paşaoğlu Otomasyon\Belgeler\Stok.pdf");
            MessageBox.Show("Dosyanız PDF biçiminde kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            gridControl1.ExportToXls(@"D:\Görkem Paşaoğlu Otomasyon\Belgeler\Stok.xls");
            MessageBox.Show("Dosyanız XLS biçiminde kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
