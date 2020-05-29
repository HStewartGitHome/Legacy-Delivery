namespace LegacyWinForm.Forms
{
    partial class SaveOrder
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
            this.Cancel = new System.Windows.Forms.Button();
            this.SaveRouter = new System.Windows.Forms.Button();
            this.SaveLocal = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(339, 226);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 0;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // SaveRouter
            // 
            this.SaveRouter.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.SaveRouter.Location = new System.Drawing.Point(53, 40);
            this.SaveRouter.Name = "SaveRouter";
            this.SaveRouter.Size = new System.Drawing.Size(162, 32);
            this.SaveRouter.TabIndex = 1;
            this.SaveRouter.Text = "Save Router";
            this.SaveRouter.UseVisualStyleBackColor = true;
            this.SaveRouter.Click += new System.EventHandler(this.SaveRouter_Click);
            // 
            // SaveLocal
            // 
            this.SaveLocal.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.SaveLocal.Enabled = false;
            this.SaveLocal.Location = new System.Drawing.Point(53, 96);
            this.SaveLocal.Name = "SaveLocal";
            this.SaveLocal.Size = new System.Drawing.Size(162, 32);
            this.SaveLocal.TabIndex = 2;
            this.SaveLocal.Text = "Save Local";
            this.SaveLocal.UseVisualStyleBackColor = true;
            this.SaveLocal.Click += new System.EventHandler(this.SaveLocal_Click);
            // 
            // SaveOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 261);
            this.Controls.Add(this.SaveLocal);
            this.Controls.Add(this.SaveRouter);
            this.Controls.Add(this.Cancel);
            this.Name = "SaveOrder";
            this.Text = "SaveOrder";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button SaveRouter;
        private System.Windows.Forms.Button SaveLocal;
    }
}