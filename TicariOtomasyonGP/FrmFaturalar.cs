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
using DevExpress.XtraEditors.Mask;

namespace TicariOtomasyonGP
{
    public partial class FrmFaturalar : Form
    {
        public FrmFaturalar()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_FATURABILGI", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        private void FrmFaturalar_Load(object sender, EventArgs e)
        {
            listele();
            temizle();
        }

        void temizle()
        {
            txtalici.Text = "";
            txtid.Text = "";
            txtserino.Text = "";
            txtsirano.Text = "";
            txtalici.Text = "";
            txtalan.Text = "";
            txteden.Text = "";
            txtvergid.Text = "";
            masksaat.Text = "";
            masktarih.Text = "";
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            if (txtfaturaid.Text == "")
            {
                SqlCommand komut = new SqlCommand("insert into TBL_FATURABILGI (SERI,SIRANO,TARIH,SAAT,VERGIDAIRE,ALICI,TESLIMEDEN,TESLIMALAN) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", txtserino.Text);
                komut.Parameters.AddWithValue("@p2", txtsirano.Text);
                komut.Parameters.AddWithValue("@p3", masktarih.Text);
                komut.Parameters.AddWithValue("@p4", masksaat.Text);
                komut.Parameters.AddWithValue("@p5", txtvergid.Text);
                komut.Parameters.AddWithValue("@p6", txtalici.Text);
                komut.Parameters.AddWithValue("@p7", txteden.Text);
                komut.Parameters.AddWithValue("@p8", txtalan.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Fatura Bilgisi Sisteme Kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
            if (txtfaturaid.Text != "")
            {
                double miktar, tutar, fiyat;
                fiyat = Convert.ToDouble(txtfiyat.Text);
                miktar = Convert.ToDouble(txtmiktar.Text);
                tutar = miktar * fiyat;
                txttutar.Text = tutar.ToString();
                SqlCommand komut2 = new SqlCommand("insert into TBL_FATURADETAY (URUNAD,MIKTAR,FIYAT,TUTAR,FATURAID) values (@p1,@p2,@p3,@p4,@p5)", bgl.baglanti());
                komut2.Parameters.AddWithValue("@p1", txturunad.Text);
                komut2.Parameters.AddWithValue("@p2", txtmiktar.Text);
                komut2.Parameters.AddWithValue("@p3", txtfiyat.Text);
                komut2.Parameters.AddWithValue("@p4", txttutar.Text);
                komut2.Parameters.AddWithValue("@p5", txtfaturaid.Text);
                komut2.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Faturaya Ait Ürün Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtid.Text = dr["FATURABILGIID"].ToString();
                txtsirano.Text = dr["SIRANO"].ToString();
                txtserino.Text = dr["SERI"].ToString();
                masktarih.Text = dr["TARIH"].ToString();
                masksaat.Text = dr["SAAT"].ToString();
                txtalici.Text = dr["ALICI"].ToString();
                txteden.Text = dr["TESLIMEDEN"].ToString();
                txtalan.Text = dr["TESLIMALAN"].ToString();
                txtvergid.Text = dr["VERGIDAIRE"].ToString();
            }
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Delete from TBL_FATURABILGI where FATURABILGIID=@P1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Fatura Silindi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TBL_FATURABILGI set SERI=@P1,SIRANO=@P2,TARIH=@P3,SAAT=@P4,VERGIDAIRE=@P5,ALICI=@P6,TESLIMEDEN=@P7,TESLIMALAN=@P8 where FATURABILGIID=@P9", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtserino.Text);
            komut.Parameters.AddWithValue("@p2", txtsirano.Text);
            komut.Parameters.AddWithValue("@p3", masktarih.Text);
            komut.Parameters.AddWithValue("@p4", masksaat.Text);
            komut.Parameters.AddWithValue("@p5", txtvergid.Text);
            komut.Parameters.AddWithValue("@p6", txtalici.Text);
            komut.Parameters.AddWithValue("@p7", txteden.Text);
            komut.Parameters.AddWithValue("@p8", txtalan.Text);
            komut.Parameters.AddWithValue("@p9", txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Fatura Bilgisi Sisteme Kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmFaturaUrunDetay fr = new FrmFaturaUrunDetay();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                fr.id = dr["FATURABILGIID"].ToString();
            }
            fr.Show();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            gridControl1.ShowPrintPreview();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            gridControl1.ExportToPdf(@"D:\Görkem Paşaoğlu Otomasyon\Belgeler\Fatura.pdf");
            MessageBox.Show("Dosyanız PDF biçiminde kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            gridControl1.ExportToXls(@"D:\Görkem Paşaoğlu Otomasyon\Belgeler\Fatura.xls");
            MessageBox.Show("Dosyanız XLS biçiminde kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            
        }

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            gridControl1.ExportToPdf(@"D:\Görkem Paşaoğlu Otomasyon\Belgeler\Fatura.pdf");
            MessageBox.Show("Dosyanız PDF biçiminde kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void simpleButton3_Click_1(object sender, EventArgs e)
        {
            gridControl1.ExportToXls(@"D:\Görkem Paşaoğlu Otomasyon\Belgeler\Fatura.xls");
            MessageBox.Show("Dosyanız XLS biçiminde kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
