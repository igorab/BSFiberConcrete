namespace BSFiberConcrete
{
    partial class BSFiberSetup
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
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("1");
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem("2");
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnLoad = new System.Windows.Forms.Button();
            this.tabGeneral = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabRebar = new System.Windows.Forms.TabPage();
            this.lvRebar = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabGeneral.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabRebar.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 47);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(966, 187);
            this.dataGridView1.TabIndex = 0;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(761, 333);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 1;
            this.btnLoad.Text = "Обновить";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // tabGeneral
            // 
            this.tabGeneral.Controls.Add(this.tabPage1);
            this.tabGeneral.Controls.Add(this.tabRebar);
            this.tabGeneral.Location = new System.Drawing.Point(12, 12);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.SelectedIndex = 0;
            this.tabGeneral.Size = new System.Drawing.Size(986, 315);
            this.tabGeneral.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(978, 289);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Бетон";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabRebar
            // 
            this.tabRebar.Controls.Add(this.lvRebar);
            this.tabRebar.Location = new System.Drawing.Point(4, 22);
            this.tabRebar.Name = "tabRebar";
            this.tabRebar.Padding = new System.Windows.Forms.Padding(3);
            this.tabRebar.Size = new System.Drawing.Size(978, 289);
            this.tabRebar.TabIndex = 1;
            this.tabRebar.Text = "Арматура";
            this.tabRebar.UseVisualStyleBackColor = true;
            // 
            // lvRebar
            // 
            this.lvRebar.HideSelection = false;
            this.lvRebar.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem7,
            listViewItem8});
            this.lvRebar.Location = new System.Drawing.Point(19, 22);
            this.lvRebar.Name = "lvRebar";
            this.lvRebar.Size = new System.Drawing.Size(801, 229);
            this.lvRebar.TabIndex = 0;
            this.lvRebar.UseCompatibleStateImageBehavior = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Свойства";
            // 
            // BSFiberSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1062, 388);
            this.Controls.Add(this.tabGeneral);
            this.Controls.Add(this.btnLoad);
            this.Name = "BSFiberSetup";
            this.Text = "Настройки";
            this.Load += new System.EventHandler(this.BSFiberSetup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabGeneral.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabRebar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.TabControl tabGeneral;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabRebar;
        private System.Windows.Forms.ListView lvRebar;
        private System.Windows.Forms.Label label1;
    }
}