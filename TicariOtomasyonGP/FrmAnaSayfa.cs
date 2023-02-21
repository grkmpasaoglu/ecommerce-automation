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
    public partial class FrmAnaSayfa : Form
    {
        public FrmAnaSayfa()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void stoklar()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select Urunad,Sum(Adet) as 'Adet' from TBL_URUNLER group by Urunad having sum(adet)<=20 order by sum(adet)", bgl.baglanti());
            da.Fill(dt);
            gridcontrolstoklar.DataSource = dt;
        }

        void fihrist()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select Ad,Telefon1 from TBL_FIRMALAR", bgl.baglanti());
            da.Fill(dt);
            gridcontrolencokfirma.DataSource = dt;
        }

        private void FrmAnaSayfa_Load(object sender, EventArgs e)
        {
            stoklar();
            fihrist();
            webBrowser1.Navigate("https://www.tcmb.gov.tr/kurlar/today.xml");
        }
    }
}
