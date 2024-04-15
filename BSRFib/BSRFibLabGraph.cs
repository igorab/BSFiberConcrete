using BSFiberConcrete.BSRFib;
using BSFiberConcrete.Lib;
using MathNet.Numerics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Series = System.Windows.Forms.DataVisualization.Charting.Series;

namespace BSFiberConcrete
{
    public partial class BSRFibLabGraph : Form
    {
        private BindingList<FaF> Qds;

        private Dictionary<string, double> LabResults;

        public BSRFibLabGraph()
        {
            InitializeComponent();

            LabResults = new Dictionary<string, double>();
        }
              
        private void BSGraph_Load(object sender, EventArgs e)
        {
            List<FaF> listFaF = BSData.LoadRChartFaF();

            Qds = new BindingList<FaF>(listFaF);
                        
            gridFaF.DataSource = Qds;
            gridFaF.ReadOnly = false;
            gridFaF.AllowUserToAddRows = false;
            gridFaF.Refresh();
            
        }

        /// <summary>
        /// В ходе испытаний для каждого образца строят графики «F – aF»
        /// </summary>        
        void InitChart()
        {
            Series series = this.ChartFaF.Series["AFSerie"]; 
            series.ChartType = SeriesChartType.Spline;
                                  
            ChartFaF.DataSource = Qds;
            ChartFaF.DataBind();

            //foreach (var item in q)
            //{
            //    series.Points.AddXY(item.aF, item.F);
            //}                       
        }

        private FibLab CalcF()
        {
            BSRFibLabTensile labTensile = new BSRFibLabTensile();
            labTensile.DsFaF = Qds.ToList();

            FibLab fibLab = new FibLab()
            {
                Id = txtBarSample.Text,
                L = (double) numL.Value,
                B = (double)numB.Value,

                Fel = Qds.Max(_x => _x.F),
                F05 = labTensile.F05(),
                F25 = labTensile.F25(),
            };

            numFel.Value = (decimal)fibLab.Fel;            
            numF05.Value = (decimal)fibLab.F05;
            numF25.Value = (decimal)fibLab.F25;

            return fibLab;            
        }


        private void btnDrawChart_Click(object sender, EventArgs e)
        {
            InitChart();

            FibLab flab = CalcF();

            // сохранить результаты
            BSQuery.SaveFibLab(new List<FibLab> { flab });
        }

        
        private void btnDSAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int mx = Qds.Max(x => x.Num) + 1;
                double mxaF = Qds.Max(x => x.aF) + 0.01;

                FaF item = new FaF() { Num = mx, F = 5, aF = mxaF };
                Qds.Add(item);
                gridFaF.DataSource = Qds;

                gridFaF.Refresh();
            }
            catch (Exception _ex) 
            {
                MessageBox.Show(_ex.Message);
            }
        }

        private void btnDSSave_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Сохранить данные?");

            if (dialogResult == DialogResult.OK)
                BSQuery.SaveFaF(Qds.ToList());
        }
      
        private void btnDSOpen_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;

                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }

                    using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
                    {
                        var q_ds = JsonSerializer.Deserialize<List<FaF>>(fs);

                        Qds = new BindingList<FaF>(q_ds);

                        gridFaF.DataSource = Qds;
                        gridFaF.Refresh();
                    }

                    MessageBox.Show(fileContent, "Файл загружен: " + filePath, MessageBoxButtons.OK);
                }
            }            
        }
      
        /// <summary>
        ///  Сохранить данные измерений в файл
        /// </summary>        
        private void btnDSSave2File_Click(object sender, EventArgs e)
        {            
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = "Document"; // Default file name
            dlg.DefaultExt = ".txt"; // Default file extension
            dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show save file dialog box
            DialogResult result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == DialogResult.OK)
            {
                // Save document
                string path = dlg.FileName;
                //string path = Path.Combine(Environment.CurrentDirectory, "");

                List<FaF> fibFaF = new List<FaF>(Qds);
                
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    JsonSerializer.Serialize<List<FaF>>(fs, fibFaF);
                }
            }
        }

        private void btnDSDel_Click(object sender, EventArgs e)
        {
            try
            {
                int idx = Qds.Count - 1;
                Qds.RemoveAt(idx);                
                gridFaF.Refresh();
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            BSRFibLabReport labReport = new BSRFibLabReport();

            labReport.ReportName = "Лаборатория";
            labReport.SampleDescr = "Образец: " + txtBarSample.Text;

            LabResults = new Dictionary<string, double>() 
            { 
                {labelL.Text, Convert.ToDouble(numL.Value)}, 
                {labelB.Text, Convert.ToDouble(numB.Value)}, 
                {labelHsp.Text, Convert.ToDouble(numHsp.Value)},
                {lblF05.Text, Convert.ToDouble(numF05.Value)}, 
                {lblF25.Text, Convert.ToDouble(numF25.Value)}, 
                {lblFeL.Text, Convert.ToDouble(numFel.Value)}
            };

            labReport.LabResults = LabResults;

            //string chartimage = "";
            MemoryStream chartimage = new MemoryStream();

            ChartFaF.SaveImage(chartimage, ChartImageFormat.Png);

            labReport.ChartImage = chartimage;

            labReport.ChartData = Qds.ToList();

            labReport.RunReport(); 
        }
    }
}
