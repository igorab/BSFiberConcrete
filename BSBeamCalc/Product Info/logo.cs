using System;

namespace CBAnsDes
{
    public sealed partial class logo
    {

        // TODO: This form can easily be set as the splash screen for the application by going to the "Application" tab
        // of the Project Designer ("Properties" under the "Project" menu).

        private int showV = 0;

        public logo()
        {
            InitializeComponent();
        }
        private void logo_Load(object sender, EventArgs e)
        {
            // Set up the dialog text at runtime according to the application's assembly information.  
            Timer1.Interval = 1000;
            Timer1.Enabled = true;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (showV >= 3)
            {
                Hide();
            }
            showV = showV + 1;
        }
    }
}