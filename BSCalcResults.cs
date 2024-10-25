//using Microsoft.Win32;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SaveFileDialog = System.Windows.Forms.SaveFileDialog;

namespace BSFiberConcrete
{
    /// <summary>
    ///  форма -данные для расчета и результаты
    /// </summary>
    public partial class BSCalcResults : Form
    {        
        public Dictionary<string, double> CalcParams {private get; set; }
        public Dictionary<string, double> CalcResults {private get; set; }

        public BSCalcResults()
        {
            CalcResults = new Dictionary<string, double>();

            InitializeComponent();
        }

        private void CreateResultsListView(ListView _listView)
        {
            ListView listView = _listView;
            ListViewItem[] items = new ListViewItem[0];

            listView.View = View.Details;
            listView.LabelEdit = true;            
            listView.AllowColumnReorder = true;            
            //listView.CheckBoxes = true;            
            listView.FullRowSelect = true;
            listView.GridLines = true;            
            //listView.Sorting = SortOrder.Ascending;
            
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

        private void CreateParamsListView(ListView _listView)
        {
            ListView listView = _listView;
            ListViewItem[] items = new ListViewItem[0];

            listView.View = View.Details;
            listView.LabelEdit = true;
            listView.AllowColumnReorder = true;
            //listView.CheckBoxes = true;
            listView.FullRowSelect = true;
            listView.GridLines = true;
            //listView.Sorting = SortOrder.Ascending;

            if (CalcParams != null)
            {
                items = new ListViewItem[CalcParams.Count];

                int idx = -1;

                foreach (var item in CalcParams)
                {
                    items[++idx] = new ListViewItem(item.Key, 0);
                    items[idx].Checked = true;
                    items[idx].SubItems.Add(item.Value.ToString());
                    items[idx].SubItems.Add(item.Key);
                    items[idx].SubItems.Add("кг/см2");
                }
            }
           
            listView.Columns.Add("Параметр", 200, HorizontalAlignment.Left);
            listView.Columns.Add("Значение", 200, HorizontalAlignment.Left);

            //Add the items to the ListView.
            listView.Items.AddRange(items);
        }



        private void BSCalcResults_Load(object sender, EventArgs e)
        {
            try
            {
                CreateParamsListView(lvParams);

                CreateResultsListView(lvResults);                

            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSaveCalc_Click(object sender, EventArgs e)
        {
            Stream myStream;
            SaveFileDialog saveFileDlg = new SaveFileDialog();

            saveFileDlg.Filter = "json files (*.json)|*.json";
            saveFileDlg.FilterIndex = 2;
            saveFileDlg.RestoreDirectory = true;

            try
            {
                if (saveFileDlg.ShowDialog() == DialogResult.OK)
                {
                    if ((myStream = saveFileDlg.OpenFile()) != null)
                    {
                        BSFiberLoadData loadData   = new BSFiberLoadData();
                        loadData.FibInitCalcParams = CalcParams;
                        loadData.SaveInitSectionsToJson((FileStream)myStream);
                        // Code to write the stream goes here.
                        myStream.Close();
                    }
                }
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
        }        
    }
}
