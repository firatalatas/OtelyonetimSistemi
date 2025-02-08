using System;
using System.Data;
using System.Windows.Forms;
using SomaGrandOtel.DAL;
using System.Threading.Tasks;
using SomaGrandOtel.BL;
using SomaGrandOtel.Entity;
using SomaGrandOtel.Sayfalar;

namespace SomaGrandOtel.Sayfalar
{
    public partial class Odalar : Form
    {
        OdaService odaService = new OdaService();
        public Odalar()
        {
            InitializeComponent();

            dgvOda.DataSource = odaService.OdalariGetir();

            dgvOda.CellClick += dgvOda_CellContentClick;
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

        private async void kapat_Click(object sender, EventArgs e)
        {
            for (int i = 100; i >= 0; i -= 5)
            {
                this.Opacity = i / 100.0;
                await Task.Delay(10);
            }
            Application.Exit();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtOdaId.Text))
                {
                    MessageBox.Show("Lütfen bir oda seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Seçilen odanın ID'sini al
                int odaID = Convert.ToInt32(txtOdaId.Text);

                // Silme işlemini gerçekleştir
                if (odaService.OdaSil(odaID))
                {
                    MessageBox.Show("Oda başarıyla silindi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvOda.DataSource = odaService.OdalariGetir(); // DataGridView'i yenile
                    Temizle();
                }
                else
                {
                    MessageBox.Show("Oda silinirken bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (string.IsNullOrEmpty(txtOdaId.Text) || string.IsNullOrEmpty(txtNumara.Text) ||
                    string.IsNullOrEmpty(txtFiyat.Text) || string.IsNullOrEmpty(txtTip.Text))
                {
                    MessageBox.Show("Lütfen tüm alanları doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int odaID = Convert.ToInt32(txtOdaId.Text);

                Oda guncellenenOda = new Oda
                {
                    OdaID = odaID,
                    OdaNumara = Convert.ToInt32(txtNumara.Text),
                    OdaFiyat = Convert.ToDecimal(txtFiyat.Text),
                    OdaTipi = txtTip.Text,
                    OdaDurum = txtDurum.Text // ComboBox'dan alınan değer
                };

                if (odaService.OdaGüncelle(guncellenenOda))
                {
                    MessageBox.Show("Oda başarıyla güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvOda.DataSource = odaService.OdalariGetir();
                    Temizle();
                }
                else
                {
                    MessageBox.Show("Oda güncellenirken bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtNumara.Text) || string.IsNullOrEmpty(txtFiyat.Text) ||
                    string.IsNullOrEmpty(txtTip.Text))
                {
                    MessageBox.Show("Lütfen tüm alanları doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Oda yeniOda = new Oda
                {
                    OdaNumara = Convert.ToInt32(txtNumara.Text),
                    OdaFiyat = Convert.ToDecimal(txtFiyat.Text),
                    OdaTipi = txtTip.Text,
                    OdaDurum = "Boş" // Varsayılan değer
                };

                if (odaService.OdaEkle(yeniOda))
                {
                    MessageBox.Show("Oda başarıyla eklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvOda.DataSource = odaService.OdalariGetir();
                    Temizle();
                }
                else
                {
                    MessageBox.Show("Oda eklenirken bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvOda_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Geçerli bir satır seçildi mi kontrol et
            {
                DataGridViewRow row = dgvOda.Rows[e.RowIndex];

                // Kolon adlarını kontrol et ve TextBox'lara aktar
                txtOdaId.Text = row.Cells["OdaID"].Value?.ToString();           // Oda ID'si
                txtNumara.Text = row.Cells["oda_numara"].Value?.ToString();   // Oda Numarası
                txtFiyat.Text = row.Cells["oda_fiyat"].Value?.ToString();     // Oda Fiyatı
                txtTip.Text = row.Cells["oda_tipi"].Value?.ToString();        // Oda Tipi
                txtDurum.Text = row.Cells["oda_durum"].Value?.ToString();    // Oda Durumu
            }
        }

        private void Temizle()
        {
            txtOdaId.Clear();
            txtTip.Clear();
            txtNumara.Clear();
            txtFiyat.Clear();
            txtDurum.Clear();
        }

        private void Odalar_Load(object sender, EventArgs e)
        {
            try
            {
                dgvOda.DataSource = odaService.OdalariGetir(); // DataGridView'e veri yükle
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
