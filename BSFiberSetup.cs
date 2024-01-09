using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;

namespace BSFiberConcrete
{
    public partial class BSFiberSetup : Form
    {
        public BeamSection BeamSection { get; set; }

        public BSFiberSetup()
        {
            InitializeComponent();            
        }

        private void InitSetupTable()
        {
            List<Elements> records = new List<Elements>();
            try
            {
                using (var streamreader = new StreamReader(BSFiberLoadData.FiberConcretePath))
                {
                    CultureInfo culture = CultureInfo.InvariantCulture;
                    IReaderConfiguration config = new CsvConfiguration(culture) { Delimiter = ";" };

                    using (var csv = new CsvReader(streamreader, culture))
                    {
                        //records = csv.GetRecords<Elements>().ToList();
                    }
                }
            }
            catch (CsvHelper.HeaderValidationException)
            {
                MessageBox.Show("Неверный формат файла");
            }
            finally
            {
                if (BeamSection == BeamSection.Ring) 
                {
                    records.Add(new Elements { Rfbtn = 224, B = 30, Yb1 = 0.9, Ybs = 1, Yft = 1.3 });
                }
                else
                    records.Add(new Elements { Rfbtn = 1, B = 30, Yb1 = 1, Ybs = 2, Yft = 3 });

                dataGridView1.DataSource = records;                
            }
        }

        private void BSFiberSetup_Load(object sender, EventArgs e)
        {
            //InitSetupTable();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            InitSetupTable();
        }
    }
}
