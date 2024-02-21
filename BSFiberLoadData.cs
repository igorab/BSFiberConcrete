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
        public static string FiberConcretePath { get { return Path.Combine(Environment.CurrentDirectory, "Templates\\FiberConcrete.csv"); } }

        private List<double> m_Prms = new List<double>();

        public double[] Params { get { return m_Prms.ToArray(); } }

        // serialized from Json
        private BSFiberParams m_FiberParams;
        // Фибробетон
        public Fiber Fiber { get { return m_FiberParams?.Fiber; } }
        // Арматура фибробетона
        public Rebar Rebar { get { return m_FiberParams?.Rebar; } }
        // Стержни арматурные
        public Rod2 Rod2 { get { return m_FiberParams?.Rod2; } }

        // Бетон, железобетон
        public Beton2 Beton2 { get { return m_FiberParams?.Beton2; } }

        private double to_double(string _num)
        {
            NumberFormatInfo formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
            double d_num;
            try
            {
                d_num = Convert.ToDouble(_num, formatter);
            }
            catch (System.FormatException)
            {
                formatter.NumberDecimalSeparator = ",";
                d_num = Convert.ToDouble(_num, formatter);
            }
            return d_num;
        }
        
        public void ReadParamsFromJson()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "Templates\\BSFiberParams.json");
            m_FiberParams = new BSFiberParams();

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                m_FiberParams = JsonSerializer.Deserialize<BSFiberParams>(fs);
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
