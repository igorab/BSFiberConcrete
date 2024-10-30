using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace BSFiberConcrete
{
    public partial class CalcCrackingForm : Form
    {
        public CalcCrackingForm(DataTable resultTable)
        {
            InitializeComponent();
            if (resultTable != null)
            {
                dataGridView1.DataSource = resultTable;
                
                dataGridView1.Columns[resultTable.Columns[0].Caption].MinimumWidth = 500;
            }
        }
    }
}
