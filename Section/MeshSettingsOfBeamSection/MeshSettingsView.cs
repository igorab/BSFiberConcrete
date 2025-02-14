using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BSFiberConcrete.Section.MeshSettingsOfBeamSection
{
    public partial class MeshSettingsView : Form
    {

        public MeshSectionSettings _settings { get; set; }
        
        public MeshSettingsView(MeshSectionSettings settings)
        {
            _settings = settings;
            InitializeComponent();

            numMeshNX.DataBindings.Add(new Binding("Value", _settings, "NX", true));
            numMeshNY.DataBindings.Add(new Binding("Value", _settings, "NY", true));
            numMinAngle.DataBindings.Add(new Binding("Value", _settings, "MinAngle", true));
            numMaxArea.DataBindings.Add(new Binding("Value", _settings, "MaxArea", true));
        }


        private void MeshSettingsView_FormClosing(object sender, FormClosingEventArgs e)
        {
            // костыль
            // чтобы не сохраненное изменение объекта NumericUpDown (пользователь не переместил курсор)
            // сохранялось в переменную _settings при закрытии формы
            _settings.NX = (int)numMeshNX.Value;
            _settings.NY = (int)numMeshNY.Value;
            _settings.MinAngle = (double)numMinAngle.Value;
            _settings.MaxArea = (double)numMaxArea.Value;
        }

    }
}
