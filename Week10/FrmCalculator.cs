using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Week10
{
    public partial class FrmCalculator : Form
    {
        public FrmCalculator()
        {
            InitializeComponent();
        }

        private void DigitHandler_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (lblDisplay.Text.Length == 1 && lblDisplay.Text == "0")
                lblDisplay.Text = button.Text;
            else
                lblDisplay.Text += button.Text;
        }

        private void btnBackspace_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text.Length >= 1)
            {
                lblDisplay.Text = lblDisplay.Text.Substring(0, lblDisplay.Text.Length -1);
            }
        }
    }
}
