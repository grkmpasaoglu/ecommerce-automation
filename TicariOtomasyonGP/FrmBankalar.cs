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
using DevExpress.Data.Linq.Helpers;

namespace TicariOtomasyonGP
{
    public partial class FrmBankalar : Form
    {
        public FrmBankalar()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Execute BankalarBilgi", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        private void FrmBankalar_Load(object sender, EventArgs e)
        {
            listele();
            sehirlistesi();
            firmalistesi();
            temizle();
        }
        void temizle()
        {
            txtbankaadi.Text = "";
            txthesaptur.Text = "";
            txthesapno.Text = "";
            txtiban.Text = "";
            txtid.Text = "";
            txtyetkili.Text = "";
            masktarih.Text = "";
            MaskTelefon.Text = "";
            lookupfirma.Text = "";
            cmbil.Text = "";
            cmbilce.Text = "";
            Txtsube.Text = "";

        }
        void firmalistesi()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select ID,AD from TBL_FIRMALAR", bgl.baglanti());
            da.Fill(dt);
            lookupfirma.Properties.ValueMember = "ID";
            lookupfirma.Properties.DisplayMember = "AD";
            lookupfirma.Properties.DataSource = dt;
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


        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TBL_BANKALAR (BANKAADI,IL,ILCE,SUBE,IBAN,HESAPNO,YETKILI,TELEFON,TARIH,HESAPTURU,FIRMAID) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtbankaadi.Text);
            komut.Parameters.AddWithValue("@p2", cmbil.Text);
            komut.Parameters.AddWithValue("@p3", cmbilce.Text);
            komut.Parameters.AddWithValue("@p4", Txtsube.Text);
            komut.Parameters.AddWithValue("@p5", txtiban.Text);
            komut.Parameters.AddWithValue("@p6", txthesapno.Text);
            komut.Parameters.AddWithValue("@p7", txtyetkili.Text);
            komut.Parameters.AddWithValue("@p8", MaskTelefon.Text);
            komut.Parameters.AddWithValue("@p9", masktarih.Text);
            komut.Parameters.AddWithValue("@p10", txthesaptur.Text);
            komut.Parameters.AddWithValue("@p11", lookupfirma.EditValue);
            komut.ExecuteNonQuery();
            listele();
            MessageBox.Show("Banka Bilgisi Sisteme Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtid.Text = dr["ID"].ToString();
                txtbankaadi.Text = dr["BANKAADI"].ToString();
                cmbil.Text = dr["IL"].ToString();
                cmbilce.Text = dr["ILCE"].ToString();
                Txtsube.Text = dr["SUBE"].ToString();
                txtiban.Text = dr["IBAN"].ToString();
                txthesapno.Text = dr["HESAPNO"].ToString();
                txtyetkili.Text = dr["YETKILI"].ToString();
                MaskTelefon.Text = dr["TELEFON"].ToString();
                masktarih.Text = dr["TARIH"].ToString();
                txthesaptur.Text = dr["HESAPTURU"].ToString();
            }
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from TBL_BANKALAR where ID=@P1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            temizle();
            MessageBox.Show("Banka Bilgileri Sistemden Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            listele();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TBL_BANKALAR set BANKAADI=@P1,IL=@P2,ILCE=@P3,SUBE=@P4,IBAN=@P5,HESAPNO=@P6,YETKILI=@P7,TELEFON=@P8,TARIH=@P9,HESAPTURU=@P10,FIRMAID=@P11 WHERE ID=@P12", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtbankaadi.Text);
            komut.Parameters.AddWithValue("@p2", cmbil.Text);
            komut.Parameters.AddWithValue("@p3", cmbilce.Text);
            komut.Parameters.AddWithValue("@p4", Txtsube.Text);
            komut.Parameters.AddWithValue("@p5", txtiban.Text);
            komut.Parameters.AddWithValue("@p6", txthesapno.Text);
            komut.Parameters.AddWithValue("@p7", txtyetkili.Text);
            komut.Parameters.AddWithValue("@p8", MaskTelefon.Text);
            komut.Parameters.AddWithValue("@p9", masktarih.Text);
            komut.Parameters.AddWithValue("@p10", txthesaptur.Text);
            komut.Parameters.AddWithValue("@p11", lookupfirma.EditValue);
            komut.Parameters.AddWithValue("@p12", txtid.Text);
            komut.ExecuteNonQuery();
            listele();
            MessageBox.Show("Banka Bilgisi Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            gridControl1.ShowPrintPreview();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            gridControl1.ExportToPdf(@"D:\Görkem Paşaoğlu Otomasyon\Belgeler\Bankalar.pdf");
            MessageBox.Show("Dosyanız PDF biçiminde kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            gridControl1.ExportToXls(@"D:\Görkem Paşaoğlu Otomasyon\Belgeler\Bankalar.xls");
            MessageBox.Show("Dosyanız XLS biçiminde kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
