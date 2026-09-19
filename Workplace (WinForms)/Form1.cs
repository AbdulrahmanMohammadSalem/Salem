using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Workplace__WinForms_ {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void betterToolStrip1_LauncherClicked(object sender, EventArgs e) {
            MessageBox.Show("Launcher Clicked");
            
        }

        class Progress : ToolStripProgressBar {
            
        }

        private void toolStripButton13_Click(object sender, EventArgs e) {
            
        }

        private void toolStripButton13_MouseEnter(object sender, EventArgs e) {
            
        }
        private const ushort r = 8;
        private void panel1_Paint(object sender, PaintEventArgs e) {
            //Salem.Drawing.DrawingHelpers.DrawCustomShape(panel1, e.Graphics, new Salem.Drawing.ShapeInfo() { BorderColor = PredefinedColors.OfficeFlat_BorderColor, BorderDashStyle = System.Drawing.Drawing2D.DashStyle.Solid, BorderSize = 1, BottomLeftRadius = r, BottomRightRadius = r, TopLeftRadius = r, TopRightRadius = r });
        }

        private void panel1_SizeChanged(object sender, EventArgs e) {
            //panel1.Invalidate();
        }

        private void betterToolStrip1_Paint(object sender, PaintEventArgs e) {
            
        }

        private void Form1_Load(object sender, EventArgs e) {
            //Salem.Drawing.DrawingHelpers.ApplyCustomRegion(betterToolStrip1, new Salem.Drawing.ShapeInfo() { BottomLeftRadius = r, BottomRightRadius = r, TopLeftRadius = r, TopRightRadius = r });
        }
    }
}
