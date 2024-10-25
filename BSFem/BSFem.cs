using BSCalcLib;
using netDxf.Header;
using netDxf;
using netDxf.IO;
using netDxf.Entities;

namespace BSFem
{
    public partial class BSFem : Form
    {
        public BSFem()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            var Z = FEMSolverODE.RunFromCode();

            if (Z != null)
            {
                textBox1.Text = string.Join("\t\n", Z);
            }
        }

        private void btnMesh_Click(object sender, EventArgs e)
        {
            BSCalcLib.BSMesh.GenerateTest();
        }

        /// <summary>
        /// Контур
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPoly_Click(object sender, EventArgs e)
        {
            BSCalcLib.TriPoly.Example();
        }

        /// <summary>
        /// Сегмент
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSegment_Click(object sender, EventArgs e)
        {
            BSCalcLib.TriSegment.Example();
        }

        public void OpenDocument()
        {
            // your DXF file name
            string file = "sample.dxf";

            // create a new document, by default it will create an AutoCad2000 DXF version
            DxfDocument doc = new DxfDocument();
            // an entity
            Line entity = new Line(new Vector2(5, 5), new Vector2(10, 5));
            // add your entities here
            doc.Entities.Add(entity);
            // save to file
            doc.Save(file);

            // this check is optional but recommended before loading a DXF file
            DxfVersion dxfVersion = DxfDocument.CheckDxfFileVersion(file);
            // netDxf is only compatible with AutoCad2000 and higher DXF versions
            if (dxfVersion < DxfVersion.AutoCad2000) return;
            // load file
            DxfDocument loaded = DxfDocument.Load(file);
        }


        private void btnDxf_Click(object sender, EventArgs e)
        {
            var res = openFileDialog.ShowDialog(this);

            if (res == DialogResult.OK)             
            {
                OpenDocument();
            }           
        }
    }
}
