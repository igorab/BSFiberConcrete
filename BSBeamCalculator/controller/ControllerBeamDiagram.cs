using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml.Linq;
using System.Xml.Schema;

namespace BSBeamCalculator
{
                    public class ControllerBeamDiagram
    {
                                public double l;
                                public string support;
                                public string load;
                                public double f;
                                public double x1;
                                        public double x2;
                                public DiagramResult result;

        public Dictionary<string, double> resultEfforts;

        public BeamDiagram beamDiagram;


                                public List<string> path2BeamDiagrams;

                                private int _numChart = 1;

        public ControllerBeamDiagram(List<string> path2Diagrams = null )
        {
            path2BeamDiagrams = path2Diagrams;
        }

        public void RunCalculation()
        {
            beamDiagram = new BeamDiagram(support, load, l, f, x1, x2);
            result = beamDiagram.CalculateBeamDiagram();

                                                                                }


                                                public double GetM(DiagramResult res, double x)
        {

            if (x < 0 && x > l) 
            { return 0; }

            if (x == 0)
            { return res.pointM[1][1]; }

            if (x == l)
            { return res.pointM[1][res.pointM[1].Length-2]; }

            List<double> X = res.pointM[0].ToList();
            List<double> M = res.pointM[1].ToList();

            if (X.Contains(x))
            { return M[X.IndexOf(x)]; }

            int index = 0;
            for (int i = 0; X.Count > i; i++)
            {
                if ((x - X[i]) < 0)
                {
                    index = i;
                    break;
                }
            }

            double resultM;
            double ro = (x - X[index - 1]) / (X[index] - X[index - 1]);
            resultM = M[index - 1] + (M[index] - M[index - 1]) * ro;

            return resultM;

        }


        public void Test()
        {
                        int n = 20;
                        int m = n + 1 + n;
                        double delta = l / (2 * n);

                        double d = 2d * 1000000d * 520800d;

            List<double> X = new List<double>();
            List<double> M = new List<double>();
            List<double> D = new List<double>();

            for (int i = 0; m > i; i++)
            {
                double tmpX = delta * i;
                double tmpM = GetM(result, tmpX);
                X.Add(tmpX);
                M.Add(tmpM);

                if (i > 0 && i % 2 != 0)
                { D.Add(d); }
            }

            List<double> XForChart = new List<double>();
            List<double> U = new List<double>();
            for (int i = 1; m > i; i = i + 2)
            {
                double u = CalculateDeflectionAtPoint(M, X, D, i);
                XForChart.Add(X[i]);
                U.Add(u);
            }

            string[] names = { "Прогиб", "см", "см", "BeamDiagramUTest" };
            CreteChart(XForChart, U, names);


            if (beamDiagram.simpleDiagram.IsCalculateBeamDeflection)
            {
                List<double> simpleU = new List<double>();
                for (int i = 1; m > i; i = i + 2)
                { simpleU.Add(beamDiagram.simpleDiagram.CalculateBeamDeflection(X[i], d)); }
                CreteChart(XForChart, simpleU, new string[] { "Прогиб", "см", "см", "SimpleBeamDiagramU" });
            }
        }


        public double CalculateDeflectionAtPoint(List<double> M, List<double> X, List<double> D, int index)
        {
            string forceType = "Concentrated";
            double forceValue = 1;

            BeamDiagram beamDiagram = new BeamDiagram(support, forceType, l, forceValue, X[index], 0);
            DiagramResult res = beamDiagram.CalculateBeamDiagram();


                        
            double sectionLength = X[index + 1] - X[index - 1];
            double u = 0;

            for (int i = 1; X.Count > i; i = i + 2)
            { 
                double a = M[i-1];
                double b = M[i];
                double c = M[i+1];
                if (b == 0)
                { 
                                        continue;
                }
                double a1 = GetM(res, X[i - 1]);
                double b1 = GetM(res, X[i]);
                double c1 = GetM(res, X[i + 1]);

                int num = (i - 1) / 2;
                double tmpU = sectionLength / (6 * D[num]) * (a * a1 + 4 * b * b1 + c * c1);
                u = u + tmpU;
            }
            return -u;
        }



                                                        public void BreakBeamIntoSections(List<double> X, List<double> valueMomentInX, List<double> valuesMomentOnSection)
        {
                        int n = 20;
                        int m = n + 1 + n;
                        double delta = this.l / (2 * n);

            for (int i = 0; m > i; i++)
            {
                double tmpX = delta * i;
                double tmpM = this.GetM(this.result, tmpX);

                X.Add(tmpX);
                valueMomentInX.Add(tmpM);

                if (i > 0 && i % 2 != 0)
                { valuesMomentOnSection.Add(tmpM); }
            }
        }


                                public double  CalculateDeflectionDiagram(List<double> X, List<double> valueMomentInX, List<double> valuesStiffnesOnSection)
        {
            double deflexionMax = 0;
                        List<double> U = new List<double>();
            List<double> XForChart = new List<double>();
            for (int i = 1; X.Count > i; i = i + 2)
            {
                double u = this.CalculateDeflectionAtPoint(valueMomentInX, X, valuesStiffnesOnSection, i);
                U.Add(u * 10);                 XForChart.Add(X[i]);
                if (deflexionMax > u * 10)                 { deflexionMax = u * 10; }
            }
            string[] names = { "Прогиб", "см", "мм", "BeamDiagramU" };
            this.CreteChart(XForChart, U, names);
            return deflexionMax;
        }

                                                        public void CalculateDeflectionDiagramByFormula(List<double> X, List<double> valuesStiffnesOnSection)
        {
                        if (this.beamDiagram.simpleDiagram.IsCalculateBeamDeflection)
            {
                double d = 0;
                foreach (double Stiffnes in valuesStiffnesOnSection)
                {
                    if (double.IsNaN(Stiffnes)) { continue; }
                    d = (d + Stiffnes) / 2;
                }
                List<double> simpleU = new List<double>();
                List<double> XForChart = new List<double>();
                for (int i = 1; X.Count > i; i = i + 2)
                {
                    XForChart.Add(X[i]);
                    double tmpU = this.beamDiagram.simpleDiagram.CalculateBeamDeflection(X[i], d) * 10;
                    simpleU.Add(tmpU);
                }
                this.CreteChart(XForChart, simpleU, new string[] { "Прогиб по формуле", "cм", "мм", "SimpleBeamDiagramU" });
            }
        }

        public void CreteChart(List<double> valueX, List<double> valueY, string[] names)
        {
            System.Windows.Forms.DataVisualization.Charting.Chart chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Title title = new System.Windows.Forms.DataVisualization.Charting.Title();

            string numChart = _numChart.ToString();

            string CAName = $"ChartArea{numChart}";
            string cName = $"chart{numChart}";
            string tName = $"Title{numChart}";
            string sName = $"Series{numChart}";

                        string textName = names[0];
            string titleX = names[1];
            string titleY = names[2];
            string name2Save = names[3];

            chartArea.Name = CAName;
            chart.Name = cName;
            chart.Text = cName;

            title.Name = tName;
            title.Text = textName;

            chart.ChartAreas.Add(chartArea);
            chart.Dock = System.Windows.Forms.DockStyle.Fill;
            chart.Location = new System.Drawing.Point(3, 3);
            chart.Size = new System.Drawing.Size(664, 217);
            chart.TabIndex = 0;
            chart.Titles.Add(title);

            chart.Series.Add(sName);
            chart.Series[sName].BorderWidth = 4;
            chart.ChartAreas[0].AxisX.Minimum = 0;
            chart.ChartAreas[0].AxisX.Maximum = l;
            chart.Series[sName].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            for (int i = 0; i < valueX.Count; i++)
            {  chart.Series[sName].Points.AddXY(valueX[i], valueY[i]); }

            Font axisFont = new System.Drawing.Font("Microsoft Sans Serif", 8F,
                ((System.Drawing.FontStyle)(System.Drawing.FontStyle.Bold)), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            chart.ChartAreas[0].AxisX.Title = titleX;
            chart.ChartAreas[0].AxisX.TitleFont = axisFont;
            chart.ChartAreas[0].AxisY.Title = titleY;
            chart.ChartAreas[0].AxisY.TitleFont = axisFont;

            SaveChart(chart, name2Save);
        }


                                public void SaveChart(System.Windows.Forms.DataVisualization.Charting.Chart chart, string pictureName)
        {
            if (path2BeamDiagrams != null)
            {
                string pathToPicture = pictureName + ".png";
                chart.SaveImage(pathToPicture, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Png);
                                if (pictureName == "BeamDiagramU")
                { 
                    if (path2BeamDiagrams.Count > 2)
                    {
                        path2BeamDiagrams[2] = pathToPicture;
                        return;
                    }
                }
                if (pictureName == "SimpleBeamDiagramU")
                {
                    if (path2BeamDiagrams.Count > 3)
                    {
                        path2BeamDiagrams[3] = pathToPicture;
                        return;
                    }
                }
                path2BeamDiagrams.Add(pathToPicture);
            }
            _numChart++;
        }
    }


    public class DiagramResult
    {
        public double[][] pointQ;
        public double[][] pointM;
        public double maxM;
        public double minM;
        public double maxAbsQ;


                                                        public DiagramResult(double[][] values_xQ_xM)
        {
                        pointQ = new double[][]{
                values_xQ_xM[0],
                values_xQ_xM[1]
            };
            pointM = new double[][]{
                values_xQ_xM[2],
                values_xQ_xM[3]
            };
                                    List<double> maxAbsPointQ = FindeMaxAbsValue(new double[][] { values_xQ_xM[0], values_xQ_xM[1] });

            maxAbsQ = maxAbsPointQ[1];
            maxM = values_xQ_xM[3].Max();
            minM = values_xQ_xM[3].Min();

        }

                                                        public static List<double> FindeMaxAbsValue(double[][] x_value)
        {
            List<double> maxAbsPoint = new List<double>();
            int indMaxAbs_Value;
            double max_value = x_value[1].Max();
            double min_value = x_value[1].Min();
            if (max_value >= Math.Abs(min_value))
            { indMaxAbs_Value = x_value[1].ToList().IndexOf(max_value); }
            else
            { indMaxAbs_Value = x_value[1].ToList().IndexOf(min_value); }
            maxAbsPoint = new List<double>() { x_value[0][indMaxAbs_Value], x_value[1][indMaxAbs_Value] };

            return maxAbsPoint;
        }

    }
}
