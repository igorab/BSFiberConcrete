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
        private BSFiberParams m_FiberParams;
        private Dictionary<string, double> m_FibInit;

        // Фибробетон
        public Fiber Fiber { get => m_FiberParams?.Fiber;  }
        // Арматура фибробетона
        public Rebar Rebar { get => m_FiberParams?.Rebar;  }
        // Стержни арматурные
        public Rod2 Rod2 { get => m_FiberParams?.Rod2;}
        
        // Единицы измерения
        public Units Units { get => m_FiberParams?.Units; }

        // Бетон, железобетон
        public Beton2 Beton2 { get => m_FiberParams?.Beton2;  }

        private double to_double(string _num) => BSHelper.ToDouble(_num);
                
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

        public void SaveInitSectionsToJson(Dictionary<string, double>  _sections)
        {
            string path = Path.Combine(Environment.CurrentDirectory, "Templates\\BSInit.json");
            Dictionary<string, double> fibInit = new Dictionary<string, double>(m_FibInit);

            foreach (var _v in _sections)
            {
                fibInit[_v.Key] = _v.Value;
            }

            using (FileStream fs = new FileStream(path, FileMode.Truncate))
            {
                JsonSerializer.Serialize<Dictionary<string, double>>(fs, fibInit);
            }
        }

        public void InitEfforts(ref Dictionary<string, double> m_Iniv)
        {
            m_Iniv = ReadInitFromJson();
            List<Efforts> eff = Lib.BSData.LoadEfforts();
            if (eff.Count > 0)
            {
                m_Iniv["Mx"] = eff[0].Mx;
                m_Iniv["My"] = eff[0].My;
                m_Iniv["N"] = eff[0].N;
                m_Iniv["Qx"] = eff[0].Qx;
                m_Iniv["Qy"] = eff[0].Qy;
            }
        }

        /// <summary>
        /// Параметры по умолчанию
        /// string json = @"{""Mx"":""value1"",""key2"":1}";
        //  var values = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, double> ReadInitFromJson()
        {
            string path = Path.Combine(Environment.CurrentDirectory, @"Templates\BSInit.json");
            var keyValuePairs = new Dictionary<string, double>();            

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, double>>(fs);

                m_FibInit = keyValuePairs;
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
                            double value = to_double(field);
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
