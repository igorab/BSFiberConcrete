using ScottPlot.Statistics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Text;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

using BSFiberConcrete.UnitsOfMeasurement.PhysicalQuantities;
using ScottPlot.Control;

namespace BSFiberConcrete.UnitsOfMeasurement
{

    /// <summary>
    /// Класс для работы с еденицами измерения
    /// </summary>
    public class LameUnitConverter
    {


        private LengthMeasurement _lengthMeasurement;

        private ForceMeasurement _forceMeasurement;

        private MomentOfForceMeasurement _momentOfForceMeasurement;


        public LameUnitConverter()
        {
            // Задаем значение единиц измерения длины в которых проводятся расчеты
            _lengthMeasurement = new LengthMeasurement(LengthUnits.m);
            _forceMeasurement = new ForceMeasurement(ForceUnits.kg);
            _momentOfForceMeasurement = new MomentOfForceMeasurement(MomentOfForceUnits.kgBycm);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelUnitsMeasurement">задаются расчетные значения ед измерения в строгом порядке</param>
        public LameUnitConverter(List<Enum> modelUnitsMeasurement)
        {
            _lengthMeasurement = new LengthMeasurement((LengthUnits)modelUnitsMeasurement[0]);
            _forceMeasurement = new ForceMeasurement((ForceUnits)modelUnitsMeasurement[1]);
            _momentOfForceMeasurement = new MomentOfForceMeasurement((MomentOfForceUnits)modelUnitsMeasurement[2]);
        }

        #region Change Custom or model Unit Measurement
        public void ChangeCustomUnitLength(int index )
        {
            _lengthMeasurement.CustomUnit = (LengthUnits)index;
        }
        public void ChangeCustomUnitForce(int index)
        {
            _forceMeasurement.CustomUnit = (ForceUnits)index;
        }
        public void ChangeCustomUnitMomentOfForce(int index)
        {
            _momentOfForceMeasurement.CustomUnit = (MomentOfForceUnits)index;
        }
        public void ChangeModelUnitLength(int index)
        {
            _lengthMeasurement.ModelUnit = (LengthUnits)index;
        }
        public void ChangeModelUnitForce(int index)
        {
            _forceMeasurement.ModelUnit = (ForceUnits)index;
        }
        public void ChangeModelUnitMomentOfForce(int index)
        {
            _momentOfForceMeasurement.ModelUnit = (MomentOfForceUnits)index;
        }
        #endregion

        #region Convert units measurement (Custom to model or model to custom)
        public double СonvertLength(double input)
        {
            return _lengthMeasurement.CustomToModelUnit(input);
        }

        public double revertConvertLength(double input)
        {
            return _lengthMeasurement.ModelToCustomUnit(input);
        }

        public double ConvertForce(double input)
        {
            return _forceMeasurement.CustomToModelUnit(input);
        }

        public double revertConvertForce(double input)
        {
            return _forceMeasurement.ModelToCustomUnit(input);
        }


        public double ConvertMomentOfForce(double input)
        {
            return _momentOfForceMeasurement.CustomToModelUnit(input);
        }

        public double revertConvertMomentOfForce(double input)
        {
            return _momentOfForceMeasurement.ModelToCustomUnit(input);
        }
        #endregion


        #region get set

        /// <summary>
        /// Получить название пользовательских ед измерения 
        /// </summary>
        /// <returns></returns>
        public string GetCustomNameLengthUnit()
        {
            return Extensions.GetDescription(_lengthMeasurement.CustomUnit);
        }

        public string GetCustomNameForceUnit()
        {
            return Extensions.GetDescription(_forceMeasurement.CustomUnit);
        }

        public string GetCustomNameMomentOfForceUnit()
        {
            return Extensions.GetDescription(_momentOfForceMeasurement.CustomUnit);
        }


        #endregion

    }
}
