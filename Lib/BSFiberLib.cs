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
        public static List<BSFiberBeton> BetonList => new List<BSFiberBeton>
        {
            new BSFiberBeton{Id = 2, Name = "B2.5", Type = 4, Rfbt3 = 0, Rfbn = 0 },
            new BSFiberBeton{Id = 3, Name = "B3.5", Type = 4, Rfbt3 = 0, Rfbn = 2.7 },
            new BSFiberBeton{Id = 5, Name = "B5", Type = 4, Rfbt3 = 0, Rfbn = 3.5 },
            new BSFiberBeton{Id = 7, Name = "B7.5", Type = 4, Rfbt3 = 0, Rfbn = 5.5 },
            new BSFiberBeton{Id = 10, Name = "B1i", Type = 2, Rfbt3 = 1.0, Rfbn = 7.5 },
            new BSFiberBeton{Id = 12, Name = "B1.25i",Type = 2, Rfbt3 = 1.25, Rfbn = 9.5},
            new BSFiberBeton{Id = 15, Name = "B1.5i", Type = 2, Rfbt3 = 1.5, Rfbn = 11 },
            new BSFiberBeton{Id = 20, Name = "B2i", Type = 2, Rfbt3 = 2.0, Rfbn = 15 },
            new BSFiberBeton{Id = 25, Name = "B2.5i", Type = 2, Rfbt3 = 2.5, Rfbn = 18.5 },
            new BSFiberBeton{Id = 30, Name = "B3i", Type = 2, Rfbt3 = 3.0, Rfbn = 22.0 },
            new BSFiberBeton{Id = 35, Name = "B3.5i", Type = 2, Rfbt3 = 3.5, Rfbn = 25.5 },
            new BSFiberBeton{Id = 40, Name = "B4i", Type = 2, Rfbt3 = 4.0, Rfbn = 29.0 },
            new BSFiberBeton{Id = 45, Name = "B4.5i", Type = 2, Rfbt3 = 4.5, Rfbn = 32.0 },
            new BSFiberBeton{Id = 50, Name = "B5i", Type = 2, Rfbt3 = 5.0, Rfbn = 36.0 },
            new BSFiberBeton{Id = 55, Name = "B5.5i", Type = 2 , Rfbt3 = 5.5, Rfbn = 39.5},
            new BSFiberBeton{Id = 60, Name = "B6i", Type = 2, Rfbt3 = 6.0, Rfbn = 43 }
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
                    return new Elements { Rfbt3n = 30.58, Yb = 1.3, Rfbn = 224.0, Yft = 1.3, Yb1 = 0.9, Yb2 = 0.9, Yb3 = 0.9, Yb5 = 1, iB = 30 };
                }               
            }
        }
    }
}
