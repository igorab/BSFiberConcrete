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
    /// <summary>
    /// Загруженные начальные параметры системы из внешних источников
    /// </summary>
    public class BSFiberLoadData
    {
        private List<double> m_Prms = new List<double>() {0,0,0,0,0,0,0,0,0};
        public double[] Params { get => m_Prms.ToArray(); }

        // serialized from Json
        private BSFiberParams m_FiberParams { get; set; }

        public Dictionary<string, double> FibInitCalcParams {private get; set; }

        // Фибробетон
        public Fiber Fiber { get => m_FiberParams?.Fiber;  }
        // Арматура фибробетона
        public Rebar Rebar { get => m_FiberParams?.Rebar;  }
       
        // Бетон, железобетон
        public Beton2 Beton2 { get => m_FiberParams?.Beton2;  }

        private double ToDouble(string _num) => BSHelper.ToDouble(_num);
                
        /// <summary>
        /// Значения по умолчанию из файла
        /// </summary>
        public void ReadParamsFromJson()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "Templates\\BSFiberParams.json");
            m_FiberParams = new BSFiberParams();

            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                m_FiberParams = JsonSerializer.Deserialize<BSFiberParams>(fs);
            }            
        }

        /// <summary>
        ///  Сохранить пользовательские данные в Json
        /// </summary>
        /// <param name="_sections"></param>
        public void SaveInitSectionsToJson(FileStream _fs = null)
        {
            string path = Path.Combine(Environment.CurrentDirectory, "Templates\\BSInit.json");

            Dictionary<string, double> fibInit = new Dictionary<string, double>(FibInitCalcParams);

            //foreach (KeyValuePair<string, double> _v in fibInit)
            //{
            //    fibInit[_v.Key] = _v.Value;
            //}

            using (FileStream fs = (_fs != null ) ? _fs : new FileStream(path, FileMode.Truncate))
            {
                JsonSerializer.Serialize<Dictionary<string, double>>(fs, fibInit);
            }
        }

        /// <summary>
        /// подтянуть введенные пользователем данные по усилиям из БД
        /// </summary>
        /// <param name="_IniEff"></param>
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

        /// <summary>
        /// Параметры по умолчанию
        /// string json = @"{""Mx"":""value1"",""key2"":1}";
        //  var values = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        ///  Загрузку данных из Excel не используем        
        /// </summary>
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
                        //Processing row
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
