using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LegacyWinForm.Forms
{
    public partial class Test : Form
    {
        public Test()
        {
            InitializeComponent();
        }

        private void NetException_Click(object sender, EventArgs e)
        {
            try
            {
                // Doing Divide by Zero test
                // LOG.Info("Test Divide by Zero exception");
                // This may not applied too much with winforms as in c++
                long lValue = 928;
                long lZero = 0;
                lValue = lValue / lZero;
            }
            catch (Exception ex)
            {
                Trace.TraceError("Exception with .net routine test", ex);
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
