using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFiberConcrete
{
    /// <summary>
    /// Расчет по прочности сталефибробетонных элементов при действии поперечных сил
    /// </summary>
    public class BSFiberCalc_Q : BSFiberCalc_MNQ
    {
        /*
        // высота и ширина сечения, см
        private double h, b;
        // Растояние до цента тяжести арматуры растянутой арматуры см
        private double a = 4;
        // рабочая высота сечения по растянутой арматуре
        private double h0;
        // поперечная сила в наклонном сечении с длиной проекции С кг
        private double Q;
        //Коэфициенты условия работы п.п. 5.2.7
        private double Yb1, Yb2, Yb3, Yb5, Yb;
        //для расчета по предельным состояниям первой группы при назначении класса сталефибробетона по прочности на растяжение
        private double Yft;

        private double Rfbn, Rfb;

        // сопротивления сталефибробетона осевому растяжению  м.п. 4.2.1 (кг/см2)
        private double Rfbtn;

        // Расчетное сопротивление сталефибробетона осевому растяжению 
        private double Rfbt;

        private double C_min, C_max;

        [DisplayName(displayName: "Предельная перерезывающая сила по полосе между наклонными сечениями")]
        private double Qult { get; set; }

        public void GetSize(double[] _t)
        {
            (h, b) = (_t[0], _t[1]);
        }

        public void GetParams(double[] _t)
        {
            h0 = h - a;
            Q = 5000;
            Yft = 1.3;
        }

        public void Calculate()
        {
            double[] C = new double[10];

            Rfbt = Rfbtn / Yft * Yb1 * Yb5;            

            for (int i = 0; i < C.Length; i ++)
            {
                double q_fbi = 1.5 * Rfbt * b * h0*h0 / C[i];
            }

        }
        */
    }
}
