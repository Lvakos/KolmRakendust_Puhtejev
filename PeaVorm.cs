using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KolmRakendust_Puhtejev
{
    public partial class PeaVorm : Form
    {
        private PictureBox github;
        private Button nuppVike, nuppKeskmine, nuppSuur;

        public PeaVorm()
        {
            Text = "Peaaken - Valikud";
            Size = new Size(400, 300);
            StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = ColorTranslator.FromHtml("#121212");
            this.MaximizeBox = false;

            // 1. Nupp: PhotoMax
            nuppVike = new Button();
            nuppVike.Text = "PhotoMax (piltide vaatamine)";
            nuppVike.Location = new Point(50, 30);
            nuppVike.Size = new Size(280, 40);
            nuppVike.Click += NuppVike_Click;
            nuppVike.BackColor = Color.LightSeaGreen;
            nuppVike.ForeColor = Color.Black;

            // 2. Nupp: Matematiiline mäng
            nuppKeskmine = new Button();
            nuppKeskmine.Text = "Matematiiline mäng";
            nuppKeskmine.Location = new Point(50, 80);
            nuppKeskmine.Size = new Size(280, 40);
            nuppKeskmine.Click += NuppKeskmine_Click;
            nuppKeskmine.BackColor = Color.SeaShell;
            nuppKeskmine.ForeColor = Color.Black;

            // 3. Nupp: Piltide mäng
            nuppSuur = new Button();
            nuppSuur.Text = "Piltide mäng";
            nuppSuur.Location = new Point(50, 130);
            nuppSuur.Size = new Size(280, 40);
            nuppSuur.Click += nuppSuur_Click;
            nuppSuur.BackColor = Color.PaleVioletRed;
            nuppSuur.ForeColor = Color.Black;

            // 4. Pilt Nupp: GitHub
            github = new PictureBox();
            github.Image = Image.FromFile(@"..\..\..\pildid\github.png");
            github.Location = new Point(160, 180);
            github.Size = new Size(50, 50);
            github.SizeMode = PictureBoxSizeMode.StretchImage;
            github.MouseClick += github_MouseClick;

            // Lisame nupud avavormile
            Controls.Add(nuppVike);
            Controls.Add(nuppKeskmine);
            Controls.Add(nuppSuur);
            Controls.Add(github);
        }

        private void NuppVike_Click(object sender, EventArgs e)
        {
            // Edastame konstruktorile: pealkiri, laius, kõrgus, värv
            photoMax vikeVorm = new photoMax();
            vikeVorm.Show();
        }

        private void github_MouseClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = "https://github.com/Lvakos/KolmRakendust_Puhtejev",
                UseShellExecute = true
            });
        }


        private void NuppKeskmine_Click(object sender, EventArgs e)
        {
            mathMang keskmineVorm = new mathMang();
            keskmineVorm.Show();
        }

        private void nuppSuur_Click(object sender, EventArgs e)
        {
            ModeValimineVorm suurVorm = new ModeValimineVorm();
            suurVorm.Show();
        }
    }
}
