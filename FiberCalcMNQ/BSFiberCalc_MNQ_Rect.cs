using MathNet.Numerics.Integration;
using System;
using System.Collections.Generic;
using System.Linq;
namespace BSFiberConcrete
{
                public class BSFiberCalc_MNQ_Rect : BSFiberCalc_MNQ
    {
        private string m_ImgCalc;        
        public BSFiberCalc_MNQ_Rect()
        {
            m_Beam = new BSBeam_Rect();            
        }
                                        public override string ImageCalc()
        {
            if (!string.IsNullOrEmpty(m_ImgCalc))
                return m_ImgCalc;
            return (N_Out) ? "Rect_N_out.PNG" : "Rect_N.PNG";
        } 
        
        public override void SetSize(double[] _t)
        {
            m_Beam.SetSizes(_t);
            (b, h, LngthCalc0) = (_t[0], _t[1], _t[2]);
            A = m_Beam.Area();
            I = m_Beam.Jy();
            
            y_t = m_Beam.y_t;
        }
                                protected new void Calculate_N()
        {
            N_In = true;
            base.Calculate_N();            
        }
                                        protected new void Calculate_N_Rods()
        {
            m_ImgCalc = "Rect_Rods_N_out.PNG";
            base.Calculate_N_Rods();            
        }
                                                        private new void Calculate_N_Out()
        {
            base.Calculate_N_Out();            
        }
                                private void CalculateQ()
        {
            m_ImgCalc = "Incline_Q.PNG";
            base.Calculate_Qx(b, h);
            base.Calculate_Qy(h, b);
        }
       
                                private void CalculateM()
        {
            base.Calculate_My(b, h);
            base.Calculate_Mx(h, b);
        }
                                public override bool Calculate()
        {            
            if (Shear)
            {                
                                CalculateQ();
                                CalculateM();
            }
            else if (UseRebar)
            {
                Calculate_N_Rods();
            }
            else
            {                
                if (N_Out)
                {
                    Calculate_N_Out();
                }
                else
                {
                    Calculate_N();
                }
            }
            return true;
        }
        public override Dictionary<string, double> Results()
        {
            
            Dictionary<string, double>  dictRes =  new Dictionary<string, double>() 
            {
                { DN(typeof(BSFiberCalc_MNQ), "M_ult"), M_ult },
                { DN(typeof(BSFiberCalc_MNQ), "UtilRate_My"), UtilRate_My },
                { DN(typeof(BSFiberCalc_MNQ), "N_ult"), N_ult },
                { DN(typeof(BSFiberCalc_MNQ), "UtilRate_N"), UtilRate_N },
                { DN(typeof(BSFiberCalc_MNQ), "Q_ult"), Q_ult },
                { DN(typeof(BSFiberCalc_MNQ), "UtilRate_Qx"), UtilRate_Qx },
            };
            
                                                
            return dictRes;
        }
    }
}
