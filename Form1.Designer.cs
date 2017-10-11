namespace Memory_Project
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
            this.SuspendLayout();
            // 
            // Start_Button
            // 
            this.Start_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.Start_Button.Location = new System.Drawing.Point(31, 67);
            this.Start_Button.Name = "Start_Button";
            this.Start_Button.Size = new System.Drawing.Size(180, 100);
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
            this.GitHub_Link.Location = new System.Drawing.Point(9, 212);
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
            this.Title.Location = new System.Drawing.Point(23, 24);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(188, 30);
            this.Title.TabIndex = 2;
            this.Title.Text = "Memory Game";
            // 
            // Start_Screen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(239, 238);
            this.Controls.Add(this.Title);
            this.Controls.Add(this.GitHub_Link);
            this.Controls.Add(this.Start_Button);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Start_Screen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Start_Button;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.LinkLabel GitHub_Link;
        private System.Windows.Forms.Label Title;
    }
}

