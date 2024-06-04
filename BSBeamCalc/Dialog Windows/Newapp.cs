using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace CBAnsDes
{
    public partial class Newapp
    {
        private ToolTip t = new ToolTip();
        private bool _F;

        public bool F
        {
            get
            {
                return _F;
            }
            set
            {
                _F = value;
            }
        }


        public Newapp(bool FL)
        {

            // This call is required by the Windows Form Designer.
            InitializeComponent();
            _F = FL;
            // Add any initialization after the InitializeComponent() call.

        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            if (Conversion.Val(TextBox2.Text) == 0d)
            {
                TextBox2.Text = 10.ToString();
            }
        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {
            if (Conversion.Val(TextBox3.Text) == 0d)
            {
                TextBox3.Text = 1.ToString();
            }
            if (TextBox3.Text.Contains("."))
            {
                TextBox3.Text = 1.ToString();
            }
        }

        private void Newapp_Load(object sender, EventArgs e)
        {

        }

        // ---Check Option Free - Free
        private void RadioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (Conversion.Val(TextBox3.Text) < 3d)
            {
                RadioButton1.Checked = true;
            }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBox1.Text))
            {
                TextBox1.Text = "Beam1";
            }
        }

        // ---Check Option Pinned - Free
        private void RadioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (Conversion.Val(TextBox3.Text) < 2d)
            {
                RadioButton1.Checked = true;
            }
        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {
            if (Information.IsNumeric(TextBox4.Text))
            {
            }

            else
            {
                TextBox4.Text = "";
            }
        }

        private void TextBox5_TextChanged(object sender, EventArgs e)
        {
            if (Information.IsNumeric(TextBox5.Text))
            {
            }

            else
            {
                TextBox5.Text = "";
            }
        }

        private void Button_Ok_Click(object sender, EventArgs e)
        {
            if (Conversion.Val(TextBox2.Text) < 0.1d)
            {
                TextBox2.Text = 10.ToString();
                Interaction.MsgBox("Sorry Length limited to 0.1 units");
                return;
            }

            int nos;
            nos = (int)Math.Round(Conversion.Val(TextBox3.Text));
            for (int i = 0, loopTo = nos - 1; i <= loopTo; i++)
            {
                var TEMPmem = new Member();
                TEMPmem.spanlength = (float)(Conversion.Val(TextBox2.Text) / Conversion.Val(TextBox3.Text));
                TEMPmem.Emodulus = (float)Conversion.Val(TextBox4.Text);
                TEMPmem.Inertia = (float)Conversion.Val(TextBox5.Text);
                TEMPmem.g = (float)Conversion.Val(TextBox6.Text);
                Indexes.mem.Add(TEMPmem);
            }
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
                Indexes.ends = 4;         // Free-Free
            }
            else if (RadioButton5.Checked == true)
            {
                Indexes.ends = 5;         // Fixed-Pinned
            }
            else if (RadioButton6.Checked == true)
            {
                Indexes.ends = 6;         // pinned-pinned
            }

            _F = true;
            Close();
            My.MyProject.Forms.MDIMain.Focus();
            My.MyProject.Forms.beamcreate.mainpic.Refresh();
        }

        private void Button_cancel_Click(object sender, EventArgs e)
        {
            _F = false;
            Close();
            My.MyProject.Forms.beamcreate.mainpic.Refresh();
        }
    }
}