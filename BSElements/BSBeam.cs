using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace BSFiberConcrete
{
                public class BSBeam : IBeamGeometry
    {
                public int RodsQty { get { return (Rods != null) ? Rods.Count : 0; } set { RodsQty = value; } }
        public List<BSRod> Rods { get; set; }

        public BSMatRod MatRod { get { return Rods?.First().MatRod; } }

                public BSMatFiber Mat { get; set; }

                public double Zfb_X { get; set; }
        public double Z_fb_Y { get; set; }

        public virtual double h { get; set; }
        public virtual double b { get; set; }

        public double Length { get; set; }

        public virtual double Width { get; }
        public virtual double Height { get; }

                                        public virtual (double, double) CG() => (Width / 2.0, Height / 2.0);


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
            return 0;
        }

        public virtual double Jx()
        {
            return 0;
        }

                                        public virtual Dictionary<string, double> GetDimension()
        {
            Dictionary<string, double> dimensionOfSection = new Dictionary<string, double>()
            {
                { "Высота сечения, h, [см]", h },
                { "Ширина сечения, b, [см]", b }
            };
            return dimensionOfSection;
        }


                                        public virtual double Sy()
        {
            return 0;
        }
                                        public virtual double Sx()
        {
            return 0;
        }

        

        public virtual double y_t => h / 2; 
        
        public BSBeam()
        {            
        }

        public virtual void SetSizes(double[] _t) { }

                                                                                public double Sigma_Z(double _N, double _Mx, double _My, double _X, double _Y)
        {
            double _Jx = Jx();
            double _Jy = Jy();
            double _Area = Area();

            double sgm_z = (_Area >0) ? _N / _Area : 0;
            sgm_z += (_Jx != 0) ? _Mx / _Jx * _X : 0;
            sgm_z += (_Jy != 0) ?  _My / _Jy * _Y : 0 ;

            return sgm_z;
        }

                                                public static BSBeam construct(BeamSection _BeamSection)
        {
            switch (_BeamSection)
            {
                case BeamSection.Rect:
                    return new BSBeam_Rect();
                case BeamSection.IBeam:
                case BeamSection.LBeam:
                case BeamSection.TBeam:
                    return new BSBeam_IT();
                case BeamSection.Ring:
                    return new BSBeam_Ring();
            }
            return new BSBeam();
        }


        public string DN(Type _T, string _property) => _T.GetProperty(_property).GetCustomAttribute<DisplayNameAttribute>().DisplayName;



    }           
}
