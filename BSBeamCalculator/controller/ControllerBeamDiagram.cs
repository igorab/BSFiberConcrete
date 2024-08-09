using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSBeamCalculator
{
    /// <summary>
    /// Класс нужен для сбора информации с формы
    /// и дальнейшей передачи информации в вычислительный класс BeamDiagram
    /// Для данных расчета используется класс 
    /// </summary>
    public static class ControllerBeamDiagram
    {
        /// <summary>
        /// Длинна балки
        /// </summary>
        public static double l;
        /// <summary>
        /// Тип защемления балки
        /// </summary>
        public static string support;
        /// <summary>
        /// тип нагрузки на балку
        /// </summary>
        public static string load;
        /// <summary>
        /// Значение силы на балку
        /// </summary>
        public static double f;
        /// <summary>
        /// координата приложение силы
        /// </summary>
        public static double x1;
        /// <summary>
        /// конечная координата приложение силы
        /// (для распределенной нагрузки)
        /// </summary>
        public static double x2;
        /// <summary>
        /// DiagramResult - класс для данных необходимых для построения грфика
        /// </summary>
        public static DiagramResult result;

        public static Dictionary<string, double> resultEfforts;


        public static void RunCalculation()
        {
            BeamDiagram BD = new BeamDiagram(support, load, l, f, x1, x2);
            result = BD.CalculateBeamDiagram();


            //if (resultEfforts.ContainsKey("Mmax"))
            //    resultEfforts["Mmax"] = result.maxM;
            //if (resultEfforts.ContainsKey("Mmin"))
            //    resultEfforts["Mmin"] = result.minM;
            //if (resultEfforts.ContainsKey("Q"))
            //    resultEfforts["Q"] = result.maxAbsQ;
        }

    }


    public class DiagramResult
    {
        public double[][] pointQ;
        public double[][] pointM;
        public double maxM;
        public double minM;
        public double maxAbsQ;


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
            //maxPointQ = FindeMaxAbsValue(new double[][] { values_xQ_xM[0], values_xQ_xM[1] });
            //maxPointM= FindeMaxAbsValue(new double[][] { values_xQ_xM[2], values_xQ_xM[3] });
            List<double> maxAbsPointQ = FindeMaxAbsValue(new double[][] { values_xQ_xM[0], values_xQ_xM[1] });

            maxAbsQ = maxAbsPointQ[1];
            maxM = values_xQ_xM[3].Max();
            minM = values_xQ_xM[3].Min();

        }

        /// <summary>
        /// Функция определяет максимальное (по модолю) значение во втором массиве
        /// и возвращает пару x_value[i][0] x_value[i][1], i - индекс максимального значения
        /// </summary>
        /// <param name="x_value"></param>
        /// <returns></returns>
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
