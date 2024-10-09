using BSFiberConcrete.BSRFib;
using BSFiberConcrete.BSRFib.FiberCalculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BSFiberConcrete
{
    public class CalcOfLenRebar : ViewModelBase
    {

        private RebarMat _Rebar;
        private FiberConcreteMaterial _FiberConcrete;
        public RebarMat Rebar
        { 
            get { return _Rebar; }
            private set { _Rebar = value; }
        }
        public FiberConcreteMaterial FiberConcrete
        {
            get { return _FiberConcrete; }
            private set { _FiberConcrete = value; }
        }

        /// <summary>
        /// коэффициент, учитывающий влияние на длину анкеровки напряженного состояния
        /// бетона и арматуры и конструктивного решения элемента в зоне анкеровки
        /// </summary>
        private double _alpha_1;

        // расчетные параметры
        private double _Rbond;
        private double _l0_an;
        private double _lan;

        #region Properties

        public double alpha_1
        {
            get { return _alpha_1; }
            private set
            {
                _alpha_1 = value;
                OnPropertyChanged();
            }
        }
        public double Rbond
        {
            get { return _Rbond; }
            private set
            {
                _Rbond = value;
                OnPropertyChanged();
            }
        }
        public double l0_an
        {
            get { return _l0_an; }
            private set
            {
                _l0_an = value;
                OnPropertyChanged();
            }
        }
        public double lan
        {
            get { return _lan; }
            private set
            {
                _lan = value;
                OnPropertyChanged();
            }
        }
        #endregion





        public CalcOfLenRebar(RebarMat rebar = null, FiberConcreteMaterial fiberConcrete = null )
        {
            if (rebar == null) { Rebar = new RebarMat(); }
            if (fiberConcrete == null) { FiberConcrete = new FiberConcreteMaterial(); }
        }


        public void SetCoefAlpha_1(int index)
        {
            if (index == 0) // стержень растянут
            {
                _alpha_1 = 1;
            }
            else // стержень сжат
            { _alpha_1 = 0.75; }
        }


        /// <summary>
        /// Расчет длины анкеровки
        /// </summary>
        public void Calculate()
        {

            Rbond = 0;
            l0_an = 0;
            lan = 0;

            Rbond = Rebar.Hita_1 * Rebar.Hita_2 * FiberConcrete.Rfbt;
            l0_an = Math.Round(Rebar.Rs * Rebar.Square / (Rbond * Rebar.us),3);
            lan = Math.Round(alpha_1 * l0_an, 3);

        }



        public void GenerateReport()
        {
            try
            {
                BSRFibLabReport labReport = new BSRFibLabReport();

                labReport.ReportName = "Определенеи длины анкеровки арматуры";
                labReport.SampleDescr = "";
                labReport.SampleName = "";

                Dictionary<string, string> InputData = new Dictionary<string, string>()
                {
                    ["Класс фибробетона по прочности на растяжение"] = FiberConcrete.Bft,
                    ["Расчетное сопротивление осевому растяжению Rfbt [МПа] "] = Convert.ToString(Math.Round(FiberConcrete.Rfbt, 3)),
                    ["Класс арматуры"] = Rebar.TypeRebar,
                    ["Диаметр арматуры ds [мм] "] = Rebar.Diameter.ToString(),
                    ["Расчетное значения сопротивления арматуры растяжению Rs [МПа] "] = Convert.ToString(Math.Round(Rebar.Rs, 3)),
                };


                Dictionary<string, string> LabItems = new Dictionary<string, string>()
                {
                    ["П 8.7. Базовая (основная) длина анкеровки l0,an [мм] "] = Convert.ToString(Math.Round(l0_an, 3)),
                    ["Требуемая расчетная длина lan [мм] "] = Convert.ToString(Math.Round(lan, 3)),
                };
                labReport.InputData = InputData;
                labReport.LabItems = LabItems;
                //labReport.ReportMessage = _msgToReport;
                labReport.RunReport();
            }
            catch (Exception _ex)
            {
                MessageBox.Show(_ex.Message);
            }
        }
    }
}
