using BSFiberConcrete.CalcGroup2;
using BSFiberConcrete.DeformationDiagram;
using MathNet.Numerics.Integration;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BSFiberConcrete
{
    /// <summary>
    /// Обработка результатов расчета по НДМ
    /// </summary>
    public class BSCalcResultNDM
    {
        [DisplayName("Количество итераций, [.]")]
        public double ItersCnt { get; private set; }

        #region 1 группа предельных состояний
        [DisplayName("Радиус кривизны продольной оси в плоскости действия моментов, Rx, [см]")]
        public double rx { get; private set; }

        [DisplayName("Радиус кривизны продольной оси в плоскости действия моментов, Ry, [см]")]
        public double ry { get; private set; }

        [DisplayName("Относительная деформация волокна в Ц.Т. сечения, e0, [.]")]
        public double eps_0 { get; private set; }

        [DisplayName("Кривизна 1/Rx, [1/см]")]
        public double Kx { get; private set; }

        [DisplayName("Кривизна 1/Ry, [1/см]")]
        public double Ky { get; private set; }

        // Растяжение >>

        [DisplayName("Напряжение в бетоне, [кг/см2]")]
        public double sigmaB { get; private set; }

        [DisplayName("Напряжение в арматуре, [кг/см2]")]
        public double sigmaS { get; private set; }

        [DisplayName("Максимальная относительная деформация в фибробетоне, [.]")]
        public double e_fb_max { get; private set; }

        [DisplayName("Коэффициент использования по деформации в фибробетоне (растяжение), [.]")]
        public double UtilRate_e_fbt => (Eps_fb_ult !=0) ? e_fb_max_p / Eps_fb_ult : 1;

        [DisplayName("Максимальная относительная деформация в арматуре, [.]")]
        public double e_s_max { get; private set; }

        [DisplayName("Коэффициент использования по деформации в арматуре (растяжение), [.]")]
        public double UtilRate_e_st => (Eps_s_ult != 0) ? e_s_max / Eps_s_ult : 1;

        // Cжатие >>
        [DisplayName("Напряжение в бетоне (сжатие), [кг/см2]")]
        public double sigmaB_p { get; private set; }

        [DisplayName("Напряжение в арматуре (сжатие), [кг/см2]")]
        public double sigmaS_p { get; private set; }

        [DisplayName("Максимальная относительная деформация в фибробетоне (сжатие) , [.]")]
        public double e_fb_max_p { get; private set; }

        [DisplayName("Коэффициент использования по деформации в фибробетоне (сжатие), [.]")]
        public double UtilRate_e_fb_p { get; private set; }

        [DisplayName("Максимальная относительная деформация в арматуре (сжатие), [.]")]
        public double e_s_max_p { get; private set; }

        [DisplayName("Коэффициент использования по деформации в арматуре (сжатие), [.]")]
        public double UtilRate_e_s_p => (Eps_s_ult != 0) ? e_s_max_p / Eps_s_ult : 1;

        // проверка по усилиям

        [DisplayName("Момент Мx, [кг*см]")]
        public double Mx_calc { get; private set; }
        [DisplayName("Момент Мy, [кг*см]")]
        public double My_calc { get; private set; }
        [DisplayName("Сила N, [кг]")]
        public double N_calc { get; private set; }

        // напряжение
        public List<double> Sig_B { get; set; } // бетон
        public List<double> Sig_S { get; set; } // арматура

        // деформации
        public List<double> Eps_B { get; set; } // бетон
        public List<double> Eps_S { get; set; } // арматура

        #endregion

        #region 2 группа предельных состояний
        [DisplayName("П6.2.13. Момент возникновения трещины, [кг*см]")]
        public double M_crc { get; set; }

        [DisplayName("П6.2.31. Кривизна 1/Rx, [1/см]")]
        public double Kx_crc { get; set; }

        [DisplayName("П6.2.31. Кривизна 1/Ry, [1/см]")]
        public double Ky_crc { get; set; }

        [DisplayName("П6.2.31. Напряжение в арматуре, [кг/см2]")]
        public double sig_s_crc { get; set; }

        [DisplayName("П6.2.31. Ширина раскрытия трещины, [см]")]
        public double a_crc { get; set; }
        #endregion

        public List<string> Msg { get; set; }
        
        private Dictionary<string, double> Res1Group { get; set; }
        private Dictionary<string, double> Res2Group { get; set; }

        public List<int> ErrorIdx { get; set; }

        #region Параметры расчета

        [DisplayName("Ширина сечения, b [см]")]
        public double b { get; set; }
        [DisplayName("Высота сечения, h [см]")]
        public double h { get; set; }

        [DisplayName("Mx [кг*см]")]
        public double Mx { get; set; }
        [DisplayName("My [кг*см]")]
        public double My { get; set; }
        [DisplayName("N [кг]")]
        public double N { get; set; }

        [DisplayName("Модуль упругости фибробетона Eb, [кг/см2]")]
        public double Eb { get; set; }

        [DisplayName("Нормативное сопротивление фибробетона на сжатие R,fbn, [кг/см2]")]
        public double Rfbn { get; set; }

        [DisplayName("Нормативное сопротивление фибробетона на растяжение R,fbtn, [кг/см2]")]
        public double Rfbtn { get; set; }

        [DisplayName("Расчетное сопротивление фибробетона на сжатие R,fbn, [кг/см2]")]
        public double Rfb { get; set; }

        [DisplayName("Расчетное сопротивление фибробетона на растяжение R,fbtn, [кг/см2]")]
        public double Rfbt { get; set; }

        [DisplayName("Модуль упругости арматуры Es, [кг/см2]")]
        public double Es { get; set; }

        [DisplayName("Нормативное сопротивление арматуры R,sn, [кг/см2]")]
        public double Rs { get; set; }

        [DisplayName("Количество стержней арматуры, [шт]")]
        public double Rods_qty { get; set; }

        [DisplayName("Общая площадь продольной арматуры, [см2]")]
        public double Rods_area { get; set; }

        [DisplayName("Максимально допустимая относительная деформация в бетоне, [.]")]
        public double Eps_fb_ult { get; set; }

        [DisplayName("Максимально допустимая относительная деформация в бетоне, [.]")]
        public double Eps_fbt_ult { get; set; }

        [DisplayName("Максимально допустимая относительная деформация в арматуре, [.]")]
        public double Eps_s_ult { get; set; }

        #endregion

        private string DN(string _attr) => BSFiberCalculation.DsplN(typeof(BSCalcResultNDM), _attr);

        private Dictionary<int, string> DictErrors = new Dictionary<int, string>()
        {
            [-1] = "Достигнута заданная сходимость метода [+]",
            [0] = "",
            [1] = "Превышен максимально допустимый предел деформации [-]",
            [2] = "Достигнуто максимальное число итераций [-]",
            [3] = "Деформации превысили разумный предел [-]"
        };


        /// <summary>
        /// внешние усилия
        /// </summary>
        public Dictionary<string, double> Efforts
        {
            get
            {
                return new Dictionary<string, double>
                {
                    { DN("Mx"), Mx },
                    { DN("My"), My },
                    { DN("N"), N  }
                };
            }
        }
        /// <summary>
        /// физические свойства бетона и арматуры 
        /// </summary>
        public Dictionary<string, double> PhysParams
        {
            get
            {
                return new Dictionary<string, double> 
                {
                    { DN("Eb"),  Eb },
                    // норм
                    { DN("Rfbn"), Rfbn },
                    { DN("Rfbtn"), Rfbtn },
                    // расч
                    { DN("Rfb"), Rfb },
                    { DN("Rfbt"), Rfbt },

                    { DN("Eps_fb_ult"), Eps_fb_ult},

                    { DN("Es"),    Es },
                    { DN("Rs"),    Rs },                    
                    { DN("Eps_s_ult"), Eps_s_ult }
                };
            }
        }
        /// <summary>
        ///  количество арматуры
        /// </summary>
        public Dictionary<string, double> Reinforcement
        {
            get
            {
                return new Dictionary<string, double>
                {
                    { DN("Rods_qty"), Rods_qty },
                    { DN("Rods_area"), Rods_area }
                };
            }
        }         

        public Dictionary<string, double> GeomParams =>  new Dictionary<string, double>
        {
            { DN("b"), b },
            { DN("h"), h }
        };
       
        /// <summary>
        /// Параметры расчета
        /// </summary>
        /// <param name="_D"></param>
        public void InitCalcParams(Dictionary<string, double> _D)
        {
            b = _D["b"];
            h = _D["h"];

            Mx = _D["Mz"];
            My = _D["My"];
            N = _D["N"];

            Eb = _D["Eb0"];
            // норм
            Rfbn = _D["Rbcn"];
            Rfbtn = _D["Rbtn"];
            // расч
            Rfb = _D["Rbc"];
            Rfbt = _D["Rbt"];

            Es = _D["Es0"];
            Rs = _D["Rscn"];

            Eps_fbt_ult = _D["ebt_ult"];
            Eps_fb_ult = _D["eb_ult"];
            //Eps_s_ult = _D["es_ult"];

            Rods_qty = _D["rods_qty"];
            Rods_area = _D["rods_area"];            
        }

        /// <summary>
        /// Результаты расчета по 1 группе предельных состояний
        /// </summary>
        /// <param name="_D1gr"></param>
        public BSCalcResultNDM(Dictionary<string, double> _D1gr)
        {
            // итерации
            ItersCnt = _D1gr["ItersCnt"];

            // деформации
            eps_0 = _D1gr["ep0"];
            Ky = _D1gr["Ky"];
            ry = _D1gr["ry"];
            Kx = _D1gr["Kz"];
            rx = _D1gr["rz"];

            // растяжение
            sigmaB = BSHelper.KNsm2ToKgssm2(_D1gr["sigB"]);
            sigmaS = BSHelper.KNsm2ToKgssm2(_D1gr["sigS"]);
            e_fb_max = _D1gr["epsB"];
            e_s_max = _D1gr["epsS"];

            // сжатие
            sigmaB_p = BSHelper.KNsm2ToKgssm2(_D1gr["sigB_p"]);
            sigmaS_p = BSHelper.KNsm2ToKgssm2(_D1gr["sigS_p"]);
            e_fb_max_p = _D1gr["epsB_p"];
            e_s_max_p = _D1gr["epsS_p"];

            // предел
            Eps_s_ult = _D1gr["esc0"];

            // проверка усилий
            Mx_calc = BSHelper.KNsm2ToKgssm2(_D1gr["Mx"]);
            My_calc = BSHelper.KNsm2ToKgssm2(_D1gr["My"]);            
            N_calc = BSHelper.kN2Kgs(_D1gr["N"]);

            // использование
            UtilRate_e_fb_p = _D1gr["UR_fb_p"];
            //UtilRate_e_s_p = _D1gr["UR_s_p"];
            Msg = new List<string>();
            Res1Group = new Dictionary<string, double>();
            Res2Group = new Dictionary<string, double>();
            ErrorIdx = new List<int>();
        }

        /// <summary>
        /// Результаты расчета по 2 группе предельных состояний
        /// </summary>
        /// <param name="_D2gr">Словарь с результатами</param>
        public void GetRes2Group(Dictionary<string, double> _D2gr)
        {            
            // кривизна
            Ky_crc = _D2gr["Ky"];
            Kx_crc = _D2gr["Kz"];

            if (Rods_qty > 0)
            {
                // момент трещинообразования
                M_crc = _D2gr["My_crc"];
                // напряжение в арматуре (нужно реализовать для каждого стержня)
                sig_s_crc = _D2gr["sig_s_crc"];
                // ширина раскрытия трещины
                a_crc = _D2gr["a_crc"];
            }
        }

        private void AddToResult(string _attr, double _value, int _group = 1)
        {
            Dictionary<string, double> res = (_group == 1) ? Res1Group : Res2Group;

            if (Math.Abs(_value) < 10e-15 || Math.Abs(_value) > 10e15)
            {
                return;
            }

            try
            {
                var KEY = BSFiberCalculation.DsplN(typeof(BSCalcResultNDM), _attr);
                if (!res.ContainsKey(KEY))
                {
                    res.Add(KEY, _value);
                }
            }
            catch (Exception _E)
            {
                MessageBox.Show(_E.Message);
            }
        }


        /// <summary>
        ///  Результаты расчета по 1 группе предельных состояний
        /// </summary>
        public void Results1Group(ref Dictionary<string, double> _CalcResults)
        {
            // 
            AddToResult("ItersCnt", ItersCnt);

            Res1Group.Add("--------Изгиб:--------", 1);
            AddToResult("eps_0", eps_0);
            AddToResult("rx", rx);
            AddToResult("Kx", Kx);
            AddToResult("ry", ry);
            AddToResult("Ky", Ky);

            // растяжение
            Res1Group.Add("--------Растяжение:--------", 1);
            AddToResult("sigmaB", sigmaB);            
            AddToResult("e_fb_max", e_fb_max);
            AddToResult("UtilRate_e_fbt", UtilRate_e_fbt);
            
            AddToResult("sigmaS", sigmaS);
            AddToResult("e_s_max", e_s_max);
            AddToResult("UtilRate_e_st", UtilRate_e_st);

            // сжатие
            Res1Group.Add("--------Сжатие:-------", 1);
            // - бетон
            AddToResult("sigmaB_p", sigmaB_p);
            AddToResult("e_fb_max_p", e_fb_max_p);
            AddToResult("UtilRate_e_fb_p", UtilRate_e_fb_p);
            // - арматура
            AddToResult("sigmaS_p", sigmaS_p);
            AddToResult("e_s_max_p", e_s_max_p);
            AddToResult("UtilRate_e_s_p", UtilRate_e_s_p);

            // усилия
            Res1Group.Add("--------Проверка по усилиям:-------", 1);
            AddToResult("Mx_calc", Mx_calc);
            AddToResult("My_calc", My_calc);
            AddToResult("N_calc", N_calc);

            _CalcResults = Res1Group;
        }

        /// <summary>
        ///  результаты расчета по 1 группе пр сост
        /// </summary>
        /// <param name="_Message"></param>
        public void ResultsMsg1Group(ref List<string> _Message)
        {
            bool res_fb = e_fb_max <= Eps_fb_ult;
            if (res_fb)
                Msg.Add(string.Format("Проверка сечения по фибробетону пройдена. e_fb_max={0} <= e_fb_ult={1} ", Math.Round(e_fb_max, 6), Eps_fb_ult));
            else
                Msg.Add(string.Format("Не пройдена проверка сечения по фибробетону. e_fb_max={0} <= e_fb_ult={1} ", Math.Round(e_fb_max, 6), Eps_fb_ult));

            bool res_s = e_s_max <= Eps_s_ult;
            if (res_s)
                Msg.Add(string.Format("Проверка сечения по арматуре пройдена. e_s_max={0} <= e_s_ult={1} ", Math.Round(e_s_max, 6), Eps_s_ult));
            else
                Msg.Add(string.Format("Не пройдена проверка сечения по арматуре. e_s_max={0} <= e_s_ult={1}", Math.Round(e_s_max, 6), Eps_s_ult));

            Msg.Add("---Предупреждения:------");
            foreach (int errid in ErrorIdx) 
            {
                if (DictErrors.TryGetValue(errid, out string _errval))
                    Msg.Add($"{_errval}");
            }

            _Message = Msg;
        }

        /// <summary>
        ///  Результаты расчета по 2 группе предельных состояний
        /// </summary>
        public void Results2Group(ref Dictionary<string, double> _CalcResults)
        {            
            AddToResult("M_crc", BSHelper.kNsm2kgssm( M_crc), 2);                                  

            AddToResult("Kx_crc", Kx_crc, 2);            
            AddToResult("Ky_crc", Ky_crc, 2);

            AddToResult("sig_s_crc", sig_s_crc, 2);

            AddToResult("a_crc", a_crc, 2);

            _CalcResults = Res2Group;
        }

        public void InitFromCalcNDM(BSCalcNDM bsCalc1)
        {
            Sig_B = bsCalc1.SigmaBResult;
            Sig_S = bsCalc1.SigmaSResult;
            Eps_B = bsCalc1.EpsilonBResult;
            Eps_S = bsCalc1.EpsilonSResult;

            ErrorIdx.Add(bsCalc1.Err);
        }
    }
}
