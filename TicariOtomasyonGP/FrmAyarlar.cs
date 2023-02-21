using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicariOtomasyonGP
{
    public partial class FrmAyarlar : Form
    {
        public FrmAyarlar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_ADMIN", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TBL_ADMIN values (@p1,@p2)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtkullaniciadi.Text);
            komut.Parameters.AddWithValue("@p2", txtsifre.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Yeni Admin Sisteme Kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            if (simpleButton1.Text == "Güncelle")
            {
                SqlCommand komut1 = new SqlCommand("update TBL_ADMIN set sifre=@p2 where kullaniciad=@p1", bgl.baglanti());
                komut1.Parameters.AddWithValue("@p1", txtkullaniciadi.Text);
                komut1.Parameters.AddWithValue("@p2", txtsifre.Text);
                komut1.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Kayıt Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

        }

        private void FrmAyarlar_Load(object sender, EventArgs e)
        {
            listele();
            txtkullaniciadi.Text = "";
            txtsifre.Text = "";
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtkullaniciadi.Text = dr["KullaniciAd"].ToString();
                txtsifre.Text = dr["Sifre"].ToString();
            }
        }

        private void txtkullaniciadi_EditValueChanged(object sender, EventArgs e)
        {
            if (txtkullaniciadi.Text != "")
            {
                simpleButton1.Text = "Güncelle";
            }
            else
            {
                simpleButton1.Text = "Kaydet";
            }
        }
    }
}
