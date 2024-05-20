namespace BSFem
{
    partial class BSFem
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnCalculate = new Button();
            textBox1 = new TextBox();
            btnPoly = new Button();
            btnSegment = new Button();
            btnMesh = new Button();
            SuspendLayout();
            // 
            // btnCalculate
            // 
            btnCalculate.Location = new Point(9, 12);
            btnCalculate.Name = "btnCalculate";
            btnCalculate.Size = new Size(75, 23);
            btnCalculate.TabIndex = 0;
            btnCalculate.Text = "Расчет";
            btnCalculate.UseVisualStyleBackColor = true;
            btnCalculate.Click += btnCalculate_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(90, 12);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(223, 192);
            textBox1.TabIndex = 1;
            // 
            // btnPoly
            // 
            btnPoly.Location = new Point(411, 90);
            btnPoly.Name = "btnPoly";
            btnPoly.Size = new Size(96, 23);
            btnPoly.TabIndex = 2;
            btnPoly.Text = "Контур";
            btnPoly.UseVisualStyleBackColor = true;
            btnPoly.Click += btnPoly_Click;
            // 
            // btnSegment
            // 
            btnSegment.Location = new Point(411, 154);
            btnSegment.Name = "btnSegment";
            btnSegment.Size = new Size(96, 23);
            btnSegment.TabIndex = 3;
            btnSegment.Text = "Сегмент";
            btnSegment.UseVisualStyleBackColor = true;
            btnSegment.Click += btnSegment_Click;
            // 
            // btnMesh
            // 
            btnMesh.Location = new Point(421, 222);
            btnMesh.Name = "btnMesh";
            btnMesh.Size = new Size(75, 23);
            btnMesh.TabIndex = 4;
            btnMesh.Text = "Сетка";
            btnMesh.UseVisualStyleBackColor = true;
            btnMesh.Click += btnMesh_Click;
            // 
            // BSFem
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(680, 334);
            Controls.Add(btnMesh);
            Controls.Add(btnSegment);
            Controls.Add(btnPoly);
            Controls.Add(textBox1);
            Controls.Add(btnCalculate);
            Name = "BSFem";
            Text = "FEM";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCalculate;
        private TextBox textBox1;
        private Button btnPoly;
        private Button btnSegment;
        private Button btnMesh;
    }
}
