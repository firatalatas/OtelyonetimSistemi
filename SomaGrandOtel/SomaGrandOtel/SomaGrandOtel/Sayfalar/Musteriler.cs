using System;
using System.Data;
using System.Windows.Forms;
using SomaGrandOtel.DAL;
using System.Threading.Tasks;
using SomaGrandOtel.BL;
using SomaGrandOtel.Sayfalar;
using SomaGrandOtel.Entity; 

namespace SomaGrandOtel.Sayfalar
{
    public partial class Musteriler : Form
    {
        private MusteriService musteriService = new MusteriService();

        public Musteriler()
        {
            InitializeComponent();
            dgvMusteri.DataSource = musteriService.MüşterileriGetir();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtAd.Text) || string.IsNullOrEmpty(txtSoyad.Text) ||
                    string.IsNullOrEmpty(txtTel.Text) || string.IsNullOrEmpty(txtEposta.Text))
                {
                    MessageBox.Show("Lütfen tüm alanları doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Musteri yeniMusteri = new Musteri
                {
                    MusteriAd = txtAd.Text,
                    MusteriSoyad = txtSoyad.Text,
                    MusteriTel = txtTel.Text,
                    MusteriEposta = txtEposta.Text
                };

                if (musteriService.MüşteriEkle(yeniMusteri))
                {
                    MessageBox.Show("Müşteri başarıyla eklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvMusteri.DataSource = musteriService.MüşterileriGetir(); // DataGridView'i yenile
                    Temizle();
                }
                else
                {
                    MessageBox.Show("Müşteri eklenirken bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMusteriID.Text) || string.IsNullOrEmpty(txtAd.Text) ||
                    string.IsNullOrEmpty(txtSoyad.Text) || string.IsNullOrEmpty(txtTel.Text) ||
                    string.IsNullOrEmpty(txtEposta.Text))
                {
                    MessageBox.Show("Lütfen tüm alanları doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int musteriID = Convert.ToInt32(txtMusteriID.Text);

                Musteri guncellenenMusteri = new Musteri
                {
                    MusteriID = musteriID,
                    MusteriAd = txtAd.Text,
                    MusteriSoyad = txtSoyad.Text,
                    MusteriTel = txtTel.Text,
                    MusteriEposta = txtEposta.Text
                };

                if (musteriService.MüşteriGüncelle(guncellenenMusteri))
                {
                    MessageBox.Show("Müşteri başarıyla güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvMusteri.DataSource = musteriService.MüşterileriGetir(); // DataGridView'i yenile
                    Temizle();
                }
                else
                {
                    MessageBox.Show("Müşteri güncellenirken bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMusteriID.Text))
                {
                    MessageBox.Show("Lütfen bir müşteri seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int musteriID = Convert.ToInt32(txtMusteriID.Text);

                if (musteriService.MüşteriSil(musteriID))
                {
                    MessageBox.Show("Müşteri başarıyla silindi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvMusteri.DataSource = musteriService.MüşterileriGetir(); // DataGridView'i yenile
                    Temizle();
                }
                else
                {
                    MessageBox.Show("Müşteri silinirken bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void geri_Click(object sender, EventArgs e)
        {
            for (int i = 100; i >= 0; i -= 5)
            {
                this.Opacity = i / 100.0;
                await Task.Delay(10);
            }

            Anasayfa anaSayfa = new Anasayfa();
            anaSayfa.Show();
            this.Close();
        }

        private async void kapat_Click(object sender, EventArgs e)
        {
            for (int i = 100; i >= 0; i -= 5)
            {
                this.Opacity = i / 100.0;
                await Task.Delay(10);
            }
            Application.Exit();
        }

        private async void indir_Click(object sender, EventArgs e)
        {
            for (int i = 100; i >= 0; i -= 5)
            {
                this.Opacity = i / 100.0;
                await Task.Delay(10);
            }

            this.WindowState = FormWindowState.Minimized;

            this.Opacity = 1.0;
        }

        private void dgvMusteri_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Geçerli bir satır seçildi mi kontrol et
            {
                DataGridViewRow row = dgvMusteri.Rows[e.RowIndex];
                txtMusteriID.Text = row.Cells["MusteriID"].Value?.ToString(); // Müşteri ID'si
                txtAd.Text = row.Cells["musteri_Ad"].Value?.ToString();               // Müşteri Adı
                txtSoyad.Text = row.Cells["musteri_Soyad"].Value?.ToString();         // Müşteri Soyadı
                txtTel.Text = row.Cells["musteri_Tel"].Value?.ToString();     // Müşteri Telefonu
                txtEposta.Text = row.Cells["musteri_Eposta"].Value?.ToString();         // Müşteri E-posta
            }
        }

        private void Temizle()
        {
            txtMusteriID.Clear();
            txtAd.Clear();
            txtSoyad.Clear();
            txtTel.Clear();
            txtEposta.Clear();
        }
    }
}