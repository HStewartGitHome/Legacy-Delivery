
using LegacyWinForm.Forms;
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

namespace LegacyWinForm
{
    public partial class Form1 : Form
    {
        Order order = null;
        public LegacyWinForm.Support.Support support { get; set;  }
        public List<Item> itemList = null;
        int TaxPercent = -1;

        public Form1()
        {
            order = null;
            support = new LegacyWinForm.Support.Support();
            InitializeComponent();
            itemList = support.GetItems();
            EnableButtons(false);
        }

        private void NewItem_Click(object sender, EventArgs e)
        {

            if (order == null)
                NewOrder();

            if (order != null)
            {
                if (order.CustomerObj == null)
                {
                    AddCustomer form = new AddCustomer(order);
                    form.ShowDialog();
                    if (form.DialogResult == DialogResult.OK)
                    {
                        if (order.CustomerObj == null)
                            order.CustomerObj = new Customer();
                        order.CustomerObj.CustomerNum = support.GetContextNum();
                    }
                }

                if (order.CustomerObj != null)
                { 
                    AddItem form1 = new AddItem(order, itemList);
                    form1.ShowDialog();
                }
                RefreshScreen();
            }
            
        }

        private void SendMessage_Click(object sender, EventArgs e)
        {
            SendMessage form = new SendMessage();
            form.ShowDialog();

        }

        private void New_Click(object sender, EventArgs e)
        {
           
            if ((  order != null ) && ( order.Items != null ) )
            {
                DialogResult dialogResult = MessageBox.Show("Do you want to save Order", "LegacyWinForm", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                    SaveOrder();
            }

            if (order == null)
                NewOrder();
            
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if ( (order != null) && ( order.Items != null ) )
                SaveOrder();
        }

        private void Test_Click(object sender, EventArgs e)
        {
            Test tester = new Test();
            tester.ShowDialog();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
           
            if ( (order != null) && ( order.Items != null ) )
            {
                DialogResult dialogResult = MessageBox.Show("Do you want to save Order", "LegacyWinForm", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                    SaveOrder();
            }
            

            Application.Exit();

        }

        internal void SaveOrder()
        {
            SaveOrder form = new SaveOrder();
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                support.SaveOrder(order);
                order = null;
                MessageBox.Show("Order has been saved", "LegacyWinForm", MessageBoxButtons.OK);

                RefreshScreen();
            }

          
        }

        internal void NewOrder()
        {
            order = support.NewOrder();
            RefreshScreen();

        }

        internal void CalculateTotal()
        {
            double dblTotal = 0;
            double dblAmount = 0;

            if (TaxPercent < 0)
                TaxPercent = support.GetTaxPercent();

            if ( ( order != null ) && (order.Items != null ) )
            {
                foreach( Item item in order.Items )
                {
                    dblAmount = item.Amount * item.Quantity;
                    dblTotal += dblAmount;
                }

                order.BeforeTax = dblTotal;
                order.Tax = (order.BeforeTax * TaxPercent) / 100;
                order.Total = order.BeforeTax + order.Tax;
            }

        }

        internal void RefreshScreen()
        {
            CalculateTotal();

            listBox1.Items.Clear();

            if ( (order != null ) && ( order.Items != null ) )
            {
                foreach( Item item in order.Items )
                {
                    string strDesc = item.Description.Trim();
                    string strAmount = "$ " + item.Amount.ToString("F2");
                    string str = item.Quantity.ToString() + "    " + strDesc.PadRight(50, ' ') + strAmount.PadLeft(10, ' ') ;
                    listBox1.Items.Add(str);
                }

                textBeforeTax.Text = "$" + order.BeforeTax.ToString("F2");
                textTax.Text = "$" + order.Tax.ToString("F2");
                textTotal.Text = "$" + order.Total.ToString("F2");
                textOrderID.Text = order.OrderNum.ToString(); 

            }
            else
            {
                textBeforeTax.Text = "";
                textTax.Text = "";
                textTotal.Text = "";
                textOrderID.Text = "";
            }

            if ((order != null) && (order.Items != null))
                EnableButtons(true);
            else
                EnableButtons(false);

        }

        internal void EnableButtons( bool validOrder )
        {
            Save.Enabled = validOrder;
            SeedItems.Enabled = !validOrder;
        }



        private void SeedItems_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you wish to send local ItemList to Server?", "LegacyWinForm", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
                support.SeedItemList();

        }
    }
}
