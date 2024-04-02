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

    }
}
