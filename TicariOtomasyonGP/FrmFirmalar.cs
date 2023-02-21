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

namespace TicariOtomasyonGP
{
    public partial class FrmFirmalar : Form
    {
        public FrmFirmalar()
        {
            InitializeComponent();
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void firmalistesi()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_FIRMALAR", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void sehirlistesi()
        {
            SqlCommand komut = new SqlCommand("Select SEHIR from TBL_ILLER", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbil.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        //void carikodaciklamalar()
        //{
        //    SqlCommand komut = new SqlCommand("Select FIRMAKOD1 from TBL_KODLAR", bgl.baglanti());
        //    SqlDataReader dr = komut.ExecuteReader();
        //    while (dr.Read())
        //    {
        //        richkod1.Text = dr[0].ToString();
        //    }
        //    bgl.baglanti().Close();
        //}

        void temizle()
        {
            txtad.Text = "";
            txtid.Text = "";
            //txtkod1.Text = "";
            //txtkod2.Text = "";
            //txtkod3.Text = "";
            Txtmail.Text = "";
            txtsektor.Text = "";
            txtvergid.Text = "";
            txtyetkili.Text = "";
            txtyetkiligorev.Text = "";
            MaskTelefon1.Text = "";
            MaskTelefon2.Text = "";
            MaskTelefon3.Text = "";
            MaskFax.Text = "";
            MaskyetkiliTC.Text = "";
            RichAdres.Text = "";
            txtad.Focus();
        }

        private void FrmFirmalar_Load(object sender, EventArgs e)
        {
            firmalistesi();

            sehirlistesi();

            //carikodaciklamalar();

            temizle();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtid.Text = dr["ID"].ToString();
                txtad.Text = dr["AD"].ToString();
                txtyetkiligorev.Text = dr["YETKILISTATU"].ToString();
                txtyetkili.Text = dr["YETKILIADSOYAD"].ToString();
                MaskyetkiliTC.Text = dr["YETKILITC"].ToString();
                txtsektor.Text = dr["SEKTOR"].ToString();
                MaskTelefon1.Text = dr["TELEFON1"].ToString();
                MaskTelefon2.Text = dr["TELEFON2"].ToString();
                MaskTelefon3.Text = dr["TELEFON3"].ToString();
                Txtmail.Text = dr["MAIL"].ToString();
                MaskFax.Text = dr["FAX"].ToString();
                cmbil.Text = dr["IL"].ToString();
                cmbilce.Text = dr["ILCE"].ToString();
                txtvergid.Text = dr["VERGIDAIRE"].ToString();
                RichAdres.Text = dr["ADRES"].ToString();
                //txtkod1.Text = dr["OZELKOD1"].ToString();
                //txtkod2.Text = dr["OZELKOD2"].ToString();
                //txtkod3.Text = dr["OZELKOD3"].ToString();
            }
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TBL_FIRMALAR (AD,YETKILISTATU,YETKILIADSOYAD,YETKILITC,SEKTOR,TELEFON1,TELEFON2,TELEFON3" +
                ",MAIL,FAX,IL,ILCE,VERGIDAIRE,ADRES) VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11,@P12,@P13,@P14)",bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", txtad.Text);
            komut.Parameters.AddWithValue("@P2", txtyetkiligorev.Text);
            komut.Parameters.AddWithValue("@P3", txtyetkili.Text);
            komut.Parameters.AddWithValue("@P4", MaskyetkiliTC.Text);
            komut.Parameters.AddWithValue("@P5", txtsektor.Text);
            komut.Parameters.AddWithValue("@P6", MaskTelefon1.Text);
            komut.Parameters.AddWithValue("@P7", MaskTelefon2.Text);
            komut.Parameters.AddWithValue("@P8", MaskTelefon3.Text);
            komut.Parameters.AddWithValue("@P9", Txtmail.Text);
            komut.Parameters.AddWithValue("@P10", MaskFax.Text);
            komut.Parameters.AddWithValue("@P11", cmbil.Text);
            komut.Parameters.AddWithValue("@P12", cmbilce.Text);
            komut.Parameters.AddWithValue("@P13", txtvergid.Text);
            komut.Parameters.AddWithValue("@P14", RichAdres.Text);
            //komut.Parameters.AddWithValue("@P15", richkod1.Text);
            //komut.Parameters.AddWithValue("@P16", richkod2.Text);
            //komut.Parameters.AddWithValue("@P17", richkod3.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Firma Sisteme Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            firmalistesi();
            temizle();

        }

        private void cmbil_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbilce.Properties.Items.Clear();
            SqlCommand komut = new SqlCommand("Select ILCE from TBL_ILCELER where SEHIR=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", cmbil.SelectedIndex + 1);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbilce.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Delete from TBL_FIRMALAR where ID=@P1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            firmalistesi();
            MessageBox.Show("Firma listeden silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Update TBL_FIRMALAR set AD=@P1,YETKILISTATU=@P2,YETKILIADSOYAD=@P3,YETKILITC=@P4,SEKTOR=@P5,TELEFON1=@P6,TELEFON2=@P7,TELEFON3=@P8," +
                "MAIL=@P9,IL=@P10,ILCE=@P11,FAX=@P12,VERGIDAIRE=@P13,ADRES=@P14 WHERE ID=@P18", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", txtad.Text);
            komut.Parameters.AddWithValue("@P2", txtyetkiligorev.Text);
            komut.Parameters.AddWithValue("@P3", txtyetkili.Text);
            komut.Parameters.AddWithValue("@P4", MaskyetkiliTC.Text);
            komut.Parameters.AddWithValue("@P5", txtsektor.Text);
            komut.Parameters.AddWithValue("@P6", MaskTelefon1.Text);
            komut.Parameters.AddWithValue("@P7", MaskTelefon2.Text);
            komut.Parameters.AddWithValue("@P8", MaskTelefon3.Text);
            komut.Parameters.AddWithValue("@P9", Txtmail.Text);
            komut.Parameters.AddWithValue("@P12", MaskFax.Text);
            komut.Parameters.AddWithValue("@P10", cmbil.Text);
            komut.Parameters.AddWithValue("@P11", cmbilce.Text);
            komut.Parameters.AddWithValue("@P13", txtvergid.Text);
            komut.Parameters.AddWithValue("@P14", RichAdres.Text);
            //komut.Parameters.AddWithValue("@P15", richkod1.Text);
            //komut.Parameters.AddWithValue("@P16", richkod2.Text);
            //komut.Parameters.AddWithValue("@P17", richkod3.Text);
            komut.Parameters.AddWithValue("@P18", txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Firma Bilgileri Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            firmalistesi();
            temizle();
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            gridControl1.ShowPrintPreview();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            gridControl1.ExportToPdf(@"D:\Görkem Paşaoğlu Otomasyon\Belgeler\Firma.pdf");
            MessageBox.Show("Dosyanız PDF biçiminde kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            gridControl1.ExportToXls(@"D:\Görkem Paşaoğlu Otomasyon\Belgeler\Firma.xls");
            MessageBox.Show("Dosyanız XLS biçiminde kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
