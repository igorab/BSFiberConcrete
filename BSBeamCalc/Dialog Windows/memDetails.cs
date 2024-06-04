using System;

namespace CBAnsDes
{
    public partial class memDetails
    {
        public string text1;

        public memDetails()
        {
            InitializeComponent();
        }
        private void memDetails_Load(object sender, EventArgs e)
        {
            TextBox1.Text = text1;
            TextBox1.SelectedText = "";
        }
    }
}