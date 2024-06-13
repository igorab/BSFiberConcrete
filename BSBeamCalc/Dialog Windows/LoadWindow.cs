using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace CBAnsDes
{
    public partial class LoadWindow
    {
        private float _Clength;
        private float _maxload;
        private int _Curline;
        private Member.P _CurP;
        private Member.U _CurU;
        private Member.M _CurM;
        private List<History> H = new List<History>();
        private EventArgs t;
        private object ob;

        private class History
        {
            public History(Member.P p)
            {
                pl = p;
            }
            public History(Member.U u)
            {
                ul = u;
            }
            public History(Member.M m)
            {
                ml = m;
            }
            private Member.P _pl;
            private Member.U _ul;
            private Member.M _ml;

            public Member.P pl
            {
                get
                {
                    return _pl;
                }
                set
                {
                    _pl = value;
                }
            }
            public Member.U ul
            {
                get
                {
                    return _ul;
                }
                set
                {
                    _ul = value;
                }
            }
            public Member.M ml
            {
                get
                {
                    return _ml;
                }
                set
                {
                    _ml = value;
                }
            }
        }

        public float maxload
        {
            get
            {
                return _maxload;
            }
            set
            {
                _maxload = value;
            }
        }

        public float Clength
        {
            get
            {
                return _Clength;
            }
            set
            {
                _Clength = value;
            }
        }

        public int Curline
        {
            get
            {
                return _Curline;
            }
            set
            {
                _Curline = value;
            }
        }

        public LoadWindow(float L, int C, Member.P CL)
        {

            // This call is required by the Windows Form Designer.
            InitializeComponent();
            _Clength = L;
            _Curline = C;
            _CurP = CL;
            Text = "Modify Point Load Window";
            float GGload = 0f;
            // ----Point Load
            foreach (var Pitm in Indexes.mem[C].Pload)
            {
                if (GGload < Pitm.pload)
                {
                    GGload = Pitm.pload;
                }
            }
            // ----UVL
            foreach (var Uitm in Indexes.mem[C].Uload)
            {
                if (GGload < Uitm.uload1)
                {
                    GGload = Uitm.uload1;
                }
                if (GGload < Uitm.uload2)
                {
                    GGload = Uitm.uload2;
                }
            }
            if (GGload < 10f)
            {
                GGload = 10f;
            }
            maxload = GGload;

            TabControl1.TabPages.Remove(TabPage2);
            TabControl1.TabPages.Remove(TabPage3);
            Button8.Enabled = false;

            TrackBar1.Maximum = (int)Math.Round(Indexes.mem[Curline].spanlength * 100f);
            TrackBar1.Value = (int)Math.Round(CL.pdist * 100f);
            TrackBar1_Scroll(ob, t);
            TextBox1.Text = CL.pload.ToString();
            // Add any initialization after the InitializeComponent() call.
            Panel1.Refresh();
        }

        public LoadWindow(float L, int C, Member.U CL)
        {

            // This call is required by the Windows Form Designer.
            InitializeComponent();
            _Clength = L;
            _Curline = C;
            _CurU = CL;
            Text = "Modify UVL Window";
            float GGload = 0f;
            // ----Point Load
            foreach (var Pitm in Indexes.mem[C].Pload)
            {
                if (GGload < Pitm.pload)
                {
                    GGload = Pitm.pload;
                }
            }
            // ----UVL
            foreach (var Uitm in Indexes.mem[C].Uload)
            {
                if (GGload < Uitm.uload1)
                {
                    GGload = Uitm.uload1;
                }
                if (GGload < Uitm.uload2)
                {
                    GGload = Uitm.uload2;
                }
            }
            if (GGload < 10f)
            {
                GGload = 10f;
            }
            maxload = GGload;

            TabControl1.TabPages.Remove(TabPage1);
            TabControl1.TabPages.Remove(TabPage3);
            Button2.Enabled = false;

            TrackBar3.Maximum = (int)Math.Round(Indexes.mem[Curline].spanlength * 100f);
            TrackBar4.Maximum = (int)Math.Round(Indexes.mem[Curline].spanlength * 100f);
            TrackBar3.Value = (int)Math.Round(CL.udist1 * 100f);
            TrackBar4.Value = (int)Math.Round(CL.udist2 * 100f);
            TrackBar3_Scroll(ob, t);
            TrackBar4_Scroll(ob, t);
            TextBox3.Text = CL.uload1.ToString();
            TextBox4.Text = CL.uload2.ToString();
            // Add any initialization after the InitializeComponent() call.
            Panel1.Refresh();
        }

        public LoadWindow(float L, int C, Member.M CL)
        {

            // This call is required by the Windows Form Designer.
            InitializeComponent();
            _Clength = L;
            _Curline = C;
            _CurM = CL;
            Text = "Modify Moment Window";
            float GGload = 0f;
            // ----Point Load
            foreach (var Pitm in Indexes.mem[C].Pload)
            {
                if (GGload < Pitm.pload)
                {
                    GGload = Pitm.pload;
                }
            }
            // ----UVL
            foreach (var Uitm in Indexes.mem[C].Uload)
            {
                if (GGload < Uitm.uload1)
                {
                    GGload = Uitm.uload1;
                }
                if (GGload < Uitm.uload2)
                {
                    GGload = Uitm.uload2;
                }
            }
            if (GGload < 10f)
            {
                GGload = 10f;
            }
            maxload = GGload;

            TabControl1.TabPages.Remove(TabPage1);
            TabControl1.TabPages.Remove(TabPage2);
            Button9.Enabled = false;

            TrackBar2.Maximum = (int)Math.Round(Indexes.mem[Curline].spanlength * 100f);
            TrackBar2.Value = (int)Math.Round(CL.mdist * 100f);
            TrackBar2_Scroll(ob, t);
            TextBox2.Text = CL.mload.ToString();
            // Add any initialization after the InitializeComponent() call.
            Panel1.Refresh();
        }

        public LoadWindow(float L, int C)
        {

            // This call is required by the Windows Form Designer.
            InitializeComponent();
            _Clength = L;
            _Curline = C;
            Text = "Add Load Window";
            float GGload = 0f;
            // ----Point Load
            foreach (var Pitm in Indexes.mem[C].Pload)
            {
                if (GGload < Pitm.pload)
                {
                    GGload = Pitm.pload;
                }
            }
            // ----UVL
            foreach (var Uitm in Indexes.mem[C].Uload)
            {
                if (GGload < Uitm.uload1)
                {
                    GGload = Uitm.uload1;
                }
                if (GGload < Uitm.uload2)
                {
                    GGload = Uitm.uload2;
                }
            }
            if (GGload < 10f)
            {
                GGload = 10f;
            }

            Button3.Enabled = false;
            Button4.Enabled = false;
            Button7.Enabled = false;
            maxload = GGload;
            // Add any initialization after the InitializeComponent() call.
            TrackBar1.Maximum = (int)Math.Round(Clength * 100f);
            TrackBar2.Maximum = (int)Math.Round(Clength * 100f);
            TrackBar3.Maximum = (int)Math.Round(Clength * 100f);
            TrackBar4.Maximum = (int)Math.Round(Clength * 100f);
            TrackBar4.Value = (int)Math.Round(Clength * 100f);
            TextBox1.Text = 10.ToString();
            TextBox2.Text = 10.ToString();
            TextBox3.Text = 10.ToString();
            TextBox4.Text = 10.ToString();
        }

        #region Track Bar Zone

        private void LoadWindow_Load(object sender, EventArgs e)
        {

            Label14.Text = (TrackBar4.Value / 100d).ToString();
            TabControl1.SelectedIndexChanged += (_, __) => MAXl();
            TabControl1.SelectedIndexChanged += (_, __) => Panel1.Refresh();
            Button3.Click += (_, __) => My.MyProject.Forms.beamcreate.mainpic.Refresh();
            Button4.Click += (_, __) => My.MyProject.Forms.beamcreate.mainpic.Refresh();
            Button7.Click += (_, __) => My.MyProject.Forms.beamcreate.mainpic.Refresh();
        }

        private void MAXl()
        {
            float GGload = 0f;
            // ----Point Load
            foreach (var Pitm in Indexes.mem[Curline].Pload)
            {
                if (GGload < Pitm.pload)
                {
                    GGload = Pitm.pload;
                }
            }
            // ----UVL
            foreach (var Uitm in Indexes.mem[Curline].Uload)
            {
                if (GGload < Uitm.uload1)
                {
                    GGload = Uitm.uload1;
                }
                if (GGload < Uitm.uload2)
                {
                    GGload = Uitm.uload2;
                }
            }
            if (TabControl1.SelectedTab.Contains(Label1))
            {
                if ((double)GGload < Conversion.Val(TextBox1.Text))
                {
                    GGload = (float)Conversion.Val(TextBox1.Text);
                }
            }
            else if (TabControl1.SelectedTab.Contains(Label2))
            {
                if ((double)GGload < Conversion.Val(TextBox3.Text))
                {
                    GGload = (float)Conversion.Val(TextBox3.Text);
                }
                if ((double)GGload < Conversion.Val(TextBox4.Text))
                {
                    GGload = (float)Conversion.Val(TextBox4.Text);
                }
            }
            maxload = GGload;
            Panel1.Refresh();
        }

        #endregion

        #region Panel1 Events
        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            paintBeam(e);
            paintTl(e);
            paintOl(e);
        }

        private void paintBeam(PaintEventArgs e)
        {
            var BeamPen = new Pen(Color.Blue, 2f);
            var dimpen = new Pen(Color.CadetBlue, 1.5f);
            var adcap = new System.Drawing.Drawing2D.AdjustableArrowCap(3f, 5f);

            dimpen.CustomStartCap = adcap;
            dimpen.CustomEndCap = adcap;
            e.Graphics.DrawLine(BeamPen, 30, 130, 430, 130);
            e.Graphics.DrawLine(dimpen, 30, 150, 430, 150);
            e.Graphics.DrawString(_Clength.ToString(), Font, dimpen.Brush, 230f, 148f);
        }

        private void paintTl(PaintEventArgs e)
        {
            if (TabControl1.SelectedTab.Contains(Label1))
            {
                paintPointload(e);
            }
            else if (TabControl1.SelectedTab.Contains(Label2))
            {
                paintUVL(e);
            }
            else if (TabControl1.SelectedTab.Contains(Label3))
            {
                paintMoment(e);
            }
        }

        private void paintOl(PaintEventArgs e)
        {
            puload(e);
            pmload(e);
            ppload(e);
        }

        #region Paint Point load
        private void paintPointload(PaintEventArgs e)
        {
            var loadpen = new Pen(Color.Green, 2f);
            var adcap = new System.Drawing.Drawing2D.AdjustableArrowCap(3f, 5f);
            float toSX;
            float toSY;
            loadpen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            loadpen.CustomStartCap = adcap;
            toSX = (float)(30d + Conversion.Val(Label12.Text) * (double)(400f / Clength));
            toSY = (float)(Conversion.Val(TextBox1.Text) * (double)(90f / maxload));

            e.Graphics.DrawLine(loadpen, toSX, 130f, toSX, 130f - toSY);
            e.Graphics.DrawString(Conversion.Val(TextBox1.Text).ToString(), Font, loadpen.Brush, toSX, 110f - toSY);

        }

        private void TrackBar1_Scroll(object sender, EventArgs e)
        {
            Label12.Text = (TrackBar1.Value / 100d).ToString();
            Panel1.Refresh();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (Conversion.Val(TextBox1.Text) <= 0d)
            {
                TextBox1.Text = 10.ToString();
            }
            MAXl();
            Panel1.Refresh();
        }

        private void ppload(PaintEventArgs e)
        {
            var loadpen = new Pen(Color.Green, 2f);
            var adcap = new System.Drawing.Drawing2D.AdjustableArrowCap(3f, 5f);
            float toSX;
            float toSY;

            loadpen.CustomStartCap = adcap;
            foreach (var itm in Indexes.mem[Curline].Pload)
            {
                toSX = 30f + itm.pdist * (400f / Clength);
                toSY = itm.pload * (90f / maxload);

                e.Graphics.DrawLine(loadpen, toSX, 130f, toSX, 130f - toSY);
                e.Graphics.DrawString(itm.pload.ToString(), Font, loadpen.Brush, toSX, 110f - toSY);
            }
        }
        #endregion

        #region Paint UVL

        private void TrackBar3_Scroll(object sender, EventArgs e)
        {
            if (TrackBar3.Value >= TrackBar4.Value)
            {
                TrackBar3.Value = TrackBar4.Value - 1;
            }
            Label13.Text = (TrackBar3.Value / 100d).ToString();
            Panel1.Refresh();
        }

        private void TrackBar4_Scroll(object sender, EventArgs e)
        {
            if (TrackBar4.Value <= TrackBar3.Value)
            {
                TrackBar4.Value = TrackBar3.Value + 1;
            }
            Label14.Text = (TrackBar4.Value / 100d).ToString();
            Panel1.Refresh();
        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {
            if (Conversion.Val(TextBox3.Text) <= 0d)
            {
                TextBox3.Text = 10.ToString();
            }
            MAXl();
            Panel1.Refresh();
        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {
            if (Conversion.Val(TextBox4.Text) <= 0d)
            {
                TextBox4.Text = 10.ToString();
            }
            MAXl();
            Panel1.Refresh();
        }

        private void paintUVL(PaintEventArgs e)
        {
            var loadpen = new Pen(Color.DeepPink, 1f);
            var adcap = new System.Drawing.Drawing2D.AdjustableArrowCap(2f, 4f);
            float toSX1, toSX2;
            float toSY1, toSY2;
            loadpen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            loadpen.CustomStartCap = adcap;
            toSX1 = (float)(30d + Conversion.Val(Label13.Text) * (double)(400f / Clength));
            toSX2 = (float)(30d + Conversion.Val(Label14.Text) * (double)(400f / Clength));
            toSY1 = (float)(Conversion.Val(TextBox3.Text) * (double)(90f / maxload));
            toSY2 = (float)(Conversion.Val(TextBox4.Text) * (double)(90f / maxload));

            e.Graphics.DrawLine(loadpen, toSX1, 130f, toSX1, 130f - toSY1);
            e.Graphics.DrawString(Conversion.Val(TextBox3.Text).ToString(), Font, loadpen.Brush, toSX1, 110f - toSY1);

            if (toSY2 == toSY1)
            {
                for (float i = toSX1, loopTo = toSX2; i <= loopTo; i += 10f)
                    e.Graphics.DrawLine(loadpen, i, 130f, i, 130f - toSY1);
            }
            else if (toSY2 > toSY1)
            {
                for (float i = toSX1, loopTo2 = toSX2; i <= loopTo2; i += 10f)
                    e.Graphics.DrawLine(loadpen, i, 130f, i, 130f - (toSY1 + (toSY2 - toSY1) / (toSX2 - toSX1) * (i - toSX1)));
            }
            else
            {
                for (float i = toSX1, loopTo1 = toSX2; i <= loopTo1; i += 10f)
                    e.Graphics.DrawLine(loadpen, i, 130f, i, 130f - (toSY2 + (toSY1 - toSY2) / (toSX2 - toSX1) * (toSX2 - i)));
            }
            e.Graphics.DrawLine(loadpen, toSX2, 130f, toSX2, 130f - toSY2);
            e.Graphics.DrawString(Conversion.Val(TextBox4.Text).ToString(), Font, loadpen.Brush, toSX2, 110f - toSY2);
        }

        private void puload(PaintEventArgs e)
        {
            var loadpen = new Pen(Color.DeepPink, 1f);
            var adcap = new System.Drawing.Drawing2D.AdjustableArrowCap(2f, 4f);
            float toSX1, toSX2;
            float toSY1, toSY2;

            loadpen.CustomStartCap = adcap;
            foreach (var itm in Indexes.mem[Curline].Uload)
            {
                toSX1 = 30f + itm.udist1 * (400f / Clength);
                toSX2 = 30f + itm.udist2 * (400f / Clength);
                toSY1 = itm.uload1 * (90f / maxload);
                toSY2 = itm.uload2 * (90f / maxload);

                e.Graphics.DrawLine(loadpen, toSX1, 130f, toSX1, 130f - toSY1);
                e.Graphics.DrawString(itm.uload1.ToString(), Font, loadpen.Brush, toSX1, 110f - toSY1);

                if (toSY2 == toSY1)
                {
                    for (float i = toSX1, loopTo = toSX2; i <= loopTo; i += 10f)
                        e.Graphics.DrawLine(loadpen, i, 130f, i, 130f - toSY1);
                }
                else if (toSY2 > toSY1)
                {
                    for (float i = toSX1, loopTo2 = toSX2; i <= loopTo2; i += 10f)
                        e.Graphics.DrawLine(loadpen, i, 130f, i, 130f - (toSY1 + (toSY2 - toSY1) / (toSX2 - toSX1) * (i - toSX1)));
                }
                else
                {
                    for (float i = toSX1, loopTo1 = toSX2; i <= loopTo1; i += 10f)
                        e.Graphics.DrawLine(loadpen, i, 130f, i, 130f - (toSY2 + (toSY1 - toSY2) / (toSX2 - toSX1) * (toSX2 - i)));
                }
                e.Graphics.DrawLine(loadpen, toSX2, 130f, toSX2, 130f - toSY2);
                e.Graphics.DrawString(itm.uload2.ToString(), Font, loadpen.Brush, toSX2, 110f - toSY2);
            }
        }
        #endregion

        #region Paint Moment
        private void TrackBar2_Scroll(object sender, EventArgs e)
        {
            Label15.Text = (TrackBar2.Value / 100d).ToString();
            Panel1.Refresh();
        }

        private void paintMoment(PaintEventArgs e)
        {
            var loadpen = new Pen(Color.Orange, 2f);
            var adcap = new System.Drawing.Drawing2D.AdjustableArrowCap(3f, 5f);
            float toSX;
            loadpen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            if (RadioButton1.Checked == true)
            {
                loadpen.CustomStartCap = adcap;
            }
            else
            {
                loadpen.CustomEndCap = adcap;
            }

            toSX = (float)(30d + Conversion.Val(Label15.Text) * (double)(400f / Clength));
            e.Graphics.DrawArc(loadpen, toSX - 15f, 115f, 30f, 30f, 270f, 180f);
            e.Graphics.DrawString(Conversion.Val(TextBox2.Text).ToString(), Font, loadpen.Brush, toSX, 80f);
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Panel1.Refresh();
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Panel1.Refresh();
        }

        private void pmload(PaintEventArgs e)
        {
            var loadpen = new Pen(Color.Orange, 2f);
            var adcap = new System.Drawing.Drawing2D.AdjustableArrowCap(3f, 5f);
            float toSX;

            foreach (var itm in Indexes.mem[Curline].Mload)
            {
                if (itm.mload > 0f)
                {
                    loadpen.CustomStartCap = adcap;
                }
                else
                {
                    loadpen.CustomEndCap = adcap;
                }
                toSX = 30f + itm.mdist * (400f / Clength);
                e.Graphics.DrawArc(loadpen, toSX - 15f, 115f, 30f, 30f, 270f, 180f);
                e.Graphics.DrawString(Math.Abs(itm.mload).ToString(), Font, loadpen.Brush, toSX, 80f);
            }
        }
        #endregion
        #endregion

        #region ADD Events
        private void Button8_Click(object sender, EventArgs e)
        {
            // --Point Load ADD
            var temp = default(Member.P);
            temp.pload = (float)Conversion.Val(TextBox1.Text);
            temp.pdist = (float)Conversion.Val(Label12.Text);

            // ----chk for conflict
            foreach (var itm in Indexes.mem[Curline].Pload)
            {
                if (itm.pdist == temp.pdist)
                {
                    return;
                }
            }
            Indexes.mem[Curline].Pload.Add(temp);
            Panel1.Refresh();
            My.MyProject.Forms.beamcreate.mainpic.Refresh();
            var tempH = new History(Indexes.mem[Curline].Pload[Indexes.mem[Curline].Pload.Count - 1]);
            H.Add(tempH);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            // --UVL ADD
            var temp = default(Member.U);
            temp.uload1 = (float)Conversion.Val(TextBox3.Text);
            temp.uload2 = (float)Conversion.Val(TextBox4.Text);
            temp.udist1 = (float)Conversion.Val(Label13.Text);
            temp.udist2 = (float)Conversion.Val(Label14.Text);
            // ----chk for conflict
            foreach (var itm in Indexes.mem[Curline].Uload)
            {
                if (itm.udist1 == temp.udist1 & itm.udist2 == temp.udist2)
                {
                    return;
                }
            }
            Indexes.mem[Curline].Uload.Add(temp);
            Panel1.Refresh();
            My.MyProject.Forms.beamcreate.mainpic.Refresh();
            var tempH = new History(Indexes.mem[Curline].Uload[Indexes.mem[Curline].Uload.Count - 1]);
            H.Add(tempH);
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            // --Moment ADD
            var temp = default(Member.M);
            if (RadioButton1.Checked == true) // ---- Clockwise
            {
                temp.mload = (float)Conversion.Val(TextBox2.Text);
            }
            else // ---- Anti clockwise
            {
                temp.mload = (float)(-1 * Conversion.Val(TextBox2.Text));
            }

            temp.mdist = (float)Conversion.Val(Label15.Text);
            // ----chk for conflict
            foreach (var itm in Indexes.mem[Curline].Mload)
            {
                if (itm.mdist == temp.mdist)
                {
                    return;
                }
            }
            Indexes.mem[Curline].Mload.Add(temp);
            Panel1.Refresh();
            My.MyProject.Forms.beamcreate.mainpic.Refresh();
            var tempH = new History(Indexes.mem[Curline].Mload[Indexes.mem[Curline].Mload.Count - 1]);
            H.Add(tempH);
        }
        #endregion

        #region Modify Events
        private void Button7_Click(object sender, EventArgs e)
        {
            // --Point Load MODIFY
            var temp = default(Member.P);
            temp.pload = (float)Conversion.Val(TextBox1.Text);
            temp.pdist = (float)Conversion.Val(Label12.Text);
            Indexes.mem[Curline].Pload.Insert(Indexes.mem[Curline].Pload.IndexOf(_CurP), temp);
            Indexes.mem[Curline].Pload.Remove(_CurP);
            Close();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            // --UVL MODIFY
            var temp = default(Member.U);
            temp.uload1 = (float)Conversion.Val(TextBox3.Text);
            temp.uload2 = (float)Conversion.Val(TextBox4.Text);
            temp.udist1 = (float)Conversion.Val(Label13.Text);
            temp.udist2 = (float)Conversion.Val(Label14.Text);
            Indexes.mem[Curline].Uload.Insert(Indexes.mem[Curline].Uload.IndexOf(_CurU), temp);
            Indexes.mem[Curline].Uload.Remove(_CurU);
            Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            // --Moment MODIFY
            var temp = default(Member.M);
            if (RadioButton1.Checked == true) // ---- Clockwise
            {
                temp.mload = (float)Conversion.Val(TextBox2.Text);
            }
            else // ---- Anti clockwise
            {
                temp.mload = (float)(-1 * Conversion.Val(TextBox2.Text));
            }
            temp.mdist = (float)Conversion.Val(Label15.Text);
            Indexes.mem[Curline].Mload.Insert(Indexes.mem[Curline].Mload.IndexOf(_CurM), temp);
            Indexes.mem[Curline].Mload.Remove(_CurM);
            Close();
        }

        #endregion

        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (H.Count != 0)
            {
                Indexes.mem[Curline].Pload.Remove(H[H.Count - 1].pl);
                Indexes.mem[Curline].Uload.Remove(H[H.Count - 1].ul);
                Indexes.mem[Curline].Mload.Remove(H[H.Count - 1].ml);

                H.RemoveAt(H.Count - 1);
                Panel1.Refresh();
                My.MyProject.Forms.beamcreate.mainpic.Refresh();
            }
            Panel1.Refresh();
        }
    }
}