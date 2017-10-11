﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Memory_Project
{
    public partial class Start_Screen : Form
    {
        bool GridInit = false;

        Form Game_Screen = new Form();

        Random rng = new Random();

        List<Image> icons = new List<Image>();

        PictureBox firstClicked = null;
        PictureBox secondClicked = null;

        public Start_Screen()
        {
            InitializeComponent();

            this.CenterToScreen();

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

        private void Game_Screen_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Start_Button.Enabled = true;
                e.Cancel = true;
                Game_Screen.Hide();
            }
        }

        private void Start_Button_Click(object sender, EventArgs e)
        {
            Start_Button.Enabled = false;

            Game_Screen.Text = "Memory Game";
            Game_Screen.Icon = this.Icon;
            Game_Screen.StartPosition = FormStartPosition.CenterScreen;
            Game_Screen.Size = new Size(440, 665);
            Game_Screen.Show();
            Game_Screen.FormClosing += new FormClosingEventHandler(Game_Screen_FormClosing);

            if (GridInit == false)
            {
                InitGrid();
            }
        }

        private void Card_Click(object sender, EventArgs e)
        {
            PictureBox clickedPic = sender as PictureBox;
            clickedPic.Enabled = false;
            clickedPic.Image = null;
            if(firstClicked == null)
            {
                firstClicked = clickedPic;
                return;
            }

            if(firstClicked != null && secondClicked == null)
            {
                secondClicked = clickedPic;
                if (firstClicked.BackgroundImage.Tag != secondClicked.BackgroundImage.Tag)
                {
                    foreach(PictureBox pic in Game_Screen.Controls)
                    {
                        pic.Enabled = false;
                    }
                    timer1.Start();
                }
                else
                {
                    foreach(PictureBox pic in Game_Screen.Controls)
                    {
                        if(pic.Image == null)
                        {
                            pic.Enabled = false;
                        }
                    }
                    CheckWinner();
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }
                return;
            }   
        }

        void CheckWinner()
        {
            foreach(PictureBox pic in Game_Screen.Controls)
            {
                if(pic.Image != null)
                {
                    return;
                }
            }

            MessageBox.Show("Je hebt gewonnen!", "Gefeliciteerd");
            Game_Screen.Close();
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

            Start_Button.Enabled = true;
        }
        void InitGrid()
        {
            GridInit = true;

            int x = 5;
            int y = 5;

            for(int r = 0; r < 4; r++)
            {
                for(int c = 0; c < 4; c++)
                {
                    PictureBox card = new PictureBox();
                    card.BorderStyle = BorderStyle.Fixed3D;
                    int rngNum = rng.Next(icons.Count);
                    card.BackgroundImage = icons[rngNum];
                    icons.RemoveAt(rngNum);
                    card.Image = Image.FromFile(@"./imgs/back.png");
                    card.Size = new Size(100, 150);
                    card.Location = new Point(x, y);
                    card.Cursor = Cursors.Hand;
                    card.Click += new EventHandler(this.Card_Click);

                    Game_Screen.Controls.Add(card);

                    x += 105;
                }
                x = 5;
                y += 155;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach(PictureBox pic in Game_Screen.Controls)
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
        }

        private void GitHub_Link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/pprotas/Memory_Game_Project");
        }
    }
}
