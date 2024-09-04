using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows.Forms;

namespace BSFiberConcrete.Lib
{
    public class BSQuery : BSData
    {
        /// <summary>
        /// Поиск по типу бетона
        /// </summary>
        /// <param name="_Id"></param>
        /// <returns>Тяжелый, мелкозернистый, легкий </returns>
        public static BetonType BetonTypeFind(int _Id)
        {
            BetonType bt = new BetonType();
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    string query = $"select * from BetonType where Id = {_Id}";
                    var output = cnn.Query<BetonType>(query, new DynamicParameters());
                    if (output.Count() > 0)
                        bt = output.ToList()[0];
                }
            }
            catch { }
                                       
            return bt;
        }

        public static List<BetonType> LoadBetonType()
        {
            List<BetonType> bt = new List<BetonType>();
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    string query = "select * from BetonType";
                    var output = cnn.Query<BetonType>(query, new DynamicParameters());
                    bt = output.ToList();
                }
            }
            catch { }
            return bt;
        }


        /// <summary>
        /// Найти строку из таблицы ТЯЖЕЛОГО бетона
        /// </summary>
        /// <param name="_BT">Класс бетона</param>
        /// <returns></returns>
        public static Beton HeavyBetonTableFind(string _BT)
        {
            Beton bt = new Beton();
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    string query = $"select * from Beton where BetonType = 1 AND BT = '{_BT}'";
                    var output = cnn.Query<Beton>(query, new DynamicParameters());
                    if (output.Count() > 0)
                        bt = output.ToList()[0];
                }
            }
            catch { }

            return bt;
        }


        public static Rebar RebarFind(string _ID)
        {
            Rebar rb = new Rebar();
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    string query = $"select * from Rebar where ID = '{_ID}'";
                    var output = cnn.Query<Rebar>(query, new DynamicParameters());
                    if (output.Count() > 0)
                        rb = output.ToList()[0];
                }
            }
            catch { }

            return rb;
        }


        public static RFiber RFiberFind(int _ID)
        {
            RFiber rb = new RFiber();
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    string query = $"select * from RFiber where ID = {_ID}";
                    var output = cnn.Query<RFiber>(query, new DynamicParameters());
                    if (output.Count() > 0)
                        rb = output.ToList()[0];
                }
            }
            catch { }

            return rb;
        }


        public static List<RFiber> RFiberLoad()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    string query = $"select * from RFiber";
                    var output = cnn.Query<RFiber>(query, new DynamicParameters());
                    return output.ToList();
                }
            }
            catch
            {
                return new List<RFiber>();
            }
        }

        public static List<FiberType> FiberTypeLoad()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    string query = $"select * from FiberType";
                    var output = cnn.Query<FiberType>(query, new DynamicParameters());
                    return output.ToList();
                }
            }
            catch
            {
                return new List<FiberType>();
            }
        }

        public static List<FiberKind> FiberKindLoad()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    string query = $"select * from FiberKind";
                    var output = cnn.Query<FiberKind>(query, new DynamicParameters());
                    return output.ToList();
                }
            }
            catch
            {
                return new List<FiberKind>();
            }
        }


        public static List<FiberGeometry> FiberGeometryLoad()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    string query = $"select * from FiberGeometry";
                    var output = cnn.Query<FiberGeometry>(query, new DynamicParameters());
                    return output.ToList();
                }
            }
            catch
            {
                return new List<FiberGeometry>();
            }
        }


        public static List<FiberLength> FiberLengthLoad()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    string query = $"select * from FiberLength";
                    var output = cnn.Query<FiberLength>(query, new DynamicParameters());
                    return output.ToList();
                }
            }
            catch
            {
                return new List<FiberLength>();
            }
        }


        public static FaF RFaF_Find(int _Num)
        {
            FaF rb = new FaF();
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    string query = $"select * from RChartFaF where Num = {_Num}";
                    var output = cnn.Query<FaF>(query, new DynamicParameters());
                    if (output.Count() > 0)
                        rb = output.ToList()[0];
                }
            }
            catch { }

            return rb;
        }

        public static void SaveFaF(List<FaF> _ds)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    cnn.Open();
                    using (var tr = cnn.BeginTransaction())
                    {
                        foreach (FaF fa in _ds)
                        {
                            if (BSQuery.RFaF_Find(fa.Num).Num != 0)
                            {
                                int cnt = cnn.Execute("update RChartFaF set aF = @aF, F = @F where Num = @Num ", fa, tr);
                            }
                            else
                            {
                                int cnt = cnn.Execute("insert into RChartFaF (Num, aF, F) values(@Num, @aF, @F)", fa, tr);
                            }
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


        public static FibLab FibLabFind(string _Id)
        {
            FibLab rb = new FibLab();
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    string query = $"select * from RFibLab where Id = '{_Id}'";
                    var output = cnn.Query<FibLab>(query, new DynamicParameters());
                    if (output.Count() > 0)
                        rb = output.ToList()[0];
                }
            }
            catch { }

            return rb;
        }

        public static void SaveFibLab(List<FibLab> _ds)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    cnn.Open();
                    using (var tr = cnn.BeginTransaction())
                    {
                        foreach (FibLab fa in _ds)
                        {
                            if ( string.IsNullOrEmpty(FibLabFind(fa.Id).Id) )
                            {
                                int cnt = cnn.Execute("insert into RFibLab (Id, Fel, F05, F25, L, B) values(@Id, @Fel, @F05, @F25, @L, @B)", fa, tr);                                
                            }
                            else
                            {
                                int cnt = cnn.Execute("update RFibLab set Fel=@Fel, F05=@F05, F25=@F25, L=@L, B=@B where Id=@Id ", fa, tr);
                            }
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


        public static Deflection_f_aF FibLabDeflectionFind(string _Id)
        {
            Deflection_f_aF rb = new Deflection_f_aF();
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    string query = $"select * from RDeflection where Id = '{_Id}'";
                    var output = cnn.Query<Deflection_f_aF>(query, new DynamicParameters());
                    if (output.Count() > 0)
                        rb = output.ToList()[0];
                }
            }
            catch { }

            return rb;
        }


        public static void SaveFibLabDeflection(List<Deflection_f_aF> _ds)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    cnn.Open();
                    using (var tr = cnn.BeginTransaction())
                    {
                        foreach (Deflection_f_aF fa in _ds)
                        {
                            if (string.IsNullOrEmpty(FibLabDeflectionFind(fa.Id).Id))
                            {
                                int cnt = cnn.Execute("insert into RDeflection (Id, Num, f, aF) values(@Id, @Num, @f, @aF)", fa, tr);
                            }
                            else
                            {
                                int cnt = cnn.Execute("update RDeflection set Id=@Id, Num=@Num, f=@f, aF=@aF where Id=@Id and Num =@Num ", fa, tr);
                            }
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

        public static List<LocalStress> UpdateLocalPunch(Dictionary<string, double> _ds)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    cnn.Open();
                    using (var tr = cnn.BeginTransaction())
                    {
                        foreach (KeyValuePair<string, double> item in _ds)
                        {                            
                            int cnt = cnn.Execute("update LocalPunch set Value=@Value where VarName=@Key ", item, tr);                            
                        }
                        tr.Commit();
                    }
                }
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }

            return BSData.LoadLocalPunch();
        }

        public static List<LocalStress> UpdateLocalCompression(Dictionary<string, double> _ds)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    cnn.Open();
                    using (var tr = cnn.BeginTransaction())
                    {
                        foreach (KeyValuePair<string, double> item in _ds)
                        {
                            int cnt = cnn.Execute("update LocalStress set Value=@Value where VarName=@Key ", item, tr);
                        }
                        tr.Commit();
                    }
                }
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }

            return BSData.LoadLocalStress();
        }


    }
}
