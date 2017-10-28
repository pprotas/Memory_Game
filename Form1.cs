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
using System.Resources;
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

        Image back = global::Memory_Project.Properties.Resources.back;

        // Overige variabelen
        int res, beurt = 1;

        bool GridInit = false; // Wordt gebruikt om te kijken of de 4x4 grid al is aangemaakt => zie InitGrid()
        bool NameScreen = false;

        Form Name_Screen = new Form(); // Het scherm waarin je de namen invoert
        Form Game_Screen = new Form(); // Het scherm waarin het spel wordt gespeeld    
        Form Highscore_Screen = new Form(); // Het scherm waarin de highscores worden weergegeven
        Form Settings_Screen = new Form();

        Button Reset_Button = new Button(); // Reset knop die de grid reset => zie Reset_Button_Click
        Button OK_Button = new Button();
        Button Settings_Button = new Button();

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
            back.Tag = "0";
            playerOne.Text = "Speler 1";
            playerTwo.Text = "Speler 2";

            #region Foto's
            icons.Add(global::Memory_Project.Properties.Resources.Cyan);
            icons.Add(global::Memory_Project.Properties.Resources.Cyan);
            icons[0].Tag = "0";
            icons[1].Tag = "0";
            icons.Add(global::Memory_Project.Properties.Resources.LBlue);
            icons.Add(global::Memory_Project.Properties.Resources.LBlue);
            icons[2].Tag = "1";
            icons[3].Tag = "1";
            icons.Add(global::Memory_Project.Properties.Resources.LGreen);
            icons.Add(global::Memory_Project.Properties.Resources.LGreen);
            icons[4].Tag = "2";
            icons[5].Tag = "2";
            icons.Add(global::Memory_Project.Properties.Resources.Orange);
            icons.Add(global::Memory_Project.Properties.Resources.Orange);
            icons[6].Tag = "3";
            icons[7].Tag = "3";
            icons.Add(global::Memory_Project.Properties.Resources.Pink);
            icons.Add(global::Memory_Project.Properties.Resources.Pink);
            icons[8].Tag = "4";
            icons[9].Tag = "4";
            icons.Add(global::Memory_Project.Properties.Resources.Purple);
            icons.Add(global::Memory_Project.Properties.Resources.Purple);
            icons[10].Tag = "5";
            icons[11].Tag = "5";
            icons.Add(global::Memory_Project.Properties.Resources.Red);
            icons.Add(global::Memory_Project.Properties.Resources.Red);
            icons[12].Tag = "6";
            icons[13].Tag = "6";
            icons.Add(global::Memory_Project.Properties.Resources.Yellow);
            icons.Add(global::Memory_Project.Properties.Resources.Yellow);
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
            File.WriteAllText(@"./memory.sav", string.Format("{0}\n{1}\n{2}\n{3}\n{4}\n{5}\n{6}\n{7}\n{8}\n{9}\n{10}\n{11}\n{12}\n{13}\n{14}\n{15}\n{16}\n{17}\n{18}\n{19}\n{20}\n{21}\n{22}", spelersLabel.Text, score2Label.Text, score4Label.Text, playerOne.Text, playerTwo.Text, location[0], location[1], location[2], location[3], location[4], location[5], location[6], location[7], location[8], location[9], location[10], location[11], location[12], location[13], location[14], location[15], beurt, back.Tag.ToString()));
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Start_Button.Enabled = true;
                Continue_Button.Enabled = true;
                e.Cancel = true;
                Game_Screen.Hide();
            }
        }

        private void Name_Screen_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Start_Button.Enabled = true;
                Continue_Button.Enabled = true;
                e.Cancel = true;
                Name_Screen.Hide();
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
            Continue_Button.Enabled = false;
            Name_Screen.ShowIcon = false;
            Name_Screen.MaximizeBox = false;
            Name_Screen.StartPosition = FormStartPosition.CenterScreen;
            Name_Screen.Size = new Size(200, 250);
            Name_Screen.Show();
            Name_Screen.FormBorderStyle = FormBorderStyle.FixedSingle;

            if (NameScreen == false)
            {
                NameScreen = true;
                Name_Screen.FormClosing += new FormClosingEventHandler(Name_Screen_FormClosing);
                
                OK_Button.Location = new Point(17, 95);
                OK_Button.Size = Start_Button.Size;
                OK_Button.Font = Start_Button.Font;
                OK_Button.Text = "OK";
                Name_Screen.Controls.Add(OK_Button);
                OK_Button.Click += new EventHandler(this.OK_Button_Click);
                
                Settings_Button.Location = new Point(17, 150);
                Settings_Button.Size = Start_Button.Size;
                Settings_Button.Font = OK_Button.Font;
                Settings_Button.Text = "Settings";
                Name_Screen.Controls.Add(Settings_Button);
                Settings_Button.Click += new EventHandler(this.Settings_Button_Click);

                playerOne.Location = new Point(42, 20);
                Name_Screen.Controls.Add(playerOne);

                Label playerOneLabel = new Label();
                playerOneLabel.Location = new Point(55, 5);
                playerOneLabel.Text = "Speler 1 naam: ";
                Name_Screen.Controls.Add(playerOneLabel);

                playerTwo.Location = new Point(42, 70);
                Name_Screen.Controls.Add(playerTwo);

                Label playerTwoLabel = new Label();
                playerTwoLabel.Location = new Point(55, 55);
                playerTwoLabel.Text = "Speler 2 naam: ";
                Name_Screen.Controls.Add(playerTwoLabel);
            }
        }

        private void Settings_Button_Click(object sender, EventArgs e)
        {
            Settings_Button.Enabled = false;
            OK_Button.Enabled = false;
            Settings_Screen.Size = new Size(450, 370);
            Settings_Screen.StartPosition = FormStartPosition.CenterScreen;
            Settings_Screen.Text = "Settings";
            Settings_Screen.Icon = this.Icon;
            Settings_Screen.MaximizeBox = false;
            Settings_Screen.FormClosing += new FormClosingEventHandler(Settings_Screen_FormClosing);
            Settings_Screen.Show();

            Label settingsLabel = new Label();
            settingsLabel.Text = "Choose your card back!";
            settingsLabel.Size = new Size(1000, 15);
            settingsLabel.Location = new Point(160, 5);
            Settings_Screen.Controls.Add(settingsLabel);

            int i = 0;
            int x = 10;
            int y = 20;
            for(int r = 0; r < 2; r++)
            {
                for(int c = 0; c < 4; c++)
                {
                    PictureBox backk = new PictureBox();
                    backk.Location = new Point(x, y);
                    backk.Size = new Size(105, 155);
                    backk.BorderStyle = BorderStyle.Fixed3D;
                    backk.Cursor = Cursors.Hand;
                    backk.Click += new EventHandler(this.Back_Click);
                    switch (i)
                    {
                        case 0:
                            backk.Image = Properties.Resources.back;
                            backk.Tag = "0";
                            break;
                        case 1:
                            backk.Image = Properties.Resources.background1;
                            backk.Tag = "1";
                            break;
                        case 2:
                            backk.Image = Properties.Resources.background2;
                            backk.Tag = "2";
                            break;
                        case 3:
                            backk.Image = Properties.Resources.background3;
                            backk.Tag = "3";
                            break;
                        case 4:
                            backk.Image = Properties.Resources.background4;
                            backk.Tag = "4";
                            break;
                        case 5:
                            backk.Image = Properties.Resources.background5;
                            backk.Tag = "5";
                            break;
                        case 6:
                            backk.Image = Properties.Resources.background6;
                            backk.Tag = "6";
                            break;
                        case 7:
                            backk.Image = Properties.Resources.background7;
                            backk.Tag = "7";
                            break;
                    }
                    Settings_Screen.Controls.Add(backk);
                    i++;
                    x += 105;
                }
                x = 10;
                y += 155;
            }
        }

        private void Back_Click(object sender, EventArgs e)
        {
            PictureBox clickedBack = sender as PictureBox;

            Settings_Screen.Close();
            back = clickedBack.Image;
            back.Tag = clickedBack.Tag;
        }

        private void Settings_Screen_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings_Button.Enabled = true;
            OK_Button.Enabled = true;
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Settings_Screen.Hide();
            }
        }

        /// <summary>
        /// Wat er gebeurt tijdens de klik op de OK_Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OK_Button_Click(object sender, EventArgs e)
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
            icons.Add(global::Memory_Project.Properties.Resources.Cyan);
            icons.Add(global::Memory_Project.Properties.Resources.Cyan);
            icons[0].Tag = "0";
            icons[1].Tag = "0";
            icons.Add(global::Memory_Project.Properties.Resources.LBlue);
            icons.Add(global::Memory_Project.Properties.Resources.LBlue);
            icons[2].Tag = "1";
            icons[3].Tag = "1";
            icons.Add(global::Memory_Project.Properties.Resources.LGreen);
            icons.Add(global::Memory_Project.Properties.Resources.LGreen);
            icons[4].Tag = "2";
            icons[5].Tag = "2";
            icons.Add(global::Memory_Project.Properties.Resources.Orange);
            icons.Add(global::Memory_Project.Properties.Resources.Orange);
            icons[6].Tag = "3";
            icons[7].Tag = "3";
            icons.Add(global::Memory_Project.Properties.Resources.Pink);
            icons.Add(global::Memory_Project.Properties.Resources.Pink);
            icons[8].Tag = "4";
            icons[9].Tag = "4";
            icons.Add(global::Memory_Project.Properties.Resources.Purple);
            icons.Add(global::Memory_Project.Properties.Resources.Purple);
            icons[10].Tag = "5";
            icons[11].Tag = "5";
            icons.Add(global::Memory_Project.Properties.Resources.Red);
            icons.Add(global::Memory_Project.Properties.Resources.Red);
            icons[12].Tag = "6";
            icons[13].Tag = "6";
            icons.Add(global::Memory_Project.Properties.Resources.Yellow);
            icons.Add(global::Memory_Project.Properties.Resources.Yellow);
            icons[14].Tag = "7";
            icons[15].Tag = "7";
            #endregion Foto's 

            InitGrid();
            InitUI();

            Name_Screen.Hide();

            // Initialiseert het game venster
            Game_Screen.Text = "Memory Game";
            Game_Screen.MaximizeBox = false;
            Game_Screen.Icon = this.Icon;
            Game_Screen.StartPosition = FormStartPosition.CenterScreen;
            Game_Screen.Size = new Size(650, 665);
            Game_Screen.Show();
            Game_Screen.FormBorderStyle = FormBorderStyle.FixedSingle;
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
            icons.Add(global::Memory_Project.Properties.Resources.Cyan);
            icons.Add(global::Memory_Project.Properties.Resources.Cyan);
            icons[0].Tag = "0";
            icons[1].Tag = "0";
            icons.Add(global::Memory_Project.Properties.Resources.LBlue);
            icons.Add(global::Memory_Project.Properties.Resources.LBlue);
            icons[2].Tag = "1";
            icons[3].Tag = "1";
            icons.Add(global::Memory_Project.Properties.Resources.LGreen);
            icons.Add(global::Memory_Project.Properties.Resources.LGreen);
            icons[4].Tag = "2";
            icons[5].Tag = "2";
            icons.Add(global::Memory_Project.Properties.Resources.Orange);
            icons.Add(global::Memory_Project.Properties.Resources.Orange);
            icons[6].Tag = "3";
            icons[7].Tag = "3";
            icons.Add(global::Memory_Project.Properties.Resources.Pink);
            icons.Add(global::Memory_Project.Properties.Resources.Pink);
            icons[8].Tag = "4";
            icons[9].Tag = "4";
            icons.Add(global::Memory_Project.Properties.Resources.Purple);
            icons.Add(global::Memory_Project.Properties.Resources.Purple);
            icons[10].Tag = "5";
            icons[11].Tag = "5";
            icons.Add(global::Memory_Project.Properties.Resources.Red);
            icons.Add(global::Memory_Project.Properties.Resources.Red);
            icons[12].Tag = "6";
            icons[13].Tag = "6";
            icons.Add(global::Memory_Project.Properties.Resources.Yellow);
            icons.Add(global::Memory_Project.Properties.Resources.Yellow);
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
                    for(int i = 0; i < 16; i++)
                    {
                        if(location[i]==Convert.ToString(firstClicked.BackgroundImage.Tag))
                        {
                            location[i] = "";
                        }
                    }
                    res = beurt % 2;
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
                    timer2.Start();
                    
                    
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
                SaveHighscore();
            }
            else if (Convert.ToInt32(score4Label.Text) > Convert.ToInt32(score2Label.Text))
            {
                MessageBox.Show(playerTwo.Text + " heeft gewonnen met een score van " + score4Label.Text + "!", "Gefeliciteerd");
                SaveHighscore();
            }
            else
            {
                MessageBox.Show("Gelijkspel met scores van " + score2Label.Text + "!");
            }
            SaveHighscore();
            #region Foto's
            icons.Add(global::Memory_Project.Properties.Resources.Cyan);
            icons.Add(global::Memory_Project.Properties.Resources.Cyan);
            icons[0].Tag = "0";
            icons[1].Tag = "0";
            icons.Add(global::Memory_Project.Properties.Resources.LBlue);
            icons.Add(global::Memory_Project.Properties.Resources.LBlue);
            icons[2].Tag = "1";
            icons[3].Tag = "1";
            icons.Add(global::Memory_Project.Properties.Resources.LGreen);
            icons.Add(global::Memory_Project.Properties.Resources.LGreen);
            icons[4].Tag = "2";
            icons[5].Tag = "2";
            icons.Add(global::Memory_Project.Properties.Resources.Orange);
            icons.Add(global::Memory_Project.Properties.Resources.Orange);
            icons[6].Tag = "3";
            icons[7].Tag = "3";
            icons.Add(global::Memory_Project.Properties.Resources.Pink);
            icons.Add(global::Memory_Project.Properties.Resources.Pink);
            icons[8].Tag = "4";
            icons[9].Tag = "4";
            icons.Add(global::Memory_Project.Properties.Resources.Purple);
            icons.Add(global::Memory_Project.Properties.Resources.Purple);
            icons[10].Tag = "5";
            icons[11].Tag = "5";
            icons.Add(global::Memory_Project.Properties.Resources.Red);
            icons.Add(global::Memory_Project.Properties.Resources.Red);
            icons[12].Tag = "6";
            icons[13].Tag = "6";
            icons.Add(global::Memory_Project.Properties.Resources.Yellow);
            icons.Add(global::Memory_Project.Properties.Resources.Yellow);
            icons[14].Tag = "7";
            icons[15].Tag = "7";
            #endregion Foto's 
        }

        void SaveHighscore()
        {
            if (!File.Exists(@"./highscores.sav"))
            {
                string[] q = {"0", "0", "0", "0", "0", "0", "0", "0", "0", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-"};
                File.Create(@"./highscores.sav").Close();
                File.WriteAllLines(@"./highscores.sav", q);
            }
            string[] highscores = File.ReadAllLines(@"./highscores.sav");
            for (int i = 0; i < 10; i++)
            {
                if (highscores[i] == "")
                {
                    highscores[i] = "0";
                }
            }
            #region Highscore speler 1 opslaan
            if (Convert.ToInt32(score2Label.Text) > Convert.ToInt32(highscores[0]))
            {
                highscores[9] = highscores[8];
                highscores[8] = highscores[7];
                highscores[7] = highscores[6];
                highscores[6] = highscores[5];
                highscores[5] = highscores[4];
                highscores[4] = highscores[3];
                highscores[3] = highscores[2];
                highscores[2] = highscores[1];
                highscores[1] = highscores[0];
                highscores[0] = score2Label.Text;
                highscores[19] = highscores[18];
                highscores[18] = highscores[17];
                highscores[17] = highscores[16];
                highscores[16] = highscores[15];
                highscores[15] = highscores[14];
                highscores[14] = highscores[13];
                highscores[13] = highscores[12];
                highscores[12] = highscores[11];
                highscores[11] = highscores[10];
                highscores[10] = playerOne.Text;
            }
            else if (Convert.ToInt32(score2Label.Text) > Convert.ToInt32(highscores[1]))
            {
                highscores[9] = highscores[8];
                highscores[8] = highscores[7];
                highscores[7] = highscores[6];
                highscores[6] = highscores[5];
                highscores[5] = highscores[4];
                highscores[4] = highscores[3];
                highscores[3] = highscores[2];
                highscores[2] = highscores[1];
                highscores[1] = score2Label.Text;
                highscores[19] = highscores[18];
                highscores[18] = highscores[17];
                highscores[17] = highscores[16];
                highscores[16] = highscores[15];
                highscores[15] = highscores[14];
                highscores[14] = highscores[13];
                highscores[13] = highscores[12];
                highscores[12] = highscores[11];
                highscores[11] = playerOne.Text;
            }
            else if (Convert.ToInt32(score2Label.Text) > Convert.ToInt32(highscores[2]))
            {
                highscores[9] = highscores[8];
                highscores[8] = highscores[7];
                highscores[7] = highscores[6];
                highscores[6] = highscores[5];
                highscores[5] = highscores[4];
                highscores[4] = highscores[3];
                highscores[3] = highscores[2];
                highscores[2] = score2Label.Text;
                highscores[19] = highscores[18];
                highscores[18] = highscores[17];
                highscores[17] = highscores[16];
                highscores[16] = highscores[15];
                highscores[15] = highscores[14];
                highscores[14] = highscores[13];
                highscores[13] = highscores[12];
                highscores[12] = playerOne.Text;
            }
            else if (Convert.ToInt32(score2Label.Text) > Convert.ToInt32(highscores[3]))
            {
                highscores[9] = highscores[8];
                highscores[8] = highscores[7];
                highscores[7] = highscores[6];
                highscores[6] = highscores[5];
                highscores[5] = highscores[4];
                highscores[4] = highscores[3];
                highscores[3] = score2Label.Text;
                highscores[19] = highscores[18];
                highscores[18] = highscores[17];
                highscores[17] = highscores[16];
                highscores[16] = highscores[15];
                highscores[15] = highscores[14];
                highscores[14] = highscores[13];
                highscores[13] = playerOne.Text;
            }
            else if (Convert.ToInt32(score2Label.Text) > Convert.ToInt32(highscores[4]))
            {
                highscores[9] = highscores[8];
                highscores[8] = highscores[7];
                highscores[7] = highscores[6];
                highscores[6] = highscores[5];
                highscores[5] = highscores[4];
                highscores[4] = score2Label.Text;
                highscores[19] = highscores[18];
                highscores[18] = highscores[17];
                highscores[17] = highscores[16];
                highscores[16] = highscores[15];
                highscores[15] = highscores[14];
                highscores[14] = playerOne.Text;
            }
            else if (Convert.ToInt32(score2Label.Text) > Convert.ToInt32(highscores[5]))
            {
                highscores[9] = highscores[8];
                highscores[8] = highscores[7];
                highscores[7] = highscores[6];
                highscores[6] = highscores[5];
                highscores[5] = score2Label.Text;
                highscores[19] = highscores[18];
                highscores[18] = highscores[17];
                highscores[17] = highscores[16];
                highscores[16] = highscores[15];
                highscores[15] = playerOne.Text;
            }
            else if (Convert.ToInt32(score2Label.Text) > Convert.ToInt32(highscores[6]))
            {
                highscores[9] = highscores[8];
                highscores[8] = highscores[7];
                highscores[7] = highscores[6];
                highscores[6] = score2Label.Text;
                highscores[19] = highscores[18];
                highscores[18] = highscores[17];
                highscores[17] = highscores[16];
                highscores[16] = playerOne.Text;
            }
            else if (Convert.ToInt32(score2Label.Text) > Convert.ToInt32(highscores[7]))
            {
                highscores[9] = highscores[8];
                highscores[8] = highscores[7];
                highscores[7] = score2Label.Text;
                highscores[19] = highscores[18];
                highscores[18] = highscores[17];
                highscores[17] = playerOne.Text;
            }
            else if (Convert.ToInt32(score2Label.Text) > Convert.ToInt32(highscores[8]))
            {
                highscores[9] = highscores[8];
                highscores[8] = score2Label.Text;
                highscores[19] = highscores[18];
                highscores[18] = playerOne.Text;
            }
            else if (Convert.ToInt32(score2Label.Text) > Convert.ToInt32(highscores[9]))
            {
                highscores[9] = score2Label.Text;
                highscores[19] = playerOne.Text;
            }
            #endregion
            #region Highscore speler 2 opslaan
            if (Convert.ToInt32(score4Label.Text) > Convert.ToInt32(highscores[0]))
            {
                highscores[9] = highscores[8];
                highscores[8] = highscores[7];
                highscores[7] = highscores[6];
                highscores[6] = highscores[5];
                highscores[5] = highscores[4];
                highscores[4] = highscores[3];
                highscores[3] = highscores[2];
                highscores[2] = highscores[1];
                highscores[1] = highscores[0];
                highscores[0] = score4Label.Text;
                highscores[19] = highscores[18];
                highscores[18] = highscores[17];
                highscores[17] = highscores[16];
                highscores[16] = highscores[15];
                highscores[15] = highscores[14];
                highscores[14] = highscores[13];
                highscores[13] = highscores[12];
                highscores[12] = highscores[11];
                highscores[11] = highscores[10];
                highscores[10] = playerTwo.Text;

            }
            else if (Convert.ToInt32(score4Label.Text) > Convert.ToInt32(highscores[1]))
            {
                highscores[9] = highscores[8];
                highscores[8] = highscores[7];
                highscores[7] = highscores[6];
                highscores[6] = highscores[5];
                highscores[5] = highscores[4];
                highscores[4] = highscores[3];
                highscores[3] = highscores[2];
                highscores[2] = highscores[1];
                highscores[1] = score4Label.Text;
                highscores[19] = highscores[18];
                highscores[18] = highscores[17];
                highscores[17] = highscores[16];
                highscores[16] = highscores[15];
                highscores[15] = highscores[14];
                highscores[14] = highscores[13];
                highscores[13] = highscores[12];
                highscores[12] = highscores[11];
                highscores[11] = playerTwo.Text;
            }
            else if (Convert.ToInt32(score4Label.Text) > Convert.ToInt32(highscores[2]))
            {
                highscores[9] = highscores[8];
                highscores[8] = highscores[7];
                highscores[7] = highscores[6];
                highscores[6] = highscores[5];
                highscores[5] = highscores[4];
                highscores[4] = highscores[3];
                highscores[3] = highscores[2];
                highscores[2] = score4Label.Text;
                highscores[19] = highscores[18];
                highscores[18] = highscores[17];
                highscores[17] = highscores[16];
                highscores[16] = highscores[15];
                highscores[15] = highscores[14];
                highscores[14] = highscores[13];
                highscores[13] = highscores[12];
                highscores[12] = playerTwo.Text;
            }
            else if (Convert.ToInt32(score4Label.Text) > Convert.ToInt32(highscores[3]))
            {
                highscores[9] = highscores[8];
                highscores[8] = highscores[7];
                highscores[7] = highscores[6];
                highscores[6] = highscores[5];
                highscores[5] = highscores[4];
                highscores[4] = highscores[3];
                highscores[3] = score4Label.Text;
                highscores[19] = highscores[18];
                highscores[18] = highscores[17];
                highscores[17] = highscores[16];
                highscores[16] = highscores[15];
                highscores[15] = highscores[14];
                highscores[14] = highscores[13];
                highscores[13] = playerTwo.Text;
            }
            else if (Convert.ToInt32(score4Label.Text) > Convert.ToInt32(highscores[4]))
            {
                highscores[9] = highscores[8];
                highscores[8] = highscores[7];
                highscores[7] = highscores[6];
                highscores[6] = highscores[5];
                highscores[5] = highscores[4];
                highscores[4] = score4Label.Text;
                highscores[19] = highscores[18];
                highscores[18] = highscores[17];
                highscores[17] = highscores[16];
                highscores[16] = highscores[15];
                highscores[15] = highscores[14];
                highscores[14] = playerTwo.Text;
            }
            else if (Convert.ToInt32(score4Label.Text) > Convert.ToInt32(highscores[5]))
            {
                highscores[9] = highscores[8];
                highscores[8] = highscores[7];
                highscores[7] = highscores[6];
                highscores[6] = highscores[5];
                highscores[5] = score4Label.Text;
                highscores[19] = highscores[18];
                highscores[18] = highscores[17];
                highscores[17] = highscores[16];
                highscores[16] = highscores[15];
                highscores[15] = playerTwo.Text;
            }
            else if (Convert.ToInt32(score4Label.Text) > Convert.ToInt32(highscores[6]))
            {
                highscores[9] = highscores[8];
                highscores[8] = highscores[7];
                highscores[7] = highscores[6];
                highscores[6] = score4Label.Text;
                highscores[19] = highscores[18];
                highscores[18] = highscores[17];
                highscores[17] = highscores[16];
                highscores[16] = playerTwo.Text;
            }
            else if (Convert.ToInt32(score4Label.Text) > Convert.ToInt32(highscores[7]))
            {
                highscores[9] = highscores[8];
                highscores[8] = highscores[7];
                highscores[7] = score4Label.Text;
                highscores[19] = highscores[18];
                highscores[18] = highscores[17];
                highscores[17] = playerTwo.Text;
            }
            else if (Convert.ToInt32(score4Label.Text) > Convert.ToInt32(highscores[8]))
            {
                highscores[9] = highscores[8];
                highscores[8] = score4Label.Text;
                highscores[19] = highscores[18];
                highscores[18] = playerTwo.Text;
            }
            else if (Convert.ToInt32(score4Label.Text) > Convert.ToInt32(highscores[9]))
            {
                highscores[9] = score4Label.Text;
                highscores[19] = playerTwo.Text;
            }
            #endregion

            File.WriteAllLines(@"./highscores.sav", highscores);
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
                    card.Image = back;
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
            firstClicked.Image = back;
            secondClicked.Image = back;
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

        private void Continue_Button_Click(object sender, EventArgs e)
        {
            string[] lines = File.ReadAllLines(@"./memory.sav");

            Game_Screen.Text = "Memory Game";
            Continue_Button.Enabled = false;
            Start_Button.Enabled = false;
            Game_Screen.MaximizeBox = false;
            Game_Screen.Icon = this.Icon;
            Game_Screen.StartPosition = FormStartPosition.CenterScreen;
            Game_Screen.Size = new Size(650, 665);
            Game_Screen.Show();
            // Maakt de FormClosing event aan
            Game_Screen.FormClosing += new FormClosingEventHandler(Game_Screen_FormClosing);

            playerOne.Text = lines[3];
            playerTwo.Text = lines[4];

            switch (lines[22])
            {
                case "0":
                    back = global::Memory_Project.Properties.Resources.back;
                    back.Tag = "0";
                    break;
                case "1":
                    back = global::Memory_Project.Properties.Resources.background1;
                    back.Tag = "1";
                    break;
                case "2":
                    back = global::Memory_Project.Properties.Resources.background2;
                    back.Tag = "2";
                    break;
                case "3":
                    back = global::Memory_Project.Properties.Resources.background3;
                    back.Tag = "3";
                    break;
                case "4":
                    back = global::Memory_Project.Properties.Resources.background4;
                    back.Tag = "4";
                    break;
                case "5":
                    back = global::Memory_Project.Properties.Resources.background5;
                    back.Tag = "5";
                    break;
                case "6":
                    back = global::Memory_Project.Properties.Resources.background6;
                    back.Tag = "6";
                    break;
                case "7":
                    back = global::Memory_Project.Properties.Resources.background7;
                    back.Tag = "7";
                    break;
            }
            if (GridInit == false)
            {
                GridInit = true;

                InitUI();

                spelersLabel.Text = lines[0];
                score2Label.Text = lines[1];
                score4Label.Text = lines[2];
                beurt = Convert.ToInt32(lines[21]);
                

                int x = 5;
                int y = 5;
                int i = 0;
                int l = 5;
                #region Foto's
                icons.Add(global::Memory_Project.Properties.Resources.Cyan);
                icons.Add(global::Memory_Project.Properties.Resources.Cyan);
                icons[0].Tag = "0";
                icons[1].Tag = "0";
                icons.Add(global::Memory_Project.Properties.Resources.LBlue);
                icons.Add(global::Memory_Project.Properties.Resources.LBlue);
                icons[2].Tag = "1";
                icons[3].Tag = "1";
                icons.Add(global::Memory_Project.Properties.Resources.LGreen);
                icons.Add(global::Memory_Project.Properties.Resources.LGreen);
                icons[4].Tag = "2";
                icons[5].Tag = "2";
                icons.Add(global::Memory_Project.Properties.Resources.Orange);
                icons.Add(global::Memory_Project.Properties.Resources.Orange);
                icons[6].Tag = "3";
                icons[7].Tag = "3";
                icons.Add(global::Memory_Project.Properties.Resources.Pink);
                icons.Add(global::Memory_Project.Properties.Resources.Pink);
                icons[8].Tag = "4";
                icons[9].Tag = "4";
                icons.Add(global::Memory_Project.Properties.Resources.Purple);
                icons.Add(global::Memory_Project.Properties.Resources.Purple);
                icons[10].Tag = "5";
                icons[11].Tag = "5";
                icons.Add(global::Memory_Project.Properties.Resources.Red);
                icons.Add(global::Memory_Project.Properties.Resources.Red);
                icons[12].Tag = "6";
                icons[13].Tag = "6";
                icons.Add(global::Memory_Project.Properties.Resources.Yellow);
                icons.Add(global::Memory_Project.Properties.Resources.Yellow);
                icons[14].Tag = "7";
                icons[15].Tag = "7";
                #endregion Foto's 
                for (int r = 0; r < 4; r++)
                {
                    for (int c = 0; c < 4; c++)
                    {
                        PictureBox card = new PictureBox();
                        card.BorderStyle = BorderStyle.Fixed3D;
                        switch (lines[l])
                        {
                            case "0":
                                card.BackgroundImage = icons[0];
                                location[i] = 0.ToString();
                                break;
                            case "1":
                                card.BackgroundImage = icons[2];
                                location[i] = 1.ToString();
                                break;
                            case "2":
                                card.BackgroundImage = icons[4];
                                location[i] = 2.ToString();
                                break;
                            case "3":
                                card.BackgroundImage = icons[6];
                                location[i] = 3.ToString();
                                break;
                            case "4":
                                card.BackgroundImage = icons[8];
                                location[i] = 4.ToString();
                                break;
                            case "5":
                                card.BackgroundImage = icons[10];
                                location[i] = 5.ToString();
                                break;
                            case "6":
                                card.BackgroundImage = icons[12];
                                location[i] = 6.ToString();
                                break;
                            case "7":
                                card.BackgroundImage = icons[14];
                                location[i] = 7.ToString();
                                break;
                        }
                        card.Image = back;
                        card.Size = new Size(104, 154);
                        card.Location = new Point(x, y);
                        card.Cursor = Cursors.Hand;
                        card.Click += new EventHandler(this.Card_Click);

                        Game_Screen.Controls.Add(card);

                        if (lines[l] == "")
                        {
                            Game_Screen.Controls.Remove(card);
                        }

                        x += 105;

                        i++;
                        l++;
                    }
                    x = 5;
                    y += 155;
                }
                Label testLabel = new Label();
                testLabel.Location = new Point(640, 640);
                testLabel.Text = location[0];
                Game_Screen.Controls.Add(testLabel);
            }
        }

        private void Highscores_Button_Click(object sender, EventArgs e)
        {
            Highscores_Button.Enabled = false;

            Highscore_Screen.FormClosing += new FormClosingEventHandler(Highscore_Screen_FormClosing);

            Highscore_Screen.StartPosition = FormStartPosition.CenterScreen;
            Highscore_Screen.MaximizeBox = false;
            Highscore_Screen.Size = new Size(220, 300);
            Highscore_Screen.FormBorderStyle = FormBorderStyle.FixedSingle;
            Highscore_Screen.Icon = this.Icon;
            Highscore_Screen.Text = "Highscores";
            Highscore_Screen.Show();

            if (!File.Exists(@"./highscores.sav"))
            {
                string[] q = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-" };
                File.Create(@"./highscores.sav").Close();
                File.WriteAllLines(@"./highscores.sav", q);
            }

            string[] highscores = File.ReadAllLines(@"./highscores.sav");

            int x = 5, y = 10;
            for (int i = 0; i < 10; i++)
            {
                Label highscoreNumberLabel = new Label();
                highscoreNumberLabel.Location = new Point(x, y);
                highscoreNumberLabel.Text = (i + 1) + ".";
                highscoreNumberLabel.Size = new Size(25, 20);
                Highscore_Screen.Controls.Add(highscoreNumberLabel);
                y += 25;
            }

            x = 30;
            y = 10;
            for (int i = 0; i < 10; i++)
            {
                TextBox highscoreNameLabel = new TextBox();
                highscoreNameLabel.ReadOnly = true;
                highscoreNameLabel.Location = new Point(x, y);
                highscoreNameLabel.Text = highscores[(i + 10)];
                highscoreNameLabel.Size = new Size(70, 20);
                Highscore_Screen.Controls.Add(highscoreNameLabel);
                y += 25;
            }

            x = 100;
            y = 10;
            for (int i = 0; i < 10; i++)
            {
                TextBox highscoreLabel = new TextBox();
                highscoreLabel.ReadOnly = true;
                highscoreLabel.Location = new Point(x, y);
                highscoreLabel.Text = highscores[i];
                Highscore_Screen.Controls.Add(highscoreLabel);
                y += 25;
            }
        }

        private void Highscore_Screen_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Highscores_Button.Enabled = true;
                e.Cancel = true;
                Highscore_Screen.Hide();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Stop();
            Game_Screen.Controls.Remove(firstClicked);
            Game_Screen.Controls.Remove(secondClicked);
            firstClicked = null;
            secondClicked = null;
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
