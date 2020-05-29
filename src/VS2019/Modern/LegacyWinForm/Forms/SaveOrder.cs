using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LegacyWinForm.Forms
{
    public partial class SaveOrder : Form
    {
        public SaveOrder()
        {
            InitializeComponent();
        }

        private void SaveLocal_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SaveRouter_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
