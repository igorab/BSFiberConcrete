using System.Data.SQLite;
using System.Data;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Dapper;

namespace BSFiberConcrete.Lib
{
    public class BSData
    {
        public static string ResourcePath(string _file) => Path.Combine(Environment.CurrentDirectory, "Resources", _file);  

        public static string DataPath(string _file)  => Path.Combine(Environment.CurrentDirectory, "Data", _file); 
        
        public static readonly string connectionString = "Data Source =.\\Data\\Fiber.db; Version = 3;";

        public static bool Connect()
        {
            bool ok = false;

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                ok = true;
            }         
            
            return ok;
        }

                                                public static string  LoadConnectionString(string id = "Default")
        {
            string s = ConfigurationManager.ConnectionStrings[id]?.ConnectionString;
            if (string.IsNullOrEmpty(s))
                s = connectionString;
            return s;
        }

                                        public static FormParams LoadFormParams()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<FormParams>("select * from Params where ID = 1", new DynamicParameters());
                    if (output != null && output.Count() > 0)                    
                        return output.ToList()[0];                    
                    else
                        return new FormParams();
                }
            }
            catch
            {
                return new FormParams();
            }
        }

                                        public static void UpdateFormParams(FormParams _prms)
        {

            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    cnn.Open();
                    using (var tr = cnn.BeginTransaction())
                    {                        
                        int cnt = cnn.Execute(@"update Params set Length = @Length,
                                                        LengthCoef = @LengthCoef, BetonType = @BetonType, Fib_i = @Fib_i, Bft3n = @Bft3n,
                                                        Bfn = @Bfn, Bftn = @Bftn, Eb = @Eb, Efbt = @Efbt, 
                                                        Rs = @Rsw, Area_s = @Area_s, Area1_s = @Area1_s, a_s = @a_s, a1_s = @a1_s    
                                                    where ID=@ID ", _prms , tr);
                        
                        tr.Commit();
                    }
                }
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
        }


                                        public static NDMSetup LoadNDMSetup(int Id = 1)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<NDMSetup>($"select * from NDMSetup where Id = {Id}", new DynamicParameters());
                    return output.ToList()[0];
                }
            }
            catch
            {
                return new NDMSetup() {Id = 0, Iters = 1000, M = 20, N = 20, MinAngle = 40, MaxArea = 10, BetonTypeId = 0 };
            }
        }

                                public static void SaveNDMSetup(NDMSetup _ndmSetup)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    cnn.Open();
                    using (var tr = cnn.BeginTransaction())
                    {
                        cnn.Execute($"update NDMSetup set " +
                            " Id = @Id, Iters = @Iters, N = @N, M = @M," +
                            " BetonTypeId = @BetonTypeId, MinAngle = @MinAngle, MaxArea = @MaxArea, " +
                            " UseRebar = @UseRebar, RebarType = @RebarType " +
                            " where Id = @Id",
                            _ndmSetup, tr);

                        tr.Commit();
                    }
                }
            }
            catch
            {
                throw new Exception("Не удалось сохранить значения в БД");
            }
        }

                                        public static List<string> LoadBetonTypeName()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<string>("select Name from BetonType", new DynamicParameters());
                    return output.ToList();
                }
            }
            catch
            {
                return new List<string>();
            }
        }

                                        public static List<FiberBft> LoadFiberBft()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<FiberBft>("select * from FiberBft", new DynamicParameters());
                    return output.ToList();
                }
            }
            catch
            {
                return new List<FiberBft>();
            }
        }

                                        public static List<Beton> LoadBetonData(int _betonTypeId)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<Beton>($"select * from Beton where BetonType = {_betonTypeId}", new DynamicParameters());
                    return output.ToList();
                }
            }
            catch
            {
                return new List<Beton>();
            }
        }

                                        public static List<Beton> LoadBetonData()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<Beton>("select * from Beton", new DynamicParameters());
                    return output.ToList();
                }
            }
            catch
            {
                return new List<Beton>();
            }
        }

                                        public static List<Coefficients> LoadCoeffs()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<Coefficients>("select * from Coefficients", new DynamicParameters());
                    return output.ToList();
                }
            }
            catch
            {
                return new List<Coefficients>();
            }
        }

                                        public static List<Rebar> LoadRebar()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<Rebar>("select * from Rebar", new DynamicParameters());
                    return output.ToList();
                }
            }
            catch
            {
                return new List<Rebar>();
            }
        }

                                        public static List<BSRod> LoadBSRod(BeamSection _SectionType)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<BSRod>(string.Format("select * from BSRod where SectionType = {0}", (int)_SectionType), 
                                                new DynamicParameters());
                    return output.ToList();
                }
            }
            catch
            {
                return new List<BSRod>();
            }
        }

                                        public static List<NdmSection> LoadNdmSection(string _SectionNum)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<NdmSection>(string.Format("select * from NdmSection where Num = '{0}'", _SectionNum),
                                                new DynamicParameters());
                    return output.ToList();
                }
            }
            catch
            {
                return new List<NdmSection>();
            }
        }

                                        public static List<Efforts> LoadEfforts()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                                        var output = cnn.Query<Efforts>("select * from Efforts", new DynamicParameters());
                    return output.ToList();
                }
            }
            catch
            {
                return new List<Efforts>();
            }
        }

                public static void SaveEfforts(Efforts _efforts)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    cnn.Open();
                    using (var tr = cnn.BeginTransaction())
                    {                        
                        int cnt = cnn.Execute("update Efforts set Mx = @Mx, My = @My, N = @N, Qx = @Qx, Qy = @Qy where Id = @Id ", _efforts, tr);
                        tr.Commit();
                    }                    
                }
            }
            catch(Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
        }

                public static void SaveEfforts(List<Efforts> _efforts, bool _clear = true)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    cnn.Open();
                    using (var tr = cnn.BeginTransaction())
                    {
                        if (_clear)
                        {
                            cnn.Execute("DELETE FROM Efforts");
                        }    

                        for (int i = 0; _efforts.Count > i; i++)
                        {
                            Efforts tmpEfforts = _efforts[i];
                            
                            int cnt = cnn.Execute($"insert into Efforts (Id, Mx, Mx, My, N, Qx, Qy) " +
                                $"values (@Id, @Mx, @Mx, @My, @N, @Qx, @Qy)", tmpEfforts, tr);
                        }
                        tr.Commit();
                    }
                }
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
        }


                public static void ClearEfforts()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    cnn.Open();
                    using (var tr = cnn.BeginTransaction())
                    {
                        int cnt = cnn.Execute("DELETE FROM Efforts");
                        tr.Commit();
                    }
                }
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
        }


                                        public static List<Elements> LoadFiberConcreteTable(string _iB = "")
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    string query;
                    if (_iB == "")
                        query = "select * from FiberConcrete";
                    else
                        query = string.Format("select * from FiberConcrete where i_B = '{0}'", _iB);

                    IEnumerable<Elements> output = cnn.Query<Elements>(query, new DynamicParameters());
                    return output.ToList();
                }
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
                return new List<Elements>();
            }
        }

        public static List<FiberConcreteClass> LoadFiberConcreteClass()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<FiberConcreteClass>("select * from FiberConcreteClass", new DynamicParameters());
                    return output.ToList();
                }
            }
            catch
            {
                return new List<FiberConcreteClass>();
            }
        }

        public static List<RFibKor> LoadRFibKn()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<RFibKor>("select * from RFibKn", new DynamicParameters());
                    return output.ToList();
                }
            }
            catch
            {
                return new List<RFibKor>();
            }
        }

        public static List<Fiber_K> LoadFiber_Kor()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<Fiber_K>("select * from Fiber_Kor", new DynamicParameters());
                    return output.ToList();
                }
            }
            catch
            {
                return new List<Fiber_K>();
            }
        }


        public static List<Fiber_K> LoadFiber_Kn()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<Fiber_K>("select * from Fiber_Kn", new DynamicParameters());
                    return output.ToList();
                }
            }
            catch
            {
                return new List<Fiber_K>();
            }
        }

        public static List<RFibKor> LoadRFibKor()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<RFibKor>("select * from RFibKor", new DynamicParameters());
                    return output.ToList();
                }
            }
            catch
            {
                return new List<RFibKor>();
            }
        }

        public static List<FaF> LoadRChartFaF()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<FaF>("select * from RChartFaF", new DynamicParameters());
                    return output.ToList();
                }
            }
            catch
            {
                return new List<FaF>();
            }
        }

        public static List<FibLab> LoadRFibLab()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<FibLab>("select * from RFibLab", new DynamicParameters());
                    return output.ToList();
                }
            }
            catch
            {
                return new List<FibLab>();
            }
        }

        public static List<Deflection_f_aF> LoadRDeflection(string _Id)
        {
            string query;
            if (string.IsNullOrEmpty(_Id))
            {
                query = string.Format("select * from RDeflection");
            }
            else
            {
                query = string.Format("select * from RDeflection where id == '{0}'", _Id);
            }

            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {                    
                    var output = cnn.Query<Deflection_f_aF>(query, new DynamicParameters());
                    return output.ToList();
                }
            }
            catch
            {
                return new List<Deflection_f_aF>();
            }
        }

        public static List<string> LoadBeamDeflection()
        {
            string query;
            
            query = string.Format("select Id from RDeflection group by Id ");
            
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<string>(query, new DynamicParameters());
                    return output.ToList();
                }
            }
            catch
            {
                return new List<string>();
            }
        }

                                        public static List<LocalStress> LoadLocalStress()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<LocalStress>("select * from LocalStress order by id", new DynamicParameters());
                    return output.ToList();
                }
            }
            catch
            {
                return new List<LocalStress>();
            }
        }

                                        public static List<LocalStress> LoadLocalPunch()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<LocalStress>("select * from LocalPunch order by id", new DynamicParameters());
                    return output.ToList();
                }
            }
            catch
            {
                return new List<LocalStress>();
            }
        }

                                        public static List<EpsilonFromAirHumidity> LoadBetonEpsilonFromAirHumidity()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<EpsilonFromAirHumidity>("select * from EpsilonFromAirHumidity", new DynamicParameters());
                    return output.ToList();
                }
            }
            catch
            {
                return new List<EpsilonFromAirHumidity>();
            }
        }

                                                public static void SaveRods(List<BSRod>  _ds, BeamSection  _BeamSection)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    cnn.Open();
                    using (var tr = cnn.BeginTransaction())
                    {
                        cnn.Execute(string.Format("delete from BSRod where SectionType = {0}", (int)_BeamSection), null , tr);

                        foreach (BSRod rod in _ds)
                        {
                            rod.SectionType = _BeamSection;
                            int cnt = cnn.Execute("insert into BSRod (CG_X, CG_Y, D, SectionType, Dnom) values (@CG_X, @CG_Y, @D, @SectionType, @Dnom)", rod, tr);
                            
                        }
                        tr.Commit();
                    }
                }
            }
            catch
            {
                throw ;
            }
        }

                                                public static void SaveSection(List<NdmSection> _ds, string _SectionNum)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    cnn.Open();
                    using (var tr = cnn.BeginTransaction())
                    {
                        cnn.Execute(string.Format("delete from NdmSection where Num = '{0}'", _SectionNum), null, tr);

                        foreach (var sec in _ds)
                        {                            
                            int cnt = cnn.Execute("insert into NdmSection (X, Y, N, Num) values (@X, @Y, @N, @Num)", sec, tr);

                        }
                        tr.Commit();
                    }
                }
            }
            catch
            {
                throw;
            }
        }

                                        public static List<InitBeamSectionGeometry> LoadBeamSectionGeometry(BeamSection _SectionType)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<InitBeamSectionGeometry>("select * from InitBeamSection", new DynamicParameters());                                        

                    return output.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        public static void UpdateBeamSectionGeometry(List<InitBeamSectionGeometry> beamSections)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    cnn.Open();
                    using (var tr = cnn.BeginTransaction())
                    {
                        foreach (InitBeamSectionGeometry bSection in beamSections)
                        {
                            cnn.Execute($"update InitBeamSection set bw = @bw, hw = @hw, bf = @bf, hf = @hf, b1f = @b1f, h1f = @h1f, r1 = @r1, r2 = @r2" +
                                $" where SectionTypeNum = @SectionTypeNum", bSection, tr);
                                                        
                        }
                        tr.Commit();
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public static List<RebarDiameters> LoadRebarDiameters()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<RebarDiameters>("select * from RebarDiameters", new RebarDiameters());
                    return output.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

                                                public static List<RebarDiameters> DiametersOfTypeRebar(string _ClassRebar)
        {
            List<RebarDiameters> rD = new List<RebarDiameters>();
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    string query = $"select * from RebarDiameters where TypeRebar = '{_ClassRebar}'";
                    var output = cnn.Query<RebarDiameters>(query, new RebarDiameters());
                    rD = output.ToList();
                    return rD;
                }
            }
            catch
            {
                return new List<RebarDiameters>();
            }
        }

                                        public static List<FiberClass> LoadFiberClass()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<FiberClass>("select * from FiberClass", new DynamicParameters());
                    return output.ToList();
                }
            }
            catch
            {
                return new List<FiberClass>();
            }
        }


                                public static NdmCrc LoadNdmCrc()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<NdmCrc>("select * from NDMCrc where id = 1", new DynamicParameters());

                    if (output != null && output.Count() > 0)
                        return output.ToList()[0];
                    else
                        return new NdmCrc() {Id =1, fi1 = 1.4, fi2 = 0.5, fi3 = 0.4, mu_fv = 0.015, kf = 1};                  
                }
            }
            catch
            {
                return new NdmCrc();
            }
        }

                                public static void SaveNdmCrc(NdmCrc _NdmCrc)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    cnn.Open();
                    using (var tr = cnn.BeginTransaction())
                    {                                               
                        cnn.Execute("update NDMCrc set fi1 = @fi1, fi2 = @fi2, fi3 = @fi3, mu_fv = @mu_fv, psi_s = @psi_s where Id = @Id", 
                            _NdmCrc, tr);                                                    
                        tr.Commit();
                    }
                }
            }
            catch
            {
                throw new Exception ("Не удалось сохранить значения в БД");
            }
        }

                                public static void SaveStrengthFactors(StrengthFactors _sFactors)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    cnn.Open();
                    using (var tr = cnn.BeginTransaction())
                    {
                        cnn.Execute("update StrengthFactors set " +
                            " Yft = @Yft, Yb = @Yb, Yb1 = @Yb1, Yb2 = @Yb2, Yb3 = @Yb3, Yb5 = @Yb5 " +
                            " where Id = @Id",
                            _sFactors, tr);
                        tr.Commit();
                    }
                }
            }
            catch
            {
                throw new Exception("Не удалось сохранить значения в БД");
            }
        }
    }
}
