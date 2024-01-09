using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BSFiberConcrete
{
    public partial class BSFormPanel : Form
    {
        private Form active;

        public BSFormPanel()
        {
            InitializeComponent();
        }

        private void BSFormPanel_Load(object sender, EventArgs e)
        {
            PanelForm(new BSFiberSetup());
        }

        private void PanelForm(Form frm)
        {
            if (active != null) { active.Close(); }

            active = frm;
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            this.Panel.Controls.Add(frm);
            this.Panel.Tag = frm;
            frm.BringToFront();
            frm.Show();
        }
    }
}
