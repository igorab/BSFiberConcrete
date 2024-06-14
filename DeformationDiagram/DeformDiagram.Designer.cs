namespace BSFiberConcrete.DeformationDiagram
{
    partial class DeformDiagram
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.chartDeformDiagram = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chartDeformDiagram)).BeginInit();
            this.SuspendLayout();
            // 
            // chartDeformDiagram
            // 
            chartArea1.Name = "ChartArea1";
            this.chartDeformDiagram.ChartAreas.Add(chartArea1);
            this.chartDeformDiagram.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartDeformDiagram.Location = new System.Drawing.Point(0, 0);
            this.chartDeformDiagram.Name = "chartDeformDiagram";
            this.chartDeformDiagram.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            this.chartDeformDiagram.Size = new System.Drawing.Size(800, 450);
            this.chartDeformDiagram.TabIndex = 0;
            this.chartDeformDiagram.Text = "chart1";
            title1.Name = "Title1";
            this.chartDeformDiagram.Titles.Add(title1);
            // 
            // DeformDiagram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.chartDeformDiagram);
            this.Name = "DeformDiagram";
            this.Text = "Диаграмма деформирования";
            ((System.ComponentModel.ISupportInitialize)(this.chartDeformDiagram)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartDeformDiagram;
    }
}