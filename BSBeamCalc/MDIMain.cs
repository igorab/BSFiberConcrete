using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace CBAnsDes
{
    public partial class MDIMain
    {
        public MDIMain()
        {
            InitializeComponent();
        }

        #region New Menu

        private void ShowNewForm(object sender, EventArgs e)
        {
            string msgrslt;
            if (Indexes.mem.Count != 0)
            {
                CreateToolStripMenuItem_Click(sender, e);
                msgrslt = ((int)MessageBox.Show("Do you want to save your Job ?", "Samson Mano", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)).ToString();
                if (Conversions.ToDouble(msgrslt) == (double)DialogResult.Yes)
                {
                    SaveAsToolStripMenuItem_Click(sender, e);
                }
                else if (Conversions.ToDouble(msgrslt) == (double)DialogResult.No)
                {
                    goto x1;
                }
                else
                {
                    My.MyProject.Forms.beamcreate.mainpic.Refresh();
                    return;
                }

            }

        x1:
            ;

            Indexes.mem.Clear();
            var Frm = new Newapp(false);
            Frm.ShowInTaskbar = false;
            Frm.ShowDialog();

            if (Frm.F != true)
            {
                ToolStrip1.Visible = false;
                EditMenu.Enabled = false;
                ComputeToolStripMenuItem.Enabled = false;
                ViewMenu.Enabled = false;
                ToolsMenu.Enabled = false;
                My.MyProject.Forms.beamcreate.Visible = false;
                My.MyProject.Forms.beamcreate.mainpic.Refresh();
                return;
            }

            // Make it a child of this MDI form before showing it.
            My.MyProject.Forms.beamcreate.MdiParent = this;
            My.MyProject.Forms.beamcreate.Text = Frm.TextBox1.Text;
            My.MyProject.Forms.beamcreate.Dock = DockStyle.Fill;
            My.MyProject.Forms.beamcreate.Show();

            ToolStrip1.Visible = true;
            ToolStripButton1.Enabled = true;
            AddMemberToolStripMenuItem.Enabled = true;
            ToolStripButton2.Enabled = true;
            BeamEndsToolStripMenuItem.Enabled = true;
            ToolStripButton3.Enabled = false;
            editMemberToolStripMenuItem.Enabled = false;
            ToolStripButton4.Enabled = false;
            RemoveMemberToolStripMenuItem.Enabled = false;
            ToolStripButton5.Enabled = false;
            AddLoadToolStripMenuItem.Enabled = false;
            ToolStripButton6.Enabled = false;
            RemoveLoadsToolStripMenuItem.Enabled = false;
            ToolStripButton7.Enabled = false;
            ModifyLoadToolStripMenuItem.Enabled = false;
            ToolStripButton8.Enabled = false;
            RemoveLoadToolStripMenuItem.Enabled = false;

            EditMenu.Enabled = true;
            ComputeToolStripMenuItem.Enabled = true;
            ViewMenu.Enabled = true;
            ToolsMenu.Enabled = true;
            CreateToolStripMenuItem_Click(sender, e);
            CreateToolStripMenuItem.Checked = true;
            My.MyProject.Forms.beamcreate.mainpic.Refresh();
            WindowState = FormWindowState.Maximized;
        }

        private void OpenFile(object sender, EventArgs e)
        {
            string msgrslt;
            CreateToolStripMenuItem_Click(sender, e);
            CreateToolStripMenuItem.Checked = true;
            if (Indexes.mem.Count != 0)
            {
                msgrslt = ((int)MessageBox.Show("Do you want to save your Job ?", "Samson Mano", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)).ToString();
                if (Conversions.ToDouble(msgrslt) == (double)DialogResult.Yes)
                {
                    SaveAsToolStripMenuItem_Click(sender, e);
                }
                else if (Conversions.ToDouble(msgrslt) == (double)DialogResult.No)
                {
                    //goto x1;
                }
                else
                {
                    My.MyProject.Forms.beamcreate.mainpic.Refresh();
                    return;
                }
            }
            else
            {
            //x1:
                ;

                Indexes.mem.Clear();
                My.MyProject.Forms.beamcreate.mainpic.Refresh();

                My.MyProject.Forms.beamcreate.Visible = false;
                ToolStrip1.Visible = false;
                ToolStrip2.Visible = false;
                EditMenu.Enabled = false;
                ComputeToolStripMenuItem.Enabled = false;
                ViewMenu.Enabled = false;
                ToolsMenu.Enabled = false;

                var ofd = new OpenFileDialog();
                ofd.DefaultExt = ".cbf";
                ofd.Filter = "Samson Mano's continuous beam Object Files (*.cbf)|*.cbf";
                ofd.ShowDialog();
                if (File.Exists(ofd.FileName))
                {
                    var cbobject = new List<object>();
                    Stream gsf = File.OpenRead(ofd.FileName);
                    var deserializer = new BinaryFormatter();
                    try
                    {
                        cbobject = (List<object>)deserializer.Deserialize(gsf);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Unable to Open !!!", "Samson Mano", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    List<Member> instance1 = (List<Member>)cbobject[0];
                    Indexes.mem = instance1;
                    int instance2 = Conversions.ToInteger(cbobject[1]);
                    Indexes.ends = instance2;
                    // ----Call For ALL
                    My.MyProject.Forms.beamcreate.MdiParent = this;
                    My.MyProject.Forms.beamcreate.Text = ofd.FileName;
                    My.MyProject.Forms.beamcreate.Dock = DockStyle.Fill;
                    My.MyProject.Forms.beamcreate.Show();

                    ToolStrip1.Visible = true;
                    ToolStripButton1.Enabled = true;
                    AddMemberToolStripMenuItem.Enabled = true;
                    ToolStripButton2.Enabled = true;
                    BeamEndsToolStripMenuItem.Enabled = true;
                    // igorab
                    //ToolStripButton3.Enabled = false;
                    //editMemberToolStripMenuItem.Enabled = false;
                    //ToolStripButton4.Enabled = false;
                    //RemoveMemberToolStripMenuItem.Enabled = false;
                    //ToolStripButton5.Enabled = false;
                    //AddLoadToolStripMenuItem.Enabled = false;
                    //ToolStripButton6.Enabled = false;
                    //RemoveLoadsToolStripMenuItem.Enabled = false;
                    //ToolStripButton7.Enabled = false;
                    //ModifyLoadToolStripMenuItem.Enabled = false;
                    //ToolStripButton8.Enabled = false;
                    //RemoveLoadToolStripMenuItem.Enabled = false;

                    EditMenu.Enabled = true;
                    ComputeToolStripMenuItem.Enabled = true;
                    ViewMenu.Enabled = true;
                    ToolsMenu.Enabled = true;
                    CreateToolStripMenuItem_Click(sender, e);
                    CreateToolStripMenuItem.Checked = true;
                    My.MyProject.Forms.beamcreate.mainpic.Refresh();
                    WindowState = FormWindowState.Maximized;
                    gsf.Close();
                }
                else
                {
                    // MessageBox.Show("File does not exist!")
                }
                My.MyProject.Forms.beamcreate.mainpic.Refresh();
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (Indexes.mem.Count != 0)
            {
                CreateToolStripMenuItem_Click(sender, e);
                CreateToolStripMenuItem.Checked = true;
                var sfd = new SaveFileDialog();
                sfd.DefaultExt = ".cbf";
                sfd.Filter = "Samson Mano's continuous beam Object Files (*.cbf)|*.cbf";
                sfd.ShowDialog();

                if (!string.IsNullOrEmpty(sfd.FileName))
                {
                    var cbobject = new List<object>();

                    cbobject.Add(Indexes.mem);
                    cbobject.Add(Indexes.ends);
                    Stream psf = File.Create(sfd.FileName);
                    var serializer = new BinaryFormatter();
                    serializer.Serialize(psf, cbobject);
                    psf.Close();
                }
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region Edit Menu

        #endregion

        #region View Menu
        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStrip.Visible = ToolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StatusStrip.Visible = StatusBarToolStripMenuItem.Checked;
        }
        #endregion

        #region Toolstrip1
        private void ToolStripButton1_Click(object sender, EventArgs e)
        {
            // --Add Member
            var a = new addmember();
            a.Text = "Add Member";
            a.ShowDialog();
        }

        private void ToolStripButton2_Click(object sender, EventArgs e)
        {
            // --Edit Ends
            var a = new Ends_Editor();
            a.ShowDialog();
        }

        private void ToolStripButton3_Click(object sender, EventArgs e)
        {
            // --Edit Member
            var a = new addmember();
            a.Text = "Modify Member";
            a.TextBox1.Text = Indexes.mem[Indexes.selline].spanlength.ToString();
            a.TextBox2.Text = Indexes.mem[Indexes.selline].Emodulus.ToString();
            a.TextBox3.Text = Indexes.mem[Indexes.selline].Inertia.ToString();
            a.TextBox6.Text = Indexes.mem[Indexes.selline].g.ToString();
            a.ShowDialog();
        }

        private void ToolStripButton4_Click(object sender, EventArgs e)
        {
            // --Remove Member
            if (Indexes.ends == 4 & Indexes.mem.Count == 3)
            {
                return;
            }
            else if (Indexes.ends == 6 & Indexes.mem.Count == 2)
            {
                return;
            }
            if (Indexes.mem.Count != 1)
            {
                Indexes.mem.RemoveAt(Indexes.selline);
                Indexes.selline = -1;
                My.MyProject.Forms.beamcreate.toolstrip1Mod();
                My.MyProject.Forms.beamcreate.mainpic.Refresh();
            }
        }

        private void ToolStripButton5_Click(object sender, EventArgs e)
        {
            // --Add Loads
            var a = new LoadWindow(Indexes.mem[Indexes.selline].spanlength, Indexes.selline);
            a.ShowDialog();
        }

        private void ToolStripButton6_Click(object sender, EventArgs e)
        {
            // --Remove Loads
            Indexes.mem[Indexes.selline].Pload.Clear();
            Indexes.mem[Indexes.selline].Uload.Clear();
            Indexes.mem[Indexes.selline].Mload.Clear();
            My.MyProject.Forms.beamcreate.mainpic.Refresh();
        }

        private void ToolStripButton7_Click(object sender, EventArgs e)
        {
            // --Edit Load
            if (My.MyProject.Forms.beamcreate.tipe == 1)
            {
                var a = new LoadWindow(Indexes.mem[Indexes.Lselline].spanlength, Indexes.Lselline, My.MyProject.Forms.beamcreate.selPL);
                a.ShowDialog();
            }
            else if (My.MyProject.Forms.beamcreate.tipe == 2)
            {
                var a = new LoadWindow(Indexes.mem[Indexes.Lselline].spanlength, Indexes.Lselline, My.MyProject.Forms.beamcreate.selUL);
                a.ShowDialog();
            }
            else if (My.MyProject.Forms.beamcreate.tipe == 3)
            {
                var a = new LoadWindow(Indexes.mem[Indexes.Lselline].spanlength, Indexes.Lselline, My.MyProject.Forms.beamcreate.selML);
                a.ShowDialog();
            }
            My.MyProject.Forms.beamcreate.mainpic.Refresh();
        }

        private void ToolStripButton8_Click(object sender, EventArgs e)
        {
            // --Single Load is removed
            if (My.MyProject.Forms.beamcreate.tipe == 1)
            {
                Indexes.mem[Indexes.Lselline].Pload.Remove(My.MyProject.Forms.beamcreate.selPL);
            }
            else if (My.MyProject.Forms.beamcreate.tipe == 2)
            {
                Indexes.mem[Indexes.Lselline].Uload.Remove(My.MyProject.Forms.beamcreate.selUL);
            }
            else if (My.MyProject.Forms.beamcreate.tipe == 3)
            {
                Indexes.mem[Indexes.Lselline].Mload.Remove(My.MyProject.Forms.beamcreate.selML);
            }
            // --Null selected load
            var ptm = new Member.P();
            var utm = new Member.U();
            var mtm = new Member.M();
            My.MyProject.Forms.beamcreate.selPL = ptm;
            My.MyProject.Forms.beamcreate.selUL = utm;
            My.MyProject.Forms.beamcreate.selML = mtm;
            Indexes.selline = -1;
            Indexes.Lselline = -1;
            My.MyProject.Forms.beamcreate.toolstrip1Mod();
            My.MyProject.Forms.beamcreate.mainpic.Refresh();
        }
        #endregion

        #region Compute Menu
        private void CreateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AnalyzeToolStripMenuItem.Checked = false;
            CreateToolStripMenuItem.Checked = true;
            DesignToolStripMenuItem.Checked = false;
            My.MyProject.Forms.beamcreate.mainpic.Visible = true;
            My.MyProject.Forms.beamcreate.respic.Visible = false;
            DesignToolStripMenuItem.Visible = false;
            ResultToolStripMenuItem.Visible = false;
            EditMenu.Enabled = true;
            ToolStrip1.Visible = true;
            ToolStrip2.Visible = false;
            My.MyProject.Forms.beamcreate.mainpic.Refresh();
            My.MyProject.Forms.beamcreate.respic.Refresh();
            ToolStripLabel1.Text = "100%";
            ToolStripLabel2.Text = "100%";
        }

        private void AnalyzeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var m in Indexes.mem)
            {
                if (m.Pload.Count != 0 | m.Uload.Count != 0 | m.Mload.Count != 0)
                {
                    // ------Analysis Part
                    // Try
                    FEAnalyzer.ContinuousBeam_analyzer();
                    // Catch ex As Exception
                    // MsgBox("Error Analysis .....                   " & vbNewLine & "Please Report Bug" & vbNewLine & "..................................................", MsgBoxStyle.OkOnly, "Bug")
                    // CreateToolStripMenuItem_Click(sender, e)
                    // Exit Sub
                    // End Try

                    DesignToolStripMenuItem.Checked = false;
                    AnalyzeToolStripMenuItem.Checked = true;
                    CreateToolStripMenuItem.Checked = false;
                    My.MyProject.Forms.beamcreate.mainpic.Visible = false;
                    My.MyProject.Forms.beamcreate.respic.Visible = true;
                    DesignToolStripMenuItem.Visible = true;
                    ResultToolStripMenuItem.Visible = true;
                    ToolStrip1.Visible = false;
                    ToolStrip2.Visible = true;
                    EditMenu.Enabled = false;
                    My.MyProject.Forms.beamcreate.mainpic.Refresh();
                    My.MyProject.Forms.beamcreate.respic.Refresh();
                    ToolStripLabel1.Text = 100.ToString();
                    ToolStripLabel2.Text = 100.ToString();

                    BendingMomentDiagramToolStripMenuItem_Click(sender, e);
                    return;
                }
                else
                {
                    continue;
                }
            }
            Interaction.MsgBox("No valid Load type is found !!!!!!!!", MsgBoxStyle.OkOnly, "No Load");
            CreateToolStripMenuItem_Click(sender, e);
        }

        private void DesignToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateToolStripMenuItem.Checked = false;
            AnalyzeToolStripMenuItem.Checked = false;
        }

        private void ShearForceDiagramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BendingMomentDiagramToolStripMenuItem.Checked == true | DeflectionToolStripMenuItem.Checked == true | SlopeToolStripMenuItem.Checked == true)
            {
                BendingMomentDiagramToolStripMenuItem.Checked = false;
                DeflectionToolStripMenuItem.Checked = false;
                SlopeToolStripMenuItem.Checked = false;
                ShearForceDiagramToolStripMenuItem.Checked = true;
                My.MyProject.Forms.beamcreate.respic.Refresh();
            }
        }

        private void BendingMomentDiagramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ShearForceDiagramToolStripMenuItem.Checked == true | DeflectionToolStripMenuItem.Checked == true | SlopeToolStripMenuItem.Checked == true)
            {
                ShearForceDiagramToolStripMenuItem.Checked = false;
                DeflectionToolStripMenuItem.Checked = false;
                BendingMomentDiagramToolStripMenuItem.Checked = true;
                SlopeToolStripMenuItem.Checked = false;
                My.MyProject.Forms.beamcreate.respic.Refresh();
            }
        }

        private void DeflectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BendingMomentDiagramToolStripMenuItem.Checked == true | ShearForceDiagramToolStripMenuItem.Checked == true | SlopeToolStripMenuItem.Checked == true)
            {
                BendingMomentDiagramToolStripMenuItem.Checked = false;
                ShearForceDiagramToolStripMenuItem.Checked = false;
                DeflectionToolStripMenuItem.Checked = true;
                SlopeToolStripMenuItem.Checked = false;
                My.MyProject.Forms.beamcreate.respic.Refresh();
            }
        }
        private void SlopeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BendingMomentDiagramToolStripMenuItem.Checked == true | ShearForceDiagramToolStripMenuItem.Checked == true | DeflectionToolStripMenuItem.Checked == true)
            {
                BendingMomentDiagramToolStripMenuItem.Checked = false;
                ShearForceDiagramToolStripMenuItem.Checked = false;
                DeflectionToolStripMenuItem.Checked = false;
                SlopeToolStripMenuItem.Checked = true;
                My.MyProject.Forms.beamcreate.respic.Refresh();
            }
        }
        #endregion


        private void MDIMain_Load(object sender, EventArgs e)
        {
            ToolStrip1.Visible = false;
            ToolStrip2.Visible = false;
            EditMenu.Enabled = false;
            ComputeToolStripMenuItem.Enabled = false;
            ViewMenu.Enabled = false;
            ToolsMenu.Enabled = false;
            // ---Scene
            // Me.Visible = False
            // logo.ShowDialog()
            // Me.Visible = True
        }

        private void GeneralInstructionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            My.MyProject.Forms.GeneralInstruction.ShowDialog();
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            My.MyProject.Forms.Aboutus.ShowDialog();
        }

        private void memdetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FEAnalyzer.ContinuousBeam_analyzer(true);
        }

        private void CalculatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var p = new Process();
            p.StartInfo.FileName = "calc.exe";
            p.Start();
        }

        private void PrintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // PrintDocument1.DefaultPageSettings.PaperSource.Kind = "Custom"



            // PrintDocument1.DefaultPageSettings.PaperSize.Height = 21 'PrintDocument1.DefaultPageSettings.PaperSize.Width = 10 PrintPreviewDialog1.Document = PrintDocument1



            PrintPreviewDialog1.ShowDialog();


        }

        private void PrintDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {


        }

        private void ToolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}