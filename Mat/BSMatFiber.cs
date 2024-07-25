using BSFiberConcrete.Lib;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BSFiberConcrete
{
    /// <summary>
    /// Свойства фибробетона
    /// </summary>
    public class BSMatFiber : IMaterial, INonlinear, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private BSMatConcrete _Concrete;



        public List<BSFiberBeton> FiberTypeOnUlt = new List<BSFiberBeton>();
        public  List<Beton> FiberTypeOnCompression ;
        public List<FiberBft> FiberTypeOnTension;

        private int _indFiberTypeOnUlt;
        private int _indFiberTypeOnCompression;
        private int _indFiberTypeOnTension;

        public int IndFiberTypeOnUlt
        {
            get { return _indFiberTypeOnUlt; }
            set
            {
                _indFiberTypeOnUlt = value;
                ChangeFiberTypeOnUlt();
            }
        }
        public int IndFiberTypeOnCompression
        {
            get { return _indFiberTypeOnCompression; }
            set
            {
                _indFiberTypeOnCompression = value;
                ChangeFiberTypeOnCompression();
            }
        }
        public int IndFiberTypeOnTension
        {
            get { return _indFiberTypeOnTension; }
            set
            {
                _indFiberTypeOnTension = value;
                ChangeFiberTypeOnTension();
            }
        }



        public string Name => BSHelper.FiberConcrete;

        //public List<string> 
        public double E_young => Efb;


        private double Yft, Yb, Yb1, Yb2, Yb3, Yb5;




        protected DeformDiagramType _typeDiagram = DeformDiagramType.D2Linear;
        public DeformDiagramType TypeDiagram { get { return _typeDiagram; } }


        /// <summary>
        /// Коэффициент упругости
        /// </summary>
        public double Nu_fb { get; set; }


        //для фибры из тонкой низкоуглеродистой проволоки МП п.п  кг/см2
        public double Ef { get; set; }



        #region Характеристики взятые из _Concrete


        [DisplayName("Числовая характеристика класса фибробетона по прочности на осевое сжатие")]
        public double B { get => _Concrete.B; }

        public double R_fb { get => _Concrete.Rb; }

        [DisplayName("Нормативное сопротивление сталефибробетона осевому сжатию Rfbn")]
        public double Rfbn { get => _Concrete.Rb_n; }

        [DisplayName("Расчетное сопротивление сталефибробетона осевому сжатию Rfbn")]
        public double Rfb { get => _Concrete.Rb; }

        //[DisplayName("Расчетное сопротивление сталефибробетона осевому сжатию Rfbn")]
        //public double Rfb { get { return R_fb_calc(); } set { Rfb = value; } }
        //// Расчетные значения сопротивления на сжатиие по B30 СП63
        //public double R_fb_calc() => (Yb != 0) ? Rfbn / Yb * Yb1 * Yb2 * Yb3 * Yb5 : 0;
        //[DisplayName("Расчетное сопротивление сталефибробетона осевому сжатию 2-й группы Rfb, ser")]
        //public double Rfb_ser { get => _Concrete.Rb_ser; }


        // CORRECT name Eb => Efb 
        // Начальный модуль упругости бетона-матрицы B30 СП63
        public double Eb
        { get => _Concrete.Eb; }
        /// <summary>
        /// Начальный модуль упругости Фибробетона на СЖАТИЕ
        /// </summary>
        public double Efb { get => _Concrete.Eb; }

        public double Eb_red { get => _Concrete.Eb_red; }

        // disputable
        //характеристика сжатой зоны сталефибробетона, принимаемая для
        // сталефибробетона из тяжелого бетона классов до В60 включительно равной 0,8
        public double Omega => (B <= 60) ? 0.8 : 0.9;


        



        // CORRECT name e_b1 => Eps_fb1 
        public double Eps_fb0 { get => _Concrete.Eps_b0; }
        public double Eps_fb1 { get => _Concrete.Eps_b1; }
        public double Eps_fb2 { get => _Concrete.Eps_b2; }
        public double Eps_fb1_red { get => _Concrete.Eps_b1_red; }

        // относительные деформации сжатого сталефибробетона при напряжениях R/b,
        // принимаемые по указаниям СП 63.13330 как для обычного бетона
        public double e_b2 { get; set; }
        public double e_b1_red { get; set; }
        public double e_b1 { get; set; }

        #endregion





        /// <summary>
        /// Начальный модуль упругости Фибробетона на РАСТЯЖЕНИЕ
        /// </summary>
        public double Efbt { get; set; }

        /// <summary>
        ///  предельное значение относительной деформации фибробетона при сжатии
        /// </summary>
        public double Eps_fb_ult { get; set; }
        /// <summary>
        ///  предельное значение относительной деформации фибробетона при растяжении
        /// </summary>
        public double Eps_fbt_ult { get; set; }


        public double Eps_fbt0 { get; set; }
        public double Eps_fbt1 { get; set; }
        public double Eps_fbt2 { get; set; }
        public double Eps_fbt3 { get; set; }


        //Расчетные значения сопротивления фибробетона растяжению
        private double m_Rfbt;
        private double m_Rfbt2;
        private double m_Rfbt3;


        // предельная относительная деформация бетона при растяжении
        public const double Ebt0 = 0.0001;

        
        // DEL
        // Класс бетона
        //public string BTCls { get; set; }







        [DisplayName("Нормативное сопротивление сталефибробетона осевому растяжению Rfbt")]
        public double Rfbtn { get; set; }

        [DisplayName("Расчетное сопротивление сталефибробетона осевому растяжению Rfbt")]
        public double Rfbt { 
            get { return (m_Rfbt > 0) ? m_Rfbt : R_fbt_calc(); }
            private set { m_Rfbt = value; } 
        }

        [DisplayName("Расчетное сопротивление сталефибробетона осевому растяжению 2-й группы Rfbt, ser")]
        public double Rfbt_ser => Rfbtn;

        [DisplayName("Остаточное нормативное сопротивление на растяжение Rfbt2,n")]
        public double Rfbt2n { get; set; }

        [DisplayName("Остаточное расчетное сопротивление на растяжение Rfbt2")]
        public double Rfbt2 { 
            get { return (m_Rfbt2 > 0) ? m_Rfbt2 : R_fbt2_calc(); }
            private set { m_Rfbt2 = value; }
        }

        [DisplayName("Остаточное нормативное сопротивление осевому растяжению Rfbt3,n")]
        public double Rfbt3n { get; set; }

        [DisplayName("Остаточное расчетное сопротивление осевому растяжению Rfbt3")]
        public double Rfbt3 { 
            get { return (m_Rfbt3 > 0) ? m_Rfbt3 : R_fbt3_calc(); } 
            private set { m_Rfbt3 = value; } 
        }


        // коэффициент приведения арматуры к фибробетону Пособие к СП 52-102-2004 п.п.2.33
        public double alfa(double _Es) => _Es / Efb;



        /// <summary>
        /// Выбрать другой класс фибробетона на остаточное сопротивление
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void ChangeFiberTypeOnUlt()
        {
            throw new NotImplementedException();
        }
        private void ChangeFiberTypeOnCompression()
        {
            
        }
        private void ChangeFiberTypeOnTension()
        {
            throw new NotImplementedException();
        }


        //Расчетное остаточное сопротивление осевому растяжению R_fbt
        public double R_fbt_calc() => (Yft != 0) ? Rfbtn / Yft * Yb1 * Yb5 : 0;

        //Расчетное остаточное сопротивление осевому растяжению R_fbt2
        public double R_fbt2_calc() => (Yft != 0) ? Rfbt2n / Yft * Yb1 * Yb5 : 0;

        //Расчетное остаточное сопротивление осевому растяжению R_fbt3
        public double R_fbt3_calc() => (Yft != 0) ? Rfbt3n / Yft * Yb1 * Yb5 : 0;



        
        /// <summary>
        /// Инициализация данными с формы
        /// </summary>        
        public BSMatFiber(double _Efb, decimal _Yft, decimal _Yb, decimal _Yb1, decimal _Yb2, decimal _Yb3, decimal _Yb5)
        {
            // DEL
            //Efb = _Efb;
            Yft = (double)_Yft;
            Yb = (double)_Yb;
            Yb1 = (double)_Yb1;
            Yb2 = (double)_Yb2;
            Yb3 = (double)_Yb3;
            Yb5 = (double)_Yb5;
            _typeDiagram = DeformDiagramType.D2Linear;
        }

        // DEL
        /// <summary>
        /// 
        /// </summary>
        /// <param name="E"> [0] - Efb; [1] - Efbt</param>
        /// <param name="Y"> [0] - Yft; [1] - Yb; [1] - Yb1; [3] - Yb2; [4] - Yb3; [5] - Yb5. </param>
        /// <param name="_R"> [0] - Rfbn; [1] - Rfbtn; [2] - Rfbt2n; [3] - Rfbt3n;</param>
        //public BSMatFiber(decimal[] E, decimal[] Y, decimal[] _R)
        //{
        //    Efb = (double)E[0];
        //    Efb = (double)E[1];
        //    Yft = (double)Y[0];
        //    Yb = (double)Y[1];
        //    Yb1 = (double)Y[2];
        //    Yb2 = (double)Y[3];
        //    Yb3 = (double)Y[4];
        //    Yb5 = (double)Y[5];
        //    Rfbn = (double)_R[0];
        //    Rfbtn = (double)_R[1];
        //    Rfbt2n = (double)_R[2];
        //    Rfbt3n = (double)_R[3];
        //    _typeDiagram = DeformDiagramType.D2Linear;


        //    BSData.LoadFiberBft();
        //}

        //TODO Удалить эти конструкторы.
        public BSMatFiber()
        { 
        
        }

        public BSMatFiber(BSMatConcrete MatConcrate)
        {
            _Concrete = MatConcrate;


            FiberTypeOnUlt = BSFiberLib.BetonList;
            FiberTypeOnCompression = _Concrete.m_Beton;
            FiberTypeOnTension = BSData.LoadFiberBft();


            //IndFiberTypeOnUlt = 0;
            //IndFiberTypeOnCompression = 0;
            //IndFiberTypeOnTension = 0;










            _typeDiagram = DeformDiagramType.D2Linear;
        }


        /// <summary>
        /// Диаграмма состояния растяжения-сжатия фибробетона, в обозначениях СП360 
        /// </summary>
        /// <param name="_eps">Деформация</param>
        /// <returns>Напряжение</returns>       
        public double Eps_StateDiagram3L(double _eps, out int _res, int _group = 1 )
        {
            _res = 0;
            if (Efb == 0 || Rfbt == 0 || Rfbt2 == 0 || Rfbt3 == 0)
                return 0;

            double sigma;

            Func<double> TensileStrength = delegate()
            {
                double e_fbt = _eps;

                double e_fbt0 = Rfbt / Efb;
                double e_fbt1 = e_fbt0 + 0.0001;
                double e_fbt2 = 0.004;
                double e_fbt3 = (Rfbt2 != 0) ? 0.02 - 0.0125 * (Rfbt3 / Rfbt2 - 0.5) : 0;
                double sigma_fbt = 0;

                if (0 <= e_fbt && e_fbt <= e_fbt0)
                {
                    sigma_fbt = Efb * e_fbt;
                }
                else if (e_fbt0 < e_fbt && e_fbt <= e_fbt1)
                {
                    sigma_fbt = Rfbt;
                }
                else if (e_fbt1 < e_fbt && e_fbt <= e_fbt2)
                {                    
                    sigma_fbt = Rfbt * (1 - (1 - Rfbt2 / Rfbt) * (e_fbt - e_fbt1) / (e_fbt2 - e_fbt1));
                }
                else if (e_fbt1 < e_fbt && e_fbt <= e_fbt3)
                {                   
                    sigma_fbt = Rfbt * (1 - (1 - Rfbt3 / Rfbt2) * (e_fbt - e_fbt2) / (e_fbt3 - e_fbt2));
                }
                else if (e_fbt > e_fbt3)
                {
                    Debug.Assert(true, "Превышено остаточное сопротиваление");

                    sigma_fbt = 0;
                }

                return sigma_fbt;

            };
            
            // Знаки НЕ соответствуют диаграмме деформирования фибробетона в СП360
            if (_eps > 0) // растягивающие напряжения:  
            {
                sigma = TensileStrength();
            }
            else // сжимающие напряжения   
            {                
                sigma = 0;
            }

            return sigma;
        }

        /// <summary>
        /// Диаграмма деформирования двухлинейная
        /// на сжатие, как для бетона
        /// знак + для деформаций сжатия - для растяжения (такая диаграмма в СП 63)
        /// </summary>
        /// <param name="_e">Деформация</param>
        /// <returns>Напряжение</returns>        
        public double Eps_StDiagram2L(double _e, out int _res, int _group = 1)
        {
            double sgm = 0;
            _res = 0;

            _e = -1 * _e;  

            if (0 <= _e && _e < e_b1_red)
            {
                sgm = Eb_red * _e;
            }
            else if (e_b1_red <= _e && _e < e_b2)
            {
                sgm = R_fb;
            }
            else if (_e < 0) // растяжение
            {
                if (_group == 1)
                {
                    sgm = 0;
                }
                else if (_group == 2)
                {
                    sgm = Rfbt_ser;

                    // условие образование трещины
                    if (Math.Abs(_e) > Ebt0)
                    {
                        _res = 2;
                    }
                }

            }
            else if (_e >= e_b2) // уточнить такую ситуацию
            {
                Debug.Assert(true, "Превышен предел прочности (временное сопротивление) ");

                sgm = 0; 
            }

            return sgm;
        }
     
    }
}
