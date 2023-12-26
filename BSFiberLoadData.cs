using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;

namespace BSFiberConcrete
{
    internal class BSFiberLoadData
    {
        public static void Load()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "Templates\\FiberConcrete.csv");
            try
            {
                using (TextFieldParser parser = new TextFieldParser(path))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");
                    while (!parser.EndOfData)
                    {
                        //Processing row
                        string[] fields = parser.ReadFields();
                        foreach (string field in fields)
                        {
                            //TODO: Process field
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
