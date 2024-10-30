using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BSFiberConcrete
{
                    public class BSMatFiber : IMaterial, INonlinear
    {
        public string Name => "Фибробетон";
        public double E_young => Efb;

                public double Eb { get => Efb; }

                public double Efbt { get; set; }

                                public double Efb { get; set; }

                                public double Nu_fb { get; set; }

                                public double Eps_fb_ult { get; set; }

                                public double Eps_fbt_ult { get; set; }

        public  double Yft, Yb, Yb1, Yb2, Yb3, Yb5;

        public double Eps_fb0 { get; set; }
        public double Eps_fb2 { get; set; }
        public double Eps_fbt2 { get; set; }
        public double Eps_fbt3 { get; set; }

                private double m_Rfbt;
        private double m_Rfbt2;
        private double m_Rfbt3;

                                public BSMatFiber(double _Efb, decimal _Yft, decimal _Yb, decimal _Yb1, decimal _Yb2, decimal _Yb3, decimal _Yb5)
        {
            Efb =  _Efb;
            Yft = (double) _Yft; 
            Yb = (double) _Yb; 
            Yb1 = (double) _Yb1; 
            Yb2 = (double) _Yb2; 
            Yb3 = (double) _Yb3; 
            Yb5 = (double) _Yb5;
        }

                public string BTCls { get; set; }

        [DisplayName("Числовая характеристика класса фибробетона по прочности на осевое сжатие")]
        public double B { get; set; }

        [DisplayName("Нормативное сопротивление сталефибробетона осевому сжатию Rfbn")]
        public double Rfbn { get; set; }

        [DisplayName("Расчетное сопротивление сталефибробетона осевому сжатию Rfbn")]
        public double Rfb { get { return R_fb_calc(); } set { Rfb = value; } }

        [DisplayName("Расчетное сопротивление сталефибробетона осевому сжатию 2-й группы Rfb, ser")]
        public double Rfb_ser => Rfbn;

        [DisplayName("Нормативное сопротивление сталефибробетона осевому растяжению Rfbt")]
        public double Rfbtn { get; set; }

        [DisplayName("Расчетное сопротивление сталефибробетона осевому растяжению Rfbt")]
        public double Rfbt { 
            get { return (m_Rfbt > 0) ? m_Rfbt : R_fbt_calc(); } 
            set { m_Rfbt = value; } 
        }

        [DisplayName("Расчетное сопротивление сталефибробетона осевому растяжению 2-й группы Rfbt, ser")]
        public double Rfbt_ser => Rfbtn;

        [DisplayName("Остаточное нормативное сопротивление на растяжение Rfbt2,n")]
        public double Rfbt2n { get; set; }

        [DisplayName("Остаточное расчетное сопротивление на растяжение Rfbt2")]
        public double Rfbt2 { 
            get { return (m_Rfbt2 > 0) ? m_Rfbt2 : R_fbt2_calc(); }
            set { m_Rfbt2 = value; }
        }

        [DisplayName("Остаточное нормативное сопротивление осевому растяжению Rfbt3,n")]
        public double Rfbt3n { get; set; }

        [DisplayName("Остаточное расчетное сопротивление осевому растяжению Rfbt3")]
        public double Rfbt3 { 
            get { return (m_Rfbt3 > 0) ? m_Rfbt3 : R_fbt3_calc(); } 
            set { m_Rfbt3 = value; } 
        }

        public double R_fb { get => Rfbn; }
        public double e_b1_red { get; set; }
        public double e_b1 { get; set; }

                public double R_fb_calc() => (Yb != 0) ? Rfbn / Yb * Yb1 * Yb2 * Yb3 * Yb5 : 0;

                public double R_fbt_calc() => (Yft != 0) ? Rfbtn / Yft * Yb1 * Yb5 : 0;

                public double R_fbt2_calc() => (Yft != 0) ? Rfbt2n / Yft * Yb1 * Yb5 : 0;

                public double R_fbt3_calc() => (Yft != 0) ? Rfbt3n / Yft * Yb1 * Yb5 : 0;

                        public double e_b2 { get; set; }

        public double Eb_red { get => (e_b1 != 0) ? R_fb / e_b1 : 0; }

                public double alfa(double _Es) => _Es / Efb;


                        public double Omega => (B<=60) ? 0.8 : 0.9;

                public const double Ebt0 = 0.0001;

                                                public double Eps_StateDiagram3L(double _eps, out int _res, int _group = 1 )
        {
            _res = 0;
            if (Efb == 0 || Rfbt == 0 || Rfbt2 == 0 || Rfbt3 == 0)
                return 0;

            double sigma;

            Func<double> TensileStrength = delegate()
            {
                double e_fbt = _eps;

                double e_fbt0 = Rfbt / Efb;
                double e_fbt1 = e_fbt0 + 0.0001;
                double e_fbt2 = 0.004;
                double e_fbt3 = (Rfbt2 != 0) ? 0.02 - 0.0125 * (Rfbt3 / Rfbt2 - 0.5) : 0;
                double sigma_fbt = 0;

                if (0 <= e_fbt && e_fbt <= e_fbt0)
                {
                    sigma_fbt = Efb * e_fbt;
                }
                else if (e_fbt0 < e_fbt && e_fbt <= e_fbt1)
                {
                    sigma_fbt = Rfbt;
                }
                else if (e_fbt1 < e_fbt && e_fbt <= e_fbt2)
                {                    
                    sigma_fbt = Rfbt * (1 - (1 - Rfbt2 / Rfbt) * (e_fbt - e_fbt1) / (e_fbt2 - e_fbt1));
                }
                else if (e_fbt1 < e_fbt && e_fbt <= e_fbt3)
                {                   
                    sigma_fbt = Rfbt * (1 - (1 - Rfbt3 / Rfbt2) * (e_fbt - e_fbt2) / (e_fbt3 - e_fbt2));
                }
                else if (e_fbt > e_fbt3)
                {
                    Debug.Assert(true, "Превышено остаточное сопротиваление");

                    sigma_fbt = 0;
                }

                return sigma_fbt;

            };
            
                        if (_eps > 0)             {
                sigma = TensileStrength();
            }
            else             {                
                sigma = 0;
            }

            return sigma;
        }

                                                                public double Eps_StDiagram2L(double _e, out int _res, int _group = 1)
        {
            double sgm = 0;
            _res = 0;

            _e = -1 * _e;  

            if (0 <= _e && _e < e_b1_red)
            {
                sgm = Eb_red * _e;
            }
            else if (e_b1_red <= _e && _e < e_b2)
            {
                sgm = R_fb;
            }
            else if (_e < 0)             {
                if (_group == 1)
                {
                    sgm = 0;
                }
                else if (_group == 2)
                {
                    sgm = Rfbt_ser;

                                        if (Math.Abs(_e) > Ebt0)
                    {
                        _res = 2;
                    }
                }

            }
            else if (_e >= e_b2)             {
                Debug.Assert(true, "Превышен предел прочности (временное сопротивление) ");

                sgm = 0; 
            }

            return sgm;
        }

                                                        public static double NumEps_fbt3(double _Rfbt2, double _Rfbt3)
        {
            if (_Rfbt2 == 0 || _Rfbt3 == 0)
                return 0;
                        double res =  0.02 - 0.0125 * (_Rfbt3 / _Rfbt2 - 0.5);
            return res;
        }

                                                        public static decimal NumEps_fb1(decimal _Rfb, decimal _Efb)
        {
            if (_Rfb == 0 || _Efb == 0)
                return 0;

            decimal R1 = _Rfb * 0.6m;
            return R1 / _Efb;
        }


                                                        public static decimal NumEps_fbt0(decimal _Rfbt, decimal _Efb)
        {
            if (_Rfbt == 0 || _Efb == 0)
                return 0;

            return _Rfbt / _Efb;
        }

        public BSMatFiber()
        {
        }

        public BSMatFiber(double _Eb)
        {
            Efb = _Eb;
        }       
    }
}
