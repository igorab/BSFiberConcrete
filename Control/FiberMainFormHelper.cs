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
        /// <summary>
        /// список названий свойств класса "InitBeamSectionGeometry", используемых для отображения в таблице dataGridSelection
        /// </summary>
        public static List<string> colNamesSectionGeometry = new List<string>() { "bw", "hw", "bf", "hf", "b1f", "h1f", "r1", "r2" };
        /// <summary>
        /// Константа для добавления в название столбцов dataGridSelection
        /// </summary>
        public const string postfixColNameSectionGeometry = ", см";


        /// <summary>
        /// Метод возращает индекс из списка beamSectionsGeometry соответсвующий beamSectionType
        /// </summary>
        /// <param name="beamSectionsGeometry"></param>
        /// <param name="beamSectionType"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Создается объект типа DataTable из beamSectionsGeometry в соответсвии с beamSectionType
        /// </summary>
        /// <param name="beamSectionsGeometry"></param>
        /// <param name="beamSectionType">тип BeamSection, определяющий инедкс  beamSectionsGeometry </param>
        /// <returns></returns>
        public static DataTable GetTableFromBeamSections(List<InitBeamSectionGeometry> beamSectionsGeometry, BeamSection beamSectionType)
        {
            int index = IndexOfSectionGeometry(beamSectionsGeometry, beamSectionType);
            InitBeamSectionGeometry SectionGeomet = beamSectionsGeometry[index];

            DataTable sectionGeometryTable = new DataTable();
            DataRow row = sectionGeometryTable.NewRow();

            // список значений, используемых для отображения
            //colNamesSectionGeometry;
            foreach (PropertyInfo property in typeof(InitBeamSectionGeometry).GetProperties())
            {
                string pName = property.Name;
                if (colNamesSectionGeometry.Contains(pName))
                {
                    if (property.GetValue(SectionGeomet) == null)
                    { continue; }

                    string tmpColName = pName + postfixColNameSectionGeometry; // добавление размерности в НАИМЕНОВАНИЕ столбца
                    sectionGeometryTable.Columns.Add(tmpColName, typeof(double));
                    row[tmpColName] = (double)property.GetValue(SectionGeomet);
                }
            }

            sectionGeometryTable.Rows.Add(row);
            return sectionGeometryTable;

            // если использовать Field, а не Property
            //foreach (FieldInfo field in typeof(InitBeamSectionGeometry).GetFields())
            //{
            //    string fName = field.Name;
            //    if (colNamesSectionGeometry.Contains(fName))
            //    {
            //        if (field.GetValue(SectionGeomet) == null)
            //        { continue; }

            //        string tmpColName = fName + postfixColNameSectionGeometry;
            //        sectionGeometryTable.Columns.Add(tmpColName, typeof(double));
            //        row[tmpColName] = (double)field.GetValue(SectionGeomet);
            //    }
            //}

        }

        /// <summary>
        /// из dataGridSection.DataSource формируется объект InitBeamSectionGeometry
        /// </summary>
        /// <param name="gridSectionTable"></param>
        /// <param name="beamSectionType"></param>
        /// <returns></returns>
        public static InitBeamSectionGeometry CreateBeamSectionsGeometry(DataTable gridSectionTable, BeamSection beamSectionType)
        {
            DataColumnCollection tableColumn = gridSectionTable.Columns;
            DataRowCollection tableRow = gridSectionTable.Rows;
            DataRow dataRow = tableRow[0];
            
            InitBeamSectionGeometry beamSectionGeometry = new InitBeamSectionGeometry();
            beamSectionGeometry.SectionTypeNum = beamSectionType;
            // 0_o
            // найти бы более человечный способ вытаскивать Description из BeamSection...
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
