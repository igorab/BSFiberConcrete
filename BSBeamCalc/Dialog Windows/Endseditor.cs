using System;
using System.Windows.Forms;

namespace CBAnsDes
{
    public partial class Ends_Editor
    {
        private ToolTip t = new ToolTip();
        public Ends_Editor()
        {

            // This call is required by the Windows Form Designer.
            InitializeComponent();

            // Add any initialization after the InitializeComponent() call.

        }

        // ---Check Option Free - Free
        private void Ends_Editor_Load(object sender, EventArgs e)
        {
            if (Indexes.mem.Count < 3)
            {
                RadioButton4.Enabled = false;
            }
            if (Indexes.mem.Count < 2)
            {
                RadioButton6.Enabled = false;
            }
        }

        private void Button_ok_Click(object sender, EventArgs e)
        {

            Close();

            if (RadioButton1.Checked == true)
            {
                Indexes.ends = 1;         // Fixed-Fixed
            }
            else if (RadioButton2.Checked == true)
            {
                Indexes.ends = 2;         // Fixed-Free
            }
            else if (RadioButton3.Checked == true)
            {
                Indexes.ends = 3;         // Pinned-Pinned
            }
            else if (RadioButton4.Checked == true)
            {
                if (Indexes.mem.Count < 3)
                {
                    return;
                }
                Indexes.ends = 4;         // Free-Free
            }
            else if (RadioButton5.Checked == true)
            {
                Indexes.ends = 5;         // Fixed-Pinned
            }
            else if (RadioButton6.Checked == true) // pinned-free
            {
                if (Indexes.mem.Count < 2)
                {
                    return;
                }
                Indexes.ends = 6;
            }
            My.MyProject.Forms.MDIMain.Focus();
            My.MyProject.Forms.beamcreate.mainpic.Refresh();
        }

        private void Button_cancel_Click(object sender, EventArgs e)
        {
            Close();
            My.MyProject.Forms.MDIMain.Focus();
            My.MyProject.Forms.beamcreate.mainpic.Refresh();
        }
    }
}