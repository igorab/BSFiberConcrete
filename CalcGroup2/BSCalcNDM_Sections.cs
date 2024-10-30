using BSCalcLib;
using BSFiberConcrete.Lib;
using System;
using System.Collections.Generic;


namespace BSFiberConcrete.CalcGroup2
{
    public partial class BSCalcNDM
    {
                                        public static (List<double>, List<double>, List<double>, double, double) 
            ReinforcementBinding(BeamSection _BeamSection, double _leftX, double _leftY, bool _useRebar = true)
        {
                        List<double> rodD = new List<double>();
                        List<double> bY = new List<double>();
                        List<double> hX = new List<double>();
                        int d_qty = 0;
                        double area_total = 0;

            if (_useRebar)
            {
                                List<BSRod> _rods = BSData.LoadBSRod(_BeamSection);
                d_qty = _rods.Count;
                foreach (BSRod lr in _rods)
                {
                    area_total += BSHelper.AreaCircle(lr.D);
                }

                foreach (BSRod lrod in _rods)
                {
                    rodD.Add(lrod.D);
                    hX.Add(lrod.CG_Y - _leftY);
                    bY.Add(lrod.CG_X - _leftX);
                }
            }

            return (rodD, hX, bY, d_qty, area_total);
        }

                                public int InitReinforcement(double _y0 = 0, double _z0 = 0 )
        {                        
                        foreach (double d in ds)
            {
                As.Add(Math.PI * Math.Pow(d, 2) / 4.0);
            }
                        for (int l = 0; l < ds.Count; l++)
            {
                y0s[l] += _y0;
                z0s[l] += _z0;
            }
                        int m = As.Count;
            return m;
        }

                                                                        public int InitRectangleSection(double _b, double _h, double _y0 = 0, double _z0 = 0)
        {
                        int n = ny * nz;           
            double sy = _b / ny;
            double sz = _h / nz;
                        double Ab1 = sy * sz;

                        for (int i = 0; i < n; i++)
                Ab.Add(Ab1);

                        for (int iz = 0; iz < nz; iz++)
                for (int iy = 0; iy < ny; iy++)
                    y0b.Add( iy * sy + sy / 2.0 + _y0);

                        for (int iz = 0; iz < nz; iz++)
                for (int iy = 0; iy < ny; iy++)
                    z0b.Add( iz * sz + sz / 2.0 + _z0);
           
            return n;
        }


                public int InitIBeamSection(double _bf, double _hf, double _bw, double _hw, double _b1f, double _h1f)
        {
            int n1 = 0, n2 = 0, n3 = 0;

            if (_bf>0 && _hf>0)
                n1 = InitRectangleSection(_bf, _hf, -_bf/2.0, 0);

            n2 = InitRectangleSection(_bw, _hw, -_bw / 2.0, _hf);

            if (_b1f > 0 && _h1f > 0)
                n3 = InitRectangleSection(_b1f, _h1f, -_b1f / 2.0, _hf + _hw);

            return n1+n2+n3;
        }
        
                private int InitRingSection(double _r1, double _R2)
        {            
            if (r1 >= R2) throw BSBeam_Ring.RadiiError();              
            List<object> Tr = Tri.CalculationScheme();
                        var triAreas = Tri.triAreas;
                        var triCGs = Tri.triCGs;             
                        foreach (var _area in triAreas)
                Ab.Add(_area);

                                    foreach (var triCG in triCGs)
            {
                y0b.Add(triCG.X);
                z0b.Add(triCG.Y);
            }
            
            return triAreas.Count; 
        }

                                private int InitAnySection()
        {
            _ = Tri.CalculationScheme(false);

                        var triAreas = Tri.triAreas;

                        var triCGs = Tri.triCGs;

                        foreach (var _area in triAreas)
                Ab.Add(_area);

                                    foreach (var triCG in triCGs)
            {
                y0b.Add(triCG.X);
                z0b.Add(triCG.Y);
            }

            return triAreas.Count;
        }
    }
}
