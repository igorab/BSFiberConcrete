using System;
using System.Globalization;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;
namespace BSFiberConcrete
{   
                public class BSFiberLoadData
    {
        private List<double> m_Prms = new List<double>() {0,0,0,0,0,0,0,0,0};
        public double[] Params { get => m_Prms.ToArray(); }
                private BSFiberParams m_FiberParams { get; set; }
        public Dictionary<string, double> FibInitCalcParams {private get; set; }
                public Fiber Fiber { get => m_FiberParams?.Fiber;  }
                public Rebar Rebar { get => m_FiberParams?.Rebar;  }
       
                public Beton2 Beton2 { get => m_FiberParams?.Beton2;  }
        private double ToDouble(string _num) => BSHelper.ToDouble(_num);
                
                                public void ReadParamsFromJson()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "Templates\\BSFiberParams.json");
            m_FiberParams = new BSFiberParams();
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                m_FiberParams = JsonSerializer.Deserialize<BSFiberParams>(fs);
            }            
        }
                                        public void SaveInitSectionsToJson(FileStream _fs = null)
        {
            string path = Path.Combine(Environment.CurrentDirectory, "Templates\\BSInit.json");
            Dictionary<string, double> fibInit = new Dictionary<string, double>(FibInitCalcParams);
                                                
            using (FileStream fs = (_fs != null ) ? _fs : new FileStream(path, FileMode.Truncate))
            {
                JsonSerializer.Serialize<Dictionary<string, double>>(fs, fibInit);
            }
        }
                                        public void InitEffortsFromDB(ref Dictionary<string, double> _IniEff)
        {                      
            List<Efforts> eff = Lib.BSData.LoadEfforts();
            if (eff.Count > 0)
            {
                _IniEff["Mx"] = eff[0].Mx;
                _IniEff["My"] = eff[0].My;
                _IniEff["N"]  = eff[0].N;
                _IniEff["Qx"] = eff[0].Qx;
                _IniEff["Qy"] = eff[0].Qy;
            }
        }
                                                        public Dictionary<string, object> ReadInitFromJson(string filePath)
        {
            string jsonfile = !string.IsNullOrEmpty(filePath) ? filePath : Path.Combine(Environment.CurrentDirectory, @"Templates\BSInit.json");
            var keyValuePairs = new Dictionary<string, object>();            
            using (FileStream fs = new FileStream(jsonfile, FileMode.OpenOrCreate))
            {
                keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(fs);
            }
            
            return keyValuePairs;
        }
                                public void Load()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "Templates\\FiberConcrete.csv");
            m_Prms = new List<double>();
            try
            {
                using (TextFieldParser parser = new TextFieldParser(path))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(";");
                    int iRow = 0;
                    while (!parser.EndOfData)
                    {
                        iRow++;
                                                string[] fields = parser.ReadFields();
                        if (iRow == 1) continue;
                        foreach (string field in fields)
                        {
                            double value = ToDouble(field);
                            m_Prms.Add(value);
                        }                        
                    }
                }
            }
            catch (System.IO.FileNotFoundException)
            {
            }            
        }
    }
}
