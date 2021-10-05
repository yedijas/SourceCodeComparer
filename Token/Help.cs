using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace Token
{
    public partial class Help : Form
    {
        String[] text = new String[6];
        Image[] gambar = new Image[6];
        int page = 1;
        public Help()
        {
            InitializeComponent();
            
            gambar[0] = Image.FromFile("gambar\\gbr1.png");
            gambar[1] = Image.FromFile("gambar\\gbr2.png");
            gambar[2] = Image.FromFile("gambar\\gbr3.png");
            gambar[3] = Image.FromFile("gambar\\gbr4.png");
            gambar[4] = Image.FromFile("gambar\\gbr5.png");
            gambar[5] = Image.FromFile("gambar\\gbr6.png");
            text[0] = "Klik pada textbox untuk memilih folder";
            text[1] = "Setelah memilih folder, silahkan klik ok";
            text[2] = "Klik tombol 'Pilih Folder' untuk mendaftarkan semua file \n"+" yang ada di folder ke combo box";
            text[3] = "Pilih nama file yang akan dideteksi pada combo box";
            text[4] = "Klik tombol 'Cek Kesamaan' untuk mengetahui tingkat kemiripan\n"+" file yang telah dipilih pada combo box";
            text[5] = "Jika ingin melihat Token, silahkan klik tombol 'Tampil Code'";
            lblText.Text = text[0];
            gbr.BackgroundImage = gambar[0];
            lblHal.Text = "Halaman " + page;
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (page <= 1)
            {
                MessageBox.Show("Anda sudah di halaman pertama");
            }
            else
            {
                page--;
                gbr.BackgroundImage = gambar[page - 1];
                lblText.Text = text[page - 1];
                lblHal.Text = "Halaman " + page;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (page >= 6)
            {
                MessageBox.Show("Anda sudah di halaman terakhir");
            }
            else
            {
                page++;
                gbr.BackgroundImage = gambar[page - 1];
                lblText.Text = text[page - 1];
                lblHal.Text = "Halaman " + page;

            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
