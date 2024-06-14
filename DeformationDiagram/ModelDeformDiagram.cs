using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFiberConcrete.DeformationDiagram
{
    public static class DataForDeformDiagram 
    {
        public static string typeMaterial;
        public static string typeDiagram;

        #region Характеристики Фибробетона для построения диаграммы деформации на растяжение
        /// <summary>
        /// норматиыное сопротивление на Растяжение кг/см2
        /// </summary>
        public static decimal Rfbt_n;
        /// <summary>
        /// Нормативное остаточное сопротивление осевому растяжению кг/см2
        /// </summary>
        public static decimal Rfbt2_n;
        /// <summary>
        /// Нормативное остаточное сопротивление осевому растяжению кг/см2
        /// </summary>
        public static decimal Rfbt3_n;
        /// <summary>
        /// Начальный модуль упругости кг/см2
        /// </summary>
        public static decimal Efb;
        // Относительные деформации
        public static decimal efbt2;
        public static decimal efbt3;
        #endregion


        #region Характеристик бетона (фибробетона) для построения диаграммы деформации на сжатие 
        /// <summary>
        /// норматиыное сопротивление на сжатие кг/см2
        /// </summary>
        public static decimal Rb_n;
        /// <summary>
        /// Начальный модуль упругости кг/см2
        /// </summary>
        public static decimal Eb;
        // Относительные деформации
        public static decimal eb0;
        public static decimal eb2;
        # endregion 

        public static decimal[,] Calculate()
        {
            decimal[,] result = new decimal[1, 1];
            if (typeMaterial == "Бетон")
            {
                decimal eb1 = Rb_n / Eb;
                if (typeDiagram == "Двухлинейная")
                {
                    result = new decimal[2, 3]
                    { {0, eb1, eb2 }, { 0, Rb_n, Rb_n} };
                }
                else if (typeDiagram == "Трехлинейная")
                {
                    decimal Rb1 = Rb_n * 0.6m;
                    result = new decimal[2, 4]
                    { {0, eb1, eb0, eb2 }, { 0, Rb1, Rb_n, Rb_n} };
                }

            }
            else if (typeMaterial == "Фибробетон")
            {
                decimal efbt0 = Rfbt_n / Efb;
                decimal efbt1 = efbt0 + 0.0001m;
                //efbt2 = 0.004m;
                //efbt3 = Math.Abs(0.02m - 0.0125m * (Rfbt3_n / Rfbt2_n));
                decimal eb1 = Rb_n / Eb;
                if (typeDiagram == "Двухлинейная")
                {
                    result = new decimal[2, 7]
                    { {-efbt3, -efbt2, -efbt1, -efbt0, 0, eb0, eb2 }, { -Rfbt3_n, -Rfbt2_n, -Rfbt_n, -Rfbt_n, 0, Rb_n, Rb_n} };
                }
                else if (typeDiagram == "Трехлинейная")
                {
                    decimal Rb1 = Rb_n * 0.6m;
                    result = new decimal[2, 8]
                    { {-efbt3, -efbt2, -efbt1, -efbt0, 0, eb1, eb0, eb2 }, { -Rfbt3_n, -Rfbt2_n, -Rfbt_n, -Rfbt_n, 0, Rb1, Rb_n, Rb_n} };
                }
            }    
            return result;
        }





    }

}
