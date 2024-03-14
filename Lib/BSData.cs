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

        //public static readonly string connectionString = $"Data Source={DataPath("Fiber.db")};";
        public static readonly string connectionString = "Data Source=Fiber.db";

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
            string s = ConfigurationManager.ConnectionStrings[id].ConnectionString;
            return s;
        }

        /// <summary>
        /// Типы бетона
        /// </summary>
        /// <returns>Список</returns>
        public static List<string> LoadTypes()
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
        /// Типы бетона
        /// </summary>
        /// <returns>Список</returns>
        public static List<string> LoadBetonData()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<string>("select Name from Beton", new DynamicParameters());
                    return output.ToList();
                }
            }
            catch
            {
                return new List<string>();
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
            catch
            {
                return new List<Elements>();
            }
        }



    }
}
