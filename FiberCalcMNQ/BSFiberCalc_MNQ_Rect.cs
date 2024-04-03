using MathNet.Numerics.Integration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BSFiberConcrete
{
    /// <summary>
    /// Прямоугольная балка
    /// </summary>
    public class BSFiberCalc_MNQ_Rect : BSFiberCalc_MNQ
    {
        private string m_ImgCalc;
        private Dictionary<string, double> m_Result;

        public BSFiberCalc_MNQ_Rect()
        {
            m_Beam = new BSBeam_Rect();
            m_Result = new Dictionary<string, double>();
        }

        /// <summary>
        /// Вернуть изрображение расчетной схемы
        /// </summary>
        /// <returns></returns>
        public override string ImageCalc()
        {
            if (!string.IsNullOrEmpty(m_ImgCalc))
                return m_ImgCalc;

            return (N_Out) ? "Rect_N_out.PNG" : "Rect_N.PNG";
        } 
        
        public override void GetSize(double[] _t)
        {
            (b, h, l0) = (m_Beam.b, m_Beam.h, m_Beam.Length) = (_t[0], _t[1], _t[2]);

            A = m_Beam.Area();

            I = m_Beam.Jy();
            
            y_t = m_Beam.y_t;
        }

        /// <summary>
        /// Расчет внецентренно сжатых элементов (6.1.13)
        /// </summary>
        protected new void Calculate_N()
        {
            base.Calculate_N();            
        }

        /// <summary>
        /// Расчет внецентренно сжатых сталефибробетонных
        /// элементов прямоугольного сечения с рабочей арматурой
        /// </summary>
        protected new void Calculate_N_Rods()
        {
            m_ImgCalc = "Rect_Rods_N_out.PNG";

            base.Calculate_N_Rods();            
        }

        /// <summary>
        /// Расчет внецентренно сжатых сталефибробетонных элементов без рабочей арматуры при
        /// расположении продольной сжимающей силы за пределами поперечного сечения элемента и внецентренно сжатых сталефибробетонных элементов без рабочей арматуры при расположении продольной
        /// сжимающей силы в пределах поперечного сечения элемента, в которых по условиям эксплуатации не
        /// допускается образование трещин
        /// </summary>
        private new void Calculate_N_Out()
        {
            base.Calculate_N_Out();            
        }

        /// <summary>
        /// Расчет элементов по полосе между наклонными сечениями
        /// </summary>
        private new void CalculateQ()
        {
            m_ImgCalc = "Incline_Q.PNG";

            base.CalculateQ();            
        }
       
        /// <summary>
        ///  Расчет элементов по наклонным сечениям на действие моментов
        /// </summary>
        private new void CalculateM()
        {
            base.CalculateM();            
        }

        /// <summary>
        ///  Вычислить
        /// </summary>
        public override void Calculate()
        {            
            if (Shear)
            {                
                // Расчет на действие поперечной силы
                CalculateQ();
                // Расчет на действие моментов
                CalculateM();
            }
            else if (UseRebar)
            {
                Calculate_N_Rods();
            }
            else
            {                
                if (N_Out)
                {
                    Calculate_N_Out();
                }
                else
                {
                    Calculate_N();
                }
            }
        }

        public override Dictionary<string, double> Results()
        {
            return new Dictionary<string, double>() { { "M_ult", M_ult }, { "Q_ult", Q_ult }, { "N_ult", N_ult } };
        }
    }
}
