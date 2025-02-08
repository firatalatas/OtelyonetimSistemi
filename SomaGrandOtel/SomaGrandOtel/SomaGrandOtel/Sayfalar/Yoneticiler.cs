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
    public partial class Yoneticiler : Form
    {
        private YoneticiService yoneticiService = new YoneticiService();
        public Yoneticiler()
        {
            InitializeComponent();
            dgvYonetici.DataSource = yoneticiService.YöneticileriGetir();
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

        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtKimlik.Text) || string.IsNullOrEmpty(txtSifre.Text))
                {
                    MessageBox.Show("Lütfen tüm alanları doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Yonetici yeniYonetici = new Yonetici
                {
                    YoneticiTC = txtKimlik.Text,
                    YoneticiSifre = Convert.ToInt32(txtSifre.Text) // Şifreyi int'e dönüştür
                };

                if (yoneticiService.YöneticiEkle(yeniYonetici))
                {
                    MessageBox.Show("Yönetici başarıyla eklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvYonetici.DataSource = yoneticiService.YöneticileriGetir(); // DataGridView'i yenile
                    Temizle();
                }
                else
                {
                    MessageBox.Show("Yönetici eklenirken bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (string.IsNullOrEmpty(txtKimlik.Text) || string.IsNullOrEmpty(txtSifre.Text))
                {
                    MessageBox.Show("Lütfen tüm alanları doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Yonetici guncellenenYonetici = new Yonetici
                {
                    YoneticiTC = txtKimlik.Text,
                    YoneticiSifre = Convert.ToInt32(txtSifre.Text) 
                };

                if (yoneticiService.YöneticiGüncelle(guncellenenYonetici))
                {
                    MessageBox.Show("Yönetici başarıyla güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvYonetici.DataSource = yoneticiService.YöneticileriGetir(); 
                    Temizle();
                }
                else
                {
                    MessageBox.Show("Yönetici güncellenirken bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (string.IsNullOrEmpty(txtKimlik.Text))
                {
                    MessageBox.Show("Lütfen bir yönetici seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string yoneticiTC = txtKimlik.Text;

                if (yoneticiService.YöneticiSil(yoneticiTC))
                {
                    MessageBox.Show("Yönetici başarıyla silindi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvYonetici.DataSource = yoneticiService.YöneticileriGetir(); 
                    Temizle();
                }
                else
                {
                    MessageBox.Show("Yönetici silinirken bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvYonetici_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) 
            {
                DataGridViewRow row = dgvYonetici.Rows[e.RowIndex];
                txtKimlik.Text = row.Cells["YoneticiTC"].Value?.ToString(); 
                txtSifre.Text = row.Cells["YoneticiSifre"].Value?.ToString(); 
            }
        }

        private void Temizle()
        {
            txtId.Clear();
            txtKimlik.Clear();
            txtSifre.Clear();
        }
    }
}
