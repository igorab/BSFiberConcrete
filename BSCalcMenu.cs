using BSFiberConcrete;
using BSFiberConcrete.Beam;
using BSFiberConcrete.Section;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BSCalcMenu
{
    public partial class BSCalcMenu : Form
    {
        public BSCalcMenu()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnUnits_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> Units= new Dictionary<string, string>()
            {
                ["Размеры сечений"] = "см",
                ["Свойства сечений"] = "см",
                ["Линейные размеры"] = "см",
                ["Силы"] = "кг",
                ["Моменты сил"] = "кг*см",
                ["Площади арматуры"] = "см2",
                ["Углы"] = "град",
                ["Распределенные силы"] = "кг/см",
                ["Распределенные моменты"] = "кг*см /см",
                ["Давления"] = "кг/см2",
                ["Расчетные сопротивления"] = "МПа -> кг/см2",
                ["Удельный вес"] = "кг/см3",
                ["Разность температур"] = "град Ц",
            };

            string txt = "";
            foreach (var u in Units)
            {
                txt += string.Format("{0}: {1}\t\n", u.Key, u.Value);
            }

            MessageBox.Show(txt);
        }

        private void btnStaticEquilibrium_Click(object sender, EventArgs e)
        {            
            BSFiberMain bsFiberMain = new BSFiberMain();
            bsFiberMain.CalcType = CalcType.Static;
            bsFiberMain.Show();
        }

        private void btnFiberClass_Click(object sender, EventArgs e)
        {
            BSFiberSetup bsFiberSetup = new BSFiberSetup();
            bsFiberSetup.TabPageIdx = 0;
            bsFiberSetup.Show();
        }

        private void btnRebarClass_Click(object sender, EventArgs e)
        {
            BSFiberSetup bsFiberSetup = new BSFiberSetup();
            bsFiberSetup.TabPageIdx = 1;
            bsFiberSetup.Show();
        }

        private void btnCoefY_Click(object sender, EventArgs e)
        {
            BSFiberSetup bsFiberSetup = new BSFiberSetup();
            bsFiberSetup.TabPageIdx = 2;
            bsFiberSetup.Show();
        }

        private void btnBetonClass_Click(object sender, EventArgs e)
        {
            BSFiberSetup bsFiberSetup = new BSFiberSetup();
            bsFiberSetup.TabPageIdx = 3;
            bsFiberSetup.Show();
        }

        private void btnNonlinearDeform_Click(object sender, EventArgs e)
        {
            BSFiberMain bsFiberMain = new BSFiberMain();
            bsFiberMain.CalcType = CalcType.Nonlinear;
            bsFiberMain.Show();
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            BSFiberAboutBox bSFiberAboutBox = new BSFiberAboutBox();
            bSFiberAboutBox.Show();
        }

        private void btnGraphAF_Click(object sender, EventArgs e)
        {
            BSGraph bSGraph = new BSGraph();
            bSGraph.Show();
        }

        private void btnBeamCalc_Click(object sender, EventArgs e)
        {
            BSBeamCalcView beamCalcView = new BSBeamCalcView();
            beamCalcView.Show();

        }

        private void btnSectionDraw_Click(object sender, EventArgs e)
        {
            BSSectionDraw bSSectionDraw = new BSSectionDraw();
            bSSectionDraw.Show();
        }

        private void btnRFiber_Click(object sender, EventArgs e)
        {
           
        }

        private void btnRFbtFiber_Click(object sender, EventArgs e)
        {
            BSRFiber bSRFiber = new BSRFiber();
            bSRFiber.Show();
        }
    }
}
