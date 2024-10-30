using System;
using System.Data;
using System.Windows.Forms;

namespace BSFiberConcrete
{
    public partial class BSFactors : Form
    {
        public BSFactors()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BSFactors_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            DataRow dr1;
            DataRow dr2;
            DataRow dr3;
            dt.Columns.Add(new System.Data.DataColumn("Factor", typeof(string)));
            dt.Columns.Add(new System.Data.DataColumn("Percent", typeof(int)));

            dr1 = dt.NewRow();
            dr1[0] = "M";
            dr1[1] = 82;

            dr2 = dt.NewRow();
            dr2[0] = "N";
            dr2[1] = 55;

            dr3 = dt.NewRow();
            dr3[0] = "Q";
            dr3[1] = 70;

            dt.Rows.Add(dr1);
            dt.Rows.Add(dr2);
            dt.Rows.Add(dr3);

            gridFactors.DataSource = dt;
        }       
    }
}
