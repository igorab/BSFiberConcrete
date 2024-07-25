using BSFiberConcrete.Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BSFiberConcrete
{
    /// <summary>
    ///  Свойства бетона
    /// </summary>
    public class BSMatConcrete : IMaterial, INonlinear, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        public string Name => BSHelper.Concrete;
        public double E_young => Eb;

        /// <summary>
        /// Получить Имена классов бетона
        /// </summary>
        public List<string> NameOfBetonClassNames { get; private set; }
        public List<Beton> m_Beton { get; private set; }

        private int _betonIndex;

        /// <summary>
        /// Класс бетона
        /// </summary>
        private string _BT;
        /// <summary>
        /// Численное значение класса бетона
        /// </summary>
        private double _B;

        private double _Rb_n;
        private double _Rb;
        private double _Rb_ser;
        private double _Eb;
        private double _Eb_red;

        private double _Rbt_n;
        private double _Rbt;
        private double _Rbt_ser;
        // напряжение бетона на сжатие соответсвующее Eps_b1
        public double _Rb1;
        // напряжение бетона на сжатие соответсвующее Eps_b1_red
        public double _Rb1_red;
        //private double _Ebt;

        private double _Eps_b0;
        private double _Eps_b1;
        private double _Eps_b2;
        private double _Eps_b1_red;


        // влажность воздуха значение из EpsilonFromAirHumidity.AirHumidityStr
        private string _AirHumidity;
        private List<EpsilonFromAirHumidity> _EpsilonFromHumidity;
        /// <summary>
        /// Индекс класса бетона в соответсвии с NameOfBetonClassNames
        /// </summary>
        public int BetonIndex
        {
            get { return _betonIndex; }
            set
            {
                _betonIndex = value;
                ChangeBetonIndex();
            }
        }

        public string AirHumidity
        {
            get { return _AirHumidity; }
            set
            {
                _AirHumidity = value;
                ChangeAirHumidity();
            }
        }

        #region Property 

        public string BT
        {
            get => _BT;
            private set
            {
                _BT = value;
                OnPropertyChanged();
            }
        }
        public double B
        {
            get => _B;
            private set
            {
                _B = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Нормативное сопротивление бетона на сжатие
        /// </summary>
        public double Rb_n
        {
            get { return _Rb_n; }
            private set
            {
                _Rb_n = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Нормативное сопротивление бетона на растяжение
        /// </summary>
        public double Rbt_n
        {
            get { return _Rbt_n; }
            private set
            {
                _Rbt_n = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Расчетное сопротивление бетона на сжатие"
        /// </summary>
        public double Rb
        {
            get { return _Rb; }
            private set
            {
                _Rb = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Расчетное сопротивление бетона на растяжение
        /// </summary>
        public double Rbt
        {
            get { return _Rbt; }
            private set
            {
                _Rbt = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Расчетное сопротивление бетона для перд.сост. 2-ой группы(сжатие)
        /// </summary>
        public double Rb_ser
        {
            get { return _Rb_ser; }
            private set
            {
                _Rb_ser = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Расчетное сопротивление бетона для перд.сост. 2-ой группы(растяжение)
        /// </summary>
        public double Rbt_ser
        {
            get { return _Rbt_ser; }
            private set
            {
                _Rbt_ser = value;
                OnPropertyChanged();
            }
        }
        public double Rb1
        {
            get { return _Rb1; }
            private set
            {
                _Rb1 = value;
                OnPropertyChanged();
            }
        }
        public double Rb1_red
        {
            get { return _Rb1_red; }
            private set
            {
                _Rb1_red = value;
                OnPropertyChanged();
            }
        }
        public double Eps_b0
        {
            get { return _Eps_b0; }
            private set
            {
                _Eps_b0 = value;
                OnPropertyChanged();
            }
        }
        public double Eps_b1
        {
            get { return _Eps_b1; }
            private set
            {
                _Eps_b1 = value;
                OnPropertyChanged();
            }
        }
        public double Eps_b2
        {
            get { return _Eps_b2; }
            private set
            {
                _Eps_b2 = value;
                OnPropertyChanged();
            }
        }
        public double Eps_b1_red
        {
            get { return _Eps_b1_red; }
            private set
            {
                _Eps_b1_red = value;
                OnPropertyChanged();
            }
        }
        public double Eb
        {
            get { return _Eb; }
            private set
            {
                _Eb = value;
                OnPropertyChanged();
            }
        }
        public double Eb_red
        {
            get { return _Eb_red; }
            private set
            {
                _Eb_red = value;
                OnPropertyChanged();
            }
        }


        /// <summary>
        /// Модуль упругости бетона на растяжение
        /// </summary>
        //public double Ebt{ get; private set; }


        ///// <summary>
        ///// Нормативное сопротивление бетона на сжатие
        ///// </summary>
        //public double Omega
        //{
        //    get { return _Omega; }
        //    private set
        //    {
        //        _Omega = value;
        //        OnPropertyChanged();
        //    }
        //}

        #endregion




        public BSMatConcrete()
        {
            m_Beton = BSData.LoadBetonData();
            GetNameOfBetonClassNames();
            BetonIndex = 0;

            _EpsilonFromHumidity = BSData.LoadBetonEpsilonFromAirHumidity();
            AirHumidity = _EpsilonFromHumidity[0].AirHumidityStr;


            //TypeDiagram = DeformDiagramType.D2Linear;

            // Для продолжительной нагрузки значение зависит от влажности
            Eps_b2 = 0.0056;
            Eps_b0 = 0.004;

        }




        /// <summary>
        /// Изменить параметры бетона, зависящие от влаждности
        /// (относительные деформации и приведенный модуль упрогости)
        /// </summary>
        private void ChangeAirHumidity()
        {
            foreach (EpsilonFromAirHumidity Eps in _EpsilonFromHumidity)
            {
                if (Eps.AirHumidityStr == _AirHumidity)
                {
                    Eps_b0 = Eps.Eps_b0;
                    Eps_b2 = Eps.Eps_b2;
                    Eps_b1_red = Eps.Eps_b1_red;
                    break;
                }

            }

            Eb_red = Rb1_red / Eps_b1_red;
        }

        private void GetNameOfBetonClassNames()
        {
            NameOfBetonClassNames = new List<string>();

            foreach (Beton b in m_Beton)
            {
                NameOfBetonClassNames.Add(b.BT);
            }
        }



        /// <summary>
        /// Изменить свойства объекта при изменении (индекса) класса бетона
        /// </summary>
        private void ChangeBetonIndex()
        {
            BT = m_Beton[_betonIndex].BT;
            B = m_Beton[_betonIndex].B;

            //TODO Несоответсвие размерностей в базе данных и размерности используемой для расчетов

            // Необходимо выполнить перевод из МПа в кгс/см^2
            double k = 10;
            Rb_n = m_Beton[_betonIndex].Rbn * k;
            Rb = m_Beton[_betonIndex].Rb * k;
            Rb_ser = Rb_n;

            Rbt_n = m_Beton[_betonIndex].Rbtn * k;
            Rbt = m_Beton[_betonIndex].Rbt * k;
            Rbt_ser = Rbt_n;

            // перевод из МПа * 10^-3 в кгс/см^2
            Eb = m_Beton[_betonIndex].Eb * 1000 * k;
            Rb1 = 0.6 * Rb;

            Eps_b1 = Rb1 / Eb;

            Rb1_red = Rb;
            Eb_red = (Eps_b1_red != 0) ? Rb / Eps_b1_red : 0;

            //Omega = m_Beton[_betonIndex].Omega;


            // Нужно вызвать, тк Rb и Eb изменились
            //ChangeBetonTypeDiagram();

        }




        // Диаграмма состояния
        public  double Eps_StateDiagram3L(double e_b, out int _res, int _group = 1)
        {
            double sigma_b = Rb;
            double sigma_b1 = 0.6 * Rb;
            _res = 0;

            Eps_b1 = sigma_b1 / Eb;

            if (0 <= e_b  && e_b <= Eps_b1 )
            {
                sigma_b = Eb * e_b;
            }
            else if (Eps_b1 < e_b && e_b < Eps_b0)
            {
                sigma_b = ((1 - sigma_b1/Rb)*(e_b - Eps_b1)/ (Eps_b0 - Eps_b1) + sigma_b1/Rb )* Rb;
            }
            else if (Eps_b0 <= e_b && e_b <= Eps_b2)
            {
                sigma_b = Rb;                    
            }

            return sigma_b;
        }

        public double Eps_StDiagram2L(double _e, out int _res, int _group = 1)
        {
            double sgm = 0;
            _res = 0;

            if (0 <= _e && _e < Eps_b1)
            {
                sgm = Eb * _e;
            }
            else if (Eps_b1 <= _e && _e < Eps_b2) 
            {
                sgm = Rb;
            }

            return sgm;
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }



    }


        

}
