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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("1");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("2");
            this.dataGridElements = new System.Windows.Forms.DataGridView();
            this.btnLoad = new System.Windows.Forms.Button();
            this.tabFiber = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.comboBetonType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabRebar = new System.Windows.Forms.TabPage();
            this.lvRebar = new System.Windows.Forms.ListView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridElements)).BeginInit();
            this.tabFiber.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabRebar.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridElements
            // 
            this.dataGridElements.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridElements.Location = new System.Drawing.Point(6, 82);
            this.dataGridElements.Name = "dataGridElements";
            this.dataGridElements.Size = new System.Drawing.Size(966, 152);
            this.dataGridElements.TabIndex = 0;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(913, 353);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 1;
            this.btnLoad.Text = "Обновить";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // tabFiber
            // 
            this.tabFiber.Controls.Add(this.tabPage1);
            this.tabFiber.Controls.Add(this.tabRebar);
            this.tabFiber.Location = new System.Drawing.Point(12, 12);
            this.tabFiber.Name = "tabFiber";
            this.tabFiber.SelectedIndex = 0;
            this.tabFiber.Size = new System.Drawing.Size(986, 335);
            this.tabFiber.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.comboBetonType);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.dataGridElements);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(978, 309);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Фибробетон";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // comboBetonType
            // 
            this.comboBetonType.FormattingEnabled = true;
            this.comboBetonType.Location = new System.Drawing.Point(9, 25);
            this.comboBetonType.Name = "comboBetonType";
            this.comboBetonType.Size = new System.Drawing.Size(121, 21);
            this.comboBetonType.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Свойства";
            // 
            // tabRebar
            // 
            this.tabRebar.Controls.Add(this.lvRebar);
            this.tabRebar.Location = new System.Drawing.Point(4, 22);
            this.tabRebar.Name = "tabRebar";
            this.tabRebar.Padding = new System.Windows.Forms.Padding(3);
            this.tabRebar.Size = new System.Drawing.Size(978, 309);
            this.tabRebar.TabIndex = 1;
            this.tabRebar.Text = "Арматура";
            this.tabRebar.UseVisualStyleBackColor = true;
            // 
            // lvRebar
            // 
            this.lvRebar.HideSelection = false;
            this.lvRebar.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.lvRebar.Location = new System.Drawing.Point(19, 22);
            this.lvRebar.Name = "lvRebar";
            this.lvRebar.Size = new System.Drawing.Size(801, 229);
            this.lvRebar.TabIndex = 0;
            this.lvRebar.UseCompatibleStateImageBehavior = false;
            // 
            // BSFiberSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1062, 388);
            this.Controls.Add(this.tabFiber);
            this.Controls.Add(this.btnLoad);
            this.Name = "BSFiberSetup";
            this.Text = "Настройки";
            this.Load += new System.EventHandler(this.BSFiberSetup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridElements)).EndInit();
            this.tabFiber.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabRebar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridElements;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.TabControl tabFiber;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabRebar;
        private System.Windows.Forms.ListView lvRebar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBetonType;
    }
}