using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace Memory_Project
{
    public partial class Start_Screen : Form
    {
        // De onderstaande variabelen moeten worden opgeslagen
        // Welke speler aan de beurt is
        public Label spelersLabel = new Label();
        // De scores van Speler 1 en Speler 2
        public Label score2Label = new Label();
        public Label score4Label = new Label();

        // Namen van de spelers
        public TextBox playerOne = new TextBox();
        public TextBox playerTwo = new TextBox();

        // Onthoudt welke kaart welk plaatje heeft door middel van Tag
        public string[] location = new string[16];

        // Overige variabelen
        int res = 1, beurt = 1;

        bool GridInit = false; // Wordt gebruikt om te kijken of de 4x4 grid al is aangemaakt => zie InitGrid()

        Form Name_Screen = new Form(); // Het scherm waarin je de namen invoert
        Form Game_Screen = new Form(); // Het scherm waarin het spel wordt gespeeld    

        Button Reset_Button = new Button(); // Reset knop die de grid reset => zie Reset_Button_Click

        Random rng = new Random(); // Wordt gebruikt om een willekeurig plaatje te kiezen => zie InitGrid()

        List<Image> icons = new List<Image>(); // De plaatjes die gebruikt worden voor het spel => zie de #region Foto's en InitGrid()

        // Deze 2 variabelen worden gebruikt om te bepalen welke 2 kaarten worden aangeklikt
        PictureBox firstClicked = null;
        PictureBox secondClicked = null;

        /// <summary>
        /// Zorgt ervoor dat het menu in het midden van het scherm komt en vult de icons List op met plaatjes
        /// </summary>
        public Start_Screen()
        {
            InitializeComponent();

            this.CenterToScreen();

            playerOne.Text = "Speler 1";
            playerTwo.Text = "Speler 2";

            #region Foto's
            icons.Add(Image.FromFile(@"./imgs/Cyan.png"));
            icons.Add(Image.FromFile(@"./imgs/Cyan.png"));
            icons[0].Tag = "0";
            icons[1].Tag = "0";
            icons.Add(Image.FromFile(@"./imgs/LBlue.png"));
            icons.Add(Image.FromFile(@"./imgs/LBlue.png"));
            icons[2].Tag = "1";
            icons[3].Tag = "1";
            icons.Add(Image.FromFile(@"./imgs/LGreen.png"));
            icons.Add(Image.FromFile(@"./imgs/LGreen.png"));
            icons[4].Tag = "2";
            icons[5].Tag = "2";
            icons.Add(Image.FromFile(@"./imgs/Orange.png"));
            icons.Add(Image.FromFile(@"./imgs/Orange.png"));
            icons[6].Tag = "3";
            icons[7].Tag = "3";
            icons.Add(Image.FromFile(@"./imgs/Pink.png"));
            icons.Add(Image.FromFile(@"./imgs/Pink.png"));
            icons[8].Tag = "4";
            icons[9].Tag = "4";
            icons.Add(Image.FromFile(@"./imgs/Purple.png"));
            icons.Add(Image.FromFile(@"./imgs/Purple.png"));
            icons[10].Tag = "5";
            icons[11].Tag = "5";
            icons.Add(Image.FromFile(@"./imgs/Red.png"));
            icons.Add(Image.FromFile(@"./imgs/Red.png"));
            icons[12].Tag = "6";
            icons[13].Tag = "6";
            icons.Add(Image.FromFile(@"./imgs/Yellow.png"));
            icons.Add(Image.FromFile(@"./imgs/Yellow.png"));
            icons[14].Tag = "7";
            icons[15].Tag = "7";
            #endregion Foto's 
        }

        /// <summary>
        /// Hide() de Game_Screen i.p.v. Close() wanneer de gebruiker op de sluitknop klikt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Game_Screen_FormClosing(object sender, FormClosingEventArgs e)
        {
            File.WriteAllText(@"C:\Users\pawpr\Desktop\memory.sav", string.Format("{0}\n{1}\n{2}\n{3}\n{4}\n{5}\n{6}\n{7}\n{8}\n{9}\n{10}\n{11}\n{12}\n{13}\n{14}\n{15}\n{16}\n{17}\n{18}\n{19}\n{20}", spelersLabel.Text, score2Label.Text, score4Label.Text, playerOne.Text, playerTwo.Text, location[0], location[1], location[2], location[3], location[4], location[5], location[6], location[7], location[8], location[9], location[10], location[11], location[12], location[13], location[14], location[15]));
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Start_Button.Enabled = true;
                e.Cancel = true;
                Game_Screen.Hide();
            }
        }

        /// <summary>
        /// Wat er gebeurt tijdens de klik op de Start_Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Start_Button_Click(object sender, EventArgs e)
        {
            // Zet de Start button uit om bugs te voorkomen (deze worden later weer aangezet)
            Start_Button.Enabled = false;

            Name_Screen.Icon = this.Icon;
            Name_Screen.StartPosition = FormStartPosition.CenterScreen;
            Name_Screen.Size = this.Size;
            Name_Screen.Show();

            Button OK_Button = new Button();
            OK_Button.Location = new Point(25, 95);
            OK_Button.Size = Start_Button.Size;
            OK_Button.Font = Start_Button.Font;
            OK_Button.Text = "OK";
            Name_Screen.Controls.Add(OK_Button);
            OK_Button.Click += new EventHandler(this.OK_Button_Click);

            playerOne.Location = new Point(40, 20);
            Name_Screen.Controls.Add(playerOne);

            Label playerOneLabel = new Label();
            playerOneLabel.Location = new Point(55, 5);
            playerOneLabel.Text = "Speler 1 naam: ";
            Name_Screen.Controls.Add(playerOneLabel);

            playerTwo.Location = new Point(40, 70);
            Name_Screen.Controls.Add(playerTwo);

            Label playerTwoLabel = new Label();
            playerTwoLabel.Location = new Point(55, 55);
            playerTwoLabel.Text = "Speler 2 naam: ";
            Name_Screen.Controls.Add(playerTwoLabel);
        }

        /// <summary>
        /// Wat er gebeurt tijdens de klik op de OK_Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OK_Button_Click(object sender, EventArgs e)
        {
            Name_Screen.Hide();

            // Initialiseert het game venster
            Game_Screen.Text = "Memory Game";
            Game_Screen.Icon = this.Icon;
            Game_Screen.StartPosition = FormStartPosition.CenterScreen;
            Game_Screen.Size = new Size(650, 665);
            Game_Screen.Show();
            // Maakt de FormClosing event aan
            Game_Screen.FormClosing += new FormClosingEventHandler(Game_Screen_FormClosing);

            // Kijkt of de grid al gemaakt is; zo ja = maak de grid en de UI (Reset knop en highscores) aan
            if (GridInit == false)
            {
                InitGrid();
                InitUI();
            }
        }

        /// <summary>
        ///  Wat er gebeurt tijdens de klik op de Reset_Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Reset_Button_Click(object sender, EventArgs e)
        {
            beurt = 1;
            Reset_Button.Enabled = false;
            // Reset de mogelijk aangeklikte knoppen om bugs te voorkomen
            firstClicked = null;
            secondClicked = null;

            // Verwijdert alles van de Game_Screen
            Game_Screen.Controls.Clear();

            // Verwijdert alles uit de List van plaatjes en zet weer de goeie plaatjes erin
            icons.Clear();
            #region Foto's
            icons.Add(Image.FromFile(@"./imgs/Cyan.png"));
            icons.Add(Image.FromFile(@"./imgs/Cyan.png"));
            icons[0].Tag = "0";
            icons[1].Tag = "0";
            icons.Add(Image.FromFile(@"./imgs/LBlue.png"));
            icons.Add(Image.FromFile(@"./imgs/LBlue.png"));
            icons[2].Tag = "1";
            icons[3].Tag = "1";
            icons.Add(Image.FromFile(@"./imgs/LGreen.png"));
            icons.Add(Image.FromFile(@"./imgs/LGreen.png"));
            icons[4].Tag = "2";
            icons[5].Tag = "2";
            icons.Add(Image.FromFile(@"./imgs/Orange.png"));
            icons.Add(Image.FromFile(@"./imgs/Orange.png"));
            icons[6].Tag = "3";
            icons[7].Tag = "3";
            icons.Add(Image.FromFile(@"./imgs/Pink.png"));
            icons.Add(Image.FromFile(@"./imgs/Pink.png"));
            icons[8].Tag = "4";
            icons[9].Tag = "4";
            icons.Add(Image.FromFile(@"./imgs/Purple.png"));
            icons.Add(Image.FromFile(@"./imgs/Purple.png"));
            icons[10].Tag = "5";
            icons[11].Tag = "5";
            icons.Add(Image.FromFile(@"./imgs/Red.png"));
            icons.Add(Image.FromFile(@"./imgs/Red.png"));
            icons[12].Tag = "6";
            icons[13].Tag = "6";
            icons.Add(Image.FromFile(@"./imgs/Yellow.png"));
            icons.Add(Image.FromFile(@"./imgs/Yellow.png"));
            icons[14].Tag = "7";
            icons[15].Tag = "7";
            #endregion Foto's 

            InitGrid();
            InitUI();
        }

        /// <summary>
        /// Wat er gebeurt tijdens de klik op een kaart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Card_Click(object sender, EventArgs e)
        {
            // Zet de reset knop aan
            Reset_Button.Enabled = true;

            // De aangeklikte knop heet nu clickedPic
            PictureBox clickedPic = sender as PictureBox;

            // Zet de aangeklikte knop uit zodat de gebruiker hem niet voor de 2e keer kan aanklikken
            clickedPic.Enabled = false;
            // Maakt het voorgrond plaatje onzichtbaar zodat de BackgroundImage te zien is
            clickedPic.Image = null;
            // Als dit de eerste plaatje is die wordt aangeklikt = firstClicked, zo niet = er gebeurt niks
            if (firstClicked == null)
            {
                firstClicked = clickedPic;
                return;
            }

            // Als firstClicked al bestaat en secondClicked niet = secondClicked wordt het 2e aangeklikte plaatje
            if (firstClicked != null && secondClicked == null)
            {
                secondClicked = clickedPic;

                // Als de plaatjes NIET hetzelfde zijn = zet het voorgrond plaatje weer aan => zie timer1_Tick
                if (firstClicked.BackgroundImage.Tag != secondClicked.BackgroundImage.Tag)
                {
                    foreach (PictureBox pic in Game_Screen.Controls.OfType<PictureBox>())
                    {
                        pic.Enabled = false;
                    }
                    // Zet de reset knop tijdelijk uit om bugs te voorkomen
                    Reset_Button.Enabled = false;
                    timer1.Start();
                }
                // Als de plaatjes wel gelijk zijn = hou het plaatje aan, reset firstClicked en secondClicker en kijk voor winnaar
                // => zie CheckWinner()
                else
                {
                    foreach (PictureBox pic in Game_Screen.Controls.OfType<PictureBox>())
                    {
                        if (pic.Image == null)
                        {
                            pic.Enabled = false;
                        }

                    }
                    switch (res)
                    {
                        case 1:
                            score2Label.Text = Convert.ToString(Convert.ToInt32(score2Label.Text) + 50);
                            break;
                        case 0:
                            score4Label.Text = Convert.ToString(Convert.ToInt32(score4Label.Text) + 50);
                            break;
                    }
                    CheckWinner();
                    Game_Screen.Controls.Remove(firstClicked);
                    Game_Screen.Controls.Remove(secondClicked);
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }
                return;
            }
        }

        /// <summary>
        /// Kijkt of er een winnaar is
        /// </summary>
        void CheckWinner()
        {
            foreach (PictureBox pic in Game_Screen.Controls.OfType<PictureBox>())
            {
                if (pic.Image != null)
                {
                    return;
                }
            }

            // De messagebox die wordt weergegeven als de gebruiker wint 
            if (Convert.ToInt32(score2Label.Text) > Convert.ToInt32(score4Label.Text))
            {
                MessageBox.Show(playerOne.Text + " heeft gewonnen met een score van " + score2Label.Text + "!", "Gefeliciteerd");
            }
            else if (Convert.ToInt32(score4Label.Text) > Convert.ToInt32(score2Label.Text))
            {
                MessageBox.Show(playerTwo.Text + " heeft gewonnen met een score van " + score4Label.Text + "!", "Gefeliciteerd");
            }
            else
            {
                MessageBox.Show("Gelijkspel met scores van " + score2Label.Text + "!");
            }
            #region Foto's
            icons.Add(Image.FromFile(@"./imgs/Cyan.png"));
            icons.Add(Image.FromFile(@"./imgs/Cyan.png"));
            icons[0].Tag = "0";
            icons[1].Tag = "0";
            icons.Add(Image.FromFile(@"./imgs/LBlue.png"));
            icons.Add(Image.FromFile(@"./imgs/LBlue.png"));
            icons[2].Tag = "1";
            icons[3].Tag = "1";
            icons.Add(Image.FromFile(@"./imgs/LGreen.png"));
            icons.Add(Image.FromFile(@"./imgs/LGreen.png"));
            icons[4].Tag = "2";
            icons[5].Tag = "2";
            icons.Add(Image.FromFile(@"./imgs/Orange.png"));
            icons.Add(Image.FromFile(@"./imgs/Orange.png"));
            icons[6].Tag = "3";
            icons[7].Tag = "3";
            icons.Add(Image.FromFile(@"./imgs/Pink.png"));
            icons.Add(Image.FromFile(@"./imgs/Pink.png"));
            icons[8].Tag = "4";
            icons[9].Tag = "4";
            icons.Add(Image.FromFile(@"./imgs/Purple.png"));
            icons.Add(Image.FromFile(@"./imgs/Purple.png"));
            icons[10].Tag = "5";
            icons[11].Tag = "5";
            icons.Add(Image.FromFile(@"./imgs/Red.png"));
            icons.Add(Image.FromFile(@"./imgs/Red.png"));
            icons[12].Tag = "6";
            icons[13].Tag = "6";
            icons.Add(Image.FromFile(@"./imgs/Yellow.png"));
            icons.Add(Image.FromFile(@"./imgs/Yellow.png"));
            icons[14].Tag = "7";
            icons[15].Tag = "7";
            #endregion Foto's 
        }

        /// <summary>
        /// Maakt de 4x4 grid van plaatjes
        /// </summary>
        void InitGrid()
        {
            GridInit = true;

            int x = 5;
            int y = 5;
            int i = 0;

            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    PictureBox card = new PictureBox();
                    int rngNum = rng.Next(icons.Count);
                    card.BorderStyle = BorderStyle.Fixed3D;
                    card.BackgroundImage = icons[rngNum];
                    location[i] = icons[rngNum].Tag.ToString();
                    icons.RemoveAt(rngNum);
                    card.Image = Image.FromFile(@"./imgs/back.png");
                    card.Size = new Size(104, 154);
                    card.Location = new Point(x, y);
                    card.Cursor = Cursors.Hand;
                    card.Click += new EventHandler(this.Card_Click);

                    Game_Screen.Controls.Add(card);

                    x += 105;

                    i++;
                }
                x = 5;
                y += 155;
            }
            Label testLabel = new Label();
            testLabel.Location = new Point(640, 640);
            testLabel.Text = location[0];
            Game_Screen.Controls.Add(testLabel);
        }

        /// <summary>
        /// Maakt de resetknop en highscore (WORK IN PROGRESS)
        /// </summary>
        void InitUI()
        {
            Label scoreLabel = new Label();
            scoreLabel.Location = new Point(425, 65);
            scoreLabel.Size = new Size(150, 30);
            scoreLabel.Font = new Font(FontFamily.GenericSansSerif, 12.0F, FontStyle.Bold);
            scoreLabel.Text = "Score " + playerOne.Text + ": ";
            Game_Screen.Controls.Add(scoreLabel);

            score2Label.Location = new Point(575, 65);
            score2Label.Font = new Font(FontFamily.GenericSansSerif, 12.0F, FontStyle.Bold);
            score2Label.Text = "100";
            Game_Screen.Controls.Add(score2Label);

            Label score3Label = new Label();
            score3Label.Location = new Point(425, 125);
            score3Label.Size = new Size(150, 30);
            score3Label.Font = new Font(FontFamily.GenericSansSerif, 12.0F, FontStyle.Bold);
            score3Label.Text = "Score " + playerTwo.Text + ": ";
            Game_Screen.Controls.Add(score3Label);

            score4Label.Location = new Point(575, 125);
            score4Label.Font = new Font(FontFamily.GenericSansSerif, 12.0F, FontStyle.Bold);
            score4Label.Text = "100";
            Game_Screen.Controls.Add(score4Label);

            if (playerTwo.Text == "")
            {
                score3Label.Hide();
                score4Label.Hide();
            }

            Reset_Button.Location = new Point(425, 5);
            Reset_Button.Size = new Size(205, 50);
            Reset_Button.Text = "Reset";
            Reset_Button.Font = new Font(FontFamily.GenericSansSerif, 18.0F, FontStyle.Regular);
            Game_Screen.Controls.Add(Reset_Button);
            Reset_Button.Click += new EventHandler(this.Reset_Button_Click);

            spelersLabel.Location = new Point(425, 225);
            spelersLabel.Size = new Size(300, 30);
            spelersLabel.Font = new Font(FontFamily.GenericSansSerif, 12.0F, FontStyle.Bold);
            spelersLabel.Text = playerOne.Text + " is aan de beurt.";
            Game_Screen.Controls.Add(spelersLabel);
        }

        /// <summary>
        /// De timer die afgaat wanneer 2 VERSCHILLENDE foto's worden aangeklikt (zodat de gebruiker wat tijd heeft
        /// om te kijken wat die fout had aangeklikt)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            Reset_Button.Enabled = true;

            foreach (PictureBox pic in Game_Screen.Controls.OfType<PictureBox>())
            {
                if (pic.Image != null)
                {
                    pic.Enabled = true;
                }
            }

            timer1.Stop();
            firstClicked.Image = Image.FromFile(@"./imgs/back.png");
            secondClicked.Image = Image.FromFile(@"./imgs/back.png");
            firstClicked.Enabled = true;
            firstClicked = null;
            secondClicked.Enabled = true;
            secondClicked = null;
            res = beurt % 2;
            switch (res)
            {
                case 1:
                    score2Label.Text = Convert.ToString(Convert.ToInt32(score2Label.Text) - 10);
                    break;
                case 0:
                    score4Label.Text = Convert.ToString(Convert.ToInt32(score4Label.Text) - 10);
                    break;
            }
            if (playerTwo.Text != "")
            {
                beurt++;
            }
            res = beurt % 2;
            switch (res)
            {
                case 1:
                    spelersLabel.Text = playerOne.Text + " is aan de beurt.";
                    break;
                case 0:
                    spelersLabel.Text = playerTwo.Text + " is aan de beurt.";
                    break;
            }
        }

        /// <summary>
        /// De omleiding naar onze GitHub wanneer de link wordt aangeklikt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GitHub_Link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/pprotas/Memory_Game");
        }
    }
}
