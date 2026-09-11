using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace KolmRakendust_Puhtejev
{
    public partial class Form1 : Form
    {
        Button close, background, clear, showpicture, appbackground, save;
        Label silt;
        TableLayoutPanel panel;
        System.Windows.Forms.CheckBox stretch;
        FlowLayoutPanel flow;
        OpenFileDialog photo;
        PictureBox pilt;
        ColorDialog colorss;
        SaveFileDialog dialog;


        System.Windows.Forms.Timer slideshowTimer;
        string[] images;
        int currentImage = 0;
        public Form1()
        {
            dialog = new SaveFileDialog();
            dialog.Filter = "JPEG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|BMP Files (*.bmp)|*.bmp|All files (*.*)|*.*";
            dialog.Title = "Salvesta pilt";

            colorss = new ColorDialog();

            photo = new OpenFileDialog();
            photo.Multiselect = true;

            photo.Filter = "JPEG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|BMP Files (*.bmp)|*.bmp|All files (*.*)|*.*";

            StartPosition = FormStartPosition.CenterParent;
            Height = 700;
            Width = 700;
            Text = "PhotoMax";
            StartPosition = FormStartPosition.CenterParent;

            // tablelayoutpanel
            panel = new TableLayoutPanel();
            panel.Dock = DockStyle.Fill;
            panel.RowCount = 2;

            panel.ColumnCount = 2;

            panel.ColumnStyles.Clear();

            panel.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 15F));

            panel.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 80F));

            panel.RowStyles.Add(
                new RowStyle(SizeType.Percent, 90F)
                );

            panel.RowStyles.Add(
                new RowStyle(SizeType.Percent, 10F)
                );

            Controls.Add(panel);

            // stretch checkbox
            stretch = new System.Windows.Forms.CheckBox();
            stretch.Text = "Venitada";
            stretch.Width = 100;
            stretch.Height = 50;
            stretch.CheckedChanged += Stretch_CheckedChanged;

            pilt = new PictureBox();

            pilt.Dock = DockStyle.Fill;
            pilt.BorderStyle = BorderStyle.FixedSingle;
            pilt.SizeMode = PictureBoxSizeMode.Zoom;
            pilt.BackColor = Color.Black;

            panel.Controls.Add(pilt, 1, 0);

            flow = new FlowLayoutPanel();
            flow.Dock = DockStyle.Fill;
            flow.FlowDirection = FlowDirection.RightToLeft;

            panel.Controls.Add(stretch, 0, 1);

            close = new Button();
            close.Text = "Sule";
            close.AutoSize = true;
            close.MouseClick += Close_MouseClick;

            save = new Button();
            save.Text = "Salvesta";
            save.AutoSize = true;
            save.MouseClick += Save_MouseClick;

            appbackground = new Button();
            appbackground.Text = "Terve vormi taust";
            appbackground.AutoSize = true;
            appbackground.MouseClick += Appbackground_MouseClick;

            background = new Button();
            background.Text = "Määra taust";
            background.AutoSize = true;
            background.MouseClick += Background_MouseClick;

            clear = new Button();
            clear.Text = "Selge pilt";
            clear.AutoSize = true;
            clear.MouseClick += Clear_MouseClick;

            showpicture = new Button();
            showpicture.Text = "Näita Pilti";
            showpicture.AutoSize = true;
            showpicture.MouseClick += Showpicture_MouseClick;

            panel.Controls.Add(flow, 1, 1);
            flow.Controls.Add(close);
            flow.Controls.Add(save);
            flow.Controls.Add(appbackground);
            flow.Controls.Add(background);
            flow.Controls.Add(clear);
            flow.Controls.Add(showpicture);

        }

        private void Save_MouseClick(object? sender, MouseEventArgs e)
        {
            if (pilt.Image == null)
                return;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string extension = Path.GetExtension(dialog.FileName).ToLower();

                if (extension == ".jpg")
                {
                    pilt.Image.Save(dialog.FileName,
                        System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                else if (extension == ".png")
                {
                    pilt.Image.Save(dialog.FileName,
                        System.Drawing.Imaging.ImageFormat.Png);
                }
                else if (extension == ".bmp")
                {
                    pilt.Image.Save(dialog.FileName,
                        System.Drawing.Imaging.ImageFormat.Bmp);
                }
            }
        }

        private void Appbackground_MouseClick(object? sender, MouseEventArgs e)
        {
            if (colorss.ShowDialog() == DialogResult.OK)
                this.BackColor = colorss.Color;
        }

        private void Stretch_CheckedChanged(object? sender, EventArgs e)
        {
            if (stretch.Checked)
                pilt.SizeMode = PictureBoxSizeMode.StretchImage;
            else
                pilt.SizeMode = PictureBoxSizeMode.Zoom;
        }


        private void Showpicture_MouseClick(object? sender, MouseEventArgs e)
        {
            if (photo.ShowDialog() == DialogResult.OK)
            {
                images = photo.FileNames;
                currentImage = 0;

                pilt.Image = Image.FromFile(images[currentImage]);

                slideshowTimer = new System.Windows.Forms.Timer();
                slideshowTimer.Interval = 3000; // 3 sekundit
                slideshowTimer.Tick += SlideshowTimer_Tick; ;
                slideshowTimer.Start();
            }
        }

        private void SlideshowTimer_Tick(object? sender, EventArgs e)
        {
            if (images.Length == 0)
                return;

            currentImage++;

            if (currentImage >= images.Length)
                currentImage = 0;

            pilt.Image = Image.FromFile(images[currentImage]);
        }

        private void Clear_MouseClick(object? sender, MouseEventArgs e)
        {
            pilt.Image = null;
        }

        private void Background_MouseClick(object? sender, MouseEventArgs e)
        {
            if (colorss.ShowDialog() == DialogResult.OK)
                pilt.BackColor = colorss.Color;
        }

        private void Close_MouseClick(object? sender, MouseEventArgs e)
        {
            this.Close();
        }
    }
}
