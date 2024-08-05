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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("1");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("2");
            this.dataGridElements = new System.Windows.Forms.DataGridView();
            this.btnLoad = new System.Windows.Forms.Button();
            this.tabFiber = new System.Windows.Forms.TabControl();
            this.tabPageFiber = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.comboBetonType = new System.Windows.Forms.ComboBox();
            this.label_i = new System.Windows.Forms.Label();
            this.comboBox_i = new System.Windows.Forms.ComboBox();
            this.num_omega = new System.Windows.Forms.NumericUpDown();
            this.num_eps_fb2 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabRebar = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.labelRebarClass = new System.Windows.Forms.Label();
            this.cmbRebarClass = new System.Windows.Forms.ComboBox();
            this.lvRebar = new System.Windows.Forms.ListView();
            this.tabCoeffs = new System.Windows.Forms.TabPage();
            this.dataGridCoeffs = new System.Windows.Forms.DataGridView();
            this.tabBeton = new System.Windows.Forms.TabPage();
            this.dataGridBeton = new System.Windows.Forms.DataGridView();
            this.bSDataBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridElements)).BeginInit();
            this.tabFiber.SuspendLayout();
            this.tabPageFiber.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_omega)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_eps_fb2)).BeginInit();
            this.tabRebar.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            this.tabCoeffs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridCoeffs)).BeginInit();
            this.tabBeton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridBeton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bSDataBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridElements
            // 
            this.dataGridElements.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridElements.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dataGridElements.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridElements.Location = new System.Drawing.Point(6, 89);
            this.dataGridElements.Name = "dataGridElements";
            this.dataGridElements.Size = new System.Drawing.Size(1117, 332);
            this.dataGridElements.TabIndex = 0;
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoad.Location = new System.Drawing.Point(989, 471);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 1;
            this.btnLoad.Text = "Обновить";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // tabFiber
            // 
            this.tabFiber.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabFiber.Controls.Add(this.tabPageFiber);
            this.tabFiber.Controls.Add(this.tabRebar);
            this.tabFiber.Controls.Add(this.tabCoeffs);
            this.tabFiber.Controls.Add(this.tabBeton);
            this.tabFiber.Location = new System.Drawing.Point(12, 12);
            this.tabFiber.Name = "tabFiber";
            this.tabFiber.SelectedIndex = 0;
            this.tabFiber.Size = new System.Drawing.Size(1137, 453);
            this.tabFiber.TabIndex = 1;
            // 
            // tabPageFiber
            // 
            this.tabPageFiber.Controls.Add(this.tableLayoutPanel1);
            this.tabPageFiber.Controls.Add(this.label1);
            this.tabPageFiber.Controls.Add(this.dataGridElements);
            this.tabPageFiber.Location = new System.Drawing.Point(4, 22);
            this.tabPageFiber.Name = "tabPageFiber";
            this.tabPageFiber.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFiber.Size = new System.Drawing.Size(1129, 427);
            this.tabPageFiber.TabIndex = 0;
            this.tabPageFiber.Text = "Фибробетон";
            this.tabPageFiber.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75.67567F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.32432F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 169F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 152F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 157F));
            this.tableLayoutPanel1.Controls.Add(this.comboBetonType, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label_i, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.comboBox_i, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.num_omega, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.num_eps_fb2, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 5, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 23);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(808, 27);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // comboBetonType
            // 
            this.comboBetonType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBetonType.FormattingEnabled = true;
            this.comboBetonType.Location = new System.Drawing.Point(3, 3);
            this.comboBetonType.Name = "comboBetonType";
            this.comboBetonType.Size = new System.Drawing.Size(121, 21);
            this.comboBetonType.TabIndex = 2;
            this.comboBetonType.SelectedIndexChanged += new System.EventHandler(this.comboBetonType_SelectedIndexChanged);
            // 
            // label_i
            // 
            this.label_i.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label_i.AutoSize = true;
            this.label_i.Location = new System.Drawing.Point(172, 7);
            this.label_i.Name = "label_i";
            this.label_i.Size = new System.Drawing.Size(15, 13);
            this.label_i.TabIndex = 3;
            this.label_i.Text = "i=";
            // 
            // comboBox_i
            // 
            this.comboBox_i.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.comboBox_i.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_i.FormattingEnabled = true;
            this.comboBox_i.Items.AddRange(new object[] {
            "a",
            "b",
            "c",
            "d",
            "e"});
            this.comboBox_i.Location = new System.Drawing.Point(226, 3);
            this.comboBox_i.Name = "comboBox_i";
            this.comboBox_i.Size = new System.Drawing.Size(121, 21);
            this.comboBox_i.TabIndex = 4;
            this.comboBox_i.SelectedValueChanged += new System.EventHandler(this.comboBox_i_SelectedValueChanged);
            // 
            // num_omega
            // 
            this.num_omega.DecimalPlaces = 4;
            this.num_omega.Location = new System.Drawing.Point(441, 3);
            this.num_omega.Name = "num_omega";
            this.num_omega.Size = new System.Drawing.Size(120, 20);
            this.num_omega.TabIndex = 5;
            // 
            // num_eps_fb2
            // 
            this.num_eps_fb2.DecimalPlaces = 4;
            this.num_eps_fb2.Location = new System.Drawing.Point(653, 3);
            this.num_eps_fb2.Name = "num_eps_fb2";
            this.num_eps_fb2.Size = new System.Drawing.Size(120, 20);
            this.num_eps_fb2.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(395, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "ω=";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(593, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "ε fb2 =";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Свойства";
            // 
            // tabRebar
            // 
            this.tabRebar.Controls.Add(this.tableLayoutPanel10);
            this.tabRebar.Controls.Add(this.lvRebar);
            this.tabRebar.Location = new System.Drawing.Point(4, 22);
            this.tabRebar.Name = "tabRebar";
            this.tabRebar.Padding = new System.Windows.Forms.Padding(3);
            this.tabRebar.Size = new System.Drawing.Size(1129, 427);
            this.tabRebar.TabIndex = 1;
            this.tabRebar.Text = "Арматура";
            this.tabRebar.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.ColumnCount = 2;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.5F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.5F));
            this.tableLayoutPanel10.Controls.Add(this.labelRebarClass, 0, 0);
            this.tableLayoutPanel10.Controls.Add(this.cmbRebarClass, 1, 0);
            this.tableLayoutPanel10.Location = new System.Drawing.Point(19, 6);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 1;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(200, 36);
            this.tableLayoutPanel10.TabIndex = 8;
            // 
            // labelRebarClass
            // 
            this.labelRebarClass.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelRebarClass.AutoSize = true;
            this.labelRebarClass.Location = new System.Drawing.Point(3, 11);
            this.labelRebarClass.Name = "labelRebarClass";
            this.labelRebarClass.Size = new System.Drawing.Size(91, 13);
            this.labelRebarClass.TabIndex = 0;
            this.labelRebarClass.Text = "Класс арматуры";
            // 
            // cmbRebarClass
            // 
            this.cmbRebarClass.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbRebarClass.FormattingEnabled = true;
            this.cmbRebarClass.Items.AddRange(new object[] {
            "A240",
            "A400",
            "A500",
            "A600",
            "A800",
            "A1000",
            "B500"});
            this.cmbRebarClass.Location = new System.Drawing.Point(104, 7);
            this.cmbRebarClass.Name = "cmbRebarClass";
            this.cmbRebarClass.Size = new System.Drawing.Size(93, 21);
            this.cmbRebarClass.TabIndex = 1;
            this.cmbRebarClass.SelectedIndexChanged += new System.EventHandler(this.cmbRebarClass_SelectedIndexChanged);
            // 
            // lvRebar
            // 
            this.lvRebar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvRebar.HideSelection = false;
            this.lvRebar.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.lvRebar.Location = new System.Drawing.Point(19, 56);
            this.lvRebar.Name = "lvRebar";
            this.lvRebar.Size = new System.Drawing.Size(1104, 343);
            this.lvRebar.TabIndex = 0;
            this.lvRebar.UseCompatibleStateImageBehavior = false;
            // 
            // tabCoeffs
            // 
            this.tabCoeffs.Controls.Add(this.dataGridCoeffs);
            this.tabCoeffs.Location = new System.Drawing.Point(4, 22);
            this.tabCoeffs.Name = "tabCoeffs";
            this.tabCoeffs.Padding = new System.Windows.Forms.Padding(3);
            this.tabCoeffs.Size = new System.Drawing.Size(1129, 427);
            this.tabCoeffs.TabIndex = 2;
            this.tabCoeffs.Text = "Коэффициенты";
            this.tabCoeffs.UseVisualStyleBackColor = true;
            // 
            // dataGridCoeffs
            // 
            this.dataGridCoeffs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridCoeffs.Location = new System.Drawing.Point(21, 24);
            this.dataGridCoeffs.Name = "dataGridCoeffs";
            this.dataGridCoeffs.Size = new System.Drawing.Size(1092, 344);
            this.dataGridCoeffs.TabIndex = 0;
            // 
            // tabBeton
            // 
            this.tabBeton.Controls.Add(this.dataGridBeton);
            this.tabBeton.Location = new System.Drawing.Point(4, 22);
            this.tabBeton.Name = "tabBeton";
            this.tabBeton.Padding = new System.Windows.Forms.Padding(3);
            this.tabBeton.Size = new System.Drawing.Size(1129, 427);
            this.tabBeton.TabIndex = 3;
            this.tabBeton.Text = "Бетон";
            this.tabBeton.UseVisualStyleBackColor = true;
            // 
            // dataGridBeton
            // 
            this.dataGridBeton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridBeton.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridBeton.Location = new System.Drawing.Point(22, 27);
            this.dataGridBeton.Name = "dataGridBeton";
            this.dataGridBeton.Size = new System.Drawing.Size(1079, 346);
            this.dataGridBeton.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(1070, 471);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // BSFiberSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1161, 506);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.tabFiber);
            this.Controls.Add(this.btnLoad);
            this.Name = "BSFiberSetup";
            this.Text = "Настройки";
            this.Load += new System.EventHandler(this.BSFiberSetup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridElements)).EndInit();
            this.tabFiber.ResumeLayout(false);
            this.tabPageFiber.ResumeLayout(false);
            this.tabPageFiber.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_omega)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_eps_fb2)).EndInit();
            this.tabRebar.ResumeLayout(false);
            this.tableLayoutPanel10.ResumeLayout(false);
            this.tableLayoutPanel10.PerformLayout();
            this.tabCoeffs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridCoeffs)).EndInit();
            this.tabBeton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridBeton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bSDataBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridElements;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.TabControl tabFiber;
        private System.Windows.Forms.TabPage tabPageFiber;
        private System.Windows.Forms.TabPage tabRebar;
        private System.Windows.Forms.ListView lvRebar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBetonType;
        private System.Windows.Forms.TabPage tabCoeffs;
        private System.Windows.Forms.DataGridView dataGridCoeffs;
        private System.Windows.Forms.BindingSource bSDataBindingSource;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label_i;
        private System.Windows.Forms.ComboBox comboBox_i;
        private System.Windows.Forms.NumericUpDown num_omega;
        private System.Windows.Forms.NumericUpDown num_eps_fb2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage tabBeton;
        private System.Windows.Forms.DataGridView dataGridBeton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
        private System.Windows.Forms.Label labelRebarClass;
        private System.Windows.Forms.ComboBox cmbRebarClass;
    }
}