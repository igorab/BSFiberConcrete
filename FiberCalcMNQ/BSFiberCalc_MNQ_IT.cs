namespace BSFiberConcrete
{
        public class BSFiberCalc_MNQ_IT : BSFiberCalc_MNQ
    {
        public BSBeam_IT beam { get; set; }
        public BSFiberCalc_MNQ_IT()
        {
            this.beam = new BSBeam_IT();
            base.m_Beam = this.beam;
        }
        public override bool Calculate()
        {
            if (N_Out)
            {
                Calculate_N_Out();                
            }
            else if (Shear)
            {
                m_ImgCalc = "Incline_Q.PNG";
                                Calculate_Qx(b, h);
                                Calculate_Qy(h, b);
                                Calculate_My(b, h);
                                Calculate_Mx(h, b);
            }
            else if (UseRebar)
            {
                Calculate_N_Rods();
            }
            else
            {
                Calculate_N();
            }
            
            return true;
        }
       
        public override void SetSize(double[] _t)
        {
            beam.SetSizes(_t);
            base.m_Beam = this.beam;
            LngthCalc0 = beam.Length;
            b = beam.b;
            h = beam.h;
            I = beam.Jx();
            A = beam.Area();
            y_t = beam.y_t;
        }
    }
}
