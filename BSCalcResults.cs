using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BSFiberConcrete
{
    public partial class BSCalcResults : Form
    {
        public Dictionary<string, double> CalcResults { get; set; }

        public BSCalcResults()
        {
            CalcResults = new Dictionary<string, double>();

            InitializeComponent();
        }

        private void CreateMyListView(ListView _listView)
        {
            ListView listView = _listView;
            ListViewItem[] items = new ListViewItem[0];

            listView.View = View.Details;
            listView.LabelEdit = true;            
            listView.AllowColumnReorder = true;            
            listView.CheckBoxes = true;            
            listView.FullRowSelect = true;
            listView.GridLines = true;            
            listView.Sorting = SortOrder.Ascending;

            if (CalcResults != null)
            {
                items = new ListViewItem[CalcResults.Count];

                int idx = -1;

                foreach (var item in CalcResults)
                {
                    items[++idx] = new ListViewItem(item.Key, 0);
                    items[idx].Checked = true;
                    items[idx].SubItems.Add(item.Value.ToString());
                    items[idx].SubItems.Add(item.Key);
                    items[idx].SubItems.Add("кг/см2");

                }
            }

            listView.Columns.Add("Параметр", 500, HorizontalAlignment.Left);
            listView.Columns.Add("Значение", 200, HorizontalAlignment.Left);

            //Add the items to the ListView.
            listView.Items.AddRange(items);
        }


        private void BSCalcResults_Load(object sender, EventArgs e)
        {
            try
            {
                CreateMyListView(lvResults);
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
