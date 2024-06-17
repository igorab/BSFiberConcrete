using MathNet.Numerics.Integration;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFiberConcrete.DeformationDiagram
{
    public class CalcDeformDiagram
    {
        public string typeMaterial;
        public string typeDiagram;

        #region Характеристики Фибробетона для построения диаграммы деформации на растяжение
        /// <summary>
        /// норматиыное сопротивление на Растяжение кг/см2
        /// </summary>
        public double Rfbt_n;
        /// <summary>
        /// Нормативное остаточное сопротивление осевому растяжению кг/см2
        /// </summary>
        public double Rfbt2_n;
        /// <summary>
        /// Нормативное остаточное сопротивление осевому растяжению кг/см2
        /// </summary>
        public double Rfbt3_n;
        /// <summary>
        /// Начальный модуль упругости кг/см2
        /// </summary>
        public double Efb;
        // Относительные деформации
        public double efbt0;
        public double efbt1;
        public double efbt2;
        public double efbt3;
        #endregion

        #region Характеристик бетона (фибробетона) для построения диаграммы деформации на сжатие 
        /// <summary>
        /// норматиыное сопротивление на сжатие кг/см2
        /// </summary>
        public double Rb_n;
        /// <summary>
        /// сопротивление на сжатие кг/см2
        /// </summary>
        public double Rb1;
        /// <summary>
        /// Начальный модуль упругости кг/см2
        /// </summary>
        public double Eb;
        // Относительные деформации
        public double eb0;
        public double eb1;
        public double eb2;
        # endregion 

        /// <summary>
        /// Массив, определяющий характерные значения относительных деформаций
        /// </summary>
        public double[] deformsArray;

        public CalcDeformDiagram()
        {
            typeMaterial = DataForDeformDiagram.typesDiagram[0];
            typeDiagram = DataForDeformDiagram.typesDiagram[1];
            Rb_n = DataForDeformDiagram.resists[0];
            Rfbt_n = DataForDeformDiagram.resists[1];
            Rfbt2_n =DataForDeformDiagram.resists[2];
            Rfbt3_n = DataForDeformDiagram.resists[3];

            eb0 = DataForDeformDiagram.deforms[0];
            eb2 = DataForDeformDiagram.deforms[1];
            efbt2 = DataForDeformDiagram.deforms[2];
            efbt3 = DataForDeformDiagram.deforms[3];

            Eb = DataForDeformDiagram.E[0];
            Efb = DataForDeformDiagram.E[1];

            if (typeDiagram == BSHelper.TwoLineDiagram)
            { Rb1 = Rb_n; }
            else if (typeDiagram == BSHelper.ThreLineDiagram)
            { Rb1 = Rb_n * 0.6; }
            eb1 = Rb1 / Eb;

            if (typeMaterial == BSHelper.Concrete)
            {
                if (typeDiagram == BSHelper.TwoLineDiagram)
                { deformsArray = new double[] { 0, eb1, eb2 }; }
                else if (typeDiagram == BSHelper.ThreLineDiagram)
                { { deformsArray = new double[] { 0, eb1, eb0, eb2 }; } }
            }
            else if (typeMaterial == BSHelper.FiberConcrete)
            {
                efbt0 = Rfbt_n / Efb;
                efbt1 = efbt0 + 0.0001;
                //efbt2 = 0.004m;
                //efbt3 = Math.Abs(0.02m - 0.0125m * (Rfbt3_n / Rfbt2_n));
                if (typeDiagram == BSHelper.TwoLineDiagram)
                { deformsArray = new double[] { -efbt3, -efbt2, -efbt1, -efbt0, 0, eb1, eb2 }; }
                else if (typeDiagram == BSHelper.ThreLineDiagram)
                { { deformsArray = new double[] { -efbt3, -efbt2, -efbt1, -efbt0, 0, eb1, eb0, eb2 }; } }
            }
        }
    

        /// <summary>
        /// 
        /// </summary>
        /// <param name="types">types[0] = typeMaterial; types[1] = typeDiagram</param>
        /// <param name="resists">Rb_n = resists[0]; Rfbt_n = resists[1]; Rfbt2_n = resists[2]; Rfbt3_n = resists[3];</param>
        /// <param name="deforms"> eb0 = deforms[0]; eb2 = deforms[1]; efbt2 = deforms[2]; efbt3 = deforms[3];</param>
        /// <param name="E"> Eb = E[0]; Efb = E[1];</param>
        public CalcDeformDiagram(string[] types, double[] resists, double[] deforms, double[] E )
        {
            typeMaterial = types[0];
            typeDiagram = types[1];
            Rb_n = resists[0];
            Rfbt_n = resists[1];
            Rfbt2_n = resists[2];
            Rfbt3_n = resists[3];

            eb0 = deforms[0];
            eb2 = deforms[1];
            efbt2 = deforms[2];
            efbt3 = deforms[3];

            Eb = E[0];
            Efb = E[1];

            if (typeDiagram == BSHelper.TwoLineDiagram)
            { Rb1 = Rb_n; }
            else if (typeDiagram == BSHelper.ThreLineDiagram)
            { Rb1 = Rb_n * 0.6; }
            eb1 = Rb1 / Eb;

            if (typeMaterial == BSHelper.Concrete)
            {
                if (typeDiagram == BSHelper.TwoLineDiagram)
                { deformsArray = new double[] { 0, eb1, eb2 }; }
                else if (typeDiagram == BSHelper.ThreLineDiagram)
                { { deformsArray = new double[] { 0, eb1, eb0, eb2 }; } }
            }
            else if (typeMaterial == BSHelper.FiberConcrete)
            {
                efbt0 = Rfbt_n / Efb;
                efbt1 = efbt0 + 0.0001;
                //efbt2 = 0.004m;
                //efbt3 = Math.Abs(0.02m - 0.0125m * (Rfbt3_n / Rfbt2_n));
                if (typeDiagram == BSHelper.TwoLineDiagram)
                { deformsArray = new double[] { -efbt3, -efbt2, -efbt1, -efbt0, 0, eb1, eb2 }; }
                else if (typeDiagram == BSHelper.ThreLineDiagram)
                { { deformsArray = new double[] { -efbt3, -efbt2, -efbt1, -efbt0, 0, eb1, eb0, eb2 }; } }
            }
        }


        /// <summary>
        /// Определяется массив относительных деформаций и напряжений
        /// </summary>
        /// <returns></returns>
        public double[,] Calculate()
        {
            double[,] result = new double[1, 1];
            if (typeMaterial == BSHelper.Concrete)
            {
                if (typeDiagram == BSHelper.TwoLineDiagram)
                { result = new double[2, 3] { {0, eb1, eb2 }, { 0, Rb_n, Rb_n} }; }
                else if (typeDiagram == BSHelper.ThreLineDiagram)
                { result = new double[2, 4]{ {0, eb1, eb0, eb2 }, { 0, Rb1, Rb_n, Rb_n} }; }
            }
            else if (typeMaterial == BSHelper.FiberConcrete)
            {
                //efbt2 = 0.004m;
                //efbt3 = Math.Abs(0.02m - 0.0125m * (Rfbt3_n / Rfbt2_n));
                if (typeDiagram == BSHelper.TwoLineDiagram)
                {
                    result = new double[2, 7]
                    { {-efbt3, -efbt2, -efbt1, -efbt0, 0, eb1, eb2 }, { -Rfbt3_n, -Rfbt2_n, -Rfbt_n, -Rfbt_n, 0, Rb_n, Rb_n} };
                }
                else if (typeDiagram == BSHelper.ThreLineDiagram)
                {
                    result = new double[2, 8]
                    { {-efbt3, -efbt2, -efbt1, -efbt0, 0, eb1, eb0, eb2 }, { -Rfbt3_n, -Rfbt2_n, -Rfbt_n, -Rfbt_n, 0, Rb1, Rb_n, Rb_n} };
                }
            }    
            return result;
        }

        /// <summary>
        /// определяется напряжение для относительной деформации epsilon
        /// </summary>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        public double getResists(double epsilon)
        {
            double res = 0;
            if (epsilon > 0)
            {
                if (epsilon > eb2)
                { return res; }
                if (typeDiagram == BSHelper.ThreLineDiagram)
                {
                    if (0 < epsilon && epsilon <= eb1)
                    { res = Eb * epsilon; }
                    else if (eb1 < epsilon && epsilon <= eb0)
                    {
                        //res = ((1 - 0.6) * (epsilon - eb1) / (eb0 - eb1) + 0.6) * Rb_n;
                        res = (Rb1 + (Rb_n - Rb1) * (epsilon - eb1) / (eb0 - eb1));
                    }
                    else if (eb0 < epsilon && epsilon <= eb2)
                    { res = Rb_n; }
                }
                else if (typeDiagram == BSHelper.TwoLineDiagram)
                {
                    if (0 < epsilon && epsilon <= eb1)
                    { res = Eb * epsilon; }
                    else if (eb1 < epsilon && epsilon <= eb2)
                    { res = Rb_n; }
                }
            }
            else if (epsilon < 0 && typeMaterial == BSHelper.FiberConcrete)
            {
                if (epsilon < -efbt3)
                { return res; }

                if (epsilon < 0 && -efbt0 <= epsilon)
                { res = Efb * epsilon; }
                if (epsilon < -efbt0 && -efbt1 <= epsilon)
                { res = - Rfbt_n; }
                else if (epsilon < -efbt1 && -efbt2 <= epsilon)
                {
                    res = - Rfbt_n * (1 + (1 - Rfbt2_n / Rfbt_n) * (epsilon - efbt1) / (efbt2 - efbt1));
                    //res = Rfbt2_n * (1 - (1 - Rfbt_n / Rfbt2_n) * (epsilon + efbt2) / (efbt1 + efbt2));    
                }
                else if (epsilon < -efbt2 && -efbt3 <= epsilon)
                {
                    res = - Rfbt2_n * (1 + (1- Rfbt3_n / Rfbt2_n) * (epsilon + efbt2) / (efbt3 - efbt2));
                    //res = (Rfbt3_n + (Rfbt3_n - Rfbt2_n) * (epsilon + efbt3) / (efbt2 - efbt3));  
                }
            }
            return res;
        }
    }

    public static class DataForDeformDiagram
    {
        public static string[] typesDiagram;
        public static double[] resists;
        public static double[] deforms;
        public static double[] E; 

    }
}
