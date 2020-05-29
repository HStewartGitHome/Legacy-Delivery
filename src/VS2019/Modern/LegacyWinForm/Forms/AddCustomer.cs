using LegacyWinForm.Models;
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
    public partial class AddCustomer : Form
    {
        public Order _order { get; set; }

        public AddCustomer(Order order)
        {
            _order = order;
            InitializeComponent();
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            if (_order != null)
            {
                if (_order.CustomerObj == null)
                    _order.CustomerObj = new Customer();

                _order.CustomerObj.Name = textName.Text;
                _order.CustomerObj.Address = textAddress.Text;
                _order.CustomerObj.City = textCity.Text;
                _order.CustomerObj.State = textState.Text;
                _order.CustomerObj.Zip = textZip.Text;
            }
            Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
