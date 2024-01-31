using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;
using System.Drawing;

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

      
        private void CreateMyListView(ListView _listView, Rebar _rebar)
        {
            // Create a new ListView control.
            ListView listView = _listView;

            //listView1.Bounds = new Rectangle(new Point(10, 10), new Size(300, 200));

            // Set the view to show details.
            listView.View = View.Details;
            // Allow the user to edit item text.
            listView.LabelEdit = true;
            // Allow the user to rearrange columns.
            listView.AllowColumnReorder = true;
            // Display check boxes.
            listView.CheckBoxes = true;
            // Select the item and subitems when selection is made.
            listView.FullRowSelect = true;
            // Display grid lines.
            listView.GridLines = true;
            // Sort the items in the list in ascending order.
            listView.Sorting = SortOrder.Ascending;

            // Create items and sets of subitems for each item.
            ListViewItem item1 = new ListViewItem("Rs", 0);            
            item1.Checked = true;
            item1.SubItems.Add(_rebar.Rs.ToString());
            item1.SubItems.Add("Расчетное сопротивление продольной арматуры");
            item1.SubItems.Add("кг/см2");

            ListViewItem item2 = new ListViewItem("As", 1);
            item2.Checked = true;
            item2.SubItems.Add(_rebar.As.ToString());
            item2.SubItems.Add("Площадь растянутой арматуры");
            item2.SubItems.Add("см2");

            ListViewItem item3 = new ListViewItem("Rsw", 0);            
            item3.Checked = true;
            item3.SubItems.Add(_rebar.Rsw.ToString());
            item3.SubItems.Add("Расчетное сопротивление поперечной арматуры");
            item3.SubItems.Add("кг/см2");

            ListViewItem item4 = new ListViewItem("Asw", 0);
            item4.Checked = true;
            item4.SubItems.Add(_rebar.Asw.ToString());
            item4.SubItems.Add("Площадь арматуры");
            item4.SubItems.Add("см2");

            ListViewItem item5 = new ListViewItem("s_w", 0);
            item5.Checked = true;
            item5.SubItems.Add(_rebar.s_w.ToString());
            item5.SubItems.Add("Шаг попреречной арматуры");
            item5.SubItems.Add("см");

            // Create columns for the items and subitems.
            // Width of -2 indicates auto-size.
            listView.Columns.Add("Параметр", 100, HorizontalAlignment.Left);
            listView.Columns.Add("Значение", 100, HorizontalAlignment.Left);
            listView.Columns.Add("Описание", -2, HorizontalAlignment.Left);
            listView.Columns.Add("Ед. изм.", 100, HorizontalAlignment.Center);

            //Add the items to the ListView.
            listView.Items.AddRange(new ListViewItem[] { item1, item2, item3, item4, item5 });            
        }

        private void BSFiberSetup_Load(object sender, EventArgs e)
        {
            InitSetupTable();

            InitRebarSetup();
        }

        private void InitRebarSetup()
        {
            BSFiberLoadData bSFiberLoadData = new BSFiberLoadData();
            bSFiberLoadData.ReadParamsFromJson();
            lvRebar.Items.Clear();
            lvRebar.Columns.Clear();
            CreateMyListView(lvRebar, bSFiberLoadData.Rebar);
        }


        private void btnLoad_Click(object sender, EventArgs e)
        {
            InitSetupTable();

            InitRebarSetup();
        }
    }
}
