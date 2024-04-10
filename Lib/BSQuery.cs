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


        /// <summary>
        /// Найти строку из таблицы обычного бетона
        /// </summary>
        /// <param name="_BT">Класс бетона</param>
        /// <returns></returns>
        public static Beton BetonTableFind(string _BT)
        {
            Beton bt = new Beton();
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    string query = $"select * from Beton where BT = '{_BT}'";
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

    }
}
