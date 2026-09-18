using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace KolmRakendust_Puhtejev
{
    public partial class mathMang : Form
    {
        // Random number generator
        Random randomizer = new Random();

        // Addition
        int addend1;
        int addend2;

        // Subtraction
        int minuend;
        int subtrahend;

        // Multiplication
        int multiplicand;
        int multiplier;

        // Division
        int dividend;
        int divisor;

        // Remaining time
        int timeLeft;

        // Controls
        Label timeLabel;
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
        System.Windows.Forms.Timer timer1;

        public mathMang()
        {
            InitializeComponent();

            CreateMathQuizUI();
        }

        private void CreateMathQuizUI()
        {
            // Form
            this.Text = "Math Quiz";
            this.Size = new Size(500, 450);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;

            Label timeTextLabel = new Label();
            timeTextLabel.Text = "Time Left";
            timeTextLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            timeTextLabel.AutoSize = true;
            timeTextLabel.Location = new Point(300, 25);

            this.Controls.Add(timeTextLabel);

            timeLabel = new Label();
            timeLabel.Text = "30 sekundit";
            timeLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            timeLabel.AutoSize = true;
            timeLabel.Location = new Point(300, 55);
            timeLabel.BackColor = Color.White;
            timeLabel.Padding = new Padding(5);

            this.Controls.Add(timeLabel);

            plusLeftLabel = CreateNumberLabel(50, 110);
            plusRightLabel = CreateNumberLabel(170, 110);

            Label plusSign = CreateOperatorLabel("+", 115, 110);
            Label plusEquals = CreateOperatorLabel("=", 230, 110);

            sum = CreateAnswerBox(270, 105);

            this.Controls.Add(plusLeftLabel);
            this.Controls.Add(plusRightLabel);
            this.Controls.Add(plusSign);
            this.Controls.Add(plusEquals);
            this.Controls.Add(sum);

            minusLeftLabel = CreateNumberLabel(50, 165);
            minusRightLabel = CreateNumberLabel(170, 165);

            Label minusSign = CreateOperatorLabel("-", 115, 165);
            Label minusEquals = CreateOperatorLabel("=", 230, 165);

            difference = CreateAnswerBox(270, 160);

            this.Controls.Add(minusLeftLabel);
            this.Controls.Add(minusRightLabel);
            this.Controls.Add(minusSign);
            this.Controls.Add(minusEquals);
            this.Controls.Add(difference);

            timesLeftLabel = CreateNumberLabel(50, 220);
            timesRightLabel = CreateNumberLabel(170, 220);

            Label timesSign = CreateOperatorLabel("×", 115, 220);
            Label timesEquals = CreateOperatorLabel("=", 230, 220);

            product = CreateAnswerBox(270, 215);

            this.Controls.Add(timesLeftLabel);
            this.Controls.Add(timesRightLabel);
            this.Controls.Add(timesSign);
            this.Controls.Add(timesEquals);
            this.Controls.Add(product);

            dividedLeftLabel = CreateNumberLabel(50, 275);
            dividedRightLabel = CreateNumberLabel(170, 275);

            Label dividedSign = CreateOperatorLabel("÷", 115, 275);
            Label dividedEquals = CreateOperatorLabel("=", 230, 275);

            quotient = CreateAnswerBox(270, 270);

            this.Controls.Add(dividedLeftLabel);
            this.Controls.Add(dividedRightLabel);
            this.Controls.Add(dividedSign);
            this.Controls.Add(dividedEquals);
            this.Controls.Add(quotient);

            startButton = new Button();

            startButton.Text = "Alusta mängida!";
            startButton.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            startButton.Size = new Size(200, 50);
            startButton.Location = new Point(140, 340);
            startButton.BackColor = Color.LightSkyBlue;
            startButton.Click += startButton_Click;

            this.Controls.Add(startButton);

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
            label.Size = new Size(60, 40);
            label.Location = new Point(x, y);
            label.TextAlign = ContentAlignment.MiddleCenter;

            return label;
        }

        private Label CreateOperatorLabel(string text, int x, int y)
        {
            Label label = new Label();

            label.Text = text;
            label.Size = new Size(40, 40);
            label.Location = new Point(x, y);
            label.TextAlign = ContentAlignment.MiddleCenter;

            return label;
        }

        private NumericUpDown CreateAnswerBox(int x, int y)
        {
            NumericUpDown box = new NumericUpDown();

            box.Size = new Size(90, 35);
            box.Location = new Point(x, y);

            box.Minimum = 0;
            box.Maximum = 1000;
            box.Value = 0;

            return box;
        }

        public void StartTheQuiz()
        {
            addend1 = randomizer.Next(51);
            addend2 = randomizer.Next(51);

            plusLeftLabel.Text = addend1.ToString();
            plusRightLabel.Text = addend2.ToString();

            sum.Value = 0;


            minuend = randomizer.Next(1, 101);
            subtrahend = randomizer.Next(1, minuend);

            minusLeftLabel.Text = minuend.ToString();
            minusRightLabel.Text = subtrahend.ToString();

            difference.Value = 0;


            multiplicand = randomizer.Next(2, 11);
            multiplier = randomizer.Next(2, 11);

            timesLeftLabel.Text = multiplicand.ToString();
            timesRightLabel.Text = multiplier.ToString();

            product.Value = 0;


            divisor = randomizer.Next(2, 11);

            int temporaryQuotient = randomizer.Next(2, 11);

            dividend = divisor * temporaryQuotient;

            dividedLeftLabel.Text = dividend.ToString();
            dividedRightLabel.Text = divisor.ToString();

            quotient.Value = 0;

 
            timeLeft = 30;

            timeLabel.Text = "30 sekundit";
            timeLabel.BackColor = Color.LightGray;
            timeLabel.ForeColor = Color.Black;

            timer1.Start();
        }

        private bool CheckTheAnswer()
        {
            if ((addend1 + addend2 == sum.Value)
                && (minuend - subtrahend == difference.Value)
                && (multiplicand * multiplier == product.Value)
                && (dividend / divisor == quotient.Value))
            {
                return true;
            }

            return false;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            StartTheQuiz();

            startButton.Enabled = false;

            sum.Focus();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (CheckTheAnswer())
            {
                timer1.Stop();

                timeLabel.Text = "Õige!";
                timeLabel.BackColor = Color.LightGreen;

                SystemSounds.Asterisk.Play();

                MessageBox.Show(
                    "Kõik vastused on õiged!",
                    "Tubli!"
                );

                startButton.Enabled = true;
            }
            else if (timeLeft > 0)
            {
                timeLeft--;

                timeLabel.Text = timeLeft + " sekundit";

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
                timeLabel.BackColor = Color.LightGray;
                timeLabel.ForeColor = Color.Black;

                MessageBox.Show(
                    "Küsimused ei olnud vastatud aegselt.",
                    "Vabandust!" 
                );

                sum.Value = addend1 + addend2;
                difference.Value = minuend - subtrahend;
                product.Value = multiplicand * multiplier;
                quotient.Value = dividend / divisor;

                startButton.Enabled = true;
            }
        }

        private void answer_Enter(object sender, EventArgs e)
        {
            NumericUpDown answerBox = sender as NumericUpDown;

            if (answerBox != null)
            {
                answerBox.Select(0, answerBox.Value.ToString().Length);
            }
        }
    }
}
