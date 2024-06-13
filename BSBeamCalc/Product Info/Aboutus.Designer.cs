using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace CBAnsDes
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class Aboutus : Form
    {

        // Form overrides dispose to clean up the component list.
        [DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && components is not null)
                {
                    components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        internal TableLayoutPanel TableLayoutPanel;
        internal Label LabelProductName;
        internal Label LabelVersion;
        internal TextBox TextBoxDescription;
        internal Button OKButton;

        // Required by the Windows Form Designer
        private System.ComponentModel.IContainer components;

        // NOTE: The following procedure is required by the Windows Form Designer
        // It can be modified using the Windows Form Designer.  
        // Do not modify it using the code editor.
        [DebuggerStepThrough()]
        private void InitializeComponent()
        {
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(Aboutus));
            TableLayoutPanel = new TableLayoutPanel();
            LabelProductName = new Label();
            LabelVersion = new Label();
            TextBoxDescription = new TextBox();
            OKButton = new Button();
            OKButton.Click += new EventHandler(OKButton_Click);
            TableLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // TableLayoutPanel
            // 
            TableLayoutPanel.ColumnCount = 2;
            TableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 2.892562f));
            TableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 97.10744f));
            TableLayoutPanel.Controls.Add(LabelProductName, 1, 0);
            TableLayoutPanel.Controls.Add(LabelVersion, 1, 1);
            TableLayoutPanel.Controls.Add(TextBoxDescription, 1, 4);
            TableLayoutPanel.Controls.Add(OKButton, 1, 5);
            TableLayoutPanel.Dock = DockStyle.Fill;
            TableLayoutPanel.Location = new Point(12, 11);
            TableLayoutPanel.Margin = new Padding(4);
            TableLayoutPanel.Name = "TableLayoutPanel";
            TableLayoutPanel.RowCount = 6;
            TableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10.0f));
            TableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10.0f));
            TableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 3.007519f));
            TableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 3.007519f));
            TableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 59.77444f));
            TableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 13.80952f));
            TableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 15.0f));
            TableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 25.0f));
            TableLayoutPanel.Size = new Size(510, 223);
            TableLayoutPanel.TabIndex = 0;
            // 
            // LabelProductName
            // 
            LabelProductName.Dock = DockStyle.Fill;
            LabelProductName.Location = new Point(22, 0);
            LabelProductName.Margin = new Padding(8, 0, 4, 0);
            LabelProductName.MaximumSize = new Size(0, 21);
            LabelProductName.Name = "LabelProductName";
            LabelProductName.Size = new Size(484, 21);
            LabelProductName.TabIndex = 0;
            LabelProductName.Text = "Continuous Beam Analyzer" + '\r' + '\n';
            LabelProductName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // LabelVersion
            // 
            LabelVersion.Dock = DockStyle.Fill;
            LabelVersion.Location = new Point(22, 22);
            LabelVersion.Margin = new Padding(8, 0, 4, 0);
            LabelVersion.MaximumSize = new Size(0, 21);
            LabelVersion.Name = "LabelVersion";
            LabelVersion.Size = new Size(484, 21);
            LabelVersion.TabIndex = 0;
            LabelVersion.Text = "Version 1";
            LabelVersion.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TextBoxDescription
            // 
            TextBoxDescription.Dock = DockStyle.Fill;
            TextBoxDescription.Location = new Point(22, 60);
            TextBoxDescription.Margin = new Padding(8, 4, 4, 4);
            TextBoxDescription.Multiline = true;
            TextBoxDescription.Name = "TextBoxDescription";
            TextBoxDescription.ReadOnly = true;
            TextBoxDescription.ScrollBars = ScrollBars.Both;
            TextBoxDescription.Size = new Size(484, 125);
            TextBoxDescription.TabIndex = 0;
            TextBoxDescription.TabStop = false;
            TextBoxDescription.Text = resources.GetString("TextBoxDescription.Text");
            // 
            // OKButton
            // 
            OKButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            OKButton.DialogResult = DialogResult.Cancel;
            OKButton.Location = new Point(406, 193);
            OKButton.Margin = new Padding(4);
            OKButton.Name = "OKButton";
            OKButton.Size = new Size(100, 26);
            OKButton.TabIndex = 0;
            OKButton.Text = "&OK";
            // 
            // Aboutus
            // 
            AutoScaleDimensions = new SizeF(8.0f, 16.0f);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = OKButton;
            ClientSize = new Size(534, 245);
            Controls.Add(TableLayoutPanel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Aboutus";
            Padding = new Padding(12, 11, 12, 11);
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "About";
            TableLayoutPanel.ResumeLayout(false);
            TableLayoutPanel.PerformLayout();
            Load += new EventHandler(Aboutus_Load);
            ResumeLayout(false);

        }

    }
}