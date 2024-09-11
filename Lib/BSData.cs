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
using System.Security.Cryptography;

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
        /// Данные формы
        /// </summary>
        /// <returns>Список</returns>
        public static FormParams LoadFormParams()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<FormParams>("select * from Params where ID = 1", new DynamicParameters());
                    if (output != null && output.Count() > 0)                    
                        return output.ToList()[0];                    
                    else
                        return new FormParams();
                }
            }
            catch
            {
                return new FormParams();
            }
        }

        /// <summary>
        ///  Сохранить введенные пользователем значения с формы
        /// </summary>
        /// <param name="_prms"></param>
        public static void UpdateFormParams(FormParams _prms)
        {

            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    cnn.Open();
                    using (var tr = cnn.BeginTransaction())
                    {                        
                        int cnt = cnn.Execute(@"update Params set Length = @Length,
                                                        LengthCoef = @LengthCoef, BetonType = @BetonType, Fib_i = @Fib_i, Bft3n = @Bft3n,
                                                        Bfn = @Bfn, Bftn = @Bftn, Eb = @Eb, Efbt = @Efbt, 
                                                        Rs = @Rsw, Area_s = @Area_s, Area1_s = @Area1_s, a_s = @a_s, a1_s = @a1_s    
                                                    where ID=@ID ", _prms , tr);
                        
                        tr.Commit();
                    }
                }
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
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
        /// Загрузка ТЯЖЕЛОГО типа бетона
        /// </summary>
        /// <returns>Список</returns>
        public static List<Beton> LoadHeavyBetonData()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<Beton>("select * from Beton where BetonType = 1", new DynamicParameters());
                    return output.ToList();
                }
            }
            catch
            {
                return new List<Beton>();
            }
        }




        /// <summary>
        /// Загрузка бетона
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

        /// <summary>
        /// Тип арматуры
        /// </summary>
        /// <returns></returns>
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
        /// Армирование
        /// </summary>
        /// <returns></returns>
        public static List<BSRod> LoadBSRod(BeamSection _SectionType)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<BSRod>(string.Format("select * from BSRod where SectionType = {0}", (int)_SectionType), 
                                                new DynamicParameters());
                    return output.ToList();
                }
            }
            catch
            {
                return new List<BSRod>();
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



        public static List<FiberConcreteClass> LoadFiberConcreteClass()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<FiberConcreteClass>("select * from FiberConcreteClass", new DynamicParameters());
                    return output.ToList();
                }
            }
            catch
            {
                return new List<FiberConcreteClass>();
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


        public static List<Fiber_K> LoadFiber_Kor()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<Fiber_K>("select * from Fiber_Kor", new DynamicParameters());
                    return output.ToList();
                }
            }
            catch
            {
                return new List<Fiber_K>();
            }
        }


        public static List<Fiber_K> LoadFiber_Kn()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<Fiber_K>("select * from Fiber_Kn", new DynamicParameters());
                    return output.ToList();
                }
            }
            catch
            {
                return new List<Fiber_K>();
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

        /// <summary>
        /// Местные нагрузки LocalStress = LocalCompression 
        /// </summary>
        /// <returns>Данные и расчет </returns>
        public static List<LocalStress> LoadLocalStress()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<LocalStress>("select * from LocalStress order by id", new DynamicParameters());
                    return output.ToList();
                }
            }
            catch
            {
                return new List<LocalStress>();
            }
        }

        /// <summary>
        /// Местные нагрузки
        /// </summary>
        /// <returns>Данные и расчет </returns>
        public static List<LocalStress> LoadLocalPunch()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<LocalStress>("select * from LocalPunch order by id", new DynamicParameters());
                    return output.ToList();
                }
            }
            catch
            {
                return new List<LocalStress>();
            }
        }


        /// <summary>
        /// Относительные деформации бетона в зависимости от влажности воздуха
        /// </summary>
        /// <returns>Список</returns>
        public static List<EpsilonFromAirHumidity> LoadBetonEpsilonFromAirHumidity()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<EpsilonFromAirHumidity>("select * from EpsilonFromAirHumidity", new DynamicParameters());
                    return output.ToList();
                }
            }
            catch
            {
                return new List<EpsilonFromAirHumidity>();
            }
        }

        public static void SaveRods(List<BSRod>  _ds, BeamSection  _BeamSection)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    cnn.Open();
                    using (var tr = cnn.BeginTransaction())
                    {
                        cnn.Execute(string.Format("delete from BSRod where SectionType = {0}", (int)_BeamSection), null , tr);

                        foreach (BSRod rod in _ds)
                        {
                            rod.SectionType = _BeamSection;
                            int cnt = cnn.Execute("insert into BSRod (CG_X, CG_Y, D, SectionType, Dnom) values (@CG_X, @CG_Y, @D, @SectionType, @Dnom)", rod, tr);
                            
                        }
                        tr.Commit();
                    }
                }
            }
            catch
            {
                throw ;
            }
        }




        /// <summary>
        /// Загружается геометрия сечений
        /// </summary>
        /// <returns>Список</returns>
        public static List<InitBeamSectionGeometry> LoadBeamSectionGeometry(BeamSection _SectionType)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<InitBeamSectionGeometry>("select * from InitBeamSection", new DynamicParameters());                    
                    //var outputTest = cnn.Query<BSRod>(string.Format("select * from InitBeamSection where SectionTypeNum = {0}", (int)_SectionType),
                    //                            new DynamicParameters());

                    return output.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        public static void UpdateBeamSectionGeometry(List<InitBeamSectionGeometry> beamSections)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    cnn.Open();
                    using (var tr = cnn.BeginTransaction())
                    {
                        foreach (InitBeamSectionGeometry bSection in beamSections)
                        {
                            cnn.Execute("update InitBeamSection set bw = @bw, hw = @hw, bf = @bf, hf = @hf, b1f = @b1f, h1f = @h1f, r1 = @r1, r2 = @r2 where SectionTypeNum = @SectionTypeNum", bSection, tr);
                            
                            //cnn.Execute($"delete from InitBeamSection where SectionTypeNum = {(int)bSection.SectionTypeNum}", null, tr);
                            //int cnt = cnn.Execute("insert into InitBeamSection (SectionTypeNum, SectionTypeStr, bw, hw, bf, hf, b1f, h1f, r1, r2) values (@SectionTypeNum, @SectionTypeStr, @bw, @hw, @bf, @hf, @b1f, @h1f, @r1, @r2)", bSection, tr);
                        }
                        tr.Commit();
                    }
                }
            }
            catch
            {
                throw;
            }
        }



        public static List<RebarDiameters> LoadRebarDiameters()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<RebarDiameters>("select * from RebarDiameters", new RebarDiameters());
                    return output.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Выборка арматуры
        /// </summary>
        /// <param name="_ClassRebar">Класс арматуры</param>
        /// <returns>Список - номинальные диаметры и площади сечения арматуры</returns>
        public static List<RebarDiameters> DiametersOfTypeRebar(string _ClassRebar)
        {
            List<RebarDiameters> rD = new List<RebarDiameters>();
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    string query = $"select * from RebarDiameters where TypeRebar = '{_ClassRebar}'";
                    var output = cnn.Query<RebarDiameters>(query, new RebarDiameters());
                    rD = output.ToList();
                    return rD;
                }
            }
            catch
            {
                return new List<RebarDiameters>();
            }
        }

        /// <summary>
        /// Класс фибры
        /// </summary>
        /// <returns></returns>
        public static List<FiberClass> LoadFiberClass()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<FiberClass>("select * from FiberClass", new DynamicParameters());
                    return output.ToList();
                }
            }
            catch
            {
                return new List<FiberClass>();
            }
        }

    }
}
