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

    public class BSMatFiber : IMaterial
    {
        public string Name => "Фибробетон";

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
