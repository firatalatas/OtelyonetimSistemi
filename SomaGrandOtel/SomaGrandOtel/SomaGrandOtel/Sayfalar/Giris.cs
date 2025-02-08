using System;
using System.Data;
using System.Windows.Forms;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using SomaGrandOtel.BL;
using SomaGrandOtel.DAL;

namespace SomaGrandOtel.Sayfalar
{
    public partial class Giris : Form
    {
        public Giris()
        {
            InitializeComponent();
        }

        private void kapat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void indir_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private async void btnGiris_Click(object sender, EventArgs e)
        {
            string tc = txtKimlik.Text;
            string sifre = txtSifre.Text;

            if (string.IsNullOrEmpty(tc) || string.IsNullOrEmpty(sifre))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            dbBaglanti db = new dbBaglanti();
            MySqlConnection conn = db.BaglantiGetir();
            YoneticiService yoneticiService = new YoneticiService();
            try
            {
                var dt = yoneticiService.YöneticileriGetir();
                bool girisBasarili = false;

                foreach (DataRow row in dt.Rows)
                {
                    if (row["yonetici_tc"].ToString() == tc && row["yonetici_sifre"].ToString() == sifre)
                    {
                        girisBasarili = true;
                        break;
                    }
                }

                if (girisBasarili)
                {
                    Anasayfa anaSayfa = new Anasayfa();
                    for (int i = 100; i >= 0; i -= 5)
                    {
                        this.Opacity = i / 100.0;
                        await Task.Delay(10);
                    }
                    anaSayfa.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Kimlik veya şifre hatalı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                db.BaglantiKapat();
            }
        }

        private void girisyap_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
