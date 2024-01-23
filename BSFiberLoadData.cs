using System;
using System.Globalization;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BSFiberConcrete
{    
    public class BSFiberLoadData
    {
        public static string FiberConcretePath { get { return Path.Combine(Environment.CurrentDirectory, "Templates\\FiberConcrete.csv"); } }

        private List<double> m_Prms = new List<double>();

        public double[] Params { get { return m_Prms.ToArray(); } }

        private BSFiberParams m_FiberParams;
        public Fiber Fiber { get { return m_FiberParams.Fiber; } }
        
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
