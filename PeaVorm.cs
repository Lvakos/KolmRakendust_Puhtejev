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
        private Button nuppVike, nuppKeskmine, nuppSuur;

        public PeaVorm()
        {
            Text = "Peaaken - Valikud";
            Size = new Size(400, 250);
            StartPosition = FormStartPosition.CenterScreen;

            // 1. Nupp: Väike aken
            nuppVike = new Button();
            nuppVike.Text = "Ava väike aken";
            nuppVike.Location = new Point(50, 30);
            nuppVike.Size = new Size(280, 40);
            nuppVike.Click += NuppVike_Click;

            // 2. Nupp: Keskmine aken
            nuppKeskmine = new Button();
            nuppKeskmine.Text = "Ava keskmine aken";
            nuppKeskmine.Location = new Point(50, 80);
            nuppKeskmine.Size = new Size(280, 40);
            nuppKeskmine.Click += NuppKeskmine_Click;

            nuppSuur = new Button();
            nuppSuur.Text = "Ava suur aken";
            nuppSuur.Location = new Point(50, 130);
            nuppSuur.Size = new Size(280, 40);
            nuppSuur.Click += nuppSuur_Click;

            // Lisame nupud avavormile
            Controls.Add(nuppVike);
            Controls.Add(nuppKeskmine);
            Controls.Add(nuppSuur);
        }

        private void NuppVike_Click(object sender, EventArgs e)
        {
            // Edastame konstruktorile: pealkiri, laius, kõrgus, värv
            Form1 vikeVorm = new Form1();
            vikeVorm.Show();
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
