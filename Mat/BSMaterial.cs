using System;
using System.ComponentModel;

namespace BSFiberConcrete
{       
                public class BSMatConcrete : IMaterial, INonlinear
    {
        public string Name => "Бетон";
        public string BT { get; set; }
        public double E_young => Eb;


        [DisplayName("Сопротивление на сжатие")]
        public double Rb { get; set; }

        [DisplayName("Модуль сжатия бетона")]
        public double Eb { get; set; }

        public double e_b0;
        public double e_b1;
        public double e_b2;

                public  double Eps_StateDiagram3L(double e_b, out int _res, int _group = 1)
        {
            double sigma_b = Rb;
            double sigma_b1 = 0.6 * Rb;
            _res = 0;

            e_b1 = sigma_b1 / Eb;

            if (0 <= e_b  && e_b <= e_b1 )
            {
                sigma_b = Eb * e_b;
            }
            else if (e_b1 < e_b && e_b < e_b0)
            {
                sigma_b = ((1 - sigma_b1/Rb)*(e_b - e_b1)/ (e_b0 - e_b1) + sigma_b1/Rb )* Rb;
            }
            else if (e_b0 <= e_b && e_b <= e_b2)
            {
                sigma_b = Rb;                    
            }

            return sigma_b;
        }

        public double Eps_StDiagram2L(double _e, out int _res, int _group = 1)
        {
            double sgm = 0;
            _res = 0;

            if (0 <= _e && _e < e_b1)
            {
                sgm = Eb * _e;
            }
            else if (e_b1 <= _e && _e < e_b2) 
            {
                sgm = Rb;
            }

            return sgm;
        }

        public BSMatConcrete()
        {
            BT = "B30";
        }
    }

        

}
