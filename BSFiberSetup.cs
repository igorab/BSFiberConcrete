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
        public List<Elements> Records { get; set; }

        public BeamSection BeamSection { get; set; }

        public BSFiberSetup()
        {
            InitializeComponent();            
        }

        private void InitSetupTable()
        {
            Records = new List<Elements>();
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
                    Records.Add(BSFiberCocreteLib.PhysElements);
                }
                else
                    Records.Add(BSFiberCocreteLib.PhysElements);

                dataGridView1.DataSource = Records;                
            }
        }

        private void BSFiberSetup_Load(object sender, EventArgs e)
        {
            InitSetupTable();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            InitSetupTable();
        }
    }
}
