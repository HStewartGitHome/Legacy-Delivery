namespace LegacyWinForm
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.NewItem = new System.Windows.Forms.Button();
            this.SendMessage = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SeedItems = new System.Windows.Forms.Button();
            this.textTotal = new System.Windows.Forms.TextBox();
            this.textTax = new System.Windows.Forms.TextBox();
            this.textBeforeTax = new System.Windows.Forms.TextBox();
            this.textOrderID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Exit = new System.Windows.Forms.Button();
            this.Test = new System.Windows.Forms.Button();
            this.Save = new System.Windows.Forms.Button();
            this.New = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // NewItem
            // 
            this.NewItem.Location = new System.Drawing.Point(6, 44);
            this.NewItem.Name = "NewItem";
            this.NewItem.Size = new System.Drawing.Size(116, 54);
            this.NewItem.TabIndex = 0;
            this.NewItem.Text = "New Item";
            this.NewItem.UseVisualStyleBackColor = true;
            this.NewItem.Click += new System.EventHandler(this.NewItem_Click);
            // 
            // SendMessage
            // 
            this.SendMessage.Location = new System.Drawing.Point(6, 230);
            this.SendMessage.Name = "SendMessage";
            this.SendMessage.Size = new System.Drawing.Size(116, 54);
            this.SendMessage.TabIndex = 1;
            this.SendMessage.Text = "SendMessage";
            this.SendMessage.UseVisualStyleBackColor = true;
            this.SendMessage.Click += new System.EventHandler(this.SendMessage_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.SeedItems);
            this.groupBox1.Controls.Add(this.textTotal);
            this.groupBox1.Controls.Add(this.textTax);
            this.groupBox1.Controls.Add(this.textBeforeTax);
            this.groupBox1.Controls.Add(this.textOrderID);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.listBox1);
            this.groupBox1.Controls.Add(this.NewItem);
            this.groupBox1.Controls.Add(this.SendMessage);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(880, 557);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Order";
            // 
            // SeedItems
            // 
            this.SeedItems.Location = new System.Drawing.Point(6, 132);
            this.SeedItems.Name = "SeedItems";
            this.SeedItems.Size = new System.Drawing.Size(116, 54);
            this.SeedItems.TabIndex = 13;
            this.SeedItems.Text = "Seed Items";
            this.SeedItems.UseVisualStyleBackColor = true;
            this.SeedItems.Click += new System.EventHandler(this.SeedItems_Click);
            // 
            // textTotal
            // 
            this.textTotal.Location = new System.Drawing.Point(738, 515);
            this.textTotal.Name = "textTotal";
            this.textTotal.ReadOnly = true;
            this.textTotal.Size = new System.Drawing.Size(136, 20);
            this.textTotal.TabIndex = 12;
            // 
            // textTax
            // 
            this.textTax.Location = new System.Drawing.Point(738, 445);
            this.textTax.Name = "textTax";
            this.textTax.ReadOnly = true;
            this.textTax.Size = new System.Drawing.Size(136, 20);
            this.textTax.TabIndex = 11;
            // 
            // textBeforeTax
            // 
            this.textBeforeTax.Location = new System.Drawing.Point(738, 417);
            this.textBeforeTax.Name = "textBeforeTax";
            this.textBeforeTax.ReadOnly = true;
            this.textBeforeTax.Size = new System.Drawing.Size(136, 20);
            this.textBeforeTax.TabIndex = 10;
            // 
            // textOrderID
            // 
            this.textOrderID.Location = new System.Drawing.Point(203, 445);
            this.textOrderID.Name = "textOrderID";
            this.textOrderID.ReadOnly = true;
            this.textOrderID.Size = new System.Drawing.Size(100, 20);
            this.textOrderID.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(584, 417);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Before Tax";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(584, 452);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Tax";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(584, 518);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Total";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(137, 445);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Order ID";
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 21;
            this.listBox1.Location = new System.Drawing.Point(137, 44);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(737, 340);
            this.listBox1.TabIndex = 4;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Exit);
            this.groupBox2.Controls.Add(this.Test);
            this.groupBox2.Controls.Add(this.Save);
            this.groupBox2.Controls.Add(this.New);
            this.groupBox2.Location = new System.Drawing.Point(911, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(140, 557);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Commands";
            // 
            // Exit
            // 
            this.Exit.Location = new System.Drawing.Point(18, 497);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(116, 54);
            this.Exit.TabIndex = 4;
            this.Exit.Text = "Exit";
            this.Exit.UseVisualStyleBackColor = true;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // Test
            // 
            this.Test.Location = new System.Drawing.Point(18, 230);
            this.Test.Name = "Test";
            this.Test.Size = new System.Drawing.Size(116, 54);
            this.Test.TabIndex = 3;
            this.Test.Text = "Test";
            this.Test.UseVisualStyleBackColor = true;
            this.Test.Click += new System.EventHandler(this.Test_Click);
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(18, 132);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(116, 54);
            this.Save.TabIndex = 2;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // New
            // 
            this.New.Location = new System.Drawing.Point(18, 44);
            this.New.Name = "New";
            this.New.Size = new System.Drawing.Size(116, 54);
            this.New.TabIndex = 1;
            this.New.Text = "New ";
            this.New.UseVisualStyleBackColor = true;
            this.New.Click += new System.EventHandler(this.New_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1063, 595);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Legacy WinForms Delivery";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button NewItem;
        private System.Windows.Forms.Button SendMessage;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textTotal;
        private System.Windows.Forms.TextBox textTax;
        private System.Windows.Forms.TextBox textBeforeTax;
        private System.Windows.Forms.TextBox textOrderID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button Exit;
        private System.Windows.Forms.Button Test;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.Button New;
        private System.Windows.Forms.Button SeedItems;
    }
}

