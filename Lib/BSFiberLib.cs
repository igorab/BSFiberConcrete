using System;
using System.Data;
using System.Data.SQLite;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSFiberConcrete;
using System.Data;

namespace BSFiberConcrete
{
    internal class BSFiberLib
    {
        /// <summary>
        /// Прочности фибробетона на растяжение
        /// </summary>
        public static List<BSFiberBeton> BetonList => new List<BSFiberBeton>
        {            
            new BSFiberBeton{Id = 100, Name = "B1i", Type = 2, Rfbt3 = 1.0, Rfbn = 7.5 },
            new BSFiberBeton{Id = 150, Name = "B1,5i",Type = 2, Rfbt3 = 1.5, Rfbn = 9.5},            
            new BSFiberBeton{Id = 200, Name = "B2i", Type = 2, Rfbt3 = 2.0, Rfbn = 15 },
            new BSFiberBeton{Id = 250, Name = "B2,5i", Type = 2, Rfbt3 = 2.5, Rfbn = 18.5 },
            new BSFiberBeton{Id = 300, Name = "B3i", Type = 2, Rfbt3 = 3.0, Rfbn = 22.0 },
            new BSFiberBeton{Id = 350, Name = "B3,5i", Type = 2, Rfbt3 = 3.5, Rfbn = 25.58 },
            new BSFiberBeton{Id = 400, Name = "B4i", Type = 2, Rfbt3 = 4.0, Rfbn = 29.0 },
            new BSFiberBeton{Id = 450, Name = "B4,5i", Type = 2, Rfbt3 = 4.5, Rfbn = 32.0 },
            new BSFiberBeton{Id = 500, Name = "B5i", Type = 2, Rfbt3 = 5.0, Rfbn = 36.0 },
            new BSFiberBeton{Id = 550, Name = "B5,5i", Type = 2 , Rfbt3 = 5.5, Rfbn = 39.5},
            new BSFiberBeton{Id = 600, Name = "B6i", Type = 2, Rfbt3 = 6.0, Rfbn = 43 }
        };

        /// <summary>
        /// Значения по-умолчанию для коэффициентов на форме
        /// </summary>
        public static Elements PhysElements
        {
            get
            {
                try
                {
                    using (IDbConnection cnn = new SQLiteConnection(Lib.BSData.LoadConnectionString()))
                    {
                        IEnumerable<Elements> output = cnn.Query<Elements>("select * from FiberConcrete where id = 1", new DynamicParameters());
                        Elements elements = output?.Count() > 0 ? output.First() : new Elements();
                        return elements;                        
                    }
                }
                catch
                {
                    return new Elements { Rfbt3n = 30.58, Yb = 1.3, Rfbn = 224.0, Yft = 1.3, Yb1 = 0.9, Yb2 = 0.9, Yb3 = 0.9, Yb5 = 1, i_B = "a" };
                }               
            }
        }
    }
}
