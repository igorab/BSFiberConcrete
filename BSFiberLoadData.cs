using System;
using System.Globalization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;
using System.Runtime.ExceptionServices;
using System.Runtime.Serialization;

namespace BSFiberConcrete
{
    internal class BSFiberLoadData
    {
        private List<double> m_Prms = new List<double>();

        public double[] Prms { get { return m_Prms.ToArray(); } }


        public class Elements
        {
            public double Rfbt { get; set; }
            public double Yft { get; set; }
            public double Yb1 { get; set; }
            public double Yb5 { get; set; }
        }

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
