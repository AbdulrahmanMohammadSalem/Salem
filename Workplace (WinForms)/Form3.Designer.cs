namespace Workplace__WinForms_ {
    partial class Form3 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form3));
            this.salDropDownList1 = new Salem.Controls.SalDropDownList();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // salDropDownList1
            // 
            resources.ApplyResources(this.salDropDownList1, "salDropDownList1");
            this.salDropDownList1.Name = "salDropDownList1";
            this.salDropDownList1.Purpose = Salem.Controls.SalDropDownBase.SalDropDownPurpose.Countries;
            this.salDropDownList1.SelectedIndex = -1;
            this.salDropDownList1.Sorted = true;
            this.salDropDownList1.TabStop = false;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.label1.Name = "label1";
            // 
            // Form3
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.label1);
            this.Controls.Add(this.salDropDownList1);
            this.Name = "Form3";
            this.TitleBarColorMode = Salem.Drawing.ColorModes.Dark;
            this.Load += new System.EventHandler(this.Form3_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Salem.Controls.SalDropDownList salDropDownList1;
        private System.Windows.Forms.Label label1;
    }
}