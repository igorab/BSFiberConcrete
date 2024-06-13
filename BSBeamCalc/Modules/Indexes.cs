using System.Collections.Generic;
using System.Drawing;

namespace CBAnsDes
{
    static class Indexes
    {
        public static List<Member> mem = new List<Member>();
        public static int ends;
        public static double Zm = 1d;
        public static Point MidPt;
        public static int selline = -1;
        public static int Tselline = -1;
        public static int Lselline = -1;
        public static List<double> BeamCoords = new List<double>();
        public static List<double> DX = new List<double>();
        public static List<double> SF = new List<double>();
        public static List<double> BM = new List<double>();
        public static List<double> DE = new List<double>();
        public static List<double> SL = new List<double>();
        public static List<int> SFMc = new List<int>();
        public static List<int> BMMc = new List<int>();
        public static List<int> DEMc = new List<int>();
        public static List<int> SLMc = new List<int>();
        public static double ShearM, MomentM, DeflectionM, SlopeM;
        public static PointF[] SFpts = new PointF[SF.Count + 1], SFmaxs = new PointF[1];
        public static PointF[] BMpts = new PointF[SF.Count + 1], BMmaxs = new PointF[1];
        public static PointF[] DEpts = new PointF[SF.Count + 1], DEmaxs = new PointF[1];
        public static PointF[] SLpts = new PointF[SF.Count + 1], SLmaxs = new PointF[1];
    }
}