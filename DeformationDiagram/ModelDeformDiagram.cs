using BSFiberConcrete.DeformationDiagram.UserControls;
using MathNet.Numerics.Integration;
using Microsoft.SqlServer.Server;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace BSFiberConcrete.DeformationDiagram
{
    public class CalcDeformDiagram
    {
        public string typeMaterial;
        public string typeDiagram;
                                        public double Rt_n;
                                public double Rt1;
                                public double Rt2_n;
                                public double Rt3_n;
                                public double Et;
                public double et0;
        public double et1;
        public double et2;
        public double et3;
                                                public double R_n;
                                public double R1;
                                public double E;
                public double e0;
        public double e1;
        public double e2;
                                        private UserControl _deformationsView;
        private double[] _valuesRelativeDeformation;
                                public double[] deformsArray;
                                        public CalcDeformDiagram(string[] typesDiagram, double[] resists, double[] elasticity)
        {
            
            typeMaterial = typesDiagram[0];
            typeDiagram = typesDiagram[1];
            R_n = resists[0];
            Rt_n = resists[1];
            Rt2_n = resists[2];
            Rt3_n = resists[3];
            E = elasticity[0];
            Et = elasticity[1];
            if (typeMaterial == BSHelper.FiberConcrete)
            {
                                                _valuesRelativeDeformation = new double[] { 0.003, 0, 0.0035, 0, 0, 0.004, 0.015 };
                SetValuesRelativeDeformation();
                _deformationsView = new FiberBetonDeformationView(_valuesRelativeDeformation);
                            }
            else if (typeMaterial == BSHelper.Rebar)
            {
                _valuesRelativeDeformation = new double[] { 0, 0, 0.025};
                SetValuesRelativeDeformation();
                _deformationsView = new RebarDeformationView(_valuesRelativeDeformation);
            }
        }
                                        public void SetValuesRelativeDeformation()
        {
            if (typeMaterial == BSHelper.FiberConcrete)
            {
                                e0 = _valuesRelativeDeformation[0];
                                e2 = _valuesRelativeDeformation[2];
                                                et2 = _valuesRelativeDeformation[5];
                et3 = _valuesRelativeDeformation[6];
                FillDiagramsData();
                _valuesRelativeDeformation[1] = e1;
                _valuesRelativeDeformation[3] = et0;
                _valuesRelativeDeformation[4] = et1;
            }
            else if (typeMaterial == BSHelper.Rebar)
            {
                e2 = _valuesRelativeDeformation[2];
                et2 = _valuesRelativeDeformation[2];
                FillDiagramsData();
                 _valuesRelativeDeformation[0] = e0;
                _valuesRelativeDeformation[1] = e1;
            }
        }
                                private void FillDiagramsData()
        {
            if (typeMaterial == BSHelper.Concrete || typeMaterial == BSHelper.FiberConcrete)
            {
                if (typeDiagram == BSHelper.TwoLineDiagram)
                { R1 = R_n; }
                else if (typeDiagram == BSHelper.ThreeLineDiagram)
                { R1 = R_n * 0.6; }
                e1 = R1 / E;
                et0 = Rt_n / Et;
                et1 = et0 + 0.0001;
                                            }
            else if (typeMaterial == BSHelper.Rebar)
            {
                if (typeDiagram == BSHelper.TwoLineDiagram)
                {
                    R1 = R_n;
                    Rt1 = Rt_n;
                }
                else if (typeDiagram == BSHelper.ThreeLineDiagram)
                {
                    R1 = R_n * 0.9;
                    Rt1 = Rt_n * 0.9;
                }
                e1 = R1 / E;
                et1 = Rt1 / Et;
                e0 = e1 + 0.002;
                et0 = et1 + 0.002;
                                            }
            if (typeMaterial == BSHelper.Concrete)
            {
                if (typeDiagram == BSHelper.TwoLineDiagram)
                { deformsArray = new double[] { 0, e1, e2 }; }
                else if (typeDiagram == BSHelper.ThreeLineDiagram)
                { deformsArray = new double[] { 0, e1, e0, e2 }; }
            }
            else if (typeMaterial == BSHelper.FiberConcrete)
            {
                if (typeDiagram == BSHelper.TwoLineDiagram)
                { deformsArray = new double[] { -et3, -et2, -et1, -et0, 0, e1, e2 }; }
                else if (typeDiagram == BSHelper.ThreeLineDiagram)
                { deformsArray = new double[] { -et3, -et2, -et1, -et0, 0, e1, e0, e2 }; }
            }
            else if (typeMaterial == BSHelper.Rebar)
            {
                if (typeDiagram == BSHelper.TwoLineDiagram)
                { deformsArray = new double[] { -e2, -e1, 0, et1, et2 }; }
                else if (typeDiagram == BSHelper.ThreeLineDiagram)
                { deformsArray = new double[] { -e2, -e0, -e1, 0, et1, et0, et2 }; }
            }
        }
                    public double[,] Calculate()
        {
            double[,] result = new double[1, 1];
            if (typeMaterial == BSHelper.Concrete)
            {
                if (typeDiagram == BSHelper.TwoLineDiagram)
                { result = new double[2, 3] { {0, e1, e2 }, { 0, R_n, R_n} }; }
                else if (typeDiagram == BSHelper.ThreeLineDiagram)
                { result = new double[2, 4]{ {0, e1, e0, e2 }, { 0, R1, R_n, R_n} }; }
            }
            else if (typeMaterial == BSHelper.FiberConcrete)
            {
                                                if (typeDiagram == BSHelper.TwoLineDiagram)
                {
                    result = new double[2, 7]
                    { {-et3, -et2, -et1, -et0, 0, e1, e2 }, { -Rt3_n, -Rt2_n, -Rt_n, -Rt_n, 0, R_n, R_n} };
                }
                else if (typeDiagram == BSHelper.ThreeLineDiagram)
                {
                    result = new double[2, 8]
                    { {-et3, -et2, -et1, -et0, 0, e1, e0, e2 }, { -Rt3_n, -Rt2_n, -Rt_n, -Rt_n, 0, R1, R_n, R_n} };
                }
            }    
            return result;
        }
                                                public double getResists(double epsilon)
        {
            double res = 0;
            if (typeMaterial == BSHelper.Rebar)
            {
                                if (epsilon > 0)
                {
                    if (epsilon > et2)
                        return res;
                    if (typeDiagram == BSHelper.ThreeLineDiagram)
                    {
                        if (0 < epsilon && epsilon <= et1)
                            res = Et * epsilon;
                        else if (et1 < epsilon && epsilon <= et0)
                            res = (Rt1 + (Rt_n * 1.1 - Rt1) * (epsilon - et1) / (et0 - et1));
                        else if (et0 < epsilon && epsilon <= et2)
                            res = Rt_n *1.1;
                    }
                    else if (typeDiagram == BSHelper.TwoLineDiagram)
                    {
                        if (0 < epsilon && epsilon <= et1)
                            res = Et * epsilon;
                        else if (et1 < epsilon && epsilon <= et2)
                            res = Rt_n;
                    }
                }
                else if (epsilon < 0)
                {
                    if (epsilon < -e2)
                        return res;
                    if (typeDiagram == BSHelper.ThreeLineDiagram)
                    {
                        if (epsilon < 0  && -e1 <= epsilon)
                            res = E * epsilon;
                        else if (epsilon < -e1 && -e0 <= epsilon)
                            res = -(R1 + (R_n * 1.1 - R1) * (epsilon - e1) / (-e0 - e1));
                        else if (epsilon < -e0 && -e2 <= epsilon)
                            res = -R_n * 1.1;
                    }
                    else if (typeDiagram == BSHelper.TwoLineDiagram)
                    {
                        if (epsilon < 0 && -e1 <= epsilon)
                            res = E * epsilon;
                        else if ( epsilon < -et1 && -et2 <= epsilon)
                            res = -R_n;
                    }
                }
            }
            else
            {
                if (epsilon > 0)
                {
                                        if (epsilon > e2)
                    { return res; }
                    if (typeDiagram == BSHelper.ThreeLineDiagram)
                    {
                        if (0 < epsilon && epsilon <= e1)
                        { res = E * epsilon; }
                        else if (e1 < epsilon && epsilon <= e0)
                        {
                                                        res = (R1 + (R_n - R1) * (epsilon - e1) / (e0 - e1));
                        }
                        else if (e0 < epsilon && epsilon <= e2)
                        { res = R_n; }
                    }
                    else if (typeDiagram == BSHelper.TwoLineDiagram)
                    {
                        if (0 < epsilon && epsilon <= e1)
                        { res = E * epsilon; }
                        else if (e1 < epsilon && epsilon <= e2)
                        { res = R_n; }
                    }
                }
                else if (epsilon < 0)
                {
                                        if (epsilon < -et3)
                    { return res; }
                    if (epsilon < 0 && -et0 <= epsilon)
                    { res = Et * epsilon; }
                    if (epsilon < -et0 && -et1 <= epsilon)
                    { res = -Rt_n; }
                    else if (epsilon < -et1 && -et2 <= epsilon)
                    {
                        res = -Rt_n * (1 + (1 - Rt2_n / Rt_n) * (epsilon - et1) / (et2 - et1));
                                            }
                    else if (epsilon < -et2 && -et3 <= epsilon)
                    {
                        res = -Rt2_n * (1 + (1 - Rt3_n / Rt2_n) * (epsilon + et2) / (et3 - et2));
                                            }
                }
            }
                                                                                                                                                                                                                                                                                                                                                
                                                                                                                                                                                    
            return res;
        }
        public void UpDateUserControll(TableLayoutPanel table)
        {
            table.Controls.Add(_deformationsView, 0, 0);
            _deformationsView.Dock = System.Windows.Forms.DockStyle.Fill;
        }
    }
    public static class DataForDeformDiagram
    {
                public static double[] resists;
        public static double[] deforms;
        public static double[] E;
    }
}
