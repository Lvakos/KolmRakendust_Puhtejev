using System.Media;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Windows.Forms;

namespace KolmRakendust_Puhtejev
{
    public partial class mathMang : Form
    {
        Random randomizer = new Random();

        int addend1;
        int addend2;

        int minuend;
        int subtrahend;

        int multiplicand;
        int multiplier;

        int dividend;
        int divisor;

        int timeLeft;
        int score;
        int difficultyMultiplier;

        string currentDifficulty;

        Label timeLabel;
        Label scoreLabel;
        Label difficultyLabel;

        Label plusLeftLabel;
        Label plusRightLabel;

        Label minusLeftLabel;
        Label minusRightLabel;

        Label timesLeftLabel;
        Label timesRightLabel;

        Label dividedLeftLabel;
        Label dividedRightLabel;

        NumericUpDown sum;
        NumericUpDown difference;
        NumericUpDown product;
        NumericUpDown quotient;

        Button startButton;
        Button leaderboardButton;
        Button finishButton;
        ComboBox difficultyComboBox;

        System.Windows.Forms.Timer timer1;

        string leaderboardFile = "leaderboard.txt";

        public mathMang()
        {
            InitializeComponent();
            CreateMathQuizUI();
        }

        private void CreateMathQuizUI()
        {
            this.Text = "Matemaatiline Mäng";
            this.Size = new Size(550, 620);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;

            Label titleLabel = new Label();
            titleLabel.Text = "MATEMAATILINE MÄNG";
            titleLabel.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(120, 15);
            this.Controls.Add(titleLabel);

            difficultyLabel = new Label();
            difficultyLabel.Text = "Raskus:";
            difficultyLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            difficultyLabel.AutoSize = true;
            difficultyLabel.Location = new Point(45, 70);
            this.Controls.Add(difficultyLabel);

            difficultyComboBox = new ComboBox();
            difficultyComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            difficultyComboBox.Font = new Font("Segoe UI", 11F);
            difficultyComboBox.Items.Add("Lihtne");
            difficultyComboBox.Items.Add("Keskmine");
            difficultyComboBox.Items.Add("Raske");
            difficultyComboBox.SelectedIndex = 0;
            difficultyComboBox.Size = new Size(130, 30);
            difficultyComboBox.Location = new Point(120, 65);
            this.Controls.Add(difficultyComboBox);

            Label scoreTextLabel = new Label();
            scoreTextLabel.Text = "Punktid:";
            scoreTextLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            scoreTextLabel.AutoSize = true;
            scoreTextLabel.Location = new Point(285, 70);
            this.Controls.Add(scoreTextLabel);

            scoreLabel = new Label();
            scoreLabel.Text = "0";
            scoreLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            scoreLabel.AutoSize = true;
            scoreLabel.Location = new Point(355, 70);
            this.Controls.Add(scoreLabel);

            Label timeTextLabel = new Label();
            timeTextLabel.Text = "Aeg:";
            timeTextLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            timeTextLabel.AutoSize = true;
            timeTextLabel.Location = new Point(285, 105);
            this.Controls.Add(timeTextLabel);

            timeLabel = new Label();
            timeLabel.Text = "60 sekundit";
            timeLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            timeLabel.AutoSize = true;
            timeLabel.Location = new Point(330, 105);
            timeLabel.BackColor = Color.LightGreen;
            timeLabel.Padding = new Padding(5);
            this.Controls.Add(timeLabel);

            plusLeftLabel = CreateNumberLabel(50, 160);
            plusRightLabel = CreateNumberLabel(180, 160);

            Label plusSign = CreateOperatorLabel("+", 125, 160);
            Label plusEquals = CreateOperatorLabel("=", 240, 160);

            sum = CreateAnswerBox(290, 155);

            this.Controls.Add(plusLeftLabel);
            this.Controls.Add(plusRightLabel);
            this.Controls.Add(plusSign);
            this.Controls.Add(plusEquals);
            this.Controls.Add(sum);

            minusLeftLabel = CreateNumberLabel(50, 215);
            minusRightLabel = CreateNumberLabel(180, 215);

            Label minusSign = CreateOperatorLabel("-", 125, 215);
            Label minusEquals = CreateOperatorLabel("=", 240, 215);

            difference = CreateAnswerBox(290, 210);

            this.Controls.Add(minusLeftLabel);
            this.Controls.Add(minusRightLabel);
            this.Controls.Add(minusSign);
            this.Controls.Add(minusEquals);
            this.Controls.Add(difference);

            timesLeftLabel = CreateNumberLabel(50, 270);
            timesRightLabel = CreateNumberLabel(180, 270);

            Label timesSign = CreateOperatorLabel("×", 125, 270);
            Label timesEquals = CreateOperatorLabel("=", 240, 270);

            product = CreateAnswerBox(290, 265);

            this.Controls.Add(timesLeftLabel);
            this.Controls.Add(timesRightLabel);
            this.Controls.Add(timesSign);
            this.Controls.Add(timesEquals);
            this.Controls.Add(product);

            dividedLeftLabel = CreateNumberLabel(50, 325);
            dividedRightLabel = CreateNumberLabel(180, 325);

            Label dividedSign = CreateOperatorLabel("÷", 125, 325);
            Label dividedEquals = CreateOperatorLabel("=", 240, 325);

            quotient = CreateAnswerBox(290, 320);

            this.Controls.Add(dividedLeftLabel);
            this.Controls.Add(dividedRightLabel);
            this.Controls.Add(dividedSign);
            this.Controls.Add(dividedEquals);
            this.Controls.Add(quotient);

            startButton = new Button();
            startButton.Text = "Alusta mängu!";
            startButton.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            startButton.Size = new Size(220, 50);
            startButton.Location = new Point(155, 390);
            startButton.BackColor = Color.LightGreen;
            startButton.Click += startButton_Click;
            this.Controls.Add(startButton);

            // mängu lõpetamise nupp
            finishButton = new Button();
            finishButton.Text = "Lõpeta mäng";
            finishButton.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            finishButton.Size = new Size(220, 40);
            finishButton.Location = new Point(155, 445);
            finishButton.BackColor = Color.LightCoral;
            finishButton.Enabled = false;
            finishButton.Click += finishButton_Click;
            this.Controls.Add(finishButton);

            leaderboardButton = new Button();
            leaderboardButton.Text = "Leaderboard";
            leaderboardButton.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            leaderboardButton.Size = new Size(220, 40);
            leaderboardButton.Location = new Point(155, 495);
            leaderboardButton.Click += leaderboardButton_Click;
            this.Controls.Add(leaderboardButton);

            timer1 = new System.Windows.Forms.Timer();
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;

            sum.Enter += answer_Enter;
            sum.Click += answer_Enter;

            difference.Enter += answer_Enter;
            difference.Click += answer_Enter;

            product.Enter += answer_Enter;
            product.Click += answer_Enter;

            quotient.Enter += answer_Enter;
            quotient.Click += answer_Enter;
        }

        private Label CreateNumberLabel(int x, int y)
        {
            Label label = new Label();
            label.Text = "?";
            label.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            label.Size = new Size(60, 40);
            label.Location = new Point(x, y);
            label.TextAlign = ContentAlignment.MiddleCenter;
            return label;
        }

        private Label CreateOperatorLabel(string text, int x, int y)
        {
            Label label = new Label();
            label.Text = text;
            label.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            label.Size = new Size(40, 40);
            label.Location = new Point(x, y);
            label.TextAlign = ContentAlignment.MiddleCenter;
            return label;
        }

        private NumericUpDown CreateAnswerBox(int x, int y)
        {
            NumericUpDown box = new NumericUpDown();
            box.Font = new Font("Segoe UI", 16F);
            box.Size = new Size(90, 35);
            box.Location = new Point(x, y);
            box.Minimum = 0;
            box.Maximum = 10000;
            box.Value = 0;
            return box;
        }

        public void StartTheQuiz()
        {
            score = 0;
            scoreLabel.Text = "0";

            currentDifficulty = difficultyComboBox.SelectedItem.ToString();

            if (currentDifficulty == "Lihtne")
            {
                difficultyMultiplier = 1;
                timeLeft = 60;

                addend1 = randomizer.Next(1, 21);
                addend2 = randomizer.Next(1, 21);

                minuend = randomizer.Next(10, 51);
                subtrahend = randomizer.Next(1, minuend);

                multiplicand = randomizer.Next(1, 6);
                multiplier = randomizer.Next(1, 6);

                divisor = randomizer.Next(2, 6);
                int temporaryQuotient = randomizer.Next(1, 6);
                dividend = divisor * temporaryQuotient;
            }
            else if (currentDifficulty == "Keskmine")
            {
                difficultyMultiplier = 2;
                timeLeft = 45;

                addend1 = randomizer.Next(10, 101);
                addend2 = randomizer.Next(10, 101);

                minuend = randomizer.Next(50, 201);
                subtrahend = randomizer.Next(10, minuend);

                multiplicand = randomizer.Next(2, 13);
                multiplier = randomizer.Next(2, 13);

                divisor = randomizer.Next(2, 13);
                int temporaryQuotient = randomizer.Next(2, 13);
                dividend = divisor * temporaryQuotient;
            }
            else
            {
                difficultyMultiplier = 3;
                timeLeft = 30;

                addend1 = randomizer.Next(100, 501);
                addend2 = randomizer.Next(100, 501);

                minuend = randomizer.Next(200, 1001);
                subtrahend = randomizer.Next(50, minuend);

                multiplicand = randomizer.Next(10, 31);
                multiplier = randomizer.Next(10, 31);

                divisor = randomizer.Next(5, 21);
                int temporaryQuotient = randomizer.Next(5, 21);
                dividend = divisor * temporaryQuotient;
            }

            plusLeftLabel.Text = addend1.ToString();
            plusRightLabel.Text = addend2.ToString();

            minusLeftLabel.Text = minuend.ToString();
            minusRightLabel.Text = subtrahend.ToString();

            timesLeftLabel.Text = multiplicand.ToString();
            timesRightLabel.Text = multiplier.ToString();

            dividedLeftLabel.Text = dividend.ToString();
            dividedRightLabel.Text = divisor.ToString();

            sum.Value = 0;
            difference.Value = 0;
            product.Value = 0;
            quotient.Value = 0;

            // eemaldame eelmise mängu värvid
            ResetAnswerColors();

            timeLabel.Text = timeLeft + " sekundit";
            timeLabel.BackColor = Color.LightGreen;
            timeLabel.ForeColor = Color.Black;

            timer1.Start();
        }

        private bool CheckTheAnswer()
        {
            return
                (addend1 + addend2 == sum.Value) &&
                (minuend - subtrahend == difference.Value) &&
                (multiplicand * multiplier == product.Value) &&
                (dividend / divisor == quotient.Value);
        }

        private int CalculateScore()
        {
            int basePoints = 100 * difficultyMultiplier;
            int speedBonus = timeLeft * 10 * difficultyMultiplier;

            return basePoints + speedBonus;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            StartTheQuiz();

            startButton.Enabled = false;
            finishButton.Enabled = true;
            difficultyComboBox.Enabled = false;

            sum.Focus();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (CheckTheAnswer())
            {
                timer1.Stop();

                int earnedPoints = CalculateScore();

                score += earnedPoints;
                scoreLabel.Text = score.ToString();

                timeLabel.Text = "Õige!";
                timeLabel.BackColor = Color.LightGreen;
                timeLabel.ForeColor = Color.Black;

                SystemSounds.Asterisk.Play();

                ShowResults("Kõik vastused on õiged!");

                return;
            }

            if (timeLeft > 0)
            {
                timeLeft--;

                timeLabel.Text = timeLeft + " sekundit";

                if (timeLeft <= 10)
                {
                    timeLabel.BackColor = Color.Orange;
                }

                if (timeLeft <= 5)
                {
                    timeLabel.BackColor = Color.Red;
                    timeLabel.ForeColor = Color.White;
                }
            }
            else
            {
                timer1.Stop();

                timeLabel.Text = "Aeg on läbi!";
                timeLabel.BackColor = Color.Gray;
                timeLabel.ForeColor = Color.White;

                ShowResults("Aeg on läbi!");

                startButton.Enabled = true;
                finishButton.Enabled = false;
                difficultyComboBox.Enabled = true;
            }
        }

        // mängu saab lõpetada enne aja lõppu
        private void finishButton_Click(object sender, EventArgs e)
        {
            timer1.Stop();

            timeLabel.Text = "Mäng lõpetatud";
            timeLabel.BackColor = Color.Gray;
            timeLabel.ForeColor = Color.White;

            ShowResults("Mäng lõpetati enne aja lõppu!");

            startButton.Enabled = true;
            finishButton.Enabled = false;
            difficultyComboBox.Enabled = true;
        }

        // näitab mängu lõpus õiged ja valed vastused
        private void ShowResults(string message)
        {
            int correctAnswers = 0;

            // liitmine
            if (sum.Value == addend1 + addend2)
            {
                sum.BackColor = Color.LightGreen;
                correctAnswers++;
            }
            else
            {
                sum.BackColor = Color.LightCoral;
            }

            // lahutamine
            if (difference.Value == minuend - subtrahend)
            {
                difference.BackColor = Color.LightGreen;
                correctAnswers++;
            }
            else
            {
                difference.BackColor = Color.LightCoral;
            }

            // korrutamine
            if (product.Value == multiplicand * multiplier)
            {
                product.BackColor = Color.LightGreen;
                correctAnswers++;
            }
            else
            {
                product.BackColor = Color.LightCoral;
            }

            // jagamine
            if (quotient.Value == dividend / divisor)
            {
                quotient.BackColor = Color.LightGreen;
                correctAnswers++;
            }
            else
            {
                quotient.BackColor = Color.LightCoral;
            }

            MessageBox.Show(
                message + "\n\n" +
                "Õigeid vastuseid: " + correctAnswers + " / 4\n" +
                "Vale vastuseid: " + (4 - correctAnswers) + "\n" +
                "Punktid: " + score,
                "Mängu tulemus",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            // kui kõik neli vastust olid õiged
            if (correctAnswers == 4)
            {
                DialogResult result = MessageBox.Show(
                    "Kõik vastused on õiged!\n\n" +
                    "Raskus: " + currentDifficulty +
                    "\nPunktid: " + score +
                    "\n\nKas soovid oma tulemuse leaderboard'i salvestada?",
                    "Tubli!",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information
                );

                if (result == DialogResult.Yes)
                {
                    AskForNickname();
                }
            }
        }

        // eemaldame eelmise mängu värvid
        private void ResetAnswerColors()
        {
            sum.BackColor = SystemColors.Window;
            difference.BackColor = SystemColors.Window;
            product.BackColor = SystemColors.Window;
            quotient.BackColor = SystemColors.Window;
        }

        private void AskForNickname()
        {
            Form nicknameForm = new Form();

            nicknameForm.Text = "Leaderboard";
            nicknameForm.Size = new Size(350, 190);
            nicknameForm.StartPosition = FormStartPosition.CenterParent;
            nicknameForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            nicknameForm.MaximizeBox = false;
            nicknameForm.MinimizeBox = false;

            Label label = new Label();
            label.Text = "Sisesta oma kasutajanimi:";
            label.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            label.AutoSize = true;
            label.Location = new Point(35, 25);

            TextBox nicknameBox = new TextBox();
            nicknameBox.Font = new Font("Segoe UI", 12F);
            nicknameBox.Size = new Size(260, 30);
            nicknameBox.Location = new Point(35, 55);

            Button saveButton = new Button();
            saveButton.Text = "Salvesta";
            saveButton.Size = new Size(100, 35);
            saveButton.Location = new Point(80, 100);
            saveButton.DialogResult = DialogResult.OK;

            Button cancelButton = new Button();
            cancelButton.Text = "Tühista";
            cancelButton.Size = new Size(100, 35);
            cancelButton.Location = new Point(185, 100);
            cancelButton.DialogResult = DialogResult.Cancel;

            nicknameForm.Controls.Add(label);
            nicknameForm.Controls.Add(nicknameBox);
            nicknameForm.Controls.Add(saveButton);
            nicknameForm.Controls.Add(cancelButton);

            nicknameForm.AcceptButton = saveButton;
            nicknameForm.CancelButton = cancelButton;

            nicknameBox.Focus();

            if (nicknameForm.ShowDialog(this) == DialogResult.OK)
            {
                string nickname = nicknameBox.Text.Trim();

                if (string.IsNullOrWhiteSpace(nickname))
                {
                    MessageBox.Show(
                        "Palun sisesta nimi!",
                        "Viga",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }

                nickname = nickname
                    .Replace("|", "")
                    .Replace("\r", "")
                    .Replace("\n", "");

                SaveScore(nickname, score, currentDifficulty);
            }
        }

        private void SaveScore(string nickname, int points, string difficulty)
        {
            string line =
                nickname + "|" +
                points + "|" +
                difficulty + "|" +
                DateTime.Now.ToString("dd.MM.yyyy HH:mm");

            File.AppendAllText(
                leaderboardFile,
                line + Environment.NewLine
            );

            MessageBox.Show(
                "Tulemus on leaderboard'i salvestatud!",
                "Salvestatud",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void leaderboardButton_Click(object sender, EventArgs e)
        {
            ShowLeaderboard();
        }

        private void ShowLeaderboard()
        {
            Form leaderboardForm = new Form();

            leaderboardForm.Text = "Leaderboard";
            leaderboardForm.Size = new Size(650, 500);
            leaderboardForm.StartPosition = FormStartPosition.CenterParent;
            leaderboardForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            leaderboardForm.MaximizeBox = false;

            Label title = new Label();
            title.Text = "LEADERBOARD";
            title.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            title.AutoSize = true;
            title.Location = new Point(230, 15);

            ListView listView = new ListView();
            listView.View = View.Details;
            listView.FullRowSelect = true;
            listView.GridLines = true;
            listView.Font = new Font("Segoe UI", 10F);
            listView.Location = new Point(25, 65);
            listView.Size = new Size(585, 340);

            listView.Columns.Add("№", 45);
            listView.Columns.Add("Nimi", 180);
            listView.Columns.Add("Punktid", 90);
            listView.Columns.Add("Raskus", 100);
            listView.Columns.Add("Kuupäev", 150);

            if (File.Exists(leaderboardFile))
            {
                string[] lines = File.ReadAllLines(leaderboardFile);

                var results = lines
                    .Select(line => line.Split('|'))
                    .Where(parts => parts.Length >= 4)
                    .Select(parts => new
                    {
                        Name = parts[0],
                        Points = int.TryParse(parts[1], out int p) ? p : 0,
                        Difficulty = parts[2],
                        Date = parts[3]
                    })
                    .OrderByDescending(x => x.Points)
                    .ToList();

                int position = 1;

                foreach (var result in results)
                {
                    ListViewItem item =
                        new ListViewItem(position.ToString());

                    item.SubItems.Add(result.Name);
                    item.SubItems.Add(result.Points.ToString());
                    item.SubItems.Add(result.Difficulty);
                    item.SubItems.Add(result.Date);

                    listView.Items.Add(item);

                    position++;
                }
            }

            if (listView.Items.Count == 0)
            {
                ListViewItem emptyItem =
                    new ListViewItem("");

                emptyItem.SubItems.Add("Leaderboard on tühi");

                listView.Items.Add(emptyItem);
            }

            Button closeButton = new Button();
            closeButton.Text = "Sulge";
            closeButton.Size = new Size(120, 40);
            closeButton.Location = new Point(255, 415);
            closeButton.Click += (sender, e) =>
            {
                leaderboardForm.Close();
            };

            leaderboardForm.Controls.Add(title);
            leaderboardForm.Controls.Add(listView);
            leaderboardForm.Controls.Add(closeButton);

            leaderboardForm.ShowDialog(this);
        }

        private void answer_Enter(object sender, EventArgs e)
        {
            NumericUpDown answerBox = sender as NumericUpDown;

            if (answerBox != null)
            {
                answerBox.Select(
                    0,
                    answerBox.Value.ToString().Length
                );
            }
        }
    }
}