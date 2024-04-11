using System.Data.SQLite;
using System.Data;
using System;
using System.Configuration;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dapper;
using System.Linq.Expressions;
using MathNet.Numerics;
using System.Reflection.Emit;

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

        /// <summary>
        /// Подключение к БД
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Строка подключения</returns>
        public static string  LoadConnectionString(string id = "Default")
        {
            string s = ConfigurationManager.ConnectionStrings[id]?.ConnectionString;
            if (string.IsNullOrEmpty(s))
                s = connectionString;
            return s;
        }

        /// <summary>
        /// Наименования типов бетона
        /// </summary>
        /// <returns>Список</returns>
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

        /// <summary>
        /// Классы бетона по сопротивлению на растяжение Bft 
        /// </summary>
        /// <returns>Список</returns>
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

        /// <summary>
        /// Типы бетона
        /// </summary>
        /// <returns>Список</returns>
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

        /// <summary>
        /// Коэффициенты
        /// </summary>
        /// <returns>Список</returns>
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

        /// <summary>
        /// Усилия 
        /// </summary>
        /// <returns>Список</returns>
        public static List<Efforts> LoadEfforts()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<Efforts>("select * from Efforts where id = 1", new DynamicParameters());
                    return output.ToList();
                }
            }
            catch
            {
                return new List<Efforts>();
            }
        }

        // сохранить данные в бд по усилиям
        public static void SaveEfforts(Efforts _efforts)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    cnn.Open();
                    using (var tr = cnn.BeginTransaction())
                    {                        
                        int cnt = cnn.Execute("update Efforts set Mx = @Mx, My = @My, N = @N, Q = @Q, Ml = @Ml, eN = @eN where Id = @Id ", _efforts, tr);
                        tr.Commit();
                    }                    
                }
            }
            catch(Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
        }


        /// <summary>
        /// Данные по фибробетону из БД
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Коэффициенты
        /// </summary>
        /// <returns>Список</returns>
        public static List<Beton> LoadBetonTable()
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






    }
}
