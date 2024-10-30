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
        private BSFiberCalc_MNQ m_FiberCalc;
                private Dictionary<string, string> m_PropAttr;
        public BSFiberReport_MNQ()
        {
            m_PropAttr = new Dictionary<string, string>();
            m_GeomParams = new Dictionary<string, double>();
            m_Coeffs = new Dictionary<string, double>();
            m_PhysParams = new Dictionary<string, double>();
            m_CalcResults = new Dictionary<string, double>();
            ReportName = typeof(BSFiberCalc_MNQ).GetCustomAttribute<DisplayNameAttribute>().DisplayName;
        }
        public virtual void InitFromFiberCalc(BSFiberCalc_MNQ _fiberCalc)
        {
            m_FiberCalc = _fiberCalc;
            m_Messages = _fiberCalc.Msg;
            m_Efforts = new Dictionary<string, double> (_fiberCalc.m_Efforts);
            ImageCalc = _fiberCalc.ImageCalc();
            GetPropertiesAttr();
            InitFromAttr();
        }
                private void GetPropertiesAttr()
        {
            PropertyInfo[] props = typeof(BSFiberCalc_MNQ).GetProperties();
            foreach (PropertyInfo prop in props)
            {
                var attrs = prop.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attrs.Length > 0)
                { 
                    DescriptionAttribute attrDescr = attrs.Cast<DescriptionAttribute>().Single();
                    string descr = attrDescr.Description;
                    var attributes = prop.GetCustomAttributes(typeof(DisplayNameAttribute), true);
                    if (attributes.Length > 0)
                    {
                        DisplayNameAttribute attr = attributes.Cast<DisplayNameAttribute>().Single();
                        string displayName = attr.DisplayName;
                        m_PropAttr.Add(prop.Name, displayName + '@' + descr);
                    }                    
                }
            }            
        }
        private void InitFromAttr()
        {            
            Type myType = typeof(BSFiberCalc_MNQ);
            foreach (var attr in m_PropAttr)
            {
                PropertyInfo prop = myType.GetProperty(attr.Key);
                object value = prop?.GetValue(m_FiberCalc);
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
                        case "Res":
                            m_CalcResults.Add(attrValue, _d);
                            break;
                        case "Beam":
                            m_Beam.Add(attrValue, _d);
                            break;
                    }                        
                }
            }
        }
    }
    
}
