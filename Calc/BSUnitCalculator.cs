﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BSFiberConcrete.Calc
{
    public partial class BSUnitCalculator : Form
    {
        public BSUnitCalculator()
        {
            InitializeComponent();
        }

        private void numFrom_ValueChanged(object sender, EventArgs e)
        {
            numTo.Value = (decimal) BSHelper.MPA2kgsm2((double?) numFrom.Value);
            numTo2.Value = (decimal)BSHelper.MPA2kNsm2((double?)numFrom.Value);
        }

        private void numFromF_ValueChanged(object sender, EventArgs e)
        {
            numToF.Value = (decimal)BSHelper.kN2Kgs((double?)numFromF.Value);
        }
        
        private decimal UConv(decimal _value, Func<double?, double> _tounit) 
        {
            return (decimal)_tounit((double) _value);
        }

        private void numFromM_ValueChanged(object sender, EventArgs e)
        {
            string uFrom = (string)cmbFromM.SelectedItem;
            string uTo = (string)cmbToM.SelectedItem ;

            if (uFrom == "кНм")
            {
                if (uTo == "кг*см")
                    numToM.Value = UConv(numFromM.Value, BSHelper.kNm2kgssm);
            }
            else if (uFrom == "Нмм")
            {
                if (uTo == "кг*см")
                    numToM.Value = UConv(numFromM.Value, BSHelper.Nmm2kgssm);
            }
            else if (uFrom == "кг*см")
            {
                if (uTo == "Нмм")
                    numToM.Value = UConv(numFromM.Value, BSHelper.kgssm2Nmm);
            }
        }
    }
}
