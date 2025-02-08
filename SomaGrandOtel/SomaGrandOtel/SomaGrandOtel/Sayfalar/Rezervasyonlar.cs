using System;
using System.Data;
using System.Windows.Forms;
using SomaGrandOtel.DAL;
using System.Threading.Tasks;
using SomaGrandOtel.BL;
using SomaGrandOtel.Sayfalar;

namespace SomaGrandOtel.Sayfalar
{
    public partial class Rezervasyonlar : Form
    {

        private RezervasyonService rezervasyonService = new RezervasyonService();
        private MusteriService musteriService = new MusteriService();
        private OdaService odaService = new OdaService();
        public Rezervasyonlar()
        {
            InitializeComponent();

            dgvRezervasyon.DataSource = rezervasyonService.RezervasyonGetir();
            dgvMusteri.DataSource = musteriService.MüşterileriGetir();
            dgvOda.DataSource = odaService.OdalariGetir();

            dgvMusteri.CellClick += dgvMusteri_CellContentClick;
            dgvOda.CellClick += dgvOda_CellContentClick;
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

        private void dgvMusteri_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
                if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvMusteri.Rows[e.RowIndex];

                txtIdMusteri.Text = row.Cells["MusteriID"].Value.ToString();
            }
        }

        private void dgvOda_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
                if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvOda.Rows[e.RowIndex];

                txtIdOda.Text = row.Cells["OdaID"].Value.ToString();
            }
        }

            private void dgvRezervasyon_CellContentClick(object sender, DataGridViewCellEventArgs e)
            {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvRezervasyon.Rows[e.RowIndex];

                txtIdRezervasyon.Text = row.Cells["RezervasyonID"].Value?.ToString();
                txtIdMusteri.Text = row.Cells["MusteriID"].Value?.ToString();
                txtIdOda.Text = row.Cells["OdaID"].Value?.ToString();
                dtGiris.Value = Convert.ToDateTime(row.Cells["RzvGirisTarihi"].Value);
                dtCikis.Value = Convert.ToDateTime(row.Cells["RzvCikisTarihi"].Value);
                txtToplamTutar.Text = row.Cells["RzvToplamTutar"].Value?.ToString();
            }
        }

            private void btnEkle_Click(object sender, EventArgs e)
            {
            try
            {
                // Yeni rezervasyon nesnesi oluştur
                Entity.Rezervasyon yeniRezervasyon = new Entity.Rezervasyon
                {
                    MusteriID = Convert.ToInt32(txtIdMusteri.Text),
                    OdaID = Convert.ToInt32(txtIdOda.Text),
                    RzvGirisTarihi = dtGiris.Value,
                    RzvCikisTarihi = dtCikis.Value
                };

                // Rezervasyonu ekle
                if (rezervasyonService.RezervasyonEkle(yeniRezervasyon))
                {
                    MessageBox.Show("Rezervasyon başarıyla eklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Toplam tutarı TextBox'a yazdır
                    txtToplamTutar.Text = yeniRezervasyon.RzvToplamTutar.ToString("C2");

                    // DataGridView'i yenile
                    dgvRezervasyon.DataSource = rezervasyonService.RezervasyonGetir();

                    // Fatura oluştur ve göster
                    string fatura = rezervasyonService.FaturaOlustur(yeniRezervasyon);
                    MessageBox.Show(fatura, "Fatura", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

            private void btnGuncelle_Click(object sender, EventArgs e)
            {
            try
            {
                // Seçili rezervasyon ID'sini al
                if (string.IsNullOrEmpty(txtIdRezervasyon.Text))
                {
                    MessageBox.Show("Lütfen bir rezervasyon seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int rezervasyonID = Convert.ToInt32(txtIdRezervasyon.Text);

                // Güncellenecek rezervasyon nesnesini oluştur
                Entity.Rezervasyon guncelRezervasyon = new Entity.Rezervasyon
                {
                    RezervasyonID = rezervasyonID,
                    MusteriID = Convert.ToInt32(txtIdMusteri.Text),
                    OdaID = Convert.ToInt32(txtIdOda.Text),
                    RzvGirisTarihi = dtGiris.Value,
                    RzvCikisTarihi = dtCikis.Value,
                    RzvToplamTutar = Convert.ToDecimal(txtToplamTutar.Text)
                };

                // Rezervasyonu güncelle
                if (rezervasyonService.RezervasyonGüncelle(guncelRezervasyon))
                {
                    MessageBox.Show("Rezervasyon başarıyla güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // DataGridView'i yenile
                    dgvRezervasyon.DataSource = rezervasyonService.RezervasyonGetir();
                }
                else
                {
                    MessageBox.Show("Rezervasyon güncellenirken bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                // Seçili rezervasyon ID'sini al
                if (string.IsNullOrEmpty(txtIdRezervasyon.Text))
                {
                    MessageBox.Show("Lütfen bir rezervasyon seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // rezervasyonID değişkeni burada tanımlanır
                int rezervasyonID = Convert.ToInt32(txtIdRezervasyon.Text);

                // Silme işlemini gerçekleştir
                if (rezervasyonService.RezervasyonSil(rezervasyonID)) // Parametreyi doğru şekilde geçir
                {
                    MessageBox.Show("Rezervasyon başarıyla silindi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // DataGridView'i yenile
                    dgvRezervasyon.DataSource = rezervasyonService.RezervasyonGetir();

                    // TextBox'ları temizle
                    Temizle();
                }
                else
                {
                    MessageBox.Show("Rezervasyon silinirken bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

            private void Temizle()
        {
            txtIdRezervasyon.Clear();
            txtIdMusteri.Clear();
            txtIdOda.Clear();
            dtGiris.Value = DateTime.Now;
            dtCikis.Value = DateTime.Now;
            txtToplamTutar.Clear();
        }
    }
    }
