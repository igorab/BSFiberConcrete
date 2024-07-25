using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BSFiberConcrete.CalcGroup2
{
    public partial class BSCalcNDM
    {
        // двутавровое сечение
        public (int, int) InitIBeamSection(double _bf, double _hf, double _bw, double _hw, double _b1f, double _h1f)
        {
            int n1 = 0, n2 = 0, n3=0, m1=0, m2=0, m3=0;

            if (_bf>0 && _hf>0)
                (n1, m1) = InitRectangleSection(_bf, _hf);

            (n2, m2) = InitRectangleSection(_bw, _hw);

            if (_b1f > 0 && _h1f > 0)
                (n3, m3) = InitRectangleSection(_b1f, _h1f);

            return (n1+n2+n3, m1+m2+m3);
        }
    }
}
