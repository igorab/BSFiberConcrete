using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;

namespace BSBeamCalculator.model
{
    public class BeamDiagram
    {
        
        protected double _force;
        protected double _startPointForce;
        protected double _endPointForce;
        protected double _beamLength;
        //protected string _supportBeamType;
        //protected string _loadBeamTupe;
        protected SimpleBeamDiagramCase _diagramType;

        public BeamDiagram(string supportType, string loadype, double length, double force, double x1, double x2)
        {
            bool a = SimpleBeamDiagramCase.supportBeamTypeValue.Contains(supportType);
            bool b = SimpleBeamDiagramCase.loadBeamTypeValue.Contains(loadype);

            //// проверка на корректность типа опор и типа нагрузки
            //if (!SimpleBeamDiagramCase.supportBeamTypeValue.Contains(supportType)
            //    && !SimpleBeamDiagramCase.loadBeamTypeValue.Contains(loadype))
            //{ throw new Exception("Программная ошибка. Неккорректно определены характеристики балки"); }

            // to do
            //
            // написать пролверки
            if (x1 > length)
            { throw new Exception("Программная ошибка. Неккорректно определены характеристики ghbkj;tyyjq "); }


            _beamLength = length;
            _diagramType = new SimpleBeamDiagramCase(supportType, loadype);
            //_supportBeamType = supportType;
            //_loadBeamTupe = loadype;
            _force = force;
            _startPointForce = x1; 
            //_endPointForce;
         }

        public DiagramResult CalculateBeamDiagram()
        {


            //List<double> x, double length, double c1, double c2, double F

            // кол-во точек
            int n = 11;
            double[] x = new double[n];
            // шаг точек
            double m = _beamLength / (n - 1);
            for (int i = 0; i < n; i++)
            { x[i] = i * m;  }
            double[][] values_X_Q_M= _diagramType.CalculateValuesForDiagram(_beamLength, _startPointForce, _endPointForce, _force);
            DiagramResult result = new DiagramResult(values_X_Q_M);

            return result;
        }



    }



    /// <summary>
    ///  Клас определяет формулу для построения эпюры моментов и сил бакли
    ///  в зависимости от защемления и типа нагрузки
    ///  дословный перевод: ПРОСТОЙ СЛУЧАЙ ДИАГРАММЫ БАЛКИ
    /// </summary>
    public  class SimpleBeamDiagramCase
    {
        /// <summary>
        /// тип опоры балки
        /// </summary>
        public string supportBeamType;
        /// <summary>
        /// тип нагрузки на балку
        /// </summary>
        public string loadBeamType;

        /// <summary>
        /// Рассматриваемые типы защемлений балки
        /// </summary>
        public static List<string> supportBeamTypeValue = new List<string>()
        { 
            "Fixed-No",
            "No-Fixed",
            "Fixed-Fixed",
            "Fixed-Movable",
            "Movable-Fixed",
            "Hinged-Movable"
        };
        /// <summary>
        /// Расссматриваемые типы нагрузок на балку
        /// </summary>
        public static List<string> loadBeamTypeValue = new List<string>()
        {
            "Uniformly-Distributed",
            "Concentrated",
        };

        public SimpleBeamDiagramCase(string supportType, string loadType)
        {
            if (supportBeamTypeValue.Contains(supportType) && loadBeamTypeValue.Contains(loadType))
            {
                this.supportBeamType = supportType;
                this.loadBeamType = loadType;
            }
            else
            {
                throw new Exception("Программная ошибка. Неккорректно определены характеристики балки");
            }
        }

        /// <summary>
        /// тестовая функция для определения момента и силы для конкретного simpleBeamCase
        /// </summary>
        /// <param name="x"></param>
        /// <param name="length"></param>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <param name="F"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public double[][] CalculateValuesForDiagram(double length, double c1, double c2, double F)
        {
            // Hinged-Movable support

            // Concentrated load 


            switch (supportBeamType)
            {
                case "Fixed-No":
                    break;
                case "Hinged-Movable":
                    if (loadBeamType == "Uniformly-Distributed")
                    { }
                    else if (loadBeamType == "Concentrated")
                    {
                        // Определение реакции опоры:
                        double R1 = F * (length - c1) / length;
                        double R2 = F * c1 / length;

                        double[] x = new double[] { 0, c1, c1+0.01, length };

                        double[][] result = {
                            x,
                            new double[x.Count()],
                            new double[x.Count()]
                        };

                        double Q;
                        double M;
                        for (int i = 0; i < x.Count(); i++)
                        {
                            double tmpX = x[i];
                            if (0 <= tmpX && tmpX <= c1)
                            {
                                Q = R1;
                                M = -R1 * tmpX;
                            }
                            else if (tmpX > c1 && tmpX <= length)
                            {
                                Q = R1 - F;
                                M = -R2 * (length - tmpX);
                            }
                            else
                            { throw new Exception("Программная ошибка. Некорректно опреджелены точки для построения графика"); }
                            result[1][i] = Q;
                            result[2][i] = M;
                        }
                        return result;
                    }
                    break;
            }
            return new double[1][];
        }
    }

    public class DiagramResult 
    {
        public List<double> valuesQ;
        public List<double> valuesM;
        public List<double> valuesx;
        public List<double> maxPointQ;
        public List<double> maxPointM;

        public DiagramResult(double[][] values_x_Q_M)
        {
            valuesx = values_x_Q_M[0].ToList();
            valuesQ = values_x_Q_M[1].ToList();
            valuesM = values_x_Q_M[2].ToList();
            

            double max_Q = values_x_Q_M[1].Max();            
            int indMax_Q = values_x_Q_M[1].ToList().IndexOf(max_Q);
            maxPointQ = new List<double>() { max_Q, values_x_Q_M[0][indMax_Q] };
            double max_M = values_x_Q_M[2].Max();
            int indMax_M = values_x_Q_M[2].ToList().IndexOf(max_M);
            maxPointM = new List<double>() { max_M, values_x_Q_M[0][indMax_M] };

        }

    }

}
