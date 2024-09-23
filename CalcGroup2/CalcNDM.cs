using BSFiberConcrete.CalcGroup2;
using BSFiberConcrete.Lib;
using MathNet.Numerics;
using System;
using System.Collections.Generic;
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

        public NDMSetup setup = BSData.LoadNDMSetup();
        
        public BSCalcResultNDM CalcRes { get; private set; }

        /// <summary>
        /// Статусы расчета, отражаемые в отчете
        /// </summary>
        private List<string> m_Message;

        public CalcNDM(BeamSection _BeamSection)
        {
            m_BeamSection = _BeamSection;
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

        public void Run()
        {
            //привязка арматуры (по X - высота, по Y ширина балки)
            double leftX = 0;
            // для прямоугольных и тавровых сечений привязка к центу нижней грани 
            if (BSHelper.IsRectangled(m_BeamSection)) leftX = -D["b"] / 2.0;
            (List<double> lD, List<double> lX, List<double> lY, double _qty, double _area) = BSCalcNDM.ReinforcementBinding(m_BeamSection, leftX, 0, setup.UseRebar);
            if (!D.ContainsKey("rods_qty"))
                D.Add("rods_qty", _qty);
            if (!D.ContainsKey("rods_area"))
                D.Add("rods_area", _area);            
            ///
            /// выполнить расчет по 1 группе предельных состояний
            ///
            BSCalcNDM bsCalcGR1 = new BSCalcNDM(GR1, m_BeamSection, setup);
            bsCalcGR1.SetDictParams(D);
            bsCalcGR1.SetRods(lD, lX, lY);
            bsCalcGR1.Run();

            BSCalcResultNDM calcRes = new BSCalcResultNDM(bsCalcGR1.Results);
            calcRes.InitFromCalcNDM(bsCalcGR1);
            calcRes.InitCalcParams(D);            
            calcRes.ResultsMsg1Group(ref m_Message);
            ///
            /// выполнить расчет по 2 группе предельных состояний
            /// 
            List<double> Xs = new List<double>();
            List<double> Ys = new List<double>();

            // Расчет по 2 группе предельных состояний - момент трещинообразования          
            BSCalcNDM bsсalcgr2(double _coefM)
            {
                BSCalcNDM bscalc = new BSCalcNDM(GR2, m_BeamSection, setup);
                bscalc.SetDictParams(D);
                bscalc.MzMyNUp(_coefM);
                bscalc.SetRods(lD, lX, lY);                
                bscalc.Run();

                calcRes.ErrorIdx.Add(bscalc.Err);
                calcRes.SetRes2Group(bscalc.Results);

                // Определение момента образования трещины
                Xs.Add(bscalc.UtilRate_fb_t); // увеличение усилия
                Ys.Add(_coefM);  // коэф использования по материалу

                return bscalc;
            }

            // Расчет по 2 группе предельных состояний - ширина раскрытия трещины           
            BSCalcNDM bsсalcgr2_crc(double _coefM, List<double> _E_s_crc = null)
            {
                BSCalcNDM bscalc = new BSCalcNDM(GR2, m_BeamSection, setup);
                bscalc.SetDictParams(D);
                bscalc.MzMyNUp(_coefM);
                bscalc.SetRods(lD, lX, lY);               
                bscalc.SetE_S_Crc(_E_s_crc);
                bscalc.Run();

                calcRes.ErrorIdx.Add(bscalc.Err);
                calcRes.SetRes2Group(bscalc.Results, false, true);                
                return bscalc;
            }

            // 1 этап
            // определяем моменты трещинообразования от кратковременных и длительных нагрузок (раздел X)
            double Mx_crc; double My_crc; double N_crc;
            
            // используем заданные усилия и определяем коэфф использования по 2-гр пр сост
            double coef = 1;
            BSCalcNDM bsCalc1 = bsсalcgr2(coef);            
            
            if (setup.UseRebar)
            {
                coef = 1.2;
                BSCalcNDM bsCalc2 = bsсalcgr2(coef);

                // Если же хотя бы один из моментов трещинообразования оказывается меньше
                // соответствующего действующего момента, выполняют второй этап расчета.
                coef = 1.4;
                BSCalcNDM bsCalc4 = bsсalcgr2(coef);                                

                coef = 1.6;
                BSCalcNDM bsCalc6 = bsсalcgr2(coef);

                coef = 1.8;
                BSCalcNDM bsCalc8 = bsсalcgr2(coef);

                double xcoef = Y_interpolate(Ys.ToArray(), Xs.ToArray(), 1.0);

                BSCalcNDM bsCalc_Mcrc = bsсalcgr2(Math.Round(xcoef-0.1, 1, MidpointRounding.AwayFromZero));

                // момент трещинообразования
                Mx_crc = bsCalc_Mcrc.Mz_crc;
                My_crc = bsCalc_Mcrc.My_crc;
                N_crc = bsCalc_Mcrc.N_crc;
                // параметр трещинообразования, для расчета ширины раскрытия трещины
                List<double> E_S_crc = bsCalc_Mcrc.EpsilonSResult;
                
                // определение ширины раскрытия трещины
                // расчитываем на заданные моменты и силы
                BSCalcNDM bsCalc_crc = bsсalcgr2_crc(1, E_S_crc);                                                                 
            }
            
            CalcRes = calcRes;
        }

    }
}
