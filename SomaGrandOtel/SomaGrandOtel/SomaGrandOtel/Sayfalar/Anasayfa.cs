using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomaGrandOtel.Sayfalar
{
    public partial class Anasayfa : Form
    {
        public Anasayfa()
        {
            InitializeComponent();
        }

        private async void btnMusteri_Click(object sender, EventArgs e)
        {
            Musteriler musteriForm = new Musteriler();
            for (int i = 100; i >= 0; i -= 5)
            {
                this.Opacity = i / 100.0;
                await Task.Delay(10);
            }
            musteriForm.Show();
            
            this.Hide(); // Mevcut formu gizle
        }

        private async void btnOda_Click(object sender, EventArgs e)
        {
            Odalar odaForm = new Odalar();
            for (int i = 100; i >= 0; i -= 5)
            {
                this.Opacity = i / 100.0;
                await Task.Delay(10);
            }
            odaForm.Show();
            
            this.Hide();
        }

        private async void btnRezervasyon_Click(object sender, EventArgs e)
        {
            Rezervasyonlar rezervasyonForm = new Rezervasyonlar();
            for (int i = 100; i >= 0; i -= 5)
            {
                this.Opacity = i / 100.0;
                await Task.Delay(10);
            }
            rezervasyonForm.Show();
            
            this.Hide();
        }

        private async void btnYonetici_Click(object sender, EventArgs e)
        {
            Yoneticiler yoneticiForm = new Yoneticiler();
            for (int i = 100; i >= 0; i -= 5)
            {
                this.Opacity = i / 100.0;
                await Task.Delay(10);
            }
            yoneticiForm.Show();
            
            this.Hide();
        }

        private async void btnCikis_Click(object sender, EventArgs e)
        {
            Giris girisForm = new Giris();
            for (int i = 100; i >= 0; i -= 5)
            {
                this.Opacity = i / 100.0;
                await Task.Delay(10);
            }
            girisForm.Show();
            
            this.Hide();
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
        }
    }
}
