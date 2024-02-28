using System.Data.SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
    }
}
