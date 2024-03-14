using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace BSFiberConcrete.Lib
{
    public class BSQuery : BSData
    {
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

    }
}
