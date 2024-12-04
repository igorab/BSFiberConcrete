using BSFiberConcrete.Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BSFiberConcrete.LocalStrength
{
    public partial class BSLocalStrength : Form
    {
        public BSLocalStrengthCalc StrengthCalc {  get; set; }
        public TypeOfLocalStrengthCalc TypeOfCalc { get; set; }

        public bool IsShowScheme 
        { 
            get 
            { 
                if (TypeOfCalc == TypeOfLocalStrengthCalc.Compression)
                    return true;
                else
                    return false;
            } 
        }

        private BindingList<LocalStress> Ds;


        public BSLocalStrength()
        {
            InitializeComponent();            
        }
       
        private void btnCalc_Click(object sender, EventArgs e)
        {
            StrengthCalc.UseReinforcement = checkBoxReinf.Checked;            
            StrengthCalc.Scheme = cmbScheme.SelectedIndex + 1;

            StrengthCalc.RunCalc();

            localStressBindingSource.DataSource = new BindingList<LocalStress>(StrengthCalc.GetDS);

            chboxReinforcement_CheckedChanged(null, null);

        }

        private void BSLocalStrength_Load(object sender, EventArgs e)
        {
            Ds = new BindingList<LocalStress> (StrengthCalc.GetDS);

            labelHeader.Text = StrengthCalc.SampleDescr();

            localStressBindingSource.DataSource = Ds;

            chboxReinforcement_CheckedChanged(null, null);            
            //labelScheme.Visible = IsShowScheme;
            cmbScheme.Visible = IsShowScheme;            
        }

        private void btnPrintReport_Click(object sender, EventArgs e)
        {
            BSLocalStrengthReport strengthReport = new BSLocalStrengthReport();
            strengthReport.ReportName = StrengthCalc.ReportName();
            strengthReport.SampleName = StrengthCalc.SampleName();
            strengthReport.SampleDescr = StrengthCalc.SampleDescr();
            strengthReport.ImageScheme = StrengthCalc.ImageScheme();
            
            if (localStressBindingSource.DataSource is BindingList<LocalStress>)
            {
                var ds = (BindingList<LocalStress>)localStressBindingSource.DataSource;
                strengthReport.DataSource = ds.ToList();
            }

            strengthReport.RunReport();
        }

        private void btnCloseForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridLocalStrength_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            try
            {
                if (StrengthCalc.Dc.ContainsKey(Convert.ToString(dataGridLocalStrength.Rows[e.RowIndex].Cells[2].Value)))
                {
                    dataGridLocalStrength.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Beige;                                        
                }
            }
            catch
            {

            }
        }

        private void chboxReinforcement_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridLocalStrength.Rows)
                {
                    if (Convert.ToInt32(row.Cells[4].Value) == 1)
                    {
                        try
                        {
                            if (checkBoxReinf.Checked == true)
                                row.Visible = true;
                            else
                                row.Visible = false;
                        }
                        catch { }
                    }
                }
                dataGridLocalStrength.Refresh();
            }
            catch
            { }            
        }

        private void labelSchemeHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            { 
                Form form = new Form();
                form.Text = "Расчетная схема";

                PictureBox pictureBox = new PictureBox();
                pictureBox.Name = "pictureBoxImg";
                pictureBox.Location = new Point(10, 10);
                pictureBox.Size = new Size(600, 600);                
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

                if (TypeOfCalc == TypeOfLocalStrengthCalc.Compression)
                    pictureBox.Image = Properties.Resources.SchemeStress;
                else if (TypeOfCalc == TypeOfLocalStrengthCalc.Punch)
                {
                    if (checkBoxReinf.Checked == true)
                        pictureBox.Image = Properties.Resources.SchemePunchReinf;
                    else
                        pictureBox.Image = Properties.Resources.SchemePunch;
                }
                Label labelTxt = new Label();
                labelTxt.Name = "labelTxt";

                if (TypeOfCalc == TypeOfLocalStrengthCalc.Compression)
                    labelTxt.Text = "Схемы для расчета элементов на местное сжатие при различном положении местной нагрузки";
                else if (TypeOfCalc == TypeOfLocalStrengthCalc.Punch)                
                    labelTxt.Text = "Схемы для расчета элементов на продавливание";
                
                labelTxt.Font = new Font(labelTxt.Font.FontFamily, 12);
                labelTxt.Location = new Point(pictureBox.Left, pictureBox.Height + 20);
                labelTxt.Size = new Size(labelTxt.PreferredWidth, labelTxt.PreferredHeight);

                form.Size = new Size(pictureBox.Width + 50, pictureBox.Height + 100);
                form.Controls.Add(pictureBox);
                form.Controls.Add(labelTxt);            
                form.ShowDialog();             
            }
            catch  (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }           
        }
    }
}
