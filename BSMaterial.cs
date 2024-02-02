using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace BSFiberConcrete
{
    public interface IMaterial
    {
        string Name { get; }        
    }

    /// <summary>
    ///  Свойства обычного бетона
    /// </summary>
    public class BSMatConcrete : IMaterial
    {
        public string Name => "Бетон";
        public string BT { get; set; }

        [DisplayName("Сопротивление на сжатие")]
        public double Rfbn { get; set; }

        public BSMatConcrete()
        {
            BT = "B30";
        }
    }

    /// <summary>
    ///  Материал стержня арматуры
    /// </summary>
    public class BSMatRod : IMaterial
    {
        public string Name => "Стержень арматуры";

        /// <summary>
        /// модуль упругости
        /// </summary>
        public double Es { get; set; }

        /// <summary>
        /// коэффициент упругости
        /// </summary>
        public double n_sj { get; set; }

        public double Eps_s_ult { get; set; } 
    }

    /// <summary>
    /// Свойства фибробетона
    /// </summary>
    public class BSMatFiber : IMaterial
    {
        public string Name => "Фибробетон";

        /// <summary>
        /// Начальный модуль упругости
        /// </summary>
        public double Efb { get; set; }

        /// <summary>
        /// Коэффициент упругости
        /// </summary>
        public double n_fb { get; set; }

        public double Eps_fb_ult { get; set; }

        public string BT { get; set; }

        [DisplayName("Сопротивление на сжатие")]
        public double Rfbn { get; set; }

        [DisplayName("Нормативное остаточное сопротивление осевому растяжению")]
        public double Rfbt3n { get; set; }   

        public BSMatFiber()
        {            
        }       
    }

}
