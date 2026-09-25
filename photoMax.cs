using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace KolmRakendust_Puhtejev
{
    public partial class photoMax : Form
    {
        Button close, background, clear, showpicture, appbackground, save;
        Button previous, next, slideshow;
        Button draw, color, clearDraw;
        Label silt;
        TableLayoutPanel panel;
        System.Windows.Forms.CheckBox stretch;
        FlowLayoutPanel flow;
        OpenFileDialog photo;
        PictureBox pilt;
        ColorDialog colorss;
        SaveFileDialog dialog;

        System.Windows.Forms.Timer slideshowTimer;
        string[] images = Array.Empty<string>();
        int currentImage = 0;

        // muutujad joonistamise jaoks
        bool drawing = false;
        bool isDrawing = false;
        Point lastPoint;
        Color drawColor = Color.Red;
        int drawWidth = 3;

        public photoMax()
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
            this.MaximizeBox = false;

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

            // joonistamise sündmused
            pilt.MouseDown += Pilt_MouseDown;
            pilt.MouseMove += Pilt_MouseMove;
            pilt.MouseUp += Pilt_MouseUp;

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

            // nupud piltide käsitsi vahetamiseks
            previous = new Button();
            previous.Text = "← Tagasi";
            previous.AutoSize = true;
            previous.MouseClick += Previous_MouseClick;

            next = new Button();
            next.Text = "Edasi →";
            next.AutoSize = true;
            next.MouseClick += Next_MouseClick;

            // slideshow nupp
            slideshow = new Button();
            slideshow.Text = "▶ Slaid-show";
            slideshow.AutoSize = true;
            slideshow.MouseClick += Slideshow_MouseClick;

            // joonistamise nupp
            draw = new Button();
            draw.Text = "Joonista";
            draw.AutoSize = true;
            draw.MouseClick += Draw_MouseClick;

            // värvi nupp
            color = new Button();
            color.Text = "Värv";
            color.AutoSize = true;
            color.MouseClick += Color_MouseClick;

            // joonistuse kustutamise nupp
            clearDraw = new Button();
            clearDraw.Text = "Kustuta joonistus";
            clearDraw.AutoSize = true;
            clearDraw.MouseClick += ClearDraw_MouseClick;

            panel.Controls.Add(flow, 1, 1);

            flow.Controls.Add(close);
            flow.Controls.Add(save);
            flow.Controls.Add(appbackground);
            flow.Controls.Add(background);
            flow.Controls.Add(clear);
            flow.Controls.Add(showpicture);

            flow.Controls.Add(previous);
            flow.Controls.Add(next);
            flow.Controls.Add(slideshow);

            flow.Controls.Add(draw);
            flow.Controls.Add(color);
            flow.Controls.Add(clearDraw);

            // timer slideshow jaoks
            slideshowTimer = new System.Windows.Forms.Timer();
            slideshowTimer.Interval = 3000;
            slideshowTimer.Tick += SlideshowTimer_Tick;
        }

        private void Save_MouseClick(object? sender, MouseEventArgs e)
        {
            if (pilt.Image == null)
                return;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                // salvestame praeguse PictureBox pildi koos joonistusega
                using (Bitmap bitmap = new Bitmap(pilt.Width, pilt.Height))
                {
                    pilt.DrawToBitmap(
                        bitmap,
                        new Rectangle(0, 0, pilt.Width, pilt.Height)
                    );

                    string extension =
                        Path.GetExtension(dialog.FileName).ToLower();

                    if (extension == ".jpg")
                    {
                        bitmap.Save(
                            dialog.FileName,
                            System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    else if (extension == ".png")
                    {
                        bitmap.Save(
                            dialog.FileName,
                            System.Drawing.Imaging.ImageFormat.Png);
                    }
                    else if (extension == ".bmp")
                    {
                        bitmap.Save(
                            dialog.FileName,
                            System.Drawing.Imaging.ImageFormat.Bmp);
                    }
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

                ShowCurrentImage();
            }
        }

        // näitab praegust pilti
        private void ShowCurrentImage()
        {
            if (images.Length == 0)
                return;

            if (currentImage < 0)
                currentImage = images.Length - 1;

            if (currentImage >= images.Length)
                currentImage = 0;

            // vabastame vana pildi
            if (pilt.Image != null)
            {
                Image oldImage = pilt.Image;
                pilt.Image = null;
                oldImage.Dispose();
            }

            // laadime uue pildi
            using (FileStream stream = new FileStream(
                images[currentImage],
                FileMode.Open,
                FileAccess.Read))
            {
                pilt.Image = Image.FromStream(stream);
                pilt.Image = new Bitmap(pilt.Image);
            }
        }

        // järgmine pilt
        private void Next_MouseClick(object? sender, MouseEventArgs e)
        {
            if (images.Length == 0)
                return;

            currentImage++;

            if (currentImage >= images.Length)
                currentImage = 0;

            ShowCurrentImage();
        }

        // eelmine pilt
        private void Previous_MouseClick(object? sender, MouseEventArgs e)
        {
            if (images.Length == 0)
                return;

            currentImage--;

            if (currentImage < 0)
                currentImage = images.Length - 1;

            ShowCurrentImage();
        }

        // slideshow sisse/välja
        private void Slideshow_MouseClick(object? sender, MouseEventArgs e)
        {
            if (images.Length == 0)
            {
                MessageBox.Show("Kõigepealt vali pildid!");
                return;
            }

            if (slideshowTimer.Enabled)
            {
                slideshowTimer.Stop();
                slideshow.Text = "▶ Slaid-show";
            }
            else
            {
                slideshowTimer.Start();
                slideshow.Text = "⏸ Peata";
            }
        }

        private void SlideshowTimer_Tick(object? sender, EventArgs e)
        {
            if (images.Length == 0)
                return;

            currentImage++;

            if (currentImage >= images.Length)
                currentImage = 0;

            ShowCurrentImage();
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

        // joonistamise režiimi sisse/välja
        private void Draw_MouseClick(object? sender, MouseEventArgs e)
        {
            if (pilt.Image == null)
            {
                MessageBox.Show("Kõigepealt vali pilt!");
                return;
            }

            drawing = !drawing;

            if (drawing)
            {
                draw.Text = "⏹ Peata joonistamine";
                pilt.Cursor = Cursors.Cross;
            }
            else
            {
                draw.Text = "Joonista";
                pilt.Cursor = Cursors.Default;
            }
        }

        // värvi valimine
        private void Color_MouseClick(object? sender, MouseEventArgs e)
        {
            if (colorss.ShowDialog() == DialogResult.OK)
            {
                drawColor = colorss.Color;
            }
        }

        // joonistuse kustutamine
        private void ClearDraw_MouseClick(object? sender, MouseEventArgs e)
        {
            if (pilt.Image == null)
                return;

            ShowCurrentImage();
        }

        // hiire vajutamine pildil
        private void Pilt_MouseDown(object? sender, MouseEventArgs e)
        {
            if (!drawing || pilt.Image == null)
                return;

            if (e.Button == MouseButtons.Left)
            {
                isDrawing = true;
                lastPoint = e.Location;
            }
        }

        // hiire liigutamine pildil
        private void Pilt_MouseMove(object? sender, MouseEventArgs e)
        {
            if (!drawing || !isDrawing || pilt.Image == null)
                return;

            using (Graphics graphics = pilt.CreateGraphics())
            {
                using (Pen pen = new Pen(drawColor, drawWidth))
                {
                    pen.StartCap =
                        System.Drawing.Drawing2D.LineCap.Round;

                    pen.EndCap =
                        System.Drawing.Drawing2D.LineCap.Round;

                    graphics.DrawLine(
                        pen,
                        lastPoint,
                        e.Location);
                }
            }

            lastPoint = e.Location;
        }

        // hiire vabastamine
        private void Pilt_MouseUp(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDrawing = false;
            }
        }
    }
}
