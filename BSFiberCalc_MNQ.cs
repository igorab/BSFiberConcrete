using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BSFiberConcrete
{

    /// <summary>
    /// Прямоугольная балка
    /// </summary>
    public class BSFiberCalc_MNQ_Rect : BSFiberCalc_MNQ
    {
        BSBeam_Rect beam = new BSBeam_Rect();

        public override void GetSize(double[] _t)
        {            
            (b, h, l0) = (beam.b, beam.h, beam.Length) = (_t[0], _t[1], _t[2]);

            A = beam.Area();

            I = beam.I_s();
        }
        
        public override void Calculate()
        {           
            double Q = m_Efforts['Q'];

            //Момент от действия полной нагрузки
            M1 = m_Efforts['M'];

            //Момент от действия постянных и длительных нагрузок нагрузок
            Ml1 = 1;

            //Коэффициент, учитывающий влияние длительности действия нагрузки, определяют по формуле (6.27)
            fi1 = (M1 != 0) ? 1 + Ml1 / M1 : 1.0;

            //относительное значение эксцентриситета продольной силы
            delta_e = e0 / h;

            if (delta_e <= 0.15)
            { delta_e = 0.15; }
            else if (delta_e >= 1.5)
            { delta_e = 1.5; }

            // Коэфициент ф.(6.26)
            k_b = 0.15 / (fi1 * (0.3d + delta_e));

            // Модуль упругости сталефибробетона п.п. (5.2.7)
            Efb = Eb * (1 - mu_fv) + Ef * mu_fv;

            //жесткость элемента в предельной по прочности стадии,определяемая по формуле (6.25)
            D = k_b * Efb * I;

            // условная критическая сила, определяемая по формуле (6.24)
            Ncr = Math.PI * Math.PI * D / Math.Pow(l0, 2);

            eta = 1 / (1 - N / Ncr);

            Ab = b * h * (1 - 2 * e0 * eta / h);

            Rfb = Rfbn / Yb * Yb1 * Yb2 * Yb3 * Yb5;

            N_ult = fi * Rfb * A;

            double flex = l0 / h;

            if (e0 <= h / 30 && l0 <= 20 * h)
            {
                N_ult = fi * Rfb * A;
            }
            else
            {
                N_ult = Rfb * Ab;
            }

            N_ult *= 0.001;
        }

    }

    public class BSFiberCalc_MNQ_Ring : BSFiberCalc_MNQ
    {
        BSBeam_Ring beam = new BSBeam_Ring();

        public override void GetSize(double[] _t)
        {
            (beam.r1, beam.r2) = (_t[0], _t[1]);
        }

        public override void GetParams(double[] _t)
        {
            base.GetParams(_t);

            e_N = 25;           
        }

        public override void Calculate()
        {
            double Ar = beam.Area();

            Rfb = Rfbn / Yb * Yb1 * Yb2 * Yb3 * Yb5;

            double Rfbt3 = Rfbt3n / Yft * Yb1 * Yb5;

            // значение относительной площади сжатой зоны сталефибробетона
            double alfa_r = (N + Rfbt3 * Ar) / ((Rfb + 3.35d * Rfbt3) * Ar);

            if (alfa_r < 0.15)
            {
                alfa_r = (N + 0.73* Rfbt3 * Ar) / ((Rfb + 2 * Rfbt3) * Ar);
            }

            N_ult = Ar * (Rfb * Math.Sin(Math.PI * alfa_r) / Math.PI + Rfbt3 * (1-1.35 * alfa_r) * 1.6 * alfa_r) * beam.r_m/ e_N  ;

            N_ult *= 0.001d;
        }
    }

    public class BSFiberCalc_MNQ_IT : BSFiberCalc_MNQ
    {
        BSBeam_IT beam = new BSBeam_IT();

        public override void Calculate()
        {
            throw new Exception("Расчет не выполнен (нет в СП)");            
        }


        public override void GetSize(double[] _t)
        {
            (beam.bf, beam.hf, beam.hw, beam.bw, beam.b1f, beam.h1f) = (_t[0], _t[1], _t[2], _t[3], _t[4], _t[5]);
        }
    }


    /// <summary>
    ///  Расчет балки на действие сил
    /// </summary>
    public class BSFiberCalc_MNQ : IBSFiberCalculation
    {
        [DisplayName("Высота сечения, см")]
        public double h { get; protected set; }

        [DisplayName("Ширина сечения, см")]
        public double b { get; protected set; }

        [DisplayName("Расчетная длинна элемента сп63 см")]
        public double l0 { get; protected set; }

        [DisplayName("Площадь сечения, см")]
        public double A { get; protected set; }

        [DisplayName("Момент инерции прямоугольного сечения")]
        public double I { get; protected set; }

        [DisplayName("Продольное усилие, кг")]
        public double N { get; protected set; }
        
        [DisplayName("случайный эксцентриситет, принимаемый по СП 63")]
        public double e0 { get; private set; }
        
        [DisplayName("Момент от действия полной нагрузки")]
        public double M1 { get; protected set; }

        [DisplayName("Момент от действия постянных и длительных нагрузок нагрузок")]
        public double Ml1 { get; protected set; }

        [DisplayName("Коэффициент, учитывающий влияние длительности действия нагрузки, определяют по формуле (6.27)")]
        public double fi1 { get; protected set; }

        [DisplayName("относительное значение эксцентриситета продольной силы")]
        public double delta_e { get; protected set; }

        [DisplayName("коэффициент ф.")]
        public double k_b { get; protected set; }

        [DisplayName("Эксцентриситет приложения  силы N")]
        public double e_N { get; protected set; }

        private Fiber m_Fiber;

        protected double Rfbt3n;
        protected double B;
        protected double Yft, Yb, Yb1, Yb2, Yb3, Yb5;
        protected double Rfbn;
        protected double fi = 0.9;
        protected double Ef, //для фибры из тонкой низкоуглеродистой проволоки МП п.п  кг/см2 
                  Eb; //Начальный модуль упругости бетона-матрицы B30 СП63
        //коэффициент фибрового армирования по объему
        protected double mu_fv;
        //Модуль упругости сталефибробетона п.п. (5.2.7)
        protected double Efb;
        // жесткость элемента в предельной по прочности стадии,определяемая по формуле (6.25)
        protected double D;
        //условная критическая сила, определяемая по формуле (6.24)
        protected double Ncr;
        //коэффициент, учитывающий влияние продольного изгиба (прогиба) элемента на его несущую способность и определяемый по формуле(6.23)
        protected double eta;
        //площадь сжатой зоны бетона ф. (6.22)
        protected double Ab;

        protected double Rfb;
        protected double N_ult;

        protected Dictionary<char, double> m_Efforts = new Dictionary<char, double>();

        public static BSFiberCalc_MNQ Construct(BeamSection _BeamSection)
        {
            BSFiberCalc_MNQ fiberCalc;

            if (BeamSection.Rect == _BeamSection)
            {
                fiberCalc = new BSFiberCalc_MNQ_Rect();
            }
            else if (BeamSection.Ring == _BeamSection)
            {
                fiberCalc = new BSFiberCalc_MNQ_Ring();
            }
            else if (BeamSection.IBeam == _BeamSection)
            {
                fiberCalc = new BSFiberCalc_MNQ_IT();
            }
            else
            {
                throw new Exception("Сечение балки не определено");
            }

            return fiberCalc;
        }


        public void GetFiberParamsFromJson(Fiber _fiber)
        {
            m_Fiber = _fiber;

            e0 = _fiber.e0;
            Ef = _fiber.Ef;
            Eb = _fiber.Eb;
            mu_fv = _fiber.mu_fv;
        }

        public virtual void Calculate() {}

        public Dictionary<string, double> GeomParams()
        {
            throw new NotImplementedException();
        }

        public virtual void GetParams(double[] _t)
        {
            (Rfbt3n, Rfbn, Yb, Yft, Yb1, Yb2, Yb3, Yb5, B) = (_t[0], _t[1], _t[2], _t[3], _t[4], _t[5], _t[6], _t[7], _t[8]);                      
        }

        public virtual void GetSize(double[] _t) {}

        public void GetEfforts(Dictionary<char, double> _efforts)
        {
            m_Efforts = _efforts;
            N = m_Efforts['N'];
        }

        public Dictionary<string, double> Results()
        {
            return new Dictionary<string, double>() { { "Rfb", Rfb }, { "N_ult", N_ult } };
        }
    }
}
