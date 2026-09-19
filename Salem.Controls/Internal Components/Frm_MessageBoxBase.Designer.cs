namespace Salem.Controls.Internal_Utils {
    partial class Frm_MessageBoxBase {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_MessageBoxBase));
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_3 = new System.Windows.Forms.Button();
            this.btn_2 = new System.Windows.Forms.Button();
            this.btn_1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Variable Text", 10F);
            this.label1.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.label1.Location = new System.Drawing.Point(76, 31);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.MaximumSize = new System.Drawing.Size(422, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(419, 88);
            this.label1.TabIndex = 0;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Salem.Controls.Properties.Resources.MsgBx_Error;
            this.pictureBox1.Location = new System.Drawing.Point(28, 33);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(40, 40);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // btn_3
            // 
            this.btn_3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_3.Font = new System.Drawing.Font("Segoe UI Variable Text", 9F);
            this.btn_3.Location = new System.Drawing.Point(402, 183);
            this.btn_3.Margin = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btn_3.Name = "btn_3";
            this.btn_3.Size = new System.Drawing.Size(96, 32);
            this.btn_3.TabIndex = 2;
            this.btn_3.Text = "Cancel";
            this.btn_3.UseVisualStyleBackColor = true;
            // 
            // btn_2
            // 
            this.btn_2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_2.Font = new System.Drawing.Font("Segoe UI Variable Text", 9F);
            this.btn_2.Location = new System.Drawing.Point(294, 183);
            this.btn_2.Margin = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btn_2.Name = "btn_2";
            this.btn_2.Size = new System.Drawing.Size(96, 32);
            this.btn_2.TabIndex = 3;
            this.btn_2.Text = "No";
            this.btn_2.UseVisualStyleBackColor = true;
            // 
            // btn_1
            // 
            this.btn_1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_1.Font = new System.Drawing.Font("Segoe UI Variable Text", 9F);
            this.btn_1.Location = new System.Drawing.Point(186, 183);
            this.btn_1.Margin = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btn_1.Name = "btn_1";
            this.btn_1.Size = new System.Drawing.Size(96, 32);
            this.btn_1.TabIndex = 4;
            this.btn_1.Text = "Yes";
            this.btn_1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(28, 115);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(470, 100);
            this.panel1.TabIndex = 5;
            // 
            // Frm_MessageBoxBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(526, 231);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btn_1);
            this.Controls.Add(this.btn_2);
            this.Controls.Add(this.btn_3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(544, 1080);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(156, 128);
            this.Name = "Frm_MessageBoxBase";
            this.Padding = new System.Windows.Forms.Padding(28, 33, 28, 16);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Caption";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btn_3;
        private System.Windows.Forms.Button btn_2;
        private System.Windows.Forms.Button btn_1;
        private System.Windows.Forms.Panel panel1;
    }
}