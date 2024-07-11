using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFiberConcrete.CalcGroup2
{
    /// <summary>
    /// Расчет по 2 группе предельных состояний
    /// </summary>
    public class BSCalcNDM_Fiber
    {
        private double Rbt_ser = 0;
        // Изгибающий момент, воспр нормальным сечением элемента при образовании трещин
        private double Mcrс = 0;
        // упругопластический момент сопротивления сечения для крайнего растчянутого волокна
        private double Wpl = 0;

        // изгиб момент от внешней нагрузки
        public double M = 0;

        public double N = 0;
        public double e_x = 0;

        public BSCalcNDM_Fiber()
        {

        }

        /// <summary>
        /// Условие
        /// </summary>
        /// <returns>Выполнено?</returns>
        public bool Condition()
        {
            return M > Mcrс;
        }

        public void Calculate()
        {
            double Wred = 0;

            Wpl = Wred; //6.2.9


            // 6.107
            Mcrс = Rbt_ser * Wpl + N * e_x;
        }

        //6.2.13
        private void CalcMcr()
        {

        }

        /// <summary>
        /// Расчет кривизны 
        /// п 6.2.31
        /// </summary>
        public void CalcCurvature()
        {
            double r, r1 = float.MaxValue, r2 = float.MaxValue;
            double k;

            k = 1 / r1 + 1 / r2;


        }

    }
}
