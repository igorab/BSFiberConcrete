using System;
using System.Data;
using System.Data.SQLite;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSFiberConcrete;

namespace BSFiberConcrete
{
    public class BSFiberLib
    {
        /// <summary>
        /// 1 группа предельных состояний
        /// </summary>
        public const int CG1 = 1;

        /// <summary>
        /// 2 группа пределеных состояний
        /// </summary>
        public const int CG2 = 2;

        public const string RebarClassDefault = "A400";

        public const double Fi = 0.9;

        /// <summary>
        /// Вычислить модуль упругости фибробетона на растяжение
        /// </summary>
        /// <param name="_Eb">Модуль упругости бетона</param>
        /// <param name="_Ef">Модуль упругости фибры</param>
        /// <param name="_mu_fv">Коэффициент фиброового армирования</param>
        /// <returns>Модуль упругости фибробетона на растяжение</returns>
        public static double E_fb(double _Eb, double _Ef, double _mu_fv)
        {
            double e_fb = _Eb * (1 - _mu_fv) + _Ef * _mu_fv;
            return e_fb;
        }

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

        // перенести в БД
        public static Dictionary<int, double> Fi_b_cr_75 = new Dictionary<int, double>
        {
            [10] = 2.8, [15] = 2.4, [20] = 2.0, [25] = 1.8, [30] = 1.6, [35] = 1.5, [40] = 1.4, [45] = 1.3, [50] = 1.2, [55] = 1.1, [60] = 1.0
        };

        // перенести в БД
        public static Dictionary<int, double> Fi_b_cr_45_75 = new Dictionary<int, double>
        {
            [10] = 3.9,
            [15] = 3.4,
            [20] = 2.8,
            [25] = 2.5,
            [30] = 2.3,
            [35] = 2.1,
            [40] = 1.9,
            [45] = 1.8,
            [50] = 1.6,
            [55] = 1.5,
            [60] = 1.4
        };

        // перенести в БД
        public static Dictionary<int, double> Fi_b_cr_40 = new Dictionary<int, double>
        {
            [10] = 5.6,
            [15] = 4.8,
            [20] = 4.0,
            [25] = 3.6,
            [30] = 3.2,
            [35] = 3.0,
            [40] = 2.8,
            [45] = 2.6,
            [50] = 2.4,
            [55] = 2.2,
            [60] = 2.0
        };

        //СП63 6.1.15
        public static double CalcFi_b_cr(int _airHumidityId, int _betonClassId)
        {
            Dictionary<int, double> DFi = new Dictionary<int, double>();

            if (_airHumidityId == 1)
            {
                DFi = Fi_b_cr_75;
            }
            else if (_airHumidityId == 2)
            {
                DFi = Fi_b_cr_45_75;
            }
            else if (_airHumidityId == 3)
            {
                DFi = Fi_b_cr_45_75;
            }
            else
            {
                return 0;
            }

            if (_betonClassId >= 10)
            {
                int bClassId = _betonClassId;
                if (_betonClassId > 60) bClassId = 60;

                if (DFi.TryGetValue(bClassId, out double fivalue))
                    return fivalue;
            }

            return 0;
        }

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

        /// <summary>
        /// Значения по-умолчанию для коэффициентов на форме
        /// </summary>
        public static StrengthFactors StrengthFactors()
        {            
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(Lib.BSData.LoadConnectionString()))
                {
                    string sql = "select * from StrengthFactors where id = 1";
                    IEnumerable<StrengthFactors> rec = cnn.Query<StrengthFactors>(sql, new DynamicParameters());

                    StrengthFactors elements = rec?.Count() > 0 ? rec.First() : new StrengthFactors();
                    return elements;
                }
            }
            catch
            {
                return new StrengthFactors { Yft = 1.3, Yb = 1.3, Yb1 = 0.9, Yb2 = 0.9, Yb3 = 0.9, Yb5 = 1 };
            }            
        }

    }
}
