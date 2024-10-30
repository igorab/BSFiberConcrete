using System.Diagnostics;
using System.Windows.Forms;
namespace BSFiberConcrete
{
                public class BSMatRod : IMaterial, INonlinear
    {
        public string Name => "Сталь";
        public double E_young => Es;
                public string RCls { get; set; }
                                public double Es { get; set; }
                public double Rsn { get; set; }
                public double Rs { get; set; }
                public double Rs_ser => Rsn;
        
                public double Rsc { get; set; }
                public double Rsc_ser => Rsn;
                public double As { get; set; }
                public double As1 { get; set; }
                                public double a_s { get; set; }
                                public double a_s1 { get; set; }
                                        public bool Reinforcement { get; set; }
                public double Nju_s { get; set; }
                                                public double Eps_s_ult(DeformDiagramType diagramType) 
        { 
            double esult = 0;
            if (diagramType == DeformDiagramType.D2Linear)
            {
                esult = 0.025;
            }
            else if (diagramType == DeformDiagramType.D3Linear)
            {
                esult = 0.015;
            }
            return esult;
        }
                                public double epsilon_s() => Es != 0 ? Rs / Es : 0;
        public double e_s0 { get; set; }
        public double e_s2 { get; set; }
        public BSMatRod()
        {
        }
        public BSMatRod(double _Es)
        {
            Es = _Es;
        }
                                                        public double Eps_StateDiagram3L(double _e_s, out int _res, int _group = 1)
        {
            double rs = Rs;
            _res = 0;
            double sigma_s = 0;
            double sigma_s1 = 0.9 * rs;
            double sigma_s2 = 1.1 * rs;
            double e_s0 = rs / Es + 0.002;
            double e_s1 = sigma_s1 / Es;
            
            if (0 <= _e_s && _e_s <= e_s1)
            {
                sigma_s = rs * _e_s;
            }
            else if (e_s1 <= _e_s && _e_s <= e_s2)
            {
                sigma_s = ((1 - sigma_s1 / rs) * (_e_s - e_s1) / (e_s0 - e_s1) + sigma_s1 / rs) * rs;
                if (sigma_s > sigma_s2) sigma_s = sigma_s2;
            }            
            else if (_e_s > e_s2)
            {
                Debug.Assert(true, "Превышена деформация арматуры");
                sigma_s = 0;
            }
            return sigma_s;
        }
                                                                public double Eps_StDiagram2L(double _e, out int _res, int _group = 1)
        {
            double sgm = 0;
            double rs = Rs;
            _res = 0;
            if (0 < _e && _e < e_s0)
            {
                sgm = Es * _e;
            }
            else if (e_s0 <= _e && _e <= e_s2)
            {
                sgm = rs;
            }
            else if (_e > e_s2)             {
                Debug.Assert(true, "Превышен предел прочности (временное сопротивление) ");
                _res = -1;
                if (_group == 1)
                    sgm = 0;
                else if (_group == 2)
                    sgm = Rs_ser;
            }
            return sgm;
        }
                                                        public static decimal NumEps_s1(decimal _Rs, decimal _Es)
        {
                        if (_Rs == 0 || _Es == 0)
                return 0;
            
            return _Rs * 0.9m  / _Es;
        }
                                                        public static decimal NumEps_s0(decimal _Rs, decimal _Es)
        {
                        if (_Rs == 0 || _Es == 0)
                return 0;
            return NumEps_s1(_Rs, _Es) + 0.002m;
        }
    }
}
