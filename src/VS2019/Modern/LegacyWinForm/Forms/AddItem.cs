using LegacyWinForm.Models;
using LegacyWinForm.Support;
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
    public partial class AddItem : Form
    {
        public Order _order { get; set; }
        public List<Item> _itemList { get; set; }


        public AddItem(Order order,
                      List<Item> itemList )
        {
            _order = order;
            _itemList = itemList;
            InitializeComponent();

     
            foreach (Item item in itemList)
            {
                ComboboxItem boxItem = new ComboboxItem();

                string strDesc = item.Description.Trim();
                string strAmount = "$ " + item.Amount.ToString("F2");
                string str = strDesc.PadRight(50, ' ') + strAmount.PadLeft(10, ' ');

                boxItem.Text = str;
                boxItem.Value = item;
                comboItems.Items.Add(boxItem);
            }
            comboItems.SelectedIndex = 0;


            for (int Index = 1; Index < 6; Index++)
                comboQty.Items.Add(Index.ToString());
            comboQty.SelectedIndex = 0;
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            if (_order != null)
            {
                ComboboxItem boxItem = (ComboboxItem)comboItems.SelectedItem;
                if (_order.Items == null)
                    _order.Items = new List<Item>();
                Item item = (Item)boxItem.Value;
                string strQty = (string)comboQty.SelectedItem;
                item.Quantity = Convert.ToInt32(strQty);
                _order.Items.Add(item);

            }

            Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
