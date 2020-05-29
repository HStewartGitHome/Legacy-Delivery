namespace LegacyWinForm.Forms
{
    partial class Test
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
            this.NetException = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // NetException
            // 
            this.NetException.Location = new System.Drawing.Point(61, 42);
            this.NetException.Name = "NetException";
            this.NetException.Size = new System.Drawing.Size(234, 56);
            this.NetException.TabIndex = 1;
            this.NetException.Text = ".Net Exception";
            this.NetException.UseVisualStyleBackColor = true;
            this.NetException.Click += new System.EventHandler(this.NetException_Click);
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(61, 247);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(234, 56);
            this.Cancel.TabIndex = 2;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // Test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 326);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.NetException);
            this.Name = "Test";
            this.Text = "Test";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button NetException;
        private System.Windows.Forms.Button Cancel;
    }
}