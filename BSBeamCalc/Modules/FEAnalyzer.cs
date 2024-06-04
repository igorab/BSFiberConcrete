using System;
using System.Collections.Generic;
using System.Drawing;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace CBAnsDes
{
    static class FEAnalyzer
    {
        private static List<Member> Tmem;

        public static void ContinuousBeam_analyzer(bool c = false)
        {
            int _NoofDivision = 2;
            Tmem = new List<Member>();
            FixTempMem(ref Tmem, _NoofDivision);

            // ------Main Variables
            string str = "";
            string b, i;
            var GSM = new double[Tmem.Count * 2 + 1 + 1, Tmem.Count * 2 + 1 + 1];
            var DOFM = new int[Tmem.Count * 2 + 1 + 1];
            var FERM = new double[Tmem.Count * 2 + 1 + 1];
            var Cbound = default(int);
            var totlength = default(double);
            int ScaTotLength = My.MyProject.Forms.beamcreate.coverpic.Width - 200;
            foreach (var itm in Tmem)
                totlength = totlength + (double)itm.spanlength;

            foreach (var itm in Tmem)
            {
                FixFER(itm);
                GStiffness(itm, ref GSM);
            }
            FixDM(ref DOFM);
            FixFM(ref FERM);


            // -------------For Check-----------------
            str = str + Constants.vbNewLine;
            str = str + "----> Degree Of Freedom <----" + Constants.vbNewLine + Constants.vbNewLine;

            foreach (var itm in Tmem)
            {
                i = (Tmem.IndexOf(itm) + 1).ToString();
                b = (Conversions.ToInteger(itm.DOF[0]) * -1).ToString();
                str = str + "Member " + i + ":   " + b + Constants.vbTab;
                b = (Conversions.ToInteger(itm.DOF[1]) * -1).ToString();
                str = str + b + Constants.vbTab;
                b = (Conversions.ToInteger(itm.DOF[2]) * -1).ToString();
                str = str + b + Constants.vbTab;
                b = (Conversions.ToInteger(itm.DOF[3]) * -1).ToString();
                str = str + b + Constants.vbTab + Constants.vbNewLine;
            }
            str = str + Constants.vbNewLine + Constants.vbNewLine;
            str = str + "----> Fixed End Moment <----" + Constants.vbNewLine + Constants.vbNewLine;

            foreach (var itm in Tmem)
            {
                i = (Tmem.IndexOf(itm) + 1).ToString();
                b = Math.Round(itm.FER[0], 2).ToString();
                str = str + "Member " + i + ":   " + b + Constants.vbTab;
                b = Math.Round(itm.FER[1], 2).ToString();
                str = str + b + Constants.vbTab;
                b = Math.Round(itm.FER[2], 2).ToString();
                str = str + b + Constants.vbTab;
                b = Math.Round(itm.FER[3], 2).ToString();
                str = str + b + Constants.vbTab + Constants.vbNewLine;
            }
            str = str + Constants.vbNewLine + Constants.vbNewLine;
            str = str + "----> Global Stiffnessmatrix <----" + Constants.vbNewLine + Constants.vbNewLine;

            for (int p = 0, loopTo = Tmem.Count * 2 + 1; p <= loopTo; p++)
            {
                str = str + "|";
                for (int t = 0, loopTo1 = Tmem.Count * 2 + 1; t <= loopTo1; t++)
                {
                    b = Math.Round(GSM[p, t], 2).ToString();
                    str = str + Constants.vbTab + b;
                }
                str = str + Constants.vbTab + "|" + Constants.vbNewLine;
            }
            str = str + Constants.vbNewLine + Constants.vbNewLine;
            str = str + "----> Load Matrix <----" + Constants.vbTab + Constants.vbTab + "----> DOF Matrix <----" + Constants.vbNewLine + Constants.vbNewLine;
            for (int p = 0, loopTo2 = Tmem.Count * 2 + 1; p <= loopTo2; p++)
            {
                str = str + "|";
                b = Math.Round(FERM[p], 2).ToString();
                str = str + Constants.vbTab + b + Constants.vbTab + "|";
                str = str + Constants.vbTab + Constants.vbTab + "|";
                b = (DOFM[p] * -1).ToString();
                str = str + Constants.vbTab + b;
                str = str + Constants.vbTab + "|" + Constants.vbNewLine;
            }

            // -------Curtailment Zone --------------
            // --------------------------------------
            Curtailment(ref GSM, ref DOFM, ref FERM, ref Cbound);
            // --------------------------------------
            // --------------------------------------

            str = str + Constants.vbNewLine + Constants.vbNewLine;
            str = str + "----> Global Stiffnessmatrix After Curtailment<----" + Constants.vbNewLine + Constants.vbNewLine;
            for (int p = 0, loopTo3 = Cbound - 1; p <= loopTo3; p++)
            {
                str = str + "|";
                for (int t = 0, loopTo4 = Cbound - 1; t <= loopTo4; t++)
                {
                    b = Math.Round(GSM[p, t], 2).ToString();
                    str = str + Constants.vbTab + b;
                }
                str = str + Constants.vbTab + "|" + Constants.vbNewLine;
            }
            str = str + Constants.vbNewLine + Constants.vbNewLine;
            str = str + "----> Load Matrix After Curtailment <----" + Constants.vbNewLine + Constants.vbNewLine;
            for (int p = 0, loopTo5 = Cbound - 1; p <= loopTo5; p++)
            {
                str = str + "|";
                b = Math.Round(FERM[p], 2).ToString();
                str = str + Constants.vbTab + b;
                str = str + Constants.vbTab + "|" + Constants.vbNewLine;
            }

            // ----------- Gauss Elimination Zone -------------
            // ------------------------------------------------
            var RESM = new double[Cbound];
            Gauss(GSM, FERM, ref RESM, Cbound - 1);
            // ------------------------------------------------
            // ------------------------------------------------

            str = str + Constants.vbNewLine + Constants.vbNewLine;
            str = str + "----> Result Matrix <----" + Constants.vbNewLine + Constants.vbNewLine;
            for (int p = 0, loopTo6 = Cbound - 1; p <= loopTo6; p++)
            {
                str = str + "|";
                b = Math.Round(RESM[p], 4).ToString();
                str = str + Constants.vbTab + b;
                str = str + Constants.vbTab + "|" + Constants.vbNewLine;
            }

            // ----------------- Welding Zone ----------------
            // -----------------------------------------------
            GSM = new double[Tmem.Count * 2 + 1 + 1, Tmem.Count * 2 + 1 + 1];
            DOFM = new int[Tmem.Count * 2 + 1 + 1];
            FERM = new double[Tmem.Count * 2 + 1 + 1];
            foreach (var itm in Tmem)
            {
                FixFER(itm);
                GStiffness(itm, ref GSM);
            }
            FixDM(ref DOFM);
            FixFM(ref FERM);
            // -----------------Deflection & Rotation at Nodes - Result matrix RESM 
            var theta_delta_matrix = new double[Tmem.Count * 2 + 1 + 1];

            Welding(ref RESM, ref FERM, ref DOFM, ref theta_delta_matrix);
            GMultiplier(ref GSM, ref RESM);
            loadMINU(ref RESM, ref FERM, ref theta_delta_matrix);
            // -----------------------------------------------
            // -----------------------------------------------

            str = str + Constants.vbNewLine + Constants.vbNewLine;
            str = str + "----> Result Matrix after welding <----" + Constants.vbNewLine + Constants.vbNewLine;
            for (int p = 0, loopTo7 = Tmem.Count * 2 + 1; p <= loopTo7; p++)
            {
                str = str + "|";
                b = Math.Round(RESM[p], 2).ToString();
                str = str + Constants.vbTab + b;
                str = str + Constants.vbTab + "|" + Constants.vbNewLine;
            }
            str = str + Constants.vbNewLine;
            str = str + "----> Reaction in Members <----" + Constants.vbNewLine + Constants.vbNewLine;
            str = str + "                     RA" + Constants.vbTab + "MA" + Constants.vbTab + "RB" + Constants.vbTab + "MB" + Constants.vbNewLine;
            foreach (var itm in Tmem)
            {
                i = (Tmem.IndexOf(itm) + 1).ToString();
                b = Math.Round(itm.RES[0], 2).ToString();
                str = str + "Member " + i + ":   " + b + Constants.vbTab;
                b = Math.Round(itm.RES[1], 2).ToString();
                str = str + b + Constants.vbTab;
                b = Math.Round(itm.RES[2], 2).ToString();
                str = str + b + Constants.vbTab;
                b = Math.Round(itm.RES[3], 2).ToString();
                str = str + b + Constants.vbTab + Constants.vbNewLine;
            }

            int _tempI = 0;
            foreach (var E in Indexes.mem)
            {
                Indexes.mem[Indexes.mem.IndexOf(E)].RES[0] = Tmem[_tempI].RES[0];
                Indexes.mem[Indexes.mem.IndexOf(E)].RES[1] = Tmem[_tempI].RES[1];
                Indexes.mem[Indexes.mem.IndexOf(E)].RES[2] = Tmem[_tempI + _NoofDivision - 1].RES[2];
                Indexes.mem[Indexes.mem.IndexOf(E)].RES[3] = Tmem[_tempI + _NoofDivision - 1].RES[3];

                Indexes.mem[Indexes.mem.IndexOf(E)].DISP[0] = Tmem[_tempI].DISP[0];
                Indexes.mem[Indexes.mem.IndexOf(E)].DISP[1] = Tmem[_tempI].DISP[1];
                Indexes.mem[Indexes.mem.IndexOf(E)].DISP[2] = Tmem[_tempI + _NoofDivision - 1].DISP[2];
                Indexes.mem[Indexes.mem.IndexOf(E)].DISP[3] = Tmem[_tempI + _NoofDivision - 1].DISP[3];
                _tempI = _tempI + _NoofDivision;
            }

            str = str + Constants.vbNewLine;
            str = str + "----> Reaction in Compiled Members <----" + Constants.vbNewLine + Constants.vbNewLine;
            str = str + "                     RA" + Constants.vbTab + "MA" + Constants.vbTab + "RB" + Constants.vbTab + "MB" + Constants.vbNewLine;
            foreach (var itm in Indexes.mem)
            {
                i = (Indexes.mem.IndexOf(itm) + 1).ToString();
                b = Math.Round(itm.RES[0], 2).ToString();
                str = str + "Member " + i + ":   " + b + Constants.vbTab;
                b = Math.Round(itm.RES[1], 2).ToString();
                str = str + b + Constants.vbTab;
                b = Math.Round(itm.RES[2], 2).ToString();
                str = str + b + Constants.vbTab;
                b = Math.Round(itm.RES[3], 2).ToString();
                str = str + b + Constants.vbTab + Constants.vbNewLine;
            }


            var EquilibriumMember = new Member();
            FixEquilibriumMember(ref EquilibriumMember);

            str = str + Constants.vbNewLine;
            str = str + "----> Equilibrium Member <----" + Constants.vbNewLine + Constants.vbNewLine;
            str = str + "Location" + Constants.vbTab + "Point Load" + Constants.vbNewLine;
            foreach (var P in EquilibriumMember.Pload)
                str = str + P.pdist + Constants.vbTab + P.pload + Constants.vbNewLine;
            str = str + Constants.vbNewLine;
            str = str + "Location" + Constants.vbTab + "Moment" + Constants.vbNewLine;
            foreach (var M in EquilibriumMember.Mload)
                str = str + M.mdist + Constants.vbTab + M.mload + Constants.vbNewLine;
            str = str + Constants.vbNewLine;
            str = str + "Location" + Constants.vbTab + "UVL" + Constants.vbNewLine;
            foreach (var U in EquilibriumMember.Uload)
            {
                str = str + U.udist1 + Constants.vbTab + U.uload1 + Constants.vbNewLine;
                str = str + U.udist2 + Constants.vbTab + U.uload2 + Constants.vbNewLine;
            }
            str = str + Constants.vbNewLine;
            str = str + "Location" + Constants.vbTab + "UVL" + Constants.vbNewLine;


            // -------------- Member Details --------------
            // --------------------------------------------
            if (c == true)
            {
                var a = new memDetails();
                a.text1 = str;
                a.ShowDialog();
                return;
            }

            CoordinateCalculator();
        }

        #region FEA Analysis
        private static void FixTempMem(ref List<Member> Tmem, int _NoofDivision)
        {
            // ----Temporary member disintegration for better results and non singular solutions for determinate beams
            foreach (var itm in Indexes.mem)
                FixDOF(itm);
            foreach (var itm in Indexes.mem)
            {
                // If itm.Pload.Count = 0 And itm.Uload.Count = 0 And itm.Mload.Count = 0 Then
                // Tmem.Add(itm)
                // Continue For
                // End If
                double _Division = (double)(itm.spanlength / _NoofDivision);
                for (int K = 1, loopTo = _NoofDivision; K <= loopTo; K++)
                {
                    var _disintegratedmember = new Member();
                    _disintegratedmember.spanlength = (float)_Division;
                    _disintegratedmember.Emodulus = itm.Emodulus;
                    _disintegratedmember.Inertia = itm.Inertia;
                    // -----Adding Point Load comes under the disintegrated member
                    foreach (var Pl in itm.Pload)
                    {
                        if ((double)Pl.pdist <= K * _Division & (double)Pl.pdist > (K - 1) * _Division)
                        {
                            var T_Pl = new Member.P();
                            T_Pl.pload = Pl.pload;
                            T_Pl.pdist = (float)((double)Pl.pdist - (K - 1) * _Division);
                            _disintegratedmember.Pload.Add(T_Pl);
                        }
                    }
                    // -----Adding UVL comes under the disintegrated member
                    foreach (var Ul in itm.Uload)
                    {
                        if ((double)Ul.udist2 <= K * _Division & (double)Ul.udist1 > (K - 1) * _Division)
                        {
                            // ----Case 1: If the whole load lies inside the disintegration
                            var T_Ul = new Member.U();
                            T_Ul.uload1 = Ul.uload1;
                            T_Ul.uload2 = Ul.uload2;
                            T_Ul.udist1 = (float)((double)Ul.udist1 - (K - 1) * _Division);
                            T_Ul.udist2 = (float)((double)Ul.udist2 - (K - 1) * _Division);
                            _disintegratedmember.Uload.Add(T_Ul);
                        }
                        else if ((double)Ul.udist1 < K * _Division & (double)Ul.udist1 > (K - 1) * _Division)
                        {
                            // ----Case 2: location 1 lies inside the disintegration
                            var T_Ul = new Member.U();
                            T_Ul.uload1 = Ul.uload1;
                            if (Ul.uload1 <= Ul.uload2)
                            {
                                T_Ul.uload2 = (float)((double)Ul.uload1 + (double)((Ul.uload2 - Ul.uload1) / (Ul.udist2 - Ul.udist1)) * (K * _Division - (double)Ul.udist1));
                            }
                            else
                            {
                                T_Ul.uload2 = (float)((double)Ul.uload2 + (double)((Ul.uload1 - Ul.uload2) / (Ul.udist2 - Ul.udist1)) * ((double)Ul.udist2 - K * _Division));
                            }
                            T_Ul.udist1 = (float)((double)Ul.udist1 - (K - 1) * _Division);
                            T_Ul.udist2 = (float)_Division;
                            _disintegratedmember.Uload.Add(T_Ul);
                        }
                        else if ((double)Ul.udist2 < K * _Division & (double)Ul.udist2 > (K - 1) * _Division)
                        {
                            // ----Case 3: location 2 lies inside the disintegration
                            var T_Ul = new Member.U();
                            if (Ul.uload1 <= Ul.uload2)
                            {
                                T_Ul.uload1 = (float)((double)Ul.uload1 + (double)((Ul.uload2 - Ul.uload1) / (Ul.udist2 - Ul.udist1)) * ((K - 1) * _Division - (double)Ul.udist1));
                            }
                            else
                            {
                                T_Ul.uload1 = (float)((double)Ul.uload2 + (double)((Ul.uload1 - Ul.uload2) / (Ul.udist2 - Ul.udist1)) * ((double)Ul.udist2 - (K - 1) * _Division));
                            }
                            T_Ul.uload2 = Ul.uload2;
                            T_Ul.udist1 = 0f;
                            T_Ul.udist2 = (float)((double)Ul.udist2 - (K - 1) * _Division);
                            _disintegratedmember.Uload.Add(T_Ul);
                        }
                        else if ((double)Ul.udist2 >= K * _Division & (double)Ul.udist1 <= (K - 1) * _Division)
                        {
                            // ----Case 4: disintegration lies inside the load
                            var T_Ul = new Member.U();
                            if (Ul.uload1 <= Ul.uload2)
                            {
                                T_Ul.uload1 = (float)((double)Ul.uload1 + (double)((Ul.uload2 - Ul.uload1) / (Ul.udist2 - Ul.udist1)) * ((K - 1) * _Division - (double)Ul.udist1));
                                T_Ul.uload2 = (float)((double)Ul.uload1 + (double)((Ul.uload2 - Ul.uload1) / (Ul.udist2 - Ul.udist1)) * (K * _Division - (double)Ul.udist1));
                            }
                            else
                            {
                                T_Ul.uload1 = (float)((double)Ul.uload2 + (double)((Ul.uload1 - Ul.uload2) / (Ul.udist2 - Ul.udist1)) * ((double)Ul.udist2 - (K - 1) * _Division));
                                T_Ul.uload2 = (float)((double)Ul.uload2 + (double)((Ul.uload1 - Ul.uload2) / (Ul.udist2 - Ul.udist1)) * ((double)Ul.udist2 - K * _Division));
                            }
                            T_Ul.udist1 = 0f;
                            T_Ul.udist2 = (float)_Division;
                            _disintegratedmember.Uload.Add(T_Ul);
                        }
                    }
                    // -----Adding moment comes under the disintegrated member
                    foreach (var Ml in itm.Mload)
                    {
                        if ((double)Ml.mdist <= K * _Division & (double)Ml.mdist > (K - 1) * _Division)
                        {
                            var T_Ml = new Member.M();
                            T_Ml.mload = Ml.mload;
                            T_Ml.mdist = (float)((double)Ml.mdist - (K - 1) * _Division);
                            _disintegratedmember.Mload.Add(T_Ml);
                        }
                    }
                    // ---Fixing Degree Of Freedom
                    if (K == 1)
                    {
                        // ----Left end
                        _disintegratedmember.DOF[0] = itm.DOF[0];
                        _disintegratedmember.DOF[1] = itm.DOF[1];
                        _disintegratedmember.DOF[2] = Conversions.ToBoolean(1);
                        _disintegratedmember.DOF[3] = Conversions.ToBoolean(1);
                    }
                    else if (K == _NoofDivision)
                    {
                        // ----Right end
                        _disintegratedmember.DOF[0] = Conversions.ToBoolean(1);
                        _disintegratedmember.DOF[1] = Conversions.ToBoolean(1);
                        _disintegratedmember.DOF[2] = itm.DOF[2];
                        _disintegratedmember.DOF[3] = itm.DOF[3];
                    }
                    else
                    {
                        // -----Inbetween disintegrated members are free to translate and rotate
                        _disintegratedmember.DOF[0] = Conversions.ToBoolean(1);
                        _disintegratedmember.DOF[1] = Conversions.ToBoolean(1);
                        _disintegratedmember.DOF[2] = Conversions.ToBoolean(1);
                        _disintegratedmember.DOF[3] = Conversions.ToBoolean(1);
                    }

                    Tmem.Add(_disintegratedmember);
                }
            }
        }

        private static void FixDOF(Member M)
        {
            if (Indexes.mem.IndexOf(M) == 0 & Indexes.mem.IndexOf(M) == Indexes.mem.Count - 1)
            {
                switch (Indexes.ends)
                {
                    case 1:         // Fixed-Fixed
                        {
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[0] = Conversions.ToBoolean(0);
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[1] = Conversions.ToBoolean(0);
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[2] = Conversions.ToBoolean(0);
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[3] = Conversions.ToBoolean(0);
                            return;
                        }
                    case 2:         // Fixed-Free
                        {
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[0] = Conversions.ToBoolean(0);
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[1] = Conversions.ToBoolean(0);
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[2] = Conversions.ToBoolean(1);
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[3] = Conversions.ToBoolean(1);
                            return;
                        }
                    case 3:         // Pinned-Pinned
                        {
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[0] = Conversions.ToBoolean(0);
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[1] = Conversions.ToBoolean(1);
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[2] = Conversions.ToBoolean(0);
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[3] = Conversions.ToBoolean(1);
                            return;
                        }
                    case 4:         // Free-Free
                        {
                            return;
                        }
                    case 5:         // Fixed-Pinned
                        {
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[0] = Conversions.ToBoolean(0);
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[1] = Conversions.ToBoolean(0);
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[2] = Conversions.ToBoolean(0);
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[3] = Conversions.ToBoolean(1);
                            return;
                        }
                    case 6:         // pinned-free
                        {
                            return;
                        }
                }
            }
            else if (Indexes.mem.IndexOf(M) == 0)
            {
                switch (Indexes.ends)
                {
                    case 1:         // Fixed-Fixed
                        {
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[0] = Conversions.ToBoolean(0);
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[1] = Conversions.ToBoolean(0);
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[2] = Conversions.ToBoolean(0);
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[3] = Conversions.ToBoolean(1);
                            return;
                        }
                    case 2:         // Fixed-Free
                        {
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[0] = Conversions.ToBoolean(0);
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[1] = Conversions.ToBoolean(0);
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[2] = Conversions.ToBoolean(0);
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[3] = Conversions.ToBoolean(1);
                            return;
                        }
                    case 3:         // Pinned-Pinned
                        {
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[0] = Conversions.ToBoolean(0);
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[1] = Conversions.ToBoolean(1);
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[2] = Conversions.ToBoolean(0);
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[3] = Conversions.ToBoolean(1);
                            return;
                        }
                    case 4:         // Free-Free
                        {
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[0] = Conversions.ToBoolean(1);
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[1] = Conversions.ToBoolean(1);
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[2] = Conversions.ToBoolean(0);
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[3] = Conversions.ToBoolean(1);
                            return;
                        }
                    case 5:         // Fixed-Pinned
                        {
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[0] = Conversions.ToBoolean(0);
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[1] = Conversions.ToBoolean(0);
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[2] = Conversions.ToBoolean(0);
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[3] = Conversions.ToBoolean(1);
                            return;
                        }
                    case 6:         // pinned-free
                        {
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[0] = Conversions.ToBoolean(0);
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[1] = Conversions.ToBoolean(1);
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[2] = Conversions.ToBoolean(0);
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[3] = Conversions.ToBoolean(1);
                            return;
                        }
                }
            }
            else if (Indexes.mem.IndexOf(M) == Indexes.mem.Count - 1)
            {
                switch (Indexes.ends)
                {
                    case 1:         // Fixed-Fixed
                        {
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[0] = Conversions.ToBoolean(0);
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[1] = Conversions.ToBoolean(1);
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[2] = Conversions.ToBoolean(0);
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[3] = Conversions.ToBoolean(0);
                            return;
                        }
                    case 2:         // Fixed-Free
                        {
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[0] = Conversions.ToBoolean(0);
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[1] = Conversions.ToBoolean(1);
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[2] = Conversions.ToBoolean(1);
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[3] = Conversions.ToBoolean(1);
                            return;
                        }
                    case 3:         // Pinned-Pinned
                        {
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[0] = Conversions.ToBoolean(0);
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[1] = Conversions.ToBoolean(1);
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[2] = Conversions.ToBoolean(0);
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[3] = Conversions.ToBoolean(1);
                            return;
                        }
                    case 4:         // Free-Free
                        {
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[0] = Conversions.ToBoolean(0);
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[1] = Conversions.ToBoolean(1);
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[2] = Conversions.ToBoolean(1);
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[3] = Conversions.ToBoolean(1);
                            return;
                        }
                    case 5:         // Fixed-Pinned
                        {
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[0] = Conversions.ToBoolean(0);
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[1] = Conversions.ToBoolean(1);
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[2] = Conversions.ToBoolean(0);
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[3] = Conversions.ToBoolean(1);
                            return;
                        }
                    case 6:         // pinned-free
                        {
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[0] = Conversions.ToBoolean(0);
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[1] = Conversions.ToBoolean(1);
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[2] = Conversions.ToBoolean(1);
                            Indexes.mem[Indexes.mem.IndexOf(M)].DOF[3] = Conversions.ToBoolean(1);
                            return;
                        }
                }
            }
            else
            {
                Indexes.mem[Indexes.mem.IndexOf(M)].DOF[0] = Conversions.ToBoolean(0);
                Indexes.mem[Indexes.mem.IndexOf(M)].DOF[1] = Conversions.ToBoolean(1);
                Indexes.mem[Indexes.mem.IndexOf(M)].DOF[2] = Conversions.ToBoolean(0);
                Indexes.mem[Indexes.mem.IndexOf(M)].DOF[3] = Conversions.ToBoolean(1);
                return;
            }

        }

        #region  Fixed End Reaction - Force & Moment
        private static void FixFER(Member E)
        {
            // <------Finding reaction--------->
            double Fi = default, Mi = default, Fj = default, Mj = default;
            FER_Total(ref Fi, ref Mi, ref Fj, ref Mj, E);

            Tmem[Tmem.IndexOf(E)].FER[0] = (float)Fi; // Upward - ive
            Tmem[Tmem.IndexOf(E)].FER[1] = (float)Mi; // Anti clockwise - ive
            Tmem[Tmem.IndexOf(E)].FER[2] = (float)Fj; // downward + ive
            Tmem[Tmem.IndexOf(E)].FER[3] = (float)Mj; // clockwise + ive
        }

        private static void FER_Total(ref double Fi, ref double Mi, ref double Fj, ref double Mj, Member Fmem)
        {
            Fi = 0d;
            Mi = 0d;
            Fj = 0d;
            Mj = 0d;
            double Tmi, Tmj, Tfi, Tfj;
            double Length = (double)Fmem.spanlength;
            double K, a, b, c;
            foreach (var PL in Fmem.Pload)
            {
                K = (double)PL.pload;
                a = (double)PL.pdist;
                b = Length - (double)PL.pdist;

                Tmi = (4d * deltaI_PointLoad((double)PL.pload, b, Length) - 2d * deltaJ_PointLoad((double)PL.pload, a, Length)) / Length;
                Tmj = (2d * deltaI_PointLoad((double)PL.pload, b, Length) - 4d * deltaJ_PointLoad((double)PL.pload, a, Length)) / Length;
                Tfi = (K * b + Tmi + Tmj) / Length;
                Tfj = (K * a - Tmi - Tmj) / Length;

                Mi = Mi + Tmi;
                Mj = Mj + Tmj;
                Fi = Fi + Tfi;
                Fj = Fj + Tfj;
            }
            foreach (var ML in Fmem.Mload)
            {
                K = (double)ML.mload;
                a = (double)ML.mdist;
                b = Length - (double)ML.mdist;

                Tmi = (4d * deltaI_Momentoad((double)ML.mload, b, Length) - 2d * deltaJ_Momentoad((double)ML.mload, a, Length)) / Length;
                Tmj = (2d * deltaI_Momentoad((double)ML.mload, b, Length) - 4d * deltaJ_Momentoad((double)ML.mload, a, Length)) / Length;
                Tfi = (K * b + Tmi + Tmj) / Length;
                Tfj = (K * a - Tmi - Tmj) / Length;

                Mi = Mi + (double)ML.mload * b * (3d * a - Length) / Math.Pow(Length, 2d);  // + Tmi
                Mj = Mj + (double)ML.mload * a * (3d * b - Length) / Math.Pow(Length, 2d); // + Tmj
                Fi = Fi + (double)(6f * ML.mload) * a * b / Math.Pow(Length, 3d); // + Tfi
                Fj = Fj - (double)(6f * ML.mload) * a * b / Math.Pow(Length, 3d); // + Tfj
            }
            foreach (var UL in Fmem.Uload)
            {
                if (UL.uload1 == UL.uload2)
                {
                    c = (double)(UL.udist2 - UL.udist1);
                    K = (double)UL.uload1 * c;
                    a = (double)UL.udist1 + c / 2d;
                    b = Length - ((double)UL.udist2 - c / 2d);

                    Tmi = (4d * deltaI_UVLoadCase1((double)UL.uload1, a, b, c, Length) - 2d * deltaJ_UVLoadCase1((double)UL.uload1, a, b, c, Length)) / Length;
                    Tmj = (2d * deltaI_UVLoadCase1((double)UL.uload1, a, b, c, Length) - 4d * deltaJ_UVLoadCase1((double)UL.uload1, a, b, c, Length)) / Length;
                    Tfi = (K * b + Tmi + Tmj) / Length;
                    Tfj = (K * a - Tmi - Tmj) / Length;

                    Mi = Mi + Tmi;
                    Mj = Mj + Tmj;
                    Fi = Fi + Tfi;
                    Fj = Fj + Tfj;
                }
                else if (UL.uload2 > UL.uload1)
                {
                    // ---------For the smallest of load - ie., uniformly distributed
                    c = (double)(UL.udist2 - UL.udist1);
                    K = (double)UL.uload1 * c;
                    a = (double)UL.udist1 + c / 2d;
                    b = Length - ((double)UL.udist2 - c / 2d);

                    Tmi = (4d * deltaI_UVLoadCase1((double)UL.uload1, a, b, c, Length) - 2d * deltaJ_UVLoadCase1((double)UL.uload1, a, b, c, Length)) / Length;
                    Tmj = (2d * deltaI_UVLoadCase1((double)UL.uload1, a, b, c, Length) - 4d * deltaJ_UVLoadCase1((double)UL.uload1, a, b, c, Length)) / Length;
                    Tfi = (K * b + Tmi + Tmj) / Length;
                    Tfj = (K * a - Tmi - Tmj) / Length;

                    Mi = Mi + Tmi;
                    Mj = Mj + Tmj;
                    Fi = Fi + Tfi;
                    Fj = Fj + Tfj;

                    // ---------For the varrying load
                    c = (double)(UL.udist2 - UL.udist1);
                    K = (double)(UL.uload2 - UL.uload1) * (c / 2d);
                    a = (double)UL.udist1 + 2d * c / 3d;
                    b = Length - ((double)UL.udist2 - c / 3d);

                    Tmi = (4d * deltaI_UVLoadCase2((double)(UL.uload2 - UL.uload1), a, c, Length) - 2d * deltaJ_UVLoadCase2((double)(UL.uload2 - UL.uload1), b, c, Length)) / Length;
                    Tmj = (2d * deltaI_UVLoadCase2((double)(UL.uload2 - UL.uload1), a, c, Length) - 4d * deltaJ_UVLoadCase2((double)(UL.uload2 - UL.uload1), b, c, Length)) / Length;
                    Tfi = (K * b + Tmi + Tmj) / Length;
                    Tfj = (K * a - Tmi - Tmj) / Length;

                    Mi = Mi + Tmi;
                    Mj = Mj + Tmj;
                    Fi = Fi + Tfi;
                    Fj = Fj + Tfj;
                }
                else if (UL.uload2 < UL.uload1)
                {
                    // ---------For the smallest of load - ie., uniformly distributed
                    c = (double)(UL.udist2 - UL.udist1);
                    K = (double)UL.uload2 * c;
                    a = (double)UL.udist1 + c / 2d;
                    b = Length - ((double)UL.udist2 - c / 2d);

                    Tmi = (4d * deltaI_UVLoadCase1((double)UL.uload2, a, b, c, Length) - 2d * deltaJ_UVLoadCase1((double)UL.uload2, a, b, c, Length)) / Length;
                    Tmj = (2d * deltaI_UVLoadCase1((double)UL.uload2, a, b, c, Length) - 4d * deltaJ_UVLoadCase1((double)UL.uload2, a, b, c, Length)) / Length;
                    Tfi = (K * b + Tmi + Tmj) / Length;
                    Tfj = (K * a - Tmi - Tmj) / Length;

                    Mi = Mi + Tmi;
                    Mj = Mj + Tmj;
                    Fi = Fi + Tfi;
                    Fj = Fj + Tfj;

                    // ---------For the varrying load
                    c = (double)(UL.udist2 - UL.udist1);
                    K = (double)(UL.uload1 - UL.uload2) * (c / 2d);
                    a = (double)UL.udist1 + c / 3d;
                    b = Length - ((double)UL.udist2 - 2d * c / 3d);

                    Tmi = (4d * deltaI_UVLoadCase3((double)(UL.uload1 - UL.uload2), a, c, Length) - 2d * deltaJ_UVLoadCase3((double)(UL.uload1 - UL.uload2), b, c, Length)) / Length;
                    Tmj = (2d * deltaI_UVLoadCase3((double)(UL.uload1 - UL.uload2), a, c, Length) - 4d * deltaJ_UVLoadCase3((double)(UL.uload1 - UL.uload2), b, c, Length)) / Length;
                    Tfi = (K * b + Tmi + Tmj) / Length;
                    Tfj = (K * a - Tmi - Tmj) / Length;

                    Mi = Mi + Tmi;
                    Mj = Mj + Tmj;
                    Fi = Fi + Tfi;
                    Fj = Fj + Tfj;
                }
            }
        }

        private static double deltaI_PointLoad(double W, double b, double L)
        {
            double Di;
            Di = W * b * (Math.Pow(L, 2d) - Math.Pow(b, 2d)) / (6d * L);
            return Di;
        }

        private static double deltaJ_PointLoad(double W, double a, double L)
        {
            double Dj;
            Dj = W * a * (Math.Pow(L, 2d) - Math.Pow(a, 2d)) / (6d * L);
            return Dj;
        }

        private static double deltaI_UVLoadCase1(double W, double a, double b, double c, double L)
        {
            // -------- Uload1 = uload2
            double Di;
            Di = W * b * c * (4d * a * (b + L) - Math.Pow(c, 2d)) / (24d * L);
            return Di;
        }

        private static double deltaJ_UVLoadCase1(double W, double a, double b, double c, double L)
        {
            // -------- Uload1 = uload2
            double Dj;
            Dj = W * a * c * (4d * b * (a + L) - Math.Pow(c, 2d)) / (24d * L);
            return Dj;
        }

        private static double deltaI_UVLoadCase2(double W, double a, double c, double L)
        {
            // -------- Uload1 < uload2
            double Di, alpha, gamma;
            alpha = a / L;
            gamma = c / L;
            Di = W * Math.Pow(L, 3d) * gamma * (270d * (alpha - Math.Pow(alpha, 3d)) - Math.Pow(gamma, 2d) * (45d * alpha - 2d * gamma)) / 3240;

            return Di;
        }

        private static double deltaJ_UVLoadCase2(double W, double b, double c, double L)
        {
            // -------- Uload1 < uload2
            double Dj, beta, gamma;
            beta = b / L;
            gamma = c / L;
            Dj = W * Math.Pow(L, 3d) * gamma * (270d * (beta - Math.Pow(beta, 3d)) - Math.Pow(gamma, 2d) * (45d * beta + 2d * gamma)) / 3240;

            return Dj;
        }

        private static double deltaI_UVLoadCase3(double W, double a, double c, double L)
        {
            // -------- Uload1 > uload2
            double Di, alpha, gamma;
            alpha = a / L;
            gamma = c / L;
            Di = W * Math.Pow(L, 3d) * gamma * (270d * (alpha - Math.Pow(alpha, 3d)) - Math.Pow(gamma, 2d) * (45d * alpha + 2d * gamma)) / 3240;

            return Di;
        }

        private static double deltaJ_UVLoadCase3(double W, double b, double c, double L)
        {
            // -------- Uload1 > uload2
            double Dj, beta, gamma;
            beta = b / L;
            gamma = c / L;
            Dj = W * Math.Pow(L, 3d) * gamma * (270d * (beta - Math.Pow(beta, 3d)) - Math.Pow(gamma, 2d) * (45d * beta - 2d * gamma)) / 3240;

            return Dj;
        }

        private static double deltaI_Momentoad(double W, double b, double L)
        {
            double Di;
            Di = W * (3d * Math.Pow(b, 2d) - Math.Pow(L, 2d)) / (6d * L);
            return Di;
        }

        private static double deltaJ_Momentoad(double W, double a, double L)
        {
            double Dj;
            Dj = W * (Math.Pow(L, 2d) - 3d * Math.Pow(a, 2d)) / (6d * L);
            return Dj;
        }
        #endregion

        private static void GStiffness(Member M, ref double[,] gm)
        {
            // -----------Stiffness matrix for Members --------------
            double c1 = (double)(M.Emodulus * M.Inertia) / ((double)M.spanlength * (Math.Pow((double)M.spanlength, 2d) + (double)(12f * M.g)));

            Tmem[Tmem.IndexOf(M)].stiff[0, 0] = (float)(12d * c1);
            Tmem[Tmem.IndexOf(M)].stiff[0, 1] = (float)((double)(6f * M.spanlength) * c1);
            Tmem[Tmem.IndexOf(M)].stiff[0, 2] = (float)(-12 * c1);
            Tmem[Tmem.IndexOf(M)].stiff[0, 3] = (float)((double)(6f * M.spanlength) * c1);
            Tmem[Tmem.IndexOf(M)].stiff[1, 0] = (float)((double)(6f * M.spanlength) * c1);
            Tmem[Tmem.IndexOf(M)].stiff[1, 1] = (float)((4d * Math.Pow((double)M.spanlength, 2d) + (double)(12f * M.g)) * c1);
            Tmem[Tmem.IndexOf(M)].stiff[1, 2] = (float)((double)(-6 * M.spanlength) * c1);
            Tmem[Tmem.IndexOf(M)].stiff[1, 3] = (float)((2d * Math.Pow((double)M.spanlength, 2d) - (double)(12f * M.g)) * c1);
            Tmem[Tmem.IndexOf(M)].stiff[2, 0] = (float)(-12 * c1);
            Tmem[Tmem.IndexOf(M)].stiff[2, 1] = (float)((double)(-6 * M.spanlength) * c1);
            Tmem[Tmem.IndexOf(M)].stiff[2, 2] = (float)(12d * c1);
            Tmem[Tmem.IndexOf(M)].stiff[2, 3] = (float)((double)(-6 * M.spanlength) * c1);
            Tmem[Tmem.IndexOf(M)].stiff[3, 0] = (float)((double)(6f * M.spanlength) * c1);
            Tmem[Tmem.IndexOf(M)].stiff[3, 1] = (float)((2d * Math.Pow((double)M.spanlength, 2d) - (double)(12f * M.g)) * c1);
            Tmem[Tmem.IndexOf(M)].stiff[3, 2] = (float)((double)(-6 * M.spanlength) * c1);
            Tmem[Tmem.IndexOf(M)].stiff[3, 3] = (float)((4d * Math.Pow((double)M.spanlength, 2d) + (double)(12f * M.g)) * c1);
            int d = Tmem.IndexOf(M) * 2;
            for (int l = 0; l <= 3; l++)
            {
                for (int k = 0; k <= 3; k++)
                    gm[l + d, k + d] = gm[l + d, k + d] + Tmem[Tmem.IndexOf(M)].stiff[l, k];
            }
        }

        private static void FixDM(ref int[] dm)
        {
            foreach (var E in Tmem)
            {
                dm[Tmem.IndexOf(E) * 2] = Conversions.ToInteger(E.DOF[0]);
                dm[Tmem.IndexOf(E) * 2 + 1] = Conversions.ToInteger(E.DOF[1]);
                dm[Tmem.IndexOf(E) * 2 + 2] = Conversions.ToInteger(E.DOF[2]);
                dm[Tmem.IndexOf(E) * 2 + 3] = Conversions.ToInteger(E.DOF[3]);
            }
        }

        private static void FixFM(ref double[] fm)
        {
            foreach (var E in Tmem)
            {
                if (Tmem.IndexOf(E) == 0)
                {
                    fm[Tmem.IndexOf(E) * 2] = E.FER[0];
                    fm[Tmem.IndexOf(E) * 2 + 1] = E.FER[1];
                    fm[Tmem.IndexOf(E) * 2 + 2] = E.FER[2];
                    fm[Tmem.IndexOf(E) * 2 + 3] = E.FER[3];
                    continue;
                }
                fm[Tmem.IndexOf(E) * 2] = fm[Tmem.IndexOf(E) * 2] + E.FER[0];
                fm[Tmem.IndexOf(E) * 2 + 1] = fm[Tmem.IndexOf(E) * 2 + 1] + E.FER[1];
                fm[Tmem.IndexOf(E) * 2 + 2] = E.FER[2];
                fm[Tmem.IndexOf(E) * 2 + 3] = E.FER[3];
            }
        }

        private static void Curtailment(ref double[,] gm, ref int[] dofm, ref double[] ferm, ref int cb)
        {
            var tgm = new double[Tmem.Count * 2 + 1 + 1, Tmem.Count * 2 + 1 + 1];
            var tdofm = new int[Tmem.Count * 2 + 1 + 1];
            var tferm = new double[Tmem.Count * 2 + 1 + 1];

            int r = default, s;
            for (int p = 0, loopTo = Tmem.Count * 2 + 1; p <= loopTo; p++)
            {
                if (dofm[p] == 0)
                {
                    continue;
                }
                else
                {
                    s = 0;
                    for (int t = 0, loopTo1 = Tmem.Count * 2 + 1; t <= loopTo1; t++)
                    {
                        if (dofm[t] == 0)
                        {
                            continue;
                        }
                        else
                        {
                            tferm[s] = ferm[t];
                            tdofm[s] = dofm[t];
                            tgm[r, s] = gm[p, t];
                            s = s + 1;
                        }
                    }
                    r = r + 1;
                }
            }

            gm = new double[r, r];
            dofm = new int[r];
            ferm = new double[r];

            for (int p = 0, loopTo2 = r - 1; p <= loopTo2; p++)
            {
                dofm[p] = tdofm[p];
                ferm[p] = tferm[p];
                for (int t = 0, loopTo3 = r - 1; t <= loopTo3; t++)
                    gm[p, t] = tgm[p, t];
            }
            cb = r;
        }

        private static void GElimination(ref double[,] A, ref double[] B, ref double[] re, int cb)
        {
            // ----Check For Double Span
            if (Tmem.Count - 1 <= 0)
            {
                return;
            }

            double[,] Triangular_A = new double[cb + 1, cb + 1 + 1];
            double line_1 = default, temporary_1, multiplier_1, sum_1;
            var soln = new double[cb + 1]; // Solution matrix
            for (int n = 0, loopTo = cb; n <= loopTo; n++)
            {
                for (int m = 0, loopTo1 = cb; m <= loopTo1; m++)
                    Triangular_A[m, n] = A[m, n];
            }

            // .... substituting the force to triangularmatrics....
            for (int n = 0, loopTo2 = cb; n <= loopTo2; n++)
                Triangular_A[n, cb + 1] = B[n];

            // ...............soving the triangular matrics.............
            for (int k = 0, loopTo3 = cb; k <= loopTo3; k++)
            {
                // ......Bring a non-zero element first by changes lines if necessary
                if (Triangular_A[k, k] == 0d)
                {
                    for (int n = k, loopTo4 = cb; n <= loopTo4; n++)
                    {
                        if (Triangular_A[n, k] != 0d)
                        {
                            line_1 = n;
                            break;
                        } // Finds line_1 with non-zero element
                    }
                    // ..........Change line k with line_1
                    for (int m = k, loopTo5 = cb; m <= loopTo5; m++)
                    {
                        temporary_1 = Triangular_A[k, m];
                        Triangular_A[k, m] = Triangular_A[(int)Math.Round(line_1), m];
                        Triangular_A[(int)Math.Round(line_1), m] = temporary_1;
                    }
                }
                // ....For other lines, make a zero element by using:
                // .........Ai1=Aij-A11*(Aij/A11)
                // .....and change all the line using the same formula for other elements
                for (int n = k + 1, loopTo6 = cb; n <= loopTo6; n++)
                {
                    if (Triangular_A[n, k] != 0d) // if it is zero, stays as it is
                    {
                        multiplier_1 = Triangular_A[n, k] / Triangular_A[k, k];
                        for (int m = k, loopTo7 = cb + 1; m <= loopTo7; m++)
                            Triangular_A[n, m] = Triangular_A[n, m] - Triangular_A[k, m] * multiplier_1;
                    }
                }
            }


            // ..... calculating the dof value..........

            // First, calculate last xi (for i = System_DIM)
            soln[cb] = Triangular_A[cb, cb + 1] / Triangular_A[cb, cb];

            // ................
            for (int n = 0, loopTo8 = cb; n <= loopTo8; n++)
            {
                sum_1 = 0d;
                for (int m = 0, loopTo9 = n; m <= loopTo9; m++)
                    sum_1 = sum_1 + soln[cb + 1 - m] * Triangular_A[cb - n, cb - m];
                soln[cb - n] = (Triangular_A[cb - n, cb + 1] - sum_1) / Triangular_A[cb - n, cb - n];

            }

            for (int n = 0, loopTo10 = cb; n <= loopTo10; n++)
                re[n] = soln[n + 1];
        }

        #region Gauss Elimination Method
        // -----Redefined Gauss Elimination Procedure
        private static void Gauss(double[,] A, double[] B, ref double[] X, int Bound)
        {
            var Triangular_A = new double[Bound + 1, Bound + 1 + 1];
            var soln = new double[Bound + 1]; // Solution matrix
            for (int m = 0, loopTo = Bound; m <= loopTo; m++)
            {
                for (int n = 0, loopTo1 = Bound; n <= loopTo1; n++)
                    Triangular_A[m, n] = A[m, n];
            }
            // .... substituting the force to triangularmatrics....
            for (int n = 0, loopTo2 = Bound; n <= loopTo2; n++)
                Triangular_A[n, Bound + 1] = B[n];
            ForwardSubstitution(ref Triangular_A, Bound);
            ReverseElimination(ref Triangular_A, ref X, Bound);
        }


        private static void ForwardSubstitution(ref double[,] _triang, int bound)
        {
            // Forward Elimination
            // Dim _fraction As Double
            for (int k = 0, loopTo = bound - 1; k <= loopTo; k++)
            {
                for (int i = k + 1, loopTo1 = bound; i <= loopTo1; i++)
                {
                    if (_triang[k, k] == 0d)
                    {
                        continue;
                    }
                    _triang[i, k] = _triang[i, k] / _triang[k, k];
                    for (int j = k + 1, loopTo2 = bound + 1; j <= loopTo2; j++)
                        _triang[i, j] = _triang[i, j] - _triang[i, k] * _triang[k, j];
                }
            }
        }

        private static void ReverseElimination(ref double[,] _triang, ref double[] X, int bound)
        {
            // Back Substitution
            for (int i = 0, loopTo = bound; i <= loopTo; i++)
                X[i] = _triang[i, bound + 1];

            for (int i = bound; i >= 0; i -= 1)
            {
                for (int j = i + 1, loopTo1 = bound; j <= loopTo1; j++)
                    X[i] = X[i] - _triang[i, j] * X[j];
                X[i] = X[i] / _triang[i, i];
            }
        }
        #endregion

        private static void Welding(ref double[] re, ref double[] fer, ref int[] dm, ref double[] Theta_Delta_matrix)
        {
            var tres = new double[Tmem.Count * 2 + 1 + 1];
            var j = default(int);
            for (int i = 0, loopTo = Tmem.Count * 2 + 1; i <= loopTo; i++)
            {
                if (dm[i] == 0)
                {
                    continue;
                }
                tres[i] = tres[i] + re[j];
                j = j + 1;
            }
            re = new double[Tmem.Count * 2 + 1 + 1];
            Theta_Delta_matrix = new double[Tmem.Count * 2 + 1 + 1];
            for (int i = 0, loopTo1 = Tmem.Count * 2 + 1; i <= loopTo1; i++)
            {
                re[i] = tres[i];
                Theta_Delta_matrix[i] = tres[i];
            }
        }

        private static void GMultiplier(ref double[,] gm, ref double[] re)
        {
            var teR = new double[Tmem.Count * 2 + 1 + 1];
            for (int i = 0, loopTo = Tmem.Count * 2 + 1; i <= loopTo; i++)
            {
                teR[i] = 0d;
                for (int j = 0, loopTo1 = Tmem.Count * 2 + 1; j <= loopTo1; j++)
                    teR[i] = teR[i] + gm[i, j] * re[j];
            }
            for (int i = 0, loopTo2 = Tmem.Count * 2 + 1; i <= loopTo2; i++)
                re[i] = teR[i];
        }

        private static void loadMINU(ref double[] re, ref double[] lo, ref double[] ThetaDelta)
        {
            for (int i = 0, loopTo = Tmem.Count * 2 + 1; i <= loopTo; i++)
                re[i] = re[i] - lo[i];
            foreach (var E in Tmem)
            {
                Tmem[Tmem.IndexOf(E)].RES[0] = (float)re[Tmem.IndexOf(E) * 2];
                Tmem[Tmem.IndexOf(E)].RES[1] = (float)re[Tmem.IndexOf(E) * 2 + 1];
                Tmem[Tmem.IndexOf(E)].RES[2] = (float)re[Tmem.IndexOf(E) * 2 + 2];
                Tmem[Tmem.IndexOf(E)].RES[3] = (float)re[Tmem.IndexOf(E) * 2 + 3];

                Tmem[Tmem.IndexOf(E)].DISP[0] = (float)ThetaDelta[Tmem.IndexOf(E) * 2];
                Tmem[Tmem.IndexOf(E)].DISP[1] = (float)ThetaDelta[Tmem.IndexOf(E) * 2 + 1];
                Tmem[Tmem.IndexOf(E)].DISP[2] = (float)ThetaDelta[Tmem.IndexOf(E) * 2 + 2];
                Tmem[Tmem.IndexOf(E)].DISP[3] = (float)ThetaDelta[Tmem.IndexOf(E) * 2 + 3];
            }
        }
        #endregion

        #region Co-ordinate calculation
        public static void CoordinateCalculator()
        {
            // ------- Normalized Equilibrium Member
            var FRmem = new Member();

            FixEquilibriumMember(ref FRmem);

            Indexes.BeamCoords.Clear();
            Indexes.DX.Clear();
            Indexes.SF.Clear();
            Indexes.BM.Clear();
            Indexes.SL.Clear();
            Indexes.DE.Clear();

            // ------- Shear Force & Bending moment Coordinate Fixing
            FixDisintegration(FRmem);
            foreach (var X in Indexes.DX)
            {
                Indexes.BeamCoords.Add(-(My.MyProject.Forms.beamcreate.coverpic.Width / 2d) + 100d + (double)((My.MyProject.Forms.beamcreate.coverpic.Width - 200) / FRmem.spanlength) * X);
                Indexes.SF.Add(Total_ShearForce_L(Math.Round(X, 2), FRmem));
                Indexes.BM.Add(Total_BendingMoment_L(Math.Round(X, 2), FRmem));
            }
            FixShearForce_Coords(FRmem);
            FixDisplacement_Slope_Deflection(FRmem);
            FixBendingMoment_Coords();
            FixSlopeDeflection_Coords();
        }

        private static void FixDisintegration(Member FRmem)
        {
            double _dx = (double)(FRmem.spanlength / (My.MyProject.Forms.beamcreate.coverpic.Width - 200));
            double _LoopV = 0d;
            var TempDX = new List<double>();
            var loopTo = (double)FRmem.spanlength;
            for (_LoopV = 0d; _dx >= 0 ? _LoopV <= loopTo : _LoopV >= loopTo; _LoopV += _dx)
                TempDX.Add(_LoopV);
            foreach (var Pl in FRmem.Pload)
            {
                if (TempDX.Contains((double)Pl.pdist) == false)
                {
                    TempDX.Add((double)Pl.pdist);
                }
            }
            foreach (var Ul in FRmem.Uload)
            {
                if (TempDX.Contains((double)Ul.udist1) == false)
                {
                    TempDX.Add((double)Ul.udist1);
                }
                if (TempDX.Contains((double)Ul.udist2) == false)
                {
                    TempDX.Add((double)Ul.udist2);
                }
            }
            foreach (var Ml in FRmem.Mload)
            {
                if (TempDX.Contains((double)Ml.mdist) == false)
                {
                    TempDX.Add((double)Ml.mdist);
                }
            }
            TempDX.Sort();
            Indexes.DX = TempDX;
        }

        private static void FixEquilibriumMember(ref Member FRmem)
        {
            double TempLength = 0d;
            var Reaction_PL_L = default(Member.P);
            var Reaction_ML_L = default(Member.M);
            var Reaction_PL_R = default(Member.P);
            var Reaction_ML_R = default(Member.M);
            foreach (var E in Indexes.mem)
            {
                // -------Left End reaction
                if (Math.Round(E.RES[0], 2) != 0d)
                {
                    Reaction_PL_L.pload = E.RES[0];
                    Reaction_PL_L.pdist = (float)TempLength;
                    FRmem.Pload.Add(Reaction_PL_L);
                }
                // -------Left End moment
                if (Math.Round(E.RES[1], 2) != 0d)
                {
                    Reaction_ML_L.mload = E.RES[1];
                    Reaction_ML_L.mdist = (float)TempLength;
                    FRmem.Mload.Add(Reaction_ML_L);
                }
                foreach (var Pl in E.Pload)
                {
                    var TPl = new Member.P();
                    TPl.pdist = (float)(TempLength + (double)Pl.pdist);
                    TPl.pload = Pl.pload;
                    FRmem.Pload.Add(TPl);
                }
                foreach (var Ul in E.Uload)
                {
                    var TUl = new Member.U();
                    TUl.udist1 = (float)(TempLength + (double)Ul.udist1);
                    TUl.udist2 = (float)(TempLength + (double)Ul.udist2);
                    TUl.uload1 = Ul.uload1;
                    TUl.uload2 = Ul.uload2;
                    FRmem.Uload.Add(TUl);
                }
                foreach (var Ml in E.Mload)
                {
                    var TMl = new Member.M();
                    TMl.mdist = (float)(TempLength + (double)Ml.mdist);
                    TMl.mload = -Ml.mload;  // --Clockwise +ive
                    FRmem.Mload.Add(TMl);
                }

                if (Indexes.mem.IndexOf(E) == Indexes.mem.Count - 1)
                {
                    // -------Right End reaction
                    if (Math.Round(E.RES[2], 2) != 0d)
                    {
                        Reaction_PL_R.pload = E.RES[2];
                        Reaction_PL_R.pdist = (float)(TempLength + (double)E.spanlength);
                        FRmem.Pload.Add(Reaction_PL_R);
                    }
                    // -------Right End moment
                    if (Math.Round(E.RES[3], 2) != 0d)
                    {
                        Reaction_ML_R.mload = E.RES[3];
                        Reaction_ML_R.mdist = (float)(TempLength + (double)E.spanlength);
                        FRmem.Mload.Add(Reaction_ML_R);
                    }
                }
                TempLength = TempLength + (double)E.spanlength;
            }
            FRmem.spanlength = (float)TempLength;
            My.MyProject.Forms.beamcreate.Tlength = (float)TempLength;
        }

        #region Shear Force Calculator
        private static double Total_ShearForce_L(double _curDx, Member Fmem)
        {
            // -----Function Returns Total Shear Force in a point from left to right
            double SF_L;
            SF_L = SF_PointLoad_L(_curDx, Fmem) + SF_UVL_L(_curDx, Fmem);
            return SF_L;
        }

        private static double SF_PointLoad_L(double _curDx, Member Fmem)
        {
            // ----Shear Force due to point load from left to right
            double SF, T_SF = default;
            foreach (var PL in Fmem.Pload)
            {
                SF = 0d;
                if (_curDx >= (double)PL.pdist)
                {
                    SF = (double)PL.pload;
                }
                T_SF = T_SF + SF;
            }
            return T_SF;
        }

        private static double SF_UVL_L(double _curDx, Member Fmem)
        {
            // ----Shear force due to UVL from left to right
            double _RectF, _TriF, _SecF, SF, T_SF = default;
            foreach (var UL in Fmem.Uload)
            {
                SF = 0d;
                if (UL.uload1 <= UL.uload2)
                {
                    if (_curDx >= (double)UL.udist2)
                    {
                        _RectF = (double)(UL.uload1 * (UL.udist2 - UL.udist1));
                        _TriF = 0.5d * (double)(UL.uload2 - UL.uload1) * (double)(UL.udist2 - UL.udist1);
                        SF = _RectF + _TriF;
                    }
                    else if (_curDx >= (double)UL.udist1 & _curDx < (double)UL.udist2)
                    {
                        _RectF = (double)UL.uload1 * (_curDx - (double)UL.udist1);
                        _SecF = (double)((UL.uload2 - UL.uload1) / (UL.udist2 - UL.udist1)) * (_curDx - (double)UL.udist1);
                        _TriF = 0.5d * _SecF * (_curDx - (double)UL.udist1);
                        SF = _RectF + _TriF;
                    }
                    T_SF = T_SF + SF;
                }
                else
                {
                    if (_curDx >= (double)UL.udist2)
                    {
                        _RectF = (double)(UL.uload2 * (UL.udist2 - UL.udist1));
                        _TriF = 0.5d * (double)(UL.uload1 - UL.uload2) * (double)(UL.udist2 - UL.udist1);
                        SF = _RectF + _TriF;
                    }
                    else if (_curDx >= (double)UL.udist1 & _curDx < (double)UL.udist2)
                    {
                        _SecF = (double)UL.uload2 + (double)((UL.uload1 - UL.uload2) / (UL.udist2 - UL.udist1)) * ((double)UL.udist2 - _curDx);
                        _RectF = _SecF * (_curDx - (double)UL.udist1);
                        _TriF = 0.5d * ((double)UL.uload1 - _SecF) * (_curDx - (double)UL.udist1);
                        SF = _RectF + _TriF;
                    }
                    T_SF = T_SF + SF;
                }
            }
            return T_SF;
        }

        private static void FixShearForce_Coords(Member Fmem)
        {
            FixShearForce_CurveCoords(Fmem);
            FixShearForce_ShowCoords(Fmem);
        }

        private static void FixShearForce_CurveCoords(Member Fmem)
        {
            // -------Finding maximum shear force for scaling the shear curve
            double maxV = 0d;
            foreach (var SForce in Indexes.SF)
            {
                if (Math.Abs(SForce) > maxV)
                {
                    maxV = Math.Abs(SForce);
                }
            }
            Indexes.ShearM = maxV;
            maxV = 180d / maxV;

            // --------Fixing the curve coordinates
            Indexes.SFpts = new PointF[Indexes.SF.Count];
            int f = 0;

            foreach (var S in Indexes.SF)
            {
                Indexes.SFpts[f].X = (float)Indexes.BeamCoords[f];
                Indexes.SFpts[f].Y = (float)(S * maxV + (double)(My.MyProject.Forms.beamcreate.MEheight / 2f));
                f = f + 1;
            }
        }

        private static void FixShearForce_ShowCoords(Member Fmem)
        {
            var CDSindex = new List<int>();
            CaptureLoadLocations(ref CDSindex, Fmem);
            Indexes.SFMc.Clear();
            Indexes.SFmaxs = new PointF[CDSindex.Count];
            int f = 0;
            foreach (var CD in CDSindex)
            {
                Indexes.SFMc.Add(CD);
                Indexes.SFmaxs[f].X = Indexes.SFpts[CD].X;
                Indexes.SFmaxs[f].Y = Indexes.SFpts[CD].Y + (Indexes.SFpts[CD].Y > My.MyProject.Forms.beamcreate.MEheight / 2f ? 15 : -30);
                f = f + 1;
            }
        }

        private static void CaptureLoadLocations(ref List<int> _CDIndex, Member Fmem)
        {
            // ------Capturing the shear force indexes for displaying
            List<double> _CDLoc;
            var _TemCDindex = new List<int>();
            double[] LoadLocation;
            int i = 0;

            if (Fmem.Pload.Count != 0)
            {
                i = i + Fmem.Pload.Count;
            }
            LoadLocation = new double[i];
            // _______________________________________________________________________
            // ------Capturing Point Load
            i = 0;
            foreach (var PL in Fmem.Pload)
            {
                LoadLocation[i] = PL.pdist;
                i = i + 1;
            }
            // ------Checking for zero values and identical values and adding to list
            // ------Only for point loads --> coz need to capture left and right
            _CDLoc = new List<double>();
            for (int j = 0, loopTo = i - 1; j <= loopTo; j++)
            {
                if (_CDLoc.Contains(LoadLocation[j]) == false)
                {
                    _CDLoc.Add(LoadLocation[j]);
                }
            }
            // ------Capturing left and right index except for start and end location
            foreach (var CDL in _CDLoc)
            {
                if (CDL == 0d)
                {
                    _TemCDindex.Add(FirstfromLeft_LoadLocation(CDL, Fmem));
                }
                else if (CDL == (double)Fmem.spanlength)
                {
                    _TemCDindex.Add(FirstfromRight_LoadLocation(CDL, Fmem));
                }
                else
                {
                    _TemCDindex.Add(FirstfromLeft_LoadLocation(CDL, Fmem));
                    _TemCDindex.Add(FirstfromRight_LoadLocation(CDL, Fmem));
                }
            }
            // _______________________________________________________________________
            // ------Capturing UVL and Moment 
            i = 0;
            if (Fmem.Uload.Count != 0)
            {
                i = i + Fmem.Uload.Count * 2;
            }
            if (Fmem.Mload.Count != 0)
            {
                i = i + Fmem.Mload.Count;
            }
            LoadLocation = new double[i];
            i = 0;
            foreach (var Ul in Fmem.Uload)
            {
                LoadLocation[i] = Ul.udist1;
                LoadLocation[i + 1] = Ul.udist2;
                i = i + 2;
            }
            foreach (var ML in Fmem.Mload)
            {
                LoadLocation[i] = ML.mdist;
                i = i + 1;
            }
            // ------Checking for zero values and identical values and adding to list
            // ------for UVL & Moment --> need to either left or right
            _CDLoc.Clear();
            for (int j = 0, loopTo1 = i - 1; j <= loopTo1; j++)
            {
                if (_CDLoc.Contains(LoadLocation[j]) == false)
                {
                    _CDLoc.Add(LoadLocation[j]);
                }
            }
            // ------Capturing left and right index except for start and end location
            foreach (var CDL in _CDLoc)
            {
                if (CDL == 0d)
                {
                    _TemCDindex.Add(FirstfromLeft_LoadLocation(CDL, Fmem));
                }
                else if (CDL == (double)Fmem.spanlength)
                {
                    _TemCDindex.Add(FirstfromRight_LoadLocation(CDL, Fmem));
                }
                else
                {
                    _TemCDindex.Add(FirstfromLeft_LoadLocation(CDL, Fmem));
                    // _TemCDindex.Add(FirstRight_LoadLocation(CDL, Fmem))
                }
            }

            // ------Checking for identical values and adding to list
            foreach (var _ind in _TemCDindex)
            {
                if (_CDIndex.Contains(_ind) == false)
                {
                    _CDIndex.Add(_ind);
                }
            }
        }

        private static int FirstfromLeft_LoadLocation(double Loc, Member FRmem)
        {
            double _dx = (double)(FRmem.spanlength / (My.MyProject.Forms.beamcreate.coverpic.Width - 200));
            int J = 0;
            foreach (var X in Indexes.DX)
            {
                if (X == Loc)
                {
                    return J;
                    return default;
                }
                J = J + 1;
            }

            return default;
        }

        private static int FirstfromRight_LoadLocation(double Loc, Member FRmem)
        {
            double _dx = (double)(FRmem.spanlength / (My.MyProject.Forms.beamcreate.coverpic.Width - 200));
            int J = 0;
            foreach (var X in Indexes.DX)
            {
                if (X == Loc)
                {
                    return J - 1;
                    return default;
                }
                J = J + 1;
            }

            return default;
        }
        #endregion

        #region Bending Moment Calculator
        private static double Total_BendingMoment_L(double _curDx, Member Fmem)
        {
            // -----Function Returns Total Bending Moment in a point from left to right
            double BM;
            BM = BM_PointLoad_L(_curDx, Fmem) + BM_UVL_L(_curDx, Fmem) + BM_moment_L(_curDx, Fmem);

            return BM;
        }

        private static double BM_PointLoad_L(double _curDx, Member Fmem)
        {
            // ----Bending moment due to point load from left to right
            double BM, T_BM = default;
            foreach (var PL in Fmem.Pload)
            {
                BM = 0d;
                if (_curDx >= (double)PL.pdist)
                {
                    BM = (double)PL.pload * (_curDx - (double)PL.pdist);
                }
                T_BM = T_BM + BM;
            }
            return T_BM;
        }

        private static double BM_UVL_L(double _curDx, Member Fmem)
        {
            // ----Bending moment due to UVL from left to right
            double _RectF, _TriF, _SecF, BM, T_BM = default;
            double _RectCentroid, _TriCentroid;
            foreach (var UL in Fmem.Uload)
            {
                BM = 0d;
                if (UL.uload1 <= UL.uload2)
                {
                    if (_curDx >= (double)UL.udist2)
                    {
                        _RectF = (double)(UL.uload1 * (UL.udist2 - UL.udist1));
                        _TriF = 0.5d * (double)(UL.uload2 - UL.uload1) * (double)(UL.udist2 - UL.udist1);
                        _RectCentroid = _curDx - ((double)UL.udist1 + (double)(UL.udist2 - UL.udist1) * (1d / 2d));
                        _TriCentroid = _curDx - ((double)UL.udist1 + (double)(UL.udist2 - UL.udist1) * (2d / 3d));
                        BM = _RectF * _RectCentroid + _TriF * _TriCentroid;
                    }
                    else if (_curDx >= (double)UL.udist1 & _curDx < (double)UL.udist2)
                    {
                        _RectF = (double)UL.uload1 * (_curDx - (double)UL.udist1);
                        _SecF = (double)((UL.uload2 - UL.uload1) / (UL.udist2 - UL.udist1)) * (_curDx - (double)UL.udist1);
                        _TriF = 0.5d * _SecF * (_curDx - (double)UL.udist1);
                        _RectCentroid = _curDx - ((double)UL.udist1 + (_curDx - (double)UL.udist1) * (1d / 2d));
                        _TriCentroid = _curDx - ((double)UL.udist1 + (_curDx - (double)UL.udist1) * (2d / 3d));
                        BM = _RectF * _RectCentroid + _TriF * _TriCentroid;
                    }
                    T_BM = T_BM + BM;
                }
                else
                {
                    if (_curDx >= (double)UL.udist2)
                    {
                        _RectF = (double)(UL.uload2 * (UL.udist2 - UL.udist1));
                        _TriF = 0.5d * (double)(UL.uload1 - UL.uload2) * (double)(UL.udist2 - UL.udist1);
                        _RectCentroid = _curDx - ((double)UL.udist1 + (double)(UL.udist2 - UL.udist1) * (1d / 2d));
                        _TriCentroid = _curDx - ((double)UL.udist1 + (double)(UL.udist2 - UL.udist1) * (1d / 3d));
                        BM = _RectF * _RectCentroid + _TriF * _TriCentroid;
                    }
                    else if (_curDx >= (double)UL.udist1 & _curDx < (double)UL.udist2)
                    {
                        _SecF = (double)UL.uload2 + (double)((UL.uload1 - UL.uload2) / (UL.udist2 - UL.udist1)) * ((double)UL.udist2 - _curDx);
                        _RectF = _SecF * (_curDx - (double)UL.udist1);
                        _TriF = 0.5d * ((double)UL.uload1 - _SecF) * (_curDx - (double)UL.udist1);
                        _RectCentroid = _curDx - ((double)UL.udist1 + (_curDx - (double)UL.udist1) * (1d / 2d));
                        _TriCentroid = _curDx - ((double)UL.udist1 + (_curDx - (double)UL.udist1) * (1d / 3d));
                        BM = _RectF * _RectCentroid + _TriF * _TriCentroid;
                    }
                    T_BM = T_BM + BM;
                }
            }
            return T_BM;
        }

        private static double BM_moment_L(double _curDx, Member Fmem)
        {
            // ----- Bending moment due to moment from left to right
            double BM, T_BM = default;
            foreach (var ML in Fmem.Mload)
            {
                BM = 0d;
                if (_curDx >= (double)ML.mdist)
                {
                    BM = (double)-ML.mload;
                }
                T_BM = T_BM + BM;
            }
            return T_BM;
        }

        private static void FixBendingMoment_Coords()
        {
            FixBendingMoment_CurveCoords();
            FixBendingMoment_ShowCoords();
        }

        private static void FixBendingMoment_CurveCoords()
        {
            // -------Finding maximum Bending Moment for scaling the shear curve
            double maxV = 0d;
            foreach (var Bmoment in Indexes.BM)
            {
                if (Math.Abs(Bmoment) > maxV)
                {
                    maxV = Math.Abs(Bmoment);
                }
            }
            Indexes.MomentM = maxV;
            maxV = 180d / maxV;

            // --------Fixing the curve coordinates
            Indexes.BMpts = new PointF[Indexes.BM.Count];
            int f = 0;
            foreach (var B in Indexes.BM)
            {
                Indexes.BMpts[f].X = (float)Indexes.BeamCoords[f];
                Indexes.BMpts[f].Y = (float)(B * maxV + (double)(My.MyProject.Forms.beamcreate.MEheight / 2f));
                f = f + 1;
            }
        }

        private static void FixBendingMoment_ShowCoords()
        {
            // ------Capturing the Bending Moment indexes for displaying
            var CDSindex = new List<int>();
            double S_sor = 0d;
            foreach (var S in Indexes.SF)
            {
                if (S > 0d & S_sor < 0d | S < 0d & S_sor > 0d)
                {
                    if (Math.Round(Indexes.BM[Indexes.SF.IndexOf(S)], 2) != 0d)
                    {
                        CDSindex.Add(Indexes.SF.IndexOf(S));
                    }
                }
                S_sor = S;
            }
            // ------Capturing Moment Location
            double L = 0d;
            foreach (var E in Indexes.mem)
            {
                foreach (var M in E.Mload)
                {
                    int I = Indexes.DX.IndexOf(L + (double)M.mdist);
                    CDSindex.Add(I - 1);
                    CDSindex.Add(I + 1);
                }
                L = L + (double)E.spanlength;
            }

            if (Conversions.ToInteger(Indexes.mem[0].DOF[1]) == 0)
            {
                CDSindex.Add(0);
            }
            if (Conversions.ToInteger(Indexes.mem[Indexes.mem.Count - 1].DOF[3]) == 0)
            {
                if (Math.Round(Indexes.BM[Indexes.SF.Count - 1], 2) != 0d)
                {
                    CDSindex.Add(Indexes.SF.Count - 1);
                }
                else if (Math.Round(Indexes.BM[Indexes.SF.Count - 2], 2) != 0d)
                {
                    CDSindex.Add(Indexes.SF.Count - 2);
                }
                else if (Math.Round(Indexes.BM[Indexes.SF.Count - 3], 2) != 0d)
                {
                    CDSindex.Add(Indexes.SF.Count - 3);
                }
                else if (Math.Round(Indexes.BM[Indexes.SF.Count - 4], 2) != 0d)
                {
                    CDSindex.Add(Indexes.SF.Count - 4);
                }
            }

            Indexes.BMMc.Clear();
            Indexes.BMmaxs = new PointF[CDSindex.Count];
            int f = 0;
            foreach (var CD in CDSindex)
            {
                Indexes.BMMc.Add(CD);
                Indexes.BMmaxs[f].X = Indexes.BMpts[CD].X;
                Indexes.BMmaxs[f].Y = Indexes.BMpts[CD].Y + (Indexes.BMpts[CD].Y > My.MyProject.Forms.beamcreate.MEheight / 2f ? 15 : -30);
                f = f + 1;
            }
        }
        #endregion

        #region Slope and deflection Calculator
        private static object GaussQuadrature_3Point(double b, double a, Member FRmem, double _Curx)
        {
            double delta = (b - a) / 2d;
            const double C1 = 0.555555556d;
            const double C2 = 0.888888889d;
            const double C3 = 0.555555556d;


            const double X1 = -0.774596669d;
            const double X2 = 0.0d;
            const double X3 = 0.774596669d;


            double FX1 = Total_BendingMoment_L(delta * X1 + (b + a) / 2d, FRmem);
            double FX2 = Total_BendingMoment_L(delta * X2 + (b + a) / 2d, FRmem);
            double FX3 = Total_BendingMoment_L(delta * X3 + (b + a) / 2d, FRmem);

            double Integration;
            Integration = delta * (C1 * FX1 + C2 * FX2 + C3 * FX3);
            return Integration;
        }

        private static void FixDisplacement_Slope_Deflection(Member FRmem)
        {
            Indexes.DE.Clear();
            Indexes.SL.Clear();

            // ------- Fixing the disintegration
            int J = 0;
            var _TDx = new List<double>[Indexes.mem.Count];
            double TotLength = 0d;
            foreach (var E in Indexes.mem)
            {
                J = 0;
                _TDx[Indexes.mem.IndexOf(E)] = new List<double>();
                foreach (var X in Indexes.DX)
                {
                    if (X >= TotLength & X < TotLength + (double)E.spanlength)
                    {
                        _TDx[Indexes.mem.IndexOf(E)].Add(X - TotLength);
                        J = J + 1;
                    }
                }
                TotLength = TotLength + (double)E.spanlength;
            }


            // --------First Integration
            var _FirstIntegration = new List<double>();
            double LowerLimit = 0d;
            double UpperLimit = 0d;
            double CummulativeBM = 0d;
            LowerLimit = Indexes.DX[0];
            UpperLimit = (Indexes.DX[0] + Indexes.DX[1]) / 2d;
            CummulativeBM = Conversions.ToDouble(GaussQuadrature_3Point(UpperLimit, LowerLimit, FRmem, Indexes.DX[0]));
            _FirstIntegration.Add(CummulativeBM);
            for (int i = 1, loopTo = Indexes.DX.Count - 2; i <= loopTo; i++)
            {
                LowerLimit = (Indexes.DX[i - 1] + Indexes.DX[i]) / 2d;
                UpperLimit = (Indexes.DX[i] + Indexes.DX[i + 1]) / 2d;
                CummulativeBM = Conversions.ToDouble(Operators.AddObject(CummulativeBM, GaussQuadrature_3Point(UpperLimit, LowerLimit, FRmem, Indexes.DX[i])));
                _FirstIntegration.Add(CummulativeBM);
            }
            LowerLimit = (Indexes.DX[Indexes.DX.Count - 2] + Indexes.DX[Indexes.DX.Count - 1]) / 2d;
            UpperLimit = Indexes.DX[Indexes.DX.Count - 1];
            CummulativeBM = Conversions.ToDouble(Operators.AddObject(CummulativeBM, GaussQuadrature_3Point(UpperLimit, LowerLimit, FRmem, Indexes.DX[Indexes.DX.Count - 1])));
            _FirstIntegration.Add(CummulativeBM);


            // --------Second Integration
            var _SecondIntegration = new List<double>();
            LowerLimit = 0d;
            UpperLimit = 0d;
            double CummulativeSL = 0d;
            LowerLimit = Indexes.DX[0];
            UpperLimit = (Indexes.DX[0] + Indexes.DX[1]) / 2d;
            CummulativeSL = (UpperLimit - LowerLimit) / 2d * _FirstIntegration[0];
            _SecondIntegration.Add(CummulativeSL);
            for (int i = 1, loopTo1 = Indexes.DX.Count - 2; i <= loopTo1; i++)
            {
                LowerLimit = (Indexes.DX[i - 1] + Indexes.DX[i]) / 2d;
                UpperLimit = (Indexes.DX[i] + Indexes.DX[i + 1]) / 2d;
                CummulativeSL = CummulativeSL + (UpperLimit - LowerLimit) / 2d * (_FirstIntegration[i - 1] + _FirstIntegration[i + 1]);
                _SecondIntegration.Add(CummulativeSL);
            }
            LowerLimit = (Indexes.DX[Indexes.DX.Count - 2] + Indexes.DX[Indexes.DX.Count - 1]) / 2d;
            UpperLimit = Indexes.DX[Indexes.DX.Count - 1];
            CummulativeSL = CummulativeSL + (UpperLimit - LowerLimit) / 2d * _FirstIntegration[Indexes.DX.Count - 1];
            _SecondIntegration.Add(CummulativeSL);


            // --------Finding Slope and deflection incorporating the integration constants
            J = 0;
            var c1 = default(double);
            TotLength = 0d;
            var c3 = default(double);
            foreach (var E in Indexes.mem)
            {
                c1 = c1 + E.DISP[1];
                c3 = c3 + E.DISP[0];
                foreach (var X in _TDx[Indexes.mem.IndexOf(E)])
                {
                    Indexes.SL.Add(_FirstIntegration[J] / (double)(E.Inertia * E.Emodulus) + c1);
                    Indexes.DE.Add(_SecondIntegration[J] / (double)(E.Inertia * E.Emodulus) + c1 * (TotLength + X) + c3);
                    J = J + 1;
                }
                c1 = c1 - E.DISP[3];
                c3 = c3 - E.DISP[2];
                TotLength = TotLength + (double)E.spanlength;
            }
        }

        private static double EI_member(double X)
        {
            double XI = 0d;
            var EI = default(double);
            foreach (var E in Indexes.mem)
            {
                if (X >= XI & X <= XI + (double)E.spanlength)
                {
                    EI = (double)(E.spanlength * E.Inertia);
                    break;
                }
                XI = XI + (double)E.spanlength;
            }
            return EI;
        }

        private static void FixSlopeDeflection_Coords()
        {
            FixSlope_CurveCoords();
            FixDeflection_CurveCoords();
            FixSlope_ShowCoords();
            FixDeflection_ShowCoords();
        }

        private static void FixSlope_CurveCoords()
        {
            // -------Finding maximum shear force for scaling the shear curve
            double maxV = 0d;
            foreach (var Slope in Indexes.SL)
            {
                if (Math.Abs(Slope) > maxV)
                {
                    maxV = Math.Abs(Slope);
                }
            }
            Indexes.SlopeM = maxV;
            maxV = 180d / maxV;

            // --------Fixing the curve coordinates
            Indexes.SLpts = new PointF[Indexes.SL.Count];
            int f = 0;

            foreach (var S in Indexes.SL)
            {
                Indexes.SLpts[f].X = (float)Indexes.BeamCoords[f];
                Indexes.SLpts[f].Y = (float)(S * maxV + (double)(My.MyProject.Forms.beamcreate.MEheight / 2f));
                f = f + 1;
            }
        }

        private static void FixSlope_ShowCoords()
        {
            // ------Capturing the Slope indexes for displaying
            var CDSindex = new List<int>();
            double S_sor = 0d;
            foreach (var B in Indexes.BM)
            {
                if (Math.Round(B, 3) > 0d & S_sor < 0d | Math.Round(B, 3) < 0d & S_sor > 0d)
                {
                    if (Math.Round(Indexes.SL[Indexes.BM.IndexOf(B)], 8) != 0d)
                    {
                        CDSindex.Add(Indexes.BM.IndexOf(B));
                    }
                }
                S_sor = Math.Round(B, 3);
            }
            // If mem(0).DOF(1) = 0 Then
            // CDSindex.Add(0)
            // End If
            // If mem(mem.Count - 1).DOF(3) = 0 Then
            // If Math.Round(BM(SF.Count - 1), 2) <> 0 Then
            // CDSindex.Add(SF.Count - 1)
            // ElseIf Math.Round(BM(SF.Count - 2), 2) <> 0 Then
            // CDSindex.Add(SF.Count - 2)
            // ElseIf Math.Round(BM(SF.Count - 3), 2) <> 0 Then
            // CDSindex.Add(SF.Count - 3)
            // ElseIf Math.Round(BM(SF.Count - 4), 2) <> 0 Then
            // CDSindex.Add(SF.Count - 4)
            // End If
            // End If

            Indexes.SLMc.Clear();
            Indexes.SLmaxs = new PointF[CDSindex.Count];
            int f = 0;
            foreach (var CD in CDSindex)
            {
                Indexes.SLMc.Add(CD);
                Indexes.SLmaxs[f].X = Indexes.SLpts[CD].X;
                Indexes.SLmaxs[f].Y = Indexes.SLpts[CD].Y + (Indexes.SLpts[CD].Y > My.MyProject.Forms.beamcreate.MEheight / 2f ? 15 : -30);
                f = f + 1;
            }
        }

        private static void FixDeflection_CurveCoords()
        {
            // -------Finding maximum shear force for scaling the shear curve
            double maxV = 0d;
            foreach (var Deflection in Indexes.DE)
            {
                if (Math.Abs(Deflection) > maxV)
                {
                    maxV = Math.Abs(Deflection);
                }
            }
            Indexes.DeflectionM = maxV;
            maxV = 180d / maxV;

            // --------Fixing the curve coordinates
            Indexes.DEpts = new PointF[Indexes.DE.Count];
            int f = 0;

            foreach (var D in Indexes.DE)
            {
                Indexes.DEpts[f].X = (float)Indexes.BeamCoords[f];
                Indexes.DEpts[f].Y = (float)(D * maxV + (double)(My.MyProject.Forms.beamcreate.MEheight / 2f));
                f = f + 1;
            }
        }

        private static void FixDeflection_ShowCoords()
        {
            // ------Capturing the Slope indexes for displaying
            var CDSindex = new List<int>();
            double S_sor = 0d;
            foreach (var S in Indexes.SL)
            {
                if (Math.Round(S, 8) > 0d & S_sor < 0d | Math.Round(S, 8) < 0d & S_sor > 0d)
                {
                    if (Math.Round(Indexes.DE[Indexes.SL.IndexOf(S)], 8) != 0d)
                    {
                        CDSindex.Add(Indexes.SL.IndexOf(S));
                    }
                }
                S_sor = Math.Round(S, 8);
            }
            // If mem(0).DOF(1) = 0 Then
            // CDSindex.Add(0)
            // End If
            // If mem(mem.Count - 1).DOF(3) = 0 Then
            // If Math.Round(BM(SF.Count - 1), 2) <> 0 Then
            // CDSindex.Add(SF.Count - 1)
            // ElseIf Math.Round(BM(SF.Count - 2), 2) <> 0 Then
            // CDSindex.Add(SF.Count - 2)
            // ElseIf Math.Round(BM(SF.Count - 3), 2) <> 0 Then
            // CDSindex.Add(SF.Count - 3)
            // ElseIf Math.Round(BM(SF.Count - 4), 2) <> 0 Then
            // CDSindex.Add(SF.Count - 4)
            // End If
            // End If

            Indexes.DEMc.Clear();
            Indexes.DEmaxs = new PointF[CDSindex.Count];
            int f = 0;
            foreach (var CD in CDSindex)
            {
                Indexes.DEMc.Add(CD);
                Indexes.DEmaxs[f].X = Indexes.DEpts[CD].X;
                Indexes.DEmaxs[f].Y = Indexes.DEpts[CD].Y + (Indexes.DEpts[CD].Y > My.MyProject.Forms.beamcreate.MEheight / 2f ? 15 : -30);
                f = f + 1;
            }
        }
        #endregion
        #endregion
    }
}