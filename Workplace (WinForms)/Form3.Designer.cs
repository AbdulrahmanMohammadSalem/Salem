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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.salDropDownList1 = new Salem.Controls.SalDropDownList();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.Location = new System.Drawing.Point(242, 344);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(246, 31);
            this.comboBox1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(561, 405);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 31);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // salDropDownList1
            // 
            this.salDropDownList1.BackColor = System.Drawing.Color.AliceBlue;
            this.salDropDownList1.BorderColor = System.Drawing.Color.CornflowerBlue;
            this.salDropDownList1.DropDownWidth = 246;
            this.salDropDownList1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.salDropDownList1.Location = new System.Drawing.Point(242, 296);
            this.salDropDownList1.Name = "salDropDownList1";
            this.salDropDownList1.SelectedIndex = -1;
            this.salDropDownList1.Size = new System.Drawing.Size(246, 31);
            this.salDropDownList1.TabIndex = 1;
            this.salDropDownList1.TabStop = false;
            // 
            // Form3
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(787, 566);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.salDropDownList1);
            this.Controls.Add(this.comboBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form3";
            this.Text = "My Screen";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private Salem.Controls.SalDropDownList salDropDownList1;
        private System.Windows.Forms.Button button1;
    }
}