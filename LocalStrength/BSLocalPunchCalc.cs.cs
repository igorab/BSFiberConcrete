using BSFiberConcrete.Lib;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFiberConcrete.LocalStrength
{
    /// <summary>
    /// Расчет на продавливание
    /// </summary>
    public class BSLocalPunchCalc : BSLocalStrengthCalc
    {
        // Ширина приложения нагрузки (см)
        protected double b1;
        protected double h0;
        // Рабочая высота плиты по x(см)
        protected double h0x;
        //Рабочая высота плиты по y(см)
        protected double h0y;
        protected double u;
        protected double Afb;
        protected double Ffb_ult;


        // Усилия:
        // сила продавливания
        protected double F;
        // Изгибающий момент по X кг*см
        protected double Mx; // кг*см
        // Изгибающий момент по Y кг*см
        protected double My; // кг*см

        // Расчет на действия усилий:
        // Момент сопротивления расчетного контура сталефибробетона при продавливании вокруг X
        protected double Wfbx;
        // Момент сопротивления расчетного контура сталефибробетона при продавливании вокруг Y
        protected double Wfby;
        //Предельный изгибающий момент
        protected double Mfb_x_ult;
        //Предельный изгибающий момент
        protected double Mfb_y_ult;
        //Коэффициент использования
        protected double FMxMy_uc;


        // Арматура:
        // Расчетное сопротивление арматуры на продавливание СП63 (кг/см2) таб.6.15
        protected double Rsw;
        // площадь сечения поперечной арматуры с шагом sw, расположенная в пределах
        // расстояния 0,5h0 по обе стороны от контура расчетного поперечного сечения по периметру контура расчетного поперечного сечения
        protected double Asw;
        //шаг поперечной арматуры
        protected double sw;
        //усилие в поперечной арматуре на единицу длины контура расчетного поперечного сечения
        protected double q_sw;
        //Предельное усилие,воспринимаемое поперечной арматурой при продавливании
        protected double Fsw_ult;
        //Предельное усилие, воспринимаемое сталефибробетоном и арматурой
        protected double Fult;
        // Длина расчетного контура №2
        protected double a;
        // Ширина расчетного контура №2
        protected double b;
        // Коэфициент использования
        protected double util_coeff;

        public BSLocalPunchCalc()
        {
            Scheme = 1;
            UseReinforcement = false;

            DCalcResult();
        }

        public override void InitDataSource()
        {
            m_DS = BSData.LoadLocalPunch();            
        }

        private void InitValuesFromDataSource()
        {
            Dictionary<string, double> D = new Dictionary<string, double>();
            foreach (var item in m_DS) D[item.VarName] = item.Value;

            // Усилия:
            (F, Mx, My) = (D["F"], D["Mx"], D["My"]);

            // Бетон:
            (a1, b1, h0x, h0y, Rfbtn, Yft, Yb1, Yb5, Rfbt, h0, u, Afb, Ffb_ult) =
                (D["a1"], D["a2"], D["h0x"], D["h0y"], D["Rfbtn"], D["Yft"], D["Yb1"], D["Yb5"], D["Rfbt"], D["h0"], D["u"], D["Afb"], D["Ffbult"]);

            // Арматура:            
            (Rsw, Asw, sw, q_sw, Fsw_ult, Fult, a, b, util_coeff) = (D["Rsw"], D["Asw"], D["sw"], D["q_sw"], D["Fsw_ult"], D["Fult"], D["a"], D["b"], D["util_coeff"]);
        }

        public override string ReportName()
        {
            return "Продавливание";
        }

        public override string SampleName()
        {
            if (UseReinforcement)
                return "Расчет сталефибробетонных элементов на продавливание при действии сосредоточенной силы c арматурой";
            else
                return "Расчет сталефибробетонных элементов на продавливание при действии сосредоточенной силы без арматуры";
        }

        public override void UpdateInputData(Dictionary<string, double> _Ds)
        {
            m_DS = BSQuery.UpdateLocalPunch(_Ds);
        }

        private void DCalcResult()
        {
            Dc = new Dictionary<string, double>()
            {
                ["h0"] = h0,
                ["u"] = u,
                ["Afb"] = Afb,
                ["Ffbult"] = Ffb_ult,
              
                ["a"] = a,
                ["b"] = b,
                ["Afb"] = Afb,
                ["Ffb_ult"] = Ffb_ult,
                ["Fult"] = Fult,
                ["util_coeff"] = util_coeff,

                ["Wfbx"] = Wfbx,
                ["Wfby"] = Wfby,
                ["Mfb_x_ult"] = Mfb_x_ult,
                ["Mfb_y_ult"] = Mfb_y_ult,
                ["FMxMy_uc"] = FMxMy_uc
            };
        }


        public override bool RunCalc()
        {
            (h0, u, Afb, Ffb_ult) = (0, 0, 0, 0);

            bool ok = base.RunCalc();

            if (!ok)
            {
                throw new Exception("Ошибка обновления данных в таблице");
            }

            try
            {
                InitValuesFromDataSource();

                // Приведенная рабочая высота сечения
                h0 = 0.5 * (h0x + h0y);

                // Периметр контура расчетного поперечного сечения
                u = 2 * a1 + 2 * b1;

                // Площадь расчетного поперечного сечения (см2) по ф.(6.92)
                Afb = u * h0;

                // Предельное усилие, воспринимаемое сталефибробетоном. (кг)
                Ffb_ult = Rfbt * Afb;

                if (F != 0 || Mx != 0 || My != 0)
                {
                    RunCalcFM();   
                }
                
                if (UseReinforcement)
                {
                    this.ReinforcementCalc();
                }

                DCalcResult();

                m_DS = BSQuery.UpdateLocalPunch(Dc);

                ok = true;
            }
            catch 
            {
                ok = false;
            }
            return ok;
        }

                
        // расчет элементов на продавливание при действии сосредоточенных сил и моментов без арматуры
        public bool RunCalcFM()
        {
            double Lx = b1 + h0;
            double Ly = a1 + h0;

            double Ifbx = 2 * ( Lx * Math.Pow(1,3) /12 + Math.Pow(Ly/2, 2) * Lx * 1 + 1 * Math.Pow(Ly, 3)/12 );
            double Ifby = 2 * (Ly * Math.Pow(1, 3) / 12 + Math.Pow(Lx / 2, 2) * Ly * 1 + 1 * Math.Pow(Lx, 3) / 12);

            double x_max = Lx / 2;
            double y_max = Ly / 2;

            // Момент сопротивления расчетного контура сталефибробетона при продавливании вокруг X
            Wfbx = Ifbx / y_max;

            // Момент сопротивления расчетного контура сталефибробетона при продавливании вокруг Y
            Wfby = Ifby / x_max;

            //Предельный изгибающий момент
            Mfb_x_ult = Rfbt * Wfbx * h0;

            //Предельный изгибающий момент
            Mfb_y_ult = Rfbt * Wfby * h0;

            FMxMy_uc = F / Ffb_ult + Mx / Mfb_x_ult + My / Mfb_y_ult;

            return true;
        }

       
        // расчет элементов на продавливание при действии сосредоточенной силы с арматурой
        public override bool ReinforcementCalc()
        {            
            // Длина расчетного контура №2
            a = a1 + 4 * h0;

            // Ширина расчетного контура №2
            b = b1 + 4 * h0;

            // Периметр контура расчетного поперечного сечения(см)
            u = 2 * a + 2 * b;

            // Площадь расчетного поперечного сечения(см)
            Afb = u * h0;

            // Предельное усилие, воспринимаемое сталефибробетоном по второму контуру. (кг)
            Ffb_ult = Rfbt * Afb;

            Fult = Ffb_ult + Fsw_ult;

            // Коэфициент использования
            util_coeff = F / Ffb_ult;

            return true;
        }

        public override string SampleDescr()
        {
            return "Расчет элементов на продавливание";
        }
    }
}
