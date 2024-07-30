namespace BSFiberConcrete.Calc
{
    partial class BSUnitCalculator
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
            this.numFrom = new System.Windows.Forms.NumericUpDown();
            this.numTo = new System.Windows.Forms.NumericUpDown();
            this.labelFrom = new System.Windows.Forms.Label();
            this.labelTo = new System.Windows.Forms.Label();
            this.comboBoxFrom = new System.Windows.Forms.ComboBox();
            this.comboBoxTo = new System.Windows.Forms.ComboBox();
            this.comboBoxTo2 = new System.Windows.Forms.ComboBox();
            this.labelTo2 = new System.Windows.Forms.Label();
            this.numTo2 = new System.Windows.Forms.NumericUpDown();
            this.comboBoxFromF = new System.Windows.Forms.ComboBox();
            this.labelFromF = new System.Windows.Forms.Label();
            this.numFromF = new System.Windows.Forms.NumericUpDown();
            this.comboBoxToF = new System.Windows.Forms.ComboBox();
            this.labelToF = new System.Windows.Forms.Label();
            this.numToF = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTo2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFromF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numToF)).BeginInit();
            this.SuspendLayout();
            // 
            // numFrom
            // 
            this.numFrom.DecimalPlaces = 4;
            this.numFrom.Location = new System.Drawing.Point(146, 34);
            this.numFrom.Maximum = new decimal(new int[] {
            1569325056,
            23283064,
            0,
            0});
            this.numFrom.Minimum = new decimal(new int[] {
            1569325056,
            23283064,
            0,
            -2147483648});
            this.numFrom.Name = "numFrom";
            this.numFrom.Size = new System.Drawing.Size(120, 20);
            this.numFrom.TabIndex = 0;
            this.numFrom.ValueChanged += new System.EventHandler(this.numFrom_ValueChanged);
            // 
            // numTo
            // 
            this.numTo.DecimalPlaces = 4;
            this.numTo.Location = new System.Drawing.Point(146, 79);
            this.numTo.Maximum = new decimal(new int[] {
            1569325056,
            23283064,
            0,
            0});
            this.numTo.Minimum = new decimal(new int[] {
            1569325056,
            23283064,
            0,
            -2147483648});
            this.numTo.Name = "numTo";
            this.numTo.Size = new System.Drawing.Size(120, 20);
            this.numTo.TabIndex = 1;
            // 
            // labelFrom
            // 
            this.labelFrom.AutoSize = true;
            this.labelFrom.Location = new System.Drawing.Point(49, 36);
            this.labelFrom.Name = "labelFrom";
            this.labelFrom.Size = new System.Drawing.Size(21, 13);
            this.labelFrom.TabIndex = 2;
            this.labelFrom.Text = "Из";
            // 
            // labelTo
            // 
            this.labelTo.AutoSize = true;
            this.labelTo.Location = new System.Drawing.Point(49, 79);
            this.labelTo.Name = "labelTo";
            this.labelTo.Size = new System.Drawing.Size(14, 13);
            this.labelTo.TabIndex = 3;
            this.labelTo.Text = "В";
            // 
            // comboBoxFrom
            // 
            this.comboBoxFrom.FormattingEnabled = true;
            this.comboBoxFrom.Location = new System.Drawing.Point(69, 33);
            this.comboBoxFrom.Name = "comboBoxFrom";
            this.comboBoxFrom.Size = new System.Drawing.Size(71, 21);
            this.comboBoxFrom.TabIndex = 4;
            this.comboBoxFrom.Text = "МПа";
            // 
            // comboBoxTo
            // 
            this.comboBoxTo.FormattingEnabled = true;
            this.comboBoxTo.Location = new System.Drawing.Point(69, 77);
            this.comboBoxTo.Name = "comboBoxTo";
            this.comboBoxTo.Size = new System.Drawing.Size(71, 21);
            this.comboBoxTo.TabIndex = 5;
            this.comboBoxTo.Text = "кгс/см2";
            // 
            // comboBoxTo2
            // 
            this.comboBoxTo2.FormattingEnabled = true;
            this.comboBoxTo2.Location = new System.Drawing.Point(69, 104);
            this.comboBoxTo2.Name = "comboBoxTo2";
            this.comboBoxTo2.Size = new System.Drawing.Size(71, 21);
            this.comboBoxTo2.TabIndex = 8;
            this.comboBoxTo2.Text = "кН/см2";
            // 
            // labelTo2
            // 
            this.labelTo2.AutoSize = true;
            this.labelTo2.Location = new System.Drawing.Point(49, 106);
            this.labelTo2.Name = "labelTo2";
            this.labelTo2.Size = new System.Drawing.Size(14, 13);
            this.labelTo2.TabIndex = 7;
            this.labelTo2.Text = "В";
            // 
            // numTo2
            // 
            this.numTo2.DecimalPlaces = 4;
            this.numTo2.Location = new System.Drawing.Point(146, 106);
            this.numTo2.Maximum = new decimal(new int[] {
            1569325056,
            23283064,
            0,
            0});
            this.numTo2.Minimum = new decimal(new int[] {
            1569325056,
            23283064,
            0,
            -2147483648});
            this.numTo2.Name = "numTo2";
            this.numTo2.Size = new System.Drawing.Size(120, 20);
            this.numTo2.TabIndex = 6;
            // 
            // comboBoxFromF
            // 
            this.comboBoxFromF.FormattingEnabled = true;
            this.comboBoxFromF.Location = new System.Drawing.Point(345, 35);
            this.comboBoxFromF.Name = "comboBoxFromF";
            this.comboBoxFromF.Size = new System.Drawing.Size(63, 21);
            this.comboBoxFromF.TabIndex = 11;
            this.comboBoxFromF.Text = "кН";
            // 
            // labelFromF
            // 
            this.labelFromF.AutoSize = true;
            this.labelFromF.Location = new System.Drawing.Point(318, 37);
            this.labelFromF.Name = "labelFromF";
            this.labelFromF.Size = new System.Drawing.Size(21, 13);
            this.labelFromF.TabIndex = 10;
            this.labelFromF.Text = "Из";
            // 
            // numFromF
            // 
            this.numFromF.DecimalPlaces = 4;
            this.numFromF.Location = new System.Drawing.Point(414, 37);
            this.numFromF.Maximum = new decimal(new int[] {
            1569325056,
            23283064,
            0,
            0});
            this.numFromF.Minimum = new decimal(new int[] {
            1569325056,
            23283064,
            0,
            -2147483648});
            this.numFromF.Name = "numFromF";
            this.numFromF.Size = new System.Drawing.Size(98, 20);
            this.numFromF.TabIndex = 9;
            this.numFromF.ValueChanged += new System.EventHandler(this.numFromF_ValueChanged);
            // 
            // comboBoxToF
            // 
            this.comboBoxToF.FormattingEnabled = true;
            this.comboBoxToF.Location = new System.Drawing.Point(345, 77);
            this.comboBoxToF.Name = "comboBoxToF";
            this.comboBoxToF.Size = new System.Drawing.Size(63, 21);
            this.comboBoxToF.TabIndex = 14;
            this.comboBoxToF.Text = "кгс";
            // 
            // labelToF
            // 
            this.labelToF.AutoSize = true;
            this.labelToF.Location = new System.Drawing.Point(325, 81);
            this.labelToF.Name = "labelToF";
            this.labelToF.Size = new System.Drawing.Size(14, 13);
            this.labelToF.TabIndex = 13;
            this.labelToF.Text = "В";
            // 
            // numToF
            // 
            this.numToF.DecimalPlaces = 4;
            this.numToF.Location = new System.Drawing.Point(414, 79);
            this.numToF.Maximum = new decimal(new int[] {
            1569325056,
            23283064,
            0,
            0});
            this.numToF.Minimum = new decimal(new int[] {
            1569325056,
            23283064,
            0,
            -2147483648});
            this.numToF.Name = "numToF";
            this.numToF.Size = new System.Drawing.Size(98, 20);
            this.numToF.TabIndex = 12;
            // 
            // BSUnitCalculator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 174);
            this.Controls.Add(this.comboBoxToF);
            this.Controls.Add(this.labelToF);
            this.Controls.Add(this.numToF);
            this.Controls.Add(this.comboBoxFromF);
            this.Controls.Add(this.labelFromF);
            this.Controls.Add(this.numFromF);
            this.Controls.Add(this.comboBoxTo2);
            this.Controls.Add(this.labelTo2);
            this.Controls.Add(this.numTo2);
            this.Controls.Add(this.comboBoxTo);
            this.Controls.Add(this.comboBoxFrom);
            this.Controls.Add(this.labelTo);
            this.Controls.Add(this.labelFrom);
            this.Controls.Add(this.numTo);
            this.Controls.Add(this.numFrom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "BSUnitCalculator";
            this.Text = "Перевод единиц";
            ((System.ComponentModel.ISupportInitialize)(this.numFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTo2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFromF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numToF)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numFrom;
        private System.Windows.Forms.NumericUpDown numTo;
        private System.Windows.Forms.Label labelFrom;
        private System.Windows.Forms.Label labelTo;
        private System.Windows.Forms.ComboBox comboBoxFrom;
        private System.Windows.Forms.ComboBox comboBoxTo;
        private System.Windows.Forms.ComboBox comboBoxTo2;
        private System.Windows.Forms.Label labelTo2;
        private System.Windows.Forms.NumericUpDown numTo2;
        private System.Windows.Forms.ComboBox comboBoxFromF;
        private System.Windows.Forms.Label labelFromF;
        private System.Windows.Forms.NumericUpDown numFromF;
        private System.Windows.Forms.ComboBox comboBoxToF;
        private System.Windows.Forms.Label labelToF;
        private System.Windows.Forms.NumericUpDown numToF;
    }
}