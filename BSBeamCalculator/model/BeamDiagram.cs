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
            { throw new Exception("Программная ошибка. Неккорректно определены характеристики бакли."); }


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
            double[][] values_xQ_xM= _diagramType.CalculateValuesForDiagram(_beamLength, _startPointForce, _endPointForce, _force);
            DiagramResult result = new DiagramResult(values_xQ_xM);

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
            "Pinned-Movable"
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
            // Pinned-Movable support

            // Concentrated load 


            double a = c1;
            double b = length - c1;
            double al = a / length;
            double bl = b / length;

            switch (supportBeamType)
            {
                case "Fixed-Fixed":
                    //double a = c1;
                    //double b = length - c1;

                    if (loadBeamType == "Concentrated")
                    {
                        // Определение реакции опоры:
                        double R1 = F * (3 * a + b) * Math.Pow((b), 2) / Math.Pow(length, 3);
                        double R2 = F * (a + 3 * b) * Math.Pow(a, 2) / Math.Pow(length, 3);

                        double M1 = F * a * Math.Pow(b, 2) / Math.Pow(length, 2);
                        double M2 = F * b * Math.Pow(a, 2) / Math.Pow(length, 2);


                        double[] xQ = new double[] { 0, a, a, length };
                        double[] xM = new double[] { 0, a, length };

                        // массивы для хранения значений силы и моментов
                        double[] Q = new double[] { R1, R1, R1 - F, R1 - F };
                        double[] M = new double[] { M1, M1 - R1 * a, M2 };
                        return new double[4][] { xQ, Q, xM, M };
                    }
                    else if (loadBeamType == "Uniformly-Distributed")
                    {
                        // Определение реакции опоры:
                        double R1 = F * length / 2;
                        double R2 = F * length / 2;

                        double M1 = F * Math.Pow(length, 2) / 12;
                        double M2 = F * Math.Pow(length, 2) / 12;

                        // Определение характерных точек для построения эпюры сил
                        double[] xQ = new double[] { 0, length };
                        double[] Q = new double[] { R1, -R2 };

                        // Точки для эпюры моментов
                        int n = 100; // кол-во точек
                        double[] xM = new double[n];
                        double[] M = new double[n];
                        // шаг точек
                        double m = length / (n - 1);
                        for (int i = 0; i < xM.Length; i++)
                        {
                            double tmpX = i * m;
                            double tmpM = (M1 - R1 * tmpX + F * tmpX * tmpX / 2);
                            xM[i] = tmpX;
                            M[i] = tmpM;
                        }
                        return new double[4][] { xQ, Q, xM, M };
                    }
                    break;

                case "Fixed-No":

                    if (loadBeamType == "Concentrated")
                    {
                        // Определение реакций:
                        double R1 = F;
                        double M1 = F * a;
                        
                        double[] xQ = new double[] { 0, a, a, length };
                        double[] xM = new double[] { 0, a, length };

                        // массивы для хранения значений силы и моментов
                        double[] Q = new double[] { R1, R1, 0,0 };
                        double[] M = new double[] { M1, 0, 0 };
                        return new double[4][] { xQ, Q, xM, M };
                    }
                    else if (loadBeamType == "Uniformly-Distributed")
                    {
                        // Определение реакций:
                        double R1 = F * length;
                        double M1 = F * Math.Pow(length,2) /2;

                        double[] xQ = new double[] { 0, length };
                        double[] Q = new double[] { R1, 0};

                        // Точки для эпюры моментов
                        int n = 100; // кол-во точек
                        double[] xM = new double[n];
                        double[] M = new double[n];
                        // шаг точек
                        double m = length / (n - 1);
                        for (int i = 0; i < xM.Length; i++)
                        {
                            double tmpX = i * m;
                            double tmpM = (M1 - R1 * tmpX + F * tmpX * tmpX / 2);
                            xM[i] = tmpX;
                            M[i] = tmpM;
                        }
                        return new double[4][] { xQ, Q, xM, M };
                    }    
                    break;
                case "No-Fixed":

                    if (loadBeamType == "Concentrated")
                    {
                        // Определение реакций:
                        double R1 = F;
                        double M1 = F * a;

                        double[] xQ = new double[] { 0, a, a, length };
                        double[] xM = new double[] { 0, a, length };

                        // массивы для хранения значений силы и моментов
                        double[] Q = new double[] { R1, R1, 0, 0 };
                        double[] M = new double[] { M1, 0, 0 };


                        //Array.Reverse(xQ);
                        Array.Reverse(Q);
                        //Array.Reverse(xM);
                        Array.Reverse(M);
                        return new double[4][] { xQ, Q, xM, M };
                    }
                    else if (loadBeamType == "Uniformly-Distributed")
                    {
                        // Определение реакций:
                        double R1 = F * length;
                        double M1 = F * Math.Pow(length, 2) / 2;

                        double[] xQ = new double[] { 0, length };
                        double[] Q = new double[] { R1, 0 };

                        // Точки для эпюры моментов
                        int n = 100; // кол-во точек
                        double[] xM = new double[n];
                        double[] M = new double[n];
                        // шаг точек
                        double m = length / (n - 1);
                        for (int i = 0; i < xM.Length; i++)
                        {
                            double tmpX = i * m;
                            double tmpM = (M1 - R1 * tmpX + F * tmpX * tmpX / 2);
                            xM[i] = tmpX;
                            M[i] = tmpM;
                        }

                        //Array.Reverse(xQ);
                        Array.Reverse(Q);
                        //Array.Reverse(xM);
                        Array.Reverse(M);
                        return new double[4][] { xQ, Q, xM, M };
                    }
                    break;

                case "Fixed-Movable":
                    //double a = c1;
                    //double b = length - c1;
                    //double al = a / length;
                    //double bl = b / length;
                    if (loadBeamType == "Concentrated")
                    {
                        // Определение реакции опоры:
                        double R1 = F * bl * (3 - Math.Pow(bl, 2)) / 2;
                        double R2 = F * Math.Pow(al, 2) * (3 - al) / 2;

                        double M1 = F * a * b * (length + b)/(2 * Math.Pow(length, 2));

                        double[] xQ = new double[] { 0, a, a, length };
                        double[] xM = new double[] { 0, a, length };

                        // массивы для хранения значений силы и моментов
                        double[] Q = new double[] { R1, R1, R1 - F, R1 - F };
                        double[] M = new double[] { M1, M1 - R1 * a, 0 };
                        return new double[4][] { xQ, Q, xM, M };
                    }
                    else if (loadBeamType == "Uniformly-Distributed")
                    {
                        // Определение реакции опоры:
                        double R1 = F * length * 5 / 8;
                        double R2 = F * length * 3 / 8;

                        double M1 = F * Math.Pow(length, 2) / 8;

                        // Определение характерных точек для построения эпюры сил
                        double[] xQ = new double[] { 0, length };
                        double[] Q = new double[] { R1, -R2 };

                        // Точки для эпюры моментов
                        int n = 100; // кол-во точек
                        double[] xM = new double[n];
                        double[] M = new double[n];
                        // шаг точек
                        double m = length / (n - 1);
                        for (int i = 0; i < xM.Length; i++)
                        {
                            double tmpX = i * m;
                            double tmpM = (M1 - R1 * tmpX + F * tmpX * tmpX / 2);
                            xM[i] = tmpX;
                            M[i] = tmpM;
                        }
                        return new double[4][] { xQ, Q, xM, M };
                    }
                    break;



                case "Movable-Fixed":
                    //double a = c1;
                    //double b = length - c1;
                    //double al = a / length;
                    //double bl = b / length;
                    if (loadBeamType == "Concentrated")
                    {
                        // Определение реакции опоры:
                        double R1 = F * bl * (3 - Math.Pow(bl, 2)) / 2;
                        double R2 = F * Math.Pow(al, 2) * (3 - al) / 2;

                        double M1 = F * a * b * (length + b) / (2 * Math.Pow(length, 2));

                        double[] xQ = new double[] { 0, a, a, length };
                        double[] xM = new double[] { 0, a, length };

                        // массивы для хранения значений силы и моментов
                        double[] Q = new double[] { R1, R1, R1 - F, R1 - F };
                        double[] M = new double[] { M1, M1 - R1 * a, 0 };

                        //Array.Reverse(xQ);
                        Array.Reverse(Q);
                        //Array.Reverse(xM);
                        Array.Reverse(M);
                        return new double[4][] { xQ, Q, xM, M };
                    }
                    else if (loadBeamType == "Uniformly-Distributed")
                    {
                        // Определение реакции опоры:
                        double R1 = F * length * 5 / 8;
                        double R2 = F * length * 3 / 8;

                        double M1 = F * Math.Pow(length, 2) / 8;

                        // Определение характерных точек для построения эпюры сил
                        double[] xQ = new double[] { 0, length };
                        double[] Q = new double[] { R1, -R2 };

                        // Точки для эпюры моментов
                        int n = 100; // кол-во точек
                        double[] xM = new double[n];
                        double[] M = new double[n];
                        // шаг точек
                        double m = length / (n - 1);
                        for (int i = 0; i < xM.Length; i++)
                        {
                            double tmpX = i * m;
                            double tmpM = (M1 - R1 * tmpX + F * tmpX * tmpX / 2);
                            xM[i] = tmpX;
                            M[i] = tmpM;
                        }

                        //Array.Reverse(xQ);
                        Array.Reverse(Q);
                        //Array.Reverse(xM);
                        Array.Reverse(M);
                        return new double[4][] { xQ, Q, xM, M };
                    }
                    break;
                case "Pinned-Movable":
                    if (loadBeamType == "Uniformly-Distributed")
                    {
                        // Определение реакции опоры:
                        double R1 = F * length / 2;
                        double R2 = F * length / 2;

                        // Определение характерных точек для построения эпюры сил
                        double[] xQ = new double[] { 0, length };
                        double[] Q = new double[] { R1, -R2 };

                        // Точки для эпюры моментов
                        int n = 100; // кол-во точек
                        double[] xM = new double[n];
                        double[] M = new double[n];
                        // шаг точек
                        double m = length / (n - 1);
                        for (int i = 0; i < xM.Length; i++)
                        {
                            double tmpX = i * m;
                            double tmpM = (R1 * tmpX - F * tmpX * tmpX / 2) * -1;
                            xM[i] = tmpX;
                            M[i] = tmpM;
                        }
                        return new double[4][] { xQ, Q, xM, M };
                    }
                    else if (loadBeamType == "Concentrated")
                    {
                        // Определение реакции опоры:
                        double R1 = F * (length - c1) / length;
                        double R2 = F * c1 / length;

                        // задаем характерные точки по x
                        double[] xQ = new double[] { 0, c1, c1, length };
                        double[] xM = new double[] { 0, c1, length };
                        // массивы для хранения значений силы и моментов
                        double[] Q = new double[] { R1, R1, R1 - F, R1 - F };
                        double[] M = new double[] { -R1 * 0, -R1 * c1,- R2 * (length - length) };


                        //double Q;
                        //double M;
                        //for (int i = 0; i < x.Count(); i++)
                        //{
                        //    double tmpX = x[i];
                        //    if (0 <= tmpX && tmpX <= c1)
                        //    {
                        //        Q = R1;
                        //        M = -R1 * tmpX;
                        //    }
                        //    else if (tmpX > c1 && tmpX <= length)
                        //    {
                        //        Q = R1 - F;
                        //        M = -R2 * (length - tmpX);
                        //    }
                        //    else
                        //    { throw new Exception("Программная ошибка. Некорректно опреджелены точки для построения графика"); }
                        //    result[1][i] = Q;
                        //    result[2][i] = M;
                        //}
                        return new double[4][]{ xQ, Q, xM, M };
                    }
                    break;
            }
            return new double[1][];
        }
    }

    public class DiagramResult 
    {
        public double[][] pointQ;
        public double[][] pointM;
        public List<double> maxPointQ;
        public List<double> maxPointM;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values_xQ_xM">4 мерный вложенный массив
        /// 0 - значения x для Q, 1 - значения Q; 2 - значения x для M, 3 - значения M  
        /// </param>
        public DiagramResult(double[][] values_xQ_xM)
        {
            //pointQ = new double[,] { values_xQ_xM[0], values_xQ_xM[1] }
            pointQ = new double[][]{
                values_xQ_xM[0],
                values_xQ_xM[1]
            };
            pointM = new double[][]{
                values_xQ_xM[2],
                values_xQ_xM[3]
            };
            maxPointQ = FindeMaxAbsValue(new double[][] { values_xQ_xM[0], values_xQ_xM[1] });
            maxPointM= FindeMaxAbsValue(new double[][] { values_xQ_xM[2], values_xQ_xM[3] });
        }

        /// <summary>
        /// Функция определяет максимальное (по модолю) значение во втором массиве
        /// и возвращает пару x_value[i][0] x_value[i][1], i - индекс максимального значения
        /// </summary>
        /// <param name="x_value"></param>
        /// <returns></returns>
        public static List<double> FindeMaxAbsValue(double[][]x_value)
        {
            List<double>  maxAbsPoint = new List<double>();
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
