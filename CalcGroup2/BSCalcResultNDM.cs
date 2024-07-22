using BSFiberConcrete.DeformationDiagram;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
        [DisplayName("Радиус кривизны продольной оси в плоскости действия моментов, Rx, см")]
        public double rx { get; set; }

        [DisplayName("Радиус кривизны продольной оси в плоскости действия моментов, Ry, см")]
        public double ry { get; set; }

        [DisplayName("Относительная деформация волокна в Ц.Т. сечения, e0")]
        public double eps_0 { get; set; }

        [DisplayName("Кривизна 1/Rx, 1/см")]
        public double Kx { get; set; }

        [DisplayName("Кривизна 1/Ry, 1/см")]
        public double Ky { get; set; }

        [DisplayName("Напряжение в бетоне, кгс/см2")]
        public double sigmaB { get; set; }

        [DisplayName("Напряжение в арматуре, кгс/см2")]
        public double sigmaS { get; set; }

        [DisplayName("Максимальная относительная деформация в бетоне")]
        public double e_fb_max { get; set; }

        [DisplayName("Максимальная относительная деформация в арматуре")]
        public double e_s_max { get; set; }

        [DisplayName("Максимальный момент возникновения трещины")]
        public double Mcrc { get; set; }
        public int groupLSD;
        public List<string> Msg { get; set; }
        public double Eps_fb_ult;
        public Dictionary<string, double> Res1Group { get; private set; }
        public Dictionary<string, double> Res2Group { get; private set; }

        public BSCalcResultNDM(Dictionary<string, double> _D)
        {
            eps_0 = _D["ep0"];
            Ky = _D["Ky"];
            ry = _D["ry"];
            Kx = _D["Kz"];
            rx = _D["rz"];
            sigmaB = _D["sigB"];
            sigmaS = _D["sigS"];
            e_fb_max = _D["epsB"];
            e_s_max = _D["epsS"];

            Msg = new List<string>();
            Res1Group = new Dictionary<string, double>();
            Res2Group = new Dictionary<string, double>();
        }

        private void AddToResult(string _attr, double _value, int _group = 1)
        {
            var res = (_group == 1) ? Res1Group : Res2Group;

            if (Math.Abs(_value) < 10e-15 || Math.Abs(_value) > 10e15)
            {
                return;
            }

            try
            {
                res.Add(BSFiberCalculation.DsplN(typeof(BSCalcResultNDM), _attr), _value);
            }
            catch (Exception _E)
            {
                MessageBox.Show(_E.Message);
            }
        }

        /// <summary>
        ///  Результаты расчета по 1 группе предельных состояний
        /// </summary>
        public void Results1Group()
        {           
            AddToResult("eps_0", eps_0);
            AddToResult("rx", rx);
            AddToResult("Kx", Kx);
            AddToResult("ry", ry);
            AddToResult("Ky", Ky);

            AddToResult("sigmaB", sigmaB);
            AddToResult("sigmaS", sigmaS);

            AddToResult("e_fb_max", e_fb_max);
            AddToResult("e_s_max", e_s_max);            

                                    
            bool res_fb = e_fb_max <= Eps_fb_ult;
            if (res_fb)
                Msg.Add(string.Format("Проверка сечения по фибробетону пройдена. e_fb_max={0} <= e_fb_ult={1} ", Math.Round(e_fb_max, 6), Eps_fb_ult));
            else
                Msg.Add(string.Format("Не пройдена проверка сечения по фибробетону. e_fb_max={0} <= e_fb_ult={1} ", Math.Round(e_fb_max, 6), Eps_fb_ult));
            
            double e_s_ult = 0; // m_Rod.Eps_s_ult(DeformDiagram);
            bool res_s = e_s_max <= e_s_ult;

            if (res_s)
                Msg.Add(string.Format("Проверка сечения по арматуре пройдена. e_s_max={0} <= e_s_ult={1} ", Math.Round(e_s_max, 6), e_s_ult));
            else
                Msg.Add(string.Format("Не пройдена проверка сечения по арматуре. e_s_max={0} <= e_s_ult={1}", Math.Round(e_s_max, 6), e_s_ult));
        }

        /// <summary>
        ///  Результаты расчета по 2 группе предельных состояний
        /// </summary>
        public void Results2Group()
        {
            Mcrc = 1234345678;
            AddToResult("Mcrc", Mcrc, groupLSD);
            AddToResult("ry", ry, groupLSD);
        }


    }
}
