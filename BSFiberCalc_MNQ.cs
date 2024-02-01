using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace BSFiberConcrete
{    
    public class BSFiberCalc_MNQ_Ring : BSFiberCalc_MNQ
    {
        public BSBeam_Ring beam { get; set; }

        public BSFiberCalc_MNQ_Ring()
        {
            this.beam = new BSBeam_Ring();
        }

        public override void GetSize(double[] _t)
        {
            (r1, r2) =  (beam.r1, beam.r2) = (_t[0], _t[1]);
            A = beam.Area();
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
        public BSBeam_IT beam { get; set; }

        public BSFiberCalc_MNQ_IT()
        {
            this.beam = new BSBeam_IT();
        }

        public override void Calculate()
        {
            throw new Exception("Расчет не выполнен (нет в СП)");            
        }

        public override void GetSize(double[] _t)
        {
            (beam.bf, beam.hf, beam.hw, beam.bw, beam.b1f, beam.h1f) = (_t[0], _t[1], _t[2], _t[3], _t[4], _t[5]);
        }
    }


    [DisplayName("Расчет элементов на действие сил и моментов")]
    public class BSFiberCalc_MNQ : IBSFiberCalculation
    {
        [DisplayName("Высота сечения, см"), Description("Geom")]
        public double h { get; protected set; }

        [DisplayName("Ширина сечения, см"), Description("Geom")]
        public double b { get; protected set; }

        [DisplayName("Радиус внешний, см"), Description("Geom")]
        public double r2 { get; protected set; }

        [DisplayName("Радиус внутренний, см"), Description("Geom")]
        public double r1 { get; protected set; }

        [DisplayName("Расчетная длинна элемента сп63 см"), Description("Geom")]
        public double l0 { get; protected set; }

        [DisplayName("Площадь сечения, см2"), Description("Geom")]
        public double A { get; protected set; }

        [DisplayName("Момент инерции сечения"), Description("Geom")]
        public double I { get; protected set; }

        [DisplayName("Продольное усилие, кг"), Description("Phys")]
        public double N { get; protected set; }

        [DisplayName("Cлучайный эксцентриситет, принимаемый по СП 63"), Description("Phys")]
        public double e0 { get; private set; }

        [DisplayName("Эксцентриситет приложения силы N"), Description("Phys")]
        public double e_N { get; protected set; }

        [DisplayName("Относительное значение эксцентриситета продольной силы"), Description("Phys")]
        public double delta_e { get; protected set; }

        [DisplayName("Момент от действия полной нагрузки"), Description("Phys")]
        public double M1 { get; protected set; }

        [DisplayName("Момент от действия постянных и длительных нагрузок нагрузок"), Description("Phys")]
        public double Ml1 { get; protected set; }

        [DisplayName("Коэффициент ф."), Description("Coef")]
        public double k_b { get; protected set; }

        [DisplayName("Коэффициент, учитывающий влияние длительности действия нагрузки  (6.27)"), Description("Coef")]
        public double fi1 { get; protected set; }

        [DisplayName("Коэффициент надежности Yft"), Description("Coef")]
        public double Yft { get; protected set; }
        [DisplayName("Коэффициент условия работы Yb"), Description("Coef")]
        public double Yb { get; protected set; }
        [DisplayName("Коэффициент условия работы Yb1"), Description("Coef")]
        public double Yb1 { get; protected set; }
        [DisplayName("Коэффициент условия работы Yb2"), Description("Coef")]
        public double Yb2 { get; protected set; }
        [DisplayName("Коэффициент условия работы Yb3"), Description("Coef")]
        public double Yb3 { get; protected set; }
        [DisplayName("Коэффициент условия работы Yb5"), Description("Coef")]
        public double Yb5 { get; protected set; }

        [DisplayName("Предельный момент"), Description("Res")]
        public double M_ult { get; protected set; }

        [DisplayName("Предельная поперечная сила"), Description("Res")]
        public double Q_ult { get; protected set; }

        [DisplayName("Предельная продольная сила"), Description("Res")]
        public double N_ult { get; protected set; }


        public Rebar Rebar {get; set;}
        public bool UseRebar { get; set; }
        public bool Fissure{ get; set; }
        public bool Shear { get; set; }

        protected Fiber m_Fiber;

        protected double Rfbt3n;
        protected double B;
       
        protected double Rfbn;
        protected double fi = 0.9;
        protected double Ef; //для фибры из тонкой низкоуглеродистой проволоки МП п.п  кг/см2 
        protected double Eb; //Начальный модуль упругости бетона-матрицы B30 СП63
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
        //поперечная сила
        protected double Q;

        protected double Rfb;
        protected double Rfbtn;
      
        // параметры продольной арматуры
        protected double[] l_rebar;
        // параметры поперечной арматуры
        protected double[] t_rebar;

        protected Dictionary<string, double> m_Efforts = new Dictionary<string, double>();

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
        
        // параметры арматуры
        public void GetRebarParams(double[] _l_rebar, double[] _t_rebar)
        {
            l_rebar = _l_rebar;
            t_rebar = _t_rebar;
        }

        public virtual void Calculate() {}

        public Dictionary<string, double> GeomParams()
        {
            throw new NotImplementedException();
        }

        public virtual void GetParams(double[] _t)
        {
            (Rfbt3n, Rfbn, Yb, Yft, Yb1, Yb2, Yb3, Yb5, B) = (_t[0], _t[1], _t[2], _t[3], _t[4], _t[5], _t[6], _t[7], _t[8]);
            Rfbtn = Rfbt3n;
        }

        public virtual void GetSize(double[] _t) {}

        public void GetEfforts(Dictionary<string, double> _efforts)
        {
            m_Efforts = _efforts;

            //Продольное усилие кг
            N = m_Efforts["N"];
            // Поперечная сила
            Q = m_Efforts["Q"];
            //Момент от действия полной нагрузки
            M1 = m_Efforts["M"];
            //Момент от действия постянных и длительных нагрузок нагрузок
            Ml1 = m_Efforts["Ml"];
            // Эксцентриситет приложения N
            e_N = m_Efforts["eN"];
        }

        public virtual Dictionary<string, double> Results()
        {
            return new Dictionary<string, double>() { { "Rfb", Rfb }, { "N_ult", N_ult } };
        }
    }
}
