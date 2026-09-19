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
            this.salDropDownList1 = new Salem.Controls.SalDropDownList();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // salDropDownList1
            // 
            this.salDropDownList1.DropDownWidth = 261;
            this.salDropDownList1.Font = new System.Drawing.Font("Readex Pro", 9F);
            this.salDropDownList1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.salDropDownList1.FormattingEnabled = false;
            this.salDropDownList1.Items.AddRange(new object[] {
            "الخيار الأول",
            "الخيار الثاني",
            "الخيار الثالث",
            "الخيار الرابع"});
            this.salDropDownList1.Location = new System.Drawing.Point(275, 303);
            this.salDropDownList1.Name = "salDropDownList1";
            this.salDropDownList1.Padding = new System.Windows.Forms.Padding(24, 0, 0, 0);
            this.salDropDownList1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.salDropDownList1.SelectedIndex = 0;
            this.salDropDownList1.Size = new System.Drawing.Size(183, 27);
            this.salDropDownList1.TabIndex = 0;
            this.salDropDownList1.TabStop = false;
            this.salDropDownList1.Text = "الخيار الأول";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(438, 63);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(275, 103);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Readex Pro", 9F);
            this.textBox1.Location = new System.Drawing.Point(275, 271);
            this.textBox1.Name = "textBox1";
            this.textBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.textBox1.Size = new System.Drawing.Size(183, 26);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "الخيار الأول";
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("Readex Pro", 9F);
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "الخيار الأول",
            "الخيار الثاني",
            "الخيار الثالث",
            "الخيار الرابع"});
            this.comboBox1.Location = new System.Drawing.Point(464, 303);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comboBox1.Size = new System.Drawing.Size(212, 34);
            this.comboBox1.TabIndex = 3;
            // 
            // Form3
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(762, 465);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.salDropDownList1);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form3";
            this.Padding = new System.Windows.Forms.Padding(66, 0, 0, 0);
            this.Text = "My Screen";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Salem.Controls.SalDropDownList salDropDownList1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}