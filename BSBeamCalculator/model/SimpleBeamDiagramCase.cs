using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSBeamCalculator
{
    /// <summary>
    ///  Клас определяет формулу для построения эпюры моментов и сил бакли
    ///  в зависимости от защемления и типа нагрузки
    ///  дословный перевод: ПРОСТОЙ СЛУЧАЙ ДИАГРАММЫ БАЛКИ
    /// </summary>
    public class SimpleBeamDiagramCase
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

        /// <summary>
        /// Определен ли делегат CalculateBeamDeflection
        /// </summary>
        public bool IsCalculateBeamDeflection;
        /// <summary>
        /// Расчитать прогиб балки по ее длине
        /// </summary>
        public Func<double, double, double> CalculateBeamDeflection;


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
        /// Функция для определения момента и силы для конкретного simpleBeamCase
        /// </summary>
        /// <param name="length">Длина балки</param>
        /// <param name="c1">начальная точка приложения силы</param>
        /// <param name="c2">конечная точка приложения силы</param>
        /// <param name="F">значение силы</param>
        /// <returns>Результат в формате [ xQ, Q, xM, M]</returns>
        /// <exception cref="Exception"></exception>
        public double[][] CalculateValuesForDiagram(double length, double c1, double c2, double F)
        {
            double a = c1;
            double b = length - c1;
            double al = a / length;
            double bl = b / length;

            double R1 = 0;
            double R2 = 0;
            double M1 = 0;
            double M2 = 0;

            switch (supportBeamType)
            {
                case "Fixed-Fixed":
                    if (loadBeamType == "Concentrated")
                    {
                        // Определение реакций опоры и моментов
                        R1 = F * (3 * a + b) * Math.Pow((b), 2) / Math.Pow(length, 3);
                        R2 = F * (a + 3 * b) * Math.Pow(a, 2) / Math.Pow(length, 3);
                        M1 = F * a * Math.Pow(b, 2) / Math.Pow(length, 2);
                        M2 = F * b * Math.Pow(a, 2) / Math.Pow(length, 2);

                        IsCalculateBeamDeflection = false;
                        //CalculateBeamDeflection = (x, D) => { return F * Math.Pow(a, 3) * Math.Pow(b, 3) / (3 * D * length; }
                    }
                    else if (loadBeamType == "Uniformly-Distributed")
                    {
                        // Определение реакции:
                        R1 = F * length / 2;
                        R2 = F * length / 2;
                        M1 = F * Math.Pow(length, 2) / 12;
                        M2 = F * Math.Pow(length, 2) / 12;

                        IsCalculateBeamDeflection = false;
                        //CalculateBeamDeflection = (x, D) =>
                        //{ return F * Math.Pow(a, 3) * Math.Pow(b, 3) / (3 * D * length); };
                    }
                    break;

                case "Fixed-No":
                    if (loadBeamType == "Concentrated")
                    {
                        // Определение реакций:
                        R1 = F;
                        M1 = F * a;
                        R2 = 0;
                        M2 = 0;

                        IsCalculateBeamDeflection = true;
                        CalculateBeamDeflection = (x, D) =>
                        {
                            if (x <= a)
                            { return -F * Math.Pow(x, 2) / (6 * D) * (3 * a - x); }
                            return -F * Math.Pow(a, 2) / (6 * D) * (3 * x - a);
                        };
                    }
                    else if (loadBeamType == "Uniformly-Distributed")
                    {
                        // Определение реакций:
                        R1 = F * length;
                        R2 = 0;
                        M1 = F * Math.Pow(length, 2) / 2;
                        M2 = 0;

                        IsCalculateBeamDeflection = true;
                        CalculateBeamDeflection = (x, D) =>
                        {
                            return -F * Math.Pow(length, 4) / (24 * D) * (Math.Pow(x, 2) / Math.Pow(length, 2)) *
                            (6 - 4 * x / length + Math.Pow(x, 2) / Math.Pow(length, 2));
                        };
                    }
                    break;

                case "No-Fixed":
                    if (loadBeamType == "Concentrated")
                    {
                        // Определение реакций:
                        R1 = 0;
                        M1 = 0;
                        R2 = F;
                        M2 = F * b;

                        IsCalculateBeamDeflection = true;
                        CalculateBeamDeflection = (x, D) => {
                            double x1 = length - x;
                            if (x1 <= a)
                            { return -F * Math.Pow(x1, 2) / (6 * D) * (3 * a - x1); }
                            return -F * Math.Pow(a, 2) / (6 * D) * (3 * x1 - a);

                        };
                    }
                    else if (loadBeamType == "Uniformly-Distributed")
                    {
                        // Определение реакций:
                        R1 = 0;
                        R2 = F * length;
                        M1 = 0;
                        M2 = F * Math.Pow(length, 2) / 2;


                        IsCalculateBeamDeflection = true;
                        CalculateBeamDeflection = (x, D) =>
                        {
                            double x1 = length - x;
                            return F * Math.Pow(length, 4) / (24 * D) * (Math.Pow(x1, 2) / Math.Pow(length, 2)) *
                            (6 - 4 * x1 / length + Math.Pow(x1, 2) / Math.Pow(length, 2));
                        };
                    }
                    break;

                case "Fixed-Movable":
                    if (loadBeamType == "Concentrated")
                    {
                        // Определение реакций:
                        R1 = F * bl * (3 - Math.Pow(bl, 2)) / 2;
                        R2 = F * Math.Pow(al, 2) * (3 - al) / 2;
                        M1 = F * a * b * (length + b) / (2 * Math.Pow(length, 2));
                        M2 = 0;

                        IsCalculateBeamDeflection = false;
                        //CalculateBeamDeflection = (x, D) =>
                        //{ return F * Math.Pow(a, 3) * Math.Pow(b, 2) * (3 * a + 4 * b) / (12 * D * length); };
                    }
                    else if (loadBeamType == "Uniformly-Distributed")
                    {
                        // Определение реакции опоры:
                        R1 = F * length * 5 / 8;
                        R2 = F * length * 3 / 8;
                        M1 = F * Math.Pow(length, 2) / 8;
                        M2 = 0;

                        IsCalculateBeamDeflection = false;
                    }
                    break;

                case "Movable-Fixed":
                    if (loadBeamType == "Concentrated")
                    {
                        // Определение реакции опоры:
                        R1 = F * Math.Pow(bl, 2) * (3 - bl) / 2;
                        R2 = F * al * (3 - Math.Pow(al, 2)) / 2;
                        M1 = 0;
                        M2 = F * a * b * (length + a) / (2 * Math.Pow(length, 2));

                        IsCalculateBeamDeflection = false;
                        //CalculateBeamDeflection = (x, D) =>
                        //{ return F * Math.Pow(b, 3) * Math.Pow(a, 2) * (3 * b + 4 * a) / (12 * D * length); };
                    }
                    else if (loadBeamType == "Uniformly-Distributed")
                    {
                        // Определение реакции опоры:
                        R1 = F * length * 3 / 8;
                        R2 = F * length * 5 / 8;
                        M1 = 0;
                        M2 = F * Math.Pow(length, 2) / 8;

                        IsCalculateBeamDeflection = false;
                    }
                    break;

                case "Pinned-Movable":
                    if (loadBeamType == "Concentrated")
                    {
                        // Определение реакций:
                        R1 = F * (length - c1) / length;
                        R2 = F * c1 / length;
                        M1 = 0;
                        M2 = 0;

                        IsCalculateBeamDeflection = true;
                        CalculateBeamDeflection = (x, D) =>
                        {
                            return -F * b * Math.Pow(length, 2) / (6 * D) * x / length *
                            (1 - Math.Pow(b, 2) / Math.Pow(length, 2) - Math.Pow(x, 3) / Math.Pow(length, 3));
                        };
                    }
                    if (loadBeamType == "Uniformly-Distributed")
                    {
                        // Определение реакции опоры:
                        R1 = F * length / 2;
                        R2 = F * length / 2;
                        M1 = 0;
                        M2 = 0;

                        IsCalculateBeamDeflection = true;
                        CalculateBeamDeflection = (x, D) =>
                        {
                            return -F * Math.Pow(length, 4) / (24 * D) * (x / length)
                            * (1 - 2 * (Math.Pow(x, 2) / Math.Pow(length, 2)) + (Math.Pow(x, 3) / Math.Pow(length, 3)));
                        };
                    }
                    break;
            }
            if (loadBeamType == "Concentrated")
            {
                double[] load = new double[5] { F, R1, R2, M1, M2, };
                double[] len = new double[3] { length, a, b };
                return CalculateSimpleConcentratedLoad(load, len);
            }
            else if (loadBeamType == "Uniformly-Distributed")
            {
                double[] load = new double[5] { F, R1, R2, M1, M2, };
                double[] len = new double[3] { length, a, b };
                return CalculateSimpDistributedleLoad(load, len);
            }
            else
            { throw new Exception("Программная ошибка. Не предусмотренная нагрузка на балку."); }
        }


        /// <summary>
        ///  Метод для расчета значений силы и момента для построения эпюр.
        ///  Рассматривается общий случай сосредоточенной нагрузки
        /// </summary>
        /// <param name="load"> массив значений реакций [F, R1, R2, M1, M2]</param>
        /// <param name="length">Массив длин [Length, a, b ]</param>
        /// <returns>Результат в формате [ xQ, Q, xM, M]</returns>
        public double[][] CalculateSimpleConcentratedLoad(double[] load, double[] len)
        {
            double F = load[0];
            double R1 = load[1];
            double R2 = load[2];
            double M1 = load[3];
            double M2 = load[4];
            double length = len[0];
            double a = len[1];
            double b = len[2];

            // Значения длины для построения эпюр
            double[] xQ = new double[] { 0, 0, a, a, length, length };
            //double[] Q = new double[] { 0, R1, R1, R1-F, R1 - F, 0 };
            double[] Q = new double[] { 0, R1, R1, -R2, -R2, 0 };
            // Значения силы и момента для построения эпюр
            double[] xM = new double[] { 0, 0, a, length, length };
            double[] M = new double[] { 0, M1, M1 - R1 * a, M2, 0 };

            return new double[4][] { xQ, Q, xM, M };
        }


        /// <summary>
        ///  Метод для расчета значений силы и моменета для построения эпюр.
        ///  Рассматривается общий случай распределенной нагрузки
        /// </summary>
        /// <param name="load"> массив значений рекаций [F, R1, R2, M1, M2]</param>
        /// <param name="length">Массив длин [Length, a, b ]</param>
        /// <returns>Результат в формате [ xQ, Q, xM, M]</returns>
        public double[][] CalculateSimpDistributedleLoad(double[] load, double[] len)
        {
            double F = load[0];
            double R1 = load[1];
            double R2 = load[2];
            double M1 = load[3];
            double M2 = load[4];
            double length = len[0];
            double a = len[1];
            double b = len[2];

            // Определение характерных точек для построения эпюры сил
            double[] xQ = new double[] { 0, 0, length, length };
            double[] Q = new double[] { 0, R1, -R2, 0 };

            // Точки для эпюры моментов
            int n = 102; // кол-во точек
            int m = n - 2;
            double[] xM = new double[n];
            double[] M = new double[n];
            // шаг точек
            double t = length / (m - 1);
            for (int i = 1; i <= m; i++)
            {
                double tmpX = (i - 1) * t;
                double tmpM = (M1 - R1 * tmpX + F * tmpX * tmpX / 2);
                xM[i] = tmpX;
                M[i] = tmpM;
            }
            xM[0] = 0;
            xM[xM.Length - 1] = length;
            M[0] = 0;
            M[M.Length - 1] = 0;
            return new double[4][] { xQ, Q, xM, M };
        }
    }
}
