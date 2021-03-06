﻿namespace Memory_Project
{
    partial class Start_Screen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Start_Screen));
            this.Start_Button = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.GitHub_Link = new System.Windows.Forms.LinkLabel();
            this.Title = new System.Windows.Forms.Label();
            this.Continue_Button = new System.Windows.Forms.Button();
            this.Highscores_Button = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Start_Button
            // 
            this.Start_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.Start_Button.Location = new System.Drawing.Point(32, 65);
            this.Start_Button.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Start_Button.Name = "Start_Button";
            this.Start_Button.Size = new System.Drawing.Size(200, 62);
            this.Start_Button.TabIndex = 0;
            this.Start_Button.Text = "Start";
            this.Start_Button.UseVisualStyleBackColor = true;
            this.Start_Button.Click += new System.EventHandler(this.Start_Button_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // GitHub_Link
            // 
            this.GitHub_Link.AutoSize = true;
            this.GitHub_Link.Location = new System.Drawing.Point(1, 268);
            this.GitHub_Link.Name = "GitHub_Link";
            this.GitHub_Link.Size = new System.Drawing.Size(90, 17);
            this.GitHub_Link.TabIndex = 1;
            this.GitHub_Link.TabStop = true;
            this.GitHub_Link.Text = "Onze GitHub";
            this.GitHub_Link.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.GitHub_Link_LinkClicked);
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Font = new System.Drawing.Font("Modern No. 20", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Title.Location = new System.Drawing.Point(31, 32);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(188, 30);
            this.Title.TabIndex = 2;
            this.Title.Text = "Memory Game";
            // 
            // Continue_Button
            // 
            this.Continue_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.Continue_Button.Location = new System.Drawing.Point(32, 134);
            this.Continue_Button.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Continue_Button.Name = "Continue_Button";
            this.Continue_Button.Size = new System.Drawing.Size(200, 62);
            this.Continue_Button.TabIndex = 3;
            this.Continue_Button.Text = "Hervat";
            this.Continue_Button.UseVisualStyleBackColor = true;
            this.Continue_Button.Click += new System.EventHandler(this.Continue_Button_Click);
            // 
            // Highscores_Button
            // 
            this.Highscores_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.Highscores_Button.Location = new System.Drawing.Point(32, 203);
            this.Highscores_Button.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Highscores_Button.Name = "Highscores_Button";
            this.Highscores_Button.Size = new System.Drawing.Size(200, 62);
            this.Highscores_Button.TabIndex = 4;
            this.Highscores_Button.Text = "Highscores";
            this.Highscores_Button.UseVisualStyleBackColor = true;
            this.Highscores_Button.Click += new System.EventHandler(this.Highscores_Button_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Memory_Project.Properties.Resources.Untitled;
            this.pictureBox1.Location = new System.Drawing.Point(255, 32);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(432, 274);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // timer2
            // 
            this.timer2.Interval = 500;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // Start_Screen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 295);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Highscores_Button);
            this.Controls.Add(this.Continue_Button);
            this.Controls.Add(this.Title);
            this.Controls.Add(this.GitHub_Link);
            this.Controls.Add(this.Start_Button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "Start_Screen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Memory Game";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Start_Button;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.LinkLabel GitHub_Link;
        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.Button Continue_Button;
        private System.Windows.Forms.Button Highscores_Button;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer2;
    }
}

