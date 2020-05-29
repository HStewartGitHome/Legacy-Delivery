
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LegacyWinForm.Support;


namespace LegacyWinForm.Forms
{
    public partial class SendMessage : Form
    {
        // MessageModel messageModel { get; set; }

        public SendMessage()
        {
           // messageModel = new MessageModel();
            InitializeComponent();
            textLocation.Text = "LEGACY_WINFORM";
            textDateTime.Text = DateTime.Now.ToString();

        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            LegacyWinForm.Models.Message message = new LegacyWinForm.Models.Message();

            message.ToName = textToName.Text;
            message.FromName = textFromName.Text;
            message.Location = textLocation.Text;
            message.Time = textDateTime.Text;
            message.MessageText = textMessage.Text;
            message.MessageType = 1;


            Support.Support support = new Support.Support();
            support.SaveMessage(message);

            Close();
        }
    }
}
