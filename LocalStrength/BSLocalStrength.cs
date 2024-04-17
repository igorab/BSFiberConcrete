using BSFiberConcrete.Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BSFiberConcrete.LocalStrength
{
    public partial class BSLocalStrength : Form
    {
        public BSLocalStrength()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {           
            // CExample excemple = new CExample();
            // System.Reflection.PropertyInfo[] properties =
            // excemple.GetType().GetProperties();
            // FieldInfo[] fields = excemple.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            // foreach (FieldInfo fi in fields)
            //  Console.WriteLine(fi.Name);

            
            //public class CExample
            //{
            //    private string privateValue = "private";
            //    protected string protectedValue = "protected";
            //}
        }

        private void BSLocalStrength_Load(object sender, EventArgs e)
        {
            var ds = new BindingList<LocalStress>(BSData.LoadLocalStress());

            localStressBindingSource.DataSource = ds;
        }

        private void btnPrintReport_Click(object sender, EventArgs e)
        {
            BSLocalStrengthReport strengthReport = new BSLocalStrengthReport();

            if (localStressBindingSource.DataSource is BindingList<LocalStress>)
            {
                var ds = (BindingList<LocalStress>)localStressBindingSource.DataSource;
                strengthReport.DataSource = ds.ToList();
            }

            strengthReport.RunReport();
        }
    }
}
