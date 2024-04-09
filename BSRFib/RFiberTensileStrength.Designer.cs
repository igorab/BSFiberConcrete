namespace BSFiberConcrete.BSRFib
{
    partial class RFiberTensileStrength
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
            this.dataGridFFF = new System.Windows.Forms.DataGridView();
            this.Fel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.F05 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.F25 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridFFF)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridFFF
            // 
            this.dataGridFFF.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridFFF.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Fel,
            this.F05,
            this.F25});
            this.dataGridFFF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridFFF.Location = new System.Drawing.Point(3, 3);
            this.dataGridFFF.Name = "dataGridFFF";
            this.dataGridFFF.Size = new System.Drawing.Size(374, 429);
            this.dataGridFFF.TabIndex = 0;
            // 
            // Fel
            // 
            this.Fel.HeaderText = "F el";
            this.Fel.Name = "Fel";
            // 
            // F05
            // 
            this.F05.HeaderText = "F 0,5";
            this.F05.Name = "F05";
            // 
            // F25
            // 
            this.F25.HeaderText = "F 2,5";
            this.F25.Name = "F25";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67.37589F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.62411F));
            this.tableLayoutPanel1.Controls.Add(this.dataGridFFF, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(41, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(564, 516);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // RFiberTensileStrength
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 576);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "RFiberTensileStrength";
            this.Text = "Определение прочности на растяжение";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridFFF)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridFFF;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fel;
        private System.Windows.Forms.DataGridViewTextBoxColumn F05;
        private System.Windows.Forms.DataGridViewTextBoxColumn F25;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}