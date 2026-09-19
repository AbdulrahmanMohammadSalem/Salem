using Ookii.Dialogs.WinForms;
using Salem.Extensions;
using Salem.Extensions.External;
using Salem.Utils;
using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;
using Workplace__WinForms_.Properties;

namespace Workplace__WinForms_ {
    public partial class Form3 : Form {
        public Form3() {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e) {
        }

        private void comboBox1_FontChanged(object sender, EventArgs e) {
        }
            
        private void fuiButton1_Click(object sender, EventArgs e) {
            int height;
            comboBox1.DrawMode = DrawMode.Normal;
            height = comboBox1.ItemHeight;
            comboBox1.DrawMode = DrawMode.OwnerDrawVariable;
            MessageBox.Show($"CorrectedItemHeight = {height}, FontHeight = {comboBox1.Font.Height}");
        }
    }

    public class c : ComboBox {
        public c() {
            
        }
    }
}
