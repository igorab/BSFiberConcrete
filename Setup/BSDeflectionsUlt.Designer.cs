namespace BSFiberConcrete.Setup
{
    partial class BSDeflectionsUlt
    {
                                private System.ComponentModel.IContainer components = null;
                                        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
                                                private void InitializeComponent()
        {
            this.tabControlDeflection = new System.Windows.Forms.TabControl();
            this.tabPageVertical = new System.Windows.Forms.TabPage();
            this.tabPageHorizontal = new System.Windows.Forms.TabPage();
            this.gridVertical = new System.Windows.Forms.DataGridView();
            this.gridHorizontal = new System.Windows.Forms.DataGridView();
            this.tabControlDeflection.SuspendLayout();
            this.tabPageVertical.SuspendLayout();
            this.tabPageHorizontal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridVertical)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridHorizontal)).BeginInit();
            this.SuspendLayout();
                                                this.tabControlDeflection.Controls.Add(this.tabPageVertical);
            this.tabControlDeflection.Controls.Add(this.tabPageHorizontal);
            this.tabControlDeflection.Location = new System.Drawing.Point(12, 12);
            this.tabControlDeflection.Name = "tabControlDeflection";
            this.tabControlDeflection.SelectedIndex = 0;
            this.tabControlDeflection.Size = new System.Drawing.Size(596, 323);
            this.tabControlDeflection.TabIndex = 0;
                                                this.tabPageVertical.Controls.Add(this.gridVertical);
            this.tabPageVertical.Location = new System.Drawing.Point(4, 22);
            this.tabPageVertical.Name = "tabPageVertical";
            this.tabPageVertical.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageVertical.Size = new System.Drawing.Size(588, 297);
            this.tabPageVertical.TabIndex = 0;
            this.tabPageVertical.Text = "Вертикальные";
            this.tabPageVertical.UseVisualStyleBackColor = true;
                                                this.tabPageHorizontal.Controls.Add(this.gridHorizontal);
            this.tabPageHorizontal.Location = new System.Drawing.Point(4, 22);
            this.tabPageHorizontal.Name = "tabPageHorizontal";
            this.tabPageHorizontal.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageHorizontal.Size = new System.Drawing.Size(588, 297);
            this.tabPageHorizontal.TabIndex = 1;
            this.tabPageHorizontal.Text = "Горизонтальные";
            this.tabPageHorizontal.UseVisualStyleBackColor = true;
                                                this.gridVertical.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridVertical.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridVertical.Location = new System.Drawing.Point(3, 3);
            this.gridVertical.Name = "gridVertical";
            this.gridVertical.Size = new System.Drawing.Size(582, 291);
            this.gridVertical.TabIndex = 0;
                                                this.gridHorizontal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridHorizontal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridHorizontal.Location = new System.Drawing.Point(3, 3);
            this.gridHorizontal.Name = "gridHorizontal";
            this.gridHorizontal.Size = new System.Drawing.Size(582, 291);
            this.gridHorizontal.TabIndex = 0;
                                                this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 369);
            this.Controls.Add(this.tabControlDeflection);
            this.Name = "BSDeflectionsUlt";
            this.Text = "Предельные прогибы";
            this.tabControlDeflection.ResumeLayout(false);
            this.tabPageVertical.ResumeLayout(false);
            this.tabPageHorizontal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridVertical)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridHorizontal)).EndInit();
            this.ResumeLayout(false);
        }
                private System.Windows.Forms.TabControl tabControlDeflection;
        private System.Windows.Forms.TabPage tabPageVertical;
        private System.Windows.Forms.DataGridView gridVertical;
        private System.Windows.Forms.TabPage tabPageHorizontal;
        private System.Windows.Forms.DataGridView gridHorizontal;
    }
}