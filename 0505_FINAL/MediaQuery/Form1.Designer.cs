namespace MediaQuery
{
    partial class Form1
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
            this.pictureBox_queryImage = new System.Windows.Forms.PictureBox();
            this.label_studentIDs = new System.Windows.Forms.Label();
            this.label_queryImage = new System.Windows.Forms.Label();
            this.label_searchImage = new System.Windows.Forms.Label();
            this.pictureBox_searchImage = new System.Windows.Forms.PictureBox();
            this.pictureBox_histogram1 = new System.Windows.Forms.PictureBox();
            this.pictureBox_histogram2 = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_queryImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_searchImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_histogram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_histogram2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox_queryImage
            // 
            this.pictureBox_queryImage.Location = new System.Drawing.Point(12, 24);
            this.pictureBox_queryImage.Name = "pictureBox_queryImage";
            this.pictureBox_queryImage.Size = new System.Drawing.Size(352, 288);
            this.pictureBox_queryImage.TabIndex = 1;
            this.pictureBox_queryImage.TabStop = false;
            // 
            // label_studentIDs
            // 
            this.label_studentIDs.AutoSize = true;
            this.label_studentIDs.Location = new System.Drawing.Point(9, 315);
            this.label_studentIDs.Name = "label_studentIDs";
            this.label_studentIDs.Size = new System.Drawing.Size(35, 13);
            this.label_studentIDs.TabIndex = 11;
            this.label_studentIDs.Text = "label2";
            // 
            // label_queryImage
            // 
            this.label_queryImage.AutoSize = true;
            this.label_queryImage.Location = new System.Drawing.Point(12, 8);
            this.label_queryImage.Name = "label_queryImage";
            this.label_queryImage.Size = new System.Drawing.Size(35, 13);
            this.label_queryImage.TabIndex = 12;
            this.label_queryImage.Text = "label3";
            // 
            // label_searchImage
            // 
            this.label_searchImage.AutoSize = true;
            this.label_searchImage.Location = new System.Drawing.Point(370, 8);
            this.label_searchImage.Name = "label_searchImage";
            this.label_searchImage.Size = new System.Drawing.Size(35, 13);
            this.label_searchImage.TabIndex = 14;
            this.label_searchImage.Text = "label4";
            // 
            // pictureBox_searchImage
            // 
            this.pictureBox_searchImage.Location = new System.Drawing.Point(370, 24);
            this.pictureBox_searchImage.Name = "pictureBox_searchImage";
            this.pictureBox_searchImage.Size = new System.Drawing.Size(352, 288);
            this.pictureBox_searchImage.TabIndex = 13;
            this.pictureBox_searchImage.TabStop = false;
            // 
            // pictureBox_histogram1
            // 
            this.pictureBox_histogram1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pictureBox_histogram1.Location = new System.Drawing.Point(12, 331);
            this.pictureBox_histogram1.Name = "pictureBox_histogram1";
            this.pictureBox_histogram1.Size = new System.Drawing.Size(224, 128);
            this.pictureBox_histogram1.TabIndex = 15;
            this.pictureBox_histogram1.TabStop = false;
            this.pictureBox_histogram1.Visible = false;
            this.pictureBox_histogram1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_histogram1_paint);
            // 
            // pictureBox_histogram2
            // 
            this.pictureBox_histogram2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pictureBox_histogram2.Location = new System.Drawing.Point(370, 331);
            this.pictureBox_histogram2.Name = "pictureBox_histogram2";
            this.pictureBox_histogram2.Size = new System.Drawing.Size(224, 128);
            this.pictureBox_histogram2.TabIndex = 16;
            this.pictureBox_histogram2.TabStop = false;
            this.pictureBox_histogram2.Visible = false;
            this.pictureBox_histogram2.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_histogram2_paint);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(622, 331);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 17;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 467);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.pictureBox_histogram2);
            this.Controls.Add(this.pictureBox_histogram1);
            this.Controls.Add(this.label_searchImage);
            this.Controls.Add(this.pictureBox_searchImage);
            this.Controls.Add(this.label_queryImage);
            this.Controls.Add(this.label_studentIDs);
            this.Controls.Add(this.pictureBox_queryImage);
            this.Name = "Form1";
            this.Text = "CSCI 576 Final Project";
            this.Shown += new System.EventHandler(this.onFormShown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_queryImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_searchImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_histogram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_histogram2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_queryImage;
        private System.Windows.Forms.Label label_studentIDs;
        private System.Windows.Forms.Label label_queryImage;
        private System.Windows.Forms.Label label_searchImage;
        private System.Windows.Forms.PictureBox pictureBox_searchImage;
        private System.Windows.Forms.PictureBox pictureBox_histogram1;
        private System.Windows.Forms.PictureBox pictureBox_histogram2;
        private System.Windows.Forms.TextBox textBox1;
    }
}

