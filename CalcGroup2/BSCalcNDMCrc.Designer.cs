namespace BSFiberConcrete.CalcGroup2
{
    partial class BSCalcNDMCrc
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.numFi1 = new System.Windows.Forms.NumericUpDown();
            this.numFi3 = new System.Windows.Forms.NumericUpDown();
            this.numPsiS = new System.Windows.Forms.NumericUpDown();
            this.linkFi3 = new System.Windows.Forms.LinkLabel();
            this.linkPsiS = new System.Windows.Forms.LinkLabel();
            this.linkFi1 = new System.Windows.Forms.LinkLabel();
            this.buttonOk = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFi1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFi3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPsiS)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.14008F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.85992F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 151F));
            this.tableLayoutPanel1.Controls.Add(this.numFi1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.numFi3, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.numPsiS, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.linkFi3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.linkPsiS, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.linkFi1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(25, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(416, 142);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // numFi1
            // 
            this.numFi1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numFi1.DecimalPlaces = 2;
            this.numFi1.Location = new System.Drawing.Point(141, 11);
            this.numFi1.Name = "numFi1";
            this.numFi1.Size = new System.Drawing.Size(120, 20);
            this.numFi1.TabIndex = 0;
            this.numFi1.Value = new decimal(new int[] {
            14,
            0,
            0,
            65536});
            // 
            // numFi3
            // 
            this.numFi3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numFi3.DecimalPlaces = 2;
            this.numFi3.Location = new System.Drawing.Point(141, 54);
            this.numFi3.Name = "numFi3";
            this.numFi3.Size = new System.Drawing.Size(120, 20);
            this.numFi3.TabIndex = 1;
            this.numFi3.Value = new decimal(new int[] {
            12,
            0,
            0,
            65536});
            // 
            // numPsiS
            // 
            this.numPsiS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numPsiS.DecimalPlaces = 2;
            this.numPsiS.Location = new System.Drawing.Point(141, 104);
            this.numPsiS.Name = "numPsiS";
            this.numPsiS.Size = new System.Drawing.Size(120, 20);
            this.numPsiS.TabIndex = 2;
            this.numPsiS.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // linkFi3
            // 
            this.linkFi3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.linkFi3.AutoSize = true;
            this.linkFi3.Location = new System.Drawing.Point(108, 58);
            this.linkFi3.Name = "linkFi3";
            this.linkFi3.Size = new System.Drawing.Size(27, 13);
            this.linkFi3.TabIndex = 4;
            this.linkFi3.TabStop = true;
            this.linkFi3.Text = "φ3 :";
            // 
            // linkPsiS
            // 
            this.linkPsiS.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.linkPsiS.AutoSize = true;
            this.linkPsiS.Location = new System.Drawing.Point(106, 107);
            this.linkPsiS.Name = "linkPsiS";
            this.linkPsiS.Size = new System.Drawing.Size(29, 13);
            this.linkPsiS.TabIndex = 5;
            this.linkPsiS.TabStop = true;
            this.linkPsiS.Text = "ψ s :";
            this.linkPsiS.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkPsiS_LinkClicked);
            // 
            // linkFi1
            // 
            this.linkFi1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.linkFi1.AutoSize = true;
            this.linkFi1.Location = new System.Drawing.Point(108, 15);
            this.linkFi1.Name = "linkFi1";
            this.linkFi1.Size = new System.Drawing.Size(27, 13);
            this.linkFi1.TabIndex = 3;
            this.linkFi1.TabStop = true;
            this.linkFi1.Text = "φ1 :";
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(372, 172);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 1;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // BSCalcNDMCrc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 215);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "BSCalcNDMCrc";
            this.Text = "Трещиностойкость";
            this.Load += new System.EventHandler(this.BSCalcNDMCrc_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFi1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFi3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPsiS)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.NumericUpDown numFi1;
        private System.Windows.Forms.NumericUpDown numFi3;
        private System.Windows.Forms.NumericUpDown numPsiS;
        private System.Windows.Forms.LinkLabel linkFi1;
        private System.Windows.Forms.LinkLabel linkFi3;
        private System.Windows.Forms.LinkLabel linkPsiS;
        private System.Windows.Forms.Button buttonOk;
    }
}