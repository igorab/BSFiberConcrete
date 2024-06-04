using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace CBAnsDes
{
    public partial class addmember
    {
        private ToolTip t = new ToolTip();

        public addmember()
        {

            // This call is required by the Windows Form Designer.
            InitializeComponent();

            // Add any initialization after the InitializeComponent() call.
            if (Text == "Modify Member")
            {

            }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (Conversion.Val(TextBox1.Text) == 0d | Conversion.Val(TextBox1.Text) < 0d)
            {
                TextBox1.Text = 4.ToString();
            }
        }


        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            if (Information.IsNumeric(TextBox2.Text))
            {
            }

            else
            {
                TextBox2.Text = "";
            }
        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {
            if (Information.IsNumeric(TextBox3.Text))
            {
            }

            else
            {
                TextBox3.Text = "";
            }
        }

        private void Button_ok_Click(object sender, EventArgs e)
        {
            if (Conversion.Val(TextBox1.Text) < 0.1d)
            {
                TextBox1.Text = 10.ToString();
                Interaction.MsgBox("Sorry Length limited to 0.1 units");
                return;
            }

            if (Text == "Add Member")
            {
                var TEMPmem = new Member();
                TEMPmem.spanlength = (float)Conversion.Val(TextBox1.Text);
                TEMPmem.Emodulus = (float)Conversion.Val(TextBox2.Text);
                TEMPmem.Inertia = (float)Conversion.Val(TextBox3.Text);
                TEMPmem.g = (float)Conversion.Val(TextBox6.Text);
                Indexes.mem.Add(TEMPmem);
                Close();
                My.MyProject.Forms.beamcreate.mainpic.Refresh();
            }
            else if (Text == "Modify Member")
            {
                Indexes.mem[Indexes.selline].spanlength = (float)Conversion.Val(TextBox1.Text);
                Indexes.mem[Indexes.selline].Emodulus = (float)Conversion.Val(TextBox2.Text);
                Indexes.mem[Indexes.selline].Inertia = (float)Conversion.Val(TextBox3.Text);
                Indexes.mem[Indexes.selline].g = (float)Conversion.Val(TextBox6.Text);
                var rp = new List<int>();
                // ---point load out of span
                foreach (var pitm in Indexes.mem[Indexes.selline].Pload)
                {
                    if (pitm.pdist > Indexes.mem[Indexes.selline].spanlength)
                    {
                        rp.Add(Indexes.mem[Indexes.selline].Pload.IndexOf(pitm));
                    }
                }
                rp.Reverse();
                if (rp.Count != 0)
                {
                    foreach (var i in rp)
                        Indexes.mem[Indexes.selline].Pload.RemoveAt(i);
                }
                rp.Clear();
                // ---UVL out of span
                foreach (var uitm in Indexes.mem[Indexes.selline].Uload)
                {
                    if (uitm.udist2 > Indexes.mem[Indexes.selline].spanlength)
                    {
                        rp.Add(Indexes.mem[Indexes.selline].Uload.IndexOf(uitm));
                    }
                }
                rp.Reverse();
                if (rp.Count != 0)
                {
                    foreach (var i in rp)
                        Indexes.mem[Indexes.selline].Uload.RemoveAt(i);
                }
                rp.Clear();
                // ---Moment out of span
                foreach (var mitm in Indexes.mem[Indexes.selline].Mload)
                {
                    if (mitm.mdist > Indexes.mem[Indexes.selline].spanlength)
                    {
                        rp.Add(Indexes.mem[Indexes.selline].Mload.IndexOf(mitm));
                    }
                }
                rp.Reverse();
                if (rp.Count != 0)
                {
                    foreach (var i in rp)
                        Indexes.mem[Indexes.selline].Mload.RemoveAt(i);
                }
                rp.Clear();
                Close();
                My.MyProject.Forms.beamcreate.mainpic.Refresh();
            }
        }

        private void Button_cancel_Click(object sender, EventArgs e)
        {
            Close();
            My.MyProject.Forms.beamcreate.mainpic.Refresh();
        }
    }
}