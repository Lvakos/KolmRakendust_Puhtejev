using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KolmRakendust_Puhtejev
{
    public partial class mathMang : Form
    {
        Button startButton;
        Label timeLabel, timeTextLabel;

        Label plusLeftLabel, plusRightLabel;
        Label minusLeftLabel, minusRightLabel;
        Label timesLeftLabel, timesRightLabel;
        Label dividedLeftLabel, dividedRightLabel;

        Label plusLabel, minusLabel, timesLabel, dividedLabel;
        Label equalsPlus, equalsMinus, equalsTimes, equalsDivided;

        NumericUpDown sum, difference, product, quotient;

        System.Windows.Forms.Timer timer;
        int timeLeft;

        public mathMang()
        {
            InitializeComponent();

            timeTextLabel = new Label();
            timeTextLabel.Text = "Time Left";
            timeTextLabel.AutoSize = true;
            timeTextLabel.Font = new Font("Arial", 15.75F);
            timeTextLabel.Location = new Point(300, 20);

            timeLabel = new Label();
            timeLabel.Text = "";
            timeLabel.AutoSize = false;
            timeLabel.BorderStyle = BorderStyle.FixedSingle;
            timeLabel.Size = new Size(200, 30);
            timeLabel.Font = new Font("Arial", 15.75F);
            timeLabel.Location = new Point(390, 20);

            Controls.Add(timeTextLabel);
            Controls.Add(timeLabel);


            plusLeftLabel = new Label();
            plusLeftLabel.Text = "?";
            plusLeftLabel.Size = new Size(60, 50);
            plusLeftLabel.Font = new Font("Arial", 18);
            plusLeftLabel.TextAlign = ContentAlignment.MiddleCenter;
            plusLeftLabel.Location = new Point(50, 75);

            plusLabel = new Label();
            plusLabel.Text = "+";
            plusLabel.Size = new Size(60, 50);
            plusLabel.Font = new Font("Arial", 18);
            plusLabel.TextAlign = ContentAlignment.MiddleCenter;
            plusLabel.Location = new Point(110, 75);

            plusRightLabel = new Label();
            plusRightLabel.Text = "?";
            plusRightLabel.Size = new Size(60, 50);
            plusRightLabel.Font = new Font("Arial", 18);
            plusRightLabel.TextAlign = ContentAlignment.MiddleCenter;
            plusRightLabel.Location = new Point(170, 75);

            equalsPlus = new Label();
            equalsPlus.Text = "=";
            equalsPlus.Size = new Size(60, 50);
            equalsPlus.Font = new Font("Arial", 18);
            equalsPlus.TextAlign = ContentAlignment.MiddleCenter;
            equalsPlus.Location = new Point(230, 75);

            sum = new NumericUpDown();
            sum.Font = new Font("Arial", 18);
            sum.Size = new Size(100, 50);
            sum.TabIndex = 1;
            sum.Location = new Point(290, 75);

            Controls.Add(plusLeftLabel);
            Controls.Add(plusLabel);
            Controls.Add(plusRightLabel);
            Controls.Add(equalsPlus);
            Controls.Add(sum);


            startButton = new Button();
            startButton.Text = "Start the quiz";
            startButton.Font = new Font("Arial", 14);
            startButton.AutoSize = true;
            startButton.TabIndex = 0;
            startButton.Location = new Point(180, 350);

            Controls.Add(startButton);

        }
    }
}
