using System;
using System.Drawing;

namespace CBAnsDes
{
    static class SizeController
    {
        public static void sizemonitor()
        {
            // Dim ht As Single
            // Dim wd As Single
            // ht = beamcreate.coverpic.Height
            // wd = beamcreate.coverpic.Width
            // beamcreate.mainpic.Top = 0
            // beamcreate.mainpic.Height = 1600
            // beamcreate.respic.Top = 0
            // beamcreate.respic.Height = 1600
            // ----Hscrollbar1
            // beamcreate.HScrollBar1.Maximum = (1600 - beamcreate.coverpic.Width)
            // beamcreate.HScrollBar1.Minimum = 50
            // beamcreate.HScrollBar1.Value = beamcreate.HScrollBar1.Maximum / 2
            // beamcreate.mainpic.Left = -(beamcreate.HScrollBar1.Value)
            // beamcreate.respic.Left = -(beamcreate.HScrollBar1.Value)
            // ----VscrollBar
            // beamcreate.VScrollBar1.Maximum = (1600 - ht)
            // beamcreate.VScrollBar1.Minimum = 100
            // beamcreate.VScrollBar1.Value = beamcreate.VScrollBar1.Maximum / 2
            // beamcreate.mainpic.Top = -(beamcreate.VScrollBar1.Value)
            // beamcreate.respic.Top = -(beamcreate.VScrollBar1.Value)
            // beamcreate.MEheight = 1600
            Indexes.Zm = 1d;
            if (My.MyProject.Forms.beamcreate.mainpic.Visible == true)
            {
                Indexes.MidPt = new Point((int)Math.Round(My.MyProject.Forms.beamcreate.mainpic.Width / 2d), (int)Math.Round(My.MyProject.Forms.beamcreate.mainpic.Height / 2d));
            }
            else
            {
                Indexes.MidPt = new Point((int)Math.Round(My.MyProject.Forms.beamcreate.respic.Width / 2d), (int)Math.Round(My.MyProject.Forms.beamcreate.respic.Height / 2d));
            }



            if (My.MyProject.Forms.MDIMain.CreateToolStripMenuItem.Checked == false)
            {
                FEAnalyzer.CoordinateCalculator();
            }

            My.MyProject.Forms.beamcreate.mainpic.Refresh();
            My.MyProject.Forms.beamcreate.respic.Refresh();
        }
    }
}