namespace BSFiberConcrete
{
    partial class BSCalcResults
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

        #region Windows Form Designer generated code

                                        private void InitializeComponent()
        {
            this.lvResults = new System.Windows.Forms.ListView();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSaveCalc = new System.Windows.Forms.Button();
            this.lvParams = new System.Windows.Forms.ListView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
                                                this.lvResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvResults.HideSelection = false;
            this.lvResults.Location = new System.Drawing.Point(3, 3);
            this.lvResults.Name = "lvResults";
            this.lvResults.Size = new System.Drawing.Size(820, 441);
            this.lvResults.TabIndex = 0;
            this.lvResults.UseCompatibleStateImageBehavior = false;
                                                this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(834, 473);
            this.tabControl.TabIndex = 1;
                                                this.tabPage1.Controls.Add(this.btnClose);
            this.tabPage1.Controls.Add(this.btnSaveCalc);
            this.tabPage1.Controls.Add(this.lvParams);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(826, 447);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Параметры";
            this.tabPage1.UseVisualStyleBackColor = true;
                                                this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnClose.Location = new System.Drawing.Point(722, 406);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(96, 33);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
                                                this.btnSaveCalc.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSaveCalc.Location = new System.Drawing.Point(612, 406);
            this.btnSaveCalc.Name = "btnSaveCalc";
            this.btnSaveCalc.Size = new System.Drawing.Size(104, 33);
            this.btnSaveCalc.TabIndex = 2;
            this.btnSaveCalc.Text = "Сохранить";
            this.btnSaveCalc.UseVisualStyleBackColor = true;
            this.btnSaveCalc.Click += new System.EventHandler(this.btnSaveCalc_Click);
                                                this.lvParams.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvParams.HideSelection = false;
            this.lvParams.Location = new System.Drawing.Point(3, 3);
            this.lvParams.Name = "lvParams";
            this.lvParams.Size = new System.Drawing.Size(820, 397);
            this.lvParams.TabIndex = 1;
            this.lvParams.UseCompatibleStateImageBehavior = false;
                                                this.tabPage2.Controls.Add(this.lvResults);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(826, 447);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Результаты";
            this.tabPage2.UseVisualStyleBackColor = true;
                                                this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 473);
            this.Controls.Add(this.tabControl);
            this.Name = "BSCalcResults";
            this.Text = "Результаты последнего расчета";
            this.Load += new System.EventHandler(this.BSCalcResults_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvResults;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView lvParams;
        private System.Windows.Forms.Button btnSaveCalc;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}