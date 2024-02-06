using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSFiberConcrete
{
    /// <summary>
    ///  Расчет исгибаемых элементов с рабочей арматурой
    /// </summary>
    internal class BSFiberCalc_NormRods
    {
    }

    /// <summary>
    ///  Расчет изгибаемого прямоугольного элемента с рабочей арматурой
    /// </summary>
    public class BSFiberCalc_RectRods : BSFibCalc_Rect
    {
        public void Calculate()
        {
            base.Calculate();

            double Mr = 2323, M_ult = 232;
            bool checkOK = Mr > M_ult;

            if (!checkOK)
                throw new Exception("Сечение не прошло проверку по арматуре");
        }

        public Dictionary<string, double> GeomParams()
        {
            throw new NotImplementedException();
        }

        public void GetParams(double[] _t)
        {
            throw new NotImplementedException();
        }

        public void GetSize(double[] _t)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, double> Results()
        {
            throw new NotImplementedException();
        }
    }
}
