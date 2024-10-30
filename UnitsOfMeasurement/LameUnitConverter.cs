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
using System.Data.Entity.Core.Mapping;

namespace BSFiberConcrete.UnitsOfMeasurement
{

                public class LameUnitConverter
    {


        private LengthMeasurement _lengthMeasurement;

        private ForceMeasurement _forceMeasurement;

        private MomentOfForceMeasurement _momentOfForceMeasurement;


        public LameUnitConverter()
        {
                        _lengthMeasurement = new LengthMeasurement(LengthUnits.m);
            _forceMeasurement = new ForceMeasurement(ForceUnits.kg);
            _momentOfForceMeasurement = new MomentOfForceMeasurement(MomentOfForceUnits.kgBycm);

        }

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

        public double ConvertRevertLength(double input)
        {
            return _lengthMeasurement.ModelToCustomUnit(input);
        }

        public double ConvertForce(double input)
        {
            return _forceMeasurement.CustomToModelUnit(input);
        }

        public double ConvertRevertForce(double input)
        {
            return _forceMeasurement.ModelToCustomUnit(input);
        }


        public double ConvertMomentOfForce(double input)
        {
            return _momentOfForceMeasurement.CustomToModelUnit(input);
        }

        public double ConvertRevertMomentOfForce(double input)
        {
            return _momentOfForceMeasurement.ModelToCustomUnit(input);
        }
        #endregion


        #region get set

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



        public string GetModelNameLengthUnit()
        {
            return Extensions.GetDescription(_lengthMeasurement.ModelUnit);
        }

        public string GetModelNameForceUnit()
        {
            return Extensions.GetDescription(_forceMeasurement.ModelUnit);
        }

        public string GetModelNameMomentOfForceUnit()
        {
            return Extensions.GetDescription(_momentOfForceMeasurement.ModelUnit);
        }


        #endregion


        #region методы в которых используются достаточно сомнительные механизмы ветвления
        
                                                public double ConvertEfforts(string effortsName, double effortsValue)
        {
            double newValue = 0; 
                        if (effortsName.Contains("M"))
            {
                                newValue = this.ConvertMomentOfForce(effortsValue);
            }
            else
            {
                                newValue = this.ConvertForce(effortsValue);
            }
            return newValue;

        }


                                                public double ConvertRevertEfforts(string effortsName, double effortsValue)
        {
            double newValue = 0;
            if (effortsName.Contains("M"))             {
                                newValue = this.ConvertRevertMomentOfForce(effortsValue);
            }
            else
            {
                                newValue = this.ConvertRevertForce(effortsValue);
            }
            return newValue;

        }


                                                        public double ConvertEffortsForReport(string str, double value, out string nameCustomlMeaserment)
        {
            nameCustomlMeaserment = "";
            string[] strArray = str.Split('[', ']');

            if (strArray.Length >= 2)
            {
                string nameUnitMeasurment = strArray[1];
                string nameModelMeaserment = this.GetModelNameForceUnit();
                if (nameModelMeaserment == nameUnitMeasurment)
                {
                    nameCustomlMeaserment = this.GetCustomNameForceUnit();
                                        return this.ConvertRevertForce(value);
                }


                nameModelMeaserment = this.GetModelNameMomentOfForceUnit();
                if (nameModelMeaserment == nameUnitMeasurment)
                {
                    nameCustomlMeaserment = this.GetCustomNameMomentOfForceUnit();
                                        return this.ConvertRevertMomentOfForce(value);
                }
            }

            return value;
        }

                                                public string ChangeHT4ForForce(string headerText)
        {
                        string[] stringArray = headerText.Split(',');
            if (stringArray[0].Contains("M"))              { return headerText; }
            string nameUnitMeasurement = this.GetCustomNameForceUnit();
            return stringArray[0] + ", " + nameUnitMeasurement;

        }


                                                public string ChangeHTForMomentOfForce(string headerText)
        {
                        string[] stringArray = headerText.Split(',');
            if (stringArray[0].Contains('M'))             {
                string nameUnitMeasurement = this.GetCustomNameMomentOfForceUnit();
                return  stringArray[0] + ", " + nameUnitMeasurement;
            }
            return headerText;
        }
        #endregion




    }
}
