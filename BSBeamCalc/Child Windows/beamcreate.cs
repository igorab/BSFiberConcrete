using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace CBAnsDes
{
    public partial class beamcreate
    {
        private float ht;
        private float totL;
        private Member.P _selPL;
        private Member.U _selUL;
        private Member.M _selML;
        private int _tipe;

        private Point PanStartDrag;
        private Point Cursor_Pt;
        private static bool isLeftDrag = false;
        private static bool isMiddleDrag = false;

        public int tipe
        {
            get
            {
                return _tipe;
            }
            set
            {
                _tipe = value;
            }
        }

        public Member.P selPL
        {
            get
            {
                return _selPL;
            }
            set
            {
                _selPL = value;
            }
        }

        public Member.U selUL
        {
            get
            {
                return _selUL;
            }
            set
            {
                _selUL = value;
            }
        }

        public Member.M selML
        {
            get
            {
                return _selML;
            }
            set
            {
                _selML = value;
            }
        }

        public float MEheight
        {
            get
            {
                return ht;
            }
            set
            {
                ht = value;
            }
        }

        public float Tlength
        {
            get
            {
                return totL;
            }
            set
            {
                totL = value;
            }
        }

        public beamcreate()
        {
            InitializeComponent();
        }

        private void beamcreate_Load(object sender, EventArgs e)
        {
            try
            {
                // mainpic.Width = 1600
                // mainpic.Height = 1600
                // respic.Width = 1600
                // respic.Height = 1600


                mainpic.Dock = DockStyle.Fill;
                respic.Dock = DockStyle.Fill;

                Indexes.MidPt = new Point((int)Math.Round(mainpic.Width / 2d), (int)Math.Round(mainpic.Height / 2d));

                // VScrollBar1.Maximum = (1600 - coverpic.Height) / 2
                // HScrollBar1.Maximum = (1600 - coverpic.Width) / 2
                // VScrollBar1.Minimum = 100
                // HScrollBar1.Minimum = 50
                // VScrollBar1.Value = VScrollBar1.Maximum / 2
                // HScrollBar1.Value = HScrollBar1.Maximum / 2
                My.MyProject.Forms.MDIMain.SFlabel.Visible = false;
                My.MyProject.Forms.MDIMain.BMlabel.Visible = false;
                My.MyProject.Forms.MDIMain.Xlabel.Visible = false;
                SizeChanged += (_, __) => SizeController.sizemonitor();
                SizeController.sizemonitor();
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Fundamental Error !!!!");
            }

        }

        #region Mainpic Events
        #region Mainpic Paint Events
        private void mainpic_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.ScaleTransform((float)Indexes.Zm, (float)Indexes.Zm);
            e.Graphics.TranslateTransform(Indexes.MidPt.X, Indexes.MidPt.Y);

            // Zero point for reference
            // e.Graphics.DrawRectangle(Pens.BlueViolet, New Rectangle(New Point(-10, -10), New Size(20, 20)))


            // '---Grids (Paint grid removed in 2021 version)
            // For i = 0 To 1600 Step 100
            // e.Graphics.DrawLine(Pens.Beige, i, 0, i, ht)
            // e.Graphics.DrawLine(Pens.Beige, 0, i, 1600, i)
            // 'e.Graphics.DrawString(i, Font, Brushes.Brown, i, ht * (8 / 10))
            // Next



            paintBeam(e);
            paintEnds(e);
            paintSupport(e);
            paintload(e);
        }

        private void paintBeam(PaintEventArgs e)
        {
            var BeamPen = new Pen(Color.Blue, 2f);
            var dimpen = new Pen(Color.CadetBlue, 1.5f);
            System.Drawing.Drawing2D.LinearGradientBrush linGrBrush;
            float StDist;
            float ScaTotLength;
            float TotLength = 0f;

            foreach (var itm in Indexes.mem)
                TotLength = TotLength + itm.spanlength;


            StDist = (float)(-(coverpic.Width / 2d) + 100d);
            ScaTotLength = coverpic.Width - 200;

            e.Graphics.DrawLine(BeamPen, StDist, 0f, StDist + ScaTotLength, 0f);
            e.Graphics.DrawLine(Pens.CadetBlue, StDist, 0 + 100, StDist, 0 + 140);

            // e.Graphics.DrawRectangle(Pens.DarkBlue, StDist - 2, (ht / 2) - 2, 4, 4)
            // e.Graphics.FillRectangle(Brushes.DarkBlue, StDist - 2, (ht / 2) - 2, 4, 4)

            float Idist = StDist;
            float dist = 0f;
            foreach (var itm in Indexes.mem)
            {
                dist = dist + itm.spanlength;
                e.Graphics.DrawLine(Pens.CadetBlue, StDist + dist * (ScaTotLength / TotLength), 0 + 100, StDist + dist * (ScaTotLength / TotLength), 0 + 140);
                // e.Graphics.DrawRectangle(Pens.DarkBlue, (StDist + (dist * (ScaTotLength / TotLength))) - 2, (ht / 2) - 2, 4, 4)
                // e.Graphics.FillRectangle(Brushes.DarkBlue, (StDist + (dist * (ScaTotLength / TotLength))) - 2, (ht / 2) - 2, 4, 4)

                // ---Dimension Line
                var adcap = new System.Drawing.Drawing2D.AdjustableArrowCap(3f, 5f);
                linGrBrush = new System.Drawing.Drawing2D.LinearGradientBrush(new Point((int)Math.Round(Idist), 0 + 120), new Point((int)Math.Round(StDist + dist * (ScaTotLength / TotLength)), 0 + 120), Color.CadetBlue, Color.Azure);
                linGrBrush.SetSigmaBellShape(0.5f, 1f);
                dimpen.Brush = linGrBrush;
                dimpen.CustomStartCap = adcap;
                dimpen.CustomEndCap = adcap;
                e.Graphics.DrawLine(dimpen, Idist, 0 + 120, StDist + dist * (ScaTotLength / TotLength), 0 + 120);


                // Defining the Rectangle for selecting the beam element
                var R = new Rectangle((int)Math.Round(Idist), 0 - 100, (int)Math.Round(StDist + dist * (ScaTotLength / TotLength) - Idist), 140);

                // e.Graphics.DrawRectangle(Pens.Black, R)
                Indexes.mem[Indexes.mem.IndexOf(itm)].rect = R;
                // ==================================================================================================================================


                e.Graphics.DrawString(itm.spanlength.ToString(), Font, Brushes.DodgerBlue, (StDist + dist * (ScaTotLength / TotLength) + Idist) / 2f, 0 + 115);
                linGrBrush = new System.Drawing.Drawing2D.LinearGradientBrush(new Point((int)Math.Round(Idist), 0 - 3), new Point((int)Math.Round(StDist + dist * (ScaTotLength / TotLength)), 0 + 3), Color.LightBlue, Color.Aqua);
                linGrBrush.SetSigmaBellShape(0.5f, 0.2f);
                if (Indexes.selline == Indexes.mem.IndexOf(itm) | Indexes.Tselline == Indexes.mem.IndexOf(itm))
                {
                    e.Graphics.DrawRectangle(Pens.WhiteSmoke, Idist, 0 - 3, StDist + dist * (ScaTotLength / TotLength) - Idist, 6f);
                    e.Graphics.FillRectangle(linGrBrush, Idist, 0 - 3, StDist + dist * (ScaTotLength / TotLength) - Idist, 6f);
                    e.Graphics.DrawLine(BeamPen, Idist, 0f, StDist + dist * (ScaTotLength / TotLength), 0f);
                }
                Idist = StDist + dist * (ScaTotLength / TotLength);
            }
        }

        private void paintEnds(PaintEventArgs e)
        {
            float StDist;
            float ScaTotLength;
            float TotLength = 0f;
            var dofpen = new Pen(Color.Firebrick, 2f);
            System.Drawing.Drawing2D.LinearGradientBrush linGrBrush;

            foreach (var itm in Indexes.mem)
                TotLength = TotLength + itm.spanlength;
            StDist = (float)(-(coverpic.Width / 2d) + 100d);
            ScaTotLength = coverpic.Width - 200;

            // ----Ends
            if (Indexes.ends == 1) // Fixed-Fixed
            {
                // ---end1
                linGrBrush = new System.Drawing.Drawing2D.LinearGradientBrush(new Point((int)Math.Round(StDist - 15f), 0), new Point((int)Math.Round(StDist - 32f), 0), Color.Beige, Color.Firebrick);
                e.Graphics.DrawRectangle(Pens.WhiteSmoke, StDist - 15f, 0 - 35, 15f, 70f);
                e.Graphics.FillRectangle(linGrBrush, StDist - 15f, 0 - 35, 15f, 70f);
                // ---end2
                linGrBrush = new System.Drawing.Drawing2D.LinearGradientBrush(new Point((int)Math.Round(StDist + ScaTotLength), 0), new Point((int)Math.Round(StDist + ScaTotLength + 15f), 0), Color.Beige, Color.Firebrick);
                e.Graphics.DrawRectangle(Pens.WhiteSmoke, StDist + ScaTotLength, 0 - 35, 15f, 70f);
                e.Graphics.FillRectangle(linGrBrush, StDist + ScaTotLength, 0 - 35, 15f, 70f);
            }
            else if (Indexes.ends == 2) // Fixed-Free
            {
                // ---end1
                linGrBrush = new System.Drawing.Drawing2D.LinearGradientBrush(new Point((int)Math.Round(StDist - 15f), 0), new Point((int)Math.Round(StDist - 32f), 0), Color.Beige, Color.Firebrick);
                e.Graphics.DrawRectangle(Pens.WhiteSmoke, StDist - 15f, 0 - 35, 15f, 70f);
                e.Graphics.FillRectangle(linGrBrush, StDist - 15f, 0 - 35, 15f, 70f);
            }

            else if (Indexes.ends == 3) // Pinned-Pinned
            {
                // ---end1
                e.Graphics.DrawLine(dofpen, StDist, 0 + 3, StDist - 8f, 0 + 19);
                e.Graphics.DrawLine(dofpen, StDist, 0 + 3, StDist + 8f, 0 + 19);
                e.Graphics.DrawLine(dofpen, StDist + 8f, 0 + 19, StDist - 8f, 0 + 19);

                e.Graphics.DrawLine(dofpen, StDist - 8f, 0 + 19, StDist, 0 + 25);
                e.Graphics.DrawLine(dofpen, StDist, 0 + 19, StDist - 8f, 0 + 25);
                e.Graphics.DrawLine(dofpen, StDist + 8f, 0 + 19, StDist, 0 + 25);
                e.Graphics.DrawLine(dofpen, StDist, 0 + 19, StDist + 8f, 0 + 25);

                e.Graphics.DrawLine(dofpen, StDist - 8f, 0 + 25, StDist + 8f, 0 + 25);
                // ---end2
                e.Graphics.DrawLine(dofpen, StDist + ScaTotLength, 0 + 3, StDist + ScaTotLength - 8f, 0 + 19);
                e.Graphics.DrawLine(dofpen, StDist + ScaTotLength, 0 + 3, StDist + ScaTotLength + 8f, 0 + 19);
                e.Graphics.DrawLine(dofpen, StDist + ScaTotLength + 8f, 0 + 19, StDist + ScaTotLength - 8f, 0 + 19);

                e.Graphics.DrawLine(dofpen, StDist + ScaTotLength - 8f, 0 + 19, StDist + ScaTotLength, 0 + 25);
                e.Graphics.DrawLine(dofpen, StDist + ScaTotLength, 0 + 19, StDist + ScaTotLength - 8f, 0 + 25);
                e.Graphics.DrawLine(dofpen, StDist + ScaTotLength + 8f, 0 + 19, StDist + ScaTotLength, 0 + 25);
                e.Graphics.DrawLine(dofpen, StDist + ScaTotLength, 0 + 19, StDist + ScaTotLength + 8f, 0 + 25);

                e.Graphics.DrawLine(dofpen, StDist + ScaTotLength - 8f, 0 + 25, StDist + ScaTotLength + 8f, 0 + 25);
            }
            // ElseIf ends = 4 Then 'Free-Free
            // ----
            else if (Indexes.ends == 5) // Fixed-Pinned
            {
                // ---end1
                linGrBrush = new System.Drawing.Drawing2D.LinearGradientBrush(new Point((int)Math.Round(StDist - 15f), 0), new Point((int)Math.Round(StDist - 32f), 0), Color.Beige, Color.Firebrick);
                e.Graphics.DrawRectangle(Pens.WhiteSmoke, StDist - 15f, 0 - 35, 15f, 70f);
                e.Graphics.FillRectangle(linGrBrush, StDist - 15f, 0 - 35, 15f, 70f);
                // ---end2
                e.Graphics.DrawLine(dofpen, StDist + ScaTotLength, 0 + 3, StDist + ScaTotLength - 8f, 0 + 19);
                e.Graphics.DrawLine(dofpen, StDist + ScaTotLength, 0 + 3, StDist + ScaTotLength + 8f, 0 + 19);
                e.Graphics.DrawLine(dofpen, StDist + ScaTotLength + 8f, 0 + 19, StDist + ScaTotLength - 8f, 0 + 19);

                e.Graphics.DrawLine(dofpen, StDist + ScaTotLength - 8f, 0 + 19, StDist + ScaTotLength, 0 + 25);
                e.Graphics.DrawLine(dofpen, StDist + ScaTotLength, 0 + 19, StDist + ScaTotLength - 8f, 0 + 25);
                e.Graphics.DrawLine(dofpen, StDist + ScaTotLength + 8f, 0 + 19, StDist + ScaTotLength, 0 + 25);
                e.Graphics.DrawLine(dofpen, StDist + ScaTotLength, 0 + 19, StDist + ScaTotLength + 8f, 0 + 25);

                e.Graphics.DrawLine(dofpen, StDist + ScaTotLength - 8f, 0 + 25, StDist + ScaTotLength + 8f, 0 + 25);
            }
            else if (Indexes.ends == 6)
            {
                // ---end1
                e.Graphics.DrawLine(dofpen, StDist, 0 + 3, StDist - 8f, 0 + 19);
                e.Graphics.DrawLine(dofpen, StDist, 0 + 3, StDist + 8f, 0 + 19);
                e.Graphics.DrawLine(dofpen, StDist + 8f, 0 + 19, StDist - 8f, 0 + 19);

                e.Graphics.DrawLine(dofpen, StDist - 8f, 0 + 19, StDist, 0 + 25);
                e.Graphics.DrawLine(dofpen, StDist, 0 + 19, StDist - 8f, 0 + 25);
                e.Graphics.DrawLine(dofpen, StDist + 8f, 0 + 19, StDist, 0 + 25);
                e.Graphics.DrawLine(dofpen, StDist, 0 + 19, StDist + 8f, 0 + 25);

                e.Graphics.DrawLine(dofpen, StDist - 8f, 0 + 25, StDist + 8f, 0 + 25);
            }
        }

        private void paintSupport(PaintEventArgs e)
        {
            var dofpen = new Pen(Color.Firebrick, 2f);
            float StDist;
            float ScaTotLength;
            float TotLength = 0f;

            foreach (var itm in Indexes.mem)
                TotLength = TotLength + itm.spanlength;
            StDist = (float)(-(coverpic.Width / 2d) + 100d);
            ScaTotLength = coverpic.Width - 200;

            var dist = default(float);
            foreach (var itm in Indexes.mem)
            {
                dist = dist + itm.spanlength;
                if (Indexes.mem.IndexOf(itm) == Indexes.mem.Count - 1)
                {
                    return;
                }
                e.Graphics.DrawLine(dofpen, StDist + dist * (ScaTotLength / TotLength), 0 + 3, StDist + dist * (ScaTotLength / TotLength) - 8f, 0 + 19);
                e.Graphics.DrawLine(dofpen, StDist + dist * (ScaTotLength / TotLength), 0 + 3, StDist + dist * (ScaTotLength / TotLength) + 8f, 0 + 19);
                e.Graphics.DrawLine(dofpen, StDist + dist * (ScaTotLength / TotLength) + 8f, 0 + 19, StDist + dist * (ScaTotLength / TotLength) - 8f, 0 + 19);

                e.Graphics.DrawLine(dofpen, StDist + dist * (ScaTotLength / TotLength) - 8f, 0 + 19, StDist + dist * (ScaTotLength / TotLength), 0 + 25);
                e.Graphics.DrawLine(dofpen, StDist + dist * (ScaTotLength / TotLength), 0 + 19, StDist + dist * (ScaTotLength / TotLength) - 8f, 0 + 25);
                e.Graphics.DrawLine(dofpen, StDist + dist * (ScaTotLength / TotLength) + 8f, 0 + 19, StDist + dist * (ScaTotLength / TotLength), 0 + 25);
                e.Graphics.DrawLine(dofpen, StDist + dist * (ScaTotLength / TotLength), 0 + 19, StDist + dist * (ScaTotLength / TotLength) + 8f, 0 + 25);

                e.Graphics.DrawLine(dofpen, StDist + dist * (ScaTotLength / TotLength) - 8f, 0 + 25, StDist + dist * (ScaTotLength / TotLength) + 8f, 0 + 25);
            }
        }

        private void paintload(PaintEventArgs e)
        {
            // Dim linGrBrush As System.Drawing.Drawing2D.LinearGradientBrush
            float StDist;
            float ScaTotLength;
            float TotLength = 0f;
            float maxload = 0f;
            // ----Finding Maximum Load
            foreach (var itm in Indexes.mem)
            {
                TotLength = TotLength + itm.spanlength;
                // ----Point Load
                foreach (var Pitm in itm.Pload)
                {
                    if (Pitm.pload > maxload)
                    {
                        maxload = Pitm.pload;
                    }
                }
                // ----UVL
                foreach (var Uitm in itm.Uload)
                {
                    if (Uitm.uload1 > maxload)
                    {
                        maxload = Uitm.uload1;
                    }
                    if (Uitm.uload2 > maxload)
                    {
                        maxload = Uitm.uload2;
                    }
                }
            }
            StDist = (float)(-(coverpic.Width / 2d) + 100d);
            ScaTotLength = coverpic.Width - 200;

            float intSTdist = StDist;
            float dist = 0f;

            Pen loadpen;
            System.Drawing.Drawing2D.AdjustableArrowCap adcap;

            var temp = default(Member.U);
            var temp1 = default(Member.P);
            var temp2 = default(Member.M);
            foreach (var itm in Indexes.mem)
            {
                dist = dist + itm.spanlength;
                // ----UVL
                loadpen = new Pen(Color.DeepPink, 1f);
                adcap = new System.Drawing.Drawing2D.AdjustableArrowCap(2f, 4f);
                loadpen.CustomStartCap = adcap;
                int j = itm.Uload.Count - 1;
                for (int i = 0, loopTo = j; i <= loopTo; i++)
                {
                    float toSX1;
                    float toSX2;
                    float toSY1;
                    float toSY2;
                    toSX1 = intSTdist + itm.Uload[i].udist1 * (ScaTotLength / TotLength);
                    toSX2 = intSTdist + itm.Uload[i].udist2 * (ScaTotLength / TotLength);
                    toSY1 = itm.Uload[i].uload1 * (100f / maxload);
                    toSY2 = itm.Uload[i].uload2 * (100f / maxload);

                    if (itm.Uload[i].udist1 == selUL.udist1 & itm.Uload[i].uload1 == selUL.uload1 & itm.Uload[i].udist2 == selUL.udist2 & itm.Uload[i].uload2 == selUL.uload2)
                    {
                        loadpen = new Pen(Color.LightPink, 2f);

                        e.Graphics.DrawLine(loadpen, toSX1, 0 - 5, toSX1, 0 - toSY1);
                        e.Graphics.DrawString(itm.Uload[i].uload1.ToString(), Font, loadpen.Brush, toSX1, 0 - 20 - toSY1);
                    }
                    loadpen = new Pen(Color.DeepPink, 1f);
                    loadpen.CustomStartCap = adcap;
                    e.Graphics.DrawLine(loadpen, toSX1, 0, toSX1, 0 - toSY1);

                    // Defining the Rectangle for selecting the UVL Load
                    // e.Graphics.DrawRectangle(Pens.Black, R)

                    // ==================================================================================================================================


                    // Defining the Rectangle for selecting the UVL Load
                    // e.Graphics.DrawRectangle(Pens.Black, R)

                    // ==================================================================================================================================
                    e.Graphics.DrawString(itm.Uload[i].uload1.ToString(), Font, loadpen.Brush, toSX1, 0 - 20 - toSY1);
                    ;

                    // #error Cannot convert MultiLineIfBlockSyntax - see comment for details
                    /* Cannot convert MultiLineIfBlockSyntax, System.ArgumentException: Элемент с тем же ключом уже был добавлен.
                                           в System.ThrowHelper.ThrowArgumentException(ExceptionResource resource)
                                           в System.Collections.Generic.Dictionary`2.Insert(TKey key, TValue value, Boolean add)
                                           в ICSharpCode.CodeConverter.CSharp.PerScopeState.<CreateLocalsAsync>d__19.MoveNext()
                                        --- Конец трассировка стека из предыдущего расположения, где возникло исключение ---
                                           в System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
                                           в ICSharpCode.CodeConverter.CSharp.PerScopeStateVisitorDecorator.<AddLocalVariablesAsync>d__6.MoveNext()
                                        --- Конец трассировка стека из предыдущего расположения, где возникло исключение ---
                                           в System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
                                           в ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.<DefaultVisitInnerAsync>d__3.MoveNext()

                                        Input:

                                                        If toSY1 >= toSY2 Then
                                                            Dim temp As Global.CBAnsDes.Member.U
                                                            temp.uload1 = itm.Uload(i).uload1
                                                            temp.uload2 = itm.Uload(i).uload2
                                                            temp.udist1 = itm.Uload(i).udist1
                                                            temp.udist2 = itm.Uload(i).udist2

                                                            ' Defining the Rectangle for selecting the UVL Load
                                                            Dim R As New Global.System.Drawing.Rectangle((toSX1), ((0) - toSY1), (toSX2 - toSX1), toSY1)
                                                            temp.rect = [R]
                                                            'e.Graphics.DrawRectangle(Pens.Black, R)

                                                            '==================================================================================================================================

                                                            Dim ind As Integer = i
                                                            Global.CBAnsDes.Indexes.mem(Global.CBAnsDes.Indexes.mem.IndexOf(itm)).Uload.Insert(ind, temp)
                                                            Global.CBAnsDes.Indexes.mem(Global.CBAnsDes.Indexes.mem.IndexOf(itm)).Uload.RemoveAt(ind + 1)
                                                        Else
                                                            Dim temp As Global.CBAnsDes.Member.U
                                                            temp.uload1 = itm.Uload(i).uload1
                                                            temp.uload2 = itm.Uload(i).uload2
                                                            temp.udist1 = itm.Uload(i).udist1
                                                            temp.udist2 = itm.Uload(i).udist2

                                                            ' Defining the Rectangle for selecting the UVL Load
                                                            Dim R As New Global.System.Drawing.Rectangle((toSX1), ((0) - toSY2), (toSX2 - toSX1), toSY2)
                                                            temp.rect = [R]
                                                            'e.Graphics.DrawRectangle(Pens.Black, R)

                                                            '==================================================================================================================================
                                                            Dim ind As Integer = i
                                                            Global.CBAnsDes.Indexes.mem(Global.CBAnsDes.Indexes.mem.IndexOf(itm)).Uload.Insert(ind, temp)
                                                            Global.CBAnsDes.Indexes.mem(Global.CBAnsDes.Indexes.mem.IndexOf(itm)).Uload.RemoveAt(ind + 1)
                                                        End If

                                         */
                    if (itm.Uload[i].udist1 == selUL.udist1 & itm.Uload[i].uload1 == selUL.uload1 & itm.Uload[i].udist2 == selUL.udist2 & itm.Uload[i].uload2 == selUL.uload2)
                    {
                        if (toSY2 == toSY1)
                        {
                            for (float k = toSX1, loopTo1 = toSX2; k <= loopTo1; k += 10f)
                            {
                                loadpen = new Pen(Color.LightPink, 3f);
                                e.Graphics.DrawLine(loadpen, k, 0, k, 0 - toSY1);
                                loadpen = new Pen(Color.DeepPink, 1f);
                                loadpen.CustomStartCap = adcap;
                                e.Graphics.DrawLine(loadpen, k, 0, k, 0 - toSY1);
                            }
                        }
                        else if (toSY2 > toSY1)
                        {
                            for (float k = toSX1, loopTo3 = toSX2; k <= loopTo3; k += 10f)
                            {
                                loadpen = new Pen(Color.LightPink, 3f);
                                e.Graphics.DrawLine(loadpen, k, 0, k, 0 - (toSY1 + (toSY2 - toSY1) / (toSX2 - toSX1) * (k - toSX1)));
                                loadpen = new Pen(Color.DeepPink, 1f);
                                loadpen.CustomStartCap = adcap;
                                e.Graphics.DrawLine(loadpen, k, 0, k, 0 - (toSY1 + (toSY2 - toSY1) / (toSX2 - toSX1) * (k - toSX1)));
                            }
                        }
                        else
                        {
                            for (float k = toSX1, loopTo2 = toSX2; k <= loopTo2; k += 10f)
                            {
                                loadpen = new Pen(Color.LightPink, 3f);
                                e.Graphics.DrawLine(loadpen, k, 0, k, 0 - (toSY2 + (toSY1 - toSY2) / (toSX2 - toSX1) * (toSX2 - k)));
                                loadpen = new Pen(Color.DeepPink, 1f);
                                loadpen.CustomStartCap = adcap;
                                e.Graphics.DrawLine(loadpen, k, 0, k, 0 - (toSY2 + (toSY1 - toSY2) / (toSX2 - toSX1) * (toSX2 - k)));
                            }
                        }
                    }
                    else
                    {
                        loadpen = new Pen(Color.DeepPink, 1f);
                        loadpen.CustomStartCap = adcap;
                        if (toSY2 == toSY1)
                        {
                            for (float k = toSX1, loopTo4 = toSX2; k <= loopTo4; k += 10f)
                                e.Graphics.DrawLine(loadpen, k, 0, k, 0 - toSY1);
                        }
                        else if (toSY2 > toSY1)
                        {
                            for (float k = toSX1, loopTo6 = toSX2; k <= loopTo6; k += 10f)
                                e.Graphics.DrawLine(loadpen, k, 0, k, 0 - (toSY1 + (toSY2 - toSY1) / (toSX2 - toSX1) * (k - toSX1)));
                        }
                        else
                        {
                            for (float k = toSX1, loopTo5 = toSX2; k <= loopTo5; k += 10f)
                                e.Graphics.DrawLine(loadpen, k, 0, k, 0 - (toSY2 + (toSY1 - toSY2) / (toSX2 - toSX1) * (toSX2 - k)));
                        }
                    }


                    if (itm.Uload[i].udist1 == selUL.udist1 & itm.Uload[i].uload1 == selUL.uload1 & itm.Uload[i].udist2 == selUL.udist2 & itm.Uload[i].uload2 == selUL.uload2)
                    {
                        loadpen = new Pen(Color.LightPink, 2f);
                        e.Graphics.DrawLine(loadpen, toSX2, 0 - 5, toSX2, 0 - toSY2);
                    }
                    loadpen = new Pen(Color.DeepPink, 1f);
                    loadpen.CustomStartCap = adcap;
                    e.Graphics.DrawLine(loadpen, toSX2, 0, toSX2, 0 - toSY2);
                    e.Graphics.DrawString(itm.Uload[i].uload2.ToString(), Font, loadpen.Brush, toSX2, 0 - 20 - toSY2);
                }
                // ----Point Load
                j = itm.Pload.Count - 1;
                for (int i = 0, loopTo7 = j; i <= loopTo7; i++)
                {
                    float toSX;
                    float toSY;
                    toSX = intSTdist + itm.Pload[i].pdist * (ScaTotLength / TotLength);
                    toSY = itm.Pload[i].pload * (100f / maxload);
                    temp1.pload = itm.Pload[i].pload;
                    temp1.pdist = itm.Pload[i].pdist;

                    // Defining the Rectangle for selecting the Point Load
                    var R = new Rectangle((int)Math.Round(toSX - 4f), (int)Math.Round(0 - toSY), 8, (int)Math.Round(toSY));
                    temp1.rect = R;
                    // e.Graphics.DrawRectangle(Pens.Black, R)

                    // ==================================================================================================================================
                    int ind = i;
                    Indexes.mem[Indexes.mem.IndexOf(itm)].Pload.Insert(ind, temp1);
                    Indexes.mem[Indexes.mem.IndexOf(itm)].Pload.RemoveAt(ind + 1);

                    if (itm.Pload[i].pdist == selPL.pdist & itm.Pload[i].pload == selPL.pload)
                    {
                        loadpen = new Pen(Color.LightGreen, 4f);

                        e.Graphics.DrawLine(loadpen, toSX, 0 - 5, toSX, 0 - toSY);
                        e.Graphics.DrawString(itm.Pload[i].pload.ToString(), Font, loadpen.Brush, toSX, 0 - 20 - toSY);
                    }
                    loadpen = new Pen(Color.Green, 2f);
                    adcap = new System.Drawing.Drawing2D.AdjustableArrowCap(3f, 5f);
                    loadpen.CustomStartCap = adcap;

                    e.Graphics.DrawLine(loadpen, toSX, 0f, toSX, 0 - toSY);
                    e.Graphics.DrawString(itm.Pload[i].pload.ToString(), Font, loadpen.Brush, toSX, 0 - 20 - toSY);

                }
                // ----Moment
                loadpen = new Pen(Color.Orange, 2f);
                adcap = new System.Drawing.Drawing2D.AdjustableArrowCap(3f, 5f);
                j = itm.Mload.Count - 1;
                for (int i = 0, loopTo8 = j; i <= loopTo8; i++)
                {
                    float toSX;
                    toSX = intSTdist + itm.Mload[i].mdist * (ScaTotLength / TotLength);

                    if (itm.Mload[i].mdist == selML.mdist & itm.Mload[i].mload == selML.mload)
                    {
                        loadpen = new Pen(Color.Yellow, 4f);
                        e.Graphics.DrawArc(loadpen, toSX - 30f, 0 - 30, 60f, 60f, 270f, 180f);
                    }
                    loadpen = new Pen(Color.Orange, 2f);
                    if (itm.Mload[i].mload > 0f)
                    {
                        loadpen.CustomStartCap = adcap;
                    }
                    else
                    {
                        loadpen.CustomEndCap = adcap;
                    }
                    temp2.mload = itm.Mload[i].mload;
                    temp2.mdist = itm.Mload[i].mdist;

                    // Defining the Rectangle for selecting the Moment Load
                    var R = new Rectangle((int)Math.Round(toSX), 0 - 30, 30, 60);
                    temp2.rect = R;
                    // e.Graphics.DrawRectangle(Pens.Black, R)

                    // ==================================================================================================================================

                    int ind = i;
                    Indexes.mem[Indexes.mem.IndexOf(itm)].Mload.Insert(ind, temp2);
                    Indexes.mem[Indexes.mem.IndexOf(itm)].Mload.RemoveAt(ind + 1);

                    e.Graphics.DrawArc(loadpen, toSX - 30f, 0 - 30, 60f, 60f, 270f, 180f);
                    e.Graphics.DrawString(Math.Abs(itm.Mload[i].mload).ToString(), Font, loadpen.Brush, toSX, 0 - 50);
                }
                intSTdist = StDist + dist * (ScaTotLength / TotLength);
            }
            // --Check For Load Selection 
            // For Each itm In mem
            // For Each Pitm In itm.Pload
            // e.Graphics.DrawRectangle(Pens.DarkRed, Pitm.rect)
            // Next
            // For Each uitm In itm.Uload
            // e.Graphics.DrawRectangle(Pens.DarkRed, uitm.rect)
            // Next
            // For Each mitm In itm.Mload
            // e.Graphics.DrawRectangle(Pens.DarkRed, mitm.rect)
            // Next
            // Next
        }

        #endregion

        #region Mainpic mouse click events
        private void mainpic_MouseClick(object sender, MouseEventArgs e)
        {
            // --Null selected load
            var ptm = new Member.P();
            var utm = new Member.U();
            var mtm = new Member.M();
            selPL = ptm;
            selUL = utm;
            selML = mtm;
            // ---Line Select
            double TempX, TempY;
            TempX = e.X / Indexes.Zm - Indexes.MidPt.X;
            TempY = e.Y / Indexes.Zm - Indexes.MidPt.Y;

            // ' MessageBox.Show(TempX.ToString() + ", " + TempY.ToString())

            if (TempY > -5) // Select member
            {
                Indexes.Lselline = -1;
                if (e.Button == MouseButtons.Left)
                {
                    foreach (var itm in Indexes.mem)
                    {
                        if (itm.rect.Contains((int)Math.Round(TempX), (int)Math.Round(TempY)))
                        {
                            Indexes.selline = Indexes.mem.IndexOf(itm);
                            toolstrip1Mod();
                            mainpic.Refresh();
                            return;
                        }
                    }
                }
                else if (e.Button == MouseButtons.Right)
                {
                    foreach (var itm in Indexes.mem)
                    {
                        if (itm.rect.Contains((int)Math.Round(TempX), (int)Math.Round(TempY)))
                        {
                            Indexes.selline = Indexes.mem.IndexOf(itm);
                        }
                    }
                    if (Indexes.selline == -1)
                    {
                        AddMemberToolStripMenuItem1.Enabled = true;
                        EditEndsToolStripMenuItem.Enabled = true;
                        EditMemebrToolStripMenuItem.Enabled = false;
                        RemoveMemberToolStripMenuItem.Enabled = false;
                        AddLoadToolStripMenuItem1.Enabled = false;
                        RemoveLoadsToolStripMenuItem.Enabled = false;
                    }
                    else
                    {
                        AddMemberToolStripMenuItem1.Enabled = false;
                        EditEndsToolStripMenuItem.Enabled = false;
                        EditMemebrToolStripMenuItem.Enabled = true;
                        RemoveMemberToolStripMenuItem.Enabled = true;
                        AddLoadToolStripMenuItem1.Enabled = true;
                        RemoveLoadsToolStripMenuItem.Enabled = true;
                    }
                    ContextMenuStrip1.Show(mainpic.PointToScreen(e.Location));
                    if (Indexes.selline != -1)
                    {
                        mainpic.Refresh();
                        return;
                    }
                }
            }
            else
            {
                Indexes.selline = -1;
                Indexes.Tselline = -1;
                // --Load Selection Procedure
                if (e.Button == MouseButtons.Left)
                {
                    foreach (var itm in Indexes.mem)
                    {
                        if (itm.rect.Contains((int)Math.Round(TempX), (int)Math.Round(TempY)) == true)
                        {
                            Indexes.Lselline = Indexes.mem.IndexOf(itm);
                            foreach (var pitm in itm.Pload)
                            {
                                if (pitm.rect.Contains((int)Math.Round(TempX), (int)Math.Round(TempY)))
                                {
                                    var uutm = new Member.U();
                                    var mmtm = new Member.M();
                                    selUL = uutm;
                                    selML = mmtm;
                                    selPL = pitm;
                                    tipe = 1;
                                    toolstrip1Mod();
                                    mainpic.Refresh();
                                    return;
                                }
                            }
                            foreach (var mitm in itm.Mload)
                            {
                                if (mitm.rect.Contains((int)Math.Round(TempX), (int)Math.Round(TempY)))
                                {
                                    var pptm = new Member.P();
                                    var uutm = new Member.U();
                                    selPL = pptm;
                                    selUL = uutm;
                                    selML = mitm;
                                    tipe = 3;
                                    toolstrip1Mod();
                                    mainpic.Refresh();
                                    return;
                                }
                            }
                            foreach (var uitm in itm.Uload)
                            {
                                if (uitm.rect.Contains((int)Math.Round(TempX), (int)Math.Round(TempY)))
                                {
                                    var pptm = new Member.P();
                                    var mmtm = new Member.M();
                                    selPL = pptm;
                                    selML = mmtm;
                                    selUL = uitm;
                                    tipe = 2;
                                    toolstrip1Mod();
                                    mainpic.Refresh();
                                    return;
                                }
                            }
                            Indexes.selline = -1;
                            Indexes.Lselline = -1;
                        }
                    }
                }
                else if (e.Button == MouseButtons.Right)
                {
                    foreach (var itm in Indexes.mem)
                    {
                        if (itm.rect.Contains((int)Math.Round(TempX), (int)Math.Round(TempY)) == true)
                        {
                            Indexes.Lselline = Indexes.mem.IndexOf(itm);
                            foreach (var pitm in itm.Pload)
                            {
                                if (pitm.rect.Contains((int)Math.Round(TempX), (int)Math.Round(TempY)))
                                {
                                    selPL = pitm;
                                    tipe = 1;
                                    goto r1;
                                }
                            }
                            foreach (var uitm in itm.Uload)
                            {
                                if (uitm.rect.Contains((int)Math.Round(TempX), (int)Math.Round(TempY)))
                                {
                                    selUL = uitm;
                                    tipe = 2;
                                    goto r1;
                                }
                            }
                            foreach (var mitm in itm.Mload)
                            {
                                if (mitm.rect.Contains((int)Math.Round(TempX), (int)Math.Round(TempY)))
                                {
                                    selML = mitm;
                                    tipe = 3;
                                    goto r1;
                                }
                            }
                            Indexes.Lselline = -1;
                            Indexes.selline = -1;
                            continue;
                        r1:
                            ;

                            mainpic.Refresh();
                            AddMemberToolStripMenuItem.Enabled = false;
                            EditEndsToolStripMenuItem1.Enabled = false;
                            RemoveLoadToolStripMenuItem.Enabled = true;
                            EditLoadToolStripMenuItem.Enabled = true;
                            ContextMenuStrip2.Show(mainpic.PointToScreen(e.Location));
                            return;
                        }
                    }
                    mainpic.Refresh();
                    AddMemberToolStripMenuItem.Enabled = true;
                    EditEndsToolStripMenuItem1.Enabled = true;
                    RemoveLoadToolStripMenuItem.Enabled = false;
                    EditLoadToolStripMenuItem.Enabled = false;
                    ContextMenuStrip2.Show(mainpic.PointToScreen(e.Location));
                }
            }
            Indexes.selline = -1;
            toolstrip1Mod();
            mainpic.Refresh();
        }

        private void mainpic_MouseDown(object sender, MouseEventArgs e)
        {
            // ------ Mouse down for Click and drag
            if (e.Button == MouseButtons.Middle)
            {
                // ---- Pan Operation
                isMiddleDrag = true;
                PanStartDrag = new Point((int)Math.Round(e.X / Indexes.Zm - Indexes.MidPt.X), (int)Math.Round(e.Y / Indexes.Zm - Indexes.MidPt.Y));
            }

            double TempX, TempY;
            TempX = e.X / Indexes.Zm - Indexes.MidPt.X;
            TempY = e.Y / Indexes.Zm - Indexes.MidPt.Y;

            if (TempY > -5) // Select member
            {
                Indexes.Tselline = -1;
                Indexes.selline = -1;
                if (e.Button == MouseButtons.Left)
                {
                    foreach (var itm in Indexes.mem)
                    {
                        if (itm.rect.Contains((int)Math.Round(TempX), (int)Math.Round(TempY)))
                        {
                            Indexes.Tselline = Indexes.mem.IndexOf(itm);
                            mainpic.Refresh();
                            return;
                        }
                    }
                    Indexes.Tselline = -1;
                }
            }

        }

        private void mainpic_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                double TempX, TempY;
                TempX = e.X / Indexes.Zm - Indexes.MidPt.X;
                TempY = e.Y / Indexes.Zm - Indexes.MidPt.Y;

                if (TempY > -5) // Select member
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        foreach (var itm in Indexes.mem)
                        {
                            if (itm.rect.Contains((int)Math.Round(TempX), (int)Math.Round(TempY)))
                            {
                                if (Indexes.selline == Indexes.Tselline | Indexes.Tselline == -1)
                                {
                                    return;
                                }
                                if (Indexes.Tselline > Indexes.selline)
                                {
                                    Indexes.mem.Insert(Indexes.selline, Indexes.mem[Indexes.Tselline]);
                                    Indexes.mem.RemoveAt(Indexes.Tselline + 1);
                                }
                                else
                                {
                                    Indexes.mem.Insert(Indexes.Tselline, Indexes.mem[Indexes.selline]);
                                    Indexes.mem.RemoveAt(Indexes.selline + 1);
                                }
                                Indexes.Tselline = -1;
                                mainpic.Refresh();
                                return;
                            }
                        }
                    }
                }

                // ------ Mouse up for Click and drag
                if (e.Button == MouseButtons.Middle)
                {
                    // --- Pan Operation Stops
                    isMiddleDrag = false;
                    mainpic.Refresh();
                }
            }

            catch (Exception ex)
            {
                Interaction.MsgBox("Error");
            }

        }


        private void mainpic_DoubleClick(object sender, EventArgs e)
        {
            // Zoom to Fit
            Indexes.MidPt = new Point((int)Math.Round(mainpic.Width / 2d), (int)Math.Round(mainpic.Height / 2d));
            Indexes.Zm = 1d;
            My.MyProject.Forms.MDIMain.ToolStripLabel1.Text = Indexes.Zm * 100d + "%";
            My.MyProject.Forms.MDIMain.ToolStripLabel2.Text = Indexes.Zm * 100d + "%";

            mainpic.Refresh();
        }

        public void toolstrip1Mod()
        {
            if (Indexes.selline == -1 & Indexes.Lselline == -1)
            {
                My.MyProject.Forms.MDIMain.ToolStripButton1.Enabled = true;
                My.MyProject.Forms.MDIMain.AddMemberToolStripMenuItem.Enabled = true;
                My.MyProject.Forms.MDIMain.ToolStripButton2.Enabled = true;
                My.MyProject.Forms.MDIMain.BeamEndsToolStripMenuItem.Enabled = true;
                My.MyProject.Forms.MDIMain.ToolStripButton3.Enabled = false;
                My.MyProject.Forms.MDIMain.editMemberToolStripMenuItem.Enabled = false;
                My.MyProject.Forms.MDIMain.ToolStripButton4.Enabled = false;
                My.MyProject.Forms.MDIMain.RemoveMemberToolStripMenuItem.Enabled = false;
                My.MyProject.Forms.MDIMain.ToolStripButton5.Enabled = false;
                My.MyProject.Forms.MDIMain.AddLoadToolStripMenuItem.Enabled = false;
                My.MyProject.Forms.MDIMain.ToolStripButton6.Enabled = false;
                My.MyProject.Forms.MDIMain.RemoveLoadsToolStripMenuItem.Enabled = false;
                My.MyProject.Forms.MDIMain.ToolStripButton7.Enabled = false;
                My.MyProject.Forms.MDIMain.ModifyLoadToolStripMenuItem.Enabled = false;
                My.MyProject.Forms.MDIMain.ToolStripButton8.Enabled = false;
                My.MyProject.Forms.MDIMain.RemoveLoadToolStripMenuItem.Enabled = false;
            }
            else if (Indexes.Lselline == -1)
            {
                My.MyProject.Forms.MDIMain.ToolStripButton1.Enabled = false;
                My.MyProject.Forms.MDIMain.AddMemberToolStripMenuItem.Enabled = false;
                My.MyProject.Forms.MDIMain.ToolStripButton2.Enabled = false;
                My.MyProject.Forms.MDIMain.BeamEndsToolStripMenuItem.Enabled = false;
                My.MyProject.Forms.MDIMain.ToolStripButton3.Enabled = true;
                My.MyProject.Forms.MDIMain.editMemberToolStripMenuItem.Enabled = true;
                My.MyProject.Forms.MDIMain.ToolStripButton4.Enabled = true;
                My.MyProject.Forms.MDIMain.RemoveMemberToolStripMenuItem.Enabled = true;
                My.MyProject.Forms.MDIMain.ToolStripButton5.Enabled = true;
                My.MyProject.Forms.MDIMain.AddLoadToolStripMenuItem.Enabled = true;
                My.MyProject.Forms.MDIMain.ToolStripButton6.Enabled = true;
                My.MyProject.Forms.MDIMain.RemoveLoadsToolStripMenuItem.Enabled = true;
                My.MyProject.Forms.MDIMain.ToolStripButton7.Enabled = false;
                My.MyProject.Forms.MDIMain.ModifyLoadToolStripMenuItem.Enabled = false;
                My.MyProject.Forms.MDIMain.ToolStripButton8.Enabled = false;
                My.MyProject.Forms.MDIMain.RemoveLoadToolStripMenuItem.Enabled = false;
            }
            else if (Indexes.Lselline != -1)
            {
                My.MyProject.Forms.MDIMain.ToolStripButton1.Enabled = false;
                My.MyProject.Forms.MDIMain.AddMemberToolStripMenuItem.Enabled = false;
                My.MyProject.Forms.MDIMain.ToolStripButton2.Enabled = false;
                My.MyProject.Forms.MDIMain.BeamEndsToolStripMenuItem.Enabled = false;
                My.MyProject.Forms.MDIMain.ToolStripButton3.Enabled = false;
                My.MyProject.Forms.MDIMain.editMemberToolStripMenuItem.Enabled = false;
                My.MyProject.Forms.MDIMain.ToolStripButton4.Enabled = false;
                My.MyProject.Forms.MDIMain.RemoveMemberToolStripMenuItem.Enabled = false;
                My.MyProject.Forms.MDIMain.ToolStripButton5.Enabled = false;
                My.MyProject.Forms.MDIMain.AddLoadToolStripMenuItem.Enabled = false;
                My.MyProject.Forms.MDIMain.ToolStripButton6.Enabled = false;
                My.MyProject.Forms.MDIMain.RemoveLoadsToolStripMenuItem.Enabled = false;
                My.MyProject.Forms.MDIMain.ToolStripButton7.Enabled = true;
                My.MyProject.Forms.MDIMain.ModifyLoadToolStripMenuItem.Enabled = true;
                My.MyProject.Forms.MDIMain.ToolStripButton8.Enabled = true;
                My.MyProject.Forms.MDIMain.RemoveLoadToolStripMenuItem.Enabled = true;
            }
        }
        #endregion

        #region Mainpic Zoom events
        private void mainpic_MouseEnter(object sender, EventArgs e)
        {
            mainpic.Focus();
        }

        private void mainpic_MouseLeave(object sender, EventArgs e)
        {
            My.MyProject.Forms.MDIMain.ToolStrip1.Focus();
        }

        // Private Sub HScrollBar1_Scroll(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ScrollEventArgs)
        // mainpic.Left = -(HScrollBar1.Value)
        // respic.Left = -(HScrollBar1.Value)
        // End Sub

        // Private Sub VScrollBar1_Scroll(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ScrollEventArgs)
        // mainpic.Top = -(VScrollBar1.Value)
        // respic.Top = -(VScrollBar1.Value)
        // End Sub

        private void mainpic_MouseMove(object sender, MouseEventArgs e)
        {
            // ------ Mouse move drag
            if (isMiddleDrag == true)
            {
                // ---- Pan Operation 
                Indexes.MidPt = new Point((int)Math.Round(e.X / Indexes.Zm - PanStartDrag.X), (int)Math.Round(e.Y / Indexes.Zm - PanStartDrag.Y));
                mainpic.Refresh();
            }
        }

        private void mainpic_MouseWheel(object sender, MouseEventArgs e)
        {
            double xw, yw;
            mainpic.Focus();
            xw = e.X / Indexes.Zm - Indexes.MidPt.X;
            yw = e.Y / Indexes.Zm - Indexes.MidPt.Y;
            if (e.Delta > 0)
            {
                if (Indexes.Zm < 100d)
                {
                    Indexes.Zm = Indexes.Zm + 0.1d;
                }
            }
            else if (e.Delta < 0)
            {
                if (Indexes.Zm > 0.101d)
                {
                    Indexes.Zm = Indexes.Zm - 0.1d;
                    // ElseIf Zm <= 0.1 Then
                    // If Zm > 0.0101 Then
                    // Zm = Zm - 0.01
                    // ElseIf Zm <= 0.01 Then
                    // If Zm > 0.00101 Then
                    // Zm = Zm - 0.001
                    // End If
                    // End If
                }
            }
            Indexes.MidPt.X = (int)Math.Round(e.X / Indexes.Zm - xw);
            Indexes.MidPt.Y = (int)Math.Round(e.Y / Indexes.Zm - yw);
            // MTPic.Refresh()


            // mainpic.SuspendLayout()
            // Dim xw, yw As Single
            // xw = e.X / Zm
            // yw = e.Y / Zm
            // If e.Delta > 0 Then
            // If Zm < 10 Then
            // Zm = Zm + 0.25
            // End If
            // Else
            // If Zm > 1 Then
            // Zm = Zm - 0.25
            // End If
            // End If
            // mainpic.Refresh()
            // If e.X <> xw * Zm Then
            // mainpic.Width = 1600 * Zm
            // mainpic.Height = ht * Zm
            // ' HScrollBar1.Maximum = (1600 * Zm) - coverpic.Width
            // ' VScrollBar1.Maximum = (1600 * Zm) - coverpic.Height

            // xw = -(mainpic.Left - ((xw * Zm) - e.X))
            // yw = -(mainpic.Top - ((yw * Zm) - e.Y))

            // 'If xw <= HScrollBar1.Minimum Then
            // '    mainpic.Left = -1
            // '    HScrollBar1.Value = 50
            // 'ElseIf xw >= HScrollBar1.Maximum Then
            // '    mainpic.Left = -HScrollBar1.Maximum
            // '    HScrollBar1.Value = HScrollBar1.Maximum
            // 'Else
            // '    mainpic.Left = -xw
            // '    HScrollBar1.Value = xw
            // 'End If
            // mainpic.Refresh()
            // 'If yw <= VScrollBar1.Minimum Then
            // '    mainpic.Top = -1
            // '    VScrollBar1.Value = 100
            // 'ElseIf yw >= VScrollBar1.Maximum Then
            // '    mainpic.Top = -VScrollBar1.Maximum
            // '    VScrollBar1.Value = VScrollBar1.Maximum
            // 'Else
            // '    mainpic.Top = -yw
            // '    VScrollBar1.Value = yw
            // 'End If

            // ' mainpic.Top = -(((ht * Zm) - ht) / 2)
            // mainpic.Invalidate()

            // mainpic.ResumeLayout()
            mainpic.Refresh();
            My.MyProject.Forms.MDIMain.ToolStripLabel1.Text = Indexes.Zm * 100d + "%";
            My.MyProject.Forms.MDIMain.ToolStripLabel2.Text = Indexes.Zm * 100d + "%";
            // End If
        }
        #endregion

        #region Context menu strip1 Events

        private void AddMemberToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var a = new addmember();
            a.Text = "Add Member";
            a.ShowDialog();
        }

        private void EditEndsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var a = new Ends_Editor();
            a.ShowDialog();
        }

        private void EditMemebrToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var a = new addmember();
            a.TextBox1.Text = Indexes.mem[Indexes.selline].spanlength.ToString();
            a.TextBox2.Text = Indexes.mem[Indexes.selline].Emodulus.ToString();
            a.TextBox3.Text = Indexes.mem[Indexes.selline].Inertia.ToString();
            a.TextBox6.Text = Indexes.mem[Indexes.selline].g.ToString();
            a.Text = "Modify Member";
            a.ShowDialog();
        }

        private void RemoveMemberToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
                toolstrip1Mod();
                mainpic.Refresh();
            }
        }

        private void AddLoadToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var a = new LoadWindow(Indexes.mem[Indexes.selline].spanlength, Indexes.selline);
            a.ShowDialog();
        }

        private void RemoveLoadsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // --ALL Loads Are Removed
            Indexes.mem[Indexes.selline].Pload.Clear();
            Indexes.mem[Indexes.selline].Uload.Clear();
            Indexes.mem[Indexes.selline].Mload.Clear();

            mainpic.Refresh();
        }

        private void EditLoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tipe == 1)
            {
                var a = new LoadWindow(Indexes.mem[Indexes.Lselline].spanlength, Indexes.Lselline, selPL);
                a.ShowDialog();
            }
            else if (tipe == 2)
            {
                var a = new LoadWindow(Indexes.mem[Indexes.Lselline].spanlength, Indexes.Lselline, selUL);
                a.ShowDialog();
            }
            else if (tipe == 3)
            {
                var a = new LoadWindow(Indexes.mem[Indexes.Lselline].spanlength, Indexes.Lselline, selML);
                a.ShowDialog();
            }
            mainpic.Refresh();
        }

        private void RemoveLoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // --Single Load is removed
            if (tipe == 1)
            {
                Indexes.mem[Indexes.Lselline].Pload.Remove(selPL);
            }
            else if (tipe == 2)
            {
                Indexes.mem[Indexes.Lselline].Uload.Remove(selUL);
            }
            else if (tipe == 3)
            {
                Indexes.mem[Indexes.Lselline].Mload.Remove(selML);
            }
            // --Null selected load
            var ptm = new Member.P();
            var utm = new Member.U();
            var mtm = new Member.M();
            selPL = ptm;
            selUL = utm;
            selML = mtm;
            Indexes.selline = -1;
            Indexes.Lselline = -1;
            toolstrip1Mod();
            mainpic.Refresh();
        }

        private void AddMemberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var a = new addmember();
            a.Text = "Add Member";
            a.ShowDialog();
        }

        private void EditEndsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var a = new Ends_Editor();
            a.ShowDialog();
        }

        #endregion
        #endregion

        #region Respic Events
        #region Respic Paint Events
        private void respic_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.ScaleTransform((float)Indexes.Zm, (float)Indexes.Zm);
            e.Graphics.TranslateTransform(Indexes.MidPt.X, Indexes.MidPt.Y);

            // '---Grids (Paint grid removed in 2021 version)
            // For i = 0 To 1600 Step 100
            // e.Graphics.DrawLine(Pens.Beige, i, 0, i, ht)
            // e.Graphics.DrawLine(Pens.Beige, 0, i, 1600, i)
            // 'e.Graphics.DrawString(i, Font, Brushes.Brown, i, ht * (8 / 10))
            // Next

            rpaintBeam(e);
            rpaintEnds(e);
            rpaintSupport(e);
            rpaintload(e);

            if (My.MyProject.Forms.MDIMain.ShearForceDiagramToolStripMenuItem.Checked == true)
            {
                rShearPaint(e);
            }
            else if (My.MyProject.Forms.MDIMain.BendingMomentDiagramToolStripMenuItem.Checked == true)
            {
                rMomentPaint(e);
            }
            else if (My.MyProject.Forms.MDIMain.DeflectionToolStripMenuItem.Checked == true)
            {
                rDeflectionPaint(e);
            }
            else if (My.MyProject.Forms.MDIMain.SlopeToolStripMenuItem.Checked == true)
            {
                rSlopePaint(e);
            }
        }

        private void rpaintBeam(PaintEventArgs e)
        {
            var BeamPen = new Pen(Color.LightGray, 2f);
            var dimpen = new Pen(Color.LightGray, 1.5f);
            System.Drawing.Drawing2D.LinearGradientBrush linGrBrush;
            float StDist;
            float ScaTotLength;
            float TotLength = 0f;

            foreach (var itm in Indexes.mem)
                TotLength = TotLength + itm.spanlength;

            StDist = (float)(-(coverpic.Width / 2d) + 100d);
            ScaTotLength = coverpic.Width - 200;

            e.Graphics.DrawLine(BeamPen, StDist, 0f, StDist + ScaTotLength, 0f);
            e.Graphics.DrawLine(Pens.LightGray, StDist, ht / 2f + 100f, StDist, ht / 2f + 140f);
            float Idist = StDist;
            float dist = 0f;
            foreach (var itm in Indexes.mem)
            {
                dist = dist + itm.spanlength;
                // ---Dimension Line
                var adcap = new System.Drawing.Drawing2D.AdjustableArrowCap(3f, 5f);
                linGrBrush = new System.Drawing.Drawing2D.LinearGradientBrush(new Point((int)Math.Round(Idist), (int)Math.Round(ht / 2f + 120f)), new Point((int)Math.Round(StDist + dist * (ScaTotLength / TotLength)), (int)Math.Round(ht / 2f + 120f)), Color.LightGray, Color.WhiteSmoke);
                linGrBrush.SetSigmaBellShape(0.5f, 1f);
                dimpen.Brush = linGrBrush;
                dimpen.CustomStartCap = adcap;
                dimpen.CustomEndCap = adcap;
                e.Graphics.DrawLine(dimpen, Idist, ht / 2f + 120f, StDist + dist * (ScaTotLength / TotLength), ht / 2f + 120f);
                e.Graphics.DrawString(itm.spanlength.ToString(), Font, Brushes.LightGray, (StDist + dist * (ScaTotLength / TotLength) + Idist) / 2f, ht / 2f + 115f);
                Idist = StDist + dist * (ScaTotLength / TotLength);
            }
        }

        private void rpaintEnds(PaintEventArgs e)
        {
            float StDist;
            float ScaTotLength;
            float TotLength = 0f;
            var dofpen = new Pen(Color.LightGray, 2f);
            System.Drawing.Drawing2D.LinearGradientBrush linGrBrush;

            foreach (var itm in Indexes.mem)
                TotLength = TotLength + itm.spanlength;
            StDist = (float)(-(coverpic.Width / 2d) + 100d);
            ScaTotLength = coverpic.Width - 200;

            // ----Ends
            if (Indexes.ends == 1) // Fixed-Fixed
            {
                // ---end1
                linGrBrush = new System.Drawing.Drawing2D.LinearGradientBrush(new Point((int)Math.Round(StDist - 15f), 0), new Point((int)Math.Round(StDist - 32f), 0), Color.WhiteSmoke, Color.LightGray);
                e.Graphics.DrawRectangle(Pens.WhiteSmoke, StDist - 15f, 0 - 35, 15f, 70f);
                e.Graphics.FillRectangle(linGrBrush, StDist - 15f, 0 - 35, 15f, 70f);
                // ---end2
                linGrBrush = new System.Drawing.Drawing2D.LinearGradientBrush(new Point((int)Math.Round(StDist + ScaTotLength), 0), new Point((int)Math.Round(StDist + ScaTotLength + 15f), 0), Color.WhiteSmoke, Color.LightGray);
                e.Graphics.DrawRectangle(Pens.WhiteSmoke, StDist + ScaTotLength, 0 - 35, 15f, 70f);
                e.Graphics.FillRectangle(linGrBrush, StDist + ScaTotLength, 0 - 35, 15f, 70f);
            }
            else if (Indexes.ends == 2) // Fixed-Free
            {
                // ---end1
                linGrBrush = new System.Drawing.Drawing2D.LinearGradientBrush(new Point((int)Math.Round(StDist - 15f), 0), new Point((int)Math.Round(StDist - 32f), 0), Color.WhiteSmoke, Color.LightGray);
                e.Graphics.DrawRectangle(Pens.WhiteSmoke, StDist - 15f, 0 - 35, 15f, 70f);
                e.Graphics.FillRectangle(linGrBrush, StDist - 15f, 0 - 35, 15f, 70f);
            }

            else if (Indexes.ends == 3) // Pinned-Pinned
            {
                // ---end1
                e.Graphics.DrawLine(dofpen, StDist, 0 + 3, StDist - 8f, 0 + 19);
                e.Graphics.DrawLine(dofpen, StDist, 0 + 3, StDist + 8f, 0 + 19);
                e.Graphics.DrawLine(dofpen, StDist + 8f, 0 + 19, StDist - 8f, 0 + 19);

                e.Graphics.DrawLine(dofpen, StDist - 8f, 0 + 19, StDist, 0 + 25);
                e.Graphics.DrawLine(dofpen, StDist, 0 + 19, StDist - 8f, 0 + 25);
                e.Graphics.DrawLine(dofpen, StDist + 8f, 0 + 19, StDist, 0 + 25);
                e.Graphics.DrawLine(dofpen, StDist, 0 + 19, StDist + 8f, 0 + 25);

                e.Graphics.DrawLine(dofpen, StDist - 8f, 0 + 25, StDist + 8f, 0 + 25);
                // ---end2
                e.Graphics.DrawLine(dofpen, StDist + ScaTotLength, 0 + 3, StDist + ScaTotLength - 8f, 0 + 19);
                e.Graphics.DrawLine(dofpen, StDist + ScaTotLength, 0 + 3, StDist + ScaTotLength + 8f, 0 + 19);
                e.Graphics.DrawLine(dofpen, StDist + ScaTotLength + 8f, 0 + 19, StDist + ScaTotLength - 8f, 0 + 19);

                e.Graphics.DrawLine(dofpen, StDist + ScaTotLength - 8f, 0 + 19, StDist + ScaTotLength, 0 + 25);
                e.Graphics.DrawLine(dofpen, StDist + ScaTotLength, 0 + 19, StDist + ScaTotLength - 8f, 0 + 25);
                e.Graphics.DrawLine(dofpen, StDist + ScaTotLength + 8f, 0 + 19, StDist + ScaTotLength, 0 + 25);
                e.Graphics.DrawLine(dofpen, StDist + ScaTotLength, 0 + 19, StDist + ScaTotLength + 8f, 0 + 25);

                e.Graphics.DrawLine(dofpen, StDist + ScaTotLength - 8f, 0 + 25, StDist + ScaTotLength + 8f, 0 + 25);
            }
            // ElseIf ends = 4 Then 'Free-Free
            // ----
            else if (Indexes.ends == 5) // Fixed-Pinned
            {
                // ---end1
                linGrBrush = new System.Drawing.Drawing2D.LinearGradientBrush(new Point((int)Math.Round(StDist - 15f), 0), new Point((int)Math.Round(StDist - 32f), 0), Color.WhiteSmoke, Color.LightGray);
                e.Graphics.DrawRectangle(Pens.WhiteSmoke, StDist - 15f, 0 - 35, 15f, 70f);
                e.Graphics.FillRectangle(linGrBrush, StDist - 15f, 0 - 35, 15f, 70f);
                // ---end2
                e.Graphics.DrawLine(dofpen, StDist + ScaTotLength, 0 + 3, StDist + ScaTotLength - 8f, 0 + 19);
                e.Graphics.DrawLine(dofpen, StDist + ScaTotLength, 0 + 3, StDist + ScaTotLength + 8f, 0 + 19);
                e.Graphics.DrawLine(dofpen, StDist + ScaTotLength + 8f, 0 + 19, StDist + ScaTotLength - 8f, 0 + 19);

                e.Graphics.DrawLine(dofpen, StDist + ScaTotLength - 8f, 0 + 19, StDist + ScaTotLength, 0 + 25);
                e.Graphics.DrawLine(dofpen, StDist + ScaTotLength, 0 + 19, StDist + ScaTotLength - 8f, 0 + 25);
                e.Graphics.DrawLine(dofpen, StDist + ScaTotLength + 8f, 0 + 19, StDist + ScaTotLength, 0 + 25);
                e.Graphics.DrawLine(dofpen, StDist + ScaTotLength, 0 + 19, StDist + ScaTotLength + 8f, 0 + 25);

                e.Graphics.DrawLine(dofpen, StDist + ScaTotLength - 8f, 0 + 25, StDist + ScaTotLength + 8f, 0 + 25);
            }
            else if (Indexes.ends == 6)
            {
                // ---end1
                e.Graphics.DrawLine(dofpen, StDist, 0 + 3, StDist - 8f, 0 + 19);
                e.Graphics.DrawLine(dofpen, StDist, 0 + 3, StDist + 8f, 0 + 19);
                e.Graphics.DrawLine(dofpen, StDist + 8f, 0 + 19, StDist - 8f, 0 + 19);

                e.Graphics.DrawLine(dofpen, StDist - 8f, 0 + 19, StDist, 0 + 25);
                e.Graphics.DrawLine(dofpen, StDist, 0 + 19, StDist - 8f, 0 + 25);
                e.Graphics.DrawLine(dofpen, StDist + 8f, 0 + 19, StDist, 0 + 25);
                e.Graphics.DrawLine(dofpen, StDist, 0 + 19, StDist + 8f, 0 + 25);

                e.Graphics.DrawLine(dofpen, StDist - 8f, 0 + 25, StDist + 8f, 0 + 25);
            }
        }

        private void rpaintSupport(PaintEventArgs e)
        {
            var dofpen = new Pen(Color.LightGray, 2f);
            float StDist;
            float ScaTotLength;
            float TotLength = 0f;

            foreach (var itm in Indexes.mem)
                TotLength = TotLength + itm.spanlength;
            StDist = (float)(-(coverpic.Width / 2d) + 100d);
            ScaTotLength = coverpic.Width - 200;

            var dist = default(float);
            foreach (var itm in Indexes.mem)
            {
                dist = dist + itm.spanlength;
                if (Indexes.mem.IndexOf(itm) == Indexes.mem.Count - 1)
                {
                    return;
                }
                e.Graphics.DrawLine(dofpen, StDist + dist * (ScaTotLength / TotLength), 0 + 3, StDist + dist * (ScaTotLength / TotLength) - 8f, 0 + 19);
                e.Graphics.DrawLine(dofpen, StDist + dist * (ScaTotLength / TotLength), 0 + 3, StDist + dist * (ScaTotLength / TotLength) + 8f, 0 + 19);
                e.Graphics.DrawLine(dofpen, StDist + dist * (ScaTotLength / TotLength) + 8f, 0 + 19, StDist + dist * (ScaTotLength / TotLength) - 8f, 0 + 19);

                e.Graphics.DrawLine(dofpen, StDist + dist * (ScaTotLength / TotLength) - 8f, 0 + 19, StDist + dist * (ScaTotLength / TotLength), 0 + 25);
                e.Graphics.DrawLine(dofpen, StDist + dist * (ScaTotLength / TotLength), 0 + 19, StDist + dist * (ScaTotLength / TotLength) - 8f, 0 + 25);
                e.Graphics.DrawLine(dofpen, StDist + dist * (ScaTotLength / TotLength) + 8f, 0 + 19, StDist + dist * (ScaTotLength / TotLength), 0 + 25);
                e.Graphics.DrawLine(dofpen, StDist + dist * (ScaTotLength / TotLength), 0 + 19, StDist + dist * (ScaTotLength / TotLength) + 8f, 0 + 25);

                e.Graphics.DrawLine(dofpen, StDist + dist * (ScaTotLength / TotLength) - 8f, 0 + 25, StDist + dist * (ScaTotLength / TotLength) + 8f, 0 + 25);
            }
        }

        private void rpaintload(PaintEventArgs e)
        {
            // Dim linGrBrush As System.Drawing.Drawing2D.LinearGradientBrush
            float StDist;
            float ScaTotLength;
            float TotLength = 0f;
            float maxload = 0f;
            // ----Finding Maximum Load
            foreach (var itm in Indexes.mem)
            {
                TotLength = TotLength + itm.spanlength;
                // ----Point Load
                foreach (var Pitm in itm.Pload)
                {
                    if (Pitm.pload > maxload)
                    {
                        maxload = Pitm.pload;
                    }
                }
                // ----UVL
                foreach (var Uitm in itm.Uload)
                {
                    if (Uitm.uload1 > maxload)
                    {
                        maxload = Uitm.uload1;
                    }
                    if (Uitm.uload2 > maxload)
                    {
                        maxload = Uitm.uload2;
                    }
                }
            }
            StDist = (float)(-(coverpic.Width / 2d) + 100d);
            ScaTotLength = coverpic.Width - 200;

            float intSTdist = StDist;
            float dist = 0f;

            Pen loadpen;
            System.Drawing.Drawing2D.AdjustableArrowCap adcap;

            foreach (var itm in Indexes.mem)
            {
                dist = dist + itm.spanlength;
                // ----UVL
                loadpen = new Pen(Color.LightGray, 1f);
                adcap = new System.Drawing.Drawing2D.AdjustableArrowCap(2f, 4f);
                loadpen.CustomStartCap = adcap;
                int j = itm.Uload.Count - 1;
                for (int i = 0, loopTo = j; i <= loopTo; i++)
                {
                    float toSX1;
                    float toSX2;
                    float toSY1;
                    float toSY2;
                    toSX1 = intSTdist + itm.Uload[i].udist1 * (ScaTotLength / TotLength);
                    toSX2 = intSTdist + itm.Uload[i].udist2 * (ScaTotLength / TotLength);
                    toSY1 = itm.Uload[i].uload1 * (100f / maxload);
                    toSY2 = itm.Uload[i].uload2 * (100f / maxload);

                    e.Graphics.DrawLine(loadpen, toSX1, 0, toSX1, 0 - toSY1);
                    e.Graphics.DrawString(itm.Uload[i].uload1.ToString(), Font, loadpen.Brush, toSX1, 0 - 20 - toSY1);
                    if (toSY2 == toSY1)
                    {
                        for (float k = toSX1, loopTo1 = toSX2; k <= loopTo1; k += 10f)
                            e.Graphics.DrawLine(loadpen, k, 0, k, 0 - toSY1);
                    }
                    else if (toSY2 > toSY1)
                    {
                        for (float k = toSX1, loopTo3 = toSX2; k <= loopTo3; k += 10f)
                            e.Graphics.DrawLine(loadpen, k, 0, k, 0 - (toSY1 + (toSY2 - toSY1) / (toSX2 - toSX1) * (k - toSX1)));
                    }
                    else
                    {
                        for (float k = toSX1, loopTo2 = toSX2; k <= loopTo2; k += 10f)
                            e.Graphics.DrawLine(loadpen, k, 0, k, 0 - (toSY2 + (toSY1 - toSY2) / (toSX2 - toSX1) * (toSX2 - k)));
                    }
                    e.Graphics.DrawLine(loadpen, toSX2, 0, toSX2, 0 - toSY2);
                    e.Graphics.DrawString(itm.Uload[i].uload2.ToString(), Font, loadpen.Brush, toSX2, 0 - 20 - toSY2);
                }
                // ----Point Load
                j = itm.Pload.Count - 1;
                loadpen = new Pen(Color.LightGray, 2f);
                adcap = new System.Drawing.Drawing2D.AdjustableArrowCap(3f, 5f);
                loadpen.CustomStartCap = adcap;
                for (int i = 0, loopTo4 = j; i <= loopTo4; i++)
                {
                    float toSX;
                    float toSY;
                    toSX = intSTdist + itm.Pload[i].pdist * (ScaTotLength / TotLength);
                    toSY = itm.Pload[i].pload * (100f / maxload);

                    e.Graphics.DrawLine(loadpen, toSX, 0f, toSX, 0 - toSY);
                    e.Graphics.DrawString(itm.Pload[i].pload.ToString(), Font, loadpen.Brush, toSX, 0 - 20 - toSY);

                }
                // ----Moment
                loadpen = new Pen(Color.LightGray, 2f);
                adcap = new System.Drawing.Drawing2D.AdjustableArrowCap(3f, 5f);
                j = itm.Mload.Count - 1;
                for (int i = 0, loopTo5 = j; i <= loopTo5; i++)
                {
                    float toSX;
                    toSX = intSTdist + itm.Mload[i].mdist * (ScaTotLength / TotLength);
                    if (itm.Mload[i].mload > 0f)
                    {
                        loadpen.CustomStartCap = adcap;
                    }
                    else
                    {
                        loadpen.CustomEndCap = adcap;
                    }
                    e.Graphics.DrawArc(loadpen, toSX - 30f, 0 - 30, 60f, 60f, 270f, 180f);
                    e.Graphics.DrawString(Math.Abs(itm.Mload[i].mload).ToString(), Font, loadpen.Brush, toSX, 0 - 50);
                }
                intSTdist = StDist + dist * (ScaTotLength / TotLength);
            }
            // --Check For Load Selection 
            // For Each itm In mem
            // For Each Pitm In itm.Pload
            // e.Graphics.DrawRectangle(Pens.DarkRed, Pitm.rect)
            // Next
            // For Each uitm In itm.Uload
            // e.Graphics.DrawRectangle(Pens.DarkRed, uitm.rect)
            // Next
            // For Each mitm In itm.Mload
            // e.Graphics.DrawRectangle(Pens.DarkRed, mitm.rect)
            // Next
            // Next
        }

        private void rShearPaint(PaintEventArgs e)
        {
            var PP = new Pen(Color.CornflowerBlue, 3f);
            var fnt = new Font("Verdana", 14f);
            e.Graphics.DrawString("Shear Force Diagram", fnt, Brushes.CornflowerBlue, (float)(-(coverpic.Width / 2d) + 100d), 0 - 260);
            e.Graphics.DrawLine(PP, (int)Math.Round(Indexes.SFpts[0].X), (int)Math.Round(MEheight / 2f), Indexes.SFpts[0].X, Indexes.SFpts[0].Y);
            e.Graphics.DrawLines(PP, Indexes.SFpts);
            fnt = new Font("Verdana", 10f, FontStyle.Bold);
            int i = 0;
            foreach (var pt in Indexes.SFmaxs)
            {
                e.Graphics.DrawString(Math.Round(Indexes.SF[Indexes.SFMc[i]], 3).ToString(), fnt, Brushes.CornflowerBlue, pt.X, pt.Y);
                i = i + 1;
            }
        }

        private void rMomentPaint(PaintEventArgs e)
        {
            var PP = new Pen(Color.Crimson, 3f);
            Font fnt;
            fnt = new Font("Verdana", 14f);
            e.Graphics.DrawString("Bending Moment Diagram", fnt, Brushes.Crimson, (float)(-(coverpic.Width / 2d) + 100d), 0 - 260);
            e.Graphics.DrawLine(PP, (int)Math.Round(Indexes.BMpts[0].X), (int)Math.Round(MEheight / 2f), Indexes.BMpts[0].X, Indexes.BMpts[0].Y);
            e.Graphics.DrawLines(PP, Indexes.BMpts);
            fnt = new Font("Verdana", 10f, FontStyle.Bold);
            int i = 0;
            foreach (var pt in Indexes.BMmaxs)
            {
                e.Graphics.DrawString(Math.Round(Indexes.BM[Indexes.BMMc[i]], 3).ToString(), fnt, Brushes.Crimson, pt.X, pt.Y);
                i = i + 1;
            }
        }

        private void rDeflectionPaint(PaintEventArgs e)
        {
            var PP = new Pen(Color.DarkGreen, 3f);
            Font fnt;
            fnt = new Font("Verdana", 14f);
            e.Graphics.DrawString("Displacement Diagram", fnt, Brushes.DarkGreen, (float)(-(coverpic.Width / 2d) + 100d), 0 - 260);
            e.Graphics.DrawLines(PP, Indexes.DEpts);
            fnt = new Font("Verdana", 10f, FontStyle.Bold);
            int i = 0;
            foreach (var pt in Indexes.DEmaxs)
            {
                e.Graphics.DrawString(Math.Round(Indexes.DE[Indexes.DEMc[i]], 8).ToString(), fnt, Brushes.DarkGreen, pt.X, pt.Y);
                i = i + 1;
            }
        }

        private void rSlopePaint(PaintEventArgs e)
        {
            var PP = new Pen(Color.DeepPink, 3f);
            Font fnt;
            fnt = new Font("Verdana", 14f);
            e.Graphics.DrawString("Slope Diagram", fnt, Brushes.DeepPink, (float)(-(coverpic.Width / 2d) + 100d), 0 - 260);
            e.Graphics.DrawLines(PP, Indexes.SLpts);
            fnt = new Font("Verdana", 10f, FontStyle.Bold);
            int i = 0;
            foreach (var pt in Indexes.SLmaxs)
            {
                e.Graphics.DrawString(Math.Round(Indexes.SL[Indexes.SLMc[i]], 8).ToString(), fnt, Brushes.DeepPink, pt.X, pt.Y);
                i = i + 1;
            }
        }
        #endregion

        #region Respic Zoom events
        private void respic_MouseEnter(object sender, EventArgs e)
        {
            respic.Focus();
        }

        private void respic_MouseLeave(object sender, EventArgs e)
        {
            My.MyProject.Forms.MDIMain.ToolStrip1.Focus();
        }


        private void respic_MouseWheel(object sender, MouseEventArgs e)
        {
            double xw, yw;
            respic.Focus();
            xw = e.X / Indexes.Zm - Indexes.MidPt.X;
            yw = e.Y / Indexes.Zm - Indexes.MidPt.Y;
            if (e.Delta > 0)
            {
                if (Indexes.Zm < 100d)
                {
                    Indexes.Zm = Indexes.Zm + 0.1d;
                }
            }
            else if (e.Delta < 0)
            {
                if (Indexes.Zm > 0.101d)
                {
                    Indexes.Zm = Indexes.Zm - 0.1d;
                    // ElseIf Zm <= 0.1 Then
                    // If Zm > 0.0101 Then
                    // Zm = Zm - 0.01
                    // ElseIf Zm <= 0.01 Then
                    // If Zm > 0.00101 Then
                    // Zm = Zm - 0.001
                    // End If
                    // End If
                }
            }
            Indexes.MidPt.X = (int)Math.Round(e.X / Indexes.Zm - xw);
            Indexes.MidPt.Y = (int)Math.Round(e.Y / Indexes.Zm - yw);

            // mainpic.Top = -(((ht * Zm) - ht) / 2)
            respic.Refresh();
            My.MyProject.Forms.MDIMain.ToolStripLabel1.Text = Indexes.Zm * 100d + "%";
            My.MyProject.Forms.MDIMain.ToolStripLabel2.Text = Indexes.Zm * 100d + "%";
            // End If
        }
        #endregion

        #region Respic Mouse Move Events

        private void respic_MouseMove(object sender, MouseEventArgs e)
        {
            // ------ Mouse move drag
            if (isMiddleDrag == true)
            {
                // ---- Pan Operation 
                Indexes.MidPt = new Point((int)Math.Round(e.X / Indexes.Zm - PanStartDrag.X), (int)Math.Round(e.Y / Indexes.Zm - PanStartDrag.Y));
                respic.Refresh();
                return;
            }

            float StDist;
            float ScaTotLength;

            StDist = (float)(-(coverpic.Width / 2d) + 100d);
            ScaTotLength = coverpic.Width - 200;

            My.MyProject.Forms.MDIMain.SFlabel.Visible = false;
            My.MyProject.Forms.MDIMain.BMlabel.Visible = false;
            My.MyProject.Forms.MDIMain.DELabel.Visible = false;
            My.MyProject.Forms.MDIMain.SLLabel.Visible = false;
            My.MyProject.Forms.MDIMain.Xlabel.Visible = false;

            double TempX, TempY;
            TempX = e.X / Indexes.Zm - Indexes.MidPt.X;
            TempY = e.Y / Indexes.Zm - Indexes.MidPt.Y;

            if (My.MyProject.Forms.MDIMain.ShearForceDiagramToolStripMenuItem.Checked == true)
            {
                if (TempX > (double)StDist & TempX < (double)(StDist + ScaTotLength) & TempY > 0 - 40 & TempY < 0 + 40)
                {
                    int i = (int)Math.Round(TempX - (double)StDist);
                    My.MyProject.Forms.MDIMain.SFlabel.Text = Math.Round(Indexes.SF[i], 4).ToString();
                    My.MyProject.Forms.MDIMain.SFlabel.Visible = true;
                    My.MyProject.Forms.MDIMain.Xlabel.Text = Math.Round((double)(totL / (coverpic.Width - 200) * i), 3) + "     ---> ";
                    My.MyProject.Forms.MDIMain.Xlabel.Visible = true;
                }
            }
            else if (My.MyProject.Forms.MDIMain.BendingMomentDiagramToolStripMenuItem.Checked == true)
            {
                if (TempX > (double)StDist & TempX < (double)(StDist + ScaTotLength) & TempY > 0 - 40 & TempY < 0 + 40)
                {
                    int i = (int)Math.Round(TempX - (double)StDist);
                    My.MyProject.Forms.MDIMain.BMlabel.Text = Math.Round(Indexes.BM[i], 4).ToString();
                    My.MyProject.Forms.MDIMain.BMlabel.Visible = true;
                    My.MyProject.Forms.MDIMain.Xlabel.Text = Math.Round((double)(totL / (coverpic.Width - 200) * i), 3) + "     ---> ";
                    My.MyProject.Forms.MDIMain.Xlabel.Visible = true;
                }
            }
            else if (My.MyProject.Forms.MDIMain.DeflectionToolStripMenuItem.Checked == true)
            {
                if (TempX > (double)StDist & TempX < (double)(StDist + ScaTotLength) & TempY > 0 - 40 & TempY < 0 + 40)
                {
                    int i = (int)Math.Round(TempX - (double)StDist);
                    My.MyProject.Forms.MDIMain.DELabel.Text = Math.Round(Indexes.DE[i], 8).ToString();
                    My.MyProject.Forms.MDIMain.DELabel.Visible = true;
                    My.MyProject.Forms.MDIMain.Xlabel.Text = Math.Round((double)(totL / (coverpic.Width - 200) * i), 3) + "     ---> ";
                    My.MyProject.Forms.MDIMain.Xlabel.Visible = true;
                }
            }
            else if (My.MyProject.Forms.MDIMain.SlopeToolStripMenuItem.Checked == true)
            {
                if (TempX > (double)StDist & TempX < (double)(StDist + ScaTotLength) & TempY > 0 - 40 & TempY < 0 + 40)
                {
                    int i = (int)Math.Round(TempX - (double)StDist);
                    My.MyProject.Forms.MDIMain.SLLabel.Text = Math.Round(Indexes.SL[i], 8).ToString();
                    My.MyProject.Forms.MDIMain.SLLabel.Visible = true;
                    My.MyProject.Forms.MDIMain.Xlabel.Text = Math.Round((double)(totL / (coverpic.Width - 200) * i), 3) + "     ---> ";
                    My.MyProject.Forms.MDIMain.Xlabel.Visible = true;
                }
            }
        }
        #endregion

        #region Respic Mouse Click Events
        private void respic_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip3.Show(respic.PointToScreen(e.Location));
            }
        }


        private void respic_MouseDown(object sender, MouseEventArgs e)
        {
            // ------ Mouse down for Click and drag
            if (e.Button == MouseButtons.Middle)
            {
                // ---- Pan Operation
                isMiddleDrag = true;
                PanStartDrag = new Point((int)Math.Round(e.X / Indexes.Zm - Indexes.MidPt.X), (int)Math.Round(e.Y / Indexes.Zm - Indexes.MidPt.Y));
                respic.Refresh();
            }
        }

        private void respic_MouseUp(object sender, MouseEventArgs e)
        {
            // ------ Mouse up for Click and drag
            if (e.Button == MouseButtons.Middle)
            {
                // --- Pan Operation Stops
                isMiddleDrag = false;
                respic.Refresh();
            }
        }

        private void respic_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Zoom to Fit
            Indexes.MidPt = new Point((int)Math.Round(respic.Width / 2d), (int)Math.Round(respic.Height / 2d));
            Indexes.Zm = 1d;
            My.MyProject.Forms.MDIMain.ToolStripLabel1.Text = Indexes.Zm * 100d + "%";
            My.MyProject.Forms.MDIMain.ToolStripLabel2.Text = Indexes.Zm * 100d + "%";

            respic.Refresh();
        }
        #endregion

        #region Context Menu 3 Events
        private void BendingMomentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // MDIMain.BendingMomentDiagramToolStripMenuItem_Click(sender, e)
            if (My.MyProject.Forms.MDIMain.ShearForceDiagramToolStripMenuItem.Checked == true | My.MyProject.Forms.MDIMain.DeflectionToolStripMenuItem.Checked == true | My.MyProject.Forms.MDIMain.SlopeToolStripMenuItem.Checked == true)
            {
                My.MyProject.Forms.MDIMain.ShearForceDiagramToolStripMenuItem.Checked = false;
                My.MyProject.Forms.MDIMain.DeflectionToolStripMenuItem.Checked = false;
                My.MyProject.Forms.MDIMain.SlopeToolStripMenuItem.Checked = false;
                My.MyProject.Forms.MDIMain.BendingMomentDiagramToolStripMenuItem.Checked = true;
                respic.Refresh();
            }
        }

        private void ShearForceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // MDIMain.ShearForceDiagramToolStripMenuItem_Click(sender, e)
            if (My.MyProject.Forms.MDIMain.BendingMomentDiagramToolStripMenuItem.Checked == true | My.MyProject.Forms.MDIMain.DeflectionToolStripMenuItem.Checked == true | My.MyProject.Forms.MDIMain.SlopeToolStripMenuItem.Checked == true)
            {
                My.MyProject.Forms.MDIMain.BendingMomentDiagramToolStripMenuItem.Checked = false;
                My.MyProject.Forms.MDIMain.DeflectionToolStripMenuItem.Checked = false;
                My.MyProject.Forms.MDIMain.SlopeToolStripMenuItem.Checked = false;
                My.MyProject.Forms.MDIMain.ShearForceDiagramToolStripMenuItem.Checked = true;
                respic.Refresh();
            }
        }

        private void DeflectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // MDIMain.DeflectionToolStripMenuItem_Click(sender, e)
            if (My.MyProject.Forms.MDIMain.BendingMomentDiagramToolStripMenuItem.Checked == true | My.MyProject.Forms.MDIMain.ShearForceDiagramToolStripMenuItem.Checked == true | My.MyProject.Forms.MDIMain.SlopeToolStripMenuItem.Checked == false)
            {
                My.MyProject.Forms.MDIMain.BendingMomentDiagramToolStripMenuItem.Checked = false;
                My.MyProject.Forms.MDIMain.ShearForceDiagramToolStripMenuItem.Checked = false;
                My.MyProject.Forms.MDIMain.SlopeToolStripMenuItem.Checked = false;
                My.MyProject.Forms.MDIMain.DeflectionToolStripMenuItem.Checked = true;
                respic.Refresh();
            }
        }

        private void SlopeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // MDIMain.SlopeToolStripMenuItem_Click(sender, e)
            if (My.MyProject.Forms.MDIMain.BendingMomentDiagramToolStripMenuItem.Checked == true | My.MyProject.Forms.MDIMain.ShearForceDiagramToolStripMenuItem.Checked == true | My.MyProject.Forms.MDIMain.DeflectionToolStripMenuItem.Checked == true)
            {
                My.MyProject.Forms.MDIMain.BendingMomentDiagramToolStripMenuItem.Checked = false;
                My.MyProject.Forms.MDIMain.ShearForceDiagramToolStripMenuItem.Checked = false;
                My.MyProject.Forms.MDIMain.DeflectionToolStripMenuItem.Checked = false;
                My.MyProject.Forms.MDIMain.SlopeToolStripMenuItem.Checked = true;
                respic.Refresh();
            }
        }




        #endregion
        #endregion



    }
}