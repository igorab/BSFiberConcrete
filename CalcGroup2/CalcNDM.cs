using BSFiberConcrete.CalcGroup2;
using BSFiberConcrete.Lib;
using MathNet.Numerics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFiberConcrete
{
    public class CalcNDM
    {
        const int GR1 = BSFiberLib.CG1;
        const int GR2 = BSFiberLib.CG2;

        private BeamSection m_BeamSection;

        // данные с формы
        public Dictionary<string, double> D;

        public NDMSetup setup { get; set; }

        public BSCalcResultNDM CalcRes => m_CalcRes;
        private BSCalcResultNDM m_CalcRes;

        //привязка арматуры(по X - высота, по Y ширина балки)
        private double LeftX;

        private List<double> Xs = new List<double>();
        private List<double> Ys = new List<double>();

        private List<double> lD;
        private List<double> lX;
        private List<double> lY;

        /// <summary>
        /// Статусы расчета, отражаемые в отчете
        /// </summary>
        private List<string> m_Message;

        public CalcNDM(BeamSection _BeamSection)
        {
            m_BeamSection = _BeamSection;
            setup = BSData.LoadNDMSetup();
            LeftX = 0;
        }

        /// <summary>
        ///  Интерполяция
        /// </summary>
        /// <param name="_Y">Усилия (моменты)</param>
        /// <param name="_X">Коэфф использования материала </param>
        /// <param name="_x">искомый коэфф использования</param>
        /// <returns>Определяем момент, при котром коэф использования = 1  </returns>
        public double Y_interpolate(double[] _Y, double[] _X, double _x)
        {             
            Lagrange.Lagrange lagrange = new Lagrange.Lagrange();

            double value = lagrange.GetValue(_X, _Y, _x);
         
            return value;
        }

        private void Init()
        {
            // для прямоугольных и тавровых сечений привязка к центу нижней грани 
            if (BSHelper.IsRectangled(m_BeamSection)) 
                LeftX = -D["b"] / 2.0;

            double _qty, _area;
            
            (lD, lX, lY, _qty, _area) = BSCalcNDM.ReinforcementBinding(m_BeamSection, LeftX, 0, setup.UseRebar);

            if (!D.ContainsKey("rods_qty"))
                D.Add("rods_qty", _qty);
            if (!D.ContainsKey("rods_area"))
                D.Add("rods_area", _area);
        }

        ///
        /// выполнить расчет по 1 группе предельных состояний
        ///
        private BSCalcNDM BSCalcGr1(double _coefM = 1.0)
        {           
            BSCalcNDM bsCalcGR1 = new BSCalcNDM(GR1, m_BeamSection, setup);
            bsCalcGR1.SetDictParams(D);
            bsCalcGR1.MzMyNUp(_coefM);
            bsCalcGR1.SetRods(lD, lX, lY);
            bsCalcGR1.Run();

            return bsCalcGR1;
        }

        private BSCalcNDM BSCalcGr2(double _coefM)
        {
            BSCalcNDM bscalc = new BSCalcNDM(GR2, m_BeamSection, setup);
            bscalc.SetDictParams(D);
            bscalc.MzMyNUp(_coefM);
            bscalc.SetRods(lD, lX, lY);
            bscalc.Run();

            return bscalc;
        }

        // Расчет по 2 группе предельных состояний - ширина раскрытия трещины           
        BSCalcNDM BSCalcGr2_Crc(double _coefM, List<double> _E_s_crc = null)
        {
            NdmCrc ndmCrc = BSData.LoadNdmCrc();
            ndmCrc.InitFi2(setup.RebarType);
            ndmCrc.InitFi3(D["N"]);
           
            BSCalcNDM bscalc = new BSCalcNDM(GR2, m_BeamSection, setup);
            bscalc.SetDictParams(D);
            bscalc.MzMyNUp(_coefM);
            bscalc.NdmCrc = ndmCrc;
            bscalc.SetRods(lD, lX, lY);
            bscalc.SetE_S_Crc(_E_s_crc);
            bscalc.Run();

            m_CalcRes.ErrorIdx.Add(bscalc.Err);
            m_CalcRes.SetRes2Group(bscalc.Results, false, true);
            return bscalc;
        }

        /// <summary>
        /// Выполнить расчет по 1 г пред сост
        /// </summary>
        /// <returns></returns>
        public bool RunGroup1()
        {
            BSCalcNDM bsCalcGR1 = BSCalcGr1();

            bool ok = bsCalcGR1.UtilRate_fb_t < 1;
            Debug.Assert(!ok, "Предел прочности сечения превышен");

            m_CalcRes = new BSCalcResultNDM(bsCalcGR1.Results);
            m_CalcRes.InitFromCalcNDM(bsCalcGR1);
            m_CalcRes.InitCalcParams(D);
            m_CalcRes.ResultsMsg1Group(ref m_Message);

            return true;
        }

        /// <summary>
        ///  GO!
        /// </summary>
        public void Run()
        {
            Init();

            bool ok = RunGroup1();

            if (ok)
            {
                BSCalcNDM bsCalc_Mcrc = RunGroup2();

                // параметр трещинообразования, для расчета ширины раскрытия трещины
                List<double> E_S_crc = bsCalc_Mcrc.EpsilonSResult;

                // определение ширины раскрытия трещины
                // расчитываем на заданные моменты и силы
                BSCalcNDM bsCalc_crc = BSCalcGr2_Crc(1.0, E_S_crc);

            }            
        }

        // Расчет по 2 группе предельных состояний - момент трещинообразования          
        private BSCalcNDM bsсalcgr2(double _coefM)
        {
            BSCalcNDM bscalc = BSCalcGr2(_coefM);
            m_CalcRes.ErrorIdx.Add(bscalc.Err);
            m_CalcRes.SetRes2Group(bscalc.Results);

            // Определение момента образования трещины
            if (bscalc.UtilRate_fb_t <= 1.0)
            {
                Xs.Add(bscalc.UtilRate_fb_t); // увеличение усилия
                Ys.Add(_coefM);  // коэф использования по материалу
            }
            return bscalc;
        }

        ///
        /// выполнить расчет по 2 группе предельных состояний
        /// 
        public BSCalcNDM RunGroup2()
        {
            double My0, Mx0, N0;

            // 1 этап
            // определяем моменты трещинообразования от кратковременных и длительных нагрузок (раздел X)                        
            // используем заданные усилия и определяем коэфф использования по 2-гр пр сост            
            if (setup.UseRebar)
            {
                double coef = 1;
                BSCalcNDM bsCalc1 = BSCalcGr2(coef);
                double ur = bsCalc1.UtilRate_fb_t;

                if (ur > 1)
                {
                    BSCalcNDM bscalc = bsсalcgr2(1/ur);
                    ur = bscalc.UtilRate_fb_t;
                }

                // Если же хотя бы один из моментов трещинообразования оказывается меньше
                // соответствующего действующего момента, выполняют второй этап расчета.

                double dH = 1;
                // применяем переменный шаг
                int iters = 0;
                while (ur < 0.8)
                {
                    BSCalcNDM bscalc = bsсalcgr2(coef);

                    iters++;
                    if (iters > 100) break;
                    if (bscalc.UtilRate_fb_t > 0.8) break;
                    coef += dH;
                    ur = bscalc.UtilRate_fb_t;
                }
                coef -= dH;

                dH = 0.2;
                for (int N = 1; N <= 100; N++)
                {
                    coef += dH;
                    BSCalcNDM _bsCalc = bsсalcgr2(coef);
                    ur = _bsCalc.UtilRate_fb_t;
                    if (_bsCalc.UtilRate_fb_t > 1)
                        break;
                }

                double y_coef = coef; // Y_interpolate(Ys.ToArray(), Xs.ToArray(), 1.0);                
                BSCalcNDM bsCalc_Mcrc = bsсalcgr2(y_coef);
                ur = bsCalc_Mcrc.UtilRate_fb_t;
                if (ur > 1.2) //коэффициент использования
                {
                    bsCalc_Mcrc = bsсalcgr2(y_coef - dH / 2.0);
                    ur = bsCalc_Mcrc.UtilRate_fb_t;
                    double My_crc = bsCalc_Mcrc.My_crc;  //  момент трещинообразования                                                         
                }

                return bsCalc_Mcrc;                
            }
            else
            {
                return bsсalcgr2(1.0);
            }
        }
    }
}
