
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace KolmRakendust_Puhtejev
{
    public partial class piltideMang : Form
    {
        private readonly Random random = new Random();

        private readonly string[] icons =
        {
            "!", "!", "N", "N",
            ",", ",", "k", "k",
            "b", "b", "v", "v",
            "w", "w", "z", "z",
            "A", "A", "C", "C",
            "D", "D", "F", "F",
            "G", "G", "H", "H",
            "J", "J", "K", "K",
            "L", "L", "M", "M"
        };

        private TableLayoutPanel gameBoard;
        private Label firstClicked;
        private Label secondClicked;
        private System.Windows.Forms.Timer hideTimer;
        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.Timer hintTimer;
        private System.Windows.Forms.Timer hintHideTimer;

        private Button hintButton;

        private int moves = 0;
        private int matchedPairs = 0;
        private int boardSize;
        private int totalPairs;
        private int points = 0;
        private int seconds = 0;

        private Label movesLabel;
        private Label pairsLabel;
        private Label pointsLabel;
        private Label timeLabel;

        public piltideMang(int size)
        {
            InitializeComponent();

            boardSize = size;
            totalPairs = (size * size) / 2;

            CreateGame();
        }

        private void CreateGame()
        {
            Text = "Piltide mäng";
            ClientSize = new Size(600, 700);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            Label title = new Label
            {
                Text = "Piltide mäng",
                Dock = DockStyle.Top,
                Height = 55,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new System.Drawing.Font("Arial", 20, FontStyle.Bold),
                ForeColor = Color.Black
            };

            Controls.Add(title);

            Button backButton = new Button
            {
                Text = "Tagasi",
                Width = 90,
                Height = 35,
                Left = 10,
                Top = 10,
                Font = new System.Drawing.Font("Arial", 10, FontStyle.Bold)
            };

            backButton.Click += BackButton_Click;

            Controls.Add(backButton);

            Panel infoPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 80
            };

            movesLabel = new Label
            {
                Text = "Käigud: 0",
                AutoSize = false,
                Width = 140,
                Height = 35,
                Left = 20,
                Top = 5,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new System.Drawing.Font("Arial", 11, FontStyle.Bold),
                ForeColor = Color.Black
            };

            pairsLabel = new Label
            {
                Text = "Paarid: 0 / " + totalPairs,
                AutoSize = false,
                Width = 150,
                Height = 35,
                Left = 215,
                Top = 5,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new System.Drawing.Font("Arial", 11, FontStyle.Bold),
                ForeColor = Color.Black
            };

            pointsLabel = new Label
            {
                Text = "Punktid: 0",
                AutoSize = false,
                Width = 150,
                Height = 35,
                Left = 415,
                Top = 5,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new System.Drawing.Font("Arial", 11, FontStyle.Bold),
                ForeColor = Color.Black
            };

            timeLabel = new Label
            {
                Text = "Aeg: 0 s",
                AutoSize = false,
                Width = 150,
                Height = 35,
                Left = 20,
                Top = 40,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new System.Drawing.Font("Arial", 11, FontStyle.Bold),
                ForeColor = Color.Black
            };

            // nupp vihje jaoks
            hintButton = new Button
            {
                Text = "Vihje",
                Width = 100,
                Height = 35,
                Left = 215,
                Top = 40,
                Enabled = false,
                Font = new System.Drawing.Font("Arial", 10, FontStyle.Bold)
            };

            hintButton.Click += HintButton_Click;

            infoPanel.Controls.Add(movesLabel);
            infoPanel.Controls.Add(pairsLabel);
            infoPanel.Controls.Add(pointsLabel);
            infoPanel.Controls.Add(timeLabel);
            infoPanel.Controls.Add(hintButton);

            Controls.Add(infoPanel);

            gameBoard = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset,
                Padding = new Padding(10),
                ColumnCount = boardSize,
                RowCount = boardSize
            };

            for (int i = 0; i < boardSize; i++)
            {
                gameBoard.ColumnStyles.Add(
                    new ColumnStyle(SizeType.Percent, 100f / boardSize)
                );

                gameBoard.RowStyles.Add(
                    new RowStyle(SizeType.Percent, 100f / boardSize)
                );
            }

            Controls.Add(gameBoard);

            int cardCount = boardSize * boardSize;

            for (int i = 0; i < cardCount; i++)
            {
                Label label = CreateCard();

                label.Click += Card_Click;

                gameBoard.Controls.Add(label);
            }

            hideTimer = new System.Windows.Forms.Timer
            {
                Interval = 750
            };

            hideTimer.Tick += HideTimer_Tick;

            gameTimer = new System.Windows.Forms.Timer
            {
                Interval = 1000
            };

            gameTimer.Tick += GameTimer_Tick;

            // таймер для появления возможности использовать подсказку
            hintTimer = new System.Windows.Forms.Timer
            {
                Interval = 30000
            };

            hintTimer.Tick += HintTimer_Tick;

            // таймер, который показывает подсказку некоторое время
            hintHideTimer = new System.Windows.Forms.Timer
            {
                Interval = 2000
            };

            hintHideTimer.Tick += HintHideTimer_Tick;

            AssignIcons();

            gameBoard.BringToFront();
            title.BringToFront();
            backButton.BringToFront();
        }

        private Label CreateCard()
        {
            int fontSize = boardSize == 4 ? 42 : 30;

            Label card = new Label
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(3),
                BackColor = Color.LightGray,
                ForeColor = Color.LightGray,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new System.Drawing.Font(
                    "Webdings",
                    fontSize,
                    FontStyle.Bold
                ),
                UseCompatibleTextRendering = true,
                Cursor = Cursors.Hand
            };

            return card;
        }

        private void AssignIcons()
        {
            List<string> uniqueIcons = icons
                .Distinct()
                .Take(totalPairs)
                .ToList();

            List<string> availableIcons = new List<string>();

            foreach (string icon in uniqueIcons)
            {
                availableIcons.Add(icon);
                availableIcons.Add(icon);
            }

            foreach (Control control in gameBoard.Controls)
            {
                Label card = control as Label;

                if (card == null)
                    continue;

                int index = random.Next(availableIcons.Count);

                card.Text = availableIcons[index];

                availableIcons.RemoveAt(index);
            }
        }

        private void Card_Click(object sender, EventArgs e)
        {
            if (hideTimer.Enabled || hintHideTimer.Enabled)
                return;

            Label clickedCard = sender as Label;

            if (clickedCard == null)
                return;

            if (clickedCard.ForeColor == Color.Black ||
                clickedCard.ForeColor == Color.Green)
                return;

            if (!gameTimer.Enabled)
            {
                gameTimer.Start();

                // запускаем отсчёт времени для первой подсказки
                hintTimer.Start();
            }

            if (firstClicked == null)
            {
                firstClicked = clickedCard;
                firstClicked.ForeColor = Color.Black;

                return;
            }

            secondClicked = clickedCard;
            secondClicked.ForeColor = Color.Black;

            moves++;
            movesLabel.Text = "Käigud: " + moves;

            if (firstClicked.Text == secondClicked.Text)
            {
                firstClicked.ForeColor = Color.Green;
                secondClicked.ForeColor = Color.Green;

                matchedPairs++;

                points += 100;

                pairsLabel.Text =
                    "Paarid: " + matchedPairs + " / " + totalPairs;

                pointsLabel.Text =
                    "Punktid: " + points;

                firstClicked = null;
                secondClicked = null;

                CheckForWinner();

                return;
            }

            points -= 10;

            if (points < 0)
                points = 0;

            pointsLabel.Text =
                "Punktid: " + points;

            hideTimer.Start();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            seconds++;

            timeLabel.Text =
                "Aeg: " + seconds + " s";
        }

        // каждые 30 секунд кнопка подсказки становится доступной
        private void HintTimer_Tick(object sender, EventArgs e)
        {
            hintButton.Enabled = true;
            hintButton.Text = "Vihje!";
        }

        // кнопка подсказки
        private void HintButton_Click(object sender, EventArgs e)
        {
            if (!hintButton.Enabled)
                return;

            hintButton.Enabled = false;
            hintButton.Text = "Oota...";

            // показываем все закрытые карты
            foreach (Control control in gameBoard.Controls)
            {
                Label card = control as Label;

                if (card == null)
                    continue;

                if (card.ForeColor == card.BackColor)
                {
                    card.ForeColor = Color.Black;
                }
            }

            // через 2 секунды снова закрываем карты
            hintHideTimer.Start();
        }

        // скрываем карты после подсказки
        private void HintHideTimer_Tick(object sender, EventArgs e)
        {
            hintHideTimer.Stop();

            foreach (Control control in gameBoard.Controls)
            {
                Label card = control as Label;

                if (card == null)
                    continue;

                // оставляем найденные пары открытыми
                if (card.ForeColor != Color.Green)
                {
                    card.ForeColor = card.BackColor;
                }
            }

            // запускаем новый отсчёт 30 секунд
            hintTimer.Start();
        }

        private void HideTimer_Tick(object sender, EventArgs e)
        {
            hideTimer.Stop();

            if (firstClicked != null)
            {
                firstClicked.ForeColor =
                    firstClicked.BackColor;
            }

            if (secondClicked != null)
            {
                secondClicked.ForeColor =
                    secondClicked.BackColor;
            }

            firstClicked = null;
            secondClicked = null;
        }

        private void CheckForWinner()
        {
            if (matchedPairs != totalPairs)
                return;

            gameTimer.Stop();
            hintTimer.Stop();
            hintHideTimer.Stop();

            DialogResult result = MessageBox.Show(
                "Palju õnne!\n\n" +
                "Sa leidsid kõik paarid!\n\n" +
                "Käikude arv: " + moves +
                "\nPunktid: " + points +
                "\nAeg: " + seconds + " sekundit" +
                "\n\nKas soovid uuesti mängida?",
                "Võit!",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information
            );

            if (result == DialogResult.Yes)
            {
                RestartGame();
            }
            else
            {
                Close();
            }
        }

        private void RestartGame()
        {
            hideTimer.Stop();
            gameTimer.Stop();
            hintTimer.Stop();
            hintHideTimer.Stop();

            firstClicked = null;
            secondClicked = null;

            moves = 0;
            matchedPairs = 0;
            points = 0;
            seconds = 0;

            movesLabel.Text = "Käigud: 0";
            pairsLabel.Text = "Paarid: 0 / " + totalPairs;
            pointsLabel.Text = "Punktid: 0";
            timeLabel.Text = "Aeg: 0 s";

            hintButton.Enabled = false;
            hintButton.Text = "Vihje";

            foreach (Control control in gameBoard.Controls)
            {
                Label card = control as Label;

                if (card == null)
                    continue;

                card.ForeColor = card.BackColor;
            }

            AssignIcons();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            hideTimer.Stop();
            gameTimer.Stop();
            hintTimer.Stop();
            hintHideTimer.Stop();

            Close();
        }
    }
}