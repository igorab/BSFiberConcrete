using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;
using System.Drawing;
using BSFiberConcrete.Lib;

namespace BSFiberConcrete
{
    public partial class BSFiberSetup : Form
    {
        public List<Elements> Records { get; set; }
        public BeamSection BeamSection { get; set; }

        public int TabPageIdx { get; set; }
        public BSFiberSetup()
        {
            InitializeComponent();            
        }

        private void InitSetupTable()
        {
            Records = new List<Elements>();
            try
            {
                comboBetonType.DataSource = BSData.LoadBetonTypeName();

                string i_b = Convert.ToString(comboBox_i.Text);
                dataGridElements.DataSource =  BSData.LoadFiberConcreteTable(i_b);

                dataGridCoeffs.DataSource = BSData.LoadCoeffs();

                dataGridBeton.DataSource = BSData.LoadBetonData();

                //using (var streamreader = new StreamReader(BSFiberLoadData.FiberConcretePath))
                //{
                //    CultureInfo culture = CultureInfo.InvariantCulture;
                //    IReaderConfiguration config = new CsvConfiguration(culture) { Delimiter = ";" };
                //    using (var csv = new CsvReader(streamreader, culture))
                //    {
                //        records = csv.GetRecords<Elements>().ToList();
                //    }
                //}
            }
            catch (CsvHelper.HeaderValidationException)
            {
                MessageBox.Show("Неверный формат файла");
            }
            catch (Exception _ex) 
            {
                MessageBox.Show(_ex.ToString() + "\n" + _ex.Message);
            }
            finally
            {
                /*
                if (BeamSection == BeamSection.Ring) 
                {
                    Records.Add(BSFiberLib.PhysElements);
                }
                else
                    Records.Add(BSFiberLib.PhysElements);

                dataGridElements.DataSource = Records;                
                */
            }
        }
      
        private void CreateMyListView(ListView _listView, Rebar _rebar)
        {
            ListView listView = _listView;
            
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
            //listView.Sorting = SortOrder.Ascending;

            ListViewItem[] items = new ListViewItem[9];
            int idx = -1;
            // Create items and sets of subitems for each item.
            items[++idx] = new ListViewItem("Cls", idx);
            items[idx].Checked = true;
            items[idx].SubItems.Add("");
            items[idx].SubItems.Add("Класс арматуры");
            items[idx].SubItems.Add("");

            items[++idx] = new ListViewItem("Rs", idx);
            items[idx].Checked = true;
            items[idx].SubItems.Add(_rebar.Rs.ToString());
            items[idx].SubItems.Add("Расчетное сопротивление продольной арматуры");
            items[idx].SubItems.Add("кг/см2");

            items[++idx] = new ListViewItem("As", idx);
            items[idx].Checked = true;
            items[idx].SubItems.Add(_rebar.As.ToString());
            items[idx].SubItems.Add("Площадь растянутой арматуры");
            items[idx].SubItems.Add("см2");

            items[++idx] = new ListViewItem("Rsw", idx);
            items[idx].Checked = true;
            items[idx].SubItems.Add(_rebar.Rsw.ToString());
            items[idx].SubItems.Add("Расчетное сопротивление поперечной арматуры");
            items[idx].SubItems.Add("кг/см2");

            items[++idx] = new ListViewItem("Asw", idx);
            items[idx].Checked = true;
            items[idx].SubItems.Add(_rebar.Asw.ToString());
            items[idx].SubItems.Add("Площадь арматуры");
            items[idx].SubItems.Add("см2");

            items[++idx] = new ListViewItem("s_w", idx);
            items[idx].Checked = true;
            items[idx].SubItems.Add(_rebar.s_w.ToString());
            items[idx].SubItems.Add("Шаг попреречной арматуры");
            items[idx].SubItems.Add("см");

            items[++idx] = new ListViewItem("a", idx);
            items[idx].Checked = true;
            items[idx].SubItems.Add(_rebar.a.ToString());
            items[idx].SubItems.Add("Расст до ц.т. растянутой арм");
            items[idx].SubItems.Add("см");

            items[++idx] = new ListViewItem("a1", idx);
            items[idx].Checked = true;
            items[idx].SubItems.Add(_rebar.a1.ToString());
            items[idx].SubItems.Add("Расст до ц.т. сжатой арм");
            items[idx].SubItems.Add("см");

            items[++idx] = new ListViewItem("k_s", idx);
            items[idx].Checked = true;
            items[idx].SubItems.Add(_rebar.k_s.ToString());
            items[idx].SubItems.Add("Влияние длительности действия нагрузки");
            items[idx].SubItems.Add("");

            // idx == 9
            // Create columns for the items and subitems.
            // Width of -2 indicates auto-size.
            listView.Columns.Add("Параметр", 100, HorizontalAlignment.Left);
            listView.Columns.Add("Значение", 100, HorizontalAlignment.Left);
            listView.Columns.Add("Описание", -2, HorizontalAlignment.Left);
            listView.Columns.Add("Ед. изм.", 100, HorizontalAlignment.Center);

            //Add the items to the ListView.
            listView.Items.AddRange(items);
        }

        private void BSFiberSetup_Load(object sender, EventArgs e)
        {
            try
            {
                InitSetupTable();

                InitRebarSetup();

                comboBox_i.SelectedIndex = 0;
                comboBetonType.SelectedIndex = 0;
                cmbRebarClass.SelectedIndex = 1;

                if (tabFiber.TabPages.Count > TabPageIdx)
                    tabFiber.SelectedTab = tabFiber.TabPages[TabPageIdx]; 
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox_i_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                string i_b = Convert.ToString(comboBox_i.Text);
                dataGridElements.DataSource = BSData.LoadFiberConcreteTable(i_b);
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
        }

        private void comboBetonType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idx = comboBetonType.SelectedIndex;
                var bt = Lib.BSQuery.BetonTypeFind(idx);
                num_eps_fb2.Value = (decimal)bt.Eps_fb2;
                num_omega.Value = (decimal)bt.Omega;
            }
            catch
            {
            }
        }

        private void cmbRebarClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lvRebar.Items[0].Text = cmbRebarClass.Text;
                var rb = Lib.BSQuery.RebarFind(cmbRebarClass.Text);
                lvRebar.Items[1].SubItems[1].Text = Convert.ToString(rb.Rs);
            }
            catch 
            { 
            }
        }
    }
}
