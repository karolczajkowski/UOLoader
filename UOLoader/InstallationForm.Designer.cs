namespace UOLoader
{
   partial class InstallationForm
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
         this.InstallationLabel = new System.Windows.Forms.Label();
         this.InstructionsLabel = new System.Windows.Forms.Label();
         this.textBox1 = new System.Windows.Forms.TextBox();
         this.ShardLabel = new System.Windows.Forms.Label();
         this.pictureBox2 = new System.Windows.Forms.PictureBox();
         this.InstallationPathLabel = new System.Windows.Forms.Label();
         this.ContinueButton = new System.Windows.Forms.PictureBox();
         this.pictureBox1 = new System.Windows.Forms.PictureBox();
         this.InstallLabel = new System.Windows.Forms.Label();
         ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.ContinueButton)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
         this.SuspendLayout();
         // 
         // InstallationLabel
         // 
         this.InstallationLabel.AutoSize = true;
         this.InstallationLabel.BackColor = System.Drawing.Color.Transparent;
         this.InstallationLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.InstallationLabel.ForeColor = System.Drawing.SystemColors.Desktop;
         this.InstallationLabel.Location = new System.Drawing.Point(79, 38);
         this.InstallationLabel.MaximumSize = new System.Drawing.Size(600, 0);
         this.InstallationLabel.Name = "InstallationLabel";
         this.InstallationLabel.Size = new System.Drawing.Size(38, 13);
         this.InstallationLabel.TabIndex = 1;
         this.InstallationLabel.Text = "label1";
         // 
         // InstructionsLabel
         // 
         this.InstructionsLabel.AutoSize = true;
         this.InstructionsLabel.BackColor = System.Drawing.Color.Transparent;
         this.InstructionsLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.InstructionsLabel.ForeColor = System.Drawing.SystemColors.Desktop;
         this.InstructionsLabel.Location = new System.Drawing.Point(79, 89);
         this.InstructionsLabel.MaximumSize = new System.Drawing.Size(600, 0);
         this.InstructionsLabel.Name = "InstructionsLabel";
         this.InstructionsLabel.Size = new System.Drawing.Size(38, 13);
         this.InstructionsLabel.TabIndex = 2;
         this.InstructionsLabel.Text = "label1";
         // 
         // textBox1
         // 
         this.textBox1.Location = new System.Drawing.Point(82, 160);
         this.textBox1.Name = "textBox1";
         this.textBox1.Size = new System.Drawing.Size(394, 20);
         this.textBox1.TabIndex = 3;
         // 
         // ShardLabel
         // 
         this.ShardLabel.AutoSize = true;
         this.ShardLabel.BackColor = System.Drawing.Color.Transparent;
         this.ShardLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.ShardLabel.ForeColor = System.Drawing.Color.Black;
         this.ShardLabel.Location = new System.Drawing.Point(212, 9);
         this.ShardLabel.Name = "ShardLabel";
         this.ShardLabel.Size = new System.Drawing.Size(45, 17);
         this.ShardLabel.TabIndex = 5;
         this.ShardLabel.Text = "label1";
         // 
         // pictureBox2
         // 
         this.pictureBox2.Image = global::UOLoader.UOLoaderResources.BrowseButton;
         this.pictureBox2.Location = new System.Drawing.Point(482, 158);
         this.pictureBox2.Name = "pictureBox2";
         this.pictureBox2.Size = new System.Drawing.Size(30, 22);
         this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
         this.pictureBox2.TabIndex = 6;
         this.pictureBox2.TabStop = false;
         this.pictureBox2.MouseEnter += new System.EventHandler(this.pictureBox2_MouseEnter);
         this.pictureBox2.MouseLeave += new System.EventHandler(this.pictureBox2_MouseLeave);
         // 
         // InstallationPathLabel
         // 
         this.InstallationPathLabel.AutoSize = true;
         this.InstallationPathLabel.BackColor = System.Drawing.Color.Transparent;
         this.InstallationPathLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.InstallationPathLabel.ForeColor = System.Drawing.SystemColors.Desktop;
         this.InstallationPathLabel.Location = new System.Drawing.Point(79, 144);
         this.InstallationPathLabel.Name = "InstallationPathLabel";
         this.InstallationPathLabel.Size = new System.Drawing.Size(38, 13);
         this.InstallationPathLabel.TabIndex = 7;
         this.InstallationPathLabel.Text = "label1";
         // 
         // ContinueButton
         // 
         this.ContinueButton.Image = global::UOLoader.UOLoaderResources.ContinueButton;
         this.ContinueButton.Location = new System.Drawing.Point(534, 158);
         this.ContinueButton.Name = "ContinueButton";
         this.ContinueButton.Size = new System.Drawing.Size(19, 21);
         this.ContinueButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
         this.ContinueButton.TabIndex = 9;
         this.ContinueButton.TabStop = false;
         this.ContinueButton.Visible = false;
         this.ContinueButton.Click += new System.EventHandler(this.ContinueButton_Click);
         this.ContinueButton.MouseEnter += new System.EventHandler(this.ContinueButton_MouseEnter);
         this.ContinueButton.MouseLeave += new System.EventHandler(this.ContinueButton_MouseLeave);
         // 
         // pictureBox1
         // 
         this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
         this.pictureBox1.Image = global::UOLoader.UOLoaderResources.uo_transparent;
         this.pictureBox1.Location = new System.Drawing.Point(9, 63);
         this.pictureBox1.Name = "pictureBox1";
         this.pictureBox1.Size = new System.Drawing.Size(64, 64);
         this.pictureBox1.TabIndex = 8;
         this.pictureBox1.TabStop = false;
         // 
         // InstallLabel
         // 
         this.InstallLabel.AutoSize = true;
         this.InstallLabel.BackColor = System.Drawing.Color.Transparent;
         this.InstallLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.InstallLabel.ForeColor = System.Drawing.SystemColors.Desktop;
         this.InstallLabel.Location = new System.Drawing.Point(559, 163);
         this.InstallLabel.MaximumSize = new System.Drawing.Size(600, 0);
         this.InstallLabel.Name = "InstallLabel";
         this.InstallLabel.Size = new System.Drawing.Size(62, 13);
         this.InstallLabel.TabIndex = 10;
         this.InstallLabel.Text = "Kontunuuj";
         this.InstallLabel.Visible = false;
         // 
         // InstallationForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.BackColor = System.Drawing.Color.Black;
         this.BackgroundImage = global::UOLoader.UOLoaderResources.InstallationBackground;
         this.ClientSize = new System.Drawing.Size(658, 207);
         this.Controls.Add(this.InstallLabel);
         this.Controls.Add(this.ContinueButton);
         this.Controls.Add(this.pictureBox1);
         this.Controls.Add(this.InstallationPathLabel);
         this.Controls.Add(this.pictureBox2);
         this.Controls.Add(this.ShardLabel);
         this.Controls.Add(this.textBox1);
         this.Controls.Add(this.InstructionsLabel);
         this.Controls.Add(this.InstallationLabel);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
         this.Name = "InstallationForm";
         this.Text = "InstallationForm";
         ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.ContinueButton)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion
      private System.Windows.Forms.Label InstallationLabel;
      private System.Windows.Forms.Label InstructionsLabel;
      private System.Windows.Forms.TextBox textBox1;
      private System.Windows.Forms.Label ShardLabel;
      private System.Windows.Forms.PictureBox pictureBox2;
      private System.Windows.Forms.Label InstallationPathLabel;
      private System.Windows.Forms.PictureBox ContinueButton;
      private System.Windows.Forms.PictureBox pictureBox1;
      private System.Windows.Forms.Label InstallLabel;
   }
}