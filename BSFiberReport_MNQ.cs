using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BSFiberConcrete
{
    public class BSFiberReport_MNQ : BSFiberReport
    {
        private BSFiberCalc_MNQ fiberCalc;
        private Dictionary<string, string> geomAttr = new Dictionary<string, string>();

        public BSFiberReport_MNQ()
        {
            m_GeomParams = new Dictionary<string, double>();
            m_Coeffs = new Dictionary<string, double>();
            m_PhysParams = new Dictionary<string, double>();
        }

        public void Init(BSFiberCalc_MNQ _fiberCalc)
        {
            fiberCalc = _fiberCalc;
            m_CalcResults = fiberCalc.Results();

            ReportName = typeof(BSFiberCalc_MNQ).GetCustomAttribute<DisplayNameAttribute>().DisplayName;

            GetPropertiesAttr();

            InitFromAttr();
        }

        // получить параметры из свойств (атрибутов)
        private void GetPropertiesAttr()
        {
            PropertyInfo[] props = typeof(BSFiberCalc_MNQ).GetProperties();
            foreach (PropertyInfo prop in props)
            {
                var attrs = prop.GetCustomAttributes(typeof(DescriptionAttribute), true);
                DescriptionAttribute attrDescr = attrs.Cast<DescriptionAttribute>().Single();
                string descr = attrDescr.Description;

                var attribute = prop.GetCustomAttributes(typeof(DisplayNameAttribute), true);
                DisplayNameAttribute attr = attribute.Cast<DisplayNameAttribute>().Single();
                string displayName = attr.DisplayName;

                geomAttr.Add(prop.Name, displayName + '@' + descr);                
            }            
        }

        private void InitFromAttr()
        {            
            Type myType = typeof(BSFiberCalc_MNQ);
            foreach (var attr in geomAttr)
            {
                PropertyInfo prop = myType.GetProperty(attr.Key);

                object value = prop?.GetValue(fiberCalc);

                string[] attrDescr = attr.Value.ToString().Split('@');

                string attrValue = attrDescr[0];
                string attrType = attrDescr[1];
                
                if (double.TryParse(value.ToString(), out double _d))
                {
                    switch (attrType)
                    {
                        case "Geom":
                            if (_d > 0)
                                m_GeomParams.Add(attrValue, _d);
                            break;
                        case "Coef":   
                            m_Coeffs.Add(attrValue, _d);
                            break;
                        case "Phys":
                            m_PhysParams.Add(attrValue, _d) ;
                            break;
                    }                        
                }
            }

            /*
         m_Beam;                  
         m_CalcResults;
            */
        }
    }

    public class BSFiberReport_N : BSFiberReport_MNQ
    {
        
    }

}
