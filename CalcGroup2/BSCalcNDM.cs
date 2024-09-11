using BSFiberConcrete.BSRFib;
using CBAnsDes.My;
using MathNet.Numerics;
using MathNet.Numerics.Statistics;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

// Рачеты по второй группе предельных состояний
namespace BSFiberConcrete.CalcGroup2
{       
    public partial class  BSCalcNDM 
    {
        /// <summary>
        /// группа предельных состояний
        /// </summary>
        private readonly int GroupLSD;

        /// <summary>   
        /// рассчитывать ширину раскрыттия трещины
        /// </summary>
        public double Eps_s_crc { get; set; }
        
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="_groupLSD"> группа предельных состояний</param>
        public BSCalcNDM(int _groupLSD)
        {
            GroupLSD = _groupLSD;
        }

        public BSCalcNDM(int _groupLSD, BeamSection  _BeamSection, int _BetonTypeId)
        {
            GroupLSD = _groupLSD;
            BeamSection = _BeamSection;
            BetonTypeId = _BetonTypeId;
        }

        /// <summary>
        /// усилия переводятся :  кг, кг*см -> кН, кН*см
        /// </summary>
        /// <param name="_Mx">сейчас в расчете по ндм - ось  Z</param>
        /// <param name="_My"></param>
        /// <param name="_N"></param>
        public void SetMN(double _Mx, double _My, double _N)
        {
            N0 = BSHelper.Kgs2kN(_N);
            My0 = BSHelper.kgssm2kNsm(_My);
            Mz0 = BSHelper.kgssm2kNsm(_Mx);
        }

        public void SetE_S_Crc(List<double> _es)
        {
            Eps_s_crc = _es.Maximum();
        }

        // параметры для расчета
        public void SetDictParams(Dictionary<string, double> _D)
        {            
            // Enforces
            N0 = BSHelper.Kgs2kN(_D["N"]);
            My0 = BSHelper.kgssm2kNsm(_D["My"]) ;
            Mz0 = BSHelper.kgssm2kNsm(_D["Mz"]) ;

            // size
            b = _D["b"];
            h = _D["h"];

            bf = _D["bf"];
            hf = _D["hf"];
            bw = _D["bw"];
            hw = _D["hw"]; 
            b1f = _D["b1f"]; 
            h1f = _D["h1f"];

            r1 = _D["r1"];
            R2 = _D["R2"];
            //
            //Mesh
            ny = (int)_D["ny"];
            nz = (int)_D["nz"];

            // beton
            Eb0 = BSHelper.Kgssm2ToKNsm2(_D["Eb0"]);
            // fiber
            Ebt = BSHelper.Kgssm2ToKNsm2(_D["Ebt"]);

            if (GroupLSD == 2) 
            {
                // сжатие
                Rbc = BSHelper.Kgssm2ToKNsm2(_D["Rbcn"]);
                // растяжение
                Rfbt = BSHelper.Kgssm2ToKNsm2(_D["Rbtn"]);
                Rfbt2 = BSHelper.Kgssm2ToKNsm2(_D["Rbt2n"]);
                Rfbt3 = BSHelper.Kgssm2ToKNsm2(_D["Rbt3n"]);
            }
            else
            {
                // сжатие
                Rbc = BSHelper.Kgssm2ToKNsm2(_D["Rbc"]);
                // растяжение
                Rfbt = BSHelper.Kgssm2ToKNsm2(_D["Rbt"]);
                Rfbt2 = BSHelper.Kgssm2ToKNsm2(_D["Rbt2"]);
                Rfbt3 = BSHelper.Kgssm2ToKNsm2(_D["Rbt3"]);
            }

            // предельные деформации, сжатие
            ebc0 = _D["ebc0"];
            ebc2 = _D["ebc2"];

            // растяжение
            efbt0 = _D["ebt0"];
            efbt2 = _D["ebt2"];
            efbt3 = _D["ebt3"];

            // steel / rebar
            Es0 = BSHelper.Kgssm2ToKNsm2(_D["Es0"]);
            if (GroupLSD == 2)
            {
                Rsc = BSHelper.Kgssm2ToKNsm2(_D["Rscn"]);
                Rst = BSHelper.Kgssm2ToKNsm2(_D["Rstn"]);
            }
            else
            {
                Rsc = BSHelper.Kgssm2ToKNsm2(_D["Rsc"]);
                Rst = BSHelper.Kgssm2ToKNsm2(_D["Rst"]);
            }
            esc2 = _D["esc2"];
            est2 = _D["est2"];

            e_fbt_ult = _D["ebt_ult"]; 
        }

        public void MzMyNUp(double _utilRate)
        {
            if (_utilRate == 0)
                return;

            Mz0 /= _utilRate;
            My0 /= _utilRate ;
            N0 /=  _utilRate;

            double iy = y_interpolate(My0);
        }

        /// <summary>
        /// Привязки арматуры
        /// </summary>
        /// <param name="_bD"></param>
        /// <param name="_bX"></param>
        /// <param name="_bY"></param>
        public void SetRods(List<double> _bD, List<double> _bX, List<double> _bY )
        {
            ds.Clear();
            y0s.Clear();
            z0s.Clear();
            
            int idx = 0;
            foreach (var d in _bD)
            {
                ds.Add(d);
                z0s.Add(_bX[idx]);
                y0s.Add(_bY[idx]);
                idx++;
            }
        }


        /// <summary>
        /// стержни арматуры       
        /// </summary>        
        public void GetRods(List<BSRod> _Rods, double _dx = 0, double _dy = 0)
        {
            ds.Clear();
            y0s.Clear();
            z0s.Clear();

            // вычисляем привязки арматуры ( 0 - левый нижний угол)
            foreach (var rod in _Rods)
            {
                ds.Add(rod.D);
                z0s.Add(rod.CG_X + _dx);
                y0s.Add(rod.CG_Y + _dy);                
            }
        }

        public BeamSection BeamSection  {
            set {
                m_BeamSection = value;
            }
        }

        private BeamSection m_BeamSection = BeamSection.Rect;

        #region Поля, свойства  - данные для расчета
        // Продольная сила, кН, - сжатие
        private double N0 = 0; //-400.0;
        // Момент отн. оси Y, кН*см
        private double My0 = 0; // 9000;
        // Момент отн. оси Z, кН*см
        private double Mz0 = 0;// 1000;
        // Ширина сечения, см
        private double b = 0; // 20.0;
        // высота сечения, см
        private double h = 0; //40.0;
        // тавр-двутавр
        private double bf, hf, bw, hw, b1f, h1f;
        // кольцо
        private double r1, R2;

        // число элементов вдоль y, шт
        private int ny = 0; // 4;
        // число элементов вдоль z шт.
        private int nz = 0; //4;

        // диаметры арматурных стержней
        private List<double> ds = new List<double>() { 0, 0, 0, 0 };
        // привязки арматуры
        private List<double> y0s = new List<double>() { 0, 0, 0, 0 };
        private List<double> z0s = new List<double>() { 0, 0, 0, 0 };

        // Параметры материалов
        // Бетон B25 кН/см2
        private double Eb0 = 0; // 30.0 * Math.Pow(10, 3) / 10.0; //Начальный модуль бетона, кН/см2
        private double Ebt = 0; //Начальный модуль упругости фибробетона, кН/см2
        private double Rbc = 0; // 14.5 / 10d; // Расчетное сопротивление бетона на сжатие, кН/см2

        private double Rfbt = 0;// 1.05 / 10d; // Расчетное сопротивление фибробетона на растяжение, кН/см2
        private double Rfbt2 = 0;// 1.05 / 10d; // Расчетное сопротивление фибробетона на сжатие, кН/см2
        private double Rfbt3 = 0;// 1.05 / 10d; // Расчетное сопротивление фибробетона на сжатие, кН/см2

        // сжатие
        private double ebc0 = 0.002; // Деформация бетона на сжатие
        private double ebc2 = 0.0035; // Предельная деформация бетона на сжатие
                 
        // растяжение 
        private double efbt0 = 0.0; // Деформация фибробетона на растяжение
        private double efbt1 = 0.0001;
        private double efbt2 = 0.00015; // Предельная деформация фибробетона на растяжение
        private double efbt3 = 0.02; // Предельная деформация фибробетона на растяжение
        private double e_fbt_ult = 0;

        // Арматура кН/см2
        private double Es0 = 0; //Начальный модуль арматуры, кН/см2       
        private double Rst = 0; // Прочность арматуры на растяжение        
        private double Rsc = 0;  // Прочность арматуры на сжатие
        // сжатие        
        private double esc0 = 0; // Rsc / Es0;
        private double esc2 = 0.025;
        // растяжение
        private double est0 = 0; // Rst / Es0;
        private double est2 = 0.025;

        private List<double> My;
        private List<double> Mz;

        // проверка сечения на усилия                
        public  double Mzint { get; private set; }
        public double Myint { get; private set; }
        public double Nint { get; private set; }

        // моменты образования трещины        
        public double Mz_crc { get; private set; }
        public double My_crc { get; private set; }       
        public double N_crc { get; private set; }

        // деформация в момент образования трещины
        public double es_crc { get; private set; }

        // напряжение в арматуре в сечении с трещиной
        public double sig_s_crc { get; private set; }

        // ширина раскрытия трещины
        public double a_crc { get; private set; }
        public List<double> A_Crc { get; private set; }
        #endregion

        // коэффициенты использоввания
        // по деформациям бетона
        public double UtilRate_fb { get; private set; }
        // по деформациям арматуры
        public double UtilRate_s { get; private set; }


        // максимальное число итераций
        private static int jmax = 20000;
        // Максимальная абсолютная погрешность
        private static double tolmax = Math.Pow(10, -6);
        private static int err = 0;
        private Dictionary<string, double> m_Results = new Dictionary<string, double>();

        public int Err => err;
        public Dictionary<string, double> Results => m_Results;
        
        /// <summary>
        /// напряжения в элементах сечения 
        /// </summary>
        public List<double> SigmaBResult { get; private set; }
        /// <summary>
        /// напряжения в сечении арматуры 
        /// </summary>
        public List<double> SigmaSResult { get; private set; }
        /// <summary>
        /// деформации в элементах сечения 
        /// </summary>
        public List<double> EpsilonBResult { get; private set; }
        /// <summary>
        /// деформации в арматуре 
        /// </summary>
        public List<double> EpsilonSResult { get; private set; }

        #region разбивка сечения на элементы
        // массив привязок бетонных эл-в к вспомогательной оси y0
        private List<double> y0b = new List<double>();

        // массив привязок бетонных эл-в к вспомогательной оси z0
        private List<double> z0b = new List<double>();

        // массив площадей элементов
        private List<double> Ab = new List<double>();

        // массив площадей арматуры
        private List<double> As = new List<double>();
        #endregion

        private void InitSectionsLists()
        {
            Ab = new List<double>();
            y0b = new List<double>();
            z0b = new List<double>();
            As = new List<double>();
            A_Crc = new List<double>();
        }
        
              
        /// <summary>
        ///  Запустить расчет
        /// </summary>
        public void Run()
        {
            try
            {                
                Calculate();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }                   
        }       
    }
}
