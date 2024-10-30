using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BSFiberConcrete
{
    public interface IBeamGeometry
    {
        double Area();
                double W_s();
        double Jy();
        double Jx();
        double b { get; set; }
        double h { get; set; }
                                        Dictionary<string, double> GetDimension();
    }
}
