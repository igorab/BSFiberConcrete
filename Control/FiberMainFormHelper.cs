using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace BSFiberConcrete.Control
{
    public class FiberMainFormHelper
    {
                                public static List<string> colNamesSectionGeometry = new List<string>() { "b", "h", "bw", "hw", "bf", "hf", "b1f", "h1f", "r1", "r2" };
                                public const string postfixColNameSectionGeometry = ", см";
                                                        public static int IndexOfSectionGeometry(List<InitBeamSectionGeometry> beamSectionsGeometry, BeamSection beamSectionType)
        {
            int indexOfLiest = -1;
            for (int i = 0; i < beamSectionsGeometry.Count; i++)
            {
                if (beamSectionsGeometry[i].SectionTypeNum == beamSectionType)
                {
                    indexOfLiest = i;
                    break;
                }
            }
            return indexOfLiest;
        }
                                                        public static DataTable GetTableFromBeamSections(List<InitBeamSectionGeometry> beamSectionsGeometry, BeamSection beamSectionType)
        {
            int index = IndexOfSectionGeometry(beamSectionsGeometry, beamSectionType);
            InitBeamSectionGeometry SectionGeomet = beamSectionsGeometry[index];
            DataTable sectionGeometryTable = new DataTable();
            DataRow row = sectionGeometryTable.NewRow();
                                    foreach (PropertyInfo property in typeof(InitBeamSectionGeometry).GetProperties())
            {
                string pName = property.Name;
                if (colNamesSectionGeometry.Contains(pName))
                {
                    if (property.GetValue(SectionGeomet) == null)
                    { continue; }
                    string tmpColName = pName + postfixColNameSectionGeometry;                     sectionGeometryTable.Columns.Add(tmpColName, typeof(double));
                    row[tmpColName] = (double)property.GetValue(SectionGeomet);
                }
            }
            sectionGeometryTable.Rows.Add(row);
            return sectionGeometryTable;
                                                                                                
                                                            
        }
                                                        public static InitBeamSectionGeometry CreateBeamSectionsGeometry(DataTable gridSectionTable, BeamSection beamSectionType)
        {
            DataColumnCollection tableColumn = gridSectionTable.Columns;
            DataRowCollection tableRow = gridSectionTable.Rows;
            DataRow dataRow = tableRow[0];
            
            InitBeamSectionGeometry beamSectionGeometry = new InitBeamSectionGeometry();
            beamSectionGeometry.SectionTypeNum = beamSectionType;
                                    DescriptionAttribute description = typeof(BeamSection).GetField(beamSectionType.ToString()).GetCustomAttribute(typeof(DescriptionAttribute), false) as DescriptionAttribute;
            beamSectionGeometry.SectionTypeStr = description.Description;
            foreach (PropertyInfo property in typeof(InitBeamSectionGeometry).GetProperties())
            {
                string pName = property.Name;
                if (colNamesSectionGeometry.Contains(pName))
                {
                    string tmpColName = pName + postfixColNameSectionGeometry;
                    if (tableColumn.Contains(tmpColName))
                    {
                        double tmValue = (double)dataRow[tmpColName];
                        property.SetValue(beamSectionGeometry, tmValue);
                    }
                    else
                    { property.SetValue(beamSectionGeometry, null); }
                }
            }
            return beamSectionGeometry;
        }
    }
}
