using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KolmRakendust_Puhtejev
{
    public partial class ModeValimineVorm : Form
    {
        public ModeValimineVorm()
        {
            Text = "Režiimi valimine";
            ClientSize = new Size(400, 300);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            Label title = new Label
            {
                Text = "Piltide mäng",
                Dock = DockStyle.Top,
                Height = 70,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Arial", 24, FontStyle.Bold),
                ForeColor = Color.Black
            };

            Button button4x4 = new Button
            {
                Text = "4 × 4",
                Width = 200,
                Height = 55,
                Left = 100,
                Top = 90,
                Font = new Font("Arial", 16, FontStyle.Bold)
            };

            Button button6x6 = new Button
            {
                Text = "6 × 6",
                Width = 200,
                Height = 55,
                Left = 100,
                Top = 160,
                Font = new Font("Arial", 16, FontStyle.Bold)
            };

            button4x4.Click += (sender, e) =>
            {
                Hide();

                Form2 game = new Form2(4);
                game.FormClosed += (s, args) => Show();
                game.Show();
            };

            button6x6.Click += (sender, e) =>
            {
                Hide();

                Form2 game = new Form2(6);
                game.FormClosed += (s, args) => Show();
                game.Show();
            };

            Controls.Add(title);
            Controls.Add(button4x4);
            Controls.Add(button6x6);
        }
    }
}
