using MathNet.Numerics.Integration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace BSFiberConcrete
{
    public interface IBeamGeometry
    {
        double Area();

        // section moment of inertia
        double W_s();

        double Jy();
        double Jx();
    }

    // конечный элемент
    public class BSElement
    {
        // Номер
        public int Num;

        // Координаты Ц.Т.
        public double Z_X { get; }
        public double Z_Y { get; }

        public double A { get; set; }
        public double B { get; set; }

        // Площадь Ц.Т.
        public double Ab {  get => Area(); } 

        // напряжение на уровне Ц.Т.
        public double Sigma { get; set; }

        // модуль упругости
        public double E { get; set; }

        // относительная деформация
        public double Epsilon { get; set; }

        public double Nu { get => calcNu(); }

        private double Area() => A * B;

        public double calcNu() => Epsilon != 0 ? Sigma / (E * Epsilon) : 1;


        public BSElement (int _N, double _X, double _Y)
        {
            Num = _N;
            Z_X = _X;
            Z_Y = _Y;            
        }
    }

    
    /// <summary>
    /// Балка
    /// </summary>
    public class BSBeam : IBeamGeometry
    {        
        // количество стержней арматуры
        public int RodsQty { get { return (Rods != null) ? Rods.Count : 0; } set { RodsQty = value; } }
        public List<BSRod> Rods { get; set; }

        public BSMatRod MatRod { get { return Rods?.First().MatRod; } }

        // Материал балки (фибробетон, переделать на универсальный)
        public BSMatFiber Mat { get; set; }

        // Координаты Ц.Т.
        public double Zfb_X { get; set; }
        public double Z_fb_Y { get; set; }

        public double Length { get; set; }

        [DisplayName("Площадь армирования, см2")]
        public double AreaS()
        {            
            double? _As = Rods?.Sum(x => x.As);
            return Convert.ToDouble(_As); 
        }

        public virtual double Area()
        {
            return 0;
        }

        public virtual double W_s()
        {            
            return 0;
        }

        public virtual double I_s()
        {
            return 0;
        }

        public virtual double Jy()
        {
            throw new NotImplementedException();
        }

        public virtual double Jx()
        {
            throw new NotImplementedException();
        }

        public BSBeam()
        {            
        }
    }           
}
