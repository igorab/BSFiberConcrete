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

namespace BSFiberConcrete.Lib
{
    public class BSData
    {
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

        public static string  LoadConnectionString(string id = "Default")
        {
            string s = ConfigurationManager.ConnectionStrings[id].ConnectionString;
            return s;
        }

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


        public static List<Elements> LoadElements()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<Elements>("select Rfbt3n, Rfbn, Yb, Yft, Yb1, Yb2, Yb3, Yb5, B from FiberConcrete", new DynamicParameters());
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
