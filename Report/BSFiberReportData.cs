using BSFiberConcrete.BSRFib;
using BSFiberConcrete.UnitsOfMeasurement;
using System;
using System.Collections.Generic;
using System.IO;

namespace BSFiberConcrete
{
    public class BSFiberReportData
    {
        public bool UseReinforcement { get; set; }
        public Dictionary<string, double> m_Beam;
        public Dictionary<string, double> m_Coeffs;
        public Dictionary<string, double> m_Efforts;
        public Dictionary<string, double> m_PhysParams;
        public Dictionary<string, double> m_GeomParams;
        public Dictionary<string, double> m_CalcResults1Group;
        public Dictionary<string, double> m_CalcResults2Group;
        public Dictionary<string, double> m_Reinforcement;
        public List<string> m_Messages;
        public List<string> m_Path2BeamDiagrams;
        public BeamSection BeamSection { get; set; }
        public LameUnitConverter UnitConverter { get; set; }
        public string ImageCalc { get; set; }
        public MemoryStream ImageStream {  get; set; }

        public void InitFromBSFiberCalculation(BSFiberCalculation _BSFibCalc, LameUnitConverter _UnitConverter)
        {
            BeamSection = _BSFibCalc.BeamSectionType();
            UseReinforcement = _BSFibCalc.UseRebar();
            m_Coeffs = _BSFibCalc.Coeffs;
            m_Efforts = _BSFibCalc.Efforts;
            m_GeomParams = _BSFibCalc.GeomParams();
            m_CalcResults1Group = _BSFibCalc.Results();
            m_Messages = _BSFibCalc.Msg;
            m_PhysParams = _BSFibCalc.PhysicalParameters();
            UnitConverter = _UnitConverter;
        }        
    }
}
