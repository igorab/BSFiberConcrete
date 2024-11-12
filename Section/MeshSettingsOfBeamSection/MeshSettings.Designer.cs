namespace BSFiberConcrete.Section.MeshSettingsOfBeamSection
{
    partial class MeshSettings
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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.tableLayoutMesh = new System.Windows.Forms.TableLayoutPanel();
            this.numMinAngle = new System.Windows.Forms.NumericUpDown();
            this.labelTriAngle = new System.Windows.Forms.Label();
            this.labelMeshNumX = new System.Windows.Forms.Label();
            this.numMeshNX = new System.Windows.Forms.NumericUpDown();
            this.labelMeshNumY = new System.Windows.Forms.Label();
            this.numMeshNY = new System.Windows.Forms.NumericUpDown();
            this.numMaxArea = new System.Windows.Forms.NumericUpDown();
            this.labelMaxArea = new System.Windows.Forms.Label();
            this.labelMeshNumXDescr = new System.Windows.Forms.Label();
            this.labelMeshNumYDescr = new System.Windows.Forms.Label();
            this.labelTriAngleDescr = new System.Windows.Forms.Label();
            this.labelMaxAreaDescr = new System.Windows.Forms.Label();
            this.tableLayoutMesh.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMinAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMeshNX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMeshNY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxArea)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(12, 12);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(148, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "Значение по умолчанию";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutMesh
            // 
            this.tableLayoutMesh.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutMesh.ColumnCount = 3;
            this.tableLayoutMesh.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutMesh.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.88506F));
            this.tableLayoutMesh.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.78161F));
            this.tableLayoutMesh.Controls.Add(this.numMinAngle, 1, 2);
            this.tableLayoutMesh.Controls.Add(this.labelTriAngle, 0, 2);
            this.tableLayoutMesh.Controls.Add(this.labelMeshNumX, 0, 0);
            this.tableLayoutMesh.Controls.Add(this.numMeshNX, 1, 0);
            this.tableLayoutMesh.Controls.Add(this.labelMeshNumY, 0, 1);
            this.tableLayoutMesh.Controls.Add(this.numMeshNY, 1, 1);
            this.tableLayoutMesh.Controls.Add(this.numMaxArea, 1, 3);
            this.tableLayoutMesh.Controls.Add(this.labelMaxArea, 0, 3);
            this.tableLayoutMesh.Controls.Add(this.labelMeshNumXDescr, 2, 0);
            this.tableLayoutMesh.Controls.Add(this.labelMeshNumYDescr, 2, 1);
            this.tableLayoutMesh.Controls.Add(this.labelTriAngleDescr, 2, 2);
            this.tableLayoutMesh.Controls.Add(this.labelMaxAreaDescr, 2, 3);
            this.tableLayoutMesh.Location = new System.Drawing.Point(77, 77);
            this.tableLayoutMesh.Name = "tableLayoutMesh";
            this.tableLayoutMesh.RowCount = 4;
            this.tableLayoutMesh.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutMesh.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutMesh.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutMesh.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutMesh.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutMesh.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutMesh.Size = new System.Drawing.Size(349, 129);
            this.tableLayoutMesh.TabIndex = 8;
            // 
            // numMinAngle
            // 
            this.numMinAngle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numMinAngle.Location = new System.Drawing.Point(120, 67);
            this.numMinAngle.Name = "numMinAngle";
            this.numMinAngle.Size = new System.Drawing.Size(97, 20);
            this.numMinAngle.TabIndex = 2;
            this.numMinAngle.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // labelTriAngle
            // 
            this.labelTriAngle.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelTriAngle.AutoSize = true;
            this.labelTriAngle.Location = new System.Drawing.Point(9, 72);
            this.labelTriAngle.Name = "labelTriAngle";
            this.labelTriAngle.Size = new System.Drawing.Size(104, 13);
            this.labelTriAngle.TabIndex = 3;
            this.labelTriAngle.Text = "Угол триангуляции";
            // 
            // labelMeshNumX
            // 
            this.labelMeshNumX.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelMeshNumX.AutoSize = true;
            this.labelMeshNumX.Location = new System.Drawing.Point(10, 10);
            this.labelMeshNumX.Name = "labelMeshNumX";
            this.labelMeshNumX.Size = new System.Drawing.Size(103, 13);
            this.labelMeshNumX.TabIndex = 1;
            this.labelMeshNumX.Text = "Размер сетки по X";
            // 
            // numMeshNX
            // 
            this.numMeshNX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numMeshNX.Location = new System.Drawing.Point(120, 4);
            this.numMeshNX.Name = "numMeshNX";
            this.numMeshNX.Size = new System.Drawing.Size(97, 20);
            this.numMeshNX.TabIndex = 0;
            this.numMeshNX.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // labelMeshNumY
            // 
            this.labelMeshNumY.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelMeshNumY.AutoSize = true;
            this.labelMeshNumY.Location = new System.Drawing.Point(10, 41);
            this.labelMeshNumY.Name = "labelMeshNumY";
            this.labelMeshNumY.Size = new System.Drawing.Size(103, 13);
            this.labelMeshNumY.TabIndex = 17;
            this.labelMeshNumY.Text = "Размер сетки по Y";
            // 
            // numMeshNY
            // 
            this.numMeshNY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numMeshNY.Location = new System.Drawing.Point(120, 36);
            this.numMeshNY.Name = "numMeshNY";
            this.numMeshNY.Size = new System.Drawing.Size(97, 20);
            this.numMeshNY.TabIndex = 18;
            this.numMeshNY.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // numMaxArea
            // 
            this.numMaxArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numMaxArea.Location = new System.Drawing.Point(120, 98);
            this.numMaxArea.Name = "numMaxArea";
            this.numMaxArea.Size = new System.Drawing.Size(97, 20);
            this.numMaxArea.TabIndex = 22;
            this.numMaxArea.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // labelMaxArea
            // 
            this.labelMaxArea.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelMaxArea.AutoSize = true;
            this.labelMaxArea.Location = new System.Drawing.Point(7, 105);
            this.labelMaxArea.Name = "labelMaxArea";
            this.labelMaxArea.Size = new System.Drawing.Size(106, 13);
            this.labelMaxArea.TabIndex = 23;
            this.labelMaxArea.Text = "Площадь элемента";
            // 
            // labelMeshNumXDescr
            // 
            this.labelMeshNumXDescr.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelMeshNumXDescr.AutoSize = true;
            this.labelMeshNumXDescr.Location = new System.Drawing.Point(224, 10);
            this.labelMeshNumXDescr.Name = "labelMeshNumXDescr";
            this.labelMeshNumXDescr.Size = new System.Drawing.Size(87, 13);
            this.labelMeshNumXDescr.TabIndex = 24;
            this.labelMeshNumXDescr.Text = "прямоуг. и тавр";
            // 
            // labelMeshNumYDescr
            // 
            this.labelMeshNumYDescr.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelMeshNumYDescr.AutoSize = true;
            this.labelMeshNumYDescr.Location = new System.Drawing.Point(224, 41);
            this.labelMeshNumYDescr.Name = "labelMeshNumYDescr";
            this.labelMeshNumYDescr.Size = new System.Drawing.Size(87, 13);
            this.labelMeshNumYDescr.TabIndex = 25;
            this.labelMeshNumYDescr.Text = "прямоуг. и тавр";
            // 
            // labelTriAngleDescr
            // 
            this.labelTriAngleDescr.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelTriAngleDescr.AutoSize = true;
            this.labelTriAngleDescr.Location = new System.Drawing.Point(224, 72);
            this.labelTriAngleDescr.Name = "labelTriAngleDescr";
            this.labelTriAngleDescr.Size = new System.Drawing.Size(43, 13);
            this.labelTriAngleDescr.TabIndex = 26;
            this.labelTriAngleDescr.Text = "кольцо";
            // 
            // labelMaxAreaDescr
            // 
            this.labelMaxAreaDescr.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelMaxAreaDescr.AutoSize = true;
            this.labelMaxAreaDescr.Location = new System.Drawing.Point(224, 105);
            this.labelMaxAreaDescr.Name = "labelMaxAreaDescr";
            this.labelMaxAreaDescr.Size = new System.Drawing.Size(87, 13);
            this.labelMaxAreaDescr.TabIndex = 27;
            this.labelMaxAreaDescr.Text = "произв сечение";
            // 
            // MeshSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 361);
            this.Controls.Add(this.tableLayoutMesh);
            this.Controls.Add(this.checkBox1);
            this.Name = "MeshSettings";
            this.Text = "MeshSettings";
            this.tableLayoutMesh.ResumeLayout(false);
            this.tableLayoutMesh.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMinAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMeshNX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMeshNY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxArea)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutMesh;
        private System.Windows.Forms.NumericUpDown numMinAngle;
        private System.Windows.Forms.Label labelTriAngle;
        private System.Windows.Forms.Label labelMeshNumX;
        private System.Windows.Forms.NumericUpDown numMeshNX;
        private System.Windows.Forms.Label labelMeshNumY;
        private System.Windows.Forms.NumericUpDown numMeshNY;
        private System.Windows.Forms.NumericUpDown numMaxArea;
        private System.Windows.Forms.Label labelMaxArea;
        private System.Windows.Forms.Label labelMeshNumXDescr;
        private System.Windows.Forms.Label labelMeshNumYDescr;
        private System.Windows.Forms.Label labelTriAngleDescr;
        private System.Windows.Forms.Label labelMaxAreaDescr;
    }
}