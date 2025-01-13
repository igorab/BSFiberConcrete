namespace BSFiberConcrete
{
    partial class CoefInform
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CoefInform));
            this.panelCoefInform = new System.Windows.Forms.Panel();
            this.pictureBoxCoefInform = new System.Windows.Forms.PictureBox();
            this.panelCoefInform.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCoefInform)).BeginInit();
            this.SuspendLayout();
            // 
            // panelCoefInform
            // 
            this.panelCoefInform.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelCoefInform.AutoScroll = true;
            this.panelCoefInform.Controls.Add(this.pictureBoxCoefInform);
            this.panelCoefInform.Location = new System.Drawing.Point(0, 0);
            this.panelCoefInform.Name = "panelCoefInform";
            this.panelCoefInform.Size = new System.Drawing.Size(784, 486);
            this.panelCoefInform.TabIndex = 0;
            // 
            // pictureBoxCoefInform
            // 
            this.pictureBoxCoefInform.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxCoefInform.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxCoefInform.Image")));
            this.pictureBoxCoefInform.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxCoefInform.Name = "pictureBoxCoefInform";
            this.pictureBoxCoefInform.Size = new System.Drawing.Size(749, 440);
            this.pictureBoxCoefInform.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxCoefInform.TabIndex = 0;
            this.pictureBoxCoefInform.TabStop = false;
            // 
            // CoefInform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 498);
            this.Controls.Add(this.panelCoefInform);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(880, 500);
            this.Name = "CoefInform";
            this.Text = "Коэффициенты условий работы";
            this.panelCoefInform.ResumeLayout(false);
            this.panelCoefInform.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCoefInform)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelCoefInform;
        private System.Windows.Forms.PictureBox pictureBoxCoefInform;
    }
}