namespace BSFiberConcrete.DeformationDiagram.UserControls
{
    partial class RebarDeformationView
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutEpsilonS = new System.Windows.Forms.TableLayoutPanel();
            this.numEps_s_ult = new System.Windows.Forms.NumericUpDown();
            this.label33 = new System.Windows.Forms.Label();
            this.labelReinforcementDeformType = new System.Windows.Forms.Label();
            this.labelReinforcementDeform = new System.Windows.Forms.Label();
            this.labelTypeDDRebar = new System.Windows.Forms.Label();
            this.labelEpsilonsult = new System.Windows.Forms.Label();
            this.labelEpsilonS2 = new System.Windows.Forms.Label();
            this.numEpsilonS2 = new System.Windows.Forms.NumericUpDown();
            this.labelEpsilonS1 = new System.Windows.Forms.Label();
            this.numEpsilonS1 = new System.Windows.Forms.NumericUpDown();
            this.labelEpsilonS0 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.numEpsilonS0 = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutEpsilonS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numEps_s_ult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEpsilonS2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEpsilonS1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEpsilonS0)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutEpsilonS
            // 
            this.tableLayoutEpsilonS.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutEpsilonS.ColumnCount = 2;
            this.tableLayoutEpsilonS.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.74359F));
            this.tableLayoutEpsilonS.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.25641F));
            this.tableLayoutEpsilonS.Controls.Add(this.numEps_s_ult, 1, 2);
            this.tableLayoutEpsilonS.Controls.Add(this.label33, 1, 1);
            this.tableLayoutEpsilonS.Controls.Add(this.labelReinforcementDeformType, 0, 7);
            this.tableLayoutEpsilonS.Controls.Add(this.labelReinforcementDeform, 1, 0);
            this.tableLayoutEpsilonS.Controls.Add(this.labelTypeDDRebar, 1, 7);
            this.tableLayoutEpsilonS.Controls.Add(this.labelEpsilonsult, 0, 2);
            this.tableLayoutEpsilonS.Controls.Add(this.labelEpsilonS2, 0, 6);
            this.tableLayoutEpsilonS.Controls.Add(this.numEpsilonS2, 1, 6);
            this.tableLayoutEpsilonS.Controls.Add(this.labelEpsilonS1, 0, 5);
            this.tableLayoutEpsilonS.Controls.Add(this.numEpsilonS1, 1, 5);
            this.tableLayoutEpsilonS.Controls.Add(this.labelEpsilonS0, 0, 4);
            this.tableLayoutEpsilonS.Controls.Add(this.label34, 1, 3);
            this.tableLayoutEpsilonS.Controls.Add(this.numEpsilonS0, 1, 4);
            this.tableLayoutEpsilonS.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutEpsilonS.Name = "tableLayoutEpsilonS";
            this.tableLayoutEpsilonS.RowCount = 8;
            this.tableLayoutEpsilonS.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutEpsilonS.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutEpsilonS.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutEpsilonS.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutEpsilonS.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutEpsilonS.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutEpsilonS.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutEpsilonS.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutEpsilonS.Size = new System.Drawing.Size(192, 276);
            this.tableLayoutEpsilonS.TabIndex = 11;
            // 
            // numEps_s_ult
            // 
            this.numEps_s_ult.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numEps_s_ult.DecimalPlaces = 8;
            this.numEps_s_ult.Enabled = false;
            this.numEps_s_ult.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numEps_s_ult.Location = new System.Drawing.Point(61, 75);
            this.numEps_s_ult.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numEps_s_ult.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.numEps_s_ult.Name = "numEps_s_ult";
            this.numEps_s_ult.Size = new System.Drawing.Size(120, 20);
            this.numEps_s_ult.TabIndex = 16;
            // 
            // label33
            // 
            this.label33.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label33.Location = new System.Drawing.Point(61, 45);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(46, 13);
            this.label33.TabIndex = 20;
            this.label33.Text = "предел:";
            // 
            // labelReinforcementDeformType
            // 
            this.labelReinforcementDeformType.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelReinforcementDeformType.AutoSize = true;
            this.labelReinforcementDeformType.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelReinforcementDeformType.Location = new System.Drawing.Point(28, 250);
            this.labelReinforcementDeformType.Name = "labelReinforcementDeformType";
            this.labelReinforcementDeformType.Size = new System.Drawing.Size(26, 13);
            this.labelReinforcementDeformType.TabIndex = 18;
            this.labelReinforcementDeformType.Text = "Тип";
            this.labelReinforcementDeformType.UseMnemonic = false;
            this.labelReinforcementDeformType.Visible = false;
            // 
            // labelReinforcementDeform
            // 
            this.labelReinforcementDeform.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelReinforcementDeform.AutoSize = true;
            this.labelReinforcementDeform.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelReinforcementDeform.Location = new System.Drawing.Point(61, 11);
            this.labelReinforcementDeform.Name = "labelReinforcementDeform";
            this.labelReinforcementDeform.Size = new System.Drawing.Size(127, 13);
            this.labelReinforcementDeform.TabIndex = 17;
            this.labelReinforcementDeform.Text = "Арматура:";
            // 
            // labelTypeDDRebar
            // 
            this.labelTypeDDRebar.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelTypeDDRebar.AutoSize = true;
            this.labelTypeDDRebar.Location = new System.Drawing.Point(61, 250);
            this.labelTypeDDRebar.Name = "labelTypeDDRebar";
            this.labelTypeDDRebar.Size = new System.Drawing.Size(84, 13);
            this.labelTypeDDRebar.TabIndex = 19;
            this.labelTypeDDRebar.Text = "Не определено";
            this.labelTypeDDRebar.Visible = false;
            // 
            // labelEpsilonsult
            // 
            this.labelEpsilonsult.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelEpsilonsult.AutoSize = true;
            this.labelEpsilonsult.Location = new System.Drawing.Point(16, 79);
            this.labelEpsilonsult.Name = "labelEpsilonsult";
            this.labelEpsilonsult.Size = new System.Drawing.Size(38, 13);
            this.labelEpsilonsult.TabIndex = 15;
            this.labelEpsilonsult.Text = "ε, s ult";
            // 
            // labelEpsilonS2
            // 
            this.labelEpsilonS2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelEpsilonS2.AutoSize = true;
            this.labelEpsilonS2.Location = new System.Drawing.Point(22, 215);
            this.labelEpsilonS2.Name = "labelEpsilonS2";
            this.labelEpsilonS2.Size = new System.Drawing.Size(32, 13);
            this.labelEpsilonS2.TabIndex = 14;
            this.labelEpsilonS2.Text = "ε, S2";
            // 
            // numEpsilonS2
            // 
            this.numEpsilonS2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numEpsilonS2.DecimalPlaces = 8;
            this.numEpsilonS2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numEpsilonS2.Location = new System.Drawing.Point(61, 211);
            this.numEpsilonS2.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numEpsilonS2.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.numEpsilonS2.Name = "numEpsilonS2";
            this.numEpsilonS2.Size = new System.Drawing.Size(120, 20);
            this.numEpsilonS2.TabIndex = 11;
            this.numEpsilonS2.Value = new decimal(new int[] {
            25,
            0,
            0,
            196608});
            this.numEpsilonS2.ValueChanged += new System.EventHandler(this.numEpsilonS2_ValueChanged);
            // 
            // labelEpsilonS1
            // 
            this.labelEpsilonS1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelEpsilonS1.AutoSize = true;
            this.labelEpsilonS1.Location = new System.Drawing.Point(22, 181);
            this.labelEpsilonS1.Name = "labelEpsilonS1";
            this.labelEpsilonS1.Size = new System.Drawing.Size(32, 13);
            this.labelEpsilonS1.TabIndex = 13;
            this.labelEpsilonS1.Text = "ε, S1";
            // 
            // numEpsilonS1
            // 
            this.numEpsilonS1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numEpsilonS1.DecimalPlaces = 8;
            this.numEpsilonS1.Enabled = false;
            this.numEpsilonS1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numEpsilonS1.Location = new System.Drawing.Point(61, 177);
            this.numEpsilonS1.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numEpsilonS1.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.numEpsilonS1.Name = "numEpsilonS1";
            this.numEpsilonS1.Size = new System.Drawing.Size(120, 20);
            this.numEpsilonS1.TabIndex = 10;
            this.numEpsilonS1.Value = new decimal(new int[] {
            25,
            0,
            0,
            196608});
            // 
            // labelEpsilonS0
            // 
            this.labelEpsilonS0.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelEpsilonS0.AutoSize = true;
            this.labelEpsilonS0.Location = new System.Drawing.Point(22, 147);
            this.labelEpsilonS0.Name = "labelEpsilonS0";
            this.labelEpsilonS0.Size = new System.Drawing.Size(32, 13);
            this.labelEpsilonS0.TabIndex = 12;
            this.labelEpsilonS0.Text = "ε, S0";
            // 
            // label34
            // 
            this.label34.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label34.Location = new System.Drawing.Point(61, 113);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(81, 13);
            this.label34.TabIndex = 21;
            this.label34.Text = "раст / сжатие:";
            // 
            // numEpsilonS0
            // 
            this.numEpsilonS0.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numEpsilonS0.DecimalPlaces = 8;
            this.numEpsilonS0.Enabled = false;
            this.numEpsilonS0.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numEpsilonS0.Location = new System.Drawing.Point(61, 143);
            this.numEpsilonS0.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numEpsilonS0.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.numEpsilonS0.Name = "numEpsilonS0";
            this.numEpsilonS0.Size = new System.Drawing.Size(120, 20);
            this.numEpsilonS0.TabIndex = 9;
            this.numEpsilonS0.Value = new decimal(new int[] {
            175,
            0,
            0,
            327680});
            // 
            // RebarDeformationView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutEpsilonS);
            this.Name = "RebarDeformationView";
            this.Size = new System.Drawing.Size(195, 298);
            this.tableLayoutEpsilonS.ResumeLayout(false);
            this.tableLayoutEpsilonS.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numEps_s_ult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEpsilonS2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEpsilonS1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEpsilonS0)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutEpsilonS;
        private System.Windows.Forms.NumericUpDown numEps_s_ult;
        private System.Windows.Forms.NumericUpDown numEpsilonS2;
        private System.Windows.Forms.NumericUpDown numEpsilonS1;
        private System.Windows.Forms.NumericUpDown numEpsilonS0;
        private System.Windows.Forms.Label labelEpsilonsult;
        private System.Windows.Forms.Label labelEpsilonS2;
        private System.Windows.Forms.Label labelEpsilonS1;
        private System.Windows.Forms.Label labelEpsilonS0;
        private System.Windows.Forms.Label labelReinforcementDeformType;
        private System.Windows.Forms.Label labelReinforcementDeform;
        private System.Windows.Forms.Label labelTypeDDRebar;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label34;
    }
}
