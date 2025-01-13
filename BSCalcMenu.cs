using BSFiberConcrete;
using BSFiberConcrete.BSRFib;
using BSFiberConcrete.BSRFib.FiberCalculator;
using BSFiberConcrete.Calc;
using BSFiberConcrete.Inform.Rebar;
using BSFiberConcrete.Lib;
using BSFiberConcrete.LocalStrength;
using BSFiberConcrete.Section;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text.Json;
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

            MessageBox.Show(txt, "Система единиц измерений в программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnStaticEquilibrium_Click(object sender, EventArgs e)
        {            
            BSFiberMain bsFiberMain = new BSFiberMain();
            bsFiberMain.CalcType = CalcType.Static;
            bsFiberMain.InitHeader(labelInfo.Text, labelNormDoc.Text);
            bsFiberMain.Show();
        }

        private void btnFiberClass_Click(object sender, EventArgs e)
        {                                                                                         
            FiberConcreteInform fcInforn = new FiberConcreteInform();
            fcInforn.Show();
        }

        private void btnRebarClass_Click(object sender, EventArgs e)
        {          
            RebarInform rebarInform = new RebarInform();
            rebarInform.Show();
        }

        private void btnCoefY_Click(object sender, EventArgs e)
        {           
            CoefInform  coefInform = new CoefInform();
            coefInform.Show();
        }

        private void btnBetonClass_Click(object sender, EventArgs e)
        {          
            ConcreteInform сoncreteInform = new ConcreteInform();
            сoncreteInform.Show();
        }

        private void btnNonlinearDeform_Click(object sender, EventArgs e)
        {
            BSFiberMain bsFiberMain = new BSFiberMain();
            bsFiberMain.CalcType = CalcType.Nonlinear;
            bsFiberMain.InitHeader(labelInfo.Text, labelNormDoc.Text);
            bsFiberMain.Show();
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            BSFiberAboutBox bSFiberAboutBox = new BSFiberAboutBox();
            bSFiberAboutBox.Show();
        }

        private void btnGraphAF_Click(object sender, EventArgs e)
        {
            BSRFibLabGraph bSGraph = new BSRFibLabGraph();
            bSGraph.Show();
        }

        private void btnBeamCalc_Click(object sender, EventArgs e)
        {            
            BSFiberMain bsFiberMain = new BSFiberMain();
            bsFiberMain.CalcType = CalcType.BeamCalc;
            bsFiberMain.InitHeader(labelInfo.Text, labelNormDoc.Text);
            bsFiberMain.Show();
        }

        private void btnSectionDraw_Click(object sender, EventArgs e)
        {
            BSSectionDraw bSSectionDraw = new BSSectionDraw();
            bSSectionDraw.Show();
        }


        private void btnRFiberTensileStrength_Click(object sender, EventArgs e)
        {
            RFiberTensileStrength tensileStrength = new RFiberTensileStrength();
            tensileStrength.Show();
        }

        private void btnRFbtFiber_Click(object sender, EventArgs e)
        {           
            ViewFiberConcreteCalc viewFiberConcreteCalc = new ViewFiberConcreteCalc();
            viewFiberConcreteCalc.Show();
        }

        private void btnBeamDeflection_Click(object sender, EventArgs e)
        {
            RSRFibDeflection fibDeflection = new RSRFibDeflection();
            fibDeflection.Show();
        }

        /// <summary>
        /// Расчет на местное сжатие
        /// </summary>        
        private void btnLocalCompressionCalc_Click(object sender, EventArgs e)
        {
            BSLocalCompressionCalc compressionCalc = new BSLocalCompressionCalc();
            compressionCalc.InitDataSource();
            BSLocalStrength localStrength = new BSLocalStrength();
            localStrength.TypeOfCalc = TypeOfLocalStrengthCalc.Compression;            
            localStrength.StrengthCalc = compressionCalc;
            localStrength.Show();                        
        }

        /// <summary>
        /// Расчет на продавливание
        /// </summary>        
        private void btnPunchCalc_Click(object sender, EventArgs e)
        {
            BSLocalPunchCalc punchCalc = new BSLocalPunchCalc();
            punchCalc.InitDataSource();            
            BSLocalStrength localStrength = new BSLocalStrength();
            localStrength.TypeOfCalc = TypeOfLocalStrengthCalc.Punch;            
            localStrength.StrengthCalc = punchCalc;            
            localStrength.Show();                        
        }

        /// <summary>
        /// 5.2.8 Основные деформационные характеристики сталефибробетона
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEfib_Click(object sender, EventArgs e)
        {
            BSEFib bsEFib = new BSEFib();
            bsEFib.Show();
        }

        private void btnUnitCalculator_Click(object sender, EventArgs e)
        {
            BSFiberConcrete.Calc.BSUnitCalculator unitCalculator = new BSUnitCalculator();

            unitCalculator.Show();
        }
       
        private void btnDeflectionsUlt_Click(object sender, EventArgs e)
        {
            //BSDeflectionsUlt deflectionsUlt = new BSDeflectionsUlt();
            //deflectionsUlt.Show();
            DeflectionUltimateInform deflectionUltimateInform = new DeflectionUltimateInform();
            deflectionUltimateInform.Show();
        }

        private void btnFiberClassSetup_Click(object sender, EventArgs e)
        {        
            //BSFiberClassSetup fiberClassSetup = new BSFiberClassSetup();
            //fiberClassSetup.Show();
            FiberRebarInform rebarInform = new FiberRebarInform();
            rebarInform.Show();
        }

        private void btnCalcOfLenRebar_Click(object sender, EventArgs e)
        {
            ViewCalcOfLenRebar calcOfLenRebar = new ViewCalcOfLenRebar();
            calcOfLenRebar.Show();
        }

        private void InitHeader()
        {
            if (BSData.ConfigId == BSFiberLib.Config297)
            {
                labelInfo.Text = "Конструкции фибробетонные с неметаллической фиброй и стальной арматурой";
                labelInfo.BackColor =  Color.BurlyWood;
                labelNormDoc.Text = "СП297.1325800.2017";
            }
            else if (BSData.ConfigId == BSFiberLib.Config405)
            {
                labelInfo.Text = "Конструкции фибробетонные с неметаллической фиброй и полимерной арматурой";
                labelInfo.BackColor = Color.Beige;
                labelNormDoc.Text = "СП405.1325800.2018";
            }
            else if (BSData.ConfigId == BSFiberLib.ConfigDefault)
            {
                labelInfo.Text = "Конструкции фибробетонные с металлической фиброй и стальной арматурой";
                labelInfo.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
                labelNormDoc.Text = "СП360.1325800.2017";
            }
        }

        private void BSCalcMenu_Load(object sender, EventArgs e)
        {            
            string path = Path.Combine(Environment.CurrentDirectory, "Templates\\BSConfig.json");
            Dictionary<string, string> config = new Dictionary<string, string>() ;

            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                config = JsonSerializer.Deserialize< Dictionary<string, string>> (fs);

                BSData.ConfigId = config["ConfigId"];
            }

            InitHeader();
        }
    }
}
